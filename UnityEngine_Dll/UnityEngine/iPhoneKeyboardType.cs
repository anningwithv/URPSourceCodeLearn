using System;
using System.ComponentModel;

namespace UnityEngine
{
	[EditorBrowsable(EditorBrowsableState.Never), Obsolete("iPhoneKeyboardType enumeration is deprecated. Please use TouchScreenKeyboardType instead (UnityUpgradable) -> TouchScreenKeyboardType", true)]
	public enum iPhoneKeyboardType
	{
		Default,
		ASCIICapable,
		NumbersAndPunctuation,
		URL,
		NumberPad,
		PhonePad,
		NamePhonePad,
		EmailAddress
	}
}
