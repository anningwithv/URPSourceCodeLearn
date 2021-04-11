using System;

namespace UnityEngine.UIElements
{
	public class VisualElementFocusChangeDirection : FocusChangeDirection
	{
		private static readonly VisualElementFocusChangeDirection s_Left = new VisualElementFocusChangeDirection(FocusChangeDirection.lastValue + 1);

		private static readonly VisualElementFocusChangeDirection s_Right = new VisualElementFocusChangeDirection(FocusChangeDirection.lastValue + 2);

		public static FocusChangeDirection left
		{
			get
			{
				return VisualElementFocusChangeDirection.s_Left;
			}
		}

		public static FocusChangeDirection right
		{
			get
			{
				return VisualElementFocusChangeDirection.s_Right;
			}
		}

		protected new static VisualElementFocusChangeDirection lastValue
		{
			get
			{
				return VisualElementFocusChangeDirection.s_Right;
			}
		}

		protected VisualElementFocusChangeDirection(int value) : base(value)
		{
		}
	}
}
