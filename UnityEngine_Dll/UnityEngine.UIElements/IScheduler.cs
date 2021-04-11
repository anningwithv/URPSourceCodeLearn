using System;

namespace UnityEngine.UIElements
{
	internal interface IScheduler
	{
		ScheduledItem ScheduleOnce(Action<TimerState> timerUpdateEvent, long delayMs);

		ScheduledItem ScheduleUntil(Action<TimerState> timerUpdateEvent, long delayMs, long intervalMs, Func<bool> stopCondition = null);

		ScheduledItem ScheduleForDuration(Action<TimerState> timerUpdateEvent, long delayMs, long intervalMs, long durationMs);

		void Unschedule(ScheduledItem item);

		void Schedule(ScheduledItem item);

		void UpdateScheduledEvents();
	}
}
