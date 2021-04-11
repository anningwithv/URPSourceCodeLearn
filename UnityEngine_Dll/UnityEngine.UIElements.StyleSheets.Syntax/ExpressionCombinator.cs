using System;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	internal enum ExpressionCombinator
	{
		None,
		Or,
		OrOr,
		AndAnd,
		Juxtaposition,
		Group
	}
}
