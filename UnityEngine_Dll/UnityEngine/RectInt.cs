using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[UsedByNativeCode]
	public struct RectInt : IEquatable<RectInt>, IFormattable
	{
		public struct PositionEnumerator : IEnumerator<Vector2Int>, IEnumerator, IDisposable
		{
			private readonly Vector2Int _min;

			private readonly Vector2Int _max;

			private Vector2Int _current;

			public Vector2Int Current
			{
				get
				{
					return this._current;
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			public PositionEnumerator(Vector2Int min, Vector2Int max)
			{
				this._current = min;
				this._min = min;
				this._max = max;
				this.Reset();
			}

			public RectInt.PositionEnumerator GetEnumerator()
			{
				return this;
			}

			public bool MoveNext()
			{
				bool flag = this._current.y >= this._max.y;
				bool result;
				if (flag)
				{
					result = false;
				}
				else
				{
					int num = this._current.x;
					this._current.x = num + 1;
					bool flag2 = this._current.x >= this._max.x;
					if (flag2)
					{
						this._current.x = this._min.x;
						bool flag3 = this._current.x >= this._max.x;
						if (flag3)
						{
							result = false;
							return result;
						}
						num = this._current.y;
						this._current.y = num + 1;
						bool flag4 = this._current.y >= this._max.y;
						if (flag4)
						{
							result = false;
							return result;
						}
					}
					result = true;
				}
				return result;
			}

			public void Reset()
			{
				this._current = this._min;
				int x = this._current.x;
				this._current.x = x - 1;
			}

			void IDisposable.Dispose()
			{
			}
		}

		private int m_XMin;

		private int m_YMin;

		private int m_Width;

		private int m_Height;

		public int x
		{
			get
			{
				return this.m_XMin;
			}
			set
			{
				this.m_XMin = value;
			}
		}

		public int y
		{
			get
			{
				return this.m_YMin;
			}
			set
			{
				this.m_YMin = value;
			}
		}

		public Vector2 center
		{
			get
			{
				return new Vector2((float)this.x + (float)this.m_Width / 2f, (float)this.y + (float)this.m_Height / 2f);
			}
		}

		public Vector2Int min
		{
			get
			{
				return new Vector2Int(this.xMin, this.yMin);
			}
			set
			{
				this.xMin = value.x;
				this.yMin = value.y;
			}
		}

		public Vector2Int max
		{
			get
			{
				return new Vector2Int(this.xMax, this.yMax);
			}
			set
			{
				this.xMax = value.x;
				this.yMax = value.y;
			}
		}

		public int width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				this.m_Width = value;
			}
		}

		public int height
		{
			get
			{
				return this.m_Height;
			}
			set
			{
				this.m_Height = value;
			}
		}

		public int xMin
		{
			get
			{
				return Math.Min(this.m_XMin, this.m_XMin + this.m_Width);
			}
			set
			{
				int xMax = this.xMax;
				this.m_XMin = value;
				this.m_Width = xMax - this.m_XMin;
			}
		}

		public int yMin
		{
			get
			{
				return Math.Min(this.m_YMin, this.m_YMin + this.m_Height);
			}
			set
			{
				int yMax = this.yMax;
				this.m_YMin = value;
				this.m_Height = yMax - this.m_YMin;
			}
		}

		public int xMax
		{
			get
			{
				return Math.Max(this.m_XMin, this.m_XMin + this.m_Width);
			}
			set
			{
				this.m_Width = value - this.m_XMin;
			}
		}

		public int yMax
		{
			get
			{
				return Math.Max(this.m_YMin, this.m_YMin + this.m_Height);
			}
			set
			{
				this.m_Height = value - this.m_YMin;
			}
		}

		public Vector2Int position
		{
			get
			{
				return new Vector2Int(this.m_XMin, this.m_YMin);
			}
			set
			{
				this.m_XMin = value.x;
				this.m_YMin = value.y;
			}
		}

		public Vector2Int size
		{
			get
			{
				return new Vector2Int(this.m_Width, this.m_Height);
			}
			set
			{
				this.m_Width = value.x;
				this.m_Height = value.y;
			}
		}

		public RectInt.PositionEnumerator allPositionsWithin
		{
			get
			{
				return new RectInt.PositionEnumerator(this.min, this.max);
			}
		}

		public void SetMinMax(Vector2Int minPosition, Vector2Int maxPosition)
		{
			this.min = minPosition;
			this.max = maxPosition;
		}

		public RectInt(int xMin, int yMin, int width, int height)
		{
			this.m_XMin = xMin;
			this.m_YMin = yMin;
			this.m_Width = width;
			this.m_Height = height;
		}

		public RectInt(Vector2Int position, Vector2Int size)
		{
			this.m_XMin = position.x;
			this.m_YMin = position.y;
			this.m_Width = size.x;
			this.m_Height = size.y;
		}

		public void ClampToBounds(RectInt bounds)
		{
			this.position = new Vector2Int(Math.Max(Math.Min(bounds.xMax, this.position.x), bounds.xMin), Math.Max(Math.Min(bounds.yMax, this.position.y), bounds.yMin));
			this.size = new Vector2Int(Math.Min(bounds.xMax - this.position.x, this.size.x), Math.Min(bounds.yMax - this.position.y, this.size.y));
		}

		public bool Contains(Vector2Int position)
		{
			return position.x >= this.xMin && position.y >= this.yMin && position.x < this.xMax && position.y < this.yMax;
		}

		public bool Overlaps(RectInt other)
		{
			return other.xMin < this.xMax && other.xMax > this.xMin && other.yMin < this.yMax && other.yMax > this.yMin;
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
			return UnityString.Format("(x:{0}, y:{1}, width:{2}, height:{3})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.width.ToString(format, formatProvider),
				this.height.ToString(format, formatProvider)
			});
		}

		public bool Equals(RectInt other)
		{
			return this.m_XMin == other.m_XMin && this.m_YMin == other.m_YMin && this.m_Width == other.m_Width && this.m_Height == other.m_Height;
		}
	}
}
