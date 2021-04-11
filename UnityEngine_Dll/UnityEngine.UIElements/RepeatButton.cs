using System;

namespace UnityEngine.UIElements
{
	public class RepeatButton : TextElement
	{
		public new class UxmlFactory : UxmlFactory<RepeatButton, RepeatButton.UxmlTraits>
		{
		}

		public new class UxmlTraits : TextElement.UxmlTraits
		{
			private UxmlLongAttributeDescription m_Delay = new UxmlLongAttributeDescription
			{
				name = "delay"
			};

			private UxmlLongAttributeDescription m_Interval = new UxmlLongAttributeDescription
			{
				name = "interval"
			};

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				RepeatButton repeatButton = (RepeatButton)ve;
				repeatButton.SetAction(null, this.m_Delay.GetValueFromBag(bag, cc), this.m_Interval.GetValueFromBag(bag, cc));
			}
		}

		private PointerClickable m_Clickable;

		public new static readonly string ussClassName = "unity-repeat-button";

		public RepeatButton()
		{
			base.AddToClassList(RepeatButton.ussClassName);
		}

		public RepeatButton(Action clickEvent, long delay, long interval) : this()
		{
			this.SetAction(clickEvent, delay, interval);
		}

		public void SetAction(Action clickEvent, long delay, long interval)
		{
			this.RemoveManipulator(this.m_Clickable);
			this.m_Clickable = new PointerClickable(clickEvent, delay, interval);
			this.AddManipulator(this.m_Clickable);
		}
	}
}
