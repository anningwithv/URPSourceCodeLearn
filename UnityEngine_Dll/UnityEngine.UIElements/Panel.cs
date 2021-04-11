using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	internal class Panel : BaseVisualElementPanel
	{
		private VisualElement m_RootContainer;

		private VisualTreeUpdater m_VisualTreeUpdater;

		private string m_PanelName;

		private uint m_Version = 0u;

		private uint m_RepaintVersion = 0u;

		private ProfilerMarker m_MarkerBeforeUpdate;

		private ProfilerMarker m_MarkerUpdate;

		private ProfilerMarker m_MarkerLayout;

		private ProfilerMarker m_MarkerBindings;

		private ProfilerMarker m_MarkerAnimations;

		private static ProfilerMarker s_MarkerPickAll = new ProfilerMarker("Panel.PickAll");

		private TimerEventScheduler m_Scheduler;

		private Shader m_StandardShader;

		private bool m_ValidatingLayout;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		internal static event Action<Panel> beforeAnyRepaint;

		public sealed override VisualElement visualTree
		{
			get
			{
				return this.m_RootContainer;
			}
		}

		public sealed override EventDispatcher dispatcher
		{
			get;
			set;
		}

		public TimerEventScheduler timerEventScheduler
		{
			get
			{
				TimerEventScheduler arg_19_0;
				if ((arg_19_0 = this.m_Scheduler) == null)
				{
					arg_19_0 = (this.m_Scheduler = new TimerEventScheduler());
				}
				return arg_19_0;
			}
		}

		internal override IScheduler scheduler
		{
			get
			{
				return this.timerEventScheduler;
			}
		}

		public override ScriptableObject ownerObject
		{
			get;
			protected set;
		}

		public override ContextType contextType
		{
			get;
			protected set;
		}

		public override SavePersistentViewData saveViewData
		{
			get;
			set;
		}

		public override GetViewDataDictionary getViewDataDictionary
		{
			get;
			set;
		}

		public sealed override FocusController focusController
		{
			get;
			set;
		}

		public sealed override IEventInterpreter eventInterpreter
		{
			get;
			set;
		}

		public override EventInterests IMGUIEventInterests
		{
			get;
			set;
		}

		internal static LoadResourceFunction loadResourceFunc
		{
			private get;
			set;
		}

		internal string name
		{
			get
			{
				return this.m_PanelName;
			}
			set
			{
				this.m_PanelName = value;
				this.CreateMarkers();
			}
		}

		internal static TimeMsFunction TimeSinceStartup
		{
			private get;
			set;
		}

		public override int IMGUIContainersCount
		{
			get;
			set;
		}

		public override IMGUIContainer rootIMGUIContainer
		{
			get;
			set;
		}

		internal override uint version
		{
			get
			{
				return this.m_Version;
			}
		}

		internal override uint repaintVersion
		{
			get
			{
				return this.m_RepaintVersion;
			}
		}

		internal override Shader standardShader
		{
			get
			{
				return this.m_StandardShader;
			}
			set
			{
				bool flag = this.m_StandardShader != value;
				if (flag)
				{
					this.m_StandardShader = value;
					base.InvokeStandardShaderChanged();
				}
			}
		}

		internal static UnityEngine.Object LoadResource(string pathName, Type type, float dpiScaling)
		{
			bool flag = Panel.loadResourceFunc != null;
			UnityEngine.Object result;
			if (flag)
			{
				result = Panel.loadResourceFunc(pathName, type, dpiScaling);
			}
			else
			{
				result = Resources.Load(pathName, type);
			}
			return result;
		}

		internal void Focus()
		{
			FocusController expr_07 = this.focusController;
			if (expr_07 != null)
			{
				expr_07.SetFocusToLastFocusedElement();
			}
		}

		internal void Blur()
		{
			FocusController expr_07 = this.focusController;
			if (expr_07 != null)
			{
				expr_07.BlurLastFocusedElement();
			}
		}

		private void CreateMarkers()
		{
			bool flag = !string.IsNullOrEmpty(this.m_PanelName);
			if (flag)
			{
				this.m_MarkerBeforeUpdate = new ProfilerMarker("Panel.BeforeUpdate." + this.m_PanelName);
				this.m_MarkerUpdate = new ProfilerMarker("Panel.Update." + this.m_PanelName);
				this.m_MarkerLayout = new ProfilerMarker("Panel.Layout." + this.m_PanelName);
				this.m_MarkerBindings = new ProfilerMarker("Panel.Bindings." + this.m_PanelName);
				this.m_MarkerAnimations = new ProfilerMarker("Panel.Animations." + this.m_PanelName);
			}
			else
			{
				this.m_MarkerBeforeUpdate = new ProfilerMarker("Panel.BeforeUpdate");
				this.m_MarkerUpdate = new ProfilerMarker("Panel.Update");
				this.m_MarkerLayout = new ProfilerMarker("Panel.Layout");
				this.m_MarkerBindings = new ProfilerMarker("Panel.Bindings");
				this.m_MarkerAnimations = new ProfilerMarker("Panel.Animations");
			}
		}

		internal static Panel CreateEditorPanel(ScriptableObject ownerObject)
		{
			return new Panel(ownerObject, ContextType.Editor, EventDispatcher.editorDispatcher);
		}

		public Panel(ScriptableObject ownerObject, ContextType contextType, EventDispatcher dispatcher)
		{
			this.<eventInterpreter>k__BackingField = EventInterpreter.s_Instance;
			this.m_ValidatingLayout = false;
			base..ctor();
			this.ownerObject = ownerObject;
			this.contextType = contextType;
			this.dispatcher = dispatcher;
			this.repaintData = new RepaintData();
			this.cursorManager = new CursorManager();
			base.contextualMenuManager = null;
			this.m_VisualTreeUpdater = new VisualTreeUpdater(this);
			this.m_RootContainer = new VisualElement
			{
				name = VisualElementUtils.GetUniqueName("unity-panel-container"),
				viewDataKey = "PanelContainer"
			};
			this.visualTree.SetPanel(this);
			this.focusController = new FocusController(new VisualElementFocusRing(this.visualTree, VisualElementFocusRing.DefaultFocusOrder.ChildOrder));
			this.CreateMarkers();
			base.InvokeHierarchyChanged(this.visualTree, HierarchyChangeType.Add);
		}

		protected override void Dispose(bool disposing)
		{
			bool disposed = base.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.m_VisualTreeUpdater.Dispose();
				}
				base.Dispose(disposing);
			}
		}

		public static long TimeSinceStartupMs()
		{
			TimeMsFunction expr_06 = Panel.TimeSinceStartup;
			return (expr_06 != null) ? expr_06() : Panel.DefaultTimeSinceStartupMs();
		}

		internal static long DefaultTimeSinceStartupMs()
		{
			return (long)(Time.realtimeSinceStartup * 1000f);
		}

		internal static VisualElement PickAllWithoutValidatingLayout(VisualElement root, Vector2 point)
		{
			return Panel.PickAll(root, point, null);
		}

		private static VisualElement PickAll(VisualElement root, Vector2 point, List<VisualElement> picked = null)
		{
			Panel.s_MarkerPickAll.Begin();
			VisualElement result = Panel.PerformPick(root, point, picked);
			Panel.s_MarkerPickAll.End();
			return result;
		}

		private static VisualElement PerformPick(VisualElement root, Vector2 point, List<VisualElement> picked = null)
		{
			bool flag = root.resolvedStyle.display == DisplayStyle.None;
			VisualElement result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = root.pickingMode == PickingMode.Ignore && root.hierarchy.childCount == 0;
				if (flag2)
				{
					result = null;
				}
				else
				{
					bool flag3 = !root.worldBoundingBox.Contains(point);
					if (flag3)
					{
						result = null;
					}
					else
					{
						Vector2 localPoint = root.WorldToLocal(point);
						bool flag4 = root.ContainsPoint(localPoint);
						bool flag5 = !flag4 && root.ShouldClip();
						if (flag5)
						{
							result = null;
						}
						else
						{
							VisualElement visualElement = null;
							int childCount = root.hierarchy.childCount;
							for (int i = childCount - 1; i >= 0; i--)
							{
								VisualElement root2 = root.hierarchy[i];
								VisualElement visualElement2 = Panel.PerformPick(root2, point, picked);
								bool flag6 = visualElement == null && visualElement2 != null && visualElement2.visible;
								if (flag6)
								{
									bool flag7 = picked == null;
									if (flag7)
									{
										result = visualElement2;
										return result;
									}
									visualElement = visualElement2;
								}
							}
							bool flag8 = (root.enabledInHierarchy && root.visible && root.pickingMode == PickingMode.Position) & flag4;
							if (flag8)
							{
								if (picked != null)
								{
									picked.Add(root);
								}
								bool flag9 = visualElement == null;
								if (flag9)
								{
									visualElement = root;
								}
							}
							result = visualElement;
						}
					}
				}
			}
			return result;
		}

		public override VisualElement PickAll(Vector2 point, List<VisualElement> picked)
		{
			this.ValidateLayout();
			bool flag = picked != null;
			if (flag)
			{
				picked.Clear();
			}
			return Panel.PickAll(this.visualTree, point, picked);
		}

		public override VisualElement Pick(Vector2 point)
		{
			this.ValidateLayout();
			Vector2 a;
			bool flag;
			VisualElement topElementUnderPointer = this.m_TopElementUnderPointers.GetTopElementUnderPointer(PointerId.mousePointerId, out a, out flag);
			bool flag2 = !flag && (a - point).sqrMagnitude < 0.25f;
			VisualElement result;
			if (flag2)
			{
				result = topElementUnderPointer;
			}
			else
			{
				result = Panel.PickAll(this.visualTree, point, null);
			}
			return result;
		}

		public override void ValidateLayout()
		{
			bool flag = !this.m_ValidatingLayout;
			if (flag)
			{
				this.m_ValidatingLayout = true;
				this.m_MarkerLayout.Begin();
				this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Styles);
				this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Layout);
				this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.TransformClip);
				this.m_MarkerLayout.End();
				this.m_ValidatingLayout = false;
			}
		}

		public override void UpdateAnimations()
		{
			this.m_MarkerAnimations.Begin();
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Animation);
			this.m_MarkerAnimations.End();
		}

		public override void UpdateBindings()
		{
			this.m_MarkerBindings.Begin();
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Bindings);
			this.m_MarkerBindings.End();
		}

		public override void ApplyStyles()
		{
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Styles);
		}

		private void UpdateForRepaint()
		{
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.ViewData);
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Styles);
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Layout);
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.TransformClip);
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Repaint);
		}

		public override void DirtyStyleSheets()
		{
			this.m_VisualTreeUpdater.DirtyStyleSheets();
		}

		public override void Repaint(Event e)
		{
			bool flag = this.contextType == ContextType.Editor;
			if (flag)
			{
				UnityEngine.Debug.Assert(GUIClip.Internal_GetCount() == 0, "UIElement is not compatible with IMGUI GUIClips, only GUIClip.ParentClipScope");
			}
			this.m_RepaintVersion = this.version;
			bool flag2 = this.contextType == ContextType.Editor;
			if (flag2)
			{
				base.pixelsPerPoint = GUIUtility.pixelsPerPoint;
			}
			this.repaintData.repaintEvent = e;
			using (this.m_MarkerBeforeUpdate.Auto())
			{
				base.InvokeBeforeUpdate();
			}
			Action<Panel> expr_7E = Panel.beforeAnyRepaint;
			if (expr_7E != null)
			{
				expr_7E(this);
			}
			using (this.m_MarkerUpdate.Auto())
			{
				this.UpdateForRepaint();
			}
			IPanelDebug expr_B7 = base.panelDebug;
			if (expr_B7 != null)
			{
				expr_B7.Refresh();
			}
		}

		internal override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			this.m_Version += 1u;
			this.m_VisualTreeUpdater.OnVersionChanged(ve, versionChangeType);
			IPanelDebug expr_23 = base.panelDebug;
			if (expr_23 != null)
			{
				expr_23.OnVersionChanged(ve, versionChangeType);
			}
		}

		internal override void SetUpdater(IVisualTreeUpdater updater, VisualTreeUpdatePhase phase)
		{
			this.m_VisualTreeUpdater.SetUpdater(updater, phase);
		}

		internal override IVisualTreeUpdater GetUpdater(VisualTreeUpdatePhase phase)
		{
			return this.m_VisualTreeUpdater.GetUpdater(phase);
		}
	}
}
