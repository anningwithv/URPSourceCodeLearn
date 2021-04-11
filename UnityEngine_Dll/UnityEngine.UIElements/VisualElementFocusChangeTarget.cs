using System;

namespace UnityEngine.UIElements
{
	internal class VisualElementFocusChangeTarget : FocusChangeDirection
	{
		private static readonly ObjectPool<VisualElementFocusChangeTarget> Pool = new ObjectPool<VisualElementFocusChangeTarget>(100);

		public Focusable target
		{
			get;
			private set;
		}

		public static VisualElementFocusChangeTarget GetPooled(Focusable target)
		{
			VisualElementFocusChangeTarget visualElementFocusChangeTarget = VisualElementFocusChangeTarget.Pool.Get();
			visualElementFocusChangeTarget.target = target;
			return visualElementFocusChangeTarget;
		}

		protected override void Dispose()
		{
			VisualElementFocusChangeTarget.Pool.Release(this);
		}

		internal override void ApplyTo(FocusController focusController, Focusable f)
		{
			f.Focus();
		}

		public VisualElementFocusChangeTarget() : base(FocusChangeDirection.unspecified)
		{
		}
	}
}
