using System;
using System.Runtime.CompilerServices;
using UnityEngine.Animations;
using UnityEngine.Bindings;
using UnityEngine.Playables;

namespace UnityEngine.Experimental.Animations
{
	[NativeHeader("Modules/Animation/AnimatorDefines.h"), NativeHeader("Modules/Animation/ScriptBindings/AnimationPlayableOutputExtensions.bindings.h"), StaticAccessor("AnimationPlayableOutputExtensionsBindings", StaticAccessorType.DoubleColon)]
	public static class AnimationPlayableOutputExtensions
	{
		public static AnimationStreamSource GetAnimationStreamSource(this AnimationPlayableOutput output)
		{
			return AnimationPlayableOutputExtensions.InternalGetAnimationStreamSource(output.GetHandle());
		}

		public static void SetAnimationStreamSource(this AnimationPlayableOutput output, AnimationStreamSource streamSource)
		{
			AnimationPlayableOutputExtensions.InternalSetAnimationStreamSource(output.GetHandle(), streamSource);
		}

		public static ushort GetSortingOrder(this AnimationPlayableOutput output)
		{
			return (ushort)AnimationPlayableOutputExtensions.InternalGetSortingOrder(output.GetHandle());
		}

		public static void SetSortingOrder(this AnimationPlayableOutput output, ushort sortingOrder)
		{
			AnimationPlayableOutputExtensions.InternalSetSortingOrder(output.GetHandle(), (int)sortingOrder);
		}

		[NativeThrows]
		private static AnimationStreamSource InternalGetAnimationStreamSource(PlayableOutputHandle output)
		{
			return AnimationPlayableOutputExtensions.InternalGetAnimationStreamSource_Injected(ref output);
		}

		[NativeThrows]
		private static void InternalSetAnimationStreamSource(PlayableOutputHandle output, AnimationStreamSource streamSource)
		{
			AnimationPlayableOutputExtensions.InternalSetAnimationStreamSource_Injected(ref output, streamSource);
		}

		[NativeThrows]
		private static int InternalGetSortingOrder(PlayableOutputHandle output)
		{
			return AnimationPlayableOutputExtensions.InternalGetSortingOrder_Injected(ref output);
		}

		[NativeThrows]
		private static void InternalSetSortingOrder(PlayableOutputHandle output, int sortingOrder)
		{
			AnimationPlayableOutputExtensions.InternalSetSortingOrder_Injected(ref output, sortingOrder);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimationStreamSource InternalGetAnimationStreamSource_Injected(ref PlayableOutputHandle output);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetAnimationStreamSource_Injected(ref PlayableOutputHandle output, AnimationStreamSource streamSource);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int InternalGetSortingOrder_Injected(ref PlayableOutputHandle output);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetSortingOrder_Injected(ref PlayableOutputHandle output, int sortingOrder);
	}
}
