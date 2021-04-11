using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeType("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public enum ComputeBufferMode
	{
		Immutable,
		Dynamic,
		Circular,
		StreamOut,
		SubUpdates
	}
}
