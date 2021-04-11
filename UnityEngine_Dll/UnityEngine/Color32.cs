using System;
using System.Globalization;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Explicit)]
	public struct Color32 : IFormattable
	{
		[Ignore(DoesNotContributeToSize = true)]
		[FieldOffset(0)]
		private int rgba;

		[FieldOffset(0)]
		public byte r;

		[FieldOffset(1)]
		public byte g;

		[FieldOffset(2)]
		public byte b;

		[FieldOffset(3)]
		public byte a;

		public byte this[int index]
		{
			get
			{
				byte result;
				switch (index)
				{
				case 0:
					result = this.r;
					break;
				case 1:
					result = this.g;
					break;
				case 2:
					result = this.b;
					break;
				case 3:
					result = this.a;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Color32 index(" + index.ToString() + ")!");
				}
				return result;
			}
			set
			{
				switch (index)
				{
				case 0:
					this.r = value;
					break;
				case 1:
					this.g = value;
					break;
				case 2:
					this.b = value;
					break;
				case 3:
					this.a = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Color32 index(" + index.ToString() + ")!");
				}
			}
		}

		public Color32(byte r, byte g, byte b, byte a)
		{
			this.rgba = 0;
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		public static implicit operator Color32(Color c)
		{
			return new Color32((byte)Mathf.Round(Mathf.Clamp01(c.r) * 255f), (byte)Mathf.Round(Mathf.Clamp01(c.g) * 255f), (byte)Mathf.Round(Mathf.Clamp01(c.b) * 255f), (byte)Mathf.Round(Mathf.Clamp01(c.a) * 255f));
		}

		public static implicit operator Color(Color32 c)
		{
			return new Color((float)c.r / 255f, (float)c.g / 255f, (float)c.b / 255f, (float)c.a / 255f);
		}

		public static Color32 Lerp(Color32 a, Color32 b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Color32((byte)((float)a.r + (float)(b.r - a.r) * t), (byte)((float)a.g + (float)(b.g - a.g) * t), (byte)((float)a.b + (float)(b.b - a.b) * t), (byte)((float)a.a + (float)(b.a - a.a) * t));
		}

		public static Color32 LerpUnclamped(Color32 a, Color32 b, float t)
		{
			return new Color32((byte)((float)a.r + (float)(b.r - a.r) * t), (byte)((float)a.g + (float)(b.g - a.g) * t), (byte)((float)a.b + (float)(b.b - a.b) * t), (byte)((float)a.a + (float)(b.a - a.a) * t));
		}

		[VisibleToOtherModules]
		internal bool InternalEquals(Color32 other)
		{
			return this.rgba == other.rgba;
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
			return UnityString.Format("RGBA({0}, {1}, {2}, {3})", new object[]
			{
				this.r.ToString(format, formatProvider),
				this.g.ToString(format, formatProvider),
				this.b.ToString(format, formatProvider),
				this.a.ToString(format, formatProvider)
			});
		}
	}
}
