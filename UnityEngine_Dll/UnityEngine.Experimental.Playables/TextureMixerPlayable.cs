using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Playables
{
	[NativeHeader("Runtime/Export/Director/TextureMixerPlayable.bindings.h"), NativeHeader("Runtime/Graphics/Director/TextureMixerPlayable.h"), NativeHeader("Runtime/Director/Core/HPlayable.h"), StaticAccessor("TextureMixerPlayableBindings", StaticAccessorType.DoubleColon), RequiredByNativeCode]
	public struct TextureMixerPlayable : IPlayable, IEquatable<TextureMixerPlayable>
	{
		private PlayableHandle m_Handle;

		public static TextureMixerPlayable Create(PlayableGraph graph)
		{
			PlayableHandle handle = TextureMixerPlayable.CreateHandle(graph);
			return new TextureMixerPlayable(handle);
		}

		private static PlayableHandle CreateHandle(PlayableGraph graph)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !TextureMixerPlayable.CreateTextureMixerPlayableInternal(ref graph, ref @null);
			PlayableHandle result;
			if (flag)
			{
				result = PlayableHandle.Null;
			}
			else
			{
				result = @null;
			}
			return result;
		}

		internal TextureMixerPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<TextureMixerPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an TextureMixerPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		public static implicit operator Playable(TextureMixerPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		public static explicit operator TextureMixerPlayable(Playable playable)
		{
			return new TextureMixerPlayable(playable.GetHandle());
		}

		public bool Equals(TextureMixerPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateTextureMixerPlayableInternal(ref PlayableGraph graph, ref PlayableHandle handle);
	}
}
