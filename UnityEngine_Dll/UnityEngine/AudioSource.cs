using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Audio;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	[StaticAccessor("AudioSourceBindings", StaticAccessorType.DoubleColon), RequireComponent(typeof(Transform))]
	public sealed class AudioSource : AudioBehaviour
	{
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("AudioSource.panLevel has been deprecated. Use AudioSource.spatialBlend instead (UnityUpgradable) -> spatialBlend", true)]
		public float panLevel
		{
			get
			{
				return this.spatialBlend;
			}
			set
			{
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("AudioSource.pan has been deprecated. Use AudioSource.panStereo instead (UnityUpgradable) -> panStereo", true)]
		public float pan
		{
			get
			{
				return this.panStereo;
			}
			set
			{
			}
		}

		public extern float volume
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public float pitch
		{
			get
			{
				return AudioSource.GetPitch(this);
			}
			set
			{
				AudioSource.SetPitch(this, value);
			}
		}

		[NativeProperty("SecPosition")]
		public extern float time
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("SamplePosition")]
		public extern int timeSamples
		{
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("AudioClip")]
		public extern AudioClip clip
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern AudioMixerGroup outputAudioMixerGroup
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool isPlaying
		{
			[NativeName("IsPlayingScripting")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool isVirtual
		{
			[NativeName("GetLastVirtualState")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool loop
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool ignoreListenerVolume
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool playOnAwake
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool ignoreListenerPause
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

		[NativeProperty("StereoPan")]
		public extern float panStereo
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("SpatialBlendMix")]
		public extern float spatialBlend
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool spatialize
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool spatializePostEffects
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float reverbZoneMix
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool bypassEffects
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool bypassListenerEffects
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool bypassReverbZones
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float dopplerLevel
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float spread
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int priority
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool mute
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float minDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float maxDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern AudioRolloffMode rolloffMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("minVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.", true)]
		public float minVolume
		{
			get
			{
				Debug.LogError("minVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
				return 0f;
			}
			set
			{
				Debug.LogError("minVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
			}
		}

		[Obsolete("maxVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.", true)]
		public float maxVolume
		{
			get
			{
				Debug.LogError("maxVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
				return 0f;
			}
			set
			{
				Debug.LogError("maxVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
			}
		}

		[Obsolete("rolloffFactor is not supported anymore. Use min-, maxDistance and rolloffMode instead.", true)]
		public float rolloffFactor
		{
			get
			{
				Debug.LogError("rolloffFactor is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
				return 0f;
			}
			set
			{
				Debug.LogError("rolloffFactor is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetPitch([NotNull("ArgumentNullException")] AudioSource source);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPitch([NotNull("ArgumentNullException")] AudioSource source, float pitch);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PlayHelper([NotNull("ArgumentNullException")] AudioSource source, ulong delay);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Play(double delay);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PlayOneShotHelper([NotNull("ArgumentNullException")] AudioSource source, [NotNull("NullExceptionObject")] AudioClip clip, float volumeScale);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Stop(bool stopOneShots);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetCustomCurveHelper([NotNull("ArgumentNullException")] AudioSource source, AudioSourceCurveType type, AnimationCurve curve);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimationCurve GetCustomCurveHelper([NotNull("ArgumentNullException")] AudioSource source, AudioSourceCurveType type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetOutputDataHelper([NotNull("ArgumentNullException")] AudioSource source, [Out] float[] samples, int channel);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSpectrumDataHelper([NotNull("ArgumentNullException")] AudioSource source, [Out] float[] samples, int channel, FFTWindow window);

		[ExcludeFromDocs]
		public void Play()
		{
			AudioSource.PlayHelper(this, 0uL);
		}

		public void Play([UnityEngine.Internal.DefaultValue("0")] ulong delay)
		{
			AudioSource.PlayHelper(this, delay);
		}

		public void PlayDelayed(float delay)
		{
			this.Play((delay < 0f) ? 0.0 : (-(double)delay));
		}

		public void PlayScheduled(double time)
		{
			this.Play((time < 0.0) ? 0.0 : time);
		}

		[ExcludeFromDocs]
		public void PlayOneShot(AudioClip clip)
		{
			this.PlayOneShot(clip, 1f);
		}

		public void PlayOneShot(AudioClip clip, [UnityEngine.Internal.DefaultValue("1.0F")] float volumeScale)
		{
			bool flag = clip == null;
			if (flag)
			{
				Debug.LogWarning("PlayOneShot was called with a null AudioClip.");
			}
			else
			{
				AudioSource.PlayOneShotHelper(this, clip, volumeScale);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetScheduledStartTime(double time);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetScheduledEndTime(double time);

		public void Stop()
		{
			this.Stop(true);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Pause();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void UnPause();

		[ExcludeFromDocs]
		public static void PlayClipAtPoint(AudioClip clip, Vector3 position)
		{
			AudioSource.PlayClipAtPoint(clip, position, 1f);
		}

		public static void PlayClipAtPoint(AudioClip clip, Vector3 position, [UnityEngine.Internal.DefaultValue("1.0F")] float volume)
		{
			GameObject gameObject = new GameObject("One shot audio");
			gameObject.transform.position = position;
			AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
			audioSource.clip = clip;
			audioSource.spatialBlend = 1f;
			audioSource.volume = volume;
			audioSource.Play();
			Object.Destroy(gameObject, clip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale));
		}

		public void SetCustomCurve(AudioSourceCurveType type, AnimationCurve curve)
		{
			AudioSource.SetCustomCurveHelper(this, type, curve);
		}

		public AnimationCurve GetCustomCurve(AudioSourceCurveType type)
		{
			return AudioSource.GetCustomCurveHelper(this, type);
		}

		[Obsolete("GetOutputData returning a float[] is deprecated, use GetOutputData and pass a pre allocated array instead.")]
		public float[] GetOutputData(int numSamples, int channel)
		{
			float[] array = new float[numSamples];
			AudioSource.GetOutputDataHelper(this, array, channel);
			return array;
		}

		public void GetOutputData(float[] samples, int channel)
		{
			AudioSource.GetOutputDataHelper(this, samples, channel);
		}

		[Obsolete("GetSpectrumData returning a float[] is deprecated, use GetSpectrumData and pass a pre allocated array instead.")]
		public float[] GetSpectrumData(int numSamples, int channel, FFTWindow window)
		{
			float[] array = new float[numSamples];
			AudioSource.GetSpectrumDataHelper(this, array, channel, window);
			return array;
		}

		public void GetSpectrumData(float[] samples, int channel, FFTWindow window)
		{
			AudioSource.GetSpectrumDataHelper(this, samples, channel, window);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool SetSpatializerFloat(int index, float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetSpatializerFloat(int index, out float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetAmbisonicDecoderFloat(int index, out float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool SetAmbisonicDecoderFloat(int index, float value);
	}
}
