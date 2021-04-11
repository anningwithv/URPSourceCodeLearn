using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Modules/Audio/Public/ScriptBindings/Audio.bindings.h"), StaticAccessor("AudioClipBindings", StaticAccessorType.DoubleColon)]
	public sealed class AudioClip : Object
	{
		public delegate void PCMReaderCallback(float[] data);

		public delegate void PCMSetPositionCallback(int position);

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		private event AudioClip.PCMReaderCallback m_PCMReaderCallback = null;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		private event AudioClip.PCMSetPositionCallback m_PCMSetPositionCallback = null;

		[NativeProperty("LengthSec")]
		public extern float length
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("SampleCount")]
		public extern int samples
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("ChannelCount")]
		public extern int channels
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int frequency
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[Obsolete("Use AudioClip.loadState instead to get more detailed information about the loading process.")]
		public extern bool isReadyToPlay
		{
			[NativeName("ReadyToPlay")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern AudioClipLoadType loadType
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool preloadAudioData
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool ambisonic
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool loadInBackground
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern AudioDataLoadState loadState
		{
			[NativeMethod(Name = "AudioClipBindings::GetLoadState", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		private AudioClip()
		{
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetData([NotNull("NullExceptionObject")] AudioClip clip, [Out] float[] data, int numSamples, int samplesOffset);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetData([NotNull("NullExceptionObject")] AudioClip clip, float[] data, int numsamples, int samplesOffset);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioClip Construct_Internal();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string GetName();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void CreateUserSound(string name, int lengthSamples, int channels, int frequency, bool stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool LoadAudioData();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool UnloadAudioData();

		public bool GetData(float[] data, int offsetSamples)
		{
			bool flag = this.channels <= 0;
			bool result;
			if (flag)
			{
				Debug.Log("AudioClip.GetData failed; AudioClip " + this.GetName() + " contains no data");
				result = false;
			}
			else
			{
				int numSamples = (data != null) ? (data.Length / this.channels) : 0;
				result = AudioClip.GetData(this, data, numSamples, offsetSamples);
			}
			return result;
		}

		public bool SetData(float[] data, int offsetSamples)
		{
			bool flag = this.channels <= 0;
			bool result;
			if (flag)
			{
				Debug.Log("AudioClip.SetData failed; AudioClip " + this.GetName() + " contains no data");
				result = false;
			}
			else
			{
				bool flag2 = offsetSamples < 0 || offsetSamples >= this.samples;
				if (flag2)
				{
					throw new ArgumentException("AudioClip.SetData failed; invalid offsetSamples");
				}
				bool flag3 = data == null || data.Length == 0;
				if (flag3)
				{
					throw new ArgumentException("AudioClip.SetData failed; invalid data");
				}
				result = AudioClip.SetData(this, data, data.Length / this.channels, offsetSamples);
			}
			return result;
		}

		[Obsolete("The _3D argument of AudioClip is deprecated. Use the spatialBlend property of AudioSource instead to morph between 2D and 3D playback.")]
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, stream);
		}

		[Obsolete("The _3D argument of AudioClip is deprecated. Use the spatialBlend property of AudioSource instead to morph between 2D and 3D playback.")]
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream, AudioClip.PCMReaderCallback pcmreadercallback)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, stream, pcmreadercallback, null);
		}

		[Obsolete("The _3D argument of AudioClip is deprecated. Use the spatialBlend property of AudioSource instead to morph between 2D and 3D playback.")]
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream, AudioClip.PCMReaderCallback pcmreadercallback, AudioClip.PCMSetPositionCallback pcmsetpositioncallback)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, stream, pcmreadercallback, pcmsetpositioncallback);
		}

		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool stream)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, stream, null, null);
		}

		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool stream, AudioClip.PCMReaderCallback pcmreadercallback)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, stream, pcmreadercallback, null);
		}

		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool stream, AudioClip.PCMReaderCallback pcmreadercallback, AudioClip.PCMSetPositionCallback pcmsetpositioncallback)
		{
			bool flag = name == null;
			if (flag)
			{
				throw new NullReferenceException();
			}
			bool flag2 = lengthSamples <= 0;
			if (flag2)
			{
				throw new ArgumentException("Length of created clip must be larger than 0");
			}
			bool flag3 = channels <= 0;
			if (flag3)
			{
				throw new ArgumentException("Number of channels in created clip must be greater than 0");
			}
			bool flag4 = frequency <= 0;
			if (flag4)
			{
				throw new ArgumentException("Frequency in created clip must be greater than 0");
			}
			AudioClip audioClip = AudioClip.Construct_Internal();
			bool flag5 = pcmreadercallback != null;
			if (flag5)
			{
				audioClip.m_PCMReaderCallback += pcmreadercallback;
			}
			bool flag6 = pcmsetpositioncallback != null;
			if (flag6)
			{
				audioClip.m_PCMSetPositionCallback += pcmsetpositioncallback;
			}
			audioClip.CreateUserSound(name, lengthSamples, channels, frequency, stream);
			return audioClip;
		}

		[RequiredByNativeCode]
		private void InvokePCMReaderCallback_Internal(float[] data)
		{
			bool flag = this.m_PCMReaderCallback != null;
			if (flag)
			{
				this.m_PCMReaderCallback(data);
			}
		}

		[RequiredByNativeCode]
		private void InvokePCMSetPositionCallback_Internal(int position)
		{
			bool flag = this.m_PCMSetPositionCallback != null;
			if (flag)
			{
				this.m_PCMSetPositionCallback(position);
			}
		}
	}
}
