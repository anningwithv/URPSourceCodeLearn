using System;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	internal struct ExpressionMultiplier
	{
		public const int Infinity = 10;

		private ExpressionMultiplierType m_Type;

		public int min;

		public int max;

		public ExpressionMultiplierType type
		{
			get
			{
				return this.m_Type;
			}
			set
			{
				this.SetType(value);
			}
		}

		public ExpressionMultiplier(ExpressionMultiplierType type = ExpressionMultiplierType.None)
		{
			this.m_Type = type;
			this.min = (this.max = 1);
			this.SetType(type);
		}

		private void SetType(ExpressionMultiplierType value)
		{
			this.m_Type = value;
			switch (value)
			{
			case ExpressionMultiplierType.ZeroOrMore:
				this.min = 0;
				this.max = 10;
				return;
			case ExpressionMultiplierType.OneOrMore:
			case ExpressionMultiplierType.OneOrMoreComma:
			case ExpressionMultiplierType.GroupAtLeastOne:
				this.min = 1;
				this.max = 10;
				return;
			case ExpressionMultiplierType.ZeroOrOne:
				this.min = 0;
				this.max = 1;
				return;
			}
			this.min = (this.max = 1);
		}
	}
}
