using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[StaticAccessor("GetAudioManager()", StaticAccessorType.Dot)]
	public sealed class Microphone
	{
		public static extern string[] devices
		{
			[NativeName("GetRecordDevices")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMicrophoneDeviceIDFromName(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioClip StartRecord(int deviceID, bool loop, float lengthSec, int frequency);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EndRecord(int deviceID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsRecording(int deviceID);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRecordPosition(int deviceID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDeviceCaps(int deviceID, out int minFreq, out int maxFreq);

		public static AudioClip Start(string deviceName, bool loop, int lengthSec, int frequency)
		{
			int microphoneDeviceIDFromName = Microphone.GetMicrophoneDeviceIDFromName(deviceName);
			bool flag = microphoneDeviceIDFromName == -1;
			if (flag)
			{
				throw new ArgumentException("Couldn't acquire device ID for device name " + deviceName);
			}
			bool flag2 = lengthSec <= 0;
			if (flag2)
			{
				throw new ArgumentException("Length of recording must be greater than zero seconds (was: " + lengthSec.ToString() + " seconds)");
			}
			bool flag3 = lengthSec > 3600;
			if (flag3)
			{
				throw new ArgumentException("Length of recording must be less than one hour (was: " + lengthSec.ToString() + " seconds)");
			}
			bool flag4 = frequency <= 0;
			if (flag4)
			{
				throw new ArgumentException("Frequency of recording must be greater than zero (was: " + frequency.ToString() + " Hz)");
			}
			return Microphone.StartRecord(microphoneDeviceIDFromName, loop, (float)lengthSec, frequency);
		}

		public static void End(string deviceName)
		{
			int microphoneDeviceIDFromName = Microphone.GetMicrophoneDeviceIDFromName(deviceName);
			bool flag = microphoneDeviceIDFromName == -1;
			if (!flag)
			{
				Microphone.EndRecord(microphoneDeviceIDFromName);
			}
		}

		public static bool IsRecording(string deviceName)
		{
			int microphoneDeviceIDFromName = Microphone.GetMicrophoneDeviceIDFromName(deviceName);
			bool flag = microphoneDeviceIDFromName == -1;
			return !flag && Microphone.IsRecording(microphoneDeviceIDFromName);
		}

		public static int GetPosition(string deviceName)
		{
			int microphoneDeviceIDFromName = Microphone.GetMicrophoneDeviceIDFromName(deviceName);
			bool flag = microphoneDeviceIDFromName == -1;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = Microphone.GetRecordPosition(microphoneDeviceIDFromName);
			}
			return result;
		}

		public static void GetDeviceCaps(string deviceName, out int minFreq, out int maxFreq)
		{
			minFreq = 0;
			maxFreq = 0;
			int microphoneDeviceIDFromName = Microphone.GetMicrophoneDeviceIDFromName(deviceName);
			bool flag = microphoneDeviceIDFromName == -1;
			if (!flag)
			{
				Microphone.GetDeviceCaps(microphoneDeviceIDFromName, out minFreq, out maxFreq);
			}
		}
	}
}
