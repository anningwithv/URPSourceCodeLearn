using System;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	[RequiredByNativeCode]
	[Serializable]
	public abstract class PlayableBehaviour : IPlayableBehaviour, ICloneable
	{
		public PlayableBehaviour()
		{
		}

		public virtual void OnGraphStart(Playable playable)
		{
		}

		public virtual void OnGraphStop(Playable playable)
		{
		}

		public virtual void OnPlayableCreate(Playable playable)
		{
		}

		public virtual void OnPlayableDestroy(Playable playable)
		{
		}

		[Obsolete("OnBehaviourDelay is obsolete; use a custom ScriptPlayable to implement this feature", false)]
		public virtual void OnBehaviourDelay(Playable playable, FrameData info)
		{
		}

		public virtual void OnBehaviourPlay(Playable playable, FrameData info)
		{
		}

		public virtual void OnBehaviourPause(Playable playable, FrameData info)
		{
		}

		public virtual void PrepareData(Playable playable, FrameData info)
		{
		}

		public virtual void PrepareFrame(Playable playable, FrameData info)
		{
		}

		public virtual void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
		}

		public virtual object Clone()
		{
			return base.MemberwiseClone();
		}
	}
}
