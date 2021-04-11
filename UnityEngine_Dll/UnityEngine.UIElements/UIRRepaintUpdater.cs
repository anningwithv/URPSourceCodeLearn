using System;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	internal class UIRRepaintUpdater : BaseVisualTreeUpdater
	{
		internal RenderChain renderChain;

		private static ProfilerMarker s_MarkerDrawChain;

		private static readonly string s_Description;

		private static readonly ProfilerMarker s_ProfilerMarker;

		public override ProfilerMarker profilerMarker
		{
			get
			{
				return UIRRepaintUpdater.s_ProfilerMarker;
			}
		}

		protected bool disposed
		{
			get;
			private set;
		}

		public UIRRepaintUpdater()
		{
			base.panelChanged += new Action<BaseVisualElementPanel>(this.OnPanelChanged);
		}

		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			bool flag = this.renderChain == null;
			if (!flag)
			{
				bool flag2 = (versionChangeType & VersionChangeType.Transform) > (VersionChangeType)0;
				bool flag3 = (versionChangeType & VersionChangeType.Size) > (VersionChangeType)0;
				bool flag4 = (versionChangeType & VersionChangeType.Overflow) > (VersionChangeType)0;
				bool flag5 = (versionChangeType & VersionChangeType.BorderRadius) > (VersionChangeType)0;
				bool flag6 = (versionChangeType & VersionChangeType.BorderWidth) > (VersionChangeType)0;
				bool flag7 = flag2 | flag3 | flag6;
				if (flag7)
				{
					this.renderChain.UIEOnTransformOrSizeChanged(ve, flag2, flag3 | flag6);
				}
				bool flag8 = flag4 | flag5;
				if (flag8)
				{
					this.renderChain.UIEOnClippingChanged(ve, false);
				}
				bool flag9 = (versionChangeType & VersionChangeType.Opacity) > (VersionChangeType)0;
				if (flag9)
				{
					this.renderChain.UIEOnOpacityChanged(ve);
				}
				bool flag10 = (versionChangeType & VersionChangeType.Repaint) > (VersionChangeType)0;
				if (flag10)
				{
					this.renderChain.UIEOnVisualsChanged(ve, false);
				}
			}
		}

		public override void Update()
		{
			RenderChain expr_07 = this.renderChain;
			bool flag = ((expr_07 != null) ? expr_07.device : null) == null;
			if (!flag)
			{
				using (UIRRepaintUpdater.s_MarkerDrawChain.Auto())
				{
					this.renderChain.ProcessChanges();
					PanelClearFlags clearFlags = base.panel.clearFlags;
					bool flag2 = clearFlags > PanelClearFlags.None;
					if (flag2)
					{
						GL.Clear((clearFlags & PanelClearFlags.Depth) > PanelClearFlags.None, (clearFlags & PanelClearFlags.Color) > PanelClearFlags.None, Color.clear, 0.99f);
					}
					this.renderChain.Render();
				}
			}
		}

		internal RenderChain DebugGetRenderChain()
		{
			return this.renderChain;
		}

		protected virtual RenderChain CreateRenderChain()
		{
			return new RenderChain(base.panel);
		}

		static UIRRepaintUpdater()
		{
			UIRRepaintUpdater.s_MarkerDrawChain = new ProfilerMarker("DrawChain");
			UIRRepaintUpdater.s_Description = "UIRepaint";
			UIRRepaintUpdater.s_ProfilerMarker = new ProfilerMarker(UIRRepaintUpdater.s_Description);
			Utility.GraphicsResourcesRecreate += new Action<bool>(UIRRepaintUpdater.OnGraphicsResourcesRecreate);
		}

		private static void OnGraphicsResourcesRecreate(bool recreate)
		{
			bool flag = !recreate;
			if (flag)
			{
				UIRenderDevice.PrepareForGfxDeviceRecreate();
			}
			Dictionary<int, Panel>.Enumerator panelsIterator = UIElementsUtility.GetPanelsIterator();
			while (panelsIterator.MoveNext())
			{
				KeyValuePair<int, Panel> current = panelsIterator.Current;
				UIRRepaintUpdater expr_32 = current.Value.GetUpdater(VisualTreeUpdatePhase.Repaint) as UIRRepaintUpdater;
				RenderChain renderChain = (expr_32 != null) ? expr_32.renderChain : null;
				if (recreate)
				{
					if (renderChain != null)
					{
						renderChain.AfterRenderDeviceRelease();
					}
				}
				else if (renderChain != null)
				{
					renderChain.BeforeRenderDeviceRelease();
				}
			}
			bool flag2 = !recreate;
			if (flag2)
			{
				UIRenderDevice.FlushAllPendingDeviceDisposes();
			}
			else
			{
				UIRenderDevice.WrapUpGfxDeviceRecreate();
			}
		}

		private void OnPanelChanged(BaseVisualElementPanel obj)
		{
			this.DisposeRenderChain();
			bool flag = base.panel != null;
			if (flag)
			{
				this.renderChain = this.CreateRenderChain();
				bool flag2 = base.panel.visualTree != null;
				if (flag2)
				{
					this.renderChain.UIEOnChildAdded(base.panel.visualTree.hierarchy.parent, base.panel.visualTree, (base.panel.visualTree.hierarchy.parent == null) ? 0 : base.panel.visualTree.hierarchy.parent.IndexOf(base.panel.visualTree));
					this.renderChain.UIEOnVisualsChanged(base.panel.visualTree, true);
				}
				base.panel.standardShaderChanged += new Action(this.OnPanelStandardShaderChanged);
				base.panel.standardWorldSpaceShaderChanged += new Action(this.OnPanelStandardWorldSpaceShaderChanged);
				base.panel.hierarchyChanged += new HierarchyEvent(this.OnPanelHierarchyChanged);
				this.OnPanelStandardShaderChanged();
				bool flag3 = base.panel.contextType == ContextType.Player;
				if (flag3)
				{
					this.OnPanelStandardWorldSpaceShaderChanged();
				}
			}
		}

		private void OnPanelHierarchyChanged(VisualElement ve, HierarchyChangeType changeType)
		{
			bool flag = this.renderChain == null || ve.panel == null;
			if (!flag)
			{
				switch (changeType)
				{
				case HierarchyChangeType.Add:
					this.renderChain.UIEOnChildAdded(ve.hierarchy.parent, ve, (ve.hierarchy.parent != null) ? ve.hierarchy.parent.IndexOf(ve) : 0);
					break;
				case HierarchyChangeType.Remove:
					this.renderChain.UIEOnChildRemoving(ve);
					break;
				case HierarchyChangeType.Move:
					this.renderChain.UIEOnChildrenReordered(ve);
					break;
				}
			}
		}

		private void OnPanelStandardShaderChanged()
		{
			bool flag = this.renderChain == null;
			if (!flag)
			{
				Shader shader = base.panel.standardShader;
				bool flag2 = shader == null;
				if (flag2)
				{
					shader = Shader.Find(UIRUtility.k_DefaultShaderName);
					Debug.Assert(shader != null, "Failed to load UIElements default shader");
					bool flag3 = shader != null;
					if (flag3)
					{
						shader.hideFlags |= HideFlags.DontSaveInEditor;
					}
				}
				this.renderChain.defaultShader = shader;
			}
		}

		private void OnPanelStandardWorldSpaceShaderChanged()
		{
			bool flag = this.renderChain == null;
			if (!flag)
			{
				Shader shader = base.panel.standardWorldSpaceShader;
				bool flag2 = shader == null;
				if (flag2)
				{
					shader = Shader.Find(UIRUtility.k_DefaultWorldSpaceShaderName);
					Debug.Assert(shader != null, "Failed to load UIElements default world-space shader");
					bool flag3 = shader != null;
					if (flag3)
					{
						shader.hideFlags |= HideFlags.DontSaveInEditor;
					}
				}
				this.renderChain.defaultWorldSpaceShader = shader;
			}
		}

		private void ResetAllElementsDataRecursive(VisualElement ve)
		{
			ve.renderChainData = default(RenderChainVEData);
			int i = ve.hierarchy.childCount - 1;
			while (i >= 0)
			{
				this.ResetAllElementsDataRecursive(ve.hierarchy[i--]);
			}
		}

		private void DisposeRenderChain()
		{
			bool flag = this.renderChain != null;
			if (flag)
			{
				IPanel panel = this.renderChain.panel;
				this.renderChain.Dispose();
				this.renderChain = null;
				bool flag2 = panel != null;
				if (flag2)
				{
					base.panel.hierarchyChanged -= new HierarchyEvent(this.OnPanelHierarchyChanged);
					base.panel.standardShaderChanged -= new Action(this.OnPanelStandardShaderChanged);
					this.ResetAllElementsDataRecursive(panel.visualTree);
				}
			}
		}

		protected override void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.DisposeRenderChain();
				}
				this.disposed = true;
			}
		}
	}
}
