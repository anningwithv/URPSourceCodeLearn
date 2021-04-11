using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[StaticAccessor("AudioListenerBindings", StaticAccessorType.DoubleColon), RequireComponent(typeof(Transform))]
	public sealed class AudioListener : AudioBehaviour
	{
		public static extern float volume
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("ListenerPause")]
		public static extern bool pause
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern AudioVelocityUpdateMode velocityUpdateMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetOutputDataHelper([Out] float[] samples, int channel);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSpectrumDataHelper([Out] float[] samples, int channel, FFTWindow window);

		[Obsolete("GetOutputData returning a float[] is deprecated, use GetOutputData and pass a pre allocated array instead.")]
		public static float[] GetOutputData(int numSamples, int channel)
		{
			float[] array = new float[numSamples];
			AudioListener.GetOutputDataHelper(array, channel);
			return array;
		}

		public static void GetOutputData(float[] samples, int channel)
		{
			AudioListener.GetOutputDataHelper(samples, channel);
		}

		[Obsolete("GetSpectrumData returning a float[] is deprecated, use GetSpectrumData and pass a pre allocated array instead.")]
		public static float[] GetSpectrumData(int numSamples, int channel, FFTWindow window)
		{
			float[] array = new float[numSamples];
			AudioListener.GetSpectrumDataHelper(array, channel, window);
			return array;
		}

		public static void GetSpectrumData(float[] samples, int channel, FFTWindow window)
		{
			AudioListener.GetSpectrumDataHelper(samples, channel, window);
		}
	}
}
