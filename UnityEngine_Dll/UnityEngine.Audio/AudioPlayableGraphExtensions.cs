using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;

namespace UnityEngine.Audio
{
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioPlayableGraphExtensions.bindings.h"), NativeHeader("Runtime/Director/Core/HPlayableOutput.h"), StaticAccessor("AudioPlayableGraphExtensionsBindings", StaticAccessorType.DoubleColon)]
	internal static class AudioPlayableGraphExtensions
	{
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool InternalCreateAudioOutput(ref PlayableGraph graph, string name, out PlayableOutputHandle handle);
	}
}
