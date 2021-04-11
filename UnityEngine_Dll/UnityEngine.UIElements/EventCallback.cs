using System;

namespace UnityEngine.UIElements
{
	public delegate void EventCallback<in TEventType>(TEventType evt);
	public delegate void EventCallback<in TEventType, in TCallbackArgs>(TEventType evt, TCallbackArgs userArgs);
}
