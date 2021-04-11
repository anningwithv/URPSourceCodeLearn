using System;

namespace UnityEngine.UIElements
{
	public interface IVisualElementScheduler
	{
		IVisualElementScheduledItem Execute(Action<TimerState> timerUpdateEvent);

		IVisualElementScheduledItem Execute(Action updateEvent);
	}
}
