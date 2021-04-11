using System;
using System.Text.RegularExpressions;
using UnityEngine.UIElements.StyleSheets.Syntax;

namespace UnityEngine.UIElements.StyleSheets
{
	internal class StyleMatcher : BaseStyleMatcher
	{
		private StylePropertyValueParser m_Parser = new StylePropertyValueParser();

		private string[] m_PropertyParts;

		private static readonly Regex s_NumberRegex = new Regex("^[+-]?\\d+(?:\\.\\d+)?$", RegexOptions.Compiled);

		private static readonly Regex s_IntegerRegex = new Regex("^[+-]?\\d+$", RegexOptions.Compiled);

		private static readonly Regex s_ZeroRegex = new Regex("^0(?:\\.0+)?$", RegexOptions.Compiled);

		private static readonly Regex s_LengthRegex = new Regex("^[+-]?\\d+(?:\\.\\d+)?(?:px)$", RegexOptions.Compiled);

		private static readonly Regex s_PercentRegex = new Regex("^[+-]?\\d+(?:\\.\\d+)?(?:%)$", RegexOptions.Compiled);

		private static readonly Regex s_HexColorRegex = new Regex("^#[a-fA-F0-9]{3}(?:[a-fA-F0-9]{3})?$", RegexOptions.Compiled);

		private static readonly Regex s_RgbRegex = new Regex("^rgb\\(\\s*(\\d+)\\s*,\\s*(\\d+)\\s*,\\s*(\\d+)\\s*\\)$", RegexOptions.Compiled);

		private static readonly Regex s_RgbaRegex = new Regex("rgba\\(\\s*(\\d+)\\s*,\\s*(\\d+)\\s*,\\s*(\\d+)\\s*,\\s*([\\d.]+)\\s*\\)$", RegexOptions.Compiled);

		private static readonly Regex s_VarFunctionRegex = new Regex("^var\\(.+\\)$", RegexOptions.Compiled);

		private static readonly Regex s_ResourceRegex = new Regex("^resource\\((.+)\\)$", RegexOptions.Compiled);

		private static readonly Regex s_UrlRegex = new Regex("^url\\((.+)\\)$", RegexOptions.Compiled);

		private string current
		{
			get
			{
				return base.hasCurrent ? this.m_PropertyParts[this.m_CurrentIndex] : null;
			}
		}

		public override int valueCount
		{
			get
			{
				return this.m_PropertyParts.Length;
			}
		}

		public override bool isVariable
		{
			get
			{
				return base.hasCurrent && this.current.StartsWith("var(");
			}
		}

		private void Initialize(string propertyValue)
		{
			base.Initialize();
			this.m_PropertyParts = this.m_Parser.Parse(propertyValue);
		}

		public MatchResult Match(Expression exp, string propertyValue)
		{
			MatchResult matchResult = new MatchResult
			{
				errorCode = MatchResultErrorCode.None
			};
			bool flag = string.IsNullOrEmpty(propertyValue);
			MatchResult result;
			if (flag)
			{
				matchResult.errorCode = MatchResultErrorCode.EmptyValue;
				result = matchResult;
			}
			else
			{
				this.Initialize(propertyValue);
				string current = this.current;
				bool flag2 = current == "initial" || current.StartsWith("env(");
				bool flag3;
				if (flag2)
				{
					base.MoveNext();
					flag3 = true;
				}
				else
				{
					flag3 = base.Match(exp);
				}
				bool flag4 = !flag3;
				if (flag4)
				{
					matchResult.errorCode = MatchResultErrorCode.Syntax;
					matchResult.errorValue = this.current;
				}
				else
				{
					bool hasCurrent = base.hasCurrent;
					if (hasCurrent)
					{
						matchResult.errorCode = MatchResultErrorCode.ExpectedEndOfValue;
						matchResult.errorValue = this.current;
					}
				}
				result = matchResult;
			}
			return result;
		}

		protected override bool MatchKeyword(string keyword)
		{
			return this.current != null && keyword == this.current.ToLower();
		}

		protected override bool MatchNumber()
		{
			string current = this.current;
			Match match = StyleMatcher.s_NumberRegex.Match(current);
			return match.Success;
		}

		protected override bool MatchInteger()
		{
			string current = this.current;
			Match match = StyleMatcher.s_IntegerRegex.Match(current);
			return match.Success;
		}

		protected override bool MatchLength()
		{
			string current = this.current;
			Match match = StyleMatcher.s_LengthRegex.Match(current);
			bool success = match.Success;
			bool result;
			if (success)
			{
				result = true;
			}
			else
			{
				match = StyleMatcher.s_ZeroRegex.Match(current);
				result = match.Success;
			}
			return result;
		}

		protected override bool MatchPercentage()
		{
			string current = this.current;
			Match match = StyleMatcher.s_PercentRegex.Match(current);
			bool success = match.Success;
			bool result;
			if (success)
			{
				result = true;
			}
			else
			{
				match = StyleMatcher.s_ZeroRegex.Match(current);
				result = match.Success;
			}
			return result;
		}

		protected override bool MatchColor()
		{
			string current = this.current;
			Match match = StyleMatcher.s_HexColorRegex.Match(current);
			bool success = match.Success;
			bool result;
			if (success)
			{
				result = true;
			}
			else
			{
				match = StyleMatcher.s_RgbRegex.Match(current);
				bool success2 = match.Success;
				if (success2)
				{
					result = true;
				}
				else
				{
					match = StyleMatcher.s_RgbaRegex.Match(current);
					bool success3 = match.Success;
					if (success3)
					{
						result = true;
					}
					else
					{
						Color clear = Color.clear;
						bool flag = StyleSheetColor.TryGetColor(current, out clear);
						result = flag;
					}
				}
			}
			return result;
		}

		protected override bool MatchResource()
		{
			string current = this.current;
			Match match = StyleMatcher.s_ResourceRegex.Match(current);
			bool flag = !match.Success;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				string input = match.Groups[1].Value.Trim();
				match = StyleMatcher.s_VarFunctionRegex.Match(input);
				result = !match.Success;
			}
			return result;
		}

		protected override bool MatchUrl()
		{
			string current = this.current;
			Match match = StyleMatcher.s_UrlRegex.Match(current);
			bool flag = !match.Success;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				string input = match.Groups[1].Value.Trim();
				match = StyleMatcher.s_VarFunctionRegex.Match(input);
				result = !match.Success;
			}
			return result;
		}
	}
}
