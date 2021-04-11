using System;

namespace UnityEngine.UIElements
{
	internal abstract class BaseVisualTreeHierarchyTrackerUpdater : BaseVisualTreeUpdater
	{
		private enum State
		{
			Waiting,
			TrackingAddOrMove,
			TrackingRemove
		}

		private BaseVisualTreeHierarchyTrackerUpdater.State m_State = BaseVisualTreeHierarchyTrackerUpdater.State.Waiting;

		private VisualElement m_CurrentChangeElement;

		private VisualElement m_CurrentChangeParent;

		protected abstract void OnHierarchyChange(VisualElement ve, HierarchyChangeType type);

		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			bool flag = (versionChangeType & VersionChangeType.Hierarchy) == VersionChangeType.Hierarchy;
			if (flag)
			{
				switch (this.m_State)
				{
				case BaseVisualTreeHierarchyTrackerUpdater.State.Waiting:
					this.ProcessNewChange(ve);
					break;
				case BaseVisualTreeHierarchyTrackerUpdater.State.TrackingAddOrMove:
					this.ProcessAddOrMove(ve);
					break;
				case BaseVisualTreeHierarchyTrackerUpdater.State.TrackingRemove:
					this.ProcessRemove(ve);
					break;
				}
			}
		}

		public override void Update()
		{
			Debug.Assert(this.m_State == BaseVisualTreeHierarchyTrackerUpdater.State.TrackingAddOrMove || this.m_State == BaseVisualTreeHierarchyTrackerUpdater.State.Waiting);
			bool flag = this.m_State == BaseVisualTreeHierarchyTrackerUpdater.State.TrackingAddOrMove;
			if (flag)
			{
				this.OnHierarchyChange(this.m_CurrentChangeElement, HierarchyChangeType.Move);
				this.m_State = BaseVisualTreeHierarchyTrackerUpdater.State.Waiting;
			}
			this.m_CurrentChangeElement = null;
			this.m_CurrentChangeParent = null;
		}

		private void ProcessNewChange(VisualElement ve)
		{
			this.m_CurrentChangeElement = ve;
			this.m_CurrentChangeParent = ve.parent;
			bool flag = this.m_CurrentChangeParent == null && ve.panel != null;
			if (flag)
			{
				this.OnHierarchyChange(this.m_CurrentChangeElement, HierarchyChangeType.Move);
				this.m_State = BaseVisualTreeHierarchyTrackerUpdater.State.Waiting;
			}
			else
			{
				this.m_State = ((this.m_CurrentChangeParent == null) ? BaseVisualTreeHierarchyTrackerUpdater.State.TrackingRemove : BaseVisualTreeHierarchyTrackerUpdater.State.TrackingAddOrMove);
			}
		}

		private void ProcessAddOrMove(VisualElement ve)
		{
			Debug.Assert(this.m_CurrentChangeParent != null);
			bool flag = this.m_CurrentChangeParent == ve;
			if (flag)
			{
				this.OnHierarchyChange(this.m_CurrentChangeElement, HierarchyChangeType.Add);
				this.m_State = BaseVisualTreeHierarchyTrackerUpdater.State.Waiting;
			}
			else
			{
				this.OnHierarchyChange(this.m_CurrentChangeElement, HierarchyChangeType.Move);
				this.ProcessNewChange(ve);
			}
		}

		private void ProcessRemove(VisualElement ve)
		{
			this.OnHierarchyChange(this.m_CurrentChangeElement, HierarchyChangeType.Remove);
			bool flag = ve.panel != null;
			if (flag)
			{
				this.m_CurrentChangeParent = null;
				this.m_CurrentChangeElement = null;
				this.m_State = BaseVisualTreeHierarchyTrackerUpdater.State.Waiting;
			}
			else
			{
				this.m_CurrentChangeElement = ve;
			}
		}
	}
}
