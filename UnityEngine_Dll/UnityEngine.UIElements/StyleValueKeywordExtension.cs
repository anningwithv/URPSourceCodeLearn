using System;

namespace UnityEngine.UIElements
{
	internal static class StyleValueKeywordExtension
	{
		public static string ToUssString(this StyleValueKeyword svk)
		{
			string result;
			switch (svk)
			{
			case StyleValueKeyword.Inherit:
				result = "inherit";
				break;
			case StyleValueKeyword.Initial:
				result = "initial";
				break;
			case StyleValueKeyword.Auto:
				result = "auto";
				break;
			case StyleValueKeyword.Unset:
				result = "unset";
				break;
			case StyleValueKeyword.True:
				result = "true";
				break;
			case StyleValueKeyword.False:
				result = "false";
				break;
			case StyleValueKeyword.None:
				result = "none";
				break;
			default:
				throw new ArgumentOutOfRangeException("svk", svk, "Unknown StyleValueKeyword");
			}
			return result;
		}
	}
}
