using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngineInternal.Input
{
	[NativeHeader("Modules/Input/Private/InputModuleBindings.h"), NativeHeader("Modules/Input/Private/InputInternal.h")]
	internal class NativeInputSystem
	{
		public static NativeUpdateCallback onUpdate;

		public static Action<NativeInputUpdateType> onBeforeUpdate;

		public static Func<NativeInputUpdateType, bool> onShouldRunUpdate;

		private static Action<int, string> s_OnDeviceDiscoveredCallback;

		public static Action<int, string> onDeviceDiscovered
		{
			get
			{
				return NativeInputSystem.s_OnDeviceDiscoveredCallback;
			}
			set
			{
				NativeInputSystem.s_OnDeviceDiscoveredCallback = value;
				NativeInputSystem.hasDeviceDiscoveredCallback = (NativeInputSystem.s_OnDeviceDiscoveredCallback != null);
			}
		}

		internal static extern bool hasDeviceDiscoveredCallback
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern double currentTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern double currentTimeOffsetToRealtimeSinceStartup
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		static NativeInputSystem()
		{
			NativeInputSystem.hasDeviceDiscoveredCallback = false;
		}

		[RequiredByNativeCode]
		internal static void NotifyBeforeUpdate(NativeInputUpdateType updateType)
		{
			Action<NativeInputUpdateType> action = NativeInputSystem.onBeforeUpdate;
			bool flag = action != null;
			if (flag)
			{
				action(updateType);
			}
		}

		[RequiredByNativeCode]
		internal unsafe static void NotifyUpdate(NativeInputUpdateType updateType, IntPtr eventBuffer)
		{
			NativeUpdateCallback nativeUpdateCallback = NativeInputSystem.onUpdate;
			NativeInputEventBuffer* ptr = (NativeInputEventBuffer*)eventBuffer.ToPointer();
			bool flag = nativeUpdateCallback == null;
			if (flag)
			{
				ptr->eventCount = 0;
				ptr->sizeInBytes = 0;
			}
			else
			{
				nativeUpdateCallback(updateType, ptr);
			}
		}

		[RequiredByNativeCode]
		internal static void NotifyDeviceDiscovered(int deviceId, string deviceDescriptor)
		{
			Action<int, string> action = NativeInputSystem.s_OnDeviceDiscoveredCallback;
			bool flag = action != null;
			if (flag)
			{
				action(deviceId, deviceDescriptor);
			}
		}

		[RequiredByNativeCode]
		internal static void ShouldRunUpdate(NativeInputUpdateType updateType, out bool retval)
		{
			Func<NativeInputUpdateType, bool> func = NativeInputSystem.onShouldRunUpdate;
			retval = (func == null || func(updateType));
		}

		[FreeFunction("AllocateInputDeviceId")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int AllocateDeviceId();

		public static void QueueInputEvent<TInputEvent>(ref TInputEvent inputEvent) where TInputEvent : struct
		{
			NativeInputSystem.QueueInputEvent((IntPtr)UnsafeUtility.AddressOf<TInputEvent>(ref inputEvent));
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void QueueInputEvent(IntPtr inputEvent);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long IOCTL(int deviceId, int code, IntPtr data, int sizeInBytes);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetPollingFrequency(float hertz);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Update(NativeInputUpdateType updateType);

		[Obsolete("This is not needed any longer.")]
		public static void SetUpdateMask(NativeInputUpdateType mask)
		{
		}
	}
}
