using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	public struct Background : IEquatable<Background>
	{
		private Texture2D m_Texture;

		private VectorImage m_VectorImage;

		public Texture2D texture
		{
			get
			{
				return this.m_Texture;
			}
			set
			{
				bool flag = value != null && this.vectorImage != null;
				if (flag)
				{
					throw new InvalidOperationException("Cannot set both texture and vectorImage on Background object");
				}
				this.m_Texture = value;
			}
		}

		public VectorImage vectorImage
		{
			get
			{
				return this.m_VectorImage;
			}
			set
			{
				bool flag = value != null && this.texture != null;
				if (flag)
				{
					throw new InvalidOperationException("Cannot set both texture and vectorImage on Background object");
				}
				this.m_VectorImage = value;
			}
		}

		[Obsolete("Use Background.FromTexture2D instead")]
		public Background(Texture2D t)
		{
			this.m_Texture = t;
			this.m_VectorImage = null;
		}

		public static Background FromTexture2D(Texture2D t)
		{
			return new Background
			{
				texture = t
			};
		}

		public static Background FromVectorImage(VectorImage vi)
		{
			return new Background
			{
				vectorImage = vi
			};
		}

		internal static Background FromObject(object obj)
		{
			Texture2D texture2D = obj as Texture2D;
			bool flag = texture2D != null;
			Background result;
			if (flag)
			{
				result = Background.FromTexture2D(texture2D);
			}
			else
			{
				VectorImage vectorImage = obj as VectorImage;
				bool flag2 = vectorImage != null;
				if (flag2)
				{
					result = Background.FromVectorImage(vectorImage);
				}
				else
				{
					result = default(Background);
				}
			}
			return result;
		}

		public static bool operator ==(Background lhs, Background rhs)
		{
			return EqualityComparer<Texture2D>.Default.Equals(lhs.texture, rhs.texture);
		}

		public static bool operator !=(Background lhs, Background rhs)
		{
			return !(lhs == rhs);
		}

		public bool Equals(Background other)
		{
			return other == this;
		}

		public override bool Equals(object obj)
		{
			bool flag = !(obj is Background);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Background lhs = (Background)obj;
				result = (lhs == this);
			}
			return result;
		}

		public override int GetHashCode()
		{
			int num = 851985039;
			bool flag = this.texture != null;
			if (flag)
			{
				num = num * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(this.texture);
			}
			bool flag2 = this.vectorImage != null;
			if (flag2)
			{
				num = num * -1521134295 + EqualityComparer<VectorImage>.Default.GetHashCode(this.vectorImage);
			}
			return num;
		}

		public override string ToString()
		{
			return string.Format("{0}", this.texture);
		}
	}
}
