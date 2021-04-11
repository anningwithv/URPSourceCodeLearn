using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	[NativeContainer]
	internal struct NativeArrayDispose
	{
		[NativeDisableUnsafePtrRestriction]
		internal unsafe void* m_Buffer;

		internal Allocator m_AllocatorLabel;

		internal AtomicSafetyHandle m_Safety;

		public void Dispose()
		{
			UnsafeUtility.Free(this.m_Buffer, this.m_AllocatorLabel);
		}
	}
}
