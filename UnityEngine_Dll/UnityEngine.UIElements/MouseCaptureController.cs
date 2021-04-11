using System;

namespace UnityEngine.UIElements
{
	public static class MouseCaptureController
	{
		private static bool m_IsMouseCapturedWarningEmitted = false;

		private static bool m_ReleaseMouseWarningEmitted = false;

		public static bool IsMouseCaptured()
		{
			return EventDispatcher.editorDispatcher.pointerState.GetCapturingElement(PointerId.mousePointerId) != null;
		}

		public static bool HasMouseCapture(this IEventHandler handler)
		{
			VisualElement handler2 = handler as VisualElement;
			return handler2.HasPointerCapture(PointerId.mousePointerId);
		}

		public static void CaptureMouse(this IEventHandler handler)
		{
			VisualElement visualElement = handler as VisualElement;
			bool flag = visualElement != null;
			if (flag)
			{
				visualElement.CapturePointer(PointerId.mousePointerId);
				visualElement.panel.ProcessPointerCapture(PointerId.mousePointerId);
			}
		}

		public static void ReleaseMouse(this IEventHandler handler)
		{
			VisualElement visualElement = handler as VisualElement;
			bool flag = visualElement != null;
			if (flag)
			{
				visualElement.ReleasePointer(PointerId.mousePointerId);
				visualElement.panel.ProcessPointerCapture(PointerId.mousePointerId);
			}
		}

		public static void ReleaseMouse()
		{
			PointerCaptureHelper.ReleaseEditorMouseCapture();
		}
	}
}
