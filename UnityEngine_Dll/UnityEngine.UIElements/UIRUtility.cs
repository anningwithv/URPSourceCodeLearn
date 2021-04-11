using System;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	internal static class UIRUtility
	{
		public static readonly string k_DefaultShaderName = Shaders.k_Runtime;

		public static readonly string k_DefaultWorldSpaceShaderName = Shaders.k_RuntimeWorld;

		public const float k_ClearZ = 0.99f;

		public const float k_MeshPosZ = 0f;

		public const float k_MaskPosZ = 1f;

		public static Vector4 ToVector4(Rect rc)
		{
			return new Vector4(rc.xMin, rc.yMin, rc.xMax, rc.yMax);
		}

		public static bool IsRoundRect(VisualElement ve)
		{
			IResolvedStyle resolvedStyle = ve.resolvedStyle;
			return resolvedStyle.borderTopLeftRadius >= Mathf.Epsilon || resolvedStyle.borderTopRightRadius >= Mathf.Epsilon || resolvedStyle.borderBottomLeftRadius >= Mathf.Epsilon || resolvedStyle.borderBottomRightRadius >= Mathf.Epsilon;
		}

		public static bool IsVectorImageBackground(VisualElement ve)
		{
			return ve.computedStyle.backgroundImage.value.vectorImage != null;
		}

		public static void Destroy(UnityEngine.Object obj)
		{
			bool flag = obj == null;
			if (!flag)
			{
				bool isPlaying = Application.isPlaying;
				if (isPlaying)
				{
					UnityEngine.Object.Destroy(obj);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(obj);
				}
			}
		}
	}
}
