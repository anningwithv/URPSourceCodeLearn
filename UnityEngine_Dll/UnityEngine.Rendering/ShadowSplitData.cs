using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	[UsedByNativeCode]
	public struct ShadowSplitData : IEquatable<ShadowSplitData>
	{
		[CompilerGenerated, UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 160)]
		public struct <m_CullingPlanes>e__FixedBuffer
		{
			public byte FixedElementField;
		}

		private const int k_MaximumCullingPlaneCount = 10;

		public static readonly int maximumCullingPlaneCount = 10;

		private int m_CullingPlaneCount;

		[FixedBuffer(typeof(byte), 160)]
		internal ShadowSplitData.<m_CullingPlanes>e__FixedBuffer m_CullingPlanes;

		private Vector4 m_CullingSphere;

		private float m_ShadowCascadeBlendCullingFactor;

		private float m_CullingNearPlane;

		public int cullingPlaneCount
		{
			get
			{
				return this.m_CullingPlaneCount;
			}
			set
			{
				bool flag = value < 0 || value > 10;
				if (flag)
				{
					throw new ArgumentException(string.Format("Value should range from {0} to ShadowSplitData.maximumCullingPlaneCount ({1}), but was {2}.", 0, 10, value));
				}
				this.m_CullingPlaneCount = value;
			}
		}

		public Vector4 cullingSphere
		{
			get
			{
				return this.m_CullingSphere;
			}
			set
			{
				this.m_CullingSphere = value;
			}
		}

		public float shadowCascadeBlendCullingFactor
		{
			get
			{
				return this.m_ShadowCascadeBlendCullingFactor;
			}
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentException(string.Format("Value should range from {0} to {1}, but was {2}.", 0, 1, value));
				}
				this.m_ShadowCascadeBlendCullingFactor = value;
			}
		}

		public unsafe Plane GetCullingPlane(int index)
		{
			bool flag = index < 0 || index >= this.cullingPlaneCount;
			if (flag)
			{
				throw new ArgumentException("index", string.Format("Index should be at least {0} and less than cullingPlaneCount ({1}), but was {2}.", 0, this.cullingPlaneCount, index));
			}
			byte* ptr = &this.m_CullingPlanes.FixedElementField;
			Plane* ptr2 = (Plane*)ptr;
			return ptr2[index];
		}

		public unsafe void SetCullingPlane(int index, Plane plane)
		{
			bool flag = index < 0 || index >= this.cullingPlaneCount;
			if (flag)
			{
				throw new ArgumentException("index", string.Format("Index should be at least {0} and less than cullingPlaneCount ({1}), but was {2}.", 0, this.cullingPlaneCount, index));
			}
			fixed (byte* ptr = &this.m_CullingPlanes.FixedElementField)
			{
				byte* ptr2 = ptr;
				Plane* ptr3 = (Plane*)ptr2;
				ptr3[index] = plane;
			}
		}

		public bool Equals(ShadowSplitData other)
		{
			bool flag = this.m_CullingPlaneCount != other.m_CullingPlaneCount;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < this.cullingPlaneCount; i++)
				{
					bool flag2 = !this.GetCullingPlane(i).Equals(other.GetCullingPlane(i));
					if (flag2)
					{
						result = false;
						return result;
					}
				}
				result = this.m_CullingSphere.Equals(other.m_CullingSphere);
			}
			return result;
		}

		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is ShadowSplitData && this.Equals((ShadowSplitData)obj);
		}

		public override int GetHashCode()
		{
			return this.m_CullingPlaneCount * 397 ^ this.m_CullingSphere.GetHashCode();
		}

		public static bool operator ==(ShadowSplitData left, ShadowSplitData right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(ShadowSplitData left, ShadowSplitData right)
		{
			return !left.Equals(right);
		}
	}
}
