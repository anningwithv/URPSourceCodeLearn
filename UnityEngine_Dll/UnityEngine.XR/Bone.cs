using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	[NativeConditional("ENABLE_VR"), NativeHeader("XRScriptingClasses.h"), NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputDevices.h"), NativeHeader("Modules/XR/XRPrefix.h"), StaticAccessor("XRInputDevices::Get()", StaticAccessorType.Dot), RequiredByNativeCode]
	public struct Bone : IEquatable<Bone>
	{
		private ulong m_DeviceId;

		private uint m_FeatureIndex;

		internal ulong deviceId
		{
			get
			{
				return this.m_DeviceId;
			}
		}

		internal uint featureIndex
		{
			get
			{
				return this.m_FeatureIndex;
			}
		}

		public bool TryGetPosition(out Vector3 position)
		{
			return Bone.Bone_TryGetPosition(this, out position);
		}

		private static bool Bone_TryGetPosition(Bone bone, out Vector3 position)
		{
			return Bone.Bone_TryGetPosition_Injected(ref bone, out position);
		}

		public bool TryGetRotation(out Quaternion rotation)
		{
			return Bone.Bone_TryGetRotation(this, out rotation);
		}

		private static bool Bone_TryGetRotation(Bone bone, out Quaternion rotation)
		{
			return Bone.Bone_TryGetRotation_Injected(ref bone, out rotation);
		}

		public bool TryGetParentBone(out Bone parentBone)
		{
			return Bone.Bone_TryGetParentBone(this, out parentBone);
		}

		private static bool Bone_TryGetParentBone(Bone bone, out Bone parentBone)
		{
			return Bone.Bone_TryGetParentBone_Injected(ref bone, out parentBone);
		}

		public bool TryGetChildBones(List<Bone> childBones)
		{
			return Bone.Bone_TryGetChildBones(this, childBones);
		}

		private static bool Bone_TryGetChildBones(Bone bone, [NotNull("ArgumentNullException")] List<Bone> childBones)
		{
			return Bone.Bone_TryGetChildBones_Injected(ref bone, childBones);
		}

		public override bool Equals(object obj)
		{
			bool flag = !(obj is Bone);
			return !flag && this.Equals((Bone)obj);
		}

		public bool Equals(Bone other)
		{
			return this.deviceId == other.deviceId && this.featureIndex == other.featureIndex;
		}

		public override int GetHashCode()
		{
			return this.deviceId.GetHashCode() ^ this.featureIndex.GetHashCode() << 1;
		}

		public static bool operator ==(Bone a, Bone b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(Bone a, Bone b)
		{
			return !(a == b);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Bone_TryGetPosition_Injected(ref Bone bone, out Vector3 position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Bone_TryGetRotation_Injected(ref Bone bone, out Quaternion rotation);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Bone_TryGetParentBone_Injected(ref Bone bone, out Bone parentBone);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Bone_TryGetChildBones_Injected(ref Bone bone, List<Bone> childBones);
	}
}
