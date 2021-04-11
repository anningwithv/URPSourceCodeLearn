using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;

namespace UnityEngine.Animations
{
	[NativeHeader("Runtime/Director/Core/HPlayableOutput.h"), NativeHeader("Runtime/Director/Core/HPlayable.h"), NativeHeader("Modules/Animation/ScriptBindings/AnimationPlayableGraphExtensions.bindings.h"), NativeHeader("Modules/Animation/Animator.h"), StaticAccessor("AnimationPlayableGraphExtensionsBindings", StaticAccessorType.DoubleColon)]
	internal static class AnimationPlayableGraphExtensions
	{
		internal static void SyncUpdateAndTimeMode(this PlayableGraph graph, Animator animator)
		{
			AnimationPlayableGraphExtensions.InternalSyncUpdateAndTimeMode(ref graph, animator);
		}

		internal static void DestroyOutput(this PlayableGraph graph, PlayableOutputHandle handle)
		{
			AnimationPlayableGraphExtensions.InternalDestroyOutput(ref graph, ref handle);
		}

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool InternalCreateAnimationOutput(ref PlayableGraph graph, string name, out PlayableOutputHandle handle);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalSyncUpdateAndTimeMode(ref PlayableGraph graph, [NotNull("ArgumentNullException")] Animator animator);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalDestroyOutput(ref PlayableGraph graph, ref PlayableOutputHandle handle);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int InternalAnimationOutputCount(ref PlayableGraph graph);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalGetAnimationOutput(ref PlayableGraph graph, int index, out PlayableOutputHandle handle);
	}
}
