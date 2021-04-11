using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	public class DropdownMenuEventInfo
	{
		public EventModifiers modifiers
		{
			[CompilerGenerated]
			get
			{
				return this.<modifiers>k__BackingField;
			}
		}

		public Vector2 mousePosition
		{
			[CompilerGenerated]
			get
			{
				return this.<mousePosition>k__BackingField;
			}
		}

		public Vector2 localMousePosition
		{
			[CompilerGenerated]
			get
			{
				return this.<localMousePosition>k__BackingField;
			}
		}

		private char character
		{
			[CompilerGenerated]
			get
			{
				return this.<character>k__BackingField;
			}
		}

		private KeyCode keyCode
		{
			[CompilerGenerated]
			get
			{
				return this.<keyCode>k__BackingField;
			}
		}

		public DropdownMenuEventInfo(EventBase e)
		{
			IMouseEvent mouseEvent = e as IMouseEvent;
			bool flag = mouseEvent != null;
			if (flag)
			{
				this.<mousePosition>k__BackingField = mouseEvent.mousePosition;
				this.<localMousePosition>k__BackingField = mouseEvent.localMousePosition;
				this.<modifiers>k__BackingField = mouseEvent.modifiers;
				this.<character>k__BackingField = '\0';
				this.<keyCode>k__BackingField = KeyCode.None;
			}
			else
			{
				IKeyboardEvent keyboardEvent = e as IKeyboardEvent;
				bool flag2 = keyboardEvent != null;
				if (flag2)
				{
					this.<character>k__BackingField = keyboardEvent.character;
					this.<keyCode>k__BackingField = keyboardEvent.keyCode;
					this.<modifiers>k__BackingField = keyboardEvent.modifiers;
					this.<mousePosition>k__BackingField = Vector2.zero;
					this.<localMousePosition>k__BackingField = Vector2.zero;
				}
			}
		}
	}
}
