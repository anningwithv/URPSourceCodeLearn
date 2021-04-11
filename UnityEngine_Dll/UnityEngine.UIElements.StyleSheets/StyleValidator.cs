using System;
using UnityEngine.UIElements.StyleSheets.Syntax;

namespace UnityEngine.UIElements.StyleSheets
{
	internal class StyleValidator
	{
		private StyleSyntaxParser m_SyntaxParser;

		private StyleMatcher m_StyleMatcher;

		public StyleValidator()
		{
			this.m_SyntaxParser = new StyleSyntaxParser();
			this.m_StyleMatcher = new StyleMatcher();
		}

		public StyleValidationResult ValidateProperty(string name, string value)
		{
			StyleValidationResult styleValidationResult = new StyleValidationResult
			{
				status = StyleValidationStatus.Ok
			};
			bool flag = name.StartsWith("--");
			StyleValidationResult result;
			if (flag)
			{
				result = styleValidationResult;
			}
			else
			{
				string text;
				bool flag2 = !StylePropertyCache.TryGetSyntax(name, out text);
				if (flag2)
				{
					string text2 = StylePropertyCache.FindClosestPropertyName(name);
					styleValidationResult.status = StyleValidationStatus.Error;
					styleValidationResult.message = "Unknown property '" + name + "'";
					bool flag3 = !string.IsNullOrEmpty(text2);
					if (flag3)
					{
						styleValidationResult.message = styleValidationResult.message + " (did you mean '" + text2 + "'?)";
					}
					result = styleValidationResult;
				}
				else
				{
					Expression expression = this.m_SyntaxParser.Parse(text);
					bool flag4 = expression == null;
					if (flag4)
					{
						styleValidationResult.status = StyleValidationStatus.Error;
						styleValidationResult.message = string.Concat(new string[]
						{
							"Invalid '",
							name,
							"' property syntax '",
							text,
							"'"
						});
						result = styleValidationResult;
					}
					else
					{
						MatchResult matchResult = this.m_StyleMatcher.Match(expression, value);
						bool flag5 = !matchResult.success;
						if (flag5)
						{
							styleValidationResult.errorValue = matchResult.errorValue;
							switch (matchResult.errorCode)
							{
							case MatchResultErrorCode.Syntax:
							{
								styleValidationResult.status = StyleValidationStatus.Error;
								bool flag6 = this.IsUnitMissing(text, value);
								if (flag6)
								{
									styleValidationResult.hint = "Property expects a unit. Did you forget to add px or %?";
								}
								else
								{
									bool flag7 = this.IsUnsupportedColor(text);
									if (flag7)
									{
										styleValidationResult.hint = "Unsupported color '" + value + "'.";
									}
								}
								styleValidationResult.message = string.Concat(new string[]
								{
									"Expected (",
									text,
									") but found '",
									matchResult.errorValue,
									"'"
								});
								break;
							}
							case MatchResultErrorCode.EmptyValue:
								styleValidationResult.status = StyleValidationStatus.Error;
								styleValidationResult.message = "Expected (" + text + ") but found empty value";
								break;
							case MatchResultErrorCode.ExpectedEndOfValue:
								styleValidationResult.status = StyleValidationStatus.Warning;
								styleValidationResult.message = "Expected end of value but found '" + matchResult.errorValue + "'";
								break;
							default:
								Debug.LogAssertion(string.Format("Unexpected error code '{0}'", matchResult.errorCode));
								break;
							}
						}
						result = styleValidationResult;
					}
				}
			}
			return result;
		}

		private bool IsUnitMissing(string propertySyntax, string propertyValue)
		{
			float num;
			return float.TryParse(propertyValue, out num) && (propertySyntax.Contains("<length>") || propertySyntax.Contains("<percentage>"));
		}

		private bool IsUnsupportedColor(string propertySyntax)
		{
			return propertySyntax.StartsWith("<color>");
		}
	}
}
