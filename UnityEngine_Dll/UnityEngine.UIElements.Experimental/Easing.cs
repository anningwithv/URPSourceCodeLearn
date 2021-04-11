using System;

namespace UnityEngine.UIElements.Experimental
{
	public static class Easing
	{
		private const float HalfPi = 1.57079637f;

		public static float Step(float t)
		{
			return (float)((t < 0.5f) ? 0 : 1);
		}

		public static float Linear(float t)
		{
			return t;
		}

		public static float InSine(float t)
		{
			return Mathf.Sin(1.57079637f * (t - 1f)) + 1f;
		}

		public static float OutSine(float t)
		{
			return Mathf.Sin(t * 1.57079637f);
		}

		public static float InOutSine(float t)
		{
			return (Mathf.Sin(3.14159274f * (t - 0.5f)) + 1f) * 0.5f;
		}

		public static float InQuad(float t)
		{
			return t * t;
		}

		public static float OutQuad(float t)
		{
			return t * (2f - t);
		}

		public static float InOutQuad(float t)
		{
			t *= 2f;
			bool flag = t < 1f;
			float result;
			if (flag)
			{
				result = t * t * 0.5f;
			}
			else
			{
				result = -0.5f * ((t - 1f) * (t - 3f) - 1f);
			}
			return result;
		}

		public static float InCubic(float t)
		{
			return Easing.InPower(t, 3);
		}

		public static float OutCubic(float t)
		{
			return Easing.OutPower(t, 3);
		}

		public static float InOutCubic(float t)
		{
			return Easing.InOutPower(t, 3);
		}

		public static float InPower(float t, int power)
		{
			return Mathf.Pow(t, (float)power);
		}

		public static float OutPower(float t, int power)
		{
			int num = (power % 2 == 0) ? -1 : 1;
			return (float)num * (Mathf.Pow(t - 1f, (float)power) + (float)num);
		}

		public static float InOutPower(float t, int power)
		{
			t *= 2f;
			bool flag = t < 1f;
			float result;
			if (flag)
			{
				result = Easing.InPower(t, power) * 0.5f;
			}
			else
			{
				int num = (power % 2 == 0) ? -1 : 1;
				result = (float)num * 0.5f * (Mathf.Pow(t - 2f, (float)power) + (float)(num * 2));
			}
			return result;
		}

		public static float InBounce(float t)
		{
			return 1f - Easing.OutBounce(1f - t);
		}

		public static float OutBounce(float t)
		{
			bool flag = t < 0.363636374f;
			float result;
			if (flag)
			{
				result = 7.5625f * t * t;
			}
			else
			{
				bool flag2 = t < 0.727272749f;
				if (flag2)
				{
					float num;
					t = (num = t - 0.545454562f);
					result = 7.5625f * num * t + 0.75f;
				}
				else
				{
					bool flag3 = t < 0.909090936f;
					if (flag3)
					{
						float num2;
						t = (num2 = t - 0.8181818f);
						result = 7.5625f * num2 * t + 0.9375f;
					}
					else
					{
						float num3;
						t = (num3 = t - 0.954545438f);
						result = 7.5625f * num3 * t + 0.984375f;
					}
				}
			}
			return result;
		}

		public static float InOutBounce(float t)
		{
			bool flag = t < 0.5f;
			float result;
			if (flag)
			{
				result = Easing.InBounce(t * 2f) * 0.5f;
			}
			else
			{
				result = Easing.OutBounce((t - 0.5f) * 2f) * 0.5f + 0.5f;
			}
			return result;
		}

		public static float InElastic(float t)
		{
			bool flag = t == 0f;
			float result;
			if (flag)
			{
				result = 0f;
			}
			else
			{
				bool flag2 = t == 1f;
				if (flag2)
				{
					result = 1f;
				}
				else
				{
					float num = 0.3f;
					float num2 = num / 4f;
					float num3 = Mathf.Pow(2f, 10f * (t -= 1f));
					result = -(num3 * Mathf.Sin((t - num2) * 6.28318548f / num));
				}
			}
			return result;
		}

		public static float OutElastic(float t)
		{
			bool flag = t == 0f;
			float result;
			if (flag)
			{
				result = 0f;
			}
			else
			{
				bool flag2 = t == 1f;
				if (flag2)
				{
					result = 1f;
				}
				else
				{
					float num = 0.3f;
					float num2 = num / 4f;
					result = Mathf.Pow(2f, -10f * t) * Mathf.Sin((t - num2) * 6.28318548f / num) + 1f;
				}
			}
			return result;
		}

		public static float InOutElastic(float t)
		{
			bool flag = t < 0.5f;
			float result;
			if (flag)
			{
				result = Easing.InElastic(t * 2f) * 0.5f;
			}
			else
			{
				result = Easing.OutElastic((t - 0.5f) * 2f) * 0.5f + 0.5f;
			}
			return result;
		}

		public static float InBack(float t)
		{
			float num = 1.70158f;
			return t * t * ((num + 1f) * t - num);
		}

		public static float OutBack(float t)
		{
			return 1f - Easing.InBack(1f - t);
		}

		public static float InOutBack(float t)
		{
			bool flag = t < 0.5f;
			float result;
			if (flag)
			{
				result = Easing.InBack(t * 2f) * 0.5f;
			}
			else
			{
				result = Easing.OutBack((t - 0.5f) * 2f) * 0.5f + 0.5f;
			}
			return result;
		}

		public static float InBack(float t, float s)
		{
			return t * t * ((s + 1f) * t - s);
		}

		public static float OutBack(float t, float s)
		{
			return 1f - Easing.InBack(1f - t, s);
		}

		public static float InOutBack(float t, float s)
		{
			bool flag = t < 0.5f;
			float result;
			if (flag)
			{
				result = Easing.InBack(t * 2f, s) * 0.5f;
			}
			else
			{
				result = Easing.OutBack((t - 0.5f) * 2f, s) * 0.5f + 0.5f;
			}
			return result;
		}

		public static float InCirc(float t)
		{
			return -(Mathf.Sqrt(1f - t * t) - 1f);
		}

		public static float OutCirc(float t)
		{
			t -= 1f;
			return Mathf.Sqrt(1f - t * t);
		}

		public static float InOutCirc(float t)
		{
			t *= 2f;
			bool flag = t < 1f;
			float result;
			if (flag)
			{
				result = -0.5f * (Mathf.Sqrt(1f - t * t) - 1f);
			}
			else
			{
				t -= 2f;
				result = 0.5f * (Mathf.Sqrt(1f - t * t) + 1f);
			}
			return result;
		}
	}
}
