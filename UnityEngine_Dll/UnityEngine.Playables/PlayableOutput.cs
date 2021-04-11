using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	[RequiredByNativeCode]
	public struct PlayableOutput : IPlayableOutput, IEquatable<PlayableOutput>
	{
		private PlayableOutputHandle m_Handle;

		private static readonly PlayableOutput m_NullPlayableOutput = new PlayableOutput(PlayableOutputHandle.Null);

		public static PlayableOutput Null
		{
			get
			{
				return PlayableOutput.m_NullPlayableOutput;
			}
		}

		[VisibleToOtherModules]
		internal PlayableOutput(PlayableOutputHandle handle)
		{
			this.m_Handle = handle;
		}

		public PlayableOutputHandle GetHandle()
		{
			return this.m_Handle;
		}

		public bool IsPlayableOutputOfType<T>() where T : struct, IPlayableOutput
		{
			return this.GetHandle().IsPlayableOutputOfType<T>();
		}

		public Type GetPlayableOutputType()
		{
			return this.GetHandle().GetPlayableOutputType();
		}

		public bool Equals(PlayableOutput other)
		{
			return this.GetHandle() == other.GetHandle();
		}
	}
}
