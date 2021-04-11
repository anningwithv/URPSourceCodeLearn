using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	[EditorBrowsable(EditorBrowsableState.Never), Obsolete("iPhoneAccelerationEvent struct is deprecated. Please use AccelerationEvent instead (UnityUpgradable) -> AccelerationEvent", true)]
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct iPhoneAccelerationEvent
	{
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("timeDelta property is deprecated. Please use AccelerationEvent.deltaTime instead (UnityUpgradable) -> AccelerationEvent.deltaTime", true)]
		public float timeDelta
		{
			get
			{
				return 0f;
			}
		}

		public Vector3 acceleration
		{
			get
			{
				return default(Vector3);
			}
		}

		public float deltaTime
		{
			get
			{
				return -1f;
			}
		}
	}
}
