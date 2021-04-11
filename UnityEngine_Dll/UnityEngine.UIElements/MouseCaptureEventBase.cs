using System;

namespace UnityEngine.UIElements
{
	public abstract class MouseCaptureEventBase<T> : PointerCaptureEventBase<T>, IMouseCaptureEvent where T : MouseCaptureEventBase<T>, new()
	{
		public new IEventHandler relatedTarget
		{
			get
			{
				return base.relatedTarget;
			}
		}

		public static T GetPooled(IEventHandler target, IEventHandler relatedTarget)
		{
			return PointerCaptureEventBase<T>.GetPooled(target, relatedTarget, 0);
		}

		protected override void Init()
		{
			base.Init();
		}
	}
}
