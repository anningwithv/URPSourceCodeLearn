using System;

namespace UnityEngine.UIElements
{
	internal enum StyleValueType
	{
		Invalid,
		Keyword,
		Float,
		Dimension,
		Color,
		ResourcePath,
		AssetReference,
		Enum,
		Variable,
		String,
		Function,
		FunctionSeparator,
		ScalableImage,
		MissingAssetReference
	}
}
