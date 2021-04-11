using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Yoga;

namespace UnityEngine.UIElements
{
	internal abstract class BaseVisualElementPanel : IPanel, IDisposable
	{
		private float m_Scale = 1f;

		internal YogaConfig yogaConfig;

		private float m_PixelsPerPoint = 1f;

		private float m_SortingPriority = 0f;

		internal ElementUnderPointer m_TopElementUnderPointers;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		internal event Action standardShaderChanged;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		internal event Action standardWorldSpaceShaderChanged;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		internal event Action<Material> updateMaterial;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		internal event HierarchyEvent hierarchyChanged;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		internal event Action<IPanel> beforeUpdate;

		public abstract EventInterests IMGUIEventInterests
		{
			get;
			set;
		}

		public abstract ScriptableObject ownerObject
		{
			get;
			protected set;
		}

		public abstract SavePersistentViewData saveViewData
		{
			get;
			set;
		}

		public abstract GetViewDataDictionary getViewDataDictionary
		{
			get;
			set;
		}

		public abstract int IMGUIContainersCount
		{
			get;
			set;
		}

		public abstract FocusController focusController
		{
			get;
			set;
		}

		public abstract IEventInterpreter eventInterpreter
		{
			get;
			set;
		}

		public abstract IMGUIContainer rootIMGUIContainer
		{
			get;
			set;
		}

