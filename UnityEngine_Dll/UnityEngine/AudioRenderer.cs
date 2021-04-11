using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Audio;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeType(Header = "Modules/Audio/Public/ScriptBindings/AudioRenderer.bindings.h")]
	public class AudioRenderer
	{
		public static bool Start()
		{
			return AudioRenderer.Internal_AudioRenderer_Start();
		}

		public static bool Stop()
		{
			return AudioRenderer.Internal_AudioRenderer_Stop();
		}

		public static int GetSampleCountForCaptureFrame()
		{
			return AudioRenderer.Internal_AudioRenderer_GetSampleCountForCaptureFrame();
		}

		internal static bool AddMixerGroupSink(AudioMixerGroup mixerGroup, NativeArray<float> buffer, bool excludeFromMix)
		{
			return AudioRenderer.Internal_AudioRenderer_AddMixerGroupSink(mixerGroup, buffer.GetUnsafePtr<float>(), buffer.Length, excludeFromMix);
		}

		public static bool Render(NativeArray<float> buffer)
		{
			return AudioRenderer.Internal_AudioRenderer_Render(buffer.GetUnsafePtr<float>(), buffer.Length);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Internal_AudioRenderer_Start();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Internal_AudioRenderer_Stop();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int Internal_AudioRenderer_GetSampleCountForCaptureFrame();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool Internal_AudioRenderer_AddMixerGroupSink(AudioMixerGroup mixerGroup, void* ptr, int length, bool excludeFromMix);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool Internal_AudioRenderer_Render(void* ptr, int length);
	}
}
