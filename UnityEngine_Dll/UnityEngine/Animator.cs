using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Modules/Animation/ScriptBindings/Animator.bindings.h"), NativeHeader("Modules/Animation/ScriptBindings/AnimatorControllerParameter.bindings.h"), NativeHeader("Modules/Animation/Animator.h"), UsedByNativeCode]
	public class Animator : Behaviour
	{
		public extern bool isOptimizable
		{
			[NativeMethod("IsOptimizable")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool isHuman
		{
			[NativeMethod("IsHuman")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool hasRootMotion
		{
			[NativeMethod("HasRootMotion")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal extern bool isRootPositionOrRotationControlledByCurves
		{
			[NativeMethod("IsRootTranslationOrRotationControllerByCurves")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float humanScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool isInitialized
		{
			[NativeMethod("IsInitialized")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public Vector3 deltaPosition
		{
			get
			{
				Vector3 result;
				this.get_deltaPosition_Injected(out result);
				return result;
			}
		}

		public Quaternion deltaRotation
		{
			get
			{
				Quaternion result;
				this.get_deltaRotation_Injected(out result);
				return result;
			}
		}

		public Vector3 velocity
		{
			get
			{
				Vector3 result;
				this.get_velocity_Injected(out result);
				return result;
			}
		}

		public Vector3 angularVelocity
		{
			get
			{
				Vector3 result;
				this.get_angularVelocity_Injected(out result);
				return result;
			}
		}

		public Vector3 rootPosition
		{
			[NativeMethod("GetAvatarPosition")]
			get
			{
				Vector3 result;
				this.get_rootPosition_Injected(out result);
				return result;
			}
			[NativeMethod("SetAvatarPosition")]
			set
			{
				this.set_rootPosition_Injected(ref value);
			}
		}

		public Quaternion rootRotation
		{
			[NativeMethod("GetAvatarRotation")]
			get
			{
				Quaternion result;
				this.get_rootRotation_Injected(out result);
				return result;
			}
			[NativeMethod("SetAvatarRotation")]
			set
			{
				this.set_rootRotation_Injected(ref value);
			}
		}

		public extern bool applyRootMotion
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("Animator.linearVelocityBlending is no longer used and has been deprecated.")]
		public extern bool linearVelocityBlending
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("Animator.animatePhysics has been deprecated. Use Animator.updateMode instead.")]
		public bool animatePhysics
		{
			get
			{
				return this.updateMode == AnimatorUpdateMode.AnimatePhysics;
			}
			set
			{
				this.updateMode = (value ? AnimatorUpdateMode.AnimatePhysics : AnimatorUpdateMode.Normal);
			}
		}

		public extern AnimatorUpdateMode updateMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool hasTransformHierarchy
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal extern bool allowConstantClipSamplingOptimization
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float gravityWeight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public Vector3 bodyPosition
		{
			get
			{
				this.CheckIfInIKPass();
				return this.bodyPositionInternal;
			}
			set
			{
				this.CheckIfInIKPass();
				this.bodyPositionInternal = value;
			}
		}

		internal Vector3 bodyPositionInternal
		{
			[NativeMethod("GetBodyPosition")]
			get
			{
				Vector3 result;
				this.get_bodyPositionInternal_Injected(out result);
				return result;
			}
			[NativeMethod("SetBodyPosition")]
			set
			{
				this.set_bodyPositionInternal_Injected(ref value);
			}
		}

		public Quaternion bodyRotation
		{
			get
			{
				this.CheckIfInIKPass();
				return this.bodyRotationInternal;
			}
			set
			{
				this.CheckIfInIKPass();
				this.bodyRotationInternal = value;
			}
		}

		internal Quaternion bodyRotationInternal
		{
			[NativeMethod("GetBodyRotation")]
			get
			{
				Quaternion result;
				this.get_bodyRotationInternal_Injected(out result);
				return result;
			}
			[NativeMethod("SetBodyRotation")]
			set
			{
				this.set_bodyRotationInternal_Injected(ref value);
			}
		}

		public extern bool stabilizeFeet
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int layerCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern AnimatorControllerParameter[] parameters
		{
			[FreeFunction(Name = "AnimatorBindings::GetParameters", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int parameterCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float feetPivotActive
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float pivotWeight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public Vector3 pivotPosition
		{
			get
			{
				Vector3 result;
				this.get_pivotPosition_Injected(out result);
				return result;
			}
		}

		public extern bool isMatchingTarget
		{
			[NativeMethod("IsMatchingTarget")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float speed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 targetPosition
		{
			get
			{
				Vector3 result;
				this.get_targetPosition_Injected(out result);
				return result;
			}
		}

		public Quaternion targetRotation
		{
			get
			{
				Quaternion result;
				this.get_targetRotation_Injected(out result);
				return result;
			}
		}

		internal extern Transform avatarRoot
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern AnimatorCullingMode cullingMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float playbackTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public float recorderStartTime
		{
			get
			{
				return this.GetRecorderStartTime();
			}
			set
			{
			}
		}

		public float recorderStopTime
		{
			get
			{
				return this.GetRecorderStopTime();
			}
			set
			{
			}
		}

		public extern AnimatorRecorderMode recorderMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern RuntimeAnimatorController runtimeAnimatorController
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool hasBoundPlayables
		{
			[NativeMethod("HasBoundPlayables")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Avatar avatar
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public PlayableGraph playableGraph
		{
			get
			{
				PlayableGraph result = default(PlayableGraph);
				this.GetCurrentGraph(ref result);
				return result;
			}
		}

		public extern bool layersAffectMassCenter
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float leftFeetBottomHeight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float rightFeetBottomHeight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeConditional("UNITY_EDITOR")]
		internal extern bool supportsOnAnimatorMove
		{
			[NativeMethod("SupportsOnAnimatorMove")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool logWarnings
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool fireEvents
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool keepAnimatorControllerStateOnDisable
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("GetCurrentAnimationClipState is obsolete. Use GetCurrentAnimatorClipInfo instead (UnityUpgradable) -> GetCurrentAnimatorClipInfo(*)", true)]
		public AnimationInfo[] GetCurrentAnimationClipState(int layerIndex)
		{
			return null;
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("GetNextAnimationClipState is obsolete. Use GetNextAnimatorClipInfo instead (UnityUpgradable) -> GetNextAnimatorClipInfo(*)", true)]
		public AnimationInfo[] GetNextAnimationClipState(int layerIndex)
		{
			return null;
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Stop is obsolete. Use Animator.enabled = false instead", true)]
		public void Stop()
		{
		}

		public float GetFloat(string name)
		{
			return this.GetFloatString(name);
		}

		public float GetFloat(int id)
		{
			return this.GetFloatID(id);
		}

		public void SetFloat(string name, float value)
		{
			this.SetFloatString(name, value);
		}

		public void SetFloat(string name, float value, float dampTime, float deltaTime)
		{
			this.SetFloatStringDamp(name, value, dampTime, deltaTime);
		}

		public void SetFloat(int id, float value)
		{
			this.SetFloatID(id, value);
		}

		public void SetFloat(int id, float value, float dampTime, float deltaTime)
		{
			this.SetFloatIDDamp(id, value, dampTime, deltaTime);
		}

		public bool GetBool(string name)
		{
			return this.GetBoolString(name);
		}

		public bool GetBool(int id)
		{
			return this.GetBoolID(id);
		}

		public void SetBool(string name, bool value)
		{
			this.SetBoolString(name, value);
		}

		public void SetBool(int id, bool value)
		{
			this.SetBoolID(id, value);
		}

		public int GetInteger(string name)
		{
			return this.GetIntegerString(name);
		}

		public int GetInteger(int id)
		{
			return this.GetIntegerID(id);
		}

		public void SetInteger(string name, int value)
		{
			this.SetIntegerString(name, value);
		}

		public void SetInteger(int id, int value)
		{
			this.SetIntegerID(id, value);
		}

		public void SetTrigger(string name)
		{
			this.SetTriggerString(name);
		}

		public void SetTrigger(int id)
		{
			this.SetTriggerID(id);
		}

		public void ResetTrigger(string name)
		{
			this.ResetTriggerString(name);
		}

		public void ResetTrigger(int id)
		{
			this.ResetTriggerID(id);
		}

		public bool IsParameterControlledByCurve(string name)
		{
			return this.IsParameterControlledByCurveString(name);
		}

		public bool IsParameterControlledByCurve(int id)
		{
			return this.IsParameterControlledByCurveID(id);
		}

		public Vector3 GetIKPosition(AvatarIKGoal goal)
		{
			this.CheckIfInIKPass();
			return this.GetGoalPosition(goal);
		}

		private Vector3 GetGoalPosition(AvatarIKGoal goal)
		{
			Vector3 result;
			this.GetGoalPosition_Injected(goal, out result);
			return result;
		}

		public void SetIKPosition(AvatarIKGoal goal, Vector3 goalPosition)
		{
			this.CheckIfInIKPass();
			this.SetGoalPosition(goal, goalPosition);
		}

		private void SetGoalPosition(AvatarIKGoal goal, Vector3 goalPosition)
		{
			this.SetGoalPosition_Injected(goal, ref goalPosition);
		}

		public Quaternion GetIKRotation(AvatarIKGoal goal)
		{
			this.CheckIfInIKPass();
			return this.GetGoalRotation(goal);
		}

		private Quaternion GetGoalRotation(AvatarIKGoal goal)
		{
			Quaternion result;
			this.GetGoalRotation_Injected(goal, out result);
			return result;
		}

		public void SetIKRotation(AvatarIKGoal goal, Quaternion goalRotation)
		{
			this.CheckIfInIKPass();
			this.SetGoalRotation(goal, goalRotation);
		}

		private void SetGoalRotation(AvatarIKGoal goal, Quaternion goalRotation)
		{
			this.SetGoalRotation_Injected(goal, ref goalRotation);
		}

		public float GetIKPositionWeight(AvatarIKGoal goal)
		{
			this.CheckIfInIKPass();
			return this.GetGoalWeightPosition(goal);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetGoalWeightPosition(AvatarIKGoal goal);

		public void SetIKPositionWeight(AvatarIKGoal goal, float value)
		{
			this.CheckIfInIKPass();
			this.SetGoalWeightPosition(goal, value);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetGoalWeightPosition(AvatarIKGoal goal, float value);

		public float GetIKRotationWeight(AvatarIKGoal goal)
		{
			this.CheckIfInIKPass();
			return this.GetGoalWeightRotation(goal);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetGoalWeightRotation(AvatarIKGoal goal);

		public void SetIKRotationWeight(AvatarIKGoal goal, float value)
		{
			this.CheckIfInIKPass();
			this.SetGoalWeightRotation(goal, value);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetGoalWeightRotation(AvatarIKGoal goal, float value);

		public Vector3 GetIKHintPosition(AvatarIKHint hint)
		{
			this.CheckIfInIKPass();
			return this.GetHintPosition(hint);
		}

		private Vector3 GetHintPosition(AvatarIKHint hint)
		{
			Vector3 result;
			this.GetHintPosition_Injected(hint, out result);
			return result;
		}

		public void SetIKHintPosition(AvatarIKHint hint, Vector3 hintPosition)
		{
			this.CheckIfInIKPass();
			this.SetHintPosition(hint, hintPosition);
		}

		private void SetHintPosition(AvatarIKHint hint, Vector3 hintPosition)
		{
			this.SetHintPosition_Injected(hint, ref hintPosition);
		}

		public float GetIKHintPositionWeight(AvatarIKHint hint)
		{
			this.CheckIfInIKPass();
			return this.GetHintWeightPosition(hint);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetHintWeightPosition(AvatarIKHint hint);

		public void SetIKHintPositionWeight(AvatarIKHint hint, float value)
		{
			this.CheckIfInIKPass();
			this.SetHintWeightPosition(hint, value);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetHintWeightPosition(AvatarIKHint hint, float value);

		public void SetLookAtPosition(Vector3 lookAtPosition)
		{
			this.CheckIfInIKPass();
			this.SetLookAtPositionInternal(lookAtPosition);
		}

		[NativeMethod("SetLookAtPosition")]
		private void SetLookAtPositionInternal(Vector3 lookAtPosition)
		{
			this.SetLookAtPositionInternal_Injected(ref lookAtPosition);
		}

		public void SetLookAtWeight(float weight)
		{
			this.CheckIfInIKPass();
			this.SetLookAtWeightInternal(weight, 0f, 1f, 0f, 0.5f);
		}

		public void SetLookAtWeight(float weight, float bodyWeight)
		{
			this.CheckIfInIKPass();
			this.SetLookAtWeightInternal(weight, bodyWeight, 1f, 0f, 0.5f);
		}

		public void SetLookAtWeight(float weight, float bodyWeight, float headWeight)
		{
			this.CheckIfInIKPass();
			this.SetLookAtWeightInternal(weight, bodyWeight, headWeight, 0f, 0.5f);
		}

		public void SetLookAtWeight(float weight, float bodyWeight, float headWeight, float eyesWeight)
		{
			this.CheckIfInIKPass();
			this.SetLookAtWeightInternal(weight, bodyWeight, headWeight, eyesWeight, 0.5f);
		}

		public void SetLookAtWeight(float weight, [UnityEngine.Internal.DefaultValue("0.0f")] float bodyWeight, [UnityEngine.Internal.DefaultValue("1.0f")] float headWeight, [UnityEngine.Internal.DefaultValue("0.0f")] float eyesWeight, [UnityEngine.Internal.DefaultValue("0.5f")] float clampWeight)
		{
			this.CheckIfInIKPass();
			this.SetLookAtWeightInternal(weight, bodyWeight, headWeight, eyesWeight, clampWeight);
		}

		[NativeMethod("SetLookAtWeight")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetLookAtWeightInternal(float weight, float bodyWeight, float headWeight, float eyesWeight, float clampWeight);

		public void SetBoneLocalRotation(HumanBodyBones humanBoneId, Quaternion rotation)
		{
			this.CheckIfInIKPass();
			this.SetBoneLocalRotationInternal(HumanTrait.GetBoneIndexFromMono((int)humanBoneId), rotation);
		}

		[NativeMethod("SetBoneLocalRotation")]
		private void SetBoneLocalRotationInternal(int humanBoneId, Quaternion rotation)
		{
			this.SetBoneLocalRotationInternal_Injected(humanBoneId, ref rotation);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern ScriptableObject GetBehaviour([NotNull("ArgumentNullException")] Type type);

		public T GetBehaviour<T>() where T : StateMachineBehaviour
		{
			return this.GetBehaviour(typeof(T)) as T;
		}

		private static T[] ConvertStateMachineBehaviour<T>(ScriptableObject[] rawObjects) where T : StateMachineBehaviour
		{
			bool flag = rawObjects == null;
			T[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				T[] array = new T[rawObjects.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (T)((object)rawObjects[i]);
				}
				result = array;
			}
			return result;
		}

		public T[] GetBehaviours<T>() where T : StateMachineBehaviour
		{
			return Animator.ConvertStateMachineBehaviour<T>(this.InternalGetBehaviours(typeof(T)));
		}

		[FreeFunction(Name = "AnimatorBindings::InternalGetBehaviours", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern ScriptableObject[] InternalGetBehaviours([NotNull("ArgumentNullException")] Type type);

		public StateMachineBehaviour[] GetBehaviours(int fullPathHash, int layerIndex)
		{
			return this.InternalGetBehavioursByKey(fullPathHash, layerIndex, typeof(StateMachineBehaviour)) as StateMachineBehaviour[];
		}

		[FreeFunction(Name = "AnimatorBindings::InternalGetBehavioursByKey", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern ScriptableObject[] InternalGetBehavioursByKey(int fullPathHash, int layerIndex, [NotNull("ArgumentNullException")] Type type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetLayerName(int layerIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetLayerIndex(string layerName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetLayerWeight(int layerIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetLayerWeight(int layerIndex, float weight);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetAnimatorStateInfo(int layerIndex, StateInfoIndex stateInfoIndex, out AnimatorStateInfo info);

		public AnimatorStateInfo GetCurrentAnimatorStateInfo(int layerIndex)
		{
			AnimatorStateInfo result;
			this.GetAnimatorStateInfo(layerIndex, StateInfoIndex.CurrentState, out result);
			return result;
		}

		public AnimatorStateInfo GetNextAnimatorStateInfo(int layerIndex)
		{
			AnimatorStateInfo result;
			this.GetAnimatorStateInfo(layerIndex, StateInfoIndex.NextState, out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetAnimatorTransitionInfo(int layerIndex, out AnimatorTransitionInfo info);

		public AnimatorTransitionInfo GetAnimatorTransitionInfo(int layerIndex)
		{
			AnimatorTransitionInfo result;
			this.GetAnimatorTransitionInfo(layerIndex, out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetAnimatorClipInfoCount(int layerIndex, bool current);

		public int GetCurrentAnimatorClipInfoCount(int layerIndex)
		{
			return this.GetAnimatorClipInfoCount(layerIndex, true);
		}

		public int GetNextAnimatorClipInfoCount(int layerIndex)
		{
			return this.GetAnimatorClipInfoCount(layerIndex, false);
		}

		[FreeFunction(Name = "AnimatorBindings::GetCurrentAnimatorClipInfo", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AnimatorClipInfo[] GetCurrentAnimatorClipInfo(int layerIndex);

		[FreeFunction(Name = "AnimatorBindings::GetNextAnimatorClipInfo", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AnimatorClipInfo[] GetNextAnimatorClipInfo(int layerIndex);

		public void GetCurrentAnimatorClipInfo(int layerIndex, List<AnimatorClipInfo> clips)
		{
			bool flag = clips == null;
			if (flag)
			{
				throw new ArgumentNullException("clips");
			}
			this.GetAnimatorClipInfoInternal(layerIndex, true, clips);
		}

		[FreeFunction(Name = "AnimatorBindings::GetAnimatorClipInfoInternal", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetAnimatorClipInfoInternal(int layerIndex, bool isCurrent, object clips);

		public void GetNextAnimatorClipInfo(int layerIndex, List<AnimatorClipInfo> clips)
		{
			bool flag = clips == null;
			if (flag)
			{
				throw new ArgumentNullException("clips");
			}
			this.GetAnimatorClipInfoInternal(layerIndex, false, clips);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsInTransition(int layerIndex);

		public AnimatorControllerParameter GetParameter(int index)
		{
			AnimatorControllerParameter[] parameters = this.parameters;
			bool flag = index < 0 || index >= this.parameters.Length;
			if (flag)
			{
				throw new IndexOutOfRangeException("Index must be between 0 and " + this.parameters.Length.ToString());
			}
			return parameters[index];
		}

		private void MatchTarget(Vector3 matchPosition, Quaternion matchRotation, int targetBodyPart, MatchTargetWeightMask weightMask, float startNormalizedTime, float targetNormalizedTime, bool completeMatch)
		{
			this.MatchTarget_Injected(ref matchPosition, ref matchRotation, targetBodyPart, ref weightMask, startNormalizedTime, targetNormalizedTime, completeMatch);
		}

		public void MatchTarget(Vector3 matchPosition, Quaternion matchRotation, AvatarTarget targetBodyPart, MatchTargetWeightMask weightMask, float startNormalizedTime)
		{
			this.MatchTarget(matchPosition, matchRotation, (int)targetBodyPart, weightMask, startNormalizedTime, 1f, true);
		}

		public void MatchTarget(Vector3 matchPosition, Quaternion matchRotation, AvatarTarget targetBodyPart, MatchTargetWeightMask weightMask, float startNormalizedTime, [UnityEngine.Internal.DefaultValue("1")] float targetNormalizedTime)
		{
			this.MatchTarget(matchPosition, matchRotation, (int)targetBodyPart, weightMask, startNormalizedTime, targetNormalizedTime, true);
		}

		public void MatchTarget(Vector3 matchPosition, Quaternion matchRotation, AvatarTarget targetBodyPart, MatchTargetWeightMask weightMask, float startNormalizedTime, [UnityEngine.Internal.DefaultValue("1")] float targetNormalizedTime, [UnityEngine.Internal.DefaultValue("true")] bool completeMatch)
		{
			this.MatchTarget(matchPosition, matchRotation, (int)targetBodyPart, weightMask, startNormalizedTime, targetNormalizedTime, completeMatch);
		}

		public void InterruptMatchTarget()
		{
			this.InterruptMatchTarget(true);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InterruptMatchTarget([UnityEngine.Internal.DefaultValue("true")] bool completeMatch);

		[Obsolete("ForceStateNormalizedTime is deprecated. Please use Play or CrossFade instead.")]
		public void ForceStateNormalizedTime(float normalizedTime)
		{
			this.Play(0, 0, normalizedTime);
		}

		public void CrossFadeInFixedTime(string stateName, float fixedTransitionDuration)
		{
			float normalizedTransitionTime = 0f;
			float fixedTimeOffset = 0f;
			int layer = -1;
			this.CrossFadeInFixedTime(Animator.StringToHash(stateName), fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		public void CrossFadeInFixedTime(string stateName, float fixedTransitionDuration, int layer)
		{
			float normalizedTransitionTime = 0f;
			float fixedTimeOffset = 0f;
			this.CrossFadeInFixedTime(Animator.StringToHash(stateName), fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		public void CrossFadeInFixedTime(string stateName, float fixedTransitionDuration, int layer, float fixedTimeOffset)
		{
			float normalizedTransitionTime = 0f;
			this.CrossFadeInFixedTime(Animator.StringToHash(stateName), fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		public void CrossFadeInFixedTime(string stateName, float fixedTransitionDuration, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("0.0f")] float fixedTimeOffset, [UnityEngine.Internal.DefaultValue("0.0f")] float normalizedTransitionTime)
		{
			this.CrossFadeInFixedTime(Animator.StringToHash(stateName), fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		public void CrossFadeInFixedTime(int stateHashName, float fixedTransitionDuration, int layer, float fixedTimeOffset)
		{
			float normalizedTransitionTime = 0f;
			this.CrossFadeInFixedTime(stateHashName, fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		public void CrossFadeInFixedTime(int stateHashName, float fixedTransitionDuration, int layer)
		{
			float normalizedTransitionTime = 0f;
			float fixedTimeOffset = 0f;
			this.CrossFadeInFixedTime(stateHashName, fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		public void CrossFadeInFixedTime(int stateHashName, float fixedTransitionDuration)
		{
			float normalizedTransitionTime = 0f;
			float fixedTimeOffset = 0f;
			int layer = -1;
			this.CrossFadeInFixedTime(stateHashName, fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		[FreeFunction(Name = "AnimatorBindings::CrossFadeInFixedTime", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CrossFadeInFixedTime(int stateHashName, float fixedTransitionDuration, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("0.0f")] float fixedTimeOffset, [UnityEngine.Internal.DefaultValue("0.0f")] float normalizedTransitionTime);

		[FreeFunction(Name = "AnimatorBindings::WriteDefaultValues", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void WriteDefaultValues();

		public void CrossFade(string stateName, float normalizedTransitionDuration, int layer, float normalizedTimeOffset)
		{
			float normalizedTransitionTime = 0f;
			this.CrossFade(stateName, normalizedTransitionDuration, layer, normalizedTimeOffset, normalizedTransitionTime);
		}

		public void CrossFade(string stateName, float normalizedTransitionDuration, int layer)
		{
			float normalizedTransitionTime = 0f;
			float normalizedTimeOffset = float.NegativeInfinity;
			this.CrossFade(stateName, normalizedTransitionDuration, layer, normalizedTimeOffset, normalizedTransitionTime);
		}

		public void CrossFade(string stateName, float normalizedTransitionDuration)
		{
			float normalizedTransitionTime = 0f;
			float normalizedTimeOffset = float.NegativeInfinity;
			int layer = -1;
			this.CrossFade(stateName, normalizedTransitionDuration, layer, normalizedTimeOffset, normalizedTransitionTime);
		}

		public void CrossFade(string stateName, float normalizedTransitionDuration, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float normalizedTimeOffset, [UnityEngine.Internal.DefaultValue("0.0f")] float normalizedTransitionTime)
		{
			this.CrossFade(Animator.StringToHash(stateName), normalizedTransitionDuration, layer, normalizedTimeOffset, normalizedTransitionTime);
		}

		[FreeFunction(Name = "AnimatorBindings::CrossFade", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CrossFade(int stateHashName, float normalizedTransitionDuration, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("0.0f")] float normalizedTimeOffset, [UnityEngine.Internal.DefaultValue("0.0f")] float normalizedTransitionTime);

		public void CrossFade(int stateHashName, float normalizedTransitionDuration, int layer, float normalizedTimeOffset)
		{
			float normalizedTransitionTime = 0f;
			this.CrossFade(stateHashName, normalizedTransitionDuration, layer, normalizedTimeOffset, normalizedTransitionTime);
		}

		public void CrossFade(int stateHashName, float normalizedTransitionDuration, int layer)
		{
			float normalizedTransitionTime = 0f;
			float normalizedTimeOffset = float.NegativeInfinity;
			this.CrossFade(stateHashName, normalizedTransitionDuration, layer, normalizedTimeOffset, normalizedTransitionTime);
		}

		public void CrossFade(int stateHashName, float normalizedTransitionDuration)
		{
			float normalizedTransitionTime = 0f;
			float normalizedTimeOffset = float.NegativeInfinity;
			int layer = -1;
			this.CrossFade(stateHashName, normalizedTransitionDuration, layer, normalizedTimeOffset, normalizedTransitionTime);
		}

		public void PlayInFixedTime(string stateName, int layer)
		{
			float fixedTime = float.NegativeInfinity;
			this.PlayInFixedTime(stateName, layer, fixedTime);
		}

		public void PlayInFixedTime(string stateName)
		{
			float fixedTime = float.NegativeInfinity;
			int layer = -1;
			this.PlayInFixedTime(stateName, layer, fixedTime);
		}

		public void PlayInFixedTime(string stateName, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float fixedTime)
		{
			this.PlayInFixedTime(Animator.StringToHash(stateName), layer, fixedTime);
		}

		[FreeFunction(Name = "AnimatorBindings::PlayInFixedTime", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void PlayInFixedTime(int stateNameHash, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float fixedTime);

		public void PlayInFixedTime(int stateNameHash, int layer)
		{
			float fixedTime = float.NegativeInfinity;
			this.PlayInFixedTime(stateNameHash, layer, fixedTime);
		}

		public void PlayInFixedTime(int stateNameHash)
		{
			float fixedTime = float.NegativeInfinity;
			int layer = -1;
			this.PlayInFixedTime(stateNameHash, layer, fixedTime);
		}

		public void Play(string stateName, int layer)
		{
			float normalizedTime = float.NegativeInfinity;
			this.Play(stateName, layer, normalizedTime);
		}

		public void Play(string stateName)
		{
			float normalizedTime = float.NegativeInfinity;
			int layer = -1;
			this.Play(stateName, layer, normalizedTime);
		}

		public void Play(string stateName, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float normalizedTime)
		{
			this.Play(Animator.StringToHash(stateName), layer, normalizedTime);
		}

		[FreeFunction(Name = "AnimatorBindings::Play", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Play(int stateNameHash, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float normalizedTime);

		public void Play(int stateNameHash, int layer)
		{
			float normalizedTime = float.NegativeInfinity;
			this.Play(stateNameHash, layer, normalizedTime);
		}

		public void Play(int stateNameHash)
		{
			float normalizedTime = float.NegativeInfinity;
			int layer = -1;
			this.Play(stateNameHash, layer, normalizedTime);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTarget(AvatarTarget targetIndex, float targetNormalizedTime);

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use mask and layers to control subset of transfroms in a skeleton.", true)]
		public bool IsControlled(Transform transform)
		{
			return false;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsBoneTransform(Transform transform);

		public Transform GetBoneTransform(HumanBodyBones humanBoneId)
		{
			bool flag = humanBoneId < HumanBodyBones.Hips || humanBoneId >= HumanBodyBones.LastBone;
			if (flag)
			{
				throw new IndexOutOfRangeException("humanBoneId must be between 0 and " + HumanBodyBones.LastBone.ToString());
			}
			return this.GetBoneTransformInternal(HumanTrait.GetBoneIndexFromMono((int)humanBoneId));
		}

		[NativeMethod("GetBoneTransform")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Transform GetBoneTransformInternal(int humanBoneId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StartPlayback();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopPlayback();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StartRecording(int frameCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopRecording();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetRecorderStartTime();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetRecorderStopTime();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void ClearInternalControllerPlayable();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasState(int layerIndex, int stateID);

		[NativeMethod(Name = "ScriptingStringToCRC32", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int StringToHash(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string GetStats();

		[FreeFunction(Name = "AnimatorBindings::GetCurrentGraph", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetCurrentGraph(ref PlayableGraph graph);

		private void CheckIfInIKPass()
		{
			bool flag = this.logWarnings && !this.IsInIKPass();
			if (flag)
			{
				Debug.LogWarning("Setting and getting Body Position/Rotation, IK Goals, Lookat and BoneLocalRotation should only be done in OnAnimatorIK or OnStateIK");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsInIKPass();

		[FreeFunction(Name = "AnimatorBindings::SetFloatString", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatString(string name, float value);

		[FreeFunction(Name = "AnimatorBindings::SetFloatID", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatID(int id, float value);

		[FreeFunction(Name = "AnimatorBindings::GetFloatString", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetFloatString(string name);

		[FreeFunction(Name = "AnimatorBindings::GetFloatID", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetFloatID(int id);

		[FreeFunction(Name = "AnimatorBindings::SetBoolString", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBoolString(string name, bool value);

		[FreeFunction(Name = "AnimatorBindings::SetBoolID", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBoolID(int id, bool value);

		[FreeFunction(Name = "AnimatorBindings::GetBoolString", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetBoolString(string name);

		[FreeFunction(Name = "AnimatorBindings::GetBoolID", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetBoolID(int id);

		[FreeFunction(Name = "AnimatorBindings::SetIntegerString", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetIntegerString(string name, int value);

		[FreeFunction(Name = "AnimatorBindings::SetIntegerID", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetIntegerID(int id, int value);

		[FreeFunction(Name = "AnimatorBindings::GetIntegerString", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetIntegerString(string name);

		[FreeFunction(Name = "AnimatorBindings::GetIntegerID", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetIntegerID(int id);

		[FreeFunction(Name = "AnimatorBindings::SetTriggerString", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTriggerString(string name);

		[FreeFunction(Name = "AnimatorBindings::SetTriggerID", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTriggerID(int id);

		[FreeFunction(Name = "AnimatorBindings::ResetTriggerString", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ResetTriggerString(string name);

		[FreeFunction(Name = "AnimatorBindings::ResetTriggerID", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ResetTriggerID(int id);

		[FreeFunction(Name = "AnimatorBindings::IsParameterControlledByCurveString", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsParameterControlledByCurveString(string name);

		[FreeFunction(Name = "AnimatorBindings::IsParameterControlledByCurveID", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsParameterControlledByCurveID(int id);

		[FreeFunction(Name = "AnimatorBindings::SetFloatStringDamp", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatStringDamp(string name, float value, float dampTime, float deltaTime);

		[FreeFunction(Name = "AnimatorBindings::SetFloatIDDamp", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatIDDamp(int id, float value, float dampTime, float deltaTime);

		[NativeConditional("UNITY_EDITOR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void OnUpdateModeChanged();

		[NativeConditional("UNITY_EDITOR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void OnCullingModeChanged();

		[NativeConditional("UNITY_EDITOR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void WriteDefaultPose();

		[NativeMethod("UpdateWithDelta")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Update(float deltaTime);

		public void Rebind()
		{
			this.Rebind(true);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Rebind(bool writeDefaultValues);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ApplyBuiltinRootMotion();

		[NativeConditional("UNITY_EDITOR")]
		internal void EvaluateController()
		{
			this.EvaluateController(0f);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void EvaluateController(float deltaTime);

		[NativeConditional("UNITY_EDITOR")]
		internal string GetCurrentStateName(int layerIndex)
		{
			return this.GetAnimatorStateName(layerIndex, true);
		}

		[NativeConditional("UNITY_EDITOR")]
		internal string GetNextStateName(int layerIndex)
		{
			return this.GetAnimatorStateName(layerIndex, false);
		}

		[NativeConditional("UNITY_EDITOR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string GetAnimatorStateName(int layerIndex, bool current);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string ResolveHash(int hash);

		[Obsolete("GetVector is deprecated.")]
		public Vector3 GetVector(string name)
		{
			return Vector3.zero;
		}

		[Obsolete("GetVector is deprecated.")]
		public Vector3 GetVector(int id)
		{
			return Vector3.zero;
		}

		[Obsolete("SetVector is deprecated.")]
		public void SetVector(string name, Vector3 value)
		{
		}

		[Obsolete("SetVector is deprecated.")]
		public void SetVector(int id, Vector3 value)
		{
		}

		[Obsolete("GetQuaternion is deprecated.")]
		public Quaternion GetQuaternion(string name)
		{
			return Quaternion.identity;
		}

		[Obsolete("GetQuaternion is deprecated.")]
		public Quaternion GetQuaternion(int id)
		{
			return Quaternion.identity;
		}

		[Obsolete("SetQuaternion is deprecated.")]
		public void SetQuaternion(string name, Quaternion value)
		{
		}

		[Obsolete("SetQuaternion is deprecated.")]
		public void SetQuaternion(int id, Quaternion value)
		{
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_deltaPosition_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_deltaRotation_Injected(out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_velocity_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_angularVelocity_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_rootPosition_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_rootPosition_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_rootRotation_Injected(out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_rootRotation_Injected(ref Quaternion value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_bodyPositionInternal_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_bodyPositionInternal_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_bodyRotationInternal_Injected(out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_bodyRotationInternal_Injected(ref Quaternion value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetGoalPosition_Injected(AvatarIKGoal goal, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetGoalPosition_Injected(AvatarIKGoal goal, ref Vector3 goalPosition);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetGoalRotation_Injected(AvatarIKGoal goal, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetGoalRotation_Injected(AvatarIKGoal goal, ref Quaternion goalRotation);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetHintPosition_Injected(AvatarIKHint hint, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetHintPosition_Injected(AvatarIKHint hint, ref Vector3 hintPosition);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetLookAtPositionInternal_Injected(ref Vector3 lookAtPosition);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBoneLocalRotationInternal_Injected(int humanBoneId, ref Quaternion rotation);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_pivotPosition_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void MatchTarget_Injected(ref Vector3 matchPosition, ref Quaternion matchRotation, int targetBodyPart, ref MatchTargetWeightMask weightMask, float startNormalizedTime, float targetNormalizedTime, bool completeMatch);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_targetPosition_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_targetRotation_Injected(out Quaternion ret);
	}
}
