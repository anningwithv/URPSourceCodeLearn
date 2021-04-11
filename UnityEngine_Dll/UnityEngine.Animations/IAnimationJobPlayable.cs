using System;
using UnityEngine.Playables;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	[MovedFrom("UnityEngine.Experimental.Animations")]
	public interface IAnimationJobPlayable : IPlayable
	{
		T GetJobData<T>() where T : struct, IAnimationJob;

		void SetJobData<T>(T jobData) where T : struct, IAnimationJob;
	}
}
