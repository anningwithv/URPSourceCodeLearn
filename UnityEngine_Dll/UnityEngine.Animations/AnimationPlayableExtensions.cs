using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;

namespace UnityEngine.Animations
{
	[NativeHeader("Runtime/Director/Core/HPlayable.h"), NativeHeader("Modules/Animation/Director/AnimationPlayableExtensions.h"), NativeHeader("Modules/Animation/AnimationClip.h")]
	public static class AnimationPlayableExtensions
	{
		public static void SetAnimatedProperties<U>(this U playable, AnimationClip clip) where U : struct, IPlayable
		{
			PlayableHandle handle = playable.GetHandle();
			AnimationPlayableExtensions.SetAnimatedPropertiesInternal(ref handle, clip);
		}

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetAnimatedPropertiesInternal(ref PlayableHandle playable, AnimationClip animatedProperties);
	}
}
