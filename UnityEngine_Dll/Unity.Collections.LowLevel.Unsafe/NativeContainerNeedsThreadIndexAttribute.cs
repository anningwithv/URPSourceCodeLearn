using System;
using UnityEngine.Scripting;

namespace Unity.Collections.LowLevel.Unsafe
{
	[AttributeUsage(AttributeTargets.Struct), Obsolete("Use NativeSetThreadIndexAttribute instead"), RequiredByNativeCode]
	public sealed class NativeContainerNeedsThreadIndexAttribute : Attribute
	{
	}
}
