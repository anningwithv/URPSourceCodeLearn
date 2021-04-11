using System;

namespace UnityEngine.UIElements
{
	internal static class VisualElementDebugExtensions
	{
		public static string GetDisplayName(this VisualElement ve, bool withHashCode = true)
		{
			bool flag = ve == null;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				string text = ve.GetType().Name;
				bool flag2 = !string.IsNullOrEmpty(ve.name);
				if (flag2)
				{
					text = text + "#" + ve.name;
				}
				if (withHashCode)
				{
					text = text + " (" + ve.GetHashCode().ToString("x8") + ")";
				}
				result = text;
			}
			return result;
		}
	}
}
