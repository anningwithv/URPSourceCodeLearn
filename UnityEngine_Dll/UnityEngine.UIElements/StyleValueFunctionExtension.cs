using System;

namespace UnityEngine.UIElements
{
	internal static class StyleValueFunctionExtension
	{
		public const string k_Var = "var";

		public const string k_Env = "env";

		public const string k_LinearGradient = "linear-gradient";

		public static StyleValueFunction FromUssString(string ussValue)
		{
			ussValue = ussValue.ToLower();
			string text = ussValue;
			string text2 = text;
			if (text2 != null)
			{
				StyleValueFunction result;
				if (!(text2 == "var"))
				{
					if (!(text2 == "env"))
					{
						if (!(text2 == "linear-gradient"))
						{
							goto IL_45;
						}
						result = StyleValueFunction.LinearGradient;
					}
					else
					{
						result = StyleValueFunction.Env;
					}
				}
				else
				{
					result = StyleValueFunction.Var;
				}
				return result;
			}
			IL_45:
			throw new ArgumentOutOfRangeException("ussValue", ussValue, "Unknown function name");
		}

		public static string ToUssString(this StyleValueFunction svf)
		{
			string result;
			switch (svf)
			{
			case StyleValueFunction.Var:
				result = "var";
				break;
			case StyleValueFunction.Env:
				result = "env";
				break;
			case StyleValueFunction.LinearGradient:
				result = "linear-gradient";
				break;
			default:
				throw new ArgumentOutOfRangeException("svf", svf, "Unknown StyleValueFunction");
			}
			return result;
		}
	}
}
