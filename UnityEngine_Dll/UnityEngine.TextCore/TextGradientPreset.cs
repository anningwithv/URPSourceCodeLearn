using System;

namespace UnityEngine.TextCore
{
	[Serializable]
	internal class TextGradientPreset : ScriptableObject
	{
		public ColorMode colorMode;

		public Color topLeft;

		public Color topRight;

		public Color bottomLeft;

		public Color bottomRight;

		public TextGradientPreset()
		{
			this.colorMode = ColorMode.FourCornersGradient;
			this.topLeft = Color.white;
			this.topRight = Color.white;
			this.bottomLeft = Color.white;
			this.bottomRight = Color.white;
		}

		public TextGradientPreset(Color color)
		{
			this.colorMode = ColorMode.FourCornersGradient;
			this.topLeft = color;
			this.topRight = color;
			this.bottomLeft = color;
			this.bottomRight = color;
		}

		public TextGradientPreset(Color color0, Color color1, Color color2, Color color3)
		{
			this.colorMode = ColorMode.FourCornersGradient;
			this.topLeft = color0;
			this.topRight = color1;
			this.bottomLeft = color2;
			this.bottomRight = color3;
		}
	}
}
