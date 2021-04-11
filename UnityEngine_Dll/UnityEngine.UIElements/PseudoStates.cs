using System;

namespace UnityEngine.UIElements
{
	[Flags]
	internal enum PseudoStates
	{
		Active = 1,
		Hover = 2,
		Checked = 8,
		Disabled = 32,
		Focus = 64,
		Root = 128
	}
}
