using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.Audio
{
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioSourceExtensions.bindings.h"), NativeHeader("AudioScriptingClasses.h"), NativeHeader("Modules/Audio/Public/AudioSource.h")]
	internal static class AudioSourceExtensionsInternal
	{
		public static void RegisterSampleProvider(this AudioSource source, AudioSampleProvider provider)
		{
			AudioSourceExtensionsInternal.Internal_RegisterSampleProviderWithAudioSource(source, provider.id);
		}

		public static void UnregisterSampleProvider(this AudioSource source, AudioSampleProvider provider)
		{
			AudioSourceExtensionsInternal.Internal_UnregisterSampleProviderFromAudioSource(source, provider.id);
		}

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_RegisterSampleProviderWithAudioSource([NotNull("NullExceptionObject")] AudioSource source, uint providerId);

		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_UnregisterSampleProviderFromAudioSource([NotNull("NullExceptionObject")] AudioSource source, uint providerId);
	}
}
