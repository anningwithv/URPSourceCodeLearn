using System;

namespace UnityEngine.Bindings
{
	[VisibleToOtherModules]
	internal enum StaticAccessorType
	{
		Dot,
		Arrow,
		DoubleColon,
		ArrowWithDefaultReturnIfNull
	}
}
