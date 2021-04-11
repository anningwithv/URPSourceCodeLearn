using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	[NativeHeader("Runtime/Director/Core/HPlayableGraph.h"), NativeHeader("Runtime/Director/Core/HPlayableOutput.h"), NativeHeader("Modules/Animation/ScriptBindings/AnimationPlayableOutput.bindings.h"), NativeHeader("Modules/Animation/Director/AnimationPlayableOutput.h"), NativeHeader("Modules/Animation/Animator.h"), StaticAccessor("AnimationPlayableOutputBindings", StaticAccessorType.DoubleColon), RequiredByNativeCode]
	public struct AnimationPlayableOutput : IPlayableOutput
	{
		private PlayableOutputHandle m_Handle;

		public static AnimationPlayableOutput Null
		{
			get
			{
				return new AnimationPlayableOutput(PlayableOutputHandle.Null);
			}
		}

		public static AnimationPlayableOutput Create(PlayableGraph graph, string name, Animator target)
		{
			PlayableOutputHandle handle;
			bool flag = !AnimationPlayableGraphExtensions.InternalCreateAnimationOutput(ref graph, name, out handle);
			AnimationPlayableOutput result;
			if (flag)
			{
				result = AnimationPlayableOutput.Null;
			}
			else
			{
				AnimationPlayableOutput animationPlayableOutput = new AnimationPlayableOutput(handle);
				animationPlayableOutput.SetTarget(target);
				result = animationPlayableOutput;
			}
			return result;
		}

		internal AnimationPlayableOutput(PlayableOutputHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOutputOfType<AnimationPlayableOutput>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationPlayableOutput.");
				}
			}
			this.m_Handle = handle;
		}

		public PlayableOutputHandle GetHandle()
		{
			return this.m_Handle;
		}

		public static implicit operator PlayableOutput(AnimationPlayableOutput output)
		{
			return new PlayableOutput(output.GetHandle());
		}

		public static explicit operator AnimationPlayableOutput(PlayableOutput output)
		{
			return new AnimationPlayableOutput(output.GetHandle());
		}

		public Animator GetTarget()
		{
			return AnimationPlayableOutput.InternalGetTarget(ref this.m_Handle);
		}

		public void SetTarget(Animator value)
		{
			AnimationPlayableOutput.InternalSetTarget(ref this.m_Handle, value);
		}

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Animator InternalGetTarget(ref PlayableOutputHandle handle);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetTarget(ref PlayableOutputHandle handle, Animator target);
	}
}
