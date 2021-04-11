using System;
using UnityEngine.Bindings;

namespace UnityEngine.IO
{
	[NativeHeader("Runtime/VirtualFileSystem/VirtualFileSystem.h")]
	internal enum ThreadIORestrictionMode
	{
		Allowed,
		TreatAsError
	}
}
