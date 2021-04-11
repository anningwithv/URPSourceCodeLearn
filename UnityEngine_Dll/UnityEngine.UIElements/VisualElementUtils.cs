using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	internal static class VisualElementUtils
	{
		private static readonly HashSet<string> s_usedNames = new HashSet<string>();

		public static string GetUniqueName(string nameBase)
		{
			string text = nameBase;
			int num = 2;
			while (VisualElementUtils.s_usedNames.Contains(text))
			{
				text = nameBase + num.ToString();
				num++;
			}
			VisualElementUtils.s_usedNames.Add(text);
			return text;
		}
	}
}
