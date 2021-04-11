using System;

namespace UnityEngine.UIElements
{
	internal interface IEventInterpreter
	{
		bool IsActivationEvent(EventBase evt);

		bool IsCancellationEvent(EventBase evt);

		bool IsNavigationEvent(EventBase evt, out NavigationDirection direction);
	}
}
