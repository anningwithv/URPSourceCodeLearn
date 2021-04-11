using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace Unity.Profiling.LowLevel.Unsafe
{
	[IsReadOnly, UsedByNativeCode]
	[StructLayout(LayoutKind.Explicit, Size = 24)]
	public struct ProfilerRecorderDescription
	{
		[FieldOffset(0)]
		private readonly ProfilerCategory category;

		[FieldOffset(2)]
		private readonly MarkerFlags flags;

		[FieldOffset(4)]
		private readonly ProfilerMarkerDataType dataType;

		[FieldOffset(5)]
		private readonly ProfilerMarkerDataUnit unitType;

		[FieldOffset(8)]
		private readonly int reserved0;

		[FieldOffset(12)]
		private readonly int nameUtf8Len;

		[FieldOffset(16)]
		private unsafe readonly byte* nameUtf8;

		public ProfilerCategory Category
		{
			get
			{
				return this.category;
			}
		}

		public MarkerFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		public ProfilerMarkerDataType DataType
		{
			get
			{
				return this.dataType;
			}
		}

		public ProfilerMarkerDataUnit UnitType
		{
			get
			{
				return this.unitType;
			}
		}

		public int NameUtf8Len
		{
			get
			{
				return this.nameUtf8Len;
			}
		}

		public unsafe byte* NameUtf8
		{
			get
			{
				return this.nameUtf8;
			}
		}

		public string Name
		{
			get
			{
				return ProfilerUnsafeUtility.Utf8ToString(this.nameUtf8, this.nameUtf8Len);
			}
		}
	}
}
