using System;
using System.Globalization;

namespace UnityEngine.UIElements.StyleSheets
{
	internal static class StyleSheetExtensions
	{
		public static string ReadAsString(this StyleSheet sheet, StyleValueHandle handle)
		{
			string result = string.Empty;
			switch (handle.valueType)
			{
			case StyleValueType.Keyword:
				result = sheet.ReadKeyword(handle).ToUssString();
				break;
			case StyleValueType.Float:
				result = sheet.ReadFloat(handle).ToString(CultureInfo.InvariantCulture.NumberFormat);
				break;
			case StyleValueType.Dimension:
				result = sheet.ReadDimension(handle).ToString();
				break;
			case StyleValueType.Color:
				result = sheet.ReadColor(handle).ToString();
				break;
			case StyleValueType.ResourcePath:
				result = sheet.ReadResourcePath(handle);
				break;
			case StyleValueType.AssetReference:
				result = sheet.ReadAssetReference(handle).ToString();
				break;
			case StyleValueType.Enum:
				result = sheet.ReadEnum(handle);
				break;
			case StyleValueType.Variable:
				result = sheet.ReadVariable(handle);
				break;
			case StyleValueType.String:
				result = sheet.ReadString(handle);
				break;
			case StyleValueType.Function:
				result = sheet.ReadFunctionName(handle);
				break;
			case StyleValueType.FunctionSeparator:
				result = ",";
				break;
			case StyleValueType.ScalableImage:
				result = sheet.ReadScalableImage(handle).ToString();
				break;
			default:
				result = "Error reading value type (" + handle.valueType.ToString() + ") at index " + handle.valueIndex.ToString();
				break;
			}
			return result;
		}

		public static bool IsVarFunction(this StyleValueHandle handle)
		{
			return handle.valueType == StyleValueType.Function && handle.valueIndex == 1;
		}
	}
}
