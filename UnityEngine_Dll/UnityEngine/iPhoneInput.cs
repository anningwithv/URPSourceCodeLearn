using System;
using System.ComponentModel;

namespace UnityEngine
{
	[EditorBrowsable(EditorBrowsableState.Never), Obsolete("iPhoneInput class is deprecated. Please use Input instead (UnityUpgradable) -> Input", true)]
	public class iPhoneInput
	{
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("orientation property is deprecated. Please use Input.deviceOrientation instead (UnityUpgradable) -> Input.deviceOrientation", true)]
		public static iPhoneOrientation orientation
		{
			get
			{
				return iPhoneOrientation.Unknown;
			}
		}

		public static iPhoneAccelerationEvent[] accelerationEvents
		{
			get
			{
				return null;
			}
		}

		public static iPhoneTouch[] touches
		{
			get
			{
				return null;
			}
		}

		public static int touchCount
		{
			get
			{
				return 0;
			}
		}

		public static bool multiTouchEnabled
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public static int accelerationEventCount
		{
			get
			{
				return 0;
			}
		}

		public static Vector3 acceleration
		{
			get
			{
				return default(Vector3);
			}
		}

		public static iPhoneTouch GetTouch(int index)
		{
			return default(iPhoneTouch);
		}

		public static iPhoneAccelerationEvent GetAccelerationEvent(int index)
		{
			return default(iPhoneAccelerationEvent);
		}
	}
}
