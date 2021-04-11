using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[UsedByNativeCode]
	[Serializable]
	public struct BoneWeight1 : IEquatable<BoneWeight1>
	{
		[SerializeField]
		private float m_Weight;

		[SerializeField]
		private int m_BoneIndex;

		public float weight
		{
			get
			{
				return this.m_Weight;
			}
			set
			{
				this.m_Weight = value;
			}
		}

		public int boneIndex
		{
			get
			{
				return this.m_BoneIndex;
			}
			set
			{
				this.m_BoneIndex = value;
			}
		}

		public override bool Equals(object other)
		{
			return other is BoneWeight1 && this.Equals((BoneWeight1)other);
		}

		public bool Equals(BoneWeight1 other)
		{
			return this.boneIndex.Equals(other.boneIndex) && this.weight.Equals(other.weight);
		}

		public override int GetHashCode()
		{
			return this.boneIndex.GetHashCode() ^ this.weight.GetHashCode();
		}

		public static bool operator ==(BoneWeight1 lhs, BoneWeight1 rhs)
		{
			return lhs.boneIndex == rhs.boneIndex && lhs.weight == rhs.weight;
		}

		public static bool operator !=(BoneWeight1 lhs, BoneWeight1 rhs)
		{
			return !(lhs == rhs);
		}
	}
}
