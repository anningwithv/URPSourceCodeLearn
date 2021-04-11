using System;

namespace UnityEngineInternal.Input
{
	[Flags]
	internal enum NativeInputUpdateType
	{
		Dynamic = 1,
		Fixed = 2,
		BeforeRender = 4,
		Editor = 8,
		IgnoreFocus = -2147483648
	}
}
