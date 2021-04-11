using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	[NativeHeader("Modules/Animation/MuscleHandle.h"), NativeHeader("Modules/Animation/Animator.h"), MovedFrom("UnityEngine.Experimental.Animations")]
	public struct MuscleHandle
	{
		public HumanPartDof humanPartDof
		{
			[IsReadOnly]
			get;
			private set;
		}

		public int dof
		{
			[IsReadOnly]
			get;
			private set;
		}

		public string name
		{
			get
			{
				return this.GetName();
			}
		}

		public static int muscleHandleCount
		{
			get
			{
				return MuscleHandle.GetMuscleHandleCount();
			}
		}

		public MuscleHandle(BodyDof bodyDof)
		{
			this.humanPartDof = HumanPartDof.Body;
			this.dof = (int)bodyDof;
		}

		public MuscleHandle(HeadDof headDof)
		{
			this.humanPartDof = HumanPartDof.Head;
			this.dof = (int)headDof;
		}

		public MuscleHandle(HumanPartDof partDof, LegDof legDof)
		{
			bool flag = partDof != HumanPartDof.LeftLeg && partDof != HumanPartDof.RightLeg;
			if (flag)
			{
				throw new InvalidOperationException("Invalid HumanPartDof for a leg, please use either HumanPartDof.LeftLeg or HumanPartDof.RightLeg.");
			}
			this.humanPartDof = partDof;
			this.dof = (int)legDof;
		}

		public MuscleHandle(HumanPartDof partDof, ArmDof armDof)
		{
			bool flag = partDof != HumanPartDof.LeftArm && partDof != HumanPartDof.RightArm;
			if (flag)
			{
				throw new InvalidOperationException("Invalid HumanPartDof for an arm, please use either HumanPartDof.LeftArm or HumanPartDof.RightArm.");
			}
			this.humanPartDof = partDof;
			this.dof = (int)armDof;
		}

		public MuscleHandle(HumanPartDof partDof, FingerDof fingerDof)
		{
			bool flag = partDof < HumanPartDof.LeftThumb || partDof > HumanPartDof.RightLittle;
			if (flag)
			{
				throw new InvalidOperationException("Invalid HumanPartDof for a finger.");
			}
			this.humanPartDof = partDof;
			this.dof = (int)fingerDof;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void GetMuscleHandles([NotNull("ArgumentNullException")] [Out] MuscleHandle[] muscleHandles);

		private string GetName()
		{
			return MuscleHandle.GetName_Injected(ref this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMuscleHandleCount();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetName_Injected(ref MuscleHandle _unity_self);
	}
}
