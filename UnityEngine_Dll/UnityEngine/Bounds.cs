using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Geometry/Ray.h"), NativeHeader("Runtime/Geometry/Intersection.h"), NativeHeader("Runtime/Math/MathScripting.h"), NativeHeader("Runtime/Geometry/AABB.h"), NativeType(Header = "Runtime/Geometry/AABB.h"), NativeClass("AABB"), RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	public struct Bounds : IEquatable<Bounds>, IFormattable
	{
		private Vector3 m_Center;

		[NativeName("m_Extent")]
		private Vector3 m_Extents;

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

		public Vector3 size
		{
			get
			{
				return this.m_Extents * 2f;
			}
			set
			{
				this.m_Extents = value * 0.5f;
			}
		}

		public Vector3 extents
		{
			get
			{
				return this.m_Extents;
			}
			set
			{
				this.m_Extents = value;
			}
		}

		public Vector3 min
		{
			get
			{
				return this.center - this.extents;
			}
			set
			{
				this.SetMinMax(value, this.max);
			}
		}

		public Vector3 max
		{
			get
			{
				return this.center + this.extents;
			}
			set
			{
				this.SetMinMax(this.min, value);
			}
		}

		public Bounds(Vector3 center, Vector3 size)
		{
			this.m_Center = center;
			this.m_Extents = size * 0.5f;
		}

		public override int GetHashCode()
		{
			return this.center.GetHashCode() ^ this.extents.GetHashCode() << 2;
		}

		public override bool Equals(object other)
		{
			bool flag = !(other is Bounds);
			return !flag && this.Equals((Bounds)other);
		}

		public bool Equals(Bounds other)
		{
			return this.center.Equals(other.center) && this.extents.Equals(other.extents);
		}

		public static bool operator ==(Bounds lhs, Bounds rhs)
		{
			return lhs.center == rhs.center && lhs.extents == rhs.extents;
		}

		public static bool operator !=(Bounds lhs, Bounds rhs)
		{
			return !(lhs == rhs);
		}

		public void SetMinMax(Vector3 min, Vector3 max)
		{
			this.extents = (max - min) * 0.5f;
			this.center = min + this.extents;
		}

		public void Encapsulate(Vector3 point)
		{
			this.SetMinMax(Vector3.Min(this.min, point), Vector3.Max(this.max, point));
		}

		public void Encapsulate(Bounds bounds)
		{
			this.Encapsulate(bounds.center - bounds.extents);
			this.Encapsulate(bounds.center + bounds.extents);
		}

		public void Expand(float amount)
		{
			amount *= 0.5f;
			this.extents += new Vector3(amount, amount, amount);
		}

		public void Expand(Vector3 amount)
		{
			this.extents += amount * 0.5f;
		}

		public bool Intersects(Bounds bounds)
		{
			return this.min.x <= bounds.max.x && this.max.x >= bounds.min.x && this.min.y <= bounds.max.y && this.max.y >= bounds.min.y && this.min.z <= bounds.max.z && this.max.z >= bounds.min.z;
		}

		public bool IntersectRay(Ray ray)
		{
			float num;
			return Bounds.IntersectRayAABB(ray, this, out num);
		}

		public bool IntersectRay(Ray ray, out float distance)
		{
			return Bounds.IntersectRayAABB(ray, this, out distance);
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
			return UnityString.Format("Center: {0}, Extents: {1}", new object[]
			{
				this.m_Center.ToString(format, formatProvider),
				this.m_Extents.ToString(format, formatProvider)
			});
		}

		[NativeMethod("IsInside", IsThreadSafe = true)]
		public bool Contains(Vector3 point)
		{
			return Bounds.Contains_Injected(ref this, ref point);
		}

		[FreeFunction("BoundsScripting::SqrDistance", HasExplicitThis = true, IsThreadSafe = true)]
		public float SqrDistance(Vector3 point)
		{
			return Bounds.SqrDistance_Injected(ref this, ref point);
		}

		[FreeFunction("IntersectRayAABB", IsThreadSafe = true)]
		private static bool IntersectRayAABB(Ray ray, Bounds bounds, out float dist)
		{
			return Bounds.IntersectRayAABB_Injected(ref ray, ref bounds, out dist);
		}

		[FreeFunction("BoundsScripting::ClosestPoint", HasExplicitThis = true, IsThreadSafe = true)]
		public Vector3 ClosestPoint(Vector3 point)
		{
			Vector3 result;
			Bounds.ClosestPoint_Injected(ref this, ref point, out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Contains_Injected(ref Bounds _unity_self, ref Vector3 point);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float SqrDistance_Injected(ref Bounds _unity_self, ref Vector3 point);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IntersectRayAABB_Injected(ref Ray ray, ref Bounds bounds, out float dist);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClosestPoint_Injected(ref Bounds _unity_self, ref Vector3 point, out Vector3 ret);
	}
}
