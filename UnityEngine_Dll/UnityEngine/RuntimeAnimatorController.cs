using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Modules/Animation/RuntimeAnimatorController.h"), ExcludeFromObjectFactory, UsedByNativeCode]
	public class RuntimeAnimatorController : Object
	{
		public extern AnimationClip[] animationClips
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		protected RuntimeAnimatorController()
		{
		}
	}
}
