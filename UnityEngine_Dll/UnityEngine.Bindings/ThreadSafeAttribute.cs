using System;

namespace UnityEngine.Bindings
{
	[AttributeUsage(AttributeTargets.Method), VisibleToOtherModules]
	internal class ThreadSafeAttribute : NativeMethodAttribute
	{
		public ThreadSafeAttribute()
		{
			base.IsThreadSafe = true;
		}
	}
}
