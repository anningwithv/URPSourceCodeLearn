using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Modules/Animation/ScriptBindings/Animation.bindings.h"), NativeHeader("Modules/Animation/AnimatorInfo.h"), UsedByNativeCode]
	public struct AnimatorClipInfo
	{
		private int m_ClipInstanceID;

		private float m_Weight;

		public AnimationClip clip
		{
			get
			{
				return (this.m_ClipInstanceID != 0) ? AnimatorClipInfo.InstanceIDToAnimationClipPPtr(this.m_ClipInstanceID) : null;
			}
		}

		public float weight
		{
			get
			{
				return this.m_Weight;
			}
		}

		[FreeFunction("AnimationBindings::InstanceIDToAnimationClipPPtr")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimationClip InstanceIDToAnimationClipPPtr(int instanceID);
	}
}
