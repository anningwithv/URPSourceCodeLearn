using System;
using System.ComponentModel;
using UnityEngine.Bindings;

namespace UnityEngine.Playables
{
	public struct PlayableBinding
	{
		[VisibleToOtherModules]
		internal delegate PlayableOutput CreateOutputMethod(PlayableGraph graph, string name);

		private string m_StreamName;

		private UnityEngine.Object m_SourceObject;

		private Type m_SourceBindingType;

		private PlayableBinding.CreateOutputMethod m_CreateOutputMethod;

		public static readonly PlayableBinding[] None = new PlayableBinding[0];

		public static readonly double DefaultDuration = double.PositiveInfinity;

		public string streamName
		{
			get
			{
				return this.m_StreamName;
			}
			set
			{
				this.m_StreamName = value;
			}
		}

		public UnityEngine.Object sourceObject
		{
			get
			{
				return this.m_SourceObject;
			}
			set
			{
				this.m_SourceObject = value;
			}
		}

		public Type outputTargetType
		{
			get
			{
				return this.m_SourceBindingType;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("sourceBindingType is no longer supported on PlayableBinding. Use outputBindingType instead to get the required output target type, and the appropriate binding create method (e.g. AnimationPlayableBinding.Create(name, key)) to create PlayableBindings", true)]
		public Type sourceBindingType
		{
			get
			{
				return this.m_SourceBindingType;
			}
			set
			{
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("streamType is no longer supported on PlayableBinding. Use the appropriate binding create method (e.g. AnimationPlayableBinding.Create(name, key)) instead.", true)]
		public DataStreamType streamType
		{
			get
			{
				return DataStreamType.None;
			}
			set
			{
			}
		}

		internal PlayableOutput CreateOutput(PlayableGraph graph)
		{
			bool flag = this.m_CreateOutputMethod != null;
			PlayableOutput result;
			if (flag)
			{
				result = this.m_CreateOutputMethod(graph, this.m_StreamName);
			}
			else
			{
				result = PlayableOutput.Null;
			}
			return result;
		}

		[VisibleToOtherModules]
		internal static PlayableBinding CreateInternal(string name, UnityEngine.Object sourceObject, Type sourceType, PlayableBinding.CreateOutputMethod createFunction)
		{
			return new PlayableBinding
			{
				m_StreamName = name,
				m_SourceObject = sourceObject,
				m_SourceBindingType = sourceType,
				m_CreateOutputMethod = createFunction
			};
		}
	}
}
