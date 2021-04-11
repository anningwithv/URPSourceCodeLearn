using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	public class FocusChangeDirection : IDisposable
	{
		private readonly int m_Value;

		public static FocusChangeDirection unspecified
		{
			[CompilerGenerated]
			get
			{
				return FocusChangeDirection.<unspecified>k__BackingField;
			}
		}

		public static FocusChangeDirection none
		{
			[CompilerGenerated]
			get
			{
				return FocusChangeDirection.<none>k__BackingField;
			}
		}

		protected static FocusChangeDirection lastValue
		{
			[CompilerGenerated]
			get
			{
				return FocusChangeDirection.<lastValue>k__BackingField;
			}
		}

		protected FocusChangeDirection(int value)
		{
			this.m_Value = value;
		}

		public static implicit operator int(FocusChangeDirection fcd)
		{
			return (fcd != null) ? fcd.m_Value : 0;
		}

		void IDisposable.Dispose()
		{
			this.Dispose();
		}

		protected virtual void Dispose()
		{
		}

		internal virtual void ApplyTo(FocusController focusController, Focusable f)
		{
			focusController.SwitchFocus(f, this, false);
		}

		static FocusChangeDirection()
		{
			// Note: this type is marked as 'beforefieldinit'.
			FocusChangeDirection.<unspecified>k__BackingField = new FocusChangeDirection(-1);
			FocusChangeDirection.<none>k__BackingField = new FocusChangeDirection(0);
			FocusChangeDirection.<lastValue>k__BackingField = FocusChangeDirection.none;
		}
	}
}
