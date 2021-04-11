using System;

namespace UnityEngine.UIElements
{
	public interface IEventHandler
	{
		void SendEvent(EventBase e);

		void HandleEvent(EventBase evt);

		bool HasTrickleDownHandlers();

		bool HasBubbleUpHandlers();
	}
}
