using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	public class IMGUIContainer : VisualElement, IDisposable
	{
		public new class UxmlFactory : UxmlFactory<IMGUIContainer, IMGUIContainer.UxmlTraits>
		{
		}

		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}

			public UxmlTraits()
			{
				base.focusIndex.defaultValue = 0;
				base.focusable.defaultValue = true;
			}
		}

		private struct GUIGlobals
		{
			public Matrix4x4 matrix;

			public Color color;

			public Color contentColor;

			public Color backgroundColor;

			public bool enabled;

			public bool changed;

			public int displayIndex;
		}

		private Action m_OnGUIHandler;

		private ObjectGUIState m_ObjectGUIState;

		internal bool useOwnerObjectGUIState;

		private bool m_CullingEnabled = false;

		private bool m_IsFocusDelegated = false;

		private bool m_RefreshCachedLayout = true;

		private GUILayoutUtility.LayoutCache m_Cache = null;

		private Rect m_CachedClippingRect = Rect.zero;

		private Matrix4x4 m_CachedTransform = Matrix4x4.identity;

		private bool lostFocus = false;

		private bool receivedFocus = false;

		private FocusChangeDirection focusChangeDirection = FocusChangeDirection.unspecified;

		private bool hasFocusableControls = false;

		private int newKeyboardFocusControlID = 0;

		public static readonly string ussClassName = "unity-imgui-container";

		private IMGUIContainer.GUIGlobals m_GUIGlobals;

		public Action onGUIHandler
		{
			get
			{
				return this.m_OnGUIHandler;
			}
			set
			{
				bool flag = this.m_OnGUIHandler != value;
				if (flag)
				{
					this.m_OnGUIHandler = value;
					base.IncrementVersion(VersionChangeType.Layout);
					base.IncrementVersion(VersionChangeType.Repaint);
				}
			}
		}

		internal ObjectGUIState guiState
		{
			get
			{
				Debug.Assert(!this.useOwnerObjectGUIState);
				bool flag = this.m_ObjectGUIState == null;
				if (flag)
				{
					this.m_ObjectGUIState = new ObjectGUIState();
				}
				return this.m_ObjectGUIState;
			}
		}

		internal Rect lastWorldClip
		{
			get;
			set;
		}

		public bool cullingEnabled
		{
			get
			{
				return this.m_CullingEnabled;
			}
			set
			{
				this.m_CullingEnabled = value;
				base.IncrementVersion(VersionChangeType.Repaint);
			}
		}

		private GUILayoutUtility.LayoutCache cache
		{
			get
			{
				bool flag = this.m_Cache == null;
				if (flag)
				{
					this.m_Cache = new GUILayoutUtility.LayoutCache(-1);
				}
				return this.m_Cache;
			}
		}

		private float layoutMeasuredWidth
		{
			get
			{
				return Mathf.Ceil(this.cache.topLevel.maxWidth);
			}
		}

		private float layoutMeasuredHeight
		{
			get
			{
				return Mathf.Ceil(this.cache.topLevel.maxHeight);
			}
		}

		public ContextType contextType
		{
			get;
			set;
		}

		internal bool focusOnlyIfHasFocusableControls
		{
			get;
			set;
		}

		public override bool canGrabFocus
		{
			get
			{
				return this.focusOnlyIfHasFocusableControls ? (this.hasFocusableControls && base.canGrabFocus) : base.canGrabFocus;
			}
		}

		public IMGUIContainer() : this(null)
		{
		}

		public IMGUIContainer(Action onGUIHandler)
		{
			this.<focusOnlyIfHasFocusableControls>k__BackingField = true;
			base..ctor();
			this.isIMGUIContainer = true;
			base.AddToClassList(IMGUIContainer.ussClassName);
			this.onGUIHandler = onGUIHandler;
			this.contextType = ContextType.Editor;
			base.focusable = true;
			base.requireMeasureFunction = true;
			base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
		}

		private void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			this.lastWorldClip = base.elementPanel.repaintData.currentWorldClip;
			mgc.painter.DrawImmediate(new Action(this.DoIMGUIRepaint), this.cullingEnabled);
		}

		private void SaveGlobals()
		{
			this.m_GUIGlobals.matrix = GUI.matrix;
			this.m_GUIGlobals.color = GUI.color;
			this.m_GUIGlobals.contentColor = GUI.contentColor;
			this.m_GUIGlobals.backgroundColor = GUI.backgroundColor;
			this.m_GUIGlobals.enabled = GUI.enabled;
			this.m_GUIGlobals.changed = GUI.changed;
			bool flag = Event.current != null;
			if (flag)
			{
				this.m_GUIGlobals.displayIndex = Event.current.displayIndex;
			}
		}

		private void RestoreGlobals()
		{
			GUI.matrix = this.m_GUIGlobals.matrix;
			GUI.color = this.m_GUIGlobals.color;
			GUI.contentColor = this.m_GUIGlobals.contentColor;
			GUI.backgroundColor = this.m_GUIGlobals.backgroundColor;
			GUI.enabled = this.m_GUIGlobals.enabled;
			GUI.changed = this.m_GUIGlobals.changed;
			bool flag = Event.current != null;
			if (flag)
			{
				Event.current.displayIndex = this.m_GUIGlobals.displayIndex;
			}
		}

		private void DoOnGUI(Event evt, Matrix4x4 parentTransform, Rect clippingRect, bool isComputingLayout, Rect layoutSize, Action onGUIHandler, bool canAffectFocus = true)
		{
			bool flag = onGUIHandler == null || base.panel == null;
			if (!flag)
			{
				int num = GUIClip.Internal_GetCount();
				this.SaveGlobals();
				float layoutMeasuredWidth = this.layoutMeasuredWidth;
				float layoutMeasuredHeight = this.layoutMeasuredHeight;
				UIElementsUtility.BeginContainerGUI(this.cache, evt, this);
				GUI.color = UIElementsUtility.editorPlayModeTintColor;
				bool flag2 = Event.current.type != EventType.Layout;
				if (flag2)
				{
					bool flag3 = this.lostFocus;
					if (flag3)
					{
						bool flag4 = this.focusController != null;
						if (flag4)
						{
							bool flag5 = GUIUtility.OwnsId(GUIUtility.keyboardControl);
							if (flag5)
							{
								GUIUtility.keyboardControl = 0;
								this.focusController.imguiKeyboardControl = 0;
							}
						}
						this.lostFocus = false;
					}
					bool flag6 = this.receivedFocus;
					if (flag6)
					{
						bool flag7 = this.hasFocusableControls;
						if (flag7)
						{
							bool flag8 = this.focusChangeDirection != FocusChangeDirection.unspecified && this.focusChangeDirection != FocusChangeDirection.none;
							if (flag8)
							{
								bool flag9 = this.focusChangeDirection == VisualElementFocusChangeDirection.left;
								if (flag9)
								{
									GUIUtility.SetKeyboardControlToLastControlId();
								}
								else
								{
									bool flag10 = this.focusChangeDirection == VisualElementFocusChangeDirection.right;
									if (flag10)
									{
										GUIUtility.SetKeyboardControlToFirstControlId();
									}
								}
							}
							else
							{
								bool flag11 = GUIUtility.keyboardControl == 0 && this.m_IsFocusDelegated;
								if (flag11)
								{
									GUIUtility.SetKeyboardControlToFirstControlId();
								}
							}
						}
						bool flag12 = this.focusController != null;
						if (flag12)
						{
							bool flag13 = this.focusController.imguiKeyboardControl != GUIUtility.keyboardControl && this.focusChangeDirection != FocusChangeDirection.unspecified;
							if (flag13)
							{
								this.newKeyboardFocusControlID = GUIUtility.keyboardControl;
							}
							this.focusController.imguiKeyboardControl = GUIUtility.keyboardControl;
						}
						this.receivedFocus = false;
						this.focusChangeDirection = FocusChangeDirection.unspecified;
					}
				}
				EventType type = Event.current.type;
				bool flag14 = false;
				try
				{
					using (new GUIClip.ParentClipScope(parentTransform, clippingRect))
					{
						onGUIHandler();
					}
				}
				catch (Exception exception)
				{
					bool flag15 = type == EventType.Layout;
					if (!flag15)
					{
						throw;
					}
					flag14 = GUIUtility.IsExitGUIException(exception);
					bool flag16 = !flag14;
					if (flag16)
					{
						Debug.LogException(exception);
					}
				}
				finally
				{
					bool flag17 = Event.current.type != EventType.Layout & canAffectFocus;
					if (flag17)
					{
						int keyboardControl = GUIUtility.keyboardControl;
						int num2 = GUIUtility.CheckForTabEvent(Event.current);
						bool flag18 = this.focusController != null;
						if (flag18)
						{
							bool flag19 = num2 < 0;
							if (flag19)
							{
								Focusable leafFocusedElement = this.focusController.GetLeafFocusedElement();
								Focusable focusable = null;
								using (KeyDownEvent pooled = KeyboardEventBase<KeyDownEvent>.GetPooled('\t', KeyCode.Tab, (num2 == -1) ? EventModifiers.None : EventModifiers.Shift))
								{
									focusable = this.focusController.SwitchFocusOnEvent(pooled);
								}
								bool flag20 = leafFocusedElement == this;
								if (flag20)
								{
									bool flag21 = focusable == this;
									if (flag21)
									{
										bool flag22 = num2 == -2;
										if (flag22)
										{
											GUIUtility.SetKeyboardControlToLastControlId();
										}
										else
										{
											bool flag23 = num2 == -1;
											if (flag23)
											{
												GUIUtility.SetKeyboardControlToFirstControlId();
											}
										}
										this.newKeyboardFocusControlID = GUIUtility.keyboardControl;
										this.focusController.imguiKeyboardControl = GUIUtility.keyboardControl;
									}
									else
									{
										GUIUtility.keyboardControl = 0;
										this.focusController.imguiKeyboardControl = 0;
									}
								}
							}
							else
							{
								bool flag24 = num2 > 0;
								if (flag24)
								{
									this.focusController.imguiKeyboardControl = GUIUtility.keyboardControl;
									this.newKeyboardFocusControlID = GUIUtility.keyboardControl;
								}
								else
								{
									bool flag25 = num2 == 0;
									if (flag25)
									{
										bool flag26 = type == EventType.MouseDown && !this.focusOnlyIfHasFocusableControls;
										if (flag26)
										{
											this.focusController.SyncIMGUIFocus(GUIUtility.keyboardControl, this, true);
										}
										else
										{
											bool flag27 = keyboardControl != GUIUtility.keyboardControl || type == EventType.MouseDown;
											if (flag27)
											{
												this.focusController.SyncIMGUIFocus(GUIUtility.keyboardControl, this, false);
											}
											else
											{
												bool flag28 = GUIUtility.keyboardControl != this.focusController.imguiKeyboardControl;
												if (flag28)
												{
													this.newKeyboardFocusControlID = GUIUtility.keyboardControl;
													bool flag29 = this.focusController.GetLeafFocusedElement() == this;
													if (flag29)
													{
														this.focusController.imguiKeyboardControl = GUIUtility.keyboardControl;
													}
													else
													{
														this.focusController.SyncIMGUIFocus(GUIUtility.keyboardControl, this, false);
													}
												}
											}
										}
									}
								}
							}
						}
						this.hasFocusableControls = GUIUtility.HasFocusableControls();
					}
				}
				UIElementsUtility.EndContainerGUI(evt, layoutSize);
				this.RestoreGlobals();
				bool flag30 = evt.type == EventType.Layout && (!Mathf.Approximately(layoutMeasuredWidth, this.layoutMeasuredWidth) || !Mathf.Approximately(layoutMeasuredHeight, this.layoutMeasuredHeight));
				if (flag30)
				{
					bool flag31 = isComputingLayout && clippingRect == Rect.zero;
					if (flag31)
					{
						base.schedule.Execute(delegate
						{
							base.IncrementVersion(VersionChangeType.Layout);
						});
					}
					else
					{
						base.IncrementVersion(VersionChangeType.Layout);
					}
				}
				bool flag32 = !flag14;
				if (flag32)
				{
					bool flag33 = evt.type != EventType.Ignore && evt.type != EventType.Used;
					if (flag33)
					{
						int num3 = GUIClip.Internal_GetCount();
						bool flag34 = num3 > num;
						if (flag34)
						{
							Debug.LogError("GUI Error: You are pushing more GUIClips than you are popping. Make sure they are balanced.");
						}
						else
						{
							bool flag35 = num3 < num;
							if (flag35)
							{
								Debug.LogError("GUI Error: You are popping more GUIClips than you are pushing. Make sure they are balanced.");
							}
						}
					}
				}
				while (GUIClip.Internal_GetCount() > num)
				{
					GUIClip.Internal_Pop();
				}
				bool flag36 = evt.type == EventType.Used;
				if (flag36)
				{
					base.IncrementVersion(VersionChangeType.Repaint);
				}
			}
		}

		public void MarkDirtyLayout()
		{
			this.m_RefreshCachedLayout = true;
			base.IncrementVersion(VersionChangeType.Layout);
		}

		public override void HandleEvent(EventBase evt)
		{
			base.HandleEvent(evt);
			bool flag = evt == null;
			if (!flag)
			{
				bool flag2 = evt.propagationPhase != PropagationPhase.TrickleDown && evt.propagationPhase != PropagationPhase.AtTarget && evt.propagationPhase != PropagationPhase.BubbleUp;
				if (!flag2)
				{
					bool flag3 = evt.imguiEvent == null;
					if (!flag3)
					{
						bool isPropagationStopped = evt.isPropagationStopped;
						if (!isPropagationStopped)
						{
							bool flag4 = this.SendEventToIMGUI(evt, true, true);
							if (flag4)
							{
								evt.StopPropagation();
								evt.PreventDefault();
							}
						}
					}
				}
			}
		}

		private void DoIMGUIRepaint()
		{
			Matrix4x4 currentOffset = base.elementPanel.repaintData.currentOffset;
			this.m_CachedClippingRect = VisualElement.ComputeAAAlignedBound(base.worldClip, currentOffset);
			this.m_CachedTransform = currentOffset * base.worldTransform;
			this.HandleIMGUIEvent(base.elementPanel.repaintData.repaintEvent, this.m_CachedTransform, this.m_CachedClippingRect, this.onGUIHandler, true);
		}

		internal bool SendEventToIMGUI(EventBase evt, bool canAffectFocus = true, bool verifyBounds = true)
		{
			bool flag = evt is IPointerEvent;
			bool result;
			if (flag)
			{
				bool flag2 = evt.imguiEvent != null && evt.imguiEvent.isDirectManipulationDevice;
				if (flag2)
				{
					bool flag3 = false;
					EventType rawType = evt.imguiEvent.rawType;
					bool flag4 = evt is PointerDownEvent;
					if (flag4)
					{
						flag3 = true;
						evt.imguiEvent.type = EventType.TouchDown;
					}
					else
					{
						bool flag5 = evt is PointerUpEvent;
						if (flag5)
						{
							flag3 = true;
							evt.imguiEvent.type = EventType.TouchUp;
						}
						else
						{
							bool flag6 = evt is PointerMoveEvent && evt.imguiEvent.rawType == EventType.MouseDrag;
							if (flag6)
							{
								flag3 = true;
								evt.imguiEvent.type = EventType.TouchMove;
							}
							else
							{
								bool flag7 = evt is PointerLeaveEvent;
								if (flag7)
								{
									flag3 = true;
									evt.imguiEvent.type = EventType.TouchLeave;
								}
								else
								{
									bool flag8 = evt is PointerEnterEvent;
									if (flag8)
									{
										flag3 = true;
										evt.imguiEvent.type = EventType.TouchEnter;
									}
									else
									{
										bool flag9 = evt is PointerStationaryEvent;
										if (flag9)
										{
											flag3 = true;
											evt.imguiEvent.type = EventType.TouchStationary;
										}
									}
								}
							}
						}
					}
					bool flag10 = flag3;
					if (flag10)
					{
						bool flag11 = this.SendEventToIMGUIRaw(evt, canAffectFocus, verifyBounds);
						evt.imguiEvent.type = rawType;
						result = flag11;
						return result;
					}
				}
				result = false;
			}
			else
			{
				result = this.SendEventToIMGUIRaw(evt, canAffectFocus, verifyBounds);
			}
			return result;
		}

		private bool SendEventToIMGUIRaw(EventBase evt, bool canAffectFocus, bool verifyBounds)
		{
			bool flag = verifyBounds && !this.VerifyBounds(evt);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2;
				using (new EventDebuggerLogIMGUICall(evt))
				{
					flag2 = this.HandleIMGUIEvent(evt.imguiEvent, canAffectFocus);
				}
				result = flag2;
			}
			return result;
		}

		private bool VerifyBounds(EventBase evt)
		{
			return this.IsContainerCapturingTheMouse() || !this.IsLocalEvent(evt) || this.IsEventInsideLocalWindow(evt);
		}

		private bool IsContainerCapturingTheMouse()
		{
			IPanel expr_08 = base.panel;
			IMGUIContainer arg_2A_1;
			if (expr_08 == null)
			{
				arg_2A_1 = null;
			}
			else
			{
				EventDispatcher expr_14 = expr_08.dispatcher;
				arg_2A_1 = ((expr_14 != null) ? expr_14.pointerState.GetCapturingElement(PointerId.mousePointerId) : null);
			}
			return this == arg_2A_1;
		}

		private bool IsLocalEvent(EventBase evt)
		{
			long eventTypeId = evt.eventTypeId;
			return eventTypeId == EventBase<MouseDownEvent>.TypeId() || eventTypeId == EventBase<MouseUpEvent>.TypeId() || eventTypeId == EventBase<MouseMoveEvent>.TypeId() || eventTypeId == EventBase<PointerDownEvent>.TypeId() || eventTypeId == EventBase<PointerUpEvent>.TypeId() || eventTypeId == EventBase<PointerMoveEvent>.TypeId();
		}

		private bool IsEventInsideLocalWindow(EventBase evt)
		{
			Rect currentClipRect = this.GetCurrentClipRect();
			IPointerEvent expr_0E = evt as IPointerEvent;
			string a = (expr_0E != null) ? expr_0E.pointerType : null;
			bool isDirectManipulationDevice = a == PointerType.touch || a == PointerType.pen;
			return GUIUtility.HitTest(currentClipRect, evt.originalMousePosition, isDirectManipulationDevice);
		}

		private bool HandleIMGUIEvent(Event e, bool canAffectFocus)
		{
			return this.HandleIMGUIEvent(e, this.onGUIHandler, canAffectFocus);
		}

		internal bool HandleIMGUIEvent(Event e, Action onGUIHandler, bool canAffectFocus)
		{
			IMGUIContainer.GetCurrentTransformAndClip(this, e, out this.m_CachedTransform, out this.m_CachedClippingRect);
			return this.HandleIMGUIEvent(e, this.m_CachedTransform, this.m_CachedClippingRect, onGUIHandler, canAffectFocus);
		}

		private bool HandleIMGUIEvent(Event e, Matrix4x4 worldTransform, Rect clippingRect, Action onGUIHandler, bool canAffectFocus)
		{
			bool flag = e == null || onGUIHandler == null || base.elementPanel == null || !base.elementPanel.IMGUIEventInterests.WantsEvent(e.rawType);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				EventType rawType = e.rawType;
				bool flag2 = rawType != EventType.Layout;
				if (flag2)
				{
					bool flag3 = this.m_RefreshCachedLayout || base.elementPanel.IMGUIEventInterests.WantsLayoutPass(e.rawType);
					if (flag3)
					{
						e.type = EventType.Layout;
						this.DoOnGUI(e, worldTransform, clippingRect, false, base.layout, onGUIHandler, canAffectFocus);
						this.m_RefreshCachedLayout = false;
						e.type = rawType;
					}
					else
					{
						this.cache.ResetCursor();
					}
				}
				this.DoOnGUI(e, worldTransform, clippingRect, false, base.layout, onGUIHandler, canAffectFocus);
				bool flag4 = this.newKeyboardFocusControlID > 0;
				if (flag4)
				{
					this.newKeyboardFocusControlID = 0;
					Event e2 = new Event
					{
						type = EventType.ExecuteCommand,
						commandName = "NewKeyboardFocus"
					};
					this.HandleIMGUIEvent(e2, true);
				}
				bool flag5 = e.rawType == EventType.Used;
				if (flag5)
				{
					result = true;
				}
				else
				{
					bool flag6 = e.rawType == EventType.MouseUp && this.HasMouseCapture();
					if (flag6)
					{
						GUIUtility.hotControl = 0;
					}
					bool flag7 = base.elementPanel == null;
					if (flag7)
					{
						GUIUtility.ExitGUI();
					}
					result = false;
				}
			}
			return result;
		}

		protected override void ExecuteDefaultAction(EventBase evt)
		{
			bool flag = evt == null;
			if (!flag)
			{
				bool flag2 = evt.eventTypeId == EventBase<BlurEvent>.TypeId();
				if (flag2)
				{
					this.lostFocus = true;
					base.IncrementVersion(VersionChangeType.Repaint);
				}
				else
				{
					bool flag3 = evt.eventTypeId == EventBase<FocusEvent>.TypeId();
					if (flag3)
					{
						FocusEvent focusEvent = evt as FocusEvent;
						this.receivedFocus = true;
						this.focusChangeDirection = focusEvent.direction;
						this.m_IsFocusDelegated = focusEvent.IsFocusDelegated;
					}
					else
					{
						bool flag4 = evt.eventTypeId == EventBase<DetachFromPanelEvent>.TypeId();
						if (flag4)
						{
							bool flag5 = base.elementPanel != null;
							if (flag5)
							{
								BaseVisualElementPanel expr_9F = base.elementPanel;
								int iMGUIContainersCount = expr_9F.IMGUIContainersCount;
								expr_9F.IMGUIContainersCount = iMGUIContainersCount - 1;
							}
						}
						else
						{
							bool flag6 = evt.eventTypeId == EventBase<AttachToPanelEvent>.TypeId();
							if (flag6)
							{
								bool flag7 = base.elementPanel != null;
								if (flag7)
								{
									BaseVisualElementPanel expr_DF = base.elementPanel;
									int iMGUIContainersCount = expr_DF.IMGUIContainersCount;
									expr_DF.IMGUIContainersCount = iMGUIContainersCount + 1;
								}
							}
						}
					}
				}
			}
		}

		protected internal override Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
		{
			float num = float.NaN;
			float num2 = float.NaN;
			bool flag = widthMode != VisualElement.MeasureMode.Exactly || heightMode != VisualElement.MeasureMode.Exactly;
			if (flag)
			{
				Event evt = new Event
				{
					type = EventType.Layout
				};
				Rect layout = base.layout;
				if (widthMode == VisualElement.MeasureMode.Exactly)
				{
					layout.width = desiredWidth;
				}
				if (heightMode == VisualElement.MeasureMode.Exactly)
				{
					layout.height = desiredHeight;
				}
				this.DoOnGUI(evt, this.m_CachedTransform, this.m_CachedClippingRect, true, layout, this.onGUIHandler, true);
				num = this.layoutMeasuredWidth;
				num2 = this.layoutMeasuredHeight;
			}
			if (widthMode != VisualElement.MeasureMode.Exactly)
			{
				if (widthMode == VisualElement.MeasureMode.AtMost)
				{
					num = Mathf.Min(num, desiredWidth);
				}
			}
			else
			{
				num = desiredWidth;
			}
			if (heightMode != VisualElement.MeasureMode.Exactly)
			{
				if (heightMode == VisualElement.MeasureMode.AtMost)
				{
					num2 = Mathf.Min(num2, desiredHeight);
				}
			}
			else
			{
				num2 = desiredHeight;
			}
			return new Vector2(num, num2);
		}

		private Rect GetCurrentClipRect()
		{
			Rect result = this.lastWorldClip;
			bool flag = result.width == 0f || result.height == 0f;
			if (flag)
			{
				result = base.worldBound;
			}
			return result;
		}

		private static void GetCurrentTransformAndClip(IMGUIContainer container, Event evt, out Matrix4x4 transform, out Rect clipRect)
		{
			clipRect = container.GetCurrentClipRect();
			transform = container.worldTransform;
			bool flag = evt.rawType == EventType.Repaint && container.elementPanel != null;
			if (flag)
			{
				transform = container.elementPanel.repaintData.currentOffset * container.worldTransform;
			}
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposeManaged)
		{
			if (disposeManaged)
			{
				ObjectGUIState expr_0D = this.m_ObjectGUIState;
				if (expr_0D != null)
				{
					expr_0D.Dispose();
				}
			}
		}
	}
}
