using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	internal class StyleSyntaxTokenizer
	{
		private List<StyleSyntaxToken> m_Tokens = new List<StyleSyntaxToken>();

		private int m_CurrentTokenIndex = -1;

		public StyleSyntaxToken current
		{
			get
			{
				bool flag = this.m_CurrentTokenIndex < 0 || this.m_CurrentTokenIndex >= this.m_Tokens.Count;
				StyleSyntaxToken result;
				if (flag)
				{
					result = new StyleSyntaxToken(StyleSyntaxTokenType.Unknown);
				}
				else
				{
					result = this.m_Tokens[this.m_CurrentTokenIndex];
				}
				return result;
			}
		}

		public StyleSyntaxToken MoveNext()
		{
			StyleSyntaxToken current = this.current;
			bool flag = current.type == StyleSyntaxTokenType.Unknown;
			StyleSyntaxToken result;
			if (flag)
			{
				result = current;
			}
			else
			{
				this.m_CurrentTokenIndex++;
				current = this.current;
				bool flag2 = this.m_CurrentTokenIndex == this.m_Tokens.Count;
				if (flag2)
				{
					this.m_CurrentTokenIndex = -1;
				}
				result = current;
			}
			return result;
		}

		public StyleSyntaxToken PeekNext()
		{
			int num = this.m_CurrentTokenIndex + 1;
			bool flag = this.m_CurrentTokenIndex < 0 || num >= this.m_Tokens.Count;
			StyleSyntaxToken result;
			if (flag)
			{
				result = new StyleSyntaxToken(StyleSyntaxTokenType.Unknown);
			}
			else
			{
				result = this.m_Tokens[num];
			}
			return result;
		}

		public void Tokenize(string syntax)
		{
			this.m_Tokens.Clear();
			this.m_CurrentTokenIndex = 0;
			syntax = syntax.Trim(new char[]
			{
				' '
			}).ToLower();
			int i = 0;
			while (i < syntax.Length)
			{
				char c = syntax[i];
				char c2 = c;
				char c3 = c2;
				if (c3 <= '?')
				{
					switch (c3)
					{
					case ' ':
						i = StyleSyntaxTokenizer.GlobCharacter(syntax, i, ' ');
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Space));
						break;
					case '!':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.ExclamationPoint));
						break;
					case '"':
					case '$':
					case '%':
					case '(':
					case ')':
						goto IL_2EA;
					case '#':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.HashMark));
						break;
					case '&':
					{
						bool flag = !StyleSyntaxTokenizer.IsNextCharacter(syntax, i, '&');
						if (flag)
						{
							string text = (i + 1 < syntax.Length) ? syntax[i + 1].ToString() : "EOF";
							Debug.LogAssertionFormat("Expected '&' got '{0}'", new object[]
							{
								text
							});
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Unknown));
						}
						else
						{
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.DoubleAmpersand));
							i++;
						}
						break;
					}
					case '\'':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.SingleQuote));
						break;
					case '*':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Asterisk));
						break;
					case '+':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Plus));
						break;
					case ',':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Comma));
						break;
					default:
						switch (c3)
						{
						case '<':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.LessThan));
							break;
						case '=':
							goto IL_2EA;
						case '>':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.GreaterThan));
							break;
						case '?':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.QuestionMark));
							break;
						default:
							goto IL_2EA;
						}
						break;
					}
				}
				else if (c3 != '[')
				{
					if (c3 != ']')
					{
						switch (c3)
						{
						case '{':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.OpenBrace));
							break;
						case '|':
						{
							bool flag2 = StyleSyntaxTokenizer.IsNextCharacter(syntax, i, '|');
							if (flag2)
							{
								this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.DoubleBar));
								i++;
							}
							else
							{
								this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.SingleBar));
							}
							break;
						}
						case '}':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.CloseBrace));
							break;
						default:
							goto IL_2EA;
						}
					}
					else
					{
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.CloseBracket));
					}
				}
				else
				{
					this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.OpenBracket));
				}
				IL_3C5:
				i++;
				continue;
				IL_2EA:
				bool flag3 = char.IsNumber(c);
				if (flag3)
				{
					int startIndex = i;
					int num = 1;
					while (StyleSyntaxTokenizer.IsNextNumber(syntax, i))
					{
						i++;
						num++;
					}
					string s = syntax.Substring(startIndex, num);
					int number = int.Parse(s);
					this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Number, number));
				}
				else
				{
					bool flag4 = char.IsLetter(c);
					if (flag4)
					{
						int startIndex2 = i;
						int num2 = 1;
						while (StyleSyntaxTokenizer.IsNextLetterOrDash(syntax, i))
						{
							i++;
							num2++;
						}
						string text2 = syntax.Substring(startIndex2, num2);
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.String, text2));
					}
					else
					{
						Debug.LogAssertionFormat("Expected letter or number got '{0}'", new object[]
						{
							c
						});
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Unknown));
					}
				}
				goto IL_3C5;
			}
			this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.End));
		}

		private static bool IsNextCharacter(string s, int index, char c)
		{
			return index + 1 < s.Length && s[index + 1] == c;
		}

		private static bool IsNextLetterOrDash(string s, int index)
		{
			return index + 1 < s.Length && (char.IsLetter(s[index + 1]) || s[index + 1] == '-');
		}

		private static bool IsNextNumber(string s, int index)
		{
			return index + 1 < s.Length && char.IsNumber(s[index + 1]);
		}

		private static int GlobCharacter(string s, int index, char c)
		{
			while (StyleSyntaxTokenizer.IsNextCharacter(s, index, c))
			{
				index++;
			}
			return index;
		}
	}
}
