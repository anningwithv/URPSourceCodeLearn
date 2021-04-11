using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	[IsReadOnly]
	internal struct DrawStates
	{
		public readonly int controlId;

		public readonly bool isHover;

		public readonly bool isActive;

		public readonly bool on;

		public readonly bool hasKeyboardFocus;

		public readonly bool hasTextInput;

		public readonly bool drawSelectionAsComposition;

		public readonly int cursorFirst;

		public readonly int cursorLast;

		public readonly Color cursorColor;

		public readonly Color selectionColor;

		public DrawStates(bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			this = new DrawStates(-1, isHover, isActive, on, hasKeyboardFocus);
		}

		public DrawStates(int controlId, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			this.controlId = controlId;
			this.isHover = isHover;
			this.isActive = isActive;
			this.on = on;
			this.hasKeyboardFocus = hasKeyboardFocus;
			this.hasTextInput = false;
			this.drawSelectionAsComposition = false;
			this.cursorFirst = (this.cursorLast = -1);
			this.selectionColor = (this.cursorColor = Color.red);
		}

		public DrawStates(int controlId, bool isHover, bool isActive, bool on, bool hasKeyboardFocus, bool drawSelectionAsComposition, int cursorFirst, int cursorLast, Color cursorColor, Color selectionColor)
		{
			this = new DrawStates(controlId, isHover, isActive, on, hasKeyboardFocus);
			this.hasTextInput = true;
			this.drawSelectionAsComposition = drawSelectionAsComposition;
			this.cursorFirst = cursorFirst;
			this.cursorLast = cursorLast;
			this.cursorColor = cursorColor;
			this.selectionColor = selectionColor;
		}
	}
}
