using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Modules/Animation/AnimatorInfo.h"), RequiredByNativeCode]
	public struct AnimatorTransitionInfo
	{
		[NativeName("fullPathHash")]
		private int m_FullPath;

		[NativeName("userNameHash")]
		private int m_UserName;

		[NativeName("nameHash")]
		private int m_Name;

		[NativeName("hasFixedDuration")]
		private bool m_HasFixedDuration;

		[NativeName("duration")]
		private float m_Duration;

		[NativeName("normalizedTime")]
		private float m_NormalizedTime;

		[NativeName("anyState")]
		private bool m_AnyState;

		[NativeName("transitionType")]
		private int m_TransitionType;

		public int fullPathHash
		{
			get
			{
				return this.m_FullPath;
			}
		}

		public int nameHash
		{
			get
			{
				return this.m_Name;
			}
		}

		public int userNameHash
		{
			get
			{
				return this.m_UserName;
			}
		}

		public DurationUnit durationUnit
		{
			get
			{
				return this.m_HasFixedDuration ? DurationUnit.Fixed : DurationUnit.Normalized;
			}
		}

		public float duration
		{
			get
			{
				return this.m_Duration;
			}
		}

		public float normalizedTime
		{
			get
			{
				return this.m_NormalizedTime;
			}
		}

		public bool anyState
		{
			get
			{
				return this.m_AnyState;
			}
		}

		internal bool entry
		{
			get
			{
				return (this.m_TransitionType & 2) != 0;
			}
		}

		internal bool exit
		{
			get
			{
				return (this.m_TransitionType & 4) != 0;
			}
		}

		public bool IsName(string name)
		{
			return Animator.StringToHash(name) == this.m_Name || Animator.StringToHash(name) == this.m_FullPath;
		}

		public bool IsUserName(string name)
		{
			return Animator.StringToHash(name) == this.m_UserName;
		}
	}
}
