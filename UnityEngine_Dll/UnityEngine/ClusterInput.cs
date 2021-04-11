using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeConditional("ENABLE_CLUSTERINPUT"), NativeHeader("Modules/ClusterInput/ClusterInput.h")]
	public class ClusterInput
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetAxis(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetButton(string name);

		[NativeConditional("ENABLE_CLUSTERINPUT", "Vector3f(0.0f, 0.0f, 0.0f)")]
		public static Vector3 GetTrackerPosition(string name)
		{
			Vector3 result;
			ClusterInput.GetTrackerPosition_Injected(name, out result);
			return result;
		}

		[NativeConditional("ENABLE_CLUSTERINPUT", "Quartenion::identity")]
		public static Quaternion GetTrackerRotation(string name)
		{
			Quaternion result;
			ClusterInput.GetTrackerRotation_Injected(name, out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetAxis(string name, float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetButton(string name, bool value);

		public static void SetTrackerPosition(string name, Vector3 value)
		{
			ClusterInput.SetTrackerPosition_Injected(name, ref value);
		}

		public static void SetTrackerRotation(string name, Quaternion value)
		{
			ClusterInput.SetTrackerRotation_Injected(name, ref value);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool AddInput(string name, string deviceName, string serverUrl, int index, ClusterInputType type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool EditInput(string name, string deviceName, string serverUrl, int index, ClusterInputType type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CheckConnectionToServer(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTrackerPosition_Injected(string name, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTrackerRotation_Injected(string name, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTrackerPosition_Injected(string name, ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTrackerRotation_Injected(string name, ref Quaternion value);
	}
}
