using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h"), UsedByNativeCode]
	public struct MeshId : IEquatable<MeshId>
	{
		private static MeshId s_InvalidId = default(MeshId);

		private ulong m_SubId1;

		private ulong m_SubId2;

		public static MeshId InvalidId
		{
			get
			{
				return MeshId.s_InvalidId;
			}
		}

		public override string ToString()
		{
			return string.Format("{0}-{1}", this.m_SubId1.ToString("X16"), this.m_SubId2.ToString("X16"));
		}

		public override int GetHashCode()
		{
			return this.m_SubId1.GetHashCode() ^ this.m_SubId2.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return obj is MeshId && this.Equals((MeshId)obj);
		}

		public bool Equals(MeshId other)
		{
			return this.m_SubId1 == other.m_SubId1 && this.m_SubId2 == other.m_SubId2;
		}

		public static bool operator ==(MeshId id1, MeshId id2)
		{
			return id1.m_SubId1 == id2.m_SubId1 && id1.m_SubId2 == id2.m_SubId2;
		}

		public static bool operator !=(MeshId id1, MeshId id2)
		{
			return id1.m_SubId1 != id2.m_SubId1 || id1.m_SubId2 != id2.m_SubId2;
		}
	}
}
