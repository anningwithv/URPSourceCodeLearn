using System;

namespace UnityEngine.UIElements
{
	internal static class PointerDeviceState
	{
		private static Vector2[] m_Positions = new Vector2[PointerId.maxPointers];

		private static IPanel[] m_Panels = new IPanel[PointerId.maxPointers];

		private static int[] m_PressedButtons = new int[PointerId.maxPointers];

		internal static void Reset()
		{
			for (int i = 0; i < PointerId.maxPointers; i++)
			{
				PointerDeviceState.m_Positions[i] = Vector2.zero;
				PointerDeviceState.m_Panels[i] = null;
				PointerDeviceState.m_PressedButtons[i] = 0;
			}
		}

		public static void SavePointerPosition(int pointerId, Vector2 position, IPanel panel)
		{
			PointerDeviceState.m_Positions[pointerId] = position;
			PointerDeviceState.m_Panels[pointerId] = panel;
		}

		public static void PressButton(int pointerId, int buttonId)
		{
			Debug.Assert(buttonId >= 0);
			Debug.Assert(buttonId < 32);
			PointerDeviceState.m_PressedButtons[pointerId] |= 1 << buttonId;
		}

		public static void ReleaseButton(int pointerId, int buttonId)
		{
			Debug.Assert(buttonId >= 0);
			Debug.Assert(buttonId < 32);
			PointerDeviceState.m_PressedButtons[pointerId] &= ~(1 << buttonId);
		}

		public static void ReleaseAllButtons(int pointerId)
		{
			PointerDeviceState.m_PressedButtons[pointerId] = 0;
		}

		public static Vector2 GetPointerPosition(int pointerId)
		{
			return PointerDeviceState.m_Positions[pointerId];
		}

		public static IPanel GetPanel(int pointerId)
		{
			return PointerDeviceState.m_Panels[pointerId];
		}

		public static int GetPressedButtons(int pointerId)
		{
			return PointerDeviceState.m_PressedButtons[pointerId];
		}

		internal static bool HasAdditionalPressedButtons(int pointerId, int exceptButtonId)
		{
			return (PointerDeviceState.m_PressedButtons[pointerId] & ~(1 << exceptButtonId)) != 0;
		}
	}
}
