using System;

namespace UnityEngine.TextCore
{
	[Serializable]
	internal class TextElement
	{
		[SerializeField]
		protected TextElementType m_ElementType;

		[SerializeField]
		private uint m_Unicode;

		private Glyph m_Glyph;

		[SerializeField]
		private uint m_GlyphIndex;

		[SerializeField]
		private float m_Scale;

		public TextElementType elementType
		{
			get
			{
				return this.m_ElementType;
			}
		}

		public uint unicode
		{
			get
			{
				return this.m_Unicode;
			}
			set
			{
				this.m_Unicode = value;
			}
		}

		public Glyph glyph
		{
			get
			{
				return this.m_Glyph;
			}
			set
			{
				this.m_Glyph = value;
			}
		}

		public uint glyphIndex
		{
			get
			{
				return this.m_GlyphIndex;
			}
			set
			{
				this.m_GlyphIndex = value;
			}
		}

		public float scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				this.m_Scale = value;
			}
		}
	}
}
