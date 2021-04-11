using System;
using System.Globalization;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[UsedByNativeCode]
	public struct Plane : IFormattable
	{
		internal const int size = 16;

		private Vector3 m_Normal;

		private float m_Distance;

		public Vector3 normal
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

		public Plane flipped
		{
			get
			{
				return new Plane(-this.m_Normal, -this.m_Distance);
			}
		}

		public Plane(Vector3 inNormal, Vector3 inPoint)
		{
			this.m_Normal = Vector3.Normalize(inNormal);
			this.m_Distance = -Vector3.Dot(this.m_Normal, inPoint);
		}

		public Plane(Vector3 inNormal, float d)
		{
			this.m_Normal = Vector3.Normalize(inNormal);
			this.m_Distance = d;
		}

		public Plane(Vector3 a, Vector3 b, Vector3 c)
		{
			this.m_Normal = Vector3.Normalize(Vector3.Cross(b - a, c - a));
			this.m_Distance = -Vector3.Dot(this.m_Normal, a);
		}

		public void SetNormalAndPosition(Vector3 inNormal, Vector3 inPoint)
		{
			this.m_Normal = Vector3.Normalize(inNormal);
			this.m_Distance = -Vector3.Dot(inNormal, inPoint);
		}

		public void Set3Points(Vector3 a, Vector3 b, Vector3 c)
		{
			this.m_Normal = Vector3.Normalize(Vector3.Cross(b - a, c - a));
			this.m_Distance = -Vector3.Dot(this.m_Normal, a);
		}

		public void Flip()
		{
			this.m_Normal = -this.m_Normal;
			this.m_Distance = -this.m_Distance;
		}

		public void Translate(Vector3 translation)
		{
			this.m_Distance += Vector3.Dot(this.m_Normal, translation);
		}

		public static Plane Translate(Plane plane, Vector3 translation)
		{
			return new Plane(plane.m_Normal, plane.m_Distance += Vector3.Dot(plane.m_Normal, translation));
		}

		public Vector3 ClosestPointOnPlane(Vector3 point)
		{
			float d = Vector3.Dot(this.m_Normal, point) + this.m_Distance;
			return point - this.m_Normal * d;
		}

		public float GetDistanceToPoint(Vector3 point)
		{
			return Vector3.Dot(this.m_Normal, point) + this.m_Distance;
		}

		public bool GetSide(Vector3 point)
		{
			return Vector3.Dot(this.m_Normal, point) + this.m_Distance > 0f;
		}

		public bool SameSide(Vector3 inPt0, Vector3 inPt1)
		{
			float distanceToPoint = this.GetDistanceToPoint(inPt0);
			float distanceToPoint2 = this.GetDistanceToPoint(inPt1);
			return (distanceToPoint > 0f && distanceToPoint2 > 0f) || (distanceToPoint <= 0f && distanceToPoint2 <= 0f);
		}

		public bool Raycast(Ray ray, out float enter)
		{
			float num = Vector3.Dot(ray.direction, this.m_Normal);
			float num2 = -Vector3.Dot(ray.origin, this.m_Normal) - this.m_Distance;
			bool flag = Mathf.Approximately(num, 0f);
			bool result;
			if (flag)
			{
				enter = 0f;
				result = false;
			}
			else
			{
				enter = num2 / num;
				result = (enter > 0f);
			}
			return result;
		}

		public override string ToString()
		{
			return this.ToString(null, CultureInfo.InvariantCulture.NumberFormat);
		}

		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.InvariantCulture.NumberFormat);
		}

		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = "F1";
			}
			return UnityString.Format("(normal:{0}, distance:{1})", new object[]
			{
				this.m_Normal.ToString(format, formatProvider),
				this.m_Distance.ToString(format, formatProvider)
			});
		}
	}
}
