using System;

namespace UnityEngine.UIElements
{
	public static class PointerCaptureHelper
	{
		private static PointerDispatchState GetStateFor(IEventHandler handler)
		{
			VisualElement visualElement = handler as VisualElement;
			PointerDispatchState arg_2C_0;
			if (visualElement == null)
			{
				arg_2C_0 = null;
			}
			else
			{
				IPanel expr_14 = visualElement.panel;
				if (expr_14 == null)
				{
					arg_2C_0 = null;
				}
				else
				{
					EventDispatcher expr_20 = expr_14.dispatcher;
					arg_2C_0 = ((expr_20 != null) ? expr_20.pointerState : null);
				}
			}
			return arg_2C_0;
		}

		public static bool HasPointerCapture(this IEventHandler handler, int pointerId)
		{
			PointerDispatchState expr_07 = PointerCaptureHelper.GetStateFor(handler);
			return expr_07 != null && expr_07.HasPointerCapture(handler, pointerId);
		}

		public static void CapturePointer(this IEventHandler handler, int pointerId)
		{
			PointerDispatchState expr_07 = PointerCaptureHelper.GetStateFor(handler);
			if (expr_07 != null)
			{
				expr_07.CapturePointer(handler, pointerId);
			}
		}

		public static void ReleasePointer(this IEventHandler handler, int pointerId)
		{
			PointerDispatchState expr_07 = PointerCaptureHelper.GetStateFor(handler);
			if (expr_07 != null)
			{
				expr_07.ReleasePointer(handler, pointerId);
			}
		}

		public static IEventHandler GetCapturingElement(this IPanel panel, int pointerId)
		{
			IEventHandler arg_1F_0;
			if (panel == null)
			{
				arg_1F_0 = null;
			}
			else
			{
				EventDispatcher expr_0D = panel.dispatcher;
				arg_1F_0 = ((expr_0D != null) ? expr_0D.pointerState.GetCapturingElement(pointerId) : null);
			}
			return arg_1F_0;
		}

		public static void ReleasePointer(this IPanel panel, int pointerId)
		{
			if (panel != null)
			{
				EventDispatcher expr_0C = panel.dispatcher;
				if (expr_0C != null)
				{
					expr_0C.pointerState.ReleasePointer(pointerId);
				}
			}
		}

		internal static void ActivateCompatibilityMouseEvents(this IPanel panel, int pointerId)
		{
			if (panel != null)
			{
				EventDispatcher expr_0C = panel.dispatcher;
				if (expr_0C != null)
				{
					expr_0C.pointerState.ActivateCompatibilityMouseEvents(pointerId);
				}
			}
		}

		internal static void PreventCompatibilityMouseEvents(this IPanel panel, int pointerId)
		{
			if (panel != null)
			{
				EventDispatcher expr_0C = panel.dispatcher;
				if (expr_0C != null)
				{
					expr_0C.pointerState.PreventCompatibilityMouseEvents(pointerId);
				}
			}
		}

		internal static bool ShouldSendCompatibilityMouseEvents(this IPanel panel, IPointerEvent evt)
		{
			bool? arg_34_0;
			if (panel == null)
			{
				arg_34_0 = null;
			}
			else
			{
				EventDispatcher expr_15 = panel.dispatcher;
				arg_34_0 = ((expr_15 != null) ? new bool?(expr_15.pointerState.ShouldSendCompatibilityMouseEvents(evt)) : null);
			}
			return arg_34_0 ?? true;
		}

		internal static void ProcessPointerCapture(this IPanel panel, int pointerId)
		{
			if (panel != null)
			{
				EventDispatcher expr_0C = panel.dispatcher;
				if (expr_0C != null)
				{
					expr_0C.pointerState.ProcessPointerCapture(pointerId);
				}
			}
		}

		internal static void ReleaseEditorMouseCapture()
		{
			EventDispatcher.editorDispatcher.pointerState.ReleasePointer(PointerId.mousePointerId);
			EventDispatcher.editorDispatcher.pointerState.ProcessPointerCapture(PointerId.mousePointerId);
		}

		internal static void ResetPointerDispatchState(this IPanel panel)
		{
			if (panel != null)
			{
				EventDispatcher expr_0C = panel.dispatcher;
				if (expr_0C != null)
				{
					expr_0C.pointerState.Reset();
				}
			}
		}
	}
}
