using System;

namespace UnityEngine.UIElements
{
	internal class AlignmentUtils
	{
		internal static float RoundToPixelGrid(float v, float pixelsPerPoint, float offset = 0.02f)
		{
			return Mathf.Floor(v * pixelsPerPoint + 0.5f + offset) / pixelsPerPoint;
		}

		internal static float CeilToPixelGrid(float v, float pixelsPerPoint, float offset = -0.02f)
		{
			return Mathf.Ceil(v * pixelsPerPoint + offset) / pixelsPerPoint;
		}

		internal static float FloorToPixelGrid(float v, float pixelsPerPoint, float offset = 0.02f)
		{
			return Mathf.Floor(v * pixelsPerPoint + offset) / pixelsPerPoint;
		}
	}
}
