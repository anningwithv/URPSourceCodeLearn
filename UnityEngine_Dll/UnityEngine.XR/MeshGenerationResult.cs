using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h"), RequiredByNativeCode]
	public struct MeshGenerationResult : IEquatable<MeshGenerationResult>
	{
		public MeshId MeshId
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<MeshId>k__BackingField;
			}
		}

		public Mesh Mesh
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<Mesh>k__BackingField;
			}
		}

		public MeshCollider MeshCollider
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<MeshCollider>k__BackingField;
			}
		}

		public MeshGenerationStatus Status
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<Status>k__BackingField;
			}
		}

		public MeshVertexAttributes Attributes
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<Attributes>k__BackingField;
			}
		}

		public override bool Equals(object obj)
		{
			bool flag = !(obj is MeshGenerationResult);
			return !flag && this.Equals((MeshGenerationResult)obj);
		}

		public bool Equals(MeshGenerationResult other)
		{
			return this.MeshId.Equals(other.MeshId) && this.Mesh.Equals(other.Mesh) && this.MeshCollider.Equals(other.MeshCollider) && this.Status.Equals(other.Status) && this.Attributes.Equals(other.Attributes);
		}

		public static bool operator ==(MeshGenerationResult lhs, MeshGenerationResult rhs)
		{
			return lhs.Equals(rhs);
		}

		public static bool operator !=(MeshGenerationResult lhs, MeshGenerationResult rhs)
		{
			return !lhs.Equals(rhs);
		}

		public override int GetHashCode()
		{
			return HashCodeHelper.Combine(HashCodeHelper.Combine(HashCodeHelper.Combine(HashCodeHelper.Combine(this.MeshId.GetHashCode(), this.Mesh.GetHashCode()), this.MeshCollider.GetHashCode()), this.Status.GetHashCode()), this.Attributes.GetHashCode());
		}
	}
}
