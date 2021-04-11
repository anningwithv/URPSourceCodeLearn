using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	[NativeConditional("ENABLE_VR")]
	public static class XRDevice
	{
		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<string> deviceLoaded;

		[Obsolete("This is obsolete, and should no longer be used. Instead, find the active XRDisplaySubsystem and check that the running property is true (for details, see XRDevice.isPresent documentation).", true)]
		public static bool isPresent
		{
			get
			{
				throw new NotSupportedException("XRDevice is Obsolete. Instead, find the active XRDisplaySubsystem and check to see if it is running.");
			}
		}

		[NativeName("DeviceRefreshRate"), StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern float refreshRate
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern float fovZoomFactor
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("SetProjectionZoomFactor"), StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetNativePtr();

		[Obsolete("This is obsolete, and should no longer be used.  Please use XRInputSubsystem.GetTrackingOriginMode."), StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern TrackingSpaceType GetTrackingSpaceType();

		[Obsolete("This is obsolete, and should no longer be used.  Please use XRInputSubsystem.TrySetTrackingOriginMode."), StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool SetTrackingSpaceType(TrackingSpaceType trackingSpaceType);

		[NativeName("DisableAutoVRCameraTracking"), StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DisableAutoXRCameraTracking([NotNull("ArgumentNullException")] Camera camera, bool disabled);

		[NativeName("UpdateEyeTextureMSAASetting"), StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UpdateEyeTextureMSAASetting();

		[RequiredByNativeCode]
		private static void InvokeDeviceLoaded(string loadedDeviceName)
		{
			bool flag = XRDevice.deviceLoaded != null;
			if (flag)
			{
				XRDevice.deviceLoaded(loadedDeviceName);
			}
		}

		static XRDevice()
		{
			// Note: this type is marked as 'beforefieldinit'.
			XRDevice.deviceLoaded = null;
		}
	}
}
