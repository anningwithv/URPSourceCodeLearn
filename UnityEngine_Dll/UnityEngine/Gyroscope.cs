using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Input/GetInput.h")]
	public class Gyroscope
	{
		private int m_GyroIndex;

		public Vector3 rotationRate
		{
			get
			{
				return Gyroscope.rotationRate_Internal(this.m_GyroIndex);
			}
		}

		public Vector3 rotationRateUnbiased
		{
			get
			{
				return Gyroscope.rotationRateUnbiased_Internal(this.m_GyroIndex);
			}
		}

		public Vector3 gravity
		{
			get
			{
				return Gyroscope.gravity_Internal(this.m_GyroIndex);
			}
		}

		public Vector3 userAcceleration
		{
			get
			{
				return Gyroscope.userAcceleration_Internal(this.m_GyroIndex);
			}
		}

		public Quaternion attitude
		{
			get
			{
				return Gyroscope.attitude_Internal(this.m_GyroIndex);
			}
		}

		public bool enabled
		{
			get
			{
				return Gyroscope.getEnabled_Internal(this.m_GyroIndex);
			}
			set
			{
				Gyroscope.setEnabled_Internal(this.m_GyroIndex, value);
			}
		}

		public float updateInterval
		{
			get
			{
				return Gyroscope.getUpdateInterval_Internal(this.m_GyroIndex);
			}
			set
			{
				Gyroscope.setUpdateInterval_Internal(this.m_GyroIndex, value);
			}
		}

		internal Gyroscope(int index)
		{
			this.m_GyroIndex = index;
		}

		[FreeFunction("GetGyroRotationRate")]
		private static Vector3 rotationRate_Internal(int idx)
		{
			Vector3 result;
			Gyroscope.rotationRate_Internal_Injected(idx, out result);
			return result;
		}

		[FreeFunction("GetGyroRotationRateUnbiased")]
		private static Vector3 rotationRateUnbiased_Internal(int idx)
		{
			Vector3 result;
			Gyroscope.rotationRateUnbiased_Internal_Injected(idx, out result);
			return result;
		}

		[FreeFunction("GetGravity")]
		private static Vector3 gravity_Internal(int idx)
		{
			Vector3 result;
			Gyroscope.gravity_Internal_Injected(idx, out result);
			return result;
		}

		[FreeFunction("GetUserAcceleration")]
		private static Vector3 userAcceleration_Internal(int idx)
		{
			Vector3 result;
			Gyroscope.userAcceleration_Internal_Injected(idx, out result);
			return result;
		}

		[FreeFunction("GetAttitude")]
		private static Quaternion attitude_Internal(int idx)
		{
			Quaternion result;
			Gyroscope.attitude_Internal_Injected(idx, out result);
			return result;
		}

		[FreeFunction("IsGyroEnabled")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool getEnabled_Internal(int idx);

		[FreeFunction("SetGyroEnabled")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void setEnabled_Internal(int idx, bool enabled);

		[FreeFunction("GetGyroUpdateInterval")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float getUpdateInterval_Internal(int idx);

		[FreeFunction("SetGyroUpdateInterval")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void setUpdateInterval_Internal(int idx, float interval);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void rotationRate_Internal_Injected(int idx, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void rotationRateUnbiased_Internal_Injected(int idx, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void gravity_Internal_Injected(int idx, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void userAcceleration_Internal_Injected(int idx, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void attitude_Internal_Injected(int idx, out Quaternion ret);
	}
}
