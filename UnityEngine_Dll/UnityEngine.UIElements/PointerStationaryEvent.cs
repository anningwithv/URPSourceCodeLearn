using System;

namespace UnityEngine.UIElements
{
	public sealed class PointerStationaryEvent : PointerEventBase<PointerStationaryEvent>
	{
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		private void LocalInit()
		{
			((IPointerEventInternal)this).recomputeTopElementUnderPointer = true;
		}

		public PointerStationaryEvent()
		{
			this.LocalInit();
		}
	}
}
