using System;

namespace UnityEngine.TextCore
{
	internal static class TextGeneratorUtilities
	{
		public static readonly Vector2 largePositiveVector2 = new Vector2(2.14748365E+09f, 2.14748365E+09f);

		public static readonly Vector2 largeNegativeVector2 = new Vector2(-214748368f, -214748368f);

		public const float largePositiveFloat = 32767f;

		public const float largeNegativeFloat = -32767f;

		public static bool Approximately(float a, float b)
		{
			return b - 0.0001f < a && a < b + 0.0001f;
		}

		public static Color32 HexCharsToColor(char[] hexChars, int tagCount)
		{
			bool flag = tagCount == 4;
			Color32 result;
			if (flag)
			{
				byte r = (byte)(TextGeneratorUtilities.HexToInt(hexChars[1]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[1]));
				byte g = (byte)(TextGeneratorUtilities.HexToInt(hexChars[2]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[2]));
				byte b = (byte)(TextGeneratorUtilities.HexToInt(hexChars[3]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[3]));
				result = new Color32(r, g, b, 255);
			}
			else
			{
				bool flag2 = tagCount == 5;
				if (flag2)
				{
					byte r2 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[1]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[1]));
					byte g2 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[2]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[2]));
					byte b2 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[3]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[3]));
					byte a = (byte)(TextGeneratorUtilities.HexToInt(hexChars[4]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[4]));
					result = new Color32(r2, g2, b2, a);
				}
				else
				{
					bool flag3 = tagCount == 7;
					if (flag3)
					{
						byte r3 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[1]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[2]));
						byte g3 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[3]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[4]));
						byte b3 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[5]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[6]));
						result = new Color32(r3, g3, b3, 255);
					}
					else
					{
						bool flag4 = tagCount == 9;
						if (flag4)
						{
							byte r4 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[1]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[2]));
							byte g4 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[3]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[4]));
							byte b4 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[5]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[6]));
							byte a2 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[7]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[8]));
							result = new Color32(r4, g4, b4, a2);
						}
						else
						{
							bool flag5 = tagCount == 10;
							if (flag5)
							{
								byte r5 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[7]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[7]));
								byte g5 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[8]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[8]));
								byte b5 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[9]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[9]));
								result = new Color32(r5, g5, b5, 255);
							}
							else
							{
								bool flag6 = tagCount == 11;
								if (flag6)
								{
									byte r6 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[7]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[7]));
									byte g6 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[8]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[8]));
									byte b6 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[9]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[9]));
									byte a3 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[10]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[10]));
									result = new Color32(r6, g6, b6, a3);
								}
								else
								{
									bool flag7 = tagCount == 13;
									if (flag7)
									{
										byte r7 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[7]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[8]));
										byte g7 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[9]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[10]));
										byte b7 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[11]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[12]));
										result = new Color32(r7, g7, b7, 255);
									}
									else
									{
										bool flag8 = tagCount == 15;
										if (flag8)
										{
											byte r8 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[7]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[8]));
											byte g8 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[9]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[10]));
											byte b8 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[11]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[12]));
											byte a4 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[13]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[14]));
											result = new Color32(r8, g8, b8, a4);
										}
										else
										{
											result = new Color32(255, 255, 255, 255);
										}
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		public static Color32 HexCharsToColor(char[] hexChars, int startIndex, int length)
		{
			bool flag = length == 7;
			Color32 result;
			if (flag)
			{
				byte r = (byte)(TextGeneratorUtilities.HexToInt(hexChars[startIndex + 1]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[startIndex + 2]));
				byte g = (byte)(TextGeneratorUtilities.HexToInt(hexChars[startIndex + 3]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[startIndex + 4]));
				byte b = (byte)(TextGeneratorUtilities.HexToInt(hexChars[startIndex + 5]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[startIndex + 6]));
				result = new Color32(r, g, b, 255);
			}
			else
			{
				bool flag2 = length == 9;
				if (flag2)
				{
					byte r2 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[startIndex + 1]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[startIndex + 2]));
					byte g2 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[startIndex + 3]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[startIndex + 4]));
					byte b2 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[startIndex + 5]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[startIndex + 6]));
					byte a = (byte)(TextGeneratorUtilities.HexToInt(hexChars[startIndex + 7]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[startIndex + 8]));
					result = new Color32(r2, g2, b2, a);
				}
				else
				{
					result = new Color32(255, 255, 255, 255);
				}
			}
			return result;
		}

		public static int HexToInt(char hex)
		{
			int result;
			switch (hex)
			{
			case '0':
				result = 0;
				return result;
			case '1':
				result = 1;
				return result;
			case '2':
				result = 2;
				return result;
			case '3':
				result = 3;
				return result;
			case '4':
				result = 4;
				return result;
			case '5':
				result = 5;
				return result;
			case '6':
				result = 6;
				return result;
			case '7':
				result = 7;
				return result;
			case '8':
				result = 8;
				return result;
			case '9':
				result = 9;
				return result;
			case ':':
			case ';':
			case '<':
			case '=':
			case '>':
			case '?':
			case '@':
				break;
			case 'A':
				result = 10;
				return result;
			case 'B':
				result = 11;
				return result;
			case 'C':
				result = 12;
				return result;
			case 'D':
				result = 13;
				return result;
			case 'E':
				result = 14;
				return result;
			case 'F':
				result = 15;
				return result;
			default:
				switch (hex)
				{
				case 'a':
					result = 10;
					return result;
				case 'b':
					result = 11;
					return result;
				case 'c':
					result = 12;
					return result;
				case 'd':
					result = 13;
					return result;
				case 'e':
					result = 14;
					return result;
				case 'f':
					result = 15;
					return result;
				}
				break;
			}
			result = 15;
			return result;
		}

		public static float ConvertToFloat(char[] chars, int startIndex, int length)
		{
			int num;
			return TextGeneratorUtilities.ConvertToFloat(chars, startIndex, length, out num);
		}

		public static float ConvertToFloat(char[] chars, int startIndex, int length, out int lastIndex)
		{
			bool flag = startIndex == 0;
			float result;
			if (flag)
			{
				lastIndex = 0;
				result = -32767f;
			}
			else
			{
				int num = startIndex + length;
				bool flag2 = true;
				float num2 = 0f;
				int num3 = 1;
				bool flag3 = chars[startIndex] == '+';
				if (flag3)
				{
					num3 = 1;
					startIndex++;
				}
				else
				{
					bool flag4 = chars[startIndex] == '-';
					if (flag4)
					{
						num3 = -1;
						startIndex++;
					}
				}
				float num4 = 0f;
				int i = startIndex;
				while (i < num)
				{
					uint num5 = (uint)chars[i];
					bool flag5 = (num5 >= 48u && num5 <= 57u) || num5 == 46u;
					if (flag5)
					{
						bool flag6 = num5 == 46u;
						if (flag6)
						{
							flag2 = false;
							num2 = 0.1f;
						}
						else
						{
							bool flag7 = flag2;
							if (flag7)
							{
								num4 = num4 * 10f + (float)((ulong)(num5 - 48u) * (ulong)((long)num3));
							}
							else
							{
								num4 += (num5 - 48u) * num2 * (float)num3;
								num2 *= 0.1f;
							}
						}
					}
					else
					{
						bool flag8 = num5 == 44u;
						if (flag8)
						{
							bool flag9 = i + 1 < num && chars[i + 1] == ' ';
							if (flag9)
							{
								lastIndex = i + 1;
							}
							else
							{
								lastIndex = i;
							}
							result = num4;
							return result;
						}
					}
					IL_116:
					i++;
					continue;
					goto IL_116;
				}
				lastIndex = num;
				result = num4;
			}
			return result;
		}

		public static Vector2 PackUV(float x, float y, float scale)
		{
			Vector2 vector;
			vector.x = (float)((int)(x * 511f));
			vector.y = (float)((int)(y * 511f));
			vector.x = vector.x * 4096f + vector.y;
			vector.y = scale;
			return vector;
		}

		public static void StringToCharArray(string sourceText, ref int[] charBuffer, ref RichTextTagStack<int> styleStack, TextGenerationSettings generationSettings)
		{
			bool flag = sourceText == null;
			if (flag)
			{
				charBuffer[0] = 0;
			}
			else
			{
				bool flag2 = charBuffer == null;
				if (flag2)
				{
					charBuffer = new int[8];
				}
				styleStack.SetDefault(0);
				int num = 0;
				int i = 0;
				while (i < sourceText.Length)
				{
					bool flag3 = sourceText[i] == '\\' && sourceText.Length > i + 1;
					if (flag3)
					{
						int num2 = (int)sourceText[i + 1];
						int num3 = num2;
						if (num3 <= 92)
						{
							if (num3 != 85)
							{
								if (num3 == 92)
								{
									bool flag4 = !generationSettings.parseControlCharacters;
									if (!flag4)
									{
										bool flag5 = sourceText.Length <= i + 2;
										if (!flag5)
										{
											bool flag6 = num + 2 > charBuffer.Length;
											if (flag6)
											{
												TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
											}
											charBuffer[num] = (int)sourceText[i + 1];
											charBuffer[num + 1] = (int)sourceText[i + 2];
											i += 2;
											num += 2;
											goto IL_381;
										}
									}
								}
							}
							else
							{
								bool flag7 = sourceText.Length > i + 9;
								if (flag7)
								{
									bool flag8 = num == charBuffer.Length;
									if (flag8)
									{
										TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
									}
									charBuffer[num] = TextGeneratorUtilities.GetUtf32(sourceText, i + 2);
									i += 9;
									num++;
									goto IL_381;
								}
							}
						}
						else if (num3 != 110)
						{
							switch (num3)
							{
							case 114:
							{
								bool flag9 = !generationSettings.parseControlCharacters;
								if (!flag9)
								{
									bool flag10 = num == charBuffer.Length;
									if (flag10)
									{
										TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
									}
									charBuffer[num] = 13;
									i++;
									num++;
									goto IL_381;
								}
								break;
							}
							case 116:
							{
								bool flag11 = !generationSettings.parseControlCharacters;
								if (!flag11)
								{
									bool flag12 = num == charBuffer.Length;
									if (flag12)
									{
										TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
									}
									charBuffer[num] = 9;
									i++;
									num++;
									goto IL_381;
								}
								break;
							}
							case 117:
							{
								bool flag13 = sourceText.Length > i + 5;
								if (flag13)
								{
									bool flag14 = num == charBuffer.Length;
									if (flag14)
									{
										TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
									}
									charBuffer[num] = (int)((ushort)TextGeneratorUtilities.GetUtf16(sourceText, i + 2));
									i += 5;
									num++;
									goto IL_381;
								}
								break;
							}
							}
						}
						else
						{
							bool flag15 = !generationSettings.parseControlCharacters;
							if (!flag15)
							{
								bool flag16 = num == charBuffer.Length;
								if (flag16)
								{
									TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
								}
								charBuffer[num] = 10;
								i++;
								num++;
								goto IL_381;
							}
						}
						goto IL_251;
					}
					goto IL_251;
					IL_381:
					i++;
					continue;
					IL_251:
					bool flag17 = char.IsHighSurrogate(sourceText[i]) && char.IsLowSurrogate(sourceText[i + 1]);
					if (flag17)
					{
						bool flag18 = num == charBuffer.Length;
						if (flag18)
						{
							TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
						}
						charBuffer[num] = char.ConvertToUtf32(sourceText[i], sourceText[i + 1]);
						i++;
						num++;
						goto IL_381;
					}
					bool flag19 = sourceText[i] == '<' && generationSettings.richText;
					if (flag19)
					{
						bool flag20 = TextGeneratorUtilities.IsTagName(ref sourceText, "<BR>", i);
						if (flag20)
						{
							bool flag21 = num == charBuffer.Length;
							if (flag21)
							{
								TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
							}
							charBuffer[num] = 10;
							num++;
							i += 3;
							goto IL_381;
						}
						bool flag22 = TextGeneratorUtilities.IsTagName(ref sourceText, "<STYLE=", i);
						if (flag22)
						{
							int num4;
							bool flag23 = TextGeneratorUtilities.ReplaceOpeningStyleTag(ref sourceText, i, out num4, ref charBuffer, ref num, ref styleStack);
							if (flag23)
							{
								i = num4;
								goto IL_381;
							}
						}
						else
						{
							bool flag24 = TextGeneratorUtilities.IsTagName(ref sourceText, "</STYLE>", i);
							if (flag24)
							{
								TextGeneratorUtilities.ReplaceClosingStyleTag(ref charBuffer, ref num, ref styleStack);
								i += 7;
								goto IL_381;
							}
						}
					}
					bool flag25 = num == charBuffer.Length;
					if (flag25)
					{
						TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
					}
					charBuffer[num] = (int)sourceText[i];
					num++;
					goto IL_381;
				}
				bool flag26 = num == charBuffer.Length;
				if (flag26)
				{
					TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
				}
				charBuffer[num] = 0;
			}
		}

		private static void ResizeInternalArray<T>(ref T[] array)
		{
			int newSize = Mathf.NextPowerOfTwo(array.Length + 1);
			Array.Resize<T>(ref array, newSize);
		}

		internal static void ResizeArray<T>(T[] array)
		{
			int num = array.Length * 2;
			bool flag = num == 0;
			if (flag)
			{
				num = 8;
			}
			Array.Resize<T>(ref array, num);
		}

		private static bool IsTagName(ref string text, string tag, int index)
		{
			bool flag = text.Length < index + tag.Length;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < tag.Length; i++)
				{
					bool flag2 = TextUtilities.ToUpperFast(text[index + i]) != tag[i];
					if (flag2)
					{
						result = false;
						return result;
					}
				}
				result = true;
			}
			return result;
		}

		private static bool IsTagName(ref int[] text, string tag, int index)
		{
			bool flag = text.Length < index + tag.Length;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < tag.Length; i++)
				{
					bool flag2 = TextUtilities.ToUpperFast((char)text[index + i]) != tag[i];
					if (flag2)
					{
						result = false;
						return result;
					}
				}
				result = true;
			}
			return result;
		}

		private static bool ReplaceOpeningStyleTag(ref int[] sourceText, int srcIndex, out int srcOffset, ref int[] charBuffer, ref int writeIndex, ref RichTextTagStack<int> styleStack)
		{
			int tagHashCode = TextGeneratorUtilities.GetTagHashCode(ref sourceText, srcIndex + 7, out srcOffset);
			TextStyle style = TextStyleSheet.GetStyle(tagHashCode);
			bool flag = style == null || srcOffset == 0;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				styleStack.Add(style.hashCode);
				int num = style.styleOpeningTagArray.Length;
				int[] styleOpeningTagArray = style.styleOpeningTagArray;
				int i = 0;
				while (i < num)
				{
					int num2 = styleOpeningTagArray[i];
					bool flag2 = num2 == 60;
					if (!flag2)
					{
						goto IL_10D;
					}
					bool flag3 = TextGeneratorUtilities.IsTagName(ref styleOpeningTagArray, "<BR>", i);
					if (!flag3)
					{
						bool flag4 = TextGeneratorUtilities.IsTagName(ref styleOpeningTagArray, "<STYLE=", i);
						if (flag4)
						{
							int num3;
							bool flag5 = TextGeneratorUtilities.ReplaceOpeningStyleTag(ref styleOpeningTagArray, i, out num3, ref charBuffer, ref writeIndex, ref styleStack);
							if (flag5)
							{
								i = num3;
								goto IL_134;
							}
						}
						else
						{
							bool flag6 = TextGeneratorUtilities.IsTagName(ref styleOpeningTagArray, "</STYLE>", i);
							if (flag6)
							{
								TextGeneratorUtilities.ReplaceClosingStyleTag(ref charBuffer, ref writeIndex, ref styleStack);
								i += 7;
								goto IL_134;
							}
						}
						goto IL_10D;
					}
					bool flag7 = writeIndex == charBuffer.Length;
					if (flag7)
					{
						TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
					}
					charBuffer[writeIndex] = 10;
					writeIndex++;
					i += 3;
					IL_134:
					i++;
					continue;
					IL_10D:
					bool flag8 = writeIndex == charBuffer.Length;
					if (flag8)
					{
						TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
					}
					charBuffer[writeIndex] = num2;
					writeIndex++;
					goto IL_134;
				}
				result = true;
			}
			return result;
		}

		private static bool ReplaceOpeningStyleTag(ref string sourceText, int srcIndex, out int srcOffset, ref int[] charBuffer, ref int writeIndex, ref RichTextTagStack<int> styleStack)
		{
			int tagHashCode = TextGeneratorUtilities.GetTagHashCode(ref sourceText, srcIndex + 7, out srcOffset);
			TextStyle style = TextStyleSheet.GetStyle(tagHashCode);
			bool flag = style == null || srcOffset == 0;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				styleStack.Add(style.hashCode);
				int num = style.styleOpeningTagArray.Length;
				int[] styleOpeningTagArray = style.styleOpeningTagArray;
				int i = 0;
				while (i < num)
				{
					int num2 = styleOpeningTagArray[i];
					bool flag2 = num2 == 60;
					if (!flag2)
					{
						goto IL_10D;
					}
					bool flag3 = TextGeneratorUtilities.IsTagName(ref styleOpeningTagArray, "<BR>", i);
					if (!flag3)
					{
						bool flag4 = TextGeneratorUtilities.IsTagName(ref styleOpeningTagArray, "<STYLE=", i);
						if (flag4)
						{
							int num3;
							bool flag5 = TextGeneratorUtilities.ReplaceOpeningStyleTag(ref styleOpeningTagArray, i, out num3, ref charBuffer, ref writeIndex, ref styleStack);
							if (flag5)
							{
								i = num3;
								goto IL_134;
							}
						}
						else
						{
							bool flag6 = TextGeneratorUtilities.IsTagName(ref styleOpeningTagArray, "</STYLE>", i);
							if (flag6)
							{
								TextGeneratorUtilities.ReplaceClosingStyleTag(ref charBuffer, ref writeIndex, ref styleStack);
								i += 7;
								goto IL_134;
							}
						}
						goto IL_10D;
					}
					bool flag7 = writeIndex == charBuffer.Length;
					if (flag7)
					{
						TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
					}
					charBuffer[writeIndex] = 10;
					writeIndex++;
					i += 3;
					IL_134:
					i++;
					continue;
					IL_10D:
					bool flag8 = writeIndex == charBuffer.Length;
					if (flag8)
					{
						TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
					}
					charBuffer[writeIndex] = num2;
					writeIndex++;
					goto IL_134;
				}
				result = true;
			}
			return result;
		}

		private static void ReplaceClosingStyleTag(ref int[] charBuffer, ref int writeIndex, ref RichTextTagStack<int> styleStack)
		{
			int hashCode = styleStack.CurrentItem();
			TextStyle style = TextStyleSheet.GetStyle(hashCode);
			styleStack.Remove();
			bool flag = style == null;
			if (!flag)
			{
				int num = style.styleClosingTagArray.Length;
				int[] styleClosingTagArray = style.styleClosingTagArray;
				int i = 0;
				while (i < num)
				{
					int num2 = styleClosingTagArray[i];
					bool flag2 = num2 == 60;
					if (!flag2)
					{
						goto IL_ED;
					}
					bool flag3 = TextGeneratorUtilities.IsTagName(ref styleClosingTagArray, "<BR>", i);
					if (!flag3)
					{
						bool flag4 = TextGeneratorUtilities.IsTagName(ref styleClosingTagArray, "<STYLE=", i);
						if (flag4)
						{
							int num3;
							bool flag5 = TextGeneratorUtilities.ReplaceOpeningStyleTag(ref styleClosingTagArray, i, out num3, ref charBuffer, ref writeIndex, ref styleStack);
							if (flag5)
							{
								i = num3;
								goto IL_110;
							}
						}
						else
						{
							bool flag6 = TextGeneratorUtilities.IsTagName(ref styleClosingTagArray, "</STYLE>", i);
							if (flag6)
							{
								TextGeneratorUtilities.ReplaceClosingStyleTag(ref charBuffer, ref writeIndex, ref styleStack);
								i += 7;
								goto IL_110;
							}
						}
						goto IL_ED;
					}
					bool flag7 = writeIndex == charBuffer.Length;
					if (flag7)
					{
						TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
					}
					charBuffer[writeIndex] = 10;
					writeIndex++;
					i += 3;
					IL_110:
					i++;
					continue;
					IL_ED:
					bool flag8 = writeIndex == charBuffer.Length;
					if (flag8)
					{
						TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
					}
					charBuffer[writeIndex] = num2;
					writeIndex++;
					goto IL_110;
				}
			}
		}

		private static int GetUtf32(string text, int i)
		{
			int num = 0;
			num += TextGeneratorUtilities.HexToInt(text[i]) << 30;
			num += TextGeneratorUtilities.HexToInt(text[i + 1]) << 24;
			num += TextGeneratorUtilities.HexToInt(text[i + 2]) << 20;
			num += TextGeneratorUtilities.HexToInt(text[i + 3]) << 16;
			num += TextGeneratorUtilities.HexToInt(text[i + 4]) << 12;
			num += TextGeneratorUtilities.HexToInt(text[i + 5]) << 8;
			num += TextGeneratorUtilities.HexToInt(text[i + 6]) << 4;
			return num + TextGeneratorUtilities.HexToInt(text[i + 7]);
		}

		private static int GetUtf16(string text, int i)
		{
			int num = 0;
			num += TextGeneratorUtilities.HexToInt(text[i]) << 12;
			num += TextGeneratorUtilities.HexToInt(text[i + 1]) << 8;
			num += TextGeneratorUtilities.HexToInt(text[i + 2]) << 4;
			return num + TextGeneratorUtilities.HexToInt(text[i + 3]);
		}

		private static int GetTagHashCode(ref int[] text, int index, out int closeIndex)
		{
			int num = 0;
			closeIndex = 0;
			for (int i = index; i < text.Length; i++)
			{
				bool flag = text[i] == 34;
				if (!flag)
				{
					bool flag2 = text[i] == 62;
					if (flag2)
					{
						closeIndex = i;
						break;
					}
					num = ((num << 5) + num ^ (int)TextUtilities.ToUpperASCIIFast((uint)((ushort)text[i])));
				}
			}
			return num;
		}

		private static int GetTagHashCode(ref string text, int index, out int closeIndex)
		{
			int num = 0;
			closeIndex = 0;
			for (int i = index; i < text.Length; i++)
			{
				bool flag = text[i] == '"';
				if (!flag)
				{
					bool flag2 = text[i] == '>';
					if (flag2)
					{
						closeIndex = i;
						break;
					}
					num = ((num << 5) + num ^ (int)TextUtilities.ToUpperASCIIFast((uint)text[i]));
				}
			}
			return num;
		}

		public static void FillCharacterVertexBuffers(int i, TextGenerationSettings generationSettings, TextInfo textInfo)
		{
			int materialReferenceIndex = textInfo.textElementInfo[i].materialReferenceIndex;
			int vertexCount = textInfo.meshInfo[materialReferenceIndex].vertexCount;
			TextElementInfo[] textElementInfo = textInfo.textElementInfo;
			textInfo.textElementInfo[i].vertexIndex = vertexCount;
			bool inverseYAxis = generationSettings.inverseYAxis;
			if (inverseYAxis)
			{
				Vector3 b;
				b.x = 0f;
				b.y = generationSettings.screenRect.y + generationSettings.screenRect.height;
				b.z = 0f;
				Vector3 position = textElementInfo[i].vertexBottomLeft.position;
				position.y *= -1f;
				textInfo.meshInfo[materialReferenceIndex].vertices[vertexCount] = position + b;
				position = textElementInfo[i].vertexTopLeft.position;
				position.y *= -1f;
				textInfo.meshInfo[materialReferenceIndex].vertices[1 + vertexCount] = position + b;
				position = textElementInfo[i].vertexTopRight.position;
				position.y *= -1f;
				textInfo.meshInfo[materialReferenceIndex].vertices[2 + vertexCount] = position + b;
				position = textElementInfo[i].vertexBottomRight.position;
				position.y *= -1f;
				textInfo.meshInfo[materialReferenceIndex].vertices[3 + vertexCount] = position + b;
			}
			else
			{
				textInfo.meshInfo[materialReferenceIndex].vertices[vertexCount] = textElementInfo[i].vertexBottomLeft.position;
				textInfo.meshInfo[materialReferenceIndex].vertices[1 + vertexCount] = textElementInfo[i].vertexTopLeft.position;
				textInfo.meshInfo[materialReferenceIndex].vertices[2 + vertexCount] = textElementInfo[i].vertexTopRight.position;
				textInfo.meshInfo[materialReferenceIndex].vertices[3 + vertexCount] = textElementInfo[i].vertexBottomRight.position;
			}
			textInfo.meshInfo[materialReferenceIndex].uvs0[vertexCount] = textElementInfo[i].vertexBottomLeft.uv;
			textInfo.meshInfo[materialReferenceIndex].uvs0[1 + vertexCount] = textElementInfo[i].vertexTopLeft.uv;
			textInfo.meshInfo[materialReferenceIndex].uvs0[2 + vertexCount] = textElementInfo[i].vertexTopRight.uv;
			textInfo.meshInfo[materialReferenceIndex].uvs0[3 + vertexCount] = textElementInfo[i].vertexBottomRight.uv;
			textInfo.meshInfo[materialReferenceIndex].uvs2[vertexCount] = textElementInfo[i].vertexBottomLeft.uv2;
			textInfo.meshInfo[materialReferenceIndex].uvs2[1 + vertexCount] = textElementInfo[i].vertexTopLeft.uv2;
			textInfo.meshInfo[materialReferenceIndex].uvs2[2 + vertexCount] = textElementInfo[i].vertexTopRight.uv2;
			textInfo.meshInfo[materialReferenceIndex].uvs2[3 + vertexCount] = textElementInfo[i].vertexBottomRight.uv2;
			textInfo.meshInfo[materialReferenceIndex].colors32[vertexCount] = textElementInfo[i].vertexBottomLeft.color;
			textInfo.meshInfo[materialReferenceIndex].colors32[1 + vertexCount] = textElementInfo[i].vertexTopLeft.color;
			textInfo.meshInfo[materialReferenceIndex].colors32[2 + vertexCount] = textElementInfo[i].vertexTopRight.color;
			textInfo.meshInfo[materialReferenceIndex].colors32[3 + vertexCount] = textElementInfo[i].vertexBottomRight.color;
			textInfo.meshInfo[materialReferenceIndex].vertexCount = vertexCount + 4;
		}

		public static void FillSpriteVertexBuffers(int i, TextGenerationSettings generationSettings, TextInfo textInfo)
		{
			int materialReferenceIndex = textInfo.textElementInfo[i].materialReferenceIndex;
			int vertexCount = textInfo.meshInfo[materialReferenceIndex].vertexCount;
			TextElementInfo[] textElementInfo = textInfo.textElementInfo;
			textInfo.textElementInfo[i].vertexIndex = vertexCount;
			bool inverseYAxis = generationSettings.inverseYAxis;
			if (inverseYAxis)
			{
				Vector3 b;
				b.x = 0f;
				b.y = generationSettings.screenRect.y + generationSettings.screenRect.height;
				b.z = 0f;
				Vector3 position = textElementInfo[i].vertexBottomLeft.position;
				position.y *= -1f;
				textInfo.meshInfo[materialReferenceIndex].vertices[vertexCount] = position + b;
				position = textElementInfo[i].vertexTopLeft.position;
				position.y *= -1f;
				textInfo.meshInfo[materialReferenceIndex].vertices[1 + vertexCount] = position + b;
				position = textElementInfo[i].vertexTopRight.position;
				position.y *= -1f;
				textInfo.meshInfo[materialReferenceIndex].vertices[2 + vertexCount] = position + b;
				position = textElementInfo[i].vertexBottomRight.position;
				position.y *= -1f;
				textInfo.meshInfo[materialReferenceIndex].vertices[3 + vertexCount] = position + b;
			}
			else
			{
				textInfo.meshInfo[materialReferenceIndex].vertices[vertexCount] = textElementInfo[i].vertexBottomLeft.position;
				textInfo.meshInfo[materialReferenceIndex].vertices[1 + vertexCount] = textElementInfo[i].vertexTopLeft.position;
				textInfo.meshInfo[materialReferenceIndex].vertices[2 + vertexCount] = textElementInfo[i].vertexTopRight.position;
				textInfo.meshInfo[materialReferenceIndex].vertices[3 + vertexCount] = textElementInfo[i].vertexBottomRight.position;
			}
			textInfo.meshInfo[materialReferenceIndex].uvs0[vertexCount] = textElementInfo[i].vertexBottomLeft.uv;
			textInfo.meshInfo[materialReferenceIndex].uvs0[1 + vertexCount] = textElementInfo[i].vertexTopLeft.uv;
			textInfo.meshInfo[materialReferenceIndex].uvs0[2 + vertexCount] = textElementInfo[i].vertexTopRight.uv;
			textInfo.meshInfo[materialReferenceIndex].uvs0[3 + vertexCount] = textElementInfo[i].vertexBottomRight.uv;
			textInfo.meshInfo[materialReferenceIndex].uvs2[vertexCount] = textElementInfo[i].vertexBottomLeft.uv2;
			textInfo.meshInfo[materialReferenceIndex].uvs2[1 + vertexCount] = textElementInfo[i].vertexTopLeft.uv2;
			textInfo.meshInfo[materialReferenceIndex].uvs2[2 + vertexCount] = textElementInfo[i].vertexTopRight.uv2;
			textInfo.meshInfo[materialReferenceIndex].uvs2[3 + vertexCount] = textElementInfo[i].vertexBottomRight.uv2;
			textInfo.meshInfo[materialReferenceIndex].colors32[vertexCount] = textElementInfo[i].vertexBottomLeft.color;
			textInfo.meshInfo[materialReferenceIndex].colors32[1 + vertexCount] = textElementInfo[i].vertexTopLeft.color;
			textInfo.meshInfo[materialReferenceIndex].colors32[2 + vertexCount] = textElementInfo[i].vertexTopRight.color;
			textInfo.meshInfo[materialReferenceIndex].colors32[3 + vertexCount] = textElementInfo[i].vertexBottomRight.color;
			textInfo.meshInfo[materialReferenceIndex].vertexCount = vertexCount + 4;
		}

		public static void AdjustLineOffset(int startIndex, int endIndex, float offset, TextInfo textInfo)
		{
			Vector3 vector = new Vector3(0f, offset, 0f);
			for (int i = startIndex; i <= endIndex; i++)
			{
				TextElementInfo[] expr_2C_cp_0_cp_0 = textInfo.textElementInfo;
				int expr_2C_cp_0_cp_1 = i;
				expr_2C_cp_0_cp_0[expr_2C_cp_0_cp_1].bottomLeft = expr_2C_cp_0_cp_0[expr_2C_cp_0_cp_1].bottomLeft - vector;
				TextElementInfo[] expr_4E_cp_0_cp_0 = textInfo.textElementInfo;
				int expr_4E_cp_0_cp_1 = i;
				expr_4E_cp_0_cp_0[expr_4E_cp_0_cp_1].topLeft = expr_4E_cp_0_cp_0[expr_4E_cp_0_cp_1].topLeft - vector;
				TextElementInfo[] expr_70_cp_0_cp_0 = textInfo.textElementInfo;
				int expr_70_cp_0_cp_1 = i;
				expr_70_cp_0_cp_0[expr_70_cp_0_cp_1].topRight = expr_70_cp_0_cp_0[expr_70_cp_0_cp_1].topRight - vector;
				TextElementInfo[] expr_92_cp_0_cp_0 = textInfo.textElementInfo;
				int expr_92_cp_0_cp_1 = i;
				expr_92_cp_0_cp_0[expr_92_cp_0_cp_1].bottomRight = expr_92_cp_0_cp_0[expr_92_cp_0_cp_1].bottomRight - vector;
				TextElementInfo[] expr_B4_cp_0_cp_0 = textInfo.textElementInfo;
				int expr_B4_cp_0_cp_1 = i;
				expr_B4_cp_0_cp_0[expr_B4_cp_0_cp_1].ascender = expr_B4_cp_0_cp_0[expr_B4_cp_0_cp_1].ascender - vector.y;
				TextElementInfo[] expr_CF_cp_0_cp_0 = textInfo.textElementInfo;
				int expr_CF_cp_0_cp_1 = i;
				expr_CF_cp_0_cp_0[expr_CF_cp_0_cp_1].baseLine = expr_CF_cp_0_cp_0[expr_CF_cp_0_cp_1].baseLine - vector.y;
				TextElementInfo[] expr_EA_cp_0_cp_0 = textInfo.textElementInfo;
				int expr_EA_cp_0_cp_1 = i;
				expr_EA_cp_0_cp_0[expr_EA_cp_0_cp_1].descender = expr_EA_cp_0_cp_0[expr_EA_cp_0_cp_1].descender - vector.y;
				bool isVisible = textInfo.textElementInfo[i].isVisible;
				if (isVisible)
				{
					TextElementInfo[] expr_123_cp_0_cp_0_cp_0 = textInfo.textElementInfo;
					int expr_123_cp_0_cp_0_cp_1 = i;
					expr_123_cp_0_cp_0_cp_0[expr_123_cp_0_cp_0_cp_1].vertexBottomLeft.position = expr_123_cp_0_cp_0_cp_0[expr_123_cp_0_cp_0_cp_1].vertexBottomLeft.position - vector;
					TextElementInfo[] expr_14A_cp_0_cp_0_cp_0 = textInfo.textElementInfo;
					int expr_14A_cp_0_cp_0_cp_1 = i;
					expr_14A_cp_0_cp_0_cp_0[expr_14A_cp_0_cp_0_cp_1].vertexTopLeft.position = expr_14A_cp_0_cp_0_cp_0[expr_14A_cp_0_cp_0_cp_1].vertexTopLeft.position - vector;
					TextElementInfo[] expr_171_cp_0_cp_0_cp_0 = textInfo.textElementInfo;
					int expr_171_cp_0_cp_0_cp_1 = i;
					expr_171_cp_0_cp_0_cp_0[expr_171_cp_0_cp_0_cp_1].vertexTopRight.position = expr_171_cp_0_cp_0_cp_0[expr_171_cp_0_cp_0_cp_1].vertexTopRight.position - vector;
					TextElementInfo[] expr_198_cp_0_cp_0_cp_0 = textInfo.textElementInfo;
					int expr_198_cp_0_cp_0_cp_1 = i;
					expr_198_cp_0_cp_0_cp_0[expr_198_cp_0_cp_0_cp_1].vertexBottomRight.position = expr_198_cp_0_cp_0_cp_0[expr_198_cp_0_cp_0_cp_1].vertexBottomRight.position - vector;
				}
			}
		}

		public static void ResizeLineExtents(int size, TextInfo textInfo)
		{
			size = ((size > 1024) ? (size + 256) : Mathf.NextPowerOfTwo(size + 1));
			LineInfo[] array = new LineInfo[size];
			for (int i = 0; i < size; i++)
			{
				bool flag = i < textInfo.lineInfo.Length;
				if (flag)
				{
					array[i] = textInfo.lineInfo[i];
				}
				else
				{
					array[i].lineExtents.min = TextGeneratorUtilities.largePositiveVector2;
					array[i].lineExtents.max = TextGeneratorUtilities.largeNegativeVector2;
					array[i].ascender = -32767f;
					array[i].descender = 32767f;
				}
			}
			textInfo.lineInfo = array;
		}

		public static FontStyles LegacyStyleToNewStyle(FontStyle fontStyle)
		{
			FontStyles result;
			switch (fontStyle)
			{
			case FontStyle.Bold:
				result = FontStyles.Bold;
				break;
			case FontStyle.Italic:
				result = FontStyles.Italic;
				break;
			case FontStyle.BoldAndItalic:
				result = (FontStyles.Bold | FontStyles.Italic);
				break;
			default:
				result = FontStyles.Normal;
				break;
			}
			return result;
		}

		public static TextAlignment LegacyAlignmentToNewAlignment(TextAnchor anchor)
		{
			TextAlignment result;
			switch (anchor)
			{
			case TextAnchor.UpperLeft:
				result = TextAlignment.TopLeft;
				break;
			case TextAnchor.UpperCenter:
				result = TextAlignment.TopCenter;
				break;
			case TextAnchor.UpperRight:
				result = TextAlignment.TopRight;
				break;
			case TextAnchor.MiddleLeft:
				result = TextAlignment.MiddleLeft;
				break;
			case TextAnchor.MiddleCenter:
				result = TextAlignment.MiddleCenter;
				break;
			case TextAnchor.MiddleRight:
				result = TextAlignment.MiddleRight;
				break;
			case TextAnchor.LowerLeft:
				result = TextAlignment.BottomLeft;
				break;
			case TextAnchor.LowerCenter:
				result = TextAlignment.BottomCenter;
				break;
			case TextAnchor.LowerRight:
				result = TextAlignment.BottomRight;
				break;
			default:
				result = TextAlignment.TopLeft;
				break;
			}
			return result;
		}
	}
}
