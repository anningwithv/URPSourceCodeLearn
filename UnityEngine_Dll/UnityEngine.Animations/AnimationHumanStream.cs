using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	[NativeHeader("Modules/Animation/Director/AnimationHumanStream.h"), NativeHeader("Modules/Animation/ScriptBindings/AnimationHumanStream.bindings.h"), MovedFrom("UnityEngine.Experimental.Animations"), RequiredByNativeCode]
	public struct AnimationHumanStream
	{
		private IntPtr stream;

		public bool isValid
		{
			get
			{
				return this.stream != IntPtr.Zero;
			}
		}

		public float humanScale
		{
			get
			{
				this.ThrowIfInvalid();
				return this.GetHumanScale();
			}
		}

		public float leftFootHeight
		{
			get
			{
				this.ThrowIfInvalid();
				return this.GetFootHeight(true);
			}
		}

		public float rightFootHeight
		{
			get
			{
				this.ThrowIfInvalid();
				return this.GetFootHeight(false);
			}
		}

		public Vector3 bodyLocalPosition
		{
			get
			{
				this.ThrowIfInvalid();
				return this.InternalGetBodyLocalPosition();
			}
			set
			{
				this.ThrowIfInvalid();
				this.InternalSetBodyLocalPosition(value);
			}
		}

		public Quaternion bodyLocalRotation
		{
			get
			{
				this.ThrowIfInvalid();
				return this.InternalGetBodyLocalRotation();
			}
			set
			{
				this.ThrowIfInvalid();
				this.InternalSetBodyLocalRotation(value);
			}
		}

		public Vector3 bodyPosition
		{
			get
			{
				this.ThrowIfInvalid();
				return this.InternalGetBodyPosition();
			}
			set
			{
				this.ThrowIfInvalid();
				this.InternalSetBodyPosition(value);
			}
		}

		public Quaternion bodyRotation
		{
			get
			{
				this.ThrowIfInvalid();
				return this.InternalGetBodyRotation();
			}
			set
			{
				this.ThrowIfInvalid();
				this.InternalSetBodyRotation(value);
			}
		}

		public Vector3 leftFootVelocity
		{
			get
			{
				this.ThrowIfInvalid();
				return this.GetLeftFootVelocity();
			}
		}

		public Vector3 rightFootVelocity
		{
			get
			{
				this.ThrowIfInvalid();
				return this.GetRightFootVelocity();
			}
		}

		private void ThrowIfInvalid()
		{
			bool flag = !this.isValid;
			if (flag)
			{
				throw new InvalidOperationException("The AnimationHumanStream is invalid.");
			}
		}

		public float GetMuscle(MuscleHandle muscle)
		{
			this.ThrowIfInvalid();
			return this.InternalGetMuscle(muscle);
		}

		public void SetMuscle(MuscleHandle muscle, float value)
		{
			this.ThrowIfInvalid();
			this.InternalSetMuscle(muscle, value);
		}

		public void ResetToStancePose()
		{
			this.ThrowIfInvalid();
			this.InternalResetToStancePose();
		}

		public Vector3 GetGoalPositionFromPose(AvatarIKGoal index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetGoalPositionFromPose(index);
		}

		public Quaternion GetGoalRotationFromPose(AvatarIKGoal index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetGoalRotationFromPose(index);
		}

		public Vector3 GetGoalLocalPosition(AvatarIKGoal index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetGoalLocalPosition(index);
		}

		public void SetGoalLocalPosition(AvatarIKGoal index, Vector3 pos)
		{
			this.ThrowIfInvalid();
			this.InternalSetGoalLocalPosition(index, pos);
		}

		public Quaternion GetGoalLocalRotation(AvatarIKGoal index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetGoalLocalRotation(index);
		}

		public void SetGoalLocalRotation(AvatarIKGoal index, Quaternion rot)
		{
			this.ThrowIfInvalid();
			this.InternalSetGoalLocalRotation(index, rot);
		}

		public Vector3 GetGoalPosition(AvatarIKGoal index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetGoalPosition(index);
		}

		public void SetGoalPosition(AvatarIKGoal index, Vector3 pos)
		{
			this.ThrowIfInvalid();
			this.InternalSetGoalPosition(index, pos);
		}

		public Quaternion GetGoalRotation(AvatarIKGoal index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetGoalRotation(index);
		}

		public void SetGoalRotation(AvatarIKGoal index, Quaternion rot)
		{
			this.ThrowIfInvalid();
			this.InternalSetGoalRotation(index, rot);
		}

		public void SetGoalWeightPosition(AvatarIKGoal index, float value)
		{
			this.ThrowIfInvalid();
			this.InternalSetGoalWeightPosition(index, value);
		}

		public void SetGoalWeightRotation(AvatarIKGoal index, float value)
		{
			this.ThrowIfInvalid();
			this.InternalSetGoalWeightRotation(index, value);
		}

		public float GetGoalWeightPosition(AvatarIKGoal index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetGoalWeightPosition(index);
		}

		public float GetGoalWeightRotation(AvatarIKGoal index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetGoalWeightRotation(index);
		}

		public Vector3 GetHintPosition(AvatarIKHint index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetHintPosition(index);
		}

		public void SetHintPosition(AvatarIKHint index, Vector3 pos)
		{
			this.ThrowIfInvalid();
			this.InternalSetHintPosition(index, pos);
		}

		public void SetHintWeightPosition(AvatarIKHint index, float value)
		{
			this.ThrowIfInvalid();
			this.InternalSetHintWeightPosition(index, value);
		}

		public float GetHintWeightPosition(AvatarIKHint index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetHintWeightPosition(index);
		}

		public void SetLookAtPosition(Vector3 lookAtPosition)
		{
			this.ThrowIfInvalid();
			this.InternalSetLookAtPosition(lookAtPosition);
		}

		public void SetLookAtClampWeight(float weight)
		{
			this.ThrowIfInvalid();
			this.InternalSetLookAtClampWeight(weight);
		}

		public void SetLookAtBodyWeight(float weight)
		{
			this.ThrowIfInvalid();
			this.InternalSetLookAtBodyWeight(weight);
		}

		public void SetLookAtHeadWeight(float weight)
		{
			this.ThrowIfInvalid();
			this.InternalSetLookAtHeadWeight(weight);
		}

		public void SetLookAtEyesWeight(float weight)
		{
			this.ThrowIfInvalid();
			this.InternalSetLookAtEyesWeight(weight);
		}

		public void SolveIK()
		{
			this.ThrowIfInvalid();
			this.InternalSolveIK();
		}

		[NativeMethod(IsThreadSafe = true)]
		private float GetHumanScale()
		{
			return AnimationHumanStream.GetHumanScale_Injected(ref this);
		}

		[NativeMethod(IsThreadSafe = true)]
		private float GetFootHeight(bool left)
		{
			return AnimationHumanStream.GetFootHeight_Injected(ref this, left);
		}

		[NativeMethod(Name = "ResetToStancePose", IsThreadSafe = true)]
		private void InternalResetToStancePose()
		{
			AnimationHumanStream.InternalResetToStancePose_Injected(ref this);
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::GetGoalPositionFromPose", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 InternalGetGoalPositionFromPose(AvatarIKGoal index)
		{
			Vector3 result;
			AnimationHumanStream.InternalGetGoalPositionFromPose_Injected(ref this, index, out result);
			return result;
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::GetGoalRotationFromPose", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion InternalGetGoalRotationFromPose(AvatarIKGoal index)
		{
			Quaternion result;
			AnimationHumanStream.InternalGetGoalRotationFromPose_Injected(ref this, index, out result);
			return result;
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::GetBodyLocalPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 InternalGetBodyLocalPosition()
		{
			Vector3 result;
			AnimationHumanStream.InternalGetBodyLocalPosition_Injected(ref this, out result);
			return result;
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::SetBodyLocalPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetBodyLocalPosition(Vector3 value)
		{
			AnimationHumanStream.InternalSetBodyLocalPosition_Injected(ref this, ref value);
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::GetBodyLocalRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion InternalGetBodyLocalRotation()
		{
			Quaternion result;
			AnimationHumanStream.InternalGetBodyLocalRotation_Injected(ref this, out result);
			return result;
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::SetBodyLocalRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetBodyLocalRotation(Quaternion value)
		{
			AnimationHumanStream.InternalSetBodyLocalRotation_Injected(ref this, ref value);
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::GetBodyPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 InternalGetBodyPosition()
		{
			Vector3 result;
			AnimationHumanStream.InternalGetBodyPosition_Injected(ref this, out result);
			return result;
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::SetBodyPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetBodyPosition(Vector3 value)
		{
			AnimationHumanStream.InternalSetBodyPosition_Injected(ref this, ref value);
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::GetBodyRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion InternalGetBodyRotation()
		{
			Quaternion result;
			AnimationHumanStream.InternalGetBodyRotation_Injected(ref this, out result);
			return result;
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::SetBodyRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetBodyRotation(Quaternion value)
		{
			AnimationHumanStream.InternalSetBodyRotation_Injected(ref this, ref value);
		}

		[NativeMethod(Name = "GetMuscle", IsThreadSafe = true)]
		private float InternalGetMuscle(MuscleHandle muscle)
		{
			return AnimationHumanStream.InternalGetMuscle_Injected(ref this, ref muscle);
		}

		[NativeMethod(Name = "SetMuscle", IsThreadSafe = true)]
		private void InternalSetMuscle(MuscleHandle muscle, float value)
		{
			AnimationHumanStream.InternalSetMuscle_Injected(ref this, ref muscle, value);
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::GetLeftFootVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetLeftFootVelocity()
		{
			Vector3 result;
			AnimationHumanStream.GetLeftFootVelocity_Injected(ref this, out result);
			return result;
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::GetRightFootVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetRightFootVelocity()
		{
			Vector3 result;
			AnimationHumanStream.GetRightFootVelocity_Injected(ref this, out result);
			return result;
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::GetGoalLocalPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 InternalGetGoalLocalPosition(AvatarIKGoal index)
		{
			Vector3 result;
			AnimationHumanStream.InternalGetGoalLocalPosition_Injected(ref this, index, out result);
			return result;
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::SetGoalLocalPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetGoalLocalPosition(AvatarIKGoal index, Vector3 pos)
		{
			AnimationHumanStream.InternalSetGoalLocalPosition_Injected(ref this, index, ref pos);
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::GetGoalLocalRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion InternalGetGoalLocalRotation(AvatarIKGoal index)
		{
			Quaternion result;
			AnimationHumanStream.InternalGetGoalLocalRotation_Injected(ref this, index, out result);
			return result;
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::SetGoalLocalRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetGoalLocalRotation(AvatarIKGoal index, Quaternion rot)
		{
			AnimationHumanStream.InternalSetGoalLocalRotation_Injected(ref this, index, ref rot);
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::GetGoalPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 InternalGetGoalPosition(AvatarIKGoal index)
		{
			Vector3 result;
			AnimationHumanStream.InternalGetGoalPosition_Injected(ref this, index, out result);
			return result;
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::SetGoalPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetGoalPosition(AvatarIKGoal index, Vector3 pos)
		{
			AnimationHumanStream.InternalSetGoalPosition_Injected(ref this, index, ref pos);
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::GetGoalRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion InternalGetGoalRotation(AvatarIKGoal index)
		{
			Quaternion result;
			AnimationHumanStream.InternalGetGoalRotation_Injected(ref this, index, out result);
			return result;
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::SetGoalRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetGoalRotation(AvatarIKGoal index, Quaternion rot)
		{
			AnimationHumanStream.InternalSetGoalRotation_Injected(ref this, index, ref rot);
		}

		[NativeMethod(Name = "SetGoalWeightPosition", IsThreadSafe = true)]
		private void InternalSetGoalWeightPosition(AvatarIKGoal index, float value)
		{
			AnimationHumanStream.InternalSetGoalWeightPosition_Injected(ref this, index, value);
		}

		[NativeMethod(Name = "SetGoalWeightRotation", IsThreadSafe = true)]
		private void InternalSetGoalWeightRotation(AvatarIKGoal index, float value)
		{
			AnimationHumanStream.InternalSetGoalWeightRotation_Injected(ref this, index, value);
		}

		[NativeMethod(Name = "GetGoalWeightPosition", IsThreadSafe = true)]
		private float InternalGetGoalWeightPosition(AvatarIKGoal index)
		{
			return AnimationHumanStream.InternalGetGoalWeightPosition_Injected(ref this, index);
		}

		[NativeMethod(Name = "GetGoalWeightRotation", IsThreadSafe = true)]
		private float InternalGetGoalWeightRotation(AvatarIKGoal index)
		{
			return AnimationHumanStream.InternalGetGoalWeightRotation_Injected(ref this, index);
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::GetHintPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 InternalGetHintPosition(AvatarIKHint index)
		{
			Vector3 result;
			AnimationHumanStream.InternalGetHintPosition_Injected(ref this, index, out result);
			return result;
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::SetHintPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetHintPosition(AvatarIKHint index, Vector3 pos)
		{
			AnimationHumanStream.InternalSetHintPosition_Injected(ref this, index, ref pos);
		}

		[NativeMethod(Name = "SetHintWeightPosition", IsThreadSafe = true)]
		private void InternalSetHintWeightPosition(AvatarIKHint index, float value)
		{
			AnimationHumanStream.InternalSetHintWeightPosition_Injected(ref this, index, value);
		}

		[NativeMethod(Name = "GetHintWeightPosition", IsThreadSafe = true)]
		private float InternalGetHintWeightPosition(AvatarIKHint index)
		{
			return AnimationHumanStream.InternalGetHintWeightPosition_Injected(ref this, index);
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::SetLookAtPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetLookAtPosition(Vector3 lookAtPosition)
		{
			AnimationHumanStream.InternalSetLookAtPosition_Injected(ref this, ref lookAtPosition);
		}

		[NativeMethod(Name = "SetLookAtClampWeight", IsThreadSafe = true)]
		private void InternalSetLookAtClampWeight(float weight)
		{
			AnimationHumanStream.InternalSetLookAtClampWeight_Injected(ref this, weight);
		}

		[NativeMethod(Name = "SetLookAtBodyWeight", IsThreadSafe = true)]
		private void InternalSetLookAtBodyWeight(float weight)
		{
			AnimationHumanStream.InternalSetLookAtBodyWeight_Injected(ref this, weight);
		}

		[NativeMethod(Name = "SetLookAtHeadWeight", IsThreadSafe = true)]
		private void InternalSetLookAtHeadWeight(float weight)
		{
			AnimationHumanStream.InternalSetLookAtHeadWeight_Injected(ref this, weight);
		}

		[NativeMethod(Name = "SetLookAtEyesWeight", IsThreadSafe = true)]
		private void InternalSetLookAtEyesWeight(float weight)
		{
			AnimationHumanStream.InternalSetLookAtEyesWeight_Injected(ref this, weight);
		}

		[NativeMethod(Name = "SolveIK", IsThreadSafe = true)]
		private void InternalSolveIK()
		{
			AnimationHumanStream.InternalSolveIK_Injected(ref this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetHumanScale_Injected(ref AnimationHumanStream _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetFootHeight_Injected(ref AnimationHumanStream _unity_self, bool left);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalResetToStancePose_Injected(ref AnimationHumanStream _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetGoalPositionFromPose_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetGoalRotationFromPose_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetBodyLocalPosition_Injected(ref AnimationHumanStream _unity_self, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetBodyLocalPosition_Injected(ref AnimationHumanStream _unity_self, ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetBodyLocalRotation_Injected(ref AnimationHumanStream _unity_self, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetBodyLocalRotation_Injected(ref AnimationHumanStream _unity_self, ref Quaternion value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetBodyPosition_Injected(ref AnimationHumanStream _unity_self, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetBodyPosition_Injected(ref AnimationHumanStream _unity_self, ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetBodyRotation_Injected(ref AnimationHumanStream _unity_self, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetBodyRotation_Injected(ref AnimationHumanStream _unity_self, ref Quaternion value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float InternalGetMuscle_Injected(ref AnimationHumanStream _unity_self, ref MuscleHandle muscle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetMuscle_Injected(ref AnimationHumanStream _unity_self, ref MuscleHandle muscle, float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLeftFootVelocity_Injected(ref AnimationHumanStream _unity_self, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRightFootVelocity_Injected(ref AnimationHumanStream _unity_self, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetGoalLocalPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetGoalLocalPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, ref Vector3 pos);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetGoalLocalRotation_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetGoalLocalRotation_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, ref Quaternion rot);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetGoalPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetGoalPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, ref Vector3 pos);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetGoalRotation_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetGoalRotation_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, ref Quaternion rot);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetGoalWeightPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetGoalWeightRotation_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float InternalGetGoalWeightPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float InternalGetGoalWeightRotation_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetHintPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKHint index, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetHintPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKHint index, ref Vector3 pos);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetHintWeightPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKHint index, float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float InternalGetHintWeightPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKHint index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetLookAtPosition_Injected(ref AnimationHumanStream _unity_self, ref Vector3 lookAtPosition);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetLookAtClampWeight_Injected(ref AnimationHumanStream _unity_self, float weight);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetLookAtBodyWeight_Injected(ref AnimationHumanStream _unity_self, float weight);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetLookAtHeadWeight_Injected(ref AnimationHumanStream _unity_self, float weight);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetLookAtEyesWeight_Injected(ref AnimationHumanStream _unity_self, float weight);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSolveIK_Injected(ref AnimationHumanStream _unity_self);
	}
}
