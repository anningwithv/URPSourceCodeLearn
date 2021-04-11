using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	[UsedByNativeCode]
	public struct VisibleLight : IEquatable<VisibleLight>
	{
		private LightType m_LightType;

		private Color m_FinalColor;

		private Rect m_ScreenRect;

		private Matrix4x4 m_LocalToWorldMatrix;

		private float m_Range;

		private float m_SpotAngle;

		private int m_InstanceId;

		private VisibleLightFlags m_Flags;

		public Light light
		{
			get
			{
				return (Light)UnityEngine.Object.FindObjectFromInstanceID(this.m_InstanceId);
			}
		}

		public LightType lightType
		{
			get
			{
				return this.m_LightType;
			}
			set
			{
				this.m_LightType = value;
			}
		}

		public Color finalColor
		{
			get
			{
				return this.m_FinalColor;
			}
			set
			{
				this.m_FinalColor = value;
			}
		}

		public Rect screenRect
		{
			get
			{
				return this.m_ScreenRect;
			}
			set
			{
				this.m_ScreenRect = value;
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

		public float range
		{
			get
			{
				return this.m_Range;
			}
			set
			{
				this.m_Range = value;
			}
		}

		public float spotAngle
		{
			get
			{
				return this.m_SpotAngle;
			}
			set
			{
				this.m_SpotAngle = value;
			}
		}

		public bool intersectsNearPlane
		{
			get
			{
				return (this.m_Flags & VisibleLightFlags.IntersectsNearPlane) > (VisibleLightFlags)0;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= VisibleLightFlags.IntersectsNearPlane;
				}
				else
				{
					this.m_Flags &= ~VisibleLightFlags.IntersectsNearPlane;
				}
			}
		}

		public bool intersectsFarPlane
		{
			get
			{
				return (this.m_Flags & VisibleLightFlags.IntersectsFarPlane) > (VisibleLightFlags)0;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= VisibleLightFlags.IntersectsFarPlane;
				}
				else
				{
					this.m_Flags &= ~VisibleLightFlags.IntersectsFarPlane;
				}
			}
		}

		public bool Equals(VisibleLight other)
		{
			return this.m_LightType == other.m_LightType && this.m_FinalColor.Equals(other.m_FinalColor) && this.m_ScreenRect.Equals(other.m_ScreenRect) && this.m_LocalToWorldMatrix.Equals(other.m_LocalToWorldMatrix) && this.m_Range.Equals(other.m_Range) && this.m_SpotAngle.Equals(other.m_SpotAngle) && this.m_InstanceId == other.m_InstanceId && this.m_Flags == other.m_Flags;
		}

		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is VisibleLight && this.Equals((VisibleLight)obj);
		}

		public override int GetHashCode()
		{
			int num = (int)this.m_LightType;
			num = (num * 397 ^ this.m_FinalColor.GetHashCode());
			num = (num * 397 ^ this.m_ScreenRect.GetHashCode());
			num = (num * 397 ^ this.m_LocalToWorldMatrix.GetHashCode());
			num = (num * 397 ^ this.m_Range.GetHashCode());
			num = (num * 397 ^ this.m_SpotAngle.GetHashCode());
			num = (num * 397 ^ this.m_InstanceId);
			return num * 397 ^ (int)this.m_Flags;
		}

		public static bool operator ==(VisibleLight left, VisibleLight right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(VisibleLight left, VisibleLight right)
		{
			return !left.Equals(right);
		}
	}
}
