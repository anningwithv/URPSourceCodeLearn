using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h"), UsedByNativeCode]
	public struct MeshInfo : IEquatable<MeshInfo>
	{
		public MeshId MeshId
		{
			[IsReadOnly]
			get;
			set;
		}

		public MeshChangeState ChangeState
		{
			[IsReadOnly]
			get;
			set;
		}

		public int PriorityHint
		{
			[IsReadOnly]
			get;
			set;
		}

		public override bool Equals(object obj)
		{
			bool flag = !(obj is MeshInfo);
			return !flag && this.Equals((MeshInfo)obj);
		}

		public bool Equals(MeshInfo other)
		{
			return this.MeshId.Equals(other.MeshId) && this.ChangeState.Equals(other.ChangeState) && this.PriorityHint.Equals(other.PriorityHint);
		}

		public static bool operator ==(MeshInfo lhs, MeshInfo rhs)
		{
			return lhs.Equals(rhs);
		}

		public static bool operator !=(MeshInfo lhs, MeshInfo rhs)
		{
			return !lhs.Equals(rhs);
		}

		public override int GetHashCode()
		{
			return HashCodeHelper.Combine(HashCodeHelper.Combine(this.MeshId.GetHashCode(), this.ChangeState.GetHashCode()), this.PriorityHint);
		}
	}
}
