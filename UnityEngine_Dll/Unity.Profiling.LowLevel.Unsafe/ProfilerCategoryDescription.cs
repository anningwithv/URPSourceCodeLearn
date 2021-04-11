using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Unity.Profiling.LowLevel.Unsafe
{
	[IsReadOnly]
	[StructLayout(LayoutKind.Explicit, Size = 24)]
	public struct ProfilerCategoryDescription
	{
		[FieldOffset(0)]
		public readonly ushort Id;

		[FieldOffset(4)]
		public readonly Color32 Color;

		[FieldOffset(8)]
		private readonly int reserved0;

		[FieldOffset(12)]
		public readonly int NameUtf8Len;

		[FieldOffset(16)]
		public unsafe readonly byte* NameUtf8;

		public string Name
		{
			get
			{
				return ProfilerUnsafeUtility.Utf8ToString(this.NameUtf8, this.NameUtf8Len);
			}
		}
	}
}
