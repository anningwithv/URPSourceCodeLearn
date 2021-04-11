using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Input/GetInput.h")]
	public class AndroidInput
	{
		public static int touchCountSecondary
		{
			get
			{
				return AndroidInput.GetTouchCount_Bindings();
			}
		}

		public static bool secondaryTouchEnabled
		{
			get
			{
				return AndroidInput.IsInputDeviceEnabled_Bindings();
			}
		}

		public static int secondaryTouchWidth
		{
			get
			{
				return AndroidInput.GetTouchpadWidth();
			}
		}

		public static int secondaryTouchHeight
		{
			get
			{
				return AndroidInput.GetTouchpadHeight();
			}
		}

		private AndroidInput()
		{
		}

		public static Touch GetSecondaryTouch(int index)
		{
			return default(Touch);
		}

		[FreeFunction, NativeConditional("PLATFORM_ANDROID")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetTouchCount_Bindings();

		[FreeFunction, NativeConditional("PLATFORM_ANDROID")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsInputDeviceEnabled_Bindings();

		[FreeFunction, NativeConditional("PLATFORM_ANDROID")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetTouchpadWidth();

		[FreeFunction, NativeConditional("PLATFORM_ANDROID")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetTouchpadHeight();
	}
}
