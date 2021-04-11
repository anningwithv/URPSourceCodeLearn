using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[Il2CppEagerStaticClassConstruction, UsedByNativeCode]
	public struct Vector3Int : IEquatable<Vector3Int>, IFormattable
	{
		private int m_X;

		private int m_Y;

		private int m_Z;

		private static readonly Vector3Int s_Zero = new Vector3Int(0, 0, 0);

		private static readonly Vector3Int s_One = new Vector3Int(1, 1, 1);

		private static readonly Vector3Int s_Up = new Vector3Int(0, 1, 0);

		private static readonly Vector3Int s_Down = new Vector3Int(0, -1, 0);

		private static readonly Vector3Int s_Left = new Vector3Int(-1, 0, 0);

		private static readonly Vector3Int s_Right = new Vector3Int(1, 0, 0);

		private static readonly Vector3Int s_Forward = new Vector3Int(0, 0, 1);

		private static readonly Vector3Int s_Back = new Vector3Int(0, 0, -1);

		public int x
		{
			get
			{
				return this.m_X;
			}
			set
			{
				this.m_X = value;
			}
		}

		public int y
		{
			get
			{
				return this.m_Y;
			}
			set
			{
				this.m_Y = value;
			}
		}

		public int z
		{
			get
			{
				return this.m_Z;
			}
			set
			{
				this.m_Z = value;
			}
		}

		public int this[int index]
		{
			get
			{
				int result;
				switch (index)
				{
				case 0:
					result = this.x;
					break;
				case 1:
					result = this.y;
					break;
				case 2:
					result = this.z;
					break;
				default:
					throw new IndexOutOfRangeException(UnityString.Format("Invalid Vector3Int index addressed: {0}!", new object[]
					{
						index
					}));
				}
				return result;
			}
			set
			{
				switch (index)
				{
				case 0:
					this.x = value;
					break;
				case 1:
					this.y = value;
					break;
				case 2:
					this.z = value;
					break;
				default:
					throw new IndexOutOfRangeException(UnityString.Format("Invalid Vector3Int index addressed: {0}!", new object[]
					{
						index
					}));
				}
			}
		}

		public float magnitude
		{
			get
			{
				return Mathf.Sqrt((float)(this.x * this.x + this.y * this.y + this.z * this.z));
			}
		}

		public int sqrMagnitude
		{
			get
			{
				return this.x * this.x + this.y * this.y + this.z * this.z;
			}
		}

		public static Vector3Int zero
		{
			get
			{
				return Vector3Int.s_Zero;
			}
		}

		public static Vector3Int one
		{
			get
			{
				return Vector3Int.s_One;
			}
		}

		public static Vector3Int up
		{
			get
			{
				return Vector3Int.s_Up;
			}
		}

		public static Vector3Int down
		{
			get
			{
				return Vector3Int.s_Down;
			}
		}

		public static Vector3Int left
		{
			get
			{
				return Vector3Int.s_Left;
			}
		}

		public static Vector3Int right
		{
			get
			{
				return Vector3Int.s_Right;
			}
		}

		public static Vector3Int forward
		{
			get
			{
				return Vector3Int.s_Forward;
			}
		}

		public static Vector3Int back
		{
			get
			{
				return Vector3Int.s_Back;
			}
		}

		[MethodImpl((MethodImplOptions)256)]
		public Vector3Int(int x, int y, int z)
		{
			this.m_X = x;
			this.m_Y = y;
			this.m_Z = z;
		}

		[MethodImpl((MethodImplOptions)256)]
		public void Set(int x, int y, int z)
		{
			this.m_X = x;
			this.m_Y = y;
			this.m_Z = z;
		}

		[MethodImpl((MethodImplOptions)256)]
		public static float Distance(Vector3Int a, Vector3Int b)
		{
			return (a - b).magnitude;
		}

		[MethodImpl((MethodImplOptions)256)]
		public static Vector3Int Min(Vector3Int lhs, Vector3Int rhs)
		{
			return new Vector3Int(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
		}

		[MethodImpl((MethodImplOptions)256)]
		public static Vector3Int Max(Vector3Int lhs, Vector3Int rhs)
		{
			return new Vector3Int(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
		}

		[MethodImpl((MethodImplOptions)256)]
		public static Vector3Int Scale(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		[MethodImpl((MethodImplOptions)256)]
		public void Scale(Vector3Int scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
			this.z *= scale.z;
		}

		[MethodImpl((MethodImplOptions)256)]
		public void Clamp(Vector3Int min, Vector3Int max)
		{
			this.x = Math.Max(min.x, this.x);
			this.x = Math.Min(max.x, this.x);
			this.y = Math.Max(min.y, this.y);
			this.y = Math.Min(max.y, this.y);
			this.z = Math.Max(min.z, this.z);
			this.z = Math.Min(max.z, this.z);
		}

		[MethodImpl((MethodImplOptions)256)]
		public static implicit operator Vector3(Vector3Int v)
		{
			return new Vector3((float)v.x, (float)v.y, (float)v.z);
		}

		[MethodImpl((MethodImplOptions)256)]
		public static explicit operator Vector2Int(Vector3Int v)
		{
			return new Vector2Int(v.x, v.y);
		}

		[MethodImpl((MethodImplOptions)256)]
		public static Vector3Int FloorToInt(Vector3 v)
		{
			return new Vector3Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y), Mathf.FloorToInt(v.z));
		}

		[MethodImpl((MethodImplOptions)256)]
		public static Vector3Int CeilToInt(Vector3 v)
		{
			return new Vector3Int(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y), Mathf.CeilToInt(v.z));
		}

		[MethodImpl((MethodImplOptions)256)]
		public static Vector3Int RoundToInt(Vector3 v)
		{
			return new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
		}

		[MethodImpl((MethodImplOptions)256)]
		public static Vector3Int operator +(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		[MethodImpl((MethodImplOptions)256)]
		public static Vector3Int operator -(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		[MethodImpl((MethodImplOptions)256)]
		public static Vector3Int operator *(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		[MethodImpl((MethodImplOptions)256)]
		public static Vector3Int operator -(Vector3Int a)
		{
			return new Vector3Int(-a.x, -a.y, -a.z);
		}

		[MethodImpl((MethodImplOptions)256)]
		public static Vector3Int operator *(Vector3Int a, int b)
		{
			return new Vector3Int(a.x * b, a.y * b, a.z * b);
		}

		[MethodImpl((MethodImplOptions)256)]
		public static Vector3Int operator *(int a, Vector3Int b)
		{
			return new Vector3Int(a * b.x, a * b.y, a * b.z);
		}

		[MethodImpl((MethodImplOptions)256)]
		public static Vector3Int operator /(Vector3Int a, int b)
		{
			return new Vector3Int(a.x / b, a.y / b, a.z / b);
		}

		[MethodImpl((MethodImplOptions)256)]
		public static bool operator ==(Vector3Int lhs, Vector3Int rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
		}

		[MethodImpl((MethodImplOptions)256)]
		public static bool operator !=(Vector3Int lhs, Vector3Int rhs)
		{
			return !(lhs == rhs);
		}

		[MethodImpl((MethodImplOptions)256)]
		public override bool Equals(object other)
		{
			bool flag = !(other is Vector3Int);
			return !flag && this.Equals((Vector3Int)other);
		}

		[MethodImpl((MethodImplOptions)256)]
		public bool Equals(Vector3Int other)
		{
			return this == other;
		}

		public override int GetHashCode()
		{
			int hashCode = this.y.GetHashCode();
			int hashCode2 = this.z.GetHashCode();
			return this.x.GetHashCode() ^ hashCode << 4 ^ hashCode >> 28 ^ hashCode2 >> 4 ^ hashCode2 << 28;
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
			return UnityString.Format("({0}, {1}, {2})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.z.ToString(format, formatProvider)
			});
		}
	}
}
