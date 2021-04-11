using System;

namespace UnityEngine.UIElements
{
	public static class INotifyValueChangedExtensions
	{
		public static bool RegisterValueChangedCallback<T>(this INotifyValueChanged<T> control, EventCallback<ChangeEvent<T>> callback)
		{
			CallbackEventHandler callbackEventHandler = control as CallbackEventHandler;
			bool flag = callbackEventHandler != null;
			bool result;
			if (flag)
			{
				callbackEventHandler.RegisterCallback<ChangeEvent<T>>(callback, TrickleDown.NoTrickleDown);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		public static bool UnregisterValueChangedCallback<T>(this INotifyValueChanged<T> control, EventCallback<ChangeEvent<T>> callback)
		{
			CallbackEventHandler callbackEventHandler = control as CallbackEventHandler;
			bool flag = callbackEventHandler != null;
			bool result;
			if (flag)
			{
				callbackEventHandler.UnregisterCallback<ChangeEvent<T>>(callback, TrickleDown.NoTrickleDown);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}
	}
}
