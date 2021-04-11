using System;
using System.Runtime.InteropServices;

namespace UnityEngineInternal.Input
{
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 20)]
	internal struct NativeInputEvent
	{
		public const int structSize = 20;

		[FieldOffset(0)]
		public NativeInputEventType type;

		[FieldOffset(4)]
		public ushort sizeInBytes;

		[FieldOffset(6)]
		public ushort deviceId;

		[FieldOffset(8)]
		public double time;

		[FieldOffset(16)]
		public int eventId;

		public NativeInputEvent(NativeInputEventType type, int sizeInBytes, int deviceId, double time)
		{
			this.type = type;
			this.sizeInBytes = (ushort)sizeInBytes;
			this.deviceId = (ushort)deviceId;
			this.eventId = 0;
			this.time = time;
		}
	}
}
