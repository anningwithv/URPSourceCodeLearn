using System;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	internal class Expression
	{
		public ExpressionType type;

		public ExpressionMultiplier multiplier;

		public DataType dataType;

		public ExpressionCombinator combinator;

		public Expression[] subExpressions;

		public string keyword;

		public Expression(ExpressionType type)
		{
			this.type = type;
			this.combinator = ExpressionCombinator.None;
			this.multiplier = new ExpressionMultiplier(ExpressionMultiplierType.None);
			this.subExpressions = null;
			this.keyword = null;
		}
	}
}
