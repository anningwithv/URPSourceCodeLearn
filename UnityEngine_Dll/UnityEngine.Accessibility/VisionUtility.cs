using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.Accessibility
{
	[UsedByNativeCode]
	public static class VisionUtility
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly VisionUtility.<>c <>9 = new VisionUtility.<>c();

			public static Func<int, Color> <>9__6_1;

			internal Color <GetColorBlindSafePaletteInternal>b__6_1(int i)
			{
				return VisionUtility.s_ColorBlindSafePalette[i];
			}

			internal float cctor>b__7_0(Color c)
			{
				return VisionUtility.ComputePerceivedLuminance(c);
			}
		}

		private static readonly Color[] s_ColorBlindSafePalette = new Color[]
		{
			new Color32(0, 0, 0, 255),
			new Color32(73, 0, 146, 255),
			new Color32(7, 71, 81, 255),
			new Color32(0, 146, 146, 255),
			new Color32(182, 109, 255, 255),
			new Color32(255, 109, 182, 255),
			new Color32(109, 182, 255, 255),
			new Color32(36, 255, 36, 255),
			new Color32(255, 182, 219, 255),
			new Color32(182, 219, 255, 255),
			new Color32(255, 255, 109, 255),
			new Color32(30, 92, 92, 255),
			new Color32(74, 154, 87, 255),
			new Color32(113, 66, 183, 255),
			new Color32(162, 66, 183, 255),
			new Color32(178, 92, 25, 255),
			new Color32(100, 100, 100, 255),
			new Color32(80, 203, 181, 255),
			new Color32(82, 205, 242, 255)
		};

		private static readonly float[] s_ColorBlindSafePaletteLuminanceValues = VisionUtility.s_ColorBlindSafePalette.Select(new Func<Color, float>(VisionUtility.<>c.<>9.<.cctor>b__7_0)).ToArray<float>();

		internal static float ComputePerceivedLuminance(Color color)
		{
			color = color.linear;
			return Mathf.LinearToGammaSpace(0.2126f * color.r + 0.7152f * color.g + 0.0722f * color.b);
		}

		internal static void GetLuminanceValuesForPalette(Color[] palette, ref float[] outLuminanceValues)
		{
			Debug.Assert(palette != null && outLuminanceValues != null, "Passed in arrays can't be null.");
			Debug.Assert(palette.Length == outLuminanceValues.Length, "Passed in arrays need to be of the same length.");
			for (int i = 0; i < palette.Length; i++)
			{
				outLuminanceValues[i] = VisionUtility.ComputePerceivedLuminance(palette[i]);
			}
		}

		public unsafe static int GetColorBlindSafePalette(Color[] palette, float minimumLuminance, float maximumLuminance)
		{
			bool flag = palette == null;
			if (flag)
			{
				throw new ArgumentNullException("palette");
			}
			Color* palette2;
			if (palette == null || palette.Length == 0)
			{
				palette2 = null;
			}
			else
			{
				palette2 = &palette[0];
			}
			return VisionUtility.GetColorBlindSafePaletteInternal((void*)palette2, palette.Length, minimumLuminance, maximumLuminance, false);
		}

		internal unsafe static int GetColorBlindSafePalette(Color32[] palette, float minimumLuminance, float maximumLuminance)
		{
			bool flag = palette == null;
			if (flag)
			{
				throw new ArgumentNullException("palette");
			}
			Color32* palette2;
			if (palette == null || palette.Length == 0)
			{
				palette2 = null;
			}
			else
			{
				palette2 = &palette[0];
			}
			return VisionUtility.GetColorBlindSafePaletteInternal((void*)palette2, palette.Length, minimumLuminance, maximumLuminance, true);
		}

		[MethodImpl((MethodImplOptions)256)]
		private unsafe static int GetColorBlindSafePaletteInternal(void* palette, int paletteLength, float minimumLuminance, float maximumLuminance, bool useColor32)
		{
			bool flag = palette == null;
			if (flag)
			{
				throw new ArgumentNullException("palette");
			}
			IEnumerable<int> arg_66_0 = from i in Enumerable.Range(0, VisionUtility.s_ColorBlindSafePalette.Length)
			where VisionUtility.s_ColorBlindSafePaletteLuminanceValues[i] >= minimumLuminance && VisionUtility.s_ColorBlindSafePaletteLuminanceValues[i] <= maximumLuminance
			select i;
			Func<int, Color> arg_66_1;
			if ((arg_66_1 = VisionUtility.<>c.<>9__6_1) == null)
			{
				arg_66_1 = (VisionUtility.<>c.<>9__6_1 = new Func<int, Color>(VisionUtility.<>c.<>9.<GetColorBlindSafePaletteInternal>b__6_1));
			}
			Color[] array = arg_66_0.Select(arg_66_1).ToArray<Color>();
			int num = Mathf.Min(paletteLength, array.Length);
			bool flag2 = num > 0;
			if (flag2)
			{
				for (int k = 0; k < paletteLength; k++)
				{
					if (useColor32)
					{
						*(Color32*)((byte*)palette + (IntPtr)k * (IntPtr)sizeof(Color32)) = array[k % num];
					}
					else
					{
						*(Color*)((byte*)palette + (IntPtr)k * (IntPtr)sizeof(Color)) = array[k % num];
					}
				}
			}
			else
			{
				for (int j = 0; j < paletteLength; j++)
				{
					if (useColor32)
					{
						*(Color32*)((byte*)palette + (IntPtr)j * (IntPtr)sizeof(Color32)) = default(Color32);
					}
					else
					{
						*(Color*)((byte*)palette + (IntPtr)j * (IntPtr)sizeof(Color)) = default(Color);
					}
				}
			}
			return num;
		}
	}
}
