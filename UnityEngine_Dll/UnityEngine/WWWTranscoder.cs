using System;
using System.IO;
using System.Text;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[VisibleToOtherModules(new string[]
	{
		"UnityEngine.UnityWebRequestWWWModule"
	})]
	internal class WWWTranscoder
	{
		private static byte[] ucHexChars = WWWForm.DefaultEncoding.GetBytes("0123456789ABCDEF");

		private static byte[] lcHexChars = WWWForm.DefaultEncoding.GetBytes("0123456789abcdef");

		private static byte urlEscapeChar = 37;

		private static byte[] urlSpace = new byte[]
		{
			43
		};

		private static byte[] dataSpace = WWWForm.DefaultEncoding.GetBytes("%20");

		private static byte[] urlForbidden = WWWForm.DefaultEncoding.GetBytes("@&;:<>=?\"'/\\!#%+$,{}|^[]`");

		private static byte qpEscapeChar = 61;

		private static byte[] qpSpace = new byte[]
		{
			95
		};

		private static byte[] qpForbidden = WWWForm.DefaultEncoding.GetBytes("&;=?\"'%+_");

		private static byte Hex2Byte(byte[] b, int offset)
		{
			byte b2 = 0;
			byte result;
			for (int i = offset; i < offset + 2; i++)
			{
				b2 *= 16;
				int num = (int)b[i];
				bool flag = num >= 48 && num <= 57;
				if (flag)
				{
					num -= 48;
				}
				else
				{
					bool flag2 = num >= 65 && num <= 75;
					if (flag2)
					{
						num -= 55;
					}
					else
					{
						bool flag3 = num >= 97 && num <= 102;
						if (flag3)
						{
							num -= 87;
						}
					}
				}
				bool flag4 = num > 15;
				if (flag4)
				{
					result = 63;
					return result;
				}
				b2 += (byte)num;
			}
			result = b2;
			return result;
		}

		private static byte[] Byte2Hex(byte b, byte[] hexChars)
		{
			return new byte[]
			{
				hexChars[b >> 4],
				hexChars[(int)(b & 15)]
			};
		}

		public static string URLEncode(string toEncode)
		{
			return WWWTranscoder.URLEncode(toEncode, Encoding.UTF8);
		}

		public static string URLEncode(string toEncode, Encoding e)
		{
			byte[] array = WWWTranscoder.Encode(e.GetBytes(toEncode), WWWTranscoder.urlEscapeChar, WWWTranscoder.urlSpace, WWWTranscoder.urlForbidden, false);
			return WWWForm.DefaultEncoding.GetString(array, 0, array.Length);
		}

		public static byte[] URLEncode(byte[] toEncode)
		{
			return WWWTranscoder.Encode(toEncode, WWWTranscoder.urlEscapeChar, WWWTranscoder.urlSpace, WWWTranscoder.urlForbidden, false);
		}

		public static string DataEncode(string toEncode)
		{
			return WWWTranscoder.DataEncode(toEncode, Encoding.UTF8);
		}

		public static string DataEncode(string toEncode, Encoding e)
		{
			byte[] array = WWWTranscoder.Encode(e.GetBytes(toEncode), WWWTranscoder.urlEscapeChar, WWWTranscoder.dataSpace, WWWTranscoder.urlForbidden, false);
			return WWWForm.DefaultEncoding.GetString(array, 0, array.Length);
		}

		public static byte[] DataEncode(byte[] toEncode)
		{
			return WWWTranscoder.Encode(toEncode, WWWTranscoder.urlEscapeChar, WWWTranscoder.dataSpace, WWWTranscoder.urlForbidden, false);
		}

		public static string QPEncode(string toEncode)
		{
			return WWWTranscoder.QPEncode(toEncode, Encoding.UTF8);
		}

		public static string QPEncode(string toEncode, Encoding e)
		{
			byte[] array = WWWTranscoder.Encode(e.GetBytes(toEncode), WWWTranscoder.qpEscapeChar, WWWTranscoder.qpSpace, WWWTranscoder.qpForbidden, true);
			return WWWForm.DefaultEncoding.GetString(array, 0, array.Length);
		}

		public static byte[] QPEncode(byte[] toEncode)
		{
			return WWWTranscoder.Encode(toEncode, WWWTranscoder.qpEscapeChar, WWWTranscoder.qpSpace, WWWTranscoder.qpForbidden, true);
		}

		public static byte[] Encode(byte[] input, byte escapeChar, byte[] space, byte[] forbidden, bool uppercase)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(input.Length * 2))
			{
				for (int i = 0; i < input.Length; i++)
				{
					bool flag = input[i] == 32;
					if (flag)
					{
						memoryStream.Write(space, 0, space.Length);
					}
					else
					{
						bool flag2 = input[i] < 32 || input[i] > 126 || WWWTranscoder.ByteArrayContains(forbidden, input[i]);
						if (flag2)
						{
							memoryStream.WriteByte(escapeChar);
							memoryStream.Write(WWWTranscoder.Byte2Hex(input[i], uppercase ? WWWTranscoder.ucHexChars : WWWTranscoder.lcHexChars), 0, 2);
						}
						else
						{
							memoryStream.WriteByte(input[i]);
						}
					}
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		private static bool ByteArrayContains(byte[] array, byte b)
		{
			int num = array.Length;
			bool result;
			for (int i = 0; i < num; i++)
			{
				bool flag = array[i] == b;
				if (flag)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public static string URLDecode(string toEncode)
		{
			return WWWTranscoder.URLDecode(toEncode, Encoding.UTF8);
		}

		public static string URLDecode(string toEncode, Encoding e)
		{
			byte[] array = WWWTranscoder.Decode(WWWForm.DefaultEncoding.GetBytes(toEncode), WWWTranscoder.urlEscapeChar, WWWTranscoder.urlSpace);
			return e.GetString(array, 0, array.Length);
		}

		public static byte[] URLDecode(byte[] toEncode)
		{
			return WWWTranscoder.Decode(toEncode, WWWTranscoder.urlEscapeChar, WWWTranscoder.urlSpace);
		}

		public static string DataDecode(string toDecode)
		{
			return WWWTranscoder.DataDecode(toDecode, Encoding.UTF8);
		}

		public static string DataDecode(string toDecode, Encoding e)
		{
			byte[] array = WWWTranscoder.Decode(WWWForm.DefaultEncoding.GetBytes(toDecode), WWWTranscoder.urlEscapeChar, WWWTranscoder.dataSpace);
			return e.GetString(array, 0, array.Length);
		}

		public static byte[] DataDecode(byte[] toDecode)
		{
			return WWWTranscoder.Decode(toDecode, WWWTranscoder.urlEscapeChar, WWWTranscoder.dataSpace);
		}

		public static string QPDecode(string toEncode)
		{
			return WWWTranscoder.QPDecode(toEncode, Encoding.UTF8);
		}

		public static string QPDecode(string toEncode, Encoding e)
		{
			byte[] array = WWWTranscoder.Decode(WWWForm.DefaultEncoding.GetBytes(toEncode), WWWTranscoder.qpEscapeChar, WWWTranscoder.qpSpace);
			return e.GetString(array, 0, array.Length);
		}

		public static byte[] QPDecode(byte[] toEncode)
		{
			return WWWTranscoder.Decode(toEncode, WWWTranscoder.qpEscapeChar, WWWTranscoder.qpSpace);
		}

		private static bool ByteSubArrayEquals(byte[] array, int index, byte[] comperand)
		{
			bool flag = array.Length - index < comperand.Length;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < comperand.Length; i++)
				{
					bool flag2 = array[index + i] != comperand[i];
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

		public static byte[] Decode(byte[] input, byte escapeChar, byte[] space)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(input.Length))
			{
				for (int i = 0; i < input.Length; i++)
				{
					bool flag = WWWTranscoder.ByteSubArrayEquals(input, i, space);
					if (flag)
					{
						i += space.Length - 1;
						memoryStream.WriteByte(32);
					}
					else
					{
						bool flag2 = input[i] == escapeChar && i + 2 < input.Length;
						if (flag2)
						{
							i++;
							memoryStream.WriteByte(WWWTranscoder.Hex2Byte(input, i++));
						}
						else
						{
							memoryStream.WriteByte(input[i]);
						}
					}
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		public static bool SevenBitClean(string s)
		{
			return WWWTranscoder.SevenBitClean(s, Encoding.UTF8);
		}

		public static bool SevenBitClean(string s, Encoding e)
		{
			return WWWTranscoder.SevenBitClean(e.GetBytes(s));
		}

		public static bool SevenBitClean(byte[] input)
		{
			bool result;
			for (int i = 0; i < input.Length; i++)
			{
				bool flag = input[i] < 32 || input[i] > 126;
				if (flag)
				{
					result = false;
					return result;
				}
			}
			result = true;
			return result;
		}
	}
}
