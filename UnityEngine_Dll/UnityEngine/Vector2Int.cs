using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[Il2CppEagerStaticClassConstruction, UsedByNativeCode]
	public struct Vector2Int : IEquatable<Vector2Int>, IFormattable
	{
		private int m_X;

		private int m_Y;

		private static readonly Vector2Int s_Zero = new Vector2Int(0, 0);

		private static readonly Vector2Int s_One = new Vector2Int(1, 1);

		private static readonly Vector2Int s_Up = new Vector2Int(0, 1);

		private static readonly Vector2Int s_Down = new Vector2Int(0, -1);

		private static readonly Vector2Int s_Left = new Vector2Int(-1, 0);

		private static readonly Vector2Int s_Right = new Vector2Int(1, 0);

		public int x
		{
			[MethodImpl((MethodImplOptions)256)]
			get
			{
				return this.m_X;
			}
			[MethodImpl((MethodImplOptions)256)]
			set
			{
				this.m_X = value;
			}
		}

		public int y
		{
			[MethodImpl((MethodImplOptions)256)]
			get
			{
				return this.m_Y;
			}
			[MethodImpl((MethodImplOptions)256)]
			set
			{
				this.m_Y = value;
			}
		}

		public int this[int index]
		{
			get
			{
				int result;
				if (index != 0)
				{
					if (index != 1)
					{
						throw new IndexOutOfRangeException(string.Format("Invalid Vector2Int index addressed: {0}!", index));
					}
					result = this.y;
				}
				else
				{
					result = this.x;
				}
				return result;
			}
			set
			{
				if (index != 0)
				{
					if (index != 1)
					{
						throw new IndexOutOfRangeException(string.Format("Invalid Vector2Int index addressed: {0}!", index));
					}
					this.y = value;
				}
				else
				{
					this.x = value;
				}
			}
		}

		public float magnitude
		{
			get
			{
				return Mathf.Sqrt((float)(this.x * this.x + this.y * this.y));
			}
		}

		public int sqrMagnitude
		{
			get
			{
				return this.x * this.x + this.y * this.y;
			}
		}

		public static Vector2Int zero
		{
			get
			{
				return Vector2Int.s_Zero;
			}
		}

		public static Vector2Int one
		{
			get
			{
				return Vector2Int.s_One;
			}
		}

		public static Vector2Int up
		{
			get
			{
				return Vector2Int.s_Up;
			}
		}

		public static Vector2Int down
		{
			get
			{
				return Vector2Int.s_Down;
			}
		}

		public static Vector2Int left
		{
			get
			{
				return Vector2Int.s_Left;
			}
		}

		public static Vector2Int right
		{
			get
			{
				return Vector2Int.s_Right;
			}
		}

		[MethodImpl((MethodImplOptions)256)]
		public Vector2Int(int x, int y)
		{
			this.m_X = x;
			this.m_Y = y;
		}

		[MethodImpl((MethodImplOptions)256)]
		public void Set(int x, int y)
		{
			this.m_X = x;
			this.m_Y = y;
		}

		[MethodImpl((MethodImplOptions)256)]
		public static float Distance(Vector2Int a, Vector2Int b)
		{
			float num = (float)(a.x - b.x);
			float num2 = (float)(a.y - b.y);
			return (float)Math.Sqrt((double)(num * num + num2 * num2));
		}

		[MethodImpl((MethodImplOptions)256)]
		public static Vector2Int Min(Vector2Int lhs, Vector2Int rhs)
		{
			return new Vector2Int(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y));
		}

		[MethodImpl((MethodImplOptions)256)]
		public static Vector2Int Max(Vector2Int lhs, Vector2Int rhs)
		{
			return new Vector2Int(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));
		}

		[MethodImpl((MethodImplOptions)256)]
		public static Vector2Int Scale(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x * b.x, a.y * b.y);
		}

		[MethodImpl((MethodImplOptions)256)]
		public void Scale(Vector2Int scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
		}

		[MethodImpl((MethodImplOptions)256)]
		public void Clamp(Vector2Int min, Vector2Int max)
		{
			this.x = Math.Max(min.x, this.x);
			this.x = Math.Min(max.x, this.x);
			this.y = Math.Max(min.y, this.y);
			this.y = Math.Min(max.y, this.y);
		}

		public static implicit operator Vector2(Vector2Int v)
		{
			return new Vector2((float)v.x, (float)v.y);
		}

		public static explicit operator Vector3Int(Vector2Int v)
		{
			return new Vector3Int(v.x, v.y, 0);
		}

		[MethodImpl((MethodImplOptions)256)]
		public static Vector2Int FloorToInt(Vector2 v)
		{
			return new Vector2Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y));
		}

		[MethodImpl((MethodImplOptions)256)]
		public static Vector2Int CeilToInt(Vector2 v)
		{
			return new Vector2Int(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y));
		}

		[MethodImpl((MethodImplOptions)256)]
		public static Vector2Int RoundToInt(Vector2 v)
		{
			return new Vector2Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
		}

		[MethodImpl((MethodImplOptions)256)]
		public static Vector2Int operator -(Vector2Int v)
		{
			return new Vector2Int(-v.x, -v.y);
		}

		[MethodImpl((MethodImplOptions)256)]
		public static Vector2Int operator +(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x + b.x, a.y + b.y);
		}

		[MethodImpl((MethodImplOptions)256)]
		public static Vector2Int operator -(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x - b.x, a.y - b.y);
		}

		[MethodImpl((MethodImplOptions)256)]
		public static Vector2Int operator *(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x * b.x, a.y * b.y);
		}

		public static Vector2Int operator *(int a, Vector2Int b)
		{
			return new Vector2Int(a * b.x, a * b.y);
		}

		[MethodImpl((MethodImplOptions)256)]
		public static Vector2Int operator *(Vector2Int a, int b)
		{
			return new Vector2Int(a.x * b, a.y * b);
		}

		[MethodImpl((MethodImplOptions)256)]
		public static Vector2Int operator /(Vector2Int a, int b)
		{
			return new Vector2Int(a.x / b, a.y / b);
		}

		[MethodImpl((MethodImplOptions)256)]
		public static bool operator ==(Vector2Int lhs, Vector2Int rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y;
		}

		[MethodImpl((MethodImplOptions)256)]
		public static bool operator !=(Vector2Int lhs, Vector2Int rhs)
		{
			return !(lhs == rhs);
		}

		[MethodImpl((MethodImplOptions)256)]
		public override bool Equals(object other)
		{
			bool flag = !(other is Vector2Int);
			return !flag && this.Equals((Vector2Int)other);
		}

		[MethodImpl((MethodImplOptions)256)]
		public bool Equals(Vector2Int other)
		{
			return this.x == other.x && this.y == other.y;
		}

		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.y.GetHashCode() << 2;
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
			return UnityString.Format("({0}, {1})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider)
			});
		}
	}
}
