using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.GlobalIllumination
{
	public struct LinearColor
	{
		private float m_red;

		private float m_green;

		private float m_blue;

		private float m_intensity;

		public float red
		{
			get
			{
				return this.m_red;
			}
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("Red color (" + value.ToString() + ") must be in range [0;1].");
				}
				this.m_red = value;
			}
		}

		public float green
		{
			get
			{
				return this.m_green;
			}
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("Green color (" + value.ToString() + ") must be in range [0;1].");
				}
				this.m_green = value;
			}
		}

		public float blue
		{
			get
			{
				return this.m_blue;
			}
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("Blue color (" + value.ToString() + ") must be in range [0;1].");
				}
				this.m_blue = value;
			}
		}

		public float intensity
		{
			get
			{
				return this.m_intensity;
			}
			set
			{
				bool flag = value < 0f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("Intensity (" + value.ToString() + ") must be positive.");
				}
				this.m_intensity = value;
			}
		}

		public static LinearColor Convert(Color color, float intensity)
		{
			Color color2 = GraphicsSettings.lightsUseLinearIntensity ? color.linear.RGBMultiplied(intensity) : color.RGBMultiplied(intensity).linear;
			float maxColorComponent = color2.maxColorComponent;
			bool flag = color2.r < 0f || color2.g < 0f || color2.b < 0f;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Concat(new string[]
				{
					"The input color to be converted must not contain negative values (red: ",
					color2.r.ToString(),
					", green: ",
					color2.g.ToString(),
					", blue: ",
					color2.b.ToString(),
					")."
				}));
			}
			bool flag2 = maxColorComponent <= 1E-20f;
			LinearColor result;
			if (flag2)
			{
				result = LinearColor.Black();
			}
			else
			{
				float num = 1f / color2.maxColorComponent;
				LinearColor linearColor;
				linearColor.m_red = color2.r * num;
				linearColor.m_green = color2.g * num;
				linearColor.m_blue = color2.b * num;
				linearColor.m_intensity = maxColorComponent;
				result = linearColor;
			}
			return result;
		}

		public static LinearColor Black()
		{
			LinearColor result;
			result.m_red = (result.m_green = (result.m_blue = (result.m_intensity = 0f)));
			return result;
		}
	}
}
