using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine.XR
{
	[NativeConditional("ENABLE_VR"), NativeHeader("Modules/VR/ScriptBindings/XR.bindings.h"), NativeHeader("Modules/VR/VRModule.h"), NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h"), NativeHeader("Runtime/Interfaces/IVRDevice.h")]
	public static class XRSettings
	{
		public enum StereoRenderingMode
		{
			MultiPass,
			SinglePass,
			SinglePassInstanced,
			SinglePassMultiview
		}

		public static extern bool enabled
		{
			[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern GameViewRenderMode gameViewRenderMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("Active"), StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern bool isDeviceActive
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern bool showDeviceView
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("RenderScale"), StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern float eyeTextureResolutionScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern int eyeTextureWidth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern int eyeTextureHeight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeConditional("ENABLE_VR", "RenderTextureDesc()"), NativeName("IntermediateEyeTextureDesc"), StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static RenderTextureDescriptor eyeTextureDesc
		{
			get
			{
				RenderTextureDescriptor result;
				XRSettings.get_eyeTextureDesc_Injected(out result);
				return result;
			}
		}

		[NativeName("DeviceEyeTextureDimension"), StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern TextureDimension deviceEyeTextureDimension
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static float renderViewportScale
		{
			get
			{
				return XRSettings.renderViewportScaleInternal;
			}
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("value", "Render viewport scale should be between 0 and 1.");
				}
				XRSettings.renderViewportScaleInternal = value;
			}
		}

		[NativeName("RenderViewportScale"), StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		internal static extern float renderViewportScaleInternal
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern float occlusionMaskScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern bool useOcclusionMesh
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("DeviceName"), StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern string loadedDeviceName
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string[] supportedDevices
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern XRSettings.StereoRenderingMode stereoRenderingMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static void LoadDeviceByName(string deviceName)
		{
			XRSettings.LoadDeviceByName(new string[]
			{
				deviceName
			});
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void LoadDeviceByName(string[] prioritizedDeviceNameList);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_eyeTextureDesc_Injected(out RenderTextureDescriptor ret);
	}
}
