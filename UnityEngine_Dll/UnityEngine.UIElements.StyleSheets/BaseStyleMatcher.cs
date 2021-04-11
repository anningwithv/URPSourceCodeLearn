using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets.Syntax;

namespace UnityEngine.UIElements.StyleSheets
{
	internal abstract class BaseStyleMatcher
	{
		private Stack<int> m_MarkStack = new Stack<int>();

		protected int m_CurrentIndex;

		public abstract int valueCount
		{
			get;
		}

		public abstract bool isVariable
		{
			get;
		}

		public bool hasCurrent
		{
			get
			{
				return this.m_CurrentIndex < this.valueCount;
			}
		}

		public int matchedVariableCount
		{
			get;
			set;
		}

		protected abstract bool MatchKeyword(string keyword);

		protected abstract bool MatchNumber();

		protected abstract bool MatchInteger();

		protected abstract bool MatchLength();

		protected abstract bool MatchPercentage();

		protected abstract bool MatchColor();

		protected abstract bool MatchResource();

		protected abstract bool MatchUrl();

		protected void Initialize()
		{
			this.m_CurrentIndex = 0;
			this.m_MarkStack.Clear();
			this.matchedVariableCount = 0;
		}

		public void MoveNext()
		{
			bool flag = this.m_CurrentIndex + 1 <= this.valueCount;
			if (flag)
			{
				this.m_CurrentIndex++;
			}
		}

		public void SaveMark()
		{
			this.m_MarkStack.Push(this.m_CurrentIndex);
		}

		public void RestoreMark()
		{
			this.m_CurrentIndex = this.m_MarkStack.Pop();
		}

		public void DropMark()
		{
			this.m_MarkStack.Pop();
		}

		protected bool Match(Expression exp)
		{
			bool flag = true;
			bool flag2 = exp.multiplier.type == ExpressionMultiplierType.None;
			if (flag2)
			{
				flag = this.MatchExpression(exp);
			}
			else
			{
				Debug.Assert(exp.multiplier.type != ExpressionMultiplierType.OneOrMoreComma, "'#' multiplier in syntax expression is not supported");
				Debug.Assert(exp.multiplier.type != ExpressionMultiplierType.GroupAtLeastOne, "'!' multiplier in syntax expression is not supported");
				int min = exp.multiplier.min;
				int max = exp.multiplier.max;
				int num = 0;
				int num2 = 0;
				while (flag && this.hasCurrent && num2 < max)
				{
					flag = this.MatchExpression(exp);
					bool flag3 = flag;
					if (flag3)
					{
						num++;
					}
					num2++;
				}
				flag = (num >= min && num <= max);
			}
			return flag;
		}

		private bool MatchExpression(Expression exp)
		{
			bool flag = false;
			bool flag2 = exp.type == ExpressionType.Combinator;
			if (flag2)
			{
				flag = this.MatchCombinator(exp);
			}
			else
			{
				bool isVariable = this.isVariable;
				if (isVariable)
				{
					flag = true;
					int matchedVariableCount = this.matchedVariableCount;
					this.matchedVariableCount = matchedVariableCount + 1;
				}
				else
				{
					bool flag3 = exp.type == ExpressionType.Data;
					if (flag3)
					{
						flag = this.MatchDataType(exp);
					}
					else
					{
						bool flag4 = exp.type == ExpressionType.Keyword;
						if (flag4)
						{
							flag = this.MatchKeyword(exp.keyword);
						}
					}
				}
				bool flag5 = flag;
				if (flag5)
				{
					this.MoveNext();
				}
			}
			bool flag6 = !flag && !this.hasCurrent && this.matchedVariableCount > 0;
			if (flag6)
			{
				flag = true;
			}
			return flag;
		}

		private bool MatchGroup(Expression exp)
		{
			Debug.Assert(exp.subExpressions.Length == 1, "Group has invalid number of sub expressions");
			Expression exp2 = exp.subExpressions[0];
			return this.Match(exp2);
		}

		private bool MatchCombinator(Expression exp)
		{
			this.SaveMark();
			bool flag = false;
			switch (exp.combinator)
			{
			case ExpressionCombinator.Or:
				flag = this.MatchOr(exp);
				break;
			case ExpressionCombinator.OrOr:
				flag = this.MatchOrOr(exp);
				break;
			case ExpressionCombinator.AndAnd:
				flag = this.MatchAndAnd(exp);
				break;
			case ExpressionCombinator.Juxtaposition:
				flag = this.MatchJuxtaposition(exp);
				break;
			case ExpressionCombinator.Group:
				flag = this.MatchGroup(exp);
				break;
			}
			bool flag2 = flag;
			if (flag2)
			{
				this.DropMark();
			}
			else
			{
				this.RestoreMark();
			}
			return flag;
		}

		private bool MatchOr(Expression exp)
		{
			bool flag = false;
			int num = 0;
			while (!flag && num < exp.subExpressions.Length)
			{
				flag = this.Match(exp.subExpressions[num]);
				num++;
			}
			return flag;
		}

		private bool MatchOrOr(Expression exp)
		{
			int num = this.MatchMany(exp);
			return num > 0;
		}

		private bool MatchAndAnd(Expression exp)
		{
			int num = this.MatchMany(exp);
			int num2 = exp.subExpressions.Length;
			return num == num2;
		}

		private unsafe int MatchMany(Expression exp)
		{
			int num = 0;
			int num2 = 0;
			int num3 = exp.subExpressions.Length;
			int* ptr = stackalloc int[num3];
			int num4 = 0;
			while (num4 < num3 && num + num2 < num3)
			{
				bool flag = false;
				for (int i = 0; i < num; i++)
				{
					bool flag2 = ptr[i] == num4;
					if (flag2)
					{
						flag = true;
						break;
					}
				}
				bool flag3 = false;
				bool flag4 = !flag;
				if (flag4)
				{
					flag3 = this.Match(exp.subExpressions[num4]);
				}
				bool flag5 = flag3;
				if (flag5)
				{
					bool flag6 = num2 == this.matchedVariableCount;
					if (flag6)
					{
						ptr[num] = num4;
						num++;
					}
					else
					{
						num2 = this.matchedVariableCount;
					}
					num4 = 0;
				}
				else
				{
					num4++;
				}
			}
			return num + num2;
		}

		private bool MatchJuxtaposition(Expression exp)
		{
			bool flag = true;
			int num = 0;
			while (flag && num < exp.subExpressions.Length)
			{
				flag = this.Match(exp.subExpressions[num]);
				num++;
			}
			return flag;
		}

		private bool MatchDataType(Expression exp)
		{
			bool result = false;
			bool hasCurrent = this.hasCurrent;
			if (hasCurrent)
			{
				switch (exp.dataType)
				{
				case DataType.Number:
					result = this.MatchNumber();
					break;
				case DataType.Integer:
					result = this.MatchInteger();
					break;
				case DataType.Length:
					result = this.MatchLength();
					break;
				case DataType.Percentage:
					result = this.MatchPercentage();
					break;
				case DataType.Color:
					result = this.MatchColor();
					break;
				case DataType.Resource:
					result = this.MatchResource();
					break;
				case DataType.Url:
					result = this.MatchUrl();
					break;
				}
			}
			return result;
		}
	}
}
