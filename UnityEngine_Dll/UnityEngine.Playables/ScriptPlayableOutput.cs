using System;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	[RequiredByNativeCode]
	public struct ScriptPlayableOutput : IPlayableOutput
	{
		private PlayableOutputHandle m_Handle;

		public static ScriptPlayableOutput Null
		{
			get
			{
				return new ScriptPlayableOutput(PlayableOutputHandle.Null);
			}
		}

		public static ScriptPlayableOutput Create(PlayableGraph graph, string name)
		{
			PlayableOutputHandle handle;
			bool flag = !graph.CreateScriptOutputInternal(name, out handle);
			ScriptPlayableOutput result;
			if (flag)
			{
				result = ScriptPlayableOutput.Null;
			}
			else
			{
				result = new ScriptPlayableOutput(handle);
			}
			return result;
		}

		internal ScriptPlayableOutput(PlayableOutputHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOutputOfType<ScriptPlayableOutput>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not a ScriptPlayableOutput.");
				}
			}
			this.m_Handle = handle;
		}

		public PlayableOutputHandle GetHandle()
		{
			return this.m_Handle;
		}

		public static implicit operator PlayableOutput(ScriptPlayableOutput output)
		{
			return new PlayableOutput(output.GetHandle());
		}

		public static explicit operator ScriptPlayableOutput(PlayableOutput output)
		{
			return new ScriptPlayableOutput(output.GetHandle());
		}
	}
}
