using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	[NativeHeader("Modules/VFX/Public/VFXManager.h"), StaticAccessor("GetVFXManager()", StaticAccessorType.Dot), RequiredByNativeCode]
	public static class VFXManager
	{
		public static extern float fixedTimeStep
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern float maxDeltaTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal static extern string renderPipeSettingsPath
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal static extern bool renderInSceneView
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal static bool activateVFX
		{
			get;
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern VisualEffect[] GetComponents();

		public static void ProcessCamera(Camera cam)
		{
			VFXManager.PrepareCamera(cam);
			VFXManager.ProcessCameraCommand(cam, null);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PrepareCamera([NotNull("NullExceptionObject")] Camera cam);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ProcessCameraCommand([NotNull("NullExceptionObject")] Camera cam, CommandBuffer cmd);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern VFXCameraBufferTypes IsCameraBufferNeeded([NotNull("NullExceptionObject")] Camera cam);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetCameraBuffer([NotNull("NullExceptionObject")] Camera cam, VFXCameraBufferTypes type, Texture buffer, int x, int y, int width, int height);
	}
}
