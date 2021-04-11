using System;
using System.ComponentModel;
using UnityEngine.Internal;

namespace UnityEngine
{
	public sealed class Screen
	{
		public static int width
		{
			get
			{
				return ShimManager.screenShim.width;
			}
		}

		public static int height
		{
			get
			{
				return ShimManager.screenShim.height;
			}
		}

		public static float dpi
		{
			get
			{
				return ShimManager.screenShim.dpi;
			}
		}

		public static Resolution currentResolution
		{
			get
			{
				return ShimManager.screenShim.currentResolution;
			}
		}

		public static Resolution[] resolutions
		{
			get
			{
				return ShimManager.screenShim.resolutions;
			}
		}

		public static bool fullScreen
		{
			get
			{
				return ShimManager.screenShim.fullScreen;
			}
			set
			{
				ShimManager.screenShim.fullScreen = value;
			}
		}

		public static FullScreenMode fullScreenMode
		{
			get
			{
				return ShimManager.screenShim.fullScreenMode;
			}
			set
			{
				ShimManager.screenShim.fullScreenMode = value;
			}
		}

		public static Rect safeArea
		{
			get
			{
				return ShimManager.screenShim.safeArea;
			}
		}

		public static Rect[] cutouts
		{
			get
			{
				return ShimManager.screenShim.cutouts;
			}
		}

		public static bool autorotateToPortrait
		{
			get
			{
				return ShimManager.screenShim.autorotateToPortrait;
			}
			set
			{
				ShimManager.screenShim.autorotateToPortrait = value;
			}
		}

		public static bool autorotateToPortraitUpsideDown
		{
			get
			{
				return ShimManager.screenShim.autorotateToPortraitUpsideDown;
			}
			set
			{
				ShimManager.screenShim.autorotateToPortraitUpsideDown = value;
			}
		}

		public static bool autorotateToLandscapeLeft
		{
			get
			{
				return ShimManager.screenShim.autorotateToLandscapeLeft;
			}
			set
			{
				ShimManager.screenShim.autorotateToLandscapeLeft = value;
			}
		}

		public static bool autorotateToLandscapeRight
		{
			get
			{
				return ShimManager.screenShim.autorotateToLandscapeRight;
			}
			set
			{
				ShimManager.screenShim.autorotateToLandscapeRight = value;
			}
		}

		public static ScreenOrientation orientation
		{
			get
			{
				return ShimManager.screenShim.orientation;
			}
			set
			{
				ShimManager.screenShim.orientation = value;
			}
		}

		public static int sleepTimeout
		{
			get
			{
				return ShimManager.screenShim.sleepTimeout;
			}
			set
			{
				ShimManager.screenShim.sleepTimeout = value;
			}
		}

		public static float brightness
		{
			get
			{
				return ShimManager.screenShim.brightness;
			}
			set
			{
				ShimManager.screenShim.brightness = value;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Property GetResolution has been deprecated. Use resolutions instead (UnityUpgradable) -> resolutions", true)]
		public static Resolution[] GetResolution
		{
			get
			{
				return null;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Property showCursor has been deprecated. Use Cursor.visible instead (UnityUpgradable) -> UnityEngine.Cursor.visible", true)]
		public static bool showCursor
		{
			get;
			set;
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use Cursor.lockState and Cursor.visible instead.", false)]
		public static bool lockCursor
		{
			get
			{
				return CursorLockMode.Locked == Cursor.lockState;
			}
			set
			{
				if (value)
				{
					Cursor.visible = false;
					Cursor.lockState = CursorLockMode.Locked;
				}
				else
				{
					Cursor.lockState = CursorLockMode.None;
					Cursor.visible = true;
				}
			}
		}

		public static void SetResolution(int width, int height, FullScreenMode fullscreenMode, [UnityEngine.Internal.DefaultValue("0")] int preferredRefreshRate)
		{
			ShimManager.screenShim.SetResolution(width, height, fullscreenMode, preferredRefreshRate);
		}

		public static void SetResolution(int width, int height, FullScreenMode fullscreenMode)
		{
			Screen.SetResolution(width, height, fullscreenMode, 0);
		}

		public static void SetResolution(int width, int height, bool fullscreen, [UnityEngine.Internal.DefaultValue("0")] int preferredRefreshRate)
		{
			Screen.SetResolution(width, height, fullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed, preferredRefreshRate);
		}

		public static void SetResolution(int width, int height, bool fullscreen)
		{
			Screen.SetResolution(width, height, fullscreen, 0);
		}
	}
}
