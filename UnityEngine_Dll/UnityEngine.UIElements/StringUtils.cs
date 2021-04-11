using System;

namespace UnityEngine.UIElements
{
	internal static class StringUtils
	{
		public unsafe static int LevenshteinDistance(string s, string t)
		{
			int length = s.Length;
			int length2 = t.Length;
			bool flag = length == 0;
			int result;
			if (flag)
			{
				result = length2;
			}
			else
			{
				bool flag2 = length2 == 0;
				if (flag2)
				{
					result = length;
				}
				else
				{
					int num = length + 1;
					int num2 = length2 + 1;
					int* ptr = stackalloc int[num * num2];
					for (int i = 0; i <= length; i++)
					{
						ptr[num2 * i] = i;
					}
					for (int j = 0; j <= length2; j++)
					{
						ptr[j] = j;
					}
					for (int k = 1; k <= length2; k++)
					{
						for (int l = 1; l <= length; l++)
						{
							bool flag3 = s[l - 1] == t[k - 1];
							if (flag3)
							{
								ptr[num2 * l + k] = ptr[num2 * (l - 1) + k - 1];
							}
							else
							{
								ptr[num2 * l + k] = Math.Min(Math.Min(ptr[num2 * (l - 1) + k] + 1, ptr[num2 * l + k - 1] + 1), ptr[num2 * (l - 1) + k - 1] + 1);
							}
						}
					}
					result = ptr[num2 * length + length2];
				}
			}
			return result;
		}
	}
}
