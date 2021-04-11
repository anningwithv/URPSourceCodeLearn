using System;
using System.Collections.Generic;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	internal class VisualTreeStyleUpdater : BaseVisualTreeUpdater
	{
		private HashSet<VisualElement> m_ApplyStyleUpdateList = new HashSet<VisualElement>();

		private bool m_IsApplyingStyles = false;

		private uint m_Version = 0u;

		private uint m_LastVersion = 0u;

		private VisualTreeStyleUpdaterTraversal m_StyleContextHierarchyTraversal = new VisualTreeStyleUpdaterTraversal();

		private static readonly string s_Description = "Update Style";

		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(VisualTreeStyleUpdater.s_Description);

		public override ProfilerMarker profilerMarker
		{
			get
			{
				return VisualTreeStyleUpdater.s_ProfilerMarker;
			}
		}

		public void DirtyStyleSheets()
		{
			StyleCache.ClearStyleCache();
			base.visualTree.IncrementVersion(VersionChangeType.StyleSheet);
		}

		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			bool flag = (versionChangeType & VersionChangeType.StyleSheet) != VersionChangeType.StyleSheet;
			if (!flag)
			{
				this.m_Version += 1u;
				bool isApplyingStyles = this.m_IsApplyingStyles;
				if (isApplyingStyles)
				{
					this.m_ApplyStyleUpdateList.Add(ve);
				}
				else
				{
					this.m_StyleContextHierarchyTraversal.AddChangedElement(ve);
				}
			}
		}

		public override void Update()
		{
			bool flag = this.m_Version == this.m_LastVersion;
			if (!flag)
			{
				this.m_LastVersion = this.m_Version;
				this.ApplyStyles();
				this.m_StyleContextHierarchyTraversal.Clear();
				foreach (VisualElement current in this.m_ApplyStyleUpdateList)
				{
					this.m_StyleContextHierarchyTraversal.AddChangedElement(current);
				}
				this.m_ApplyStyleUpdateList.Clear();
			}
		}

		private void ApplyStyles()
		{
			Debug.Assert(base.visualTree.panel != null);
			this.m_IsApplyingStyles = true;
			this.m_StyleContextHierarchyTraversal.PrepareTraversal(base.panel.scaledPixelsPerPoint);
			this.m_StyleContextHierarchyTraversal.Traverse(base.visualTree);
			this.m_IsApplyingStyles = false;
		}
	}
}
