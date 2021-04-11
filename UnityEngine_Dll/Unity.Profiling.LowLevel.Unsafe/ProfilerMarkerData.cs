using System;
using System.Runtime.InteropServices;

namespace Unity.Profiling.LowLevel.Unsafe
{
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	public struct ProfilerMarkerData
	{
		[FieldOffset(0)]
		public byte Type;

		[FieldOffset(1)]
		private readonly byte reserved0;

		[FieldOffset(2)]
		private readonly ushort reserved1;

		[FieldOffset(4)]
		public uint Size;

		[FieldOffset(8)]
		public unsafe void* Ptr;
	}
}
