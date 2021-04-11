using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/Animation/HumanTrait.h")]
	public class HumanTrait
	{
		public static extern int MuscleCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string[] MuscleName
		{
			[NativeMethod("GetMuscleNames")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern int BoneCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string[] BoneName
		{
			[NativeMethod("MonoBoneNames")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern int RequiredBoneCount
		{
			[NativeMethod("RequiredBoneCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetBoneIndexFromMono(int humanId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetBoneIndexToMono(int boneIndex);

		public static int MuscleFromBone(int i, int dofIndex)
		{
			return HumanTrait.Internal_MuscleFromBone(HumanTrait.GetBoneIndexFromMono(i), dofIndex);
		}

		[NativeMethod("MuscleFromBone")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_MuscleFromBone(int i, int dofIndex);

		public static int BoneFromMuscle(int i)
		{
			return HumanTrait.GetBoneIndexToMono(HumanTrait.Internal_BoneFromMuscle(i));
		}

		[NativeMethod("BoneFromMuscle")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_BoneFromMuscle(int i);

		public static bool RequiredBone(int i)
		{
			return HumanTrait.Internal_RequiredBone(HumanTrait.GetBoneIndexFromMono(i));
		}

		[NativeMethod("RequiredBone")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_RequiredBone(int i);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetMuscleDefaultMin(int i);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetMuscleDefaultMax(int i);

		public static float GetBoneDefaultHierarchyMass(int i)
		{
			return HumanTrait.Internal_GetBoneHierarchyMass(HumanTrait.GetBoneIndexFromMono(i));
		}

		public static int GetParentBone(int i)
		{
			int num = HumanTrait.Internal_GetParent(HumanTrait.GetBoneIndexFromMono(i));
			return (num != -1) ? HumanTrait.GetBoneIndexToMono(num) : -1;
		}

		[NativeMethod("GetBoneHierarchyMass")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float Internal_GetBoneHierarchyMass(int i);

		[NativeMethod("GetParent")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetParent(int i);
	}
}
