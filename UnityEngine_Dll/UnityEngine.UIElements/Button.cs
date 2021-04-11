using System;

namespace UnityEngine.UIElements
{
	public class Button : TextElement
	{
		public new class UxmlFactory : UxmlFactory<Button, Button.UxmlTraits>
		{
		}

		public new class UxmlTraits : TextElement.UxmlTraits
		{
		}

		public new static readonly string ussClassName = "unity-button";

		private Clickable m_Clickable;

		private static readonly string NonEmptyString = " ";

		[Obsolete("onClick is obsolete. Use clicked instead (UnityUpgradable) -> clicked", true)]
		public event Action onClick
		{
			add
			{
				this.clicked += value;
			}
			remove
			{
				this.clicked -= value;
			}
		}

		public event Action clicked
		{
			add
			{
				bool flag = this.m_Clickable == null;
				if (flag)
				{
					this.clickable = new PointerClickable(value);
				}
				else
				{
					this.m_Clickable.clicked += value;
				}
			}
			remove
			{
				bool flag = this.m_Clickable != null;
				if (flag)
				{
					this.m_Clickable.clicked -= value;
				}
			}
		}

		public Clickable clickable
		{
			get
			{
				return this.m_Clickable;
			}
			set
			{
				bool flag = this.m_Clickable != null && this.m_Clickable.target == this;
				if (flag)
				{
					this.RemoveManipulator(this.m_Clickable);
				}
				this.m_Clickable = value;
				bool flag2 = this.m_Clickable != null;
				if (flag2)
				{
					this.AddManipulator(this.m_Clickable);
				}
			}
		}

		public Button() : this(null)
		{
		}

		public Button(Action clickEvent)
		{
			base.AddToClassList(Button.ussClassName);
			this.clickable = new PointerClickable(clickEvent);
		}

		protected internal override Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
		{
			string text = this.text;
			bool flag = string.IsNullOrEmpty(text);
			if (flag)
			{
				text = Button.NonEmptyString;
			}
			return base.MeasureTextSize(text, desiredWidth, widthMode, desiredHeight, heightMode);
		}

		protected override void ExecuteDefaultActionAtTarget(EventBase evt)
		{
			base.ExecuteDefaultActionAtTarget(evt);
			bool flag = evt == null;
			if (!flag)
			{
				bool flag2 = base.eventInterpreter.IsActivationEvent(evt);
				if (flag2)
				{
					this.clickable.SimulateSingleClick(evt, 100);
					evt.StopPropagation();
				}
			}
		}
	}
}