		internal float scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				bool flag = !Mathf.Approximately(this.m_Scale, value);
				if (flag)
				{
					this.m_Scale = value;
					this.visualTree.IncrementVersion(VersionChangeType.Layout);
					this.yogaConfig.PointScaleFactor = this.scaledPixelsPerPoint;
					this.visualTree.IncrementVersion(VersionChangeType.StyleSheet);
				}
			}
		}

		internal float pixelsPerPoint
		{
			get
			{
				return this.m_PixelsPerPoint;
			}
			set
			{
				bool flag = !Mathf.Approximately(this.m_PixelsPerPoint, value);
				if (flag)
				{
					this.m_PixelsPerPoint = value;
					this.visualTree.IncrementVersion(VersionChangeType.Layout);
					this.yogaConfig.PointScaleFactor = this.scaledPixelsPerPoint;
					this.visualTree.IncrementVersion(VersionChangeType.StyleSheet);
				}
			}
		}

		public float scaledPixelsPerPoint
		{
			get
			{
				return this.m_PixelsPerPoint * this.m_Scale;
			}
		}

		public float sortingPriority
		{
			get
			{
				return this.m_SortingPriority;
			}
			set
			{
				bool flag = !Mathf.Approximately(this.m_SortingPriority, value);
				if (flag)
				{
					this.m_SortingPriority = value;
					bool flag2 = this.contextType == ContextType.Player;
					if (flag2)
					{
						UIElementsRuntimeUtility.SetPanelOrderingDirty();
					}
				}
			}
		}

		internal PanelClearFlags clearFlags
		{
			get;
			set;
		}

		internal bool duringLayoutPhase
		{
			get;
			set;
		}

		internal bool isDirty
		{
			get
			{
				bool arg_35_0;
				if (this.version == this.repaintVersion)
				{
					IPanelDebug expr_15 = this.panelDebug;
					Panel expr_26 = (Panel)((expr_15 != null) ? expr_15.debuggerOverlayPanel : null);
					arg_35_0 = (expr_26 != null && expr_26.isDirty);
				}
				else
				{
					arg_35_0 = true;
				}
				return arg_35_0;
			}
		}

		internal abstract uint version
		{
			get;
		}

		internal abstract uint repaintVersion
		{
			get;
		}

		internal virtual RepaintData repaintData
		{
			get;
			set;
		}

		internal virtual ICursorManager cursorManager
		{
			get;
			set;
		}

		public ContextualMenuManager contextualMenuManager
		{
			get;
			internal set;
		}

		public abstract VisualElement visualTree
		{
			get;
		}

		public abstract EventDispatcher dispatcher
		{
			get;
			set;
		}

		internal abstract IScheduler scheduler
		{
			get;
		}

		public abstract ContextType contextType
		{
			get;
			protected set;
		}

		internal bool disposed
		{
			get;
			private set;
		}

		internal abstract Shader standardShader
		{
			get;
			set;
		}

		internal virtual Shader standardWorldSpaceShader
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public IPanelDebug panelDebug
		{
			get;
			set;
		}

		protected BaseVisualElementPanel()
		{
			this.<clearFlags>k__BackingField = PanelClearFlags.All;
			this.m_TopElementUnderPointers = new ElementUnderPointer();
			base..ctor();
			this.yogaConfig = new YogaConfig();
			this.yogaConfig.UseWebDefaults = YogaConfig.Default.UseWebDefaults;
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					bool flag = this.panelDebug != null;
					if (flag)
					{
						this.panelDebug.DetachAllDebuggers();
						this.panelDebug = null;
					}
					bool flag2 = this.ownerObject != null;
					if (flag2)
					{
						UIElementsUtility.RemoveCachedPanel(this.ownerObject.GetInstanceID());
					}
				}
				this.yogaConfig = null;
				this.disposed = true;
			}
		}

		public abstract void Repaint(Event e);

		public abstract void ValidateLayout();

		public abstract void UpdateAnimations();

		public abstract void UpdateBindings();

		public abstract void ApplyStyles();

		public abstract void DirtyStyleSheets();

		internal abstract void OnVersionChanged(VisualElement ele, VersionChangeType changeTypeFlag);

		internal abstract void SetUpdater(IVisualTreeUpdater updater, VisualTreeUpdatePhase phase);

		internal void SendEvent(EventBase e, DispatchMode dispatchMode = DispatchMode.Default)
		{
			UnityEngine.Debug.Assert(this.dispatcher != null);
			EventDispatcher expr_16 = this.dispatcher;
			if (expr_16 != null)
			{
				expr_16.Dispatch(e, this, dispatchMode);
			}
		}

		public abstract VisualElement Pick(Vector2 point);

		public abstract VisualElement PickAll(Vector2 point, List<VisualElement> picked);

		internal abstract IVisualTreeUpdater GetUpdater(VisualTreeUpdatePhase phase);

		internal VisualElement GetTopElementUnderPointer(int pointerId)
		{
			return this.m_TopElementUnderPointers.GetTopElementUnderPointer(pointerId);
		}

		internal VisualElement RecomputeTopElementUnderPointer(Vector2 pointerPos, EventBase triggerEvent)
		{
			VisualElement visualElement = this.Pick(pointerPos);
			this.m_TopElementUnderPointers.SetElementUnderPointer(visualElement, triggerEvent);
			return visualElement;
		}

		internal void ClearCachedElementUnderPointer(EventBase triggerEvent)
		{
			this.m_TopElementUnderPointers.SetTemporaryElementUnderPointer(null, triggerEvent);
		}

		private void SetElementUnderPointer(VisualElement newElementUnderPointer, int pointerId, Vector2 pointerPos)
		{
			this.m_TopElementUnderPointers.SetElementUnderPointer(newElementUnderPointer, pointerId, pointerPos);
		}

		internal void CommitElementUnderPointers()
		{
			this.m_TopElementUnderPointers.CommitElementUnderPointers(this.dispatcher);
		}

		protected void InvokeStandardShaderChanged()
		{
			bool flag = this.standardShaderChanged != null;
			if (flag)
			{
				this.standardShaderChanged();
			}
		}

		protected void InvokeStandardWorldSpaceShaderChanged()
		{
			bool flag = this.standardWorldSpaceShaderChanged != null;
			if (flag)
			{
				this.standardWorldSpaceShaderChanged();
			}
		}

		internal void InvokeUpdateMaterial(Material mat)
		{
			Action<Material> expr_07 = this.updateMaterial;
			if (expr_07 != null)
			{
				expr_07(mat);
			}
		}

		internal void InvokeHierarchyChanged(VisualElement ve, HierarchyChangeType changeType)
		{
			bool flag = this.hierarchyChanged != null;
			if (flag)
			{
				this.hierarchyChanged(ve, changeType);
			}
		}

		internal void InvokeBeforeUpdate()
		{
			Action<IPanel> expr_07 = this.beforeUpdate;
			if (expr_07 != null)
			{
				expr_07(this);
			}
		}

		internal void UpdateElementUnderPointers()
		{
			int[] hoveringPointers = PointerId.hoveringPointers;
			for (int i = 0; i < hoveringPointers.Length; i++)
			{
				int pointerId = hoveringPointers[i];
				bool flag = PointerDeviceState.GetPanel(pointerId) != this;
				if (flag)
				{
					this.SetElementUnderPointer(null, pointerId, new Vector2(-3.40282347E+38f, -3.40282347E+38f));
				}
				else
				{
					Vector2 pointerPosition = PointerDeviceState.GetPointerPosition(pointerId);
					VisualElement newElementUnderPointer = this.PickAll(pointerPosition, null);
					this.SetElementUnderPointer(newElementUnderPointer, pointerId, pointerPosition);
				}
			}
			this.CommitElementUnderPointers();
		}

		public virtual void Update()
		{
			this.scheduler.UpdateScheduledEvents();
			this.ValidateLayout();
			this.UpdateAnimations();
			this.UpdateBindings();
		}
	}
}
