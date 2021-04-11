using System;
using System.Collections.Generic;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	internal class VisualTreeBindingsUpdater : BaseVisualTreeHierarchyTrackerUpdater
	{
		private static readonly string s_Description = "Update Bindings";

		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(VisualTreeBindingsUpdater.s_Description);

		private readonly HashSet<VisualElement> m_ElementsWithBindings = new HashSet<VisualElement>();

		private readonly HashSet<VisualElement> m_ElementsToAdd = new HashSet<VisualElement>();

		private readonly HashSet<VisualElement> m_ElementsToRemove = new HashSet<VisualElement>();

		private const int kMinUpdateDelay = 100;

		private long m_LastUpdateTime = 0L;

		private static ProfilerMarker s_MarkerUpdate = new ProfilerMarker("Bindings.Update");

		private static ProfilerMarker s_MarkerPoll = new ProfilerMarker("Bindings.PollElementsWithBindings");

		private List<IBinding> updatedBindings = new List<IBinding>();

		public override ProfilerMarker profilerMarker
		{
			get
			{
				return VisualTreeBindingsUpdater.s_ProfilerMarker;
			}
		}

		private IBinding GetUpdaterFromElement(VisualElement ve)
		{
			IBindable expr_07 = ve as IBindable;
			return (expr_07 != null) ? expr_07.binding : null;
		}

		private void StartTracking(VisualElement ve)
		{
			this.m_ElementsToAdd.Add(ve);
			this.m_ElementsToRemove.Remove(ve);
		}

		private void StopTracking(VisualElement ve)
		{
			this.m_ElementsToRemove.Add(ve);
			this.m_ElementsToAdd.Remove(ve);
		}

		private void StartTrackingRecursive(VisualElement ve)
		{
			IBinding updaterFromElement = this.GetUpdaterFromElement(ve);
			bool flag = updaterFromElement != null;
			if (flag)
			{
				this.StartTracking(ve);
			}
			int childCount = ve.hierarchy.childCount;
			for (int i = 0; i < childCount; i++)
			{
				VisualElement ve2 = ve.hierarchy[i];
				this.StartTrackingRecursive(ve2);
			}
		}

		private void StopTrackingRecursive(VisualElement ve)
		{
			this.StopTracking(ve);
			int childCount = ve.hierarchy.childCount;
			for (int i = 0; i < childCount; i++)
			{
				VisualElement ve2 = ve.hierarchy[i];
				this.StopTrackingRecursive(ve2);
			}
		}

		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			base.OnVersionChanged(ve, versionChangeType);
			bool flag = (versionChangeType & VersionChangeType.Bindings) == VersionChangeType.Bindings;
			if (flag)
			{
				bool flag2 = this.GetUpdaterFromElement(ve) != null;
				if (flag2)
				{
					this.StartTracking(ve);
				}
				else
				{
					this.StopTracking(ve);
				}
			}
		}

		protected override void OnHierarchyChange(VisualElement ve, HierarchyChangeType type)
		{
			if (type != HierarchyChangeType.Add)
			{
				if (type == HierarchyChangeType.Remove)
				{
					this.StopTrackingRecursive(ve);
				}
			}
			else
			{
				this.StartTrackingRecursive(ve);
			}
		}

		private static long CurrentTime()
		{
			return Panel.TimeSinceStartupMs();
		}

		public void PerformTrackingOperations()
		{
			foreach (VisualElement current in this.m_ElementsToAdd)
			{
				IBinding updaterFromElement = this.GetUpdaterFromElement(current);
				bool flag = updaterFromElement != null;
				if (flag)
				{
					this.m_ElementsWithBindings.Add(current);
				}
			}
			this.m_ElementsToAdd.Clear();
			foreach (VisualElement current2 in this.m_ElementsToRemove)
			{
				this.m_ElementsWithBindings.Remove(current2);
			}
			this.m_ElementsToRemove.Clear();
		}

		public override void Update()
		{
			base.Update();
			this.PerformTrackingOperations();
			bool flag = this.m_ElementsWithBindings.Count > 0;
			if (flag)
			{
				long num = VisualTreeBindingsUpdater.CurrentTime();
				bool flag2 = this.m_LastUpdateTime + 100L < num;
				if (flag2)
				{
					this.UpdateBindings();
					this.m_LastUpdateTime = num;
				}
			}
		}

		private void UpdateBindings()
		{
			VisualTreeBindingsUpdater.s_MarkerUpdate.Begin();
			foreach (VisualElement current in this.m_ElementsWithBindings)
			{
				IBinding updaterFromElement = this.GetUpdaterFromElement(current);
				bool flag = updaterFromElement == null || current.elementPanel != base.panel;
				if (flag)
				{
					if (updaterFromElement != null)
					{
						updaterFromElement.Release();
					}
					this.StopTracking(current);
				}
				else
				{
					this.updatedBindings.Add(updaterFromElement);
				}
			}
			foreach (IBinding current2 in this.updatedBindings)
			{
				current2.PreUpdate();
			}
			foreach (IBinding current3 in this.updatedBindings)
			{
				current3.Update();
			}
			this.updatedBindings.Clear();
			VisualTreeBindingsUpdater.s_MarkerUpdate.End();
		}

		internal void PollElementsWithBindings(Action<VisualElement, IBinding> callback)
		{
			VisualTreeBindingsUpdater.s_MarkerPoll.Begin();
			this.PerformTrackingOperations();
			bool flag = this.m_ElementsWithBindings.Count > 0;
			if (flag)
			{
				foreach (VisualElement current in this.m_ElementsWithBindings)
				{
					IBinding updaterFromElement = this.GetUpdaterFromElement(current);
					bool flag2 = updaterFromElement == null || current.elementPanel != base.panel;
					if (flag2)
					{
						if (updaterFromElement != null)
						{
							updaterFromElement.Release();
						}
						this.StopTracking(current);
					}
					else
					{
						callback(current, updaterFromElement);
					}
				}
			}
			VisualTreeBindingsUpdater.s_MarkerPoll.End();
		}
	}
}
