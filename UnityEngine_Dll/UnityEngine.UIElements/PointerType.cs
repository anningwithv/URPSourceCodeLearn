using System;

namespace UnityEngine.UIElements
{
	public static class PointerType
	{
		public static readonly string mouse = "mouse";

		public static readonly string touch = "touch";

		public static readonly string pen = "pen";

		public static readonly string unknown = "";

		internal static string GetPointerType(int pointerId)
		{
			bool flag = pointerId == PointerId.mousePointerId;
			string result;
			if (flag)
			{
				result = PointerType.mouse;
			}
			else
			{
				result = PointerType.touch;
			}
			return result;
		}

		internal static bool IsDirectManipulationDevice(string pointerType)
		{
			return pointerType == PointerType.touch || pointerType == PointerType.pen;
		}
	}
}
