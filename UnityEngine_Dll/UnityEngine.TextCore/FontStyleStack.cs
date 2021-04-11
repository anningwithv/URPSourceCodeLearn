using System;

namespace UnityEngine.TextCore
{
	internal struct FontStyleStack
	{
		public byte bold;

		public byte italic;

		public byte underline;

		public byte strikethrough;

		public byte highlight;

		public byte superscript;

		public byte subscript;

		public byte uppercase;

		public byte lowercase;

		public byte smallcaps;

		public void Clear()
		{
			this.bold = 0;
			this.italic = 0;
			this.underline = 0;
			this.strikethrough = 0;
			this.highlight = 0;
			this.superscript = 0;
			this.subscript = 0;
			this.uppercase = 0;
			this.lowercase = 0;
			this.smallcaps = 0;
		}

		public byte Add(FontStyles style)
		{
			byte result;
			if (style <= FontStyles.Strikethrough)
			{
				switch (style)
				{
				case FontStyles.Bold:
					this.bold += 1;
					result = this.bold;
					return result;
				case FontStyles.Italic:
					this.italic += 1;
					result = this.italic;
					return result;
				case FontStyles.Bold | FontStyles.Italic:
					break;
				case FontStyles.Underline:
					this.underline += 1;
					result = this.underline;
					return result;
				default:
					if (style == FontStyles.Strikethrough)
					{
						this.strikethrough += 1;
						result = this.strikethrough;
						return result;
					}
					break;
				}
			}
			else
			{
				if (style == FontStyles.Superscript)
				{
					this.superscript += 1;
					result = this.superscript;
					return result;
				}
				if (style == FontStyles.Subscript)
				{
					this.subscript += 1;
					result = this.subscript;
					return result;
				}
				if (style == FontStyles.Highlight)
				{
					this.highlight += 1;
					result = this.highlight;
					return result;
				}
			}
			result = 0;
			return result;
		}

		public byte Remove(FontStyles style)
		{
			byte result;
			if (style <= FontStyles.Strikethrough)
			{
				switch (style)
				{
				case FontStyles.Bold:
				{
					bool flag = this.bold > 1;
					if (flag)
					{
						this.bold -= 1;
					}
					else
					{
						this.bold = 0;
					}
					result = this.bold;
					return result;
				}
				case FontStyles.Italic:
				{
					bool flag2 = this.italic > 1;
					if (flag2)
					{
						this.italic -= 1;
					}
					else
					{
						this.italic = 0;
					}
					result = this.italic;
					return result;
				}
				case FontStyles.Bold | FontStyles.Italic:
					break;
				case FontStyles.Underline:
				{
					bool flag3 = this.underline > 1;
					if (flag3)
					{
						this.underline -= 1;
					}
					else
					{
						this.underline = 0;
					}
					result = this.underline;
					return result;
				}
				default:
					if (style == FontStyles.Strikethrough)
					{
						bool flag4 = this.strikethrough > 1;
						if (flag4)
						{
							this.strikethrough -= 1;
						}
						else
						{
							this.strikethrough = 0;
						}
						result = this.strikethrough;
						return result;
					}
					break;
				}
			}
			else
			{
				if (style == FontStyles.Superscript)
				{
					bool flag5 = this.superscript > 1;
					if (flag5)
					{
						this.superscript -= 1;
					}
					else
					{
						this.superscript = 0;
					}
					result = this.superscript;
					return result;
				}
				if (style == FontStyles.Subscript)
				{
					bool flag6 = this.subscript > 1;
					if (flag6)
					{
						this.subscript -= 1;
					}
					else
					{
						this.subscript = 0;
					}
					result = this.subscript;
					return result;
				}
				if (style == FontStyles.Highlight)
				{
					bool flag7 = this.highlight > 1;
					if (flag7)
					{
						this.highlight -= 1;
					}
					else
					{
						this.highlight = 0;
					}
					result = this.highlight;
					return result;
				}
			}
			result = 0;
			return result;
		}
	}
}
