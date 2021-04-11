using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	internal class TimerEventScheduler : IScheduler
	{
		private class TimerEventSchedulerItem : ScheduledItem
		{
			private readonly Action<TimerState> m_TimerUpdateEvent;

			public TimerEventSchedulerItem(Action<TimerState> updateEvent)
			{
				this.m_TimerUpdateEvent = updateEvent;
			}

			public override void PerformTimerUpdate(TimerState state)
			{
				Action<TimerState> expr_07 = this.m_TimerUpdateEvent;
				if (expr_07 != null)
				{
					expr_07(state);
				}
			}

			public override string ToString()
			{
				return this.m_TimerUpdateEvent.ToString();
			}
		}

		private readonly List<ScheduledItem> m_ScheduledItems = new List<ScheduledItem>();

		private bool m_TransactionMode;

		private readonly List<ScheduledItem> m_ScheduleTransactions = new List<ScheduledItem>();

		private readonly HashSet<ScheduledItem> m_UnscheduleTransactions = new HashSet<ScheduledItem>();

		internal bool disableThrottling = false;

		private int m_LastUpdatedIndex = -1;

		public void Schedule(ScheduledItem item)
		{
			bool flag = item == null;
			if (!flag)
			{
				bool flag2 = item == null;
				if (flag2)
				{
					throw new NotSupportedException("Scheduled Item type is not supported by this scheduler");
				}
				bool transactionMode = this.m_TransactionMode;
				if (transactionMode)
				{
					bool flag3 = this.m_UnscheduleTransactions.Remove(item);
					if (!flag3)
					{
						bool flag4 = this.m_ScheduledItems.Contains(item) || this.m_ScheduleTransactions.Contains(item);
						if (flag4)
						{
							throw new ArgumentException("Cannot schedule function " + item + " more than once");
						}
						this.m_ScheduleTransactions.Add(item);
					}
				}
				else
				{
					bool flag5 = this.m_ScheduledItems.Contains(item);
					if (flag5)
					{
						throw new ArgumentException("Cannot schedule function " + item + " more than once");
					}
					this.m_ScheduledItems.Add(item);
				}
			}
		}

		public ScheduledItem ScheduleOnce(Action<TimerState> timerUpdateEvent, long delayMs)
		{
			TimerEventScheduler.TimerEventSchedulerItem timerEventSchedulerItem = new TimerEventScheduler.TimerEventSchedulerItem(timerUpdateEvent)
			{
				delayMs = delayMs
			};
			this.Schedule(timerEventSchedulerItem);
			return timerEventSchedulerItem;
		}

		public ScheduledItem ScheduleUntil(Action<TimerState> timerUpdateEvent, long delayMs, long intervalMs, Func<bool> stopCondition)
		{
			TimerEventScheduler.TimerEventSchedulerItem timerEventSchedulerItem = new TimerEventScheduler.TimerEventSchedulerItem(timerUpdateEvent)
			{
				delayMs = delayMs,
				intervalMs = intervalMs,
				timerUpdateStopCondition = stopCondition
			};
			this.Schedule(timerEventSchedulerItem);
			return timerEventSchedulerItem;
		}

		public ScheduledItem ScheduleForDuration(Action<TimerState> timerUpdateEvent, long delayMs, long intervalMs, long durationMs)
		{
			TimerEventScheduler.TimerEventSchedulerItem timerEventSchedulerItem = new TimerEventScheduler.TimerEventSchedulerItem(timerUpdateEvent)
			{
				delayMs = delayMs,
				intervalMs = intervalMs,
				timerUpdateStopCondition = null
			};
			timerEventSchedulerItem.SetDuration(durationMs);
			this.Schedule(timerEventSchedulerItem);
			return timerEventSchedulerItem;
		}

		private bool RemovedScheduledItemAt(int index)
		{
			bool flag = index >= 0;
			bool result;
			if (flag)
			{
				this.m_ScheduledItems.RemoveAt(index);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		public void Unschedule(ScheduledItem item)
		{
			bool flag = item != null;
			if (flag)
			{
				bool transactionMode = this.m_TransactionMode;
				if (transactionMode)
				{
					bool flag2 = this.m_UnscheduleTransactions.Contains(item);
					if (flag2)
					{
						throw new ArgumentException("Cannot unschedule scheduled function twice" + ((item != null) ? item.ToString() : null));
					}
					bool flag3 = this.m_ScheduleTransactions.Remove(item);
					if (!flag3)
					{
						bool flag4 = this.m_ScheduledItems.Contains(item);
						if (!flag4)
						{
							throw new ArgumentException("Cannot unschedule unknown scheduled function " + ((item != null) ? item.ToString() : null));
						}
						this.m_UnscheduleTransactions.Add(item);
					}
				}
				else
				{
					bool flag5 = !this.PrivateUnSchedule(item);
					if (flag5)
					{
						throw new ArgumentException("Cannot unschedule unknown scheduled function " + ((item != null) ? item.ToString() : null));
					}
				}
				item.OnItemUnscheduled();
			}
		}

		private bool PrivateUnSchedule(ScheduledItem sItem)
		{
			return this.m_ScheduleTransactions.Remove(sItem) || this.RemovedScheduledItemAt(this.m_ScheduledItems.IndexOf(sItem));
		}

		public void UpdateScheduledEvents()
		{
			try
			{
				this.m_TransactionMode = true;
				long num = Panel.TimeSinceStartupMs();
				int count = this.m_ScheduledItems.Count;
				long num2 = num + 20L;
				int num3 = this.m_LastUpdatedIndex + 1;
				bool flag = num3 >= count;
				if (flag)
				{
					num3 = 0;
				}
				for (int i = 0; i < count; i++)
				{
					num = Panel.TimeSinceStartupMs();
					bool flag2 = !this.disableThrottling && num >= num2;
					if (flag2)
					{
						break;
					}
					int num4 = num3 + i;
					bool flag3 = num4 >= count;
					if (flag3)
					{
						num4 -= count;
					}
					ScheduledItem scheduledItem = this.m_ScheduledItems[num4];
					bool flag4 = false;
					bool flag5 = num - scheduledItem.delayMs >= scheduledItem.startMs;
					if (flag5)
					{
						TimerState state = new TimerState
						{
							start = scheduledItem.startMs,
							now = num
						};
						bool flag6 = !this.m_UnscheduleTransactions.Contains(scheduledItem);
						if (flag6)
						{
							scheduledItem.PerformTimerUpdate(state);
						}
						scheduledItem.startMs = num;
						scheduledItem.delayMs = scheduledItem.intervalMs;
						bool flag7 = scheduledItem.ShouldUnschedule();
						if (flag7)
						{
							flag4 = true;
						}
					}
					bool flag8 = flag4 || (scheduledItem.endTimeMs > 0L && num > scheduledItem.endTimeMs);
					if (flag8)
					{
						bool flag9 = !this.m_UnscheduleTransactions.Contains(scheduledItem);
						if (flag9)
						{
							this.Unschedule(scheduledItem);
						}
					}
					this.m_LastUpdatedIndex = num4;
				}
			}
			finally
			{
				this.m_TransactionMode = false;
				foreach (ScheduledItem current in this.m_UnscheduleTransactions)
				{
					this.PrivateUnSchedule(current);
				}
				this.m_UnscheduleTransactions.Clear();
				foreach (ScheduledItem current2 in this.m_ScheduleTransactions)
				{
					this.Schedule(current2);
				}
				this.m_ScheduleTransactions.Clear();
			}
		}
	}
}
