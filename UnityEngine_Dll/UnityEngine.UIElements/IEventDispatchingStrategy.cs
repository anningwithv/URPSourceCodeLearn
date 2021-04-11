using System;

namespace UnityEngine.UIElements
{
	internal interface IEventDispatchingStrategy
	{
		bool CanDispatchEvent(EventBase evt);

		void DispatchEvent(EventBase evt, IPanel panel);
	}
}
