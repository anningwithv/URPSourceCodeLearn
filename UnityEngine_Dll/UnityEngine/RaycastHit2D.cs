using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Interfaces/IPhysics2D.h"), NativeClass("RaycastHit2D", "struct RaycastHit2D;"), RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	public struct RaycastHit2D
	{
		[NativeName("centroid")]
		private Vector2 m_Centroid;

		[NativeName("point")]
		private Vector2 m_Point;

		[NativeName("normal")]
		private Vector2 m_Normal;

		[NativeName("distance")]
		private float m_Distance;

		[NativeName("fraction")]
		private float m_Fraction;

		[NativeName("collider")]
		private int m_Collider;

		public Vector2 centroid
		{
			get
			{
				return this.m_Centroid;
			}
			set
			{
				this.m_Centroid = value;
			}
		}

		public Vector2 point
		{
			get
			{
				return this.m_Point;
			}
			set
			{
				this.m_Point = value;
			}
		}

		public Vector2 normal
		{
			get
			{
				return this.m_Normal;
			}
			set
			{
				this.m_Normal = value;
			}
		}

		public float distance
		{
			get
			{
				return this.m_Distance;
			}
			set
			{
				this.m_Distance = value;
			}
		}

		public float fraction
		{
			get
			{
				return this.m_Fraction;
			}
			set
			{
				this.m_Fraction = value;
			}
		}

		public Collider2D collider
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_Collider) as Collider2D;
			}
		}

		public Rigidbody2D rigidbody
		{
			get
			{
				return (this.collider != null) ? this.collider.attachedRigidbody : null;
			}
		}

		public Transform transform
		{
			get
			{
				Rigidbody2D rigidbody = this.rigidbody;
				bool flag = rigidbody != null;
				Transform result;
				if (flag)
				{
					result = rigidbody.transform;
				}
				else
				{
					bool flag2 = this.collider != null;
					if (flag2)
					{
						result = this.collider.transform;
					}
					else
					{
						result = null;
					}
				}
				return result;
			}
		}

		public static implicit operator bool(RaycastHit2D hit)
		{
			return hit.collider != null;
		}

		public int CompareTo(RaycastHit2D other)
		{
			bool flag = this.collider == null;
			int result;
			if (flag)
			{
				result = 1;
			}
			else
			{
				bool flag2 = other.collider == null;
				if (flag2)
				{
					result = -1;
				}
				else
				{
					result = this.fraction.CompareTo(other.fraction);
				}
			}
			return result;
		}
	}
}
