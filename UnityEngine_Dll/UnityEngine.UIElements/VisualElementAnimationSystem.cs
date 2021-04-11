using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Profiling;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	internal class VisualElementAnimationSystem : BaseVisualTreeUpdater
	{
		private HashSet<IValueAnimationUpdate> m_Animations = new HashSet<IValueAnimationUpdate>();

		private List<IValueAnimationUpdate> m_IterationList = new List<IValueAnimationUpdate>();

		private bool m_HasNewAnimations = false;

		private bool m_IterationListDirty = false;

		private static readonly string s_Description = "Animation Update";

		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(VisualElementAnimationSystem.s_Description);

		private long lastUpdate;

		public override ProfilerMarker profilerMarker
		{
			get
			{
				return VisualElementAnimationSystem.s_ProfilerMarker;
			}
		}

		private long CurrentTimeMs()
		{
			return Panel.TimeSinceStartupMs();
		}

		public void UnregisterAnimation(IValueAnimationUpdate anim)
		{
			this.m_Animations.Remove(anim);
			this.m_IterationListDirty = true;
		}

		public void UnregisterAnimations(List<IValueAnimationUpdate> anims)
		{
			foreach (IValueAnimationUpdate current in anims)
			{
				this.m_Animations.Remove(current);
			}
			this.m_IterationListDirty = true;
		}

		public void RegisterAnimation(IValueAnimationUpdate anim)
		{
			this.m_Animations.Add(anim);
			this.m_HasNewAnimations = true;
			this.m_IterationListDirty = true;
		}

		public void RegisterAnimations(List<IValueAnimationUpdate> anims)
		{
			foreach (IValueAnimationUpdate current in anims)
			{
				this.m_Animations.Add(current);
			}
			this.m_HasNewAnimations = true;
			this.m_IterationListDirty = true;
		}

		public override void Update()
		{
			long num = Panel.TimeSinceStartupMs();
			bool iterationListDirty = this.m_IterationListDirty;
			if (iterationListDirty)
			{
				this.m_IterationList = this.m_Animations.ToList<IValueAnimationUpdate>();
				this.m_IterationListDirty = false;
			}
			bool flag = this.m_HasNewAnimations || this.lastUpdate != num;
			if (flag)
			{
				foreach (IValueAnimationUpdate current in this.m_IterationList)
				{
					current.Tick(num);
				}
				this.m_HasNewAnimations = false;
				this.lastUpdate = num;
			}
		}

		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
		}
	}
}
