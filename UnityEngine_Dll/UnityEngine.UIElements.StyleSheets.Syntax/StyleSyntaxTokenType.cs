using System;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	internal enum StyleSyntaxTokenType
	{
		Unknown,
		String,
		Number,
		Space,
		SingleBar,
		DoubleBar,
		DoubleAmpersand,
		Comma,
		SingleQuote,
		Asterisk,
		Plus,
		QuestionMark,
		HashMark,
		ExclamationPoint,
		OpenBracket,
		CloseBracket,
		OpenBrace,
		CloseBrace,
		LessThan,
		GreaterThan,
		End
	}
}
