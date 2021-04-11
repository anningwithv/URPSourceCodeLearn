using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	[NativeConditional("ENABLE_VR"), NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputDevices.h"), StaticAccessor("XRInputDevices::Get()", StaticAccessorType.Dot), UsedByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class InputDevices
	{
		private static List<InputDevice> s_InputDeviceList;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<InputDevice> deviceConnected;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<InputDevice> deviceDisconnected;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<InputDevice> deviceConfigChanged;

		public static InputDevice GetDeviceAtXRNode(XRNode node)
		{
			ulong deviceIdAtXRNode = InputTracking.GetDeviceIdAtXRNode(node);
			return new InputDevice(deviceIdAtXRNode);
		}

		public static void GetDevicesAtXRNode(XRNode node, List<InputDevice> inputDevices)
		{
			bool flag = inputDevices == null;
			if (flag)
			{
				throw new ArgumentNullException("inputDevices");
			}
			List<ulong> list = new List<ulong>();
			InputTracking.GetDeviceIdsAtXRNode_Internal(node, list);
			inputDevices.Clear();
			foreach (ulong current in list)
			{
				InputDevice item = new InputDevice(current);
				bool isValid = item.isValid;
				if (isValid)
				{
					inputDevices.Add(item);
				}
			}
		}

		public static void GetDevices(List<InputDevice> inputDevices)
		{
			bool flag = inputDevices == null;
			if (flag)
			{
				throw new ArgumentNullException("inputDevices");
			}
			inputDevices.Clear();
			InputDevices.GetDevices_Internal(inputDevices);
		}

		[Obsolete("This API has been marked as deprecated and will be removed in future versions. Please use InputDevices.GetDevicesWithCharacteristics instead.")]
		public static void GetDevicesWithRole(InputDeviceRole role, List<InputDevice> inputDevices)
		{
			bool flag = inputDevices == null;
			if (flag)
			{
				throw new ArgumentNullException("inputDevices");
			}
			bool flag2 = InputDevices.s_InputDeviceList == null;
			if (flag2)
			{
				InputDevices.s_InputDeviceList = new List<InputDevice>();
			}
			InputDevices.GetDevices_Internal(InputDevices.s_InputDeviceList);
			inputDevices.Clear();
			foreach (InputDevice current in InputDevices.s_InputDeviceList)
			{
				bool flag3 = current.role == role;
				if (flag3)
				{
					inputDevices.Add(current);
				}
			}
		}

		public static void GetDevicesWithCharacteristics(InputDeviceCharacteristics desiredCharacteristics, List<InputDevice> inputDevices)
		{
			bool flag = inputDevices == null;
			if (flag)
			{
				throw new ArgumentNullException("inputDevices");
			}
			bool flag2 = InputDevices.s_InputDeviceList == null;
			if (flag2)
			{
				InputDevices.s_InputDeviceList = new List<InputDevice>();
			}
			InputDevices.GetDevices_Internal(InputDevices.s_InputDeviceList);
			inputDevices.Clear();
			foreach (InputDevice current in InputDevices.s_InputDeviceList)
			{
				bool flag3 = (current.characteristics & desiredCharacteristics) == desiredCharacteristics;
				if (flag3)
				{
					inputDevices.Add(current);
				}
			}
		}

		[RequiredByNativeCode]
		private static void InvokeConnectionEvent(ulong deviceId, ConnectionChangeType change)
		{
			switch (change)
			{
			case ConnectionChangeType.Connected:
			{
				bool flag = InputDevices.deviceConnected != null;
				if (flag)
				{
					InputDevices.deviceConnected(new InputDevice(deviceId));
				}
				break;
			}
			case ConnectionChangeType.Disconnected:
			{
				bool flag2 = InputDevices.deviceDisconnected != null;
				if (flag2)
				{
					InputDevices.deviceDisconnected(new InputDevice(deviceId));
				}
				break;
			}
			case ConnectionChangeType.ConfigChange:
			{
				bool flag3 = InputDevices.deviceConfigChanged != null;
				if (flag3)
				{
					InputDevices.deviceConfigChanged(new InputDevice(deviceId));
				}
				break;
			}
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDevices_Internal([NotNull("ArgumentNullException")] List<InputDevice> inputDevices);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool SendHapticImpulse(ulong deviceId, uint channel, float amplitude, float duration);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool SendHapticBuffer(ulong deviceId, uint channel, [NotNull("ArgumentNullException")] byte[] buffer);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetHapticCapabilities(ulong deviceId, out HapticCapabilities capabilities);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void StopHaptics(ulong deviceId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureUsages(ulong deviceId, [NotNull("ArgumentNullException")] List<InputFeatureUsage> featureUsages);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValue_bool(ulong deviceId, string usage, out bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValue_UInt32(ulong deviceId, string usage, out uint value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValue_float(ulong deviceId, string usage, out float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValue_Vector2f(ulong deviceId, string usage, out Vector2 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValue_Vector3f(ulong deviceId, string usage, out Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValue_Quaternionf(ulong deviceId, string usage, out Quaternion value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValue_Custom(ulong deviceId, string usage, [Out] byte[] value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValueAtTime_bool(ulong deviceId, string usage, long time, out bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValueAtTime_UInt32(ulong deviceId, string usage, long time, out uint value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValueAtTime_float(ulong deviceId, string usage, long time, out float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValueAtTime_Vector2f(ulong deviceId, string usage, long time, out Vector2 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValueAtTime_Vector3f(ulong deviceId, string usage, long time, out Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValueAtTime_Quaternionf(ulong deviceId, string usage, long time, out Quaternion value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValue_XRHand(ulong deviceId, string usage, out Hand value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValue_XRBone(ulong deviceId, string usage, out Bone value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValue_XREyes(ulong deviceId, string usage, out Eyes value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsDeviceValid(ulong deviceId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetDeviceName(ulong deviceId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetDeviceManufacturer(ulong deviceId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetDeviceSerialNumber(ulong deviceId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern InputDeviceCharacteristics GetDeviceCharacteristics(ulong deviceId);

		internal static InputDeviceRole GetDeviceRole(ulong deviceId)
		{
			InputDeviceCharacteristics deviceCharacteristics = InputDevices.GetDeviceCharacteristics(deviceId);
			bool flag = (deviceCharacteristics & (InputDeviceCharacteristics.HeadMounted | InputDeviceCharacteristics.TrackedDevice)) == (InputDeviceCharacteristics.HeadMounted | InputDeviceCharacteristics.TrackedDevice);
			InputDeviceRole result;
			if (flag)
			{
				result = InputDeviceRole.Generic;
			}
			else
			{
				bool flag2 = (deviceCharacteristics & (InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.TrackedDevice | InputDeviceCharacteristics.Left)) == (InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.TrackedDevice | InputDeviceCharacteristics.Left);
				if (flag2)
				{
					result = InputDeviceRole.LeftHanded;
				}
				else
				{
					bool flag3 = (deviceCharacteristics & (InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.TrackedDevice | InputDeviceCharacteristics.Right)) == (InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.TrackedDevice | InputDeviceCharacteristics.Right);
					if (flag3)
					{
						result = InputDeviceRole.RightHanded;
					}
					else
					{
						bool flag4 = (deviceCharacteristics & InputDeviceCharacteristics.Controller) == InputDeviceCharacteristics.Controller;
						if (flag4)
						{
							result = InputDeviceRole.GameController;
						}
						else
						{
							bool flag5 = (deviceCharacteristics & (InputDeviceCharacteristics.TrackedDevice | InputDeviceCharacteristics.TrackingReference)) == (InputDeviceCharacteristics.TrackedDevice | InputDeviceCharacteristics.TrackingReference);
							if (flag5)
							{
								result = InputDeviceRole.TrackingReference;
							}
							else
							{
								bool flag6 = (deviceCharacteristics & InputDeviceCharacteristics.TrackedDevice) == InputDeviceCharacteristics.TrackedDevice;
								if (flag6)
								{
									result = InputDeviceRole.HardwareTracker;
								}
								else
								{
									result = InputDeviceRole.Unknown;
								}
							}
						}
					}
				}
			}
			return result;
		}
	}
}
