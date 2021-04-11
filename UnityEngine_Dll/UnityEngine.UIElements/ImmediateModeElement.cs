using System;

namespace UnityEngine.UIElements
{
	public abstract class ImmediateModeElement : VisualElement
	{
		private bool m_CullingEnabled = false;

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

		public ImmediateModeElement()
		{
			base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
		}

		private void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			mgc.painter.DrawImmediate(new Action(this.ImmediateRepaint), this.cullingEnabled);
		}

		protected abstract void ImmediateRepaint();
	}
}
