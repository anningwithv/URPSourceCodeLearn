using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	[NativeConditional("ENABLE_ONSCREEN_KEYBOARD"), NativeHeader("Runtime/Export/TouchScreenKeyboard/TouchScreenKeyboard.bindings.h"), NativeHeader("Runtime/Input/KeyboardOnScreen.h")]
	public class TouchScreenKeyboard
	{
		public enum Status
		{
			Visible,
			Done,
			Canceled,
			LostFocus
		}

		[NonSerialized]
		internal IntPtr m_Ptr;

		public static bool isSupported
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				RuntimePlatform runtimePlatform = platform;
				RuntimePlatform runtimePlatform2 = runtimePlatform;
				if (runtimePlatform2 <= RuntimePlatform.MetroPlayerARM)
				{
					if (runtimePlatform2 != RuntimePlatform.IPhonePlayer && runtimePlatform2 != RuntimePlatform.Android && runtimePlatform2 - RuntimePlatform.MetroPlayerX86 > 2)
					{
						goto IL_4D;
					}
				}
				else if (runtimePlatform2 <= RuntimePlatform.Switch)
				{
					if (runtimePlatform2 != RuntimePlatform.PS4 && runtimePlatform2 - RuntimePlatform.tvOS > 1)
					{
						goto IL_4D;
					}
				}
				else if (runtimePlatform2 != RuntimePlatform.Stadia && runtimePlatform2 != RuntimePlatform.PS5)
				{
					goto IL_4D;
				}
				bool result = true;
				return result;
				IL_4D:
				result = false;
				return result;
			}
		}

		public static bool isInPlaceEditingAllowed
		{
			get
			{
				return false;
			}
		}

		public extern string text
		{
			[NativeName("GetText")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("SetText")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool hideInput
		{
			[NativeName("IsInputHidden")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("SetInputHidden")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool active
		{
			[NativeName("IsActive")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("SetActive")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("Property done is deprecated, use status instead")]
		public bool done
		{
			get
			{
				return TouchScreenKeyboard.GetDone(this.m_Ptr);
			}
		}

		[Obsolete("Property wasCanceled is deprecated, use status instead.")]
		public bool wasCanceled
		{
			get
			{
				return TouchScreenKeyboard.GetWasCanceled(this.m_Ptr);
			}
		}

		public extern TouchScreenKeyboard.Status status
		{
			[NativeName("GetKeyboardStatus")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int characterLimit
		{
			[NativeName("GetCharacterLimit")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("SetCharacterLimit")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool canGetSelection
		{
			[NativeName("CanGetSelection")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool canSetSelection
		{
			[NativeName("CanSetSelection")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public RangeInt selection
		{
			get
			{
				RangeInt result;
				TouchScreenKeyboard.GetSelection(out result.start, out result.length);
				return result;
			}
			set
			{
				bool flag = value.start < 0 || value.length < 0 || value.start + value.length > this.text.Length;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("selection", "Selection is out of range.");
				}
				TouchScreenKeyboard.SetSelection(value.start, value.length);
			}
		}

		public extern TouchScreenKeyboardType type
		{
			[NativeName("GetKeyboardType")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public int targetDisplay
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		[NativeConditional("ENABLE_ONSCREEN_KEYBOARD", "RectT<float>()")]
		public static Rect area
		{
			[NativeName("GetRect")]
			get
			{
				Rect result;
				TouchScreenKeyboard.get_area_Injected(out result);
				return result;
			}
		}

		public static extern bool visible
		{
			[NativeName("IsVisible")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[FreeFunction("TouchScreenKeyboard_Destroy", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		private void Destroy()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				TouchScreenKeyboard.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		~TouchScreenKeyboard()
		{
			this.Destroy();
		}

		public TouchScreenKeyboard(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure, bool alert, string textPlaceholder, int characterLimit)
		{
			TouchScreenKeyboard_InternalConstructorHelperArguments touchScreenKeyboard_InternalConstructorHelperArguments = default(TouchScreenKeyboard_InternalConstructorHelperArguments);
			touchScreenKeyboard_InternalConstructorHelperArguments.keyboardType = Convert.ToUInt32(keyboardType);
			touchScreenKeyboard_InternalConstructorHelperArguments.autocorrection = Convert.ToUInt32(autocorrection);
			touchScreenKeyboard_InternalConstructorHelperArguments.multiline = Convert.ToUInt32(multiline);
			touchScreenKeyboard_InternalConstructorHelperArguments.secure = Convert.ToUInt32(secure);
			touchScreenKeyboard_InternalConstructorHelperArguments.alert = Convert.ToUInt32(alert);
			touchScreenKeyboard_InternalConstructorHelperArguments.characterLimit = characterLimit;
			this.m_Ptr = TouchScreenKeyboard.TouchScreenKeyboard_InternalConstructorHelper(ref touchScreenKeyboard_InternalConstructorHelperArguments, text, textPlaceholder);
		}

		[FreeFunction("TouchScreenKeyboard_InternalConstructorHelper")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr TouchScreenKeyboard_InternalConstructorHelper(ref TouchScreenKeyboard_InternalConstructorHelperArguments arguments, string text, string textPlaceholder);

		public static TouchScreenKeyboard Open(string text, [DefaultValue("TouchScreenKeyboardType.Default")] TouchScreenKeyboardType keyboardType, [DefaultValue("true")] bool autocorrection, [DefaultValue("false")] bool multiline, [DefaultValue("false")] bool secure, [DefaultValue("false")] bool alert, [DefaultValue("\"\"")] string textPlaceholder, [DefaultValue("0")] int characterLimit)
		{
			return new TouchScreenKeyboard(text, keyboardType, autocorrection, multiline, secure, alert, textPlaceholder, characterLimit);
		}

		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure, bool alert, string textPlaceholder)
		{
			int characterLimit = 0;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, secure, alert, textPlaceholder, characterLimit);
		}

		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure, bool alert)
		{
			int characterLimit = 0;
			string textPlaceholder = "";
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, secure, alert, textPlaceholder, characterLimit);
		}

		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure)
		{
			int characterLimit = 0;
			string textPlaceholder = "";
			bool alert = false;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, secure, alert, textPlaceholder, characterLimit);
		}

		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline)
		{
			int characterLimit = 0;
			string textPlaceholder = "";
			bool alert = false;
			bool secure = false;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, secure, alert, textPlaceholder, characterLimit);
		}

		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection)
		{
			int characterLimit = 0;
			string textPlaceholder = "";
			bool alert = false;
			bool secure = false;
			bool multiline = false;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, secure, alert, textPlaceholder, characterLimit);
		}

		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType)
		{
			int characterLimit = 0;
			string textPlaceholder = "";
			bool alert = false;
			bool secure = false;
			bool multiline = false;
			bool autocorrection = true;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, secure, alert, textPlaceholder, characterLimit);
		}

		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text)
		{
			int characterLimit = 0;
			string textPlaceholder = "";
			bool alert = false;
			bool secure = false;
			bool multiline = false;
			bool autocorrection = true;
			TouchScreenKeyboardType keyboardType = TouchScreenKeyboardType.Default;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, secure, alert, textPlaceholder, characterLimit);
		}

		[FreeFunction("TouchScreenKeyboard_GetDone")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetDone(IntPtr ptr);

		[FreeFunction("TouchScreenKeyboard_GetWasCanceled")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetWasCanceled(IntPtr ptr);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSelection(out int start, out int length);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSelection(int start, int length);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_area_Injected(out Rect ret);
	}
}
