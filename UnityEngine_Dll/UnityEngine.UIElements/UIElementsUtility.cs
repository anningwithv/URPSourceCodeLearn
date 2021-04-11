using System;
using System.Collections.Generic;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	internal class UIElementsUtility : IUIElementsUtility
	{
		private static Stack<IMGUIContainer> s_ContainerStack = new Stack<IMGUIContainer>();

		private static Dictionary<int, Panel> s_UIElementsCache = new Dictionary<int, Panel>();

		private static Event s_EventInstance = new Event();

		internal static Color editorPlayModeTintColor = Color.white;

		internal static Action<IMGUIContainer> s_BeginContainerCallback;

		internal static Action<IMGUIContainer> s_EndContainerCallback;

		private static UIElementsUtility s_Instance = new UIElementsUtility();

		internal static List<Panel> s_PanelsIterationList = new List<Panel>();

		internal static readonly string s_RepaintProfilerMarkerName = "UIElementsUtility.DoDispatch(Repaint Event)";

		internal static readonly string s_EventProfilerMarkerName = "UIElementsUtility.DoDispatch(Non Repaint Event)";

		private static readonly ProfilerMarker s_RepaintProfilerMarker = new ProfilerMarker(UIElementsUtility.s_RepaintProfilerMarkerName);

		private static readonly ProfilerMarker s_EventProfilerMarker = new ProfilerMarker(UIElementsUtility.s_EventProfilerMarkerName);

		private UIElementsUtility()
		{
			UIEventRegistration.RegisterUIElementSystem(this);
		}

		internal static IMGUIContainer GetCurrentIMGUIContainer()
		{
			bool flag = UIElementsUtility.s_ContainerStack.Count > 0;
			IMGUIContainer result;
			if (flag)
			{
				result = UIElementsUtility.s_ContainerStack.Peek();
			}
			else
			{
				result = null;
			}
			return result;
		}

		bool IUIElementsUtility.MakeCurrentIMGUIContainerDirty()
		{
			bool flag = UIElementsUtility.s_ContainerStack.Count > 0;
			bool result;
			if (flag)
			{
				UIElementsUtility.s_ContainerStack.Peek().MarkDirtyLayout();
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		bool IUIElementsUtility.TakeCapture()
		{
			bool flag = UIElementsUtility.s_ContainerStack.Count > 0;
			bool result;
			if (flag)
			{
				IMGUIContainer iMGUIContainer = UIElementsUtility.s_ContainerStack.Peek();
				IEventHandler capturingElement = iMGUIContainer.panel.GetCapturingElement(PointerId.mousePointerId);
				bool flag2 = capturingElement != null && capturingElement != iMGUIContainer;
				if (flag2)
				{
					Debug.Log("Should not grab hot control with an active capture");
				}
				iMGUIContainer.CaptureMouse();
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		bool IUIElementsUtility.ReleaseCapture()
		{
			PointerCaptureHelper.ReleaseEditorMouseCapture();
			return false;
		}

		bool IUIElementsUtility.ProcessEvent(int instanceID, IntPtr nativeEventPtr, ref bool eventHandled)
		{
			Panel panel;
			bool flag = nativeEventPtr != IntPtr.Zero && UIElementsUtility.s_UIElementsCache.TryGetValue(instanceID, out panel);
			bool result;
			if (flag)
			{
				bool flag2 = panel.contextType == ContextType.Editor;
				if (flag2)
				{
					UIElementsUtility.s_EventInstance.CopyFromPtr(nativeEventPtr);
					eventHandled = UIElementsUtility.DoDispatch(panel);
				}
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		bool IUIElementsUtility.CleanupRoots()
		{
			UIElementsUtility.s_EventInstance = null;
			UIElementsUtility.s_UIElementsCache = null;
			UIElementsUtility.s_ContainerStack = null;
			UIElementsUtility.s_BeginContainerCallback = null;
			UIElementsUtility.s_EndContainerCallback = null;
			EventDispatcher.ClearEditorDispatcher();
			return false;
		}

		bool IUIElementsUtility.EndContainerGUIFromException(Exception exception)
		{
			bool flag = UIElementsUtility.s_ContainerStack.Count > 0;
			if (flag)
			{
				GUIUtility.EndContainer();
				UIElementsUtility.s_ContainerStack.Pop();
			}
			return false;
		}

		void IUIElementsUtility.UpdateSchedulers()
		{
			UIElementsUtility.s_PanelsIterationList.Clear();
			UIElementsUtility.GetAllPanels(UIElementsUtility.s_PanelsIterationList, ContextType.Editor);
			foreach (Panel current in UIElementsUtility.s_PanelsIterationList)
			{
				current.timerEventScheduler.UpdateScheduledEvents();
				current.UpdateAnimations();
				current.UpdateBindings();
			}
		}

		void IUIElementsUtility.RequestRepaintForPanels(Action<ScriptableObject> repaintCallback)
		{
			Dictionary<int, Panel>.Enumerator panelsIterator = UIElementsUtility.GetPanelsIterator();
			while (panelsIterator.MoveNext())
			{
				KeyValuePair<int, Panel> current = panelsIterator.Current;
				Panel value = current.Value;
				bool flag = value.contextType != ContextType.Editor;
				if (!flag)
				{
					bool isDirty = value.isDirty;
					if (isDirty)
					{
						repaintCallback(value.ownerObject);
					}
				}
			}
		}

		public static void RegisterCachedPanel(int instanceID, Panel panel)
		{
			UIElementsUtility.s_UIElementsCache.Add(instanceID, panel);
		}

		public static void RemoveCachedPanel(int instanceID)
		{
			UIElementsUtility.s_UIElementsCache.Remove(instanceID);
		}

		public static bool TryGetPanel(int instanceID, out Panel panel)
		{
			return UIElementsUtility.s_UIElementsCache.TryGetValue(instanceID, out panel);
		}

		internal static void BeginContainerGUI(GUILayoutUtility.LayoutCache cache, Event evt, IMGUIContainer container)
		{
			bool useOwnerObjectGUIState = container.useOwnerObjectGUIState;
			if (useOwnerObjectGUIState)
			{
				GUIUtility.BeginContainerFromOwner(container.elementPanel.ownerObject);
			}
			else
			{
				GUIUtility.BeginContainer(container.guiState);
			}
			UIElementsUtility.s_ContainerStack.Push(container);
			GUIUtility.s_SkinMode = (int)container.contextType;
			GUIUtility.s_OriginalID = container.elementPanel.ownerObject.GetInstanceID();
			bool flag = Event.current == null;
			if (flag)
			{
				Event.current = evt;
			}
			else
			{
				Event.current.CopyFrom(evt);
			}
			bool flag2 = UIElementsUtility.s_BeginContainerCallback != null;
			if (flag2)
			{
				UIElementsUtility.s_BeginContainerCallback(container);
			}
			GUI.enabled = container.enabledInHierarchy;
			GUILayoutUtility.BeginContainer(cache);
			GUIUtility.ResetGlobalState();
		}

		internal static void EndContainerGUI(Event evt, Rect layoutSize)
		{
			bool flag = Event.current.type == EventType.Layout && UIElementsUtility.s_ContainerStack.Count > 0;
			if (flag)
			{
				GUILayoutUtility.LayoutFromContainer(layoutSize.width, layoutSize.height);
			}
			GUILayoutUtility.SelectIDList(GUIUtility.s_OriginalID, false);
			GUIContent.ClearStaticCache();
			bool flag2 = UIElementsUtility.s_ContainerStack.Count > 0;
			if (flag2)
			{
				IMGUIContainer obj = UIElementsUtility.s_ContainerStack.Peek();
				bool flag3 = UIElementsUtility.s_EndContainerCallback != null;
				if (flag3)
				{
					UIElementsUtility.s_EndContainerCallback(obj);
				}
			}
			evt.CopyFrom(Event.current);
			bool flag4 = UIElementsUtility.s_ContainerStack.Count > 0;
			if (flag4)
			{
				GUIUtility.EndContainer();
				UIElementsUtility.s_ContainerStack.Pop();
			}
		}

		internal static EventBase CreateEvent(Event systemEvent)
		{
			return UIElementsUtility.CreateEvent(systemEvent, systemEvent.rawType);
		}

		internal static EventBase CreateEvent(Event systemEvent, EventType eventType)
		{
			EventBase pooled;
			switch (eventType)
			{
			case EventType.MouseDown:
			{
				bool flag = PointerDeviceState.HasAdditionalPressedButtons(PointerId.mousePointerId, systemEvent.button);
				if (flag)
				{
					pooled = PointerEventBase<PointerMoveEvent>.GetPooled(systemEvent);
					return pooled;
				}
				pooled = PointerEventBase<PointerDownEvent>.GetPooled(systemEvent);
				return pooled;
			}
			case EventType.MouseUp:
			{
				bool flag2 = PointerDeviceState.HasAdditionalPressedButtons(PointerId.mousePointerId, systemEvent.button);
				if (flag2)
				{
					pooled = PointerEventBase<PointerMoveEvent>.GetPooled(systemEvent);
					return pooled;
				}
				pooled = PointerEventBase<PointerUpEvent>.GetPooled(systemEvent);
				return pooled;
			}
			case EventType.MouseMove:
				pooled = PointerEventBase<PointerMoveEvent>.GetPooled(systemEvent);
				return pooled;
			case EventType.MouseDrag:
				pooled = PointerEventBase<PointerMoveEvent>.GetPooled(systemEvent);
				return pooled;
			case EventType.KeyDown:
				pooled = KeyboardEventBase<KeyDownEvent>.GetPooled(systemEvent);
				return pooled;
			case EventType.KeyUp:
				pooled = KeyboardEventBase<KeyUpEvent>.GetPooled(systemEvent);
				return pooled;
			case EventType.ScrollWheel:
				pooled = WheelEvent.GetPooled(systemEvent);
				return pooled;
			case EventType.DragUpdated:
				pooled = DragUpdatedEvent.GetPooled(systemEvent);
				return pooled;
			case EventType.DragPerform:
				pooled = MouseEventBase<DragPerformEvent>.GetPooled(systemEvent);
				return pooled;
			case EventType.ValidateCommand:
				pooled = CommandEventBase<ValidateCommandEvent>.GetPooled(systemEvent);
				return pooled;
			case EventType.ExecuteCommand:
				pooled = CommandEventBase<ExecuteCommandEvent>.GetPooled(systemEvent);
				return pooled;
			case EventType.DragExited:
				pooled = DragExitedEvent.GetPooled(systemEvent);
				return pooled;
			case EventType.ContextClick:
				pooled = MouseEventBase<ContextClickEvent>.GetPooled(systemEvent);
				return pooled;
			case EventType.MouseEnterWindow:
				pooled = MouseEventBase<MouseEnterWindowEvent>.GetPooled(systemEvent);
				return pooled;
			case EventType.MouseLeaveWindow:
				pooled = MouseLeaveWindowEvent.GetPooled(systemEvent);
				return pooled;
			}
			pooled = IMGUIEvent.GetPooled(systemEvent);
			return pooled;
		}

		private static bool DoDispatch(BaseVisualElementPanel panel)
		{
			bool result = false;
			bool flag = UIElementsUtility.s_EventInstance.type == EventType.Repaint;
			if (flag)
			{
				using (UIElementsUtility.s_RepaintProfilerMarker.Auto())
				{
					panel.Repaint(UIElementsUtility.s_EventInstance);
				}
				IPanelDebug expr_46 = panel.panelDebug;
				Panel expr_57 = ((expr_46 != null) ? expr_46.debuggerOverlayPanel : null) as Panel;
				if (expr_57 != null)
				{
					expr_57.Repaint(UIElementsUtility.s_EventInstance);
				}
				result = (panel.IMGUIContainersCount > 0);
			}
			else
			{
				panel.ValidateLayout();
				using (EventBase eventBase = UIElementsUtility.CreateEvent(UIElementsUtility.s_EventInstance))
				{
					bool flag2 = UIElementsUtility.s_EventInstance.type == EventType.Used || UIElementsUtility.s_EventInstance.type == EventType.Layout || UIElementsUtility.s_EventInstance.type == EventType.ExecuteCommand || UIElementsUtility.s_EventInstance.type == EventType.ValidateCommand;
					using (UIElementsUtility.s_EventProfilerMarker.Auto())
					{
						panel.SendEvent(eventBase, flag2 ? DispatchMode.Immediate : DispatchMode.Default);
					}
					bool isPropagationStopped = eventBase.isPropagationStopped;
					if (isPropagationStopped)
					{
						panel.visualTree.IncrementVersion(VersionChangeType.Repaint);
						result = true;
					}
				}
			}
			return result;
		}

		internal static void GetAllPanels(List<Panel> panels, ContextType contextType)
		{
			Dictionary<int, Panel>.Enumerator panelsIterator = UIElementsUtility.GetPanelsIterator();
			while (panelsIterator.MoveNext())
			{
				KeyValuePair<int, Panel> current = panelsIterator.Current;
				bool flag = current.Value.contextType == contextType;
				if (flag)
				{
					current = panelsIterator.Current;
					panels.Add(current.Value);
				}
			}
		}

		internal static Dictionary<int, Panel>.Enumerator GetPanelsIterator()
		{
			return UIElementsUtility.s_UIElementsCache.GetEnumerator();
		}

		internal static Panel FindOrCreateEditorPanel(ScriptableObject ownerObject)
		{
			Panel panel;
			bool flag = !UIElementsUtility.s_UIElementsCache.TryGetValue(ownerObject.GetInstanceID(), out panel);
			if (flag)
			{
				panel = Panel.CreateEditorPanel(ownerObject);
				UIElementsUtility.RegisterCachedPanel(ownerObject.GetInstanceID(), panel);
			}
			else
			{
				Debug.Assert(ContextType.Editor == panel.contextType, "Panel is not an editor panel.");
			}
			return panel;
		}

		internal static void InMemoryAssetsHaveBeenChanged()
		{
		}
	}
}
