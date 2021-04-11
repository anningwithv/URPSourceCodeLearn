using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	internal abstract class BaseVisualTreeUpdater : IVisualTreeUpdater, IDisposable
	{
		private BaseVisualElementPanel m_Panel;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event Action<BaseVisualElementPanel> panelChanged;

		public BaseVisualElementPanel panel
		{
			get
			{
				return this.m_Panel;
			}
			set
			{
				this.m_Panel = value;
				bool flag = this.panelChanged != null;
				if (flag)
				{
					this.panelChanged(value);
				}
			}
		}

		public VisualElement visualTree
		{
			get
			{
				return this.panel.visualTree;
			}
		}

		public abstract ProfilerMarker profilerMarker
		{
			get;
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
		}

		public abstract void Update();

		public abstract void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType);
	}
}
