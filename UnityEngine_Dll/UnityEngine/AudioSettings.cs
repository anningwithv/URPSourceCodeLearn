using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Modules/Audio/Public/ScriptBindings/Audio.bindings.h"), StaticAccessor("GetAudioManager()", StaticAccessorType.Dot)]
	public sealed class AudioSettings
	{
		public delegate void AudioConfigurationChangeHandler(bool deviceWasChanged);

		public static class Mobile
		{
			[method: CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
			public static event Action<bool> OnMuteStateChanged;

			public static bool muteState
			{
				get
				{
					return false;
				}
			}

			public static bool stopAudioOutputOnMute
			{
				get
				{
					return false;
				}
				set
				{
					Debug.LogWarning("Setting AudioSettings.Mobile.stopAudioOutputOnMute is possible on iOS and Android only");
				}
			}

			public static bool audioOutputStarted
			{
				get
				{
					return true;
				}
			}

			public static void StartAudioOutput()
			{
				Debug.LogWarning("AudioSettings.Mobile.StartAudioOutput is implemented for iOS and Android only");
			}

			public static void StopAudioOutput()
			{
				Debug.LogWarning("AudioSettings.Mobile.StopAudioOutput is implemented for iOS and Android only");
			}
		}

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event AudioSettings.AudioConfigurationChangeHandler OnAudioConfigurationChanged;

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("AudioSettings.driverCaps is obsolete. Use driverCapabilities instead (UnityUpgradable) -> driverCapabilities", true)]
		public static AudioSpeakerMode driverCaps
		{
			get
			{
				return AudioSettings.driverCapabilities;
			}
		}

		public static extern AudioSpeakerMode driverCapabilities
		{
			[NativeName("GetSpeakerModeCaps")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static AudioSpeakerMode speakerMode
		{
			get
			{
				return AudioSettings.GetSpeakerMode();
			}
			set
			{
				Debug.LogWarning("Setting AudioSettings.speakerMode is deprecated and has been replaced by audio project settings and the AudioSettings.GetConfiguration/AudioSettings.Reset API.");
				AudioConfiguration configuration = AudioSettings.GetConfiguration();
				configuration.speakerMode = value;
				bool flag = !AudioSettings.SetConfiguration(configuration);
				if (flag)
				{
					Debug.LogWarning("Setting AudioSettings.speakerMode failed");
				}
			}
		}

		internal static extern int profilerCaptureFlags
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern double dspTime
		{
			[NativeMethod(Name = "GetDSPTime", IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static int outputSampleRate
		{
			get
			{
				return AudioSettings.GetSampleRate();
			}
			set
			{
				Debug.LogWarning("Setting AudioSettings.outputSampleRate is deprecated and has been replaced by audio project settings and the AudioSettings.GetConfiguration/AudioSettings.Reset API.");
				AudioConfiguration configuration = AudioSettings.GetConfiguration();
				configuration.sampleRate = value;
				bool flag = !AudioSettings.SetConfiguration(configuration);
				if (flag)
				{
					Debug.LogWarning("Setting AudioSettings.outputSampleRate failed");
				}
			}
		}

		internal static extern bool editingInPlaymode
		{
			[NativeName("IsEditingInPlaymode")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("SetEditingInPlaymode")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal static extern bool unityAudioDisabled
		{
			[NativeName("IsAudioDisabled")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioSpeakerMode GetSpeakerMode();

		[NativeMethod(Name = "AudioSettings::SetConfiguration", IsFreeFunction = true), NativeThrows]
		private static bool SetConfiguration(AudioConfiguration config)
		{
			return AudioSettings.SetConfiguration_Injected(ref config);
		}

		[NativeMethod(Name = "AudioSettings::GetSampleRate", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSampleRate();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetSpatializerName(string pluginName);

		[NativeMethod(Name = "AudioSettings::GetDSPBufferSize", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void GetDSPBufferSize(out int bufferLength, out int numBuffers);

		[Obsolete("AudioSettings.SetDSPBufferSize is deprecated and has been replaced by audio project settings and the AudioSettings.GetConfiguration/AudioSettings.Reset API.")]
		public static void SetDSPBufferSize(int bufferLength, int numBuffers)
		{
			Debug.LogWarning("AudioSettings.SetDSPBufferSize is deprecated and has been replaced by audio project settings and the AudioSettings.GetConfiguration/AudioSettings.Reset API.");
			AudioConfiguration configuration = AudioSettings.GetConfiguration();
			configuration.dspBufferSize = bufferLength;
			bool flag = !AudioSettings.SetConfiguration(configuration);
			if (flag)
			{
				Debug.LogWarning("SetDSPBufferSize failed");
			}
		}

		[NativeMethod(Name = "AudioSettings::GetSpatializerNames", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetSpatializerPluginNames();

		[NativeName("GetCurrentSpatializerDefinitionName")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetSpatializerPluginName();

		public static void SetSpatializerPluginName(string pluginName)
		{
			bool flag = !AudioSettings.SetSpatializerName(pluginName);
			if (flag)
			{
				throw new ArgumentException("Invalid spatializer plugin name");
			}
		}

		public static AudioConfiguration GetConfiguration()
		{
			AudioConfiguration result;
			AudioSettings.GetConfiguration_Injected(out result);
			return result;
		}

		public static bool Reset(AudioConfiguration config)
		{
			return AudioSettings.SetConfiguration(config);
		}

		[RequiredByNativeCode]
		internal static void InvokeOnAudioConfigurationChanged(bool deviceWasChanged)
		{
			bool flag = AudioSettings.OnAudioConfigurationChanged != null;
			if (flag)
			{
				AudioSettings.OnAudioConfigurationChanged(deviceWasChanged);
			}
		}

		[NativeMethod(Name = "AudioSettings::GetCurrentAmbisonicDefinitionName", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetAmbisonicDecoderPluginName();

		[NativeMethod(Name = "AudioSettings::SetAmbisonicName", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetAmbisonicDecoderPluginName(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetConfiguration_Injected(ref AudioConfiguration config);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetConfiguration_Injected(out AudioConfiguration ret);
	}
}
