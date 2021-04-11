using System;

namespace UnityEngine.TextCore
{
	[Serializable]
	internal class SpriteCharacter : TextElement
	{
		[SerializeField]
		private string m_Name;

		[SerializeField]
		private int m_HashCode;

		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				bool flag = value == this.m_Name;
				if (!flag)
				{
					this.m_Name = value;
					this.m_HashCode = TextUtilities.GetHashCodeCaseInSensitive(this.m_Name);
				}
			}
		}

		public int hashCode
		{
			get
			{
				return this.m_HashCode;
			}
		}

		public SpriteCharacter()
		{
			this.m_ElementType = TextElementType.Sprite;
		}

		public SpriteCharacter(uint unicode, SpriteGlyph glyph)
		{
			this.m_ElementType = TextElementType.Sprite;
			base.unicode = unicode;
			base.glyphIndex = glyph.index;
			base.glyph = glyph;
			base.scale = 1f;
		}
	}
}
