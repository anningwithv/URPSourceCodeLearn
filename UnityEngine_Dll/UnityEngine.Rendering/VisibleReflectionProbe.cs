using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	[UsedByNativeCode]
	public struct VisibleReflectionProbe : IEquatable<VisibleReflectionProbe>
	{
		private Bounds m_Bounds;

		private Matrix4x4 m_LocalToWorldMatrix;

		private Vector4 m_HdrData;

		private Vector3 m_Center;

		private float m_BlendDistance;

		private int m_Importance;

		private int m_BoxProjection;

		private int m_InstanceId;

		private int m_TextureId;

		public Texture texture
		{
			get
			{
				return (Texture)UnityEngine.Object.FindObjectFromInstanceID(this.m_TextureId);
			}
		}

		public ReflectionProbe reflectionProbe
		{
			get
			{
				return (ReflectionProbe)UnityEngine.Object.FindObjectFromInstanceID(this.m_InstanceId);
			}
		}

		public Bounds bounds
		{
			get
			{
				return this.m_Bounds;
			}
			set
			{
				this.m_Bounds = value;
			}
		}

		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				return this.m_LocalToWorldMatrix;
			}
			set
			{
				this.m_LocalToWorldMatrix = value;
			}
		}

		public Vector4 hdrData
		{
			get
			{
				return this.m_HdrData;
			}
			set
			{
				this.m_HdrData = value;
			}
		}

		public Vector3 center
		{
			get
			{
				return this.m_Center;
			}
			set
			{
				this.m_Center = value;
			}
		}

		public float blendDistance
		{
			get
			{
				return this.m_BlendDistance;
			}
			set
			{
				this.m_BlendDistance = value;
			}
		}

		public int importance
		{
			get
			{
				return this.m_Importance;
			}
			set
			{
				this.m_Importance = value;
			}
		}

		public bool isBoxProjection
		{
			get
			{
				return Convert.ToBoolean(this.m_BoxProjection);
			}
			set
			{
				this.m_BoxProjection = Convert.ToInt32(value);
			}
		}

		public bool Equals(VisibleReflectionProbe other)
		{
			return this.m_Bounds.Equals(other.m_Bounds) && this.m_LocalToWorldMatrix.Equals(other.m_LocalToWorldMatrix) && this.m_HdrData.Equals(other.m_HdrData) && this.m_Center.Equals(other.m_Center) && this.m_BlendDistance.Equals(other.m_BlendDistance) && this.m_Importance == other.m_Importance && this.m_BoxProjection == other.m_BoxProjection && this.m_InstanceId == other.m_InstanceId && this.m_TextureId == other.m_TextureId;
		}

		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is VisibleReflectionProbe && this.Equals((VisibleReflectionProbe)obj);
		}

		public override int GetHashCode()
		{
			int num = this.m_Bounds.GetHashCode();
			num = (num * 397 ^ this.m_LocalToWorldMatrix.GetHashCode());
			num = (num * 397 ^ this.m_HdrData.GetHashCode());
			num = (num * 397 ^ this.m_Center.GetHashCode());
			num = (num * 397 ^ this.m_BlendDistance.GetHashCode());
			num = (num * 397 ^ this.m_Importance);
			num = (num * 397 ^ this.m_BoxProjection);
			num = (num * 397 ^ this.m_InstanceId);
			return num * 397 ^ this.m_TextureId;
		}

		public static bool operator ==(VisibleReflectionProbe left, VisibleReflectionProbe right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(VisibleReflectionProbe left, VisibleReflectionProbe right)
		{
			return !left.Equals(right);
		}
	}
}
