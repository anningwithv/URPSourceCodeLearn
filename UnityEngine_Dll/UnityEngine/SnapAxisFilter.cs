using System;

namespace UnityEngine
{
	internal struct SnapAxisFilter : IEquatable<SnapAxisFilter>
	{
		private const SnapAxis X = SnapAxis.X;

		private const SnapAxis Y = SnapAxis.Y;

		private const SnapAxis Z = SnapAxis.Z;

		public static readonly SnapAxisFilter all = new SnapAxisFilter(SnapAxis.All);

		private SnapAxis m_Mask;

		public float x
		{
			get
			{
				return ((this.m_Mask & SnapAxis.X) == SnapAxis.X) ? 1f : 0f;
			}
		}

		public float y
		{
			get
			{
				return ((this.m_Mask & SnapAxis.Y) == SnapAxis.Y) ? 1f : 0f;
			}
		}

		public float z
		{
			get
			{
				return ((this.m_Mask & SnapAxis.Z) == SnapAxis.Z) ? 1f : 0f;
			}
		}

		public int active
		{
			get
			{
				int num = 0;
				bool flag = (this.m_Mask & SnapAxis.X) > SnapAxis.None;
				if (flag)
				{
					num++;
				}
				bool flag2 = (this.m_Mask & SnapAxis.Y) > SnapAxis.None;
				if (flag2)
				{
					num++;
				}
				bool flag3 = (this.m_Mask & SnapAxis.Z) > SnapAxis.None;
				if (flag3)
				{
					num++;
				}
				return num;
			}
		}

		public float this[int i]
		{
			get
			{
				bool flag = i < 0 || i > 2;
				if (flag)
				{
					throw new IndexOutOfRangeException();
				}
				return (float)(SnapAxis.X & this.m_Mask >> (i & 31)) * 1f;
			}
			set
			{
				bool flag = i < 0 || i > 2;
				if (flag)
				{
					throw new IndexOutOfRangeException();
				}
				this.m_Mask &= (SnapAxis)(~(SnapAxis)(1 << i));
				this.m_Mask |= (SnapAxis)(((value > 0f) ? 1 : 0) << (i & 31));
			}
		}

		public SnapAxisFilter(Vector3 v)
		{
			this.m_Mask = SnapAxis.None;
			float num = 1E-06f;
			bool flag = Mathf.Abs(v.x) > num;
			if (flag)
			{
				this.m_Mask |= SnapAxis.X;
			}
			bool flag2 = Mathf.Abs(v.y) > num;
			if (flag2)
			{
				this.m_Mask |= SnapAxis.Y;
			}
			bool flag3 = Mathf.Abs(v.z) > num;
			if (flag3)
			{
				this.m_Mask |= SnapAxis.Z;
			}
		}

		public SnapAxisFilter(SnapAxis axis)
		{
			this.m_Mask = SnapAxis.None;
			bool flag = (axis & SnapAxis.X) == SnapAxis.X;
			if (flag)
			{
				this.m_Mask |= SnapAxis.X;
			}
			bool flag2 = (axis & SnapAxis.Y) == SnapAxis.Y;
			if (flag2)
			{
				this.m_Mask |= SnapAxis.Y;
			}
			bool flag3 = (axis & SnapAxis.Z) == SnapAxis.Z;
			if (flag3)
			{
				this.m_Mask |= SnapAxis.Z;
			}
		}

		public override string ToString()
		{
			return string.Format("{{{0}, {1}, {2}}}", this.x, this.y, this.z);
		}

		public static implicit operator Vector3(SnapAxisFilter mask)
		{
			return new Vector3(mask.x, mask.y, mask.z);
		}

		public static explicit operator SnapAxisFilter(Vector3 v)
		{
			return new SnapAxisFilter(v);
		}

		public static explicit operator SnapAxis(SnapAxisFilter mask)
		{
			return mask.m_Mask;
		}

		public static SnapAxisFilter operator |(SnapAxisFilter left, SnapAxisFilter right)
		{
			return new SnapAxisFilter(left.m_Mask | right.m_Mask);
		}

		public static SnapAxisFilter operator &(SnapAxisFilter left, SnapAxisFilter right)
		{
			return new SnapAxisFilter(left.m_Mask & right.m_Mask);
		}

		public static SnapAxisFilter operator ^(SnapAxisFilter left, SnapAxisFilter right)
		{
			return new SnapAxisFilter(left.m_Mask ^ right.m_Mask);
		}

		public static SnapAxisFilter operator ~(SnapAxisFilter left)
		{
			return new SnapAxisFilter(~left.m_Mask);
		}

		public static Vector3 operator *(SnapAxisFilter mask, float value)
		{
			return new Vector3(mask.x * value, mask.y * value, mask.z * value);
		}

		public static Vector3 operator *(SnapAxisFilter mask, Vector3 right)
		{
			return new Vector3(mask.x * right.x, mask.y * right.y, mask.z * right.z);
		}

		public static Vector3 operator *(Quaternion rotation, SnapAxisFilter mask)
		{
			int active = mask.active;
			bool flag = active > 2;
			Vector3 result;
			if (flag)
			{
				result = mask;
			}
			else
			{
				Vector3 vector = rotation * mask;
				vector = new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
				bool flag2 = active > 1;
				if (flag2)
				{
					result = new Vector3((float)((vector.x > vector.y || vector.x > vector.z) ? 1 : 0), (float)((vector.y > vector.x || vector.y > vector.z) ? 1 : 0), (float)((vector.z > vector.x || vector.z > vector.y) ? 1 : 0));
				}
				else
				{
					result = new Vector3((float)((vector.x > vector.y && vector.x > vector.z) ? 1 : 0), (float)((vector.y > vector.z && vector.y > vector.x) ? 1 : 0), (float)((vector.z > vector.x && vector.z > vector.y) ? 1 : 0));
				}
			}
			return result;
		}

		public static bool operator ==(SnapAxisFilter left, SnapAxisFilter right)
		{
			return left.m_Mask == right.m_Mask;
		}

		public static bool operator !=(SnapAxisFilter left, SnapAxisFilter right)
		{
			return !(left == right);
		}

		public bool Equals(SnapAxisFilter other)
		{
			return this.m_Mask == other.m_Mask;
		}

		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is SnapAxisFilter && this.Equals((SnapAxisFilter)obj);
		}

		public override int GetHashCode()
		{
			return this.m_Mask.GetHashCode();
		}
	}
}
