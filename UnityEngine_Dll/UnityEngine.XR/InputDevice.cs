using System;
using System.Collections.Generic;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	[NativeConditional("ENABLE_VR"), UsedByNativeCode]
	public struct InputDevice : IEquatable<InputDevice>
	{
		private static List<XRInputSubsystem> s_InputSubsystemCache;

		private ulong m_DeviceId;

		private bool m_Initialized;

		private ulong deviceId
		{
			get
			{
				return this.m_Initialized ? this.m_DeviceId : 18446744073709551615uL;
			}
		}

		public XRInputSubsystem subsystem
		{
			get
			{
				bool flag = InputDevice.s_InputSubsystemCache == null;
				if (flag)
				{
					InputDevice.s_InputSubsystemCache = new List<XRInputSubsystem>();
				}
				bool initialized = this.m_Initialized;
				XRInputSubsystem result;
				if (initialized)
				{
					uint num = (uint)(this.m_DeviceId >> 32);
					SubsystemManager.GetSubsystems<XRInputSubsystem>(InputDevice.s_InputSubsystemCache);
					for (int i = 0; i < InputDevice.s_InputSubsystemCache.Count; i++)
					{
						bool flag2 = num == InputDevice.s_InputSubsystemCache[i].GetIndex();
						if (flag2)
						{
							result = InputDevice.s_InputSubsystemCache[i];
							return result;
						}
					}
				}
				result = null;
				return result;
			}
		}

		public bool isValid
		{
			get
			{
				return this.IsValidId() && InputDevices.IsDeviceValid(this.m_DeviceId);
			}
		}

		public string name
		{
			get
			{
				return this.IsValidId() ? InputDevices.GetDeviceName(this.m_DeviceId) : null;
			}
		}

		[Obsolete("This API has been marked as deprecated and will be removed in future versions. Please use InputDevice.characteristics instead.")]
		public InputDeviceRole role
		{
			get
			{
				return this.IsValidId() ? InputDevices.GetDeviceRole(this.m_DeviceId) : InputDeviceRole.Unknown;
			}
		}

		public string manufacturer
		{
			get
			{
				return this.IsValidId() ? InputDevices.GetDeviceManufacturer(this.m_DeviceId) : null;
			}
		}

		public string serialNumber
		{
			get
			{
				return this.IsValidId() ? InputDevices.GetDeviceSerialNumber(this.m_DeviceId) : null;
			}
		}

		public InputDeviceCharacteristics characteristics
		{
			get
			{
				return this.IsValidId() ? InputDevices.GetDeviceCharacteristics(this.m_DeviceId) : InputDeviceCharacteristics.None;
			}
		}

		internal InputDevice(ulong deviceId)
		{
			this.m_DeviceId = deviceId;
			this.m_Initialized = true;
		}

		private bool IsValidId()
		{
			return this.deviceId != 18446744073709551615uL;
		}

		public bool SendHapticImpulse(uint channel, float amplitude, float duration = 1f)
		{
			bool flag = !this.IsValidId();
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = amplitude < 0f;
				if (flag2)
				{
					throw new ArgumentException("Amplitude of SendHapticImpulse cannot be negative.");
				}
				bool flag3 = duration < 0f;
				if (flag3)
				{
					throw new ArgumentException("Duration of SendHapticImpulse cannot be negative.");
				}
				result = InputDevices.SendHapticImpulse(this.m_DeviceId, channel, amplitude, duration);
			}
			return result;
		}

		public bool SendHapticBuffer(uint channel, byte[] buffer)
		{
			bool flag = !this.IsValidId();
			return !flag && InputDevices.SendHapticBuffer(this.m_DeviceId, channel, buffer);
		}

		public bool TryGetHapticCapabilities(out HapticCapabilities capabilities)
		{
			bool flag = this.CheckValidAndSetDefault<HapticCapabilities>(out capabilities);
			return flag && InputDevices.TryGetHapticCapabilities(this.m_DeviceId, out capabilities);
		}

		public void StopHaptics()
		{
			bool flag = this.IsValidId();
			if (flag)
			{
				InputDevices.StopHaptics(this.m_DeviceId);
			}
		}

		public bool TryGetFeatureUsages(List<InputFeatureUsage> featureUsages)
		{
			bool flag = this.IsValidId();
			return flag && InputDevices.TryGetFeatureUsages(this.m_DeviceId, featureUsages);
		}

		public bool TryGetFeatureValue(InputFeatureUsage<bool> usage, out bool value)
		{
			bool flag = this.CheckValidAndSetDefault<bool>(out value);
			return flag && InputDevices.TryGetFeatureValue_bool(this.m_DeviceId, usage.name, out value);
		}

		public bool TryGetFeatureValue(InputFeatureUsage<uint> usage, out uint value)
		{
			bool flag = this.CheckValidAndSetDefault<uint>(out value);
			return flag && InputDevices.TryGetFeatureValue_UInt32(this.m_DeviceId, usage.name, out value);
		}

		public bool TryGetFeatureValue(InputFeatureUsage<float> usage, out float value)
		{
			bool flag = this.CheckValidAndSetDefault<float>(out value);
			return flag && InputDevices.TryGetFeatureValue_float(this.m_DeviceId, usage.name, out value);
		}

		public bool TryGetFeatureValue(InputFeatureUsage<Vector2> usage, out Vector2 value)
		{
			bool flag = this.CheckValidAndSetDefault<Vector2>(out value);
			return flag && InputDevices.TryGetFeatureValue_Vector2f(this.m_DeviceId, usage.name, out value);
		}

		public bool TryGetFeatureValue(InputFeatureUsage<Vector3> usage, out Vector3 value)
		{
			bool flag = this.CheckValidAndSetDefault<Vector3>(out value);
			return flag && InputDevices.TryGetFeatureValue_Vector3f(this.m_DeviceId, usage.name, out value);
		}

		public bool TryGetFeatureValue(InputFeatureUsage<Quaternion> usage, out Quaternion value)
		{
			bool flag = this.CheckValidAndSetDefault<Quaternion>(out value);
			return flag && InputDevices.TryGetFeatureValue_Quaternionf(this.m_DeviceId, usage.name, out value);
		}

		public bool TryGetFeatureValue(InputFeatureUsage<Hand> usage, out Hand value)
		{
			bool flag = this.CheckValidAndSetDefault<Hand>(out value);
			return flag && InputDevices.TryGetFeatureValue_XRHand(this.m_DeviceId, usage.name, out value);
		}

		public bool TryGetFeatureValue(InputFeatureUsage<Bone> usage, out Bone value)
		{
			bool flag = this.CheckValidAndSetDefault<Bone>(out value);
			return flag && InputDevices.TryGetFeatureValue_XRBone(this.m_DeviceId, usage.name, out value);
		}

		public bool TryGetFeatureValue(InputFeatureUsage<Eyes> usage, out Eyes value)
		{
			bool flag = this.CheckValidAndSetDefault<Eyes>(out value);
			return flag && InputDevices.TryGetFeatureValue_XREyes(this.m_DeviceId, usage.name, out value);
		}

		public bool TryGetFeatureValue(InputFeatureUsage<byte[]> usage, byte[] value)
		{
			bool flag = this.IsValidId();
			return flag && InputDevices.TryGetFeatureValue_Custom(this.m_DeviceId, usage.name, value);
		}

		public bool TryGetFeatureValue(InputFeatureUsage<InputTrackingState> usage, out InputTrackingState value)
		{
			bool flag = this.IsValidId();
			bool result;
			if (flag)
			{
				uint num = 0u;
				bool flag2 = InputDevices.TryGetFeatureValue_UInt32(this.m_DeviceId, usage.name, out num);
				if (flag2)
				{
					value = (InputTrackingState)num;
					result = true;
					return result;
				}
			}
			value = InputTrackingState.None;
			result = false;
			return result;
		}

		public bool TryGetFeatureValue(InputFeatureUsage<bool> usage, DateTime time, out bool value)
		{
			bool flag = this.CheckValidAndSetDefault<bool>(out value);
			return flag && InputDevices.TryGetFeatureValueAtTime_bool(this.m_DeviceId, usage.name, TimeConverter.LocalDateTimeToUnixTimeMilliseconds(time), out value);
		}

		public bool TryGetFeatureValue(InputFeatureUsage<uint> usage, DateTime time, out uint value)
		{
			bool flag = this.CheckValidAndSetDefault<uint>(out value);
			return flag && InputDevices.TryGetFeatureValueAtTime_UInt32(this.m_DeviceId, usage.name, TimeConverter.LocalDateTimeToUnixTimeMilliseconds(time), out value);
		}

		public bool TryGetFeatureValue(InputFeatureUsage<float> usage, DateTime time, out float value)
		{
			bool flag = this.CheckValidAndSetDefault<float>(out value);
			return flag && InputDevices.TryGetFeatureValueAtTime_float(this.m_DeviceId, usage.name, TimeConverter.LocalDateTimeToUnixTimeMilliseconds(time), out value);
		}

		public bool TryGetFeatureValue(InputFeatureUsage<Vector2> usage, DateTime time, out Vector2 value)
		{
			bool flag = this.CheckValidAndSetDefault<Vector2>(out value);
			return flag && InputDevices.TryGetFeatureValueAtTime_Vector2f(this.m_DeviceId, usage.name, TimeConverter.LocalDateTimeToUnixTimeMilliseconds(time), out value);
		}

		public bool TryGetFeatureValue(InputFeatureUsage<Vector3> usage, DateTime time, out Vector3 value)
		{
			bool flag = this.CheckValidAndSetDefault<Vector3>(out value);
			return flag && InputDevices.TryGetFeatureValueAtTime_Vector3f(this.m_DeviceId, usage.name, TimeConverter.LocalDateTimeToUnixTimeMilliseconds(time), out value);
		}

		public bool TryGetFeatureValue(InputFeatureUsage<Quaternion> usage, DateTime time, out Quaternion value)
		{
			bool flag = this.CheckValidAndSetDefault<Quaternion>(out value);
			return flag && InputDevices.TryGetFeatureValueAtTime_Quaternionf(this.m_DeviceId, usage.name, TimeConverter.LocalDateTimeToUnixTimeMilliseconds(time), out value);
		}

		public bool TryGetFeatureValue(InputFeatureUsage<InputTrackingState> usage, DateTime time, out InputTrackingState value)
		{
			bool flag = this.IsValidId();
			bool result;
			if (flag)
			{
				uint num = 0u;
				bool flag2 = InputDevices.TryGetFeatureValueAtTime_UInt32(this.m_DeviceId, usage.name, TimeConverter.LocalDateTimeToUnixTimeMilliseconds(time), out num);
				if (flag2)
				{
					value = (InputTrackingState)num;
					result = true;
					return result;
				}
			}
			value = InputTrackingState.None;
			result = false;
			return result;
		}

		private bool CheckValidAndSetDefault<T>(out T value)
		{
			value = default(T);
			return this.IsValidId();
		}

		public override bool Equals(object obj)
		{
			bool flag = !(obj is InputDevice);
			return !flag && this.Equals((InputDevice)obj);
		}

		public bool Equals(InputDevice other)
		{
			return this.deviceId == other.deviceId;
		}

		public override int GetHashCode()
		{
			return this.deviceId.GetHashCode();
		}

		public static bool operator ==(InputDevice a, InputDevice b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(InputDevice a, InputDevice b)
		{
			return !(a == b);
		}
	}
}
