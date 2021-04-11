using System;
using System.Collections.Generic;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	internal class VisualTreeViewDataUpdater : BaseVisualTreeUpdater
	{
		private HashSet<VisualElement> m_UpdateList = new HashSet<VisualElement>();

		private HashSet<VisualElement> m_ParentList = new HashSet<VisualElement>();

		private const int kMaxValidatePersistentDataCount = 5;

		private uint m_Version = 0u;

		private uint m_LastVersion = 0u;

		private static readonly string s_Description = "Update ViewData";

		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(VisualTreeViewDataUpdater.s_Description);

		public override ProfilerMarker profilerMarker
		{
			get
			{
				return VisualTreeViewDataUpdater.s_ProfilerMarker;
			}
		}

		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			bool flag = (versionChangeType & VersionChangeType.ViewData) != VersionChangeType.ViewData;
			if (!flag)
			{
				this.m_Version += 1u;
				this.m_UpdateList.Add(ve);
				this.PropagateToParents(ve);
			}
		}

		public override void Update()
		{
			bool flag = this.m_Version == this.m_LastVersion;
			if (!flag)
			{
				int num = 0;
				while (this.m_LastVersion != this.m_Version)
				{
					this.m_LastVersion = this.m_Version;
					this.ValidateViewDataOnSubTree(base.visualTree, true);
					num++;
					bool flag2 = num > 5;
					if (flag2)
					{
						string arg_58_0 = "UIElements: Too many children recursively added that rely on persistent view data: ";
						VisualElement expr_4C = base.visualTree;
						Debug.LogError(arg_58_0 + ((expr_4C != null) ? expr_4C.ToString() : null));
						break;
					}
				}
				this.m_UpdateList.Clear();
				this.m_ParentList.Clear();
			}
		}

		private void ValidateViewDataOnSubTree(VisualElement ve, bool enablePersistence)
		{
			enablePersistence = ve.IsViewDataPersitenceSupportedOnChildren(enablePersistence);
			bool flag = this.m_UpdateList.Contains(ve);
			if (flag)
			{
				this.m_UpdateList.Remove(ve);
				ve.OnViewDataReady(enablePersistence);
			}
			bool flag2 = this.m_ParentList.Contains(ve);
			if (flag2)
			{
				this.m_ParentList.Remove(ve);
				int childCount = ve.hierarchy.childCount;
				for (int i = 0; i < childCount; i++)
				{
					this.ValidateViewDataOnSubTree(ve.hierarchy[i], enablePersistence);
				}
			}
		}

		private void PropagateToParents(VisualElement ve)
		{
			for (VisualElement parent = ve.hierarchy.parent; parent != null; parent = parent.hierarchy.parent)
			{
				bool flag = !this.m_ParentList.Add(parent);
				if (flag)
				{
					break;
				}
			}
		}
	}
}
