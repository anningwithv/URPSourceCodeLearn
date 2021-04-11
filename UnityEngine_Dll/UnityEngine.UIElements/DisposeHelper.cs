using System;
using System.Diagnostics;

namespace UnityEngine.UIElements
{
	internal class DisposeHelper
	{
		[Conditional("UNITY_UIELEMENTS_DEBUG_DISPOSE")]
		public static void NotifyMissingDispose(IDisposable disposable)
		{
			bool flag = disposable == null;
			if (!flag)
			{
				UnityEngine.Debug.LogError("An IDisposable instance of type '" + disposable.GetType().FullName + "' has not been disposed.");
			}
		}

		public static void NotifyDisposedUsed(IDisposable disposable)
		{
			UnityEngine.Debug.LogError("An instance of type '" + disposable.GetType().FullName + "' is being used although it has been disposed.");
		}
	}
}
