using System;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	internal abstract class BaseRuntimePanel : Panel, IRuntimePanel
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly BaseRuntimePanel.<>c <>9 = new BaseRuntimePanel.<>c();

			internal Vector2 cctor>b__17_0(Vector2 p)
			{
				return p;
			}
		}

		private Shader m_StandardWorldSpaceShader;

		internal RenderTexture targetTexture = null;

		internal Matrix4x4 panelToWorld = Matrix4x4.identity;

		internal static readonly Func<Vector2, Vector2> DefaultScreenToPanelSpace = new Func<Vector2, Vector2>(BaseRuntimePanel.<>c.<>9.<.cctor>b__17_0);

		private Func<Vector2, Vector2> m_ScreenToPanelSpace = BaseRuntimePanel.DefaultScreenToPanelSpace;

		internal override Shader standardWorldSpaceShader
		{
			get
			{
				return this.m_StandardWorldSpaceShader;
			}
			set
			{
				bool flag = this.m_StandardWorldSpaceShader != value;
				if (flag)
				{
					this.m_StandardWorldSpaceShader = value;
					base.InvokeStandardWorldSpaceShaderChanged();
				}
			}
		}

		internal bool drawToCameras
		{
			get
			{
				UIRRepaintUpdater expr_0D = this.GetUpdater(VisualTreeUpdatePhase.Repaint) as UIRRepaintUpdater;
				bool arg_47_0;
				if (expr_0D == null)
				{
					arg_47_0 = false;
				}
				else
				{
					RenderChain expr_19 = expr_0D.renderChain;
					bool? flag = (expr_19 != null) ? new bool?(expr_19.drawInCameras) : null;
					bool flag2 = true;
					arg_47_0 = (flag.GetValueOrDefault() == flag2 & flag.HasValue);
				}
				return arg_47_0;
			}
			set
			{
				UIRRepaintUpdater expr_0D = this.GetUpdater(VisualTreeUpdatePhase.Repaint) as UIRRepaintUpdater;
				RenderChain renderChain = (expr_0D != null) ? expr_0D.renderChain : null;
				bool flag = renderChain != null;
				if (flag)
				{
					renderChain.drawInCameras = value;
				}
			}
		}

		public Func<Vector2, Vector2> screenToPanelSpace
		{
			get
			{
				return this.m_ScreenToPanelSpace;
			}
			set
			{
				this.m_ScreenToPanelSpace = (value ?? BaseRuntimePanel.DefaultScreenToPanelSpace);
			}
		}

		protected BaseRuntimePanel(ScriptableObject ownerObject, EventDispatcher dispatcher = null) : base(ownerObject, ContextType.Player, dispatcher)
		{
		}

		public override void Repaint(Event e)
		{
			bool flag = this.targetTexture == null;
			if (flag)
			{
				RenderTexture active = RenderTexture.active;
				int num = (active != null) ? active.width : Screen.width;
				int num2 = (active != null) ? active.height : Screen.height;
				GL.Viewport(new Rect(0f, 0f, (float)num, (float)num2));
				base.clearFlags = PanelClearFlags.Depth;
				base.Repaint(e);
			}
			else
			{
				RenderTexture active2 = RenderTexture.active;
				RenderTexture.active = this.targetTexture;
				base.clearFlags = PanelClearFlags.All;
				base.Repaint(e);
				RenderTexture.active = active2;
			}
		}

		internal Vector2 ScreenToPanel(Vector2 screen)
		{
			return this.screenToPanelSpace(screen) / base.scale;
		}
	}
}
