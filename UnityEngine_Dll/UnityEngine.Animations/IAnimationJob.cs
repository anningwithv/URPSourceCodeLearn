using System;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	[JobProducerType(typeof(ProcessAnimationJobStruct<>)), MovedFrom("UnityEngine.Experimental.Animations")]
	public interface IAnimationJob
	{
		void ProcessAnimation(AnimationStream stream);

		void ProcessRootMotion(AnimationStream stream);
	}
}
