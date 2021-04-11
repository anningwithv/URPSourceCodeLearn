using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Camera/Camera.h")]
	internal class CameraRaycastHelper
	{
		[FreeFunction("CameraScripting::RaycastTry")]
		internal static GameObject RaycastTry(Camera cam, Ray ray, float distance, int layerMask)
		{
			return CameraRaycastHelper.RaycastTry_Injected(cam, ref ray, distance, layerMask);
		}

		[FreeFunction("CameraScripting::RaycastTry2D")]
		internal static GameObject RaycastTry2D(Camera cam, Ray ray, float distance, int layerMask)
		{
			return CameraRaycastHelper.RaycastTry2D_Injected(cam, ref ray, distance, layerMask);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GameObject RaycastTry_Injected(Camera cam, ref Ray ray, float distance, int layerMask);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GameObject RaycastTry2D_Injected(Camera cam, ref Ray ray, float distance, int layerMask);
	}
}
