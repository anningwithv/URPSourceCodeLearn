using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	internal abstract class ScheduledItem
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly ScheduledItem.<>c <>9 = new ScheduledItem.<>c();

			internal bool cctor>b__25_0()
			{
				return true;
			}

			internal bool cctor>b__25_1()
			{
				return false;
			}
		}

		public Func<bool> timerUpdateStopCondition;

		public static readonly Func<bool> OnceCondition = new Func<bool>(ScheduledItem.<>c.<>9.<.cctor>b__25_0);

		public static readonly Func<bool> ForeverCondition = new Func<bool>(ScheduledItem.<>c.<>9.<.cctor>b__25_1);

		public long startMs
		{
			get;
			set;
		}

		public long delayMs
		{
			get;
			set;
		}

		public long intervalMs
		{
			get;
			set;
		}

		public long endTimeMs
		{
			get;
			private set;
		}

		public ScheduledItem()
		{
			this.ResetStartTime();
			this.timerUpdateStopCondition = ScheduledItem.OnceCondition;
		}

		protected void ResetStartTime()
		{
			this.startMs = Panel.TimeSinceStartupMs();
		}

		public void SetDuration(long durationMs)
		{
			this.endTimeMs = this.startMs + durationMs;
		}

		public abstract void PerformTimerUpdate(TimerState state);

		internal virtual void OnItemUnscheduled()
		{
		}

		public virtual bool ShouldUnschedule()
		{
			bool flag = this.timerUpdateStopCondition != null;
			return flag && this.timerUpdateStopCondition();
		}
	}
}
