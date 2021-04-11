using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[UsedByNativeCode]
	[Serializable]
	public struct BoneWeight : IEquatable<BoneWeight>
	{
		[SerializeField]
		private float m_Weight0;

		[SerializeField]
		private float m_Weight1;

		[SerializeField]
		private float m_Weight2;

		[SerializeField]
		private float m_Weight3;

		[SerializeField]
		private int m_BoneIndex0;

		[SerializeField]
		private int m_BoneIndex1;

		[SerializeField]
		private int m_BoneIndex2;

		[SerializeField]
		private int m_BoneIndex3;

		public float weight0
		{
			get
			{
				return this.m_Weight0;
			}
			set
			{
				this.m_Weight0 = value;
			}
		}

		public float weight1
		{
			get
			{
				return this.m_Weight1;
			}
			set
			{
				this.m_Weight1 = value;
			}
		}

		public float weight2
		{
			get
			{
				return this.m_Weight2;
			}
			set
			{
				this.m_Weight2 = value;
			}
		}

		public float weight3
		{
			get
			{
				return this.m_Weight3;
			}
			set
			{
				this.m_Weight3 = value;
			}
		}

		public int boneIndex0
		{
			get
			{
				return this.m_BoneIndex0;
			}
			set
			{
				this.m_BoneIndex0 = value;
			}
		}

		public int boneIndex1
		{
			get
			{
				return this.m_BoneIndex1;
			}
			set
			{
				this.m_BoneIndex1 = value;
			}
		}

		public int boneIndex2
		{
			get
			{
				return this.m_BoneIndex2;
			}
			set
			{
				this.m_BoneIndex2 = value;
			}
		}

		public int boneIndex3
		{
			get
			{
				return this.m_BoneIndex3;
			}
			set
			{
				this.m_BoneIndex3 = value;
			}
		}

		public override int GetHashCode()
		{
			return this.boneIndex0.GetHashCode() ^ this.boneIndex1.GetHashCode() << 2 ^ this.boneIndex2.GetHashCode() >> 2 ^ this.boneIndex3.GetHashCode() >> 1 ^ this.weight0.GetHashCode() << 5 ^ this.weight1.GetHashCode() << 4 ^ this.weight2.GetHashCode() >> 4 ^ this.weight3.GetHashCode() >> 3;
		}

		public override bool Equals(object other)
		{
			return other is BoneWeight && this.Equals((BoneWeight)other);
		}

		public bool Equals(BoneWeight other)
		{
			return this.boneIndex0.Equals(other.boneIndex0) && this.boneIndex1.Equals(other.boneIndex1) && this.boneIndex2.Equals(other.boneIndex2) && this.boneIndex3.Equals(other.boneIndex3) && new Vector4(this.weight0, this.weight1, this.weight2, this.weight3).Equals(new Vector4(other.weight0, other.weight1, other.weight2, other.weight3));
		}

		public static bool operator ==(BoneWeight lhs, BoneWeight rhs)
		{
			return lhs.boneIndex0 == rhs.boneIndex0 && lhs.boneIndex1 == rhs.boneIndex1 && lhs.boneIndex2 == rhs.boneIndex2 && lhs.boneIndex3 == rhs.boneIndex3 && new Vector4(lhs.weight0, lhs.weight1, lhs.weight2, lhs.weight3) == new Vector4(rhs.weight0, rhs.weight1, rhs.weight2, rhs.weight3);
		}

		public static bool operator !=(BoneWeight lhs, BoneWeight rhs)
		{
			return !(lhs == rhs);
		}
	}
}
