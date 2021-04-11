using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Scripting/TextAsset.h")]
	public class TextAsset : Object
	{
		internal enum CreateOptions
		{
			None,
			CreateNativeObject
		}

		private static class EncodingUtility
		{
			internal static readonly KeyValuePair<byte[], Encoding>[] encodingLookup;

			internal static readonly Encoding targetEncoding;

			static EncodingUtility()
			{
				TextAsset.EncodingUtility.targetEncoding = Encoding.GetEncoding(Encoding.UTF8.CodePage, new EncoderReplacementFallback("�"), new DecoderReplacementFallback("�"));
				Encoding encoding = new UTF32Encoding(true, true, true);
				Encoding encoding2 = new UTF32Encoding(false, true, true);
				Encoding encoding3 = new UnicodeEncoding(true, true, true);
				Encoding encoding4 = new UnicodeEncoding(false, true, true);
				Encoding encoding5 = new UTF8Encoding(true, true);
				TextAsset.EncodingUtility.encodingLookup = new KeyValuePair<byte[], Encoding>[]
				{
					new KeyValuePair<byte[], Encoding>(encoding.GetPreamble(), encoding),
					new KeyValuePair<byte[], Encoding>(encoding2.GetPreamble(), encoding2),
					new KeyValuePair<byte[], Encoding>(encoding3.GetPreamble(), encoding3),
					new KeyValuePair<byte[], Encoding>(encoding4.GetPreamble(), encoding4),
					new KeyValuePair<byte[], Encoding>(encoding5.GetPreamble(), encoding5)
				};
			}
		}

		public extern byte[] bytes
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public string text
		{
			get
			{
				return TextAsset.DecodeString(this.bytes);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern byte[] GetPreviewBytes(int maxByteCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateInstance([Writable] TextAsset self, string text);

		public override string ToString()
		{
			return this.text;
		}

		public TextAsset() : this(TextAsset.CreateOptions.CreateNativeObject, null)
		{
		}

		public TextAsset(string text) : this(TextAsset.CreateOptions.CreateNativeObject, text)
		{
		}

		internal TextAsset(TextAsset.CreateOptions options, string text)
		{
			bool flag = options == TextAsset.CreateOptions.CreateNativeObject;
			if (flag)
			{
				TextAsset.Internal_CreateInstance(this, text);
			}
		}

		internal string GetPreview(int maxChars)
		{
			return TextAsset.DecodeString(this.GetPreviewBytes(maxChars * 4));
		}

		internal static string DecodeString(byte[] bytes)
		{
			int num = TextAsset.EncodingUtility.encodingLookup.Length;
			int i = 0;
			int num2;
			string @string;
			while (i < num)
			{
				byte[] key = TextAsset.EncodingUtility.encodingLookup[i].Key;
				num2 = key.Length;
				bool flag = bytes.Length >= num2;
				if (flag)
				{
					for (int j = 0; j < num2; j++)
					{
						bool flag2 = key[j] != bytes[j];
						if (flag2)
						{
							num2 = -1;
						}
					}
					bool flag3 = num2 < 0;
					if (!flag3)
					{
						try
						{
							Encoding value = TextAsset.EncodingUtility.encodingLookup[i].Value;
							@string = value.GetString(bytes, num2, bytes.Length - num2);
							return @string;
						}
						catch
						{
						}
					}
				}
				IL_A2:
				i++;
				continue;
				goto IL_A2;
			}
			num2 = 0;
			Encoding targetEncoding = TextAsset.EncodingUtility.targetEncoding;
			@string = targetEncoding.GetString(bytes, num2, bytes.Length - num2);
			return @string;
		}
	}
}
