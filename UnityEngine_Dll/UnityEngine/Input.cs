using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Input/InputBindings.h")]
	public class Input
	{
		private static LocationService locationServiceInstance;

		private static Compass compassInstance;

		private static Gyroscope s_MainGyro;

		public static extern bool simulateMouseWithTouches
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeThrows]
		public static extern bool anyKey
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeThrows]
		public static extern bool anyKeyDown
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeThrows]
		public static extern string inputString
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeThrows]
		public static Vector3 mousePosition
		{
			get
			{
				Vector3 result;
				Input.get_mousePosition_Injected(out result);
				return result;
			}
		}

		[NativeThrows]
		public static Vector2 mouseScrollDelta
		{
			get
			{
				Vector2 result;
				Input.get_mouseScrollDelta_Injected(out result);
				return result;
			}
		}

		public static extern IMECompositionMode imeCompositionMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern string compositionString
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool imeIsSelected
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static Vector2 compositionCursorPos
		{
			get
			{
				Vector2 result;
				Input.get_compositionCursorPos_Injected(out result);
				return result;
			}
			set
			{
				Input.set_compositionCursorPos_Injected(ref value);
			}
		}

		[Obsolete("eatKeyPressOnTextFieldFocus property is deprecated, and only provided to support legacy behavior.")]
		public static extern bool eatKeyPressOnTextFieldFocus
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool mousePresent
		{
			[FreeFunction("GetMousePresent")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern int touchCount
		{
			[FreeFunction("GetTouchCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool touchPressureSupported
		{
			[FreeFunction("IsTouchPressureSupported")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool stylusTouchSupported
		{
			[FreeFunction("IsStylusTouchSupported")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool touchSupported
		{
			[FreeFunction("IsTouchSupported")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool multiTouchEnabled
		{
			[FreeFunction("IsMultiTouchEnabled")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("SetMultiTouchEnabled")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("isGyroAvailable property is deprecated. Please use SystemInfo.supportsGyroscope instead.")]
		public static extern bool isGyroAvailable
		{
			[FreeFunction("IsGyroAvailable")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern DeviceOrientation deviceOrientation
		{
			[FreeFunction("GetOrientation")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static Vector3 acceleration
		{
			[FreeFunction("GetAcceleration")]
			get
			{
				Vector3 result;
				Input.get_acceleration_Injected(out result);
				return result;
			}
		}

		public static extern bool compensateSensors
		{
			[FreeFunction("IsCompensatingSensors")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("SetCompensatingSensors")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int accelerationEventCount
		{
			[FreeFunction("GetAccelerationCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool backButtonLeavesApp
		{
			[FreeFunction("GetBackButtonLeavesApp")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("SetBackButtonLeavesApp")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static LocationService location
		{
			get
			{
				bool flag = Input.locationServiceInstance == null;
				if (flag)
				{
					Input.locationServiceInstance = new LocationService();
				}
				return Input.locationServiceInstance;
			}
		}

		public static Compass compass
		{
			get
			{
				bool flag = Input.compassInstance == null;
				if (flag)
				{
					Input.compassInstance = new Compass();
				}
				return Input.compassInstance;
			}
		}

		public static Gyroscope gyro
		{
			get
			{
				bool flag = Input.s_MainGyro == null;
				if (flag)
				{
					Input.s_MainGyro = new Gyroscope(Input.GetGyroInternal());
				}
				return Input.s_MainGyro;
			}
		}

		public static Touch[] touches
		{
			get
			{
				int touchCount = Input.touchCount;
				Touch[] array = new Touch[touchCount];
				for (int i = 0; i < touchCount; i++)
				{
					array[i] = Input.GetTouch(i);
				}
				return array;
			}
		}

		public static AccelerationEvent[] accelerationEvents
		{
			get
			{
				int accelerationEventCount = Input.accelerationEventCount;
				AccelerationEvent[] array = new AccelerationEvent[accelerationEventCount];
				for (int i = 0; i < accelerationEventCount; i++)
				{
					array[i] = Input.GetAccelerationEvent(i);
				}
				return array;
			}
		}

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyInt(KeyCode key);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyString(string name);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyUpInt(KeyCode key);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyUpString(string name);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyDownInt(KeyCode key);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyDownString(string name);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetAxis(string axisName);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetAxisRaw(string axisName);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetButton(string buttonName);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetButtonDown(string buttonName);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetButtonUp(string buttonName);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetMouseButton(int button);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetMouseButtonDown(int button);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetMouseButtonUp(int button);

		[FreeFunction("ResetInput")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ResetInputAxes();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsJoystickPreconfigured(string joystickName);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetJoystickNames();

		[NativeThrows]
		public static Touch GetTouch(int index)
		{
			Touch result;
			Input.GetTouch_Injected(index, out result);
			return result;
		}

		[NativeThrows]
		public static AccelerationEvent GetAccelerationEvent(int index)
		{
			AccelerationEvent result;
			Input.GetAccelerationEvent_Injected(index, out result);
			return result;
		}

		public static bool GetKey(KeyCode key)
		{
			return Input.GetKeyInt(key);
		}

		public static bool GetKey(string name)
		{
			return Input.GetKeyString(name);
		}

		public static bool GetKeyUp(KeyCode key)
		{
			return Input.GetKeyUpInt(key);
		}

		public static bool GetKeyUp(string name)
		{
			return Input.GetKeyUpString(name);
		}

		public static bool GetKeyDown(KeyCode key)
		{
			return Input.GetKeyDownInt(key);
		}

		public static bool GetKeyDown(string name)
		{
			return Input.GetKeyDownString(name);
		}

		[Conditional("UNITY_EDITOR")]
		internal static void SimulateTouch(int id, Vector2 position, TouchPhase action)
		{
			Input.SimulateTouchInternal(id, position, action, DateTime.Now.Ticks);
		}

		[Conditional("UNITY_EDITOR"), FreeFunction("SimulateTouch"), NativeConditional("UNITY_EDITOR")]
		private static void SimulateTouchInternal(int id, Vector2 position, TouchPhase action, long timestamp)
		{
			Input.SimulateTouchInternal_Injected(id, ref position, action, timestamp);
		}

		[FreeFunction("GetGyro")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGyroInternal();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTouch_Injected(int index, out Touch ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAccelerationEvent_Injected(int index, out AccelerationEvent ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SimulateTouchInternal_Injected(int id, ref Vector2 position, TouchPhase action, long timestamp);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_mousePosition_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_mouseScrollDelta_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_compositionCursorPos_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_compositionCursorPos_Injected(ref Vector2 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_acceleration_Injected(out Vector3 ret);
	}
}
