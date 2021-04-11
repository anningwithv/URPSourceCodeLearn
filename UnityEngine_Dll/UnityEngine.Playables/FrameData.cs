using System;

namespace UnityEngine.Playables
{
	public struct FrameData
	{
		[Flags]
		internal enum Flags
		{
			Evaluate = 1,
			SeekOccured = 2,
			Loop = 4,
			Hold = 8,
			EffectivePlayStateDelayed = 16,
			EffectivePlayStatePlaying = 32
		}

		public enum EvaluationType
		{
			Evaluate,
			Playback
		}

		internal ulong m_FrameID;

		internal double m_DeltaTime;

		internal float m_Weight;

		internal float m_EffectiveWeight;

		internal double m_EffectiveParentDelay;

		internal float m_EffectiveParentSpeed;

		internal float m_EffectiveSpeed;

		internal FrameData.Flags m_Flags;

		internal PlayableOutput m_Output;

		public ulong frameId
		{
			get
			{
				return this.m_FrameID;
			}
		}

		public float deltaTime
		{
			get
			{
				return (float)this.m_DeltaTime;
			}
		}

		public float weight
		{
			get
			{
				return this.m_Weight;
			}
		}

		public float effectiveWeight
		{
			get
			{
				return this.m_EffectiveWeight;
			}
		}

		[Obsolete("effectiveParentDelay is obsolete; use a custom ScriptPlayable to implement this feature", false)]
		public double effectiveParentDelay
		{
			get
			{
				return this.m_EffectiveParentDelay;
			}
		}

		public float effectiveParentSpeed
		{
			get
			{
				return this.m_EffectiveParentSpeed;
			}
		}

		public float effectiveSpeed
		{
			get
			{
				return this.m_EffectiveSpeed;
			}
		}

		public FrameData.EvaluationType evaluationType
		{
			get
			{
				return this.HasFlags(FrameData.Flags.Evaluate) ? FrameData.EvaluationType.Evaluate : FrameData.EvaluationType.Playback;
			}
		}

		public bool seekOccurred
		{
			get
			{
				return this.HasFlags(FrameData.Flags.SeekOccured);
			}
		}

		public bool timeLooped
		{
			get
			{
				return this.HasFlags(FrameData.Flags.Loop);
			}
		}

		public bool timeHeld
		{
			get
			{
				return this.HasFlags(FrameData.Flags.Hold);
			}
		}

		public PlayableOutput output
		{
			get
			{
				return this.m_Output;
			}
		}

		public PlayState effectivePlayState
		{
			get
			{
				bool flag = this.HasFlags(FrameData.Flags.EffectivePlayStateDelayed);
				PlayState result;
				if (flag)
				{
					result = PlayState.Delayed;
				}
				else
				{
					bool flag2 = this.HasFlags(FrameData.Flags.EffectivePlayStatePlaying);
					if (flag2)
					{
						result = PlayState.Playing;
					}
					else
					{
						result = PlayState.Paused;
					}
				}
				return result;
			}
		}

		private bool HasFlags(FrameData.Flags flag)
		{
			return (this.m_Flags & flag) == flag;
		}
	}
}
