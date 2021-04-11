using System;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	internal struct StyleSyntaxToken
	{
		public StyleSyntaxTokenType type;

		public string text;

		public int number;

		public StyleSyntaxToken(StyleSyntaxTokenType t)
		{
			this.type = t;
			this.text = null;
			this.number = 0;
		}

		public StyleSyntaxToken(StyleSyntaxTokenType type, string text)
		{
			this.type = type;
			this.text = text;
			this.number = 0;
		}

		public StyleSyntaxToken(StyleSyntaxTokenType type, int number)
		{
			this.type = type;
			this.text = null;
			this.number = number;
		}
	}
}
