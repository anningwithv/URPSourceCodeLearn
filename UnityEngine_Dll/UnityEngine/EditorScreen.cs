using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/ScreenManager.h"), NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h"), StaticAccessor("GetScreenManager()", StaticAccessorType.Dot)]
	internal sealed class EditorScreen
	{
		public static extern int width
		{
			[NativeMethod(Name = "GetWidth", IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern int height
		{
			[NativeMethod(Name = "GetHeight", IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern float dpi
		{
			[NativeName("GetDPI")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static ScreenOrientation orientation
		{
			get
			{
				return EditorScreen.GetScreenOrientation();
			}
			set
			{
				bool flag = value == ScreenOrientation.Unknown;
				if (flag)
				{
					Debug.Log("ScreenOrientation.Unknown is deprecated. Please use ScreenOrientation.AutoRotation");
					value = ScreenOrientation.AutoRotation;
				}
				EditorScreen.RequestOrientation(value);
			}
		}

		[NativeProperty("ScreenTimeout")]
		public static extern int sleepTimeout
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static bool autorotateToPortrait
		{
			get
			{
				return EditorScreen.IsOrientationEnabled(EnabledOrientation.kAutorotateToPortrait);
			}
			set
			{
				EditorScreen.SetOrientationEnabled(EnabledOrientation.kAutorotateToPortrait, value);
			}
		}

		public static bool autorotateToPortraitUpsideDown
		{
			get
			{
				return EditorScreen.IsOrientationEnabled(EnabledOrientation.kAutorotateToPortraitUpsideDown);
			}
			set
			{
				EditorScreen.SetOrientationEnabled(EnabledOrientation.kAutorotateToPortraitUpsideDown, value);
			}
		}

		public static bool autorotateToLandscapeLeft
		{
			get
			{
				return EditorScreen.IsOrientationEnabled(EnabledOrientation.kAutorotateToLandscapeLeft);
			}
			set
			{
				EditorScreen.SetOrientationEnabled(EnabledOrientation.kAutorotateToLandscapeLeft, value);
			}
		}

		public static bool autorotateToLandscapeRight
		{
			get
			{
				return EditorScreen.IsOrientationEnabled(EnabledOrientation.kAutorotateToLandscapeRight);
			}
			set
			{
				EditorScreen.SetOrientationEnabled(EnabledOrientation.kAutorotateToLandscapeRight, value);
			}
		}

		public static Resolution currentResolution
		{
			get
			{
				Resolution result;
				EditorScreen.get_currentResolution_Injected(out result);
				return result;
			}
		}

		public static extern bool fullScreen
		{
			[NativeName("IsFullscreen")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("RequestSetFullscreenFromScript")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern FullScreenMode fullScreenMode
		{
			[NativeName("GetFullscreenMode")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("RequestSetFullscreenModeFromScript")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static Rect safeArea
		{
			get
			{
				Rect result;
				EditorScreen.get_safeArea_Injected(out result);
				return result;
			}
		}

		public static extern Rect[] cutouts
		{
			[FreeFunction("ScreenScripting::GetCutouts")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern Resolution[] resolutions
		{
			[FreeFunction("ScreenScripting::GetResolutions")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern float brightness
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RequestOrientation(ScreenOrientation orient);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ScreenOrientation GetScreenOrientation();

		[NativeName("GetIsOrientationEnabled")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsOrientationEnabled(EnabledOrientation orient);

		[NativeName("SetIsOrientationEnabled")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetOrientationEnabled(EnabledOrientation orient, bool enabled);

		[NativeName("RequestResolution")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetResolution(int width, int height, FullScreenMode fullscreenMode, [DefaultValue("0")] int preferredRefreshRate);

		public static void SetResolution(int width, int height, FullScreenMode fullscreenMode)
		{
			EditorScreen.SetResolution(width, height, fullscreenMode, 0);
		}

		public static void SetResolution(int width, int height, bool fullscreen, [DefaultValue("0")] int preferredRefreshRate)
		{
			EditorScreen.SetResolution(width, height, fullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed, preferredRefreshRate);
		}

		public static void SetResolution(int width, int height, bool fullscreen)
		{
			EditorScreen.SetResolution(width, height, fullscreen, 0);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_currentResolution_Injected(out Resolution ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_safeArea_Injected(out Rect ret);
	}
}
