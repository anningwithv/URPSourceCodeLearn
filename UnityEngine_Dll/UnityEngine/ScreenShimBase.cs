using System;

namespace UnityEngine
{
	internal class ScreenShimBase : IDisposable
	{
		public virtual int width
		{
			get
			{
				return EditorScreen.width;
			}
		}

		public virtual int height
		{
			get
			{
				return EditorScreen.height;
			}
		}

		public virtual float dpi
		{
			get
			{
				return EditorScreen.dpi;
			}
		}

		public virtual Resolution currentResolution
		{
			get
			{
				return EditorScreen.currentResolution;
			}
		}

		public virtual Resolution[] resolutions
		{
			get
			{
				return EditorScreen.resolutions;
			}
		}

		public virtual bool fullScreen
		{
			get
			{
				return EditorScreen.fullScreen;
			}
			set
			{
				EditorScreen.fullScreen = value;
			}
		}

		public virtual FullScreenMode fullScreenMode
		{
			get
			{
				return EditorScreen.fullScreenMode;
			}
			set
			{
				EditorScreen.fullScreenMode = value;
			}
		}

		public virtual Rect safeArea
		{
			get
			{
				return EditorScreen.safeArea;
			}
		}

		public virtual Rect[] cutouts
		{
			get
			{
				return EditorScreen.cutouts;
			}
		}

		public virtual bool autorotateToPortrait
		{
			get
			{
				return EditorScreen.autorotateToPortrait;
			}
			set
			{
				EditorScreen.autorotateToPortrait = value;
			}
		}

		public virtual bool autorotateToPortraitUpsideDown
		{
			get
			{
				return EditorScreen.autorotateToPortraitUpsideDown;
			}
			set
			{
				EditorScreen.autorotateToPortraitUpsideDown = value;
			}
		}

		public virtual bool autorotateToLandscapeLeft
		{
			get
			{
				return EditorScreen.autorotateToLandscapeLeft;
			}
			set
			{
				EditorScreen.autorotateToLandscapeLeft = value;
			}
		}

		public virtual bool autorotateToLandscapeRight
		{
			get
			{
				return EditorScreen.autorotateToLandscapeRight;
			}
			set
			{
				EditorScreen.autorotateToLandscapeRight = value;
			}
		}

		public virtual ScreenOrientation orientation
		{
			get
			{
				return EditorScreen.orientation;
			}
			set
			{
				EditorScreen.orientation = value;
			}
		}

		public virtual int sleepTimeout
		{
			get
			{
				return EditorScreen.sleepTimeout;
			}
			set
			{
				EditorScreen.sleepTimeout = value;
			}
		}

		public virtual float brightness
		{
			get
			{
				return EditorScreen.brightness;
			}
			set
			{
				EditorScreen.brightness = value;
			}
		}

		public void Dispose()
		{
			ShimManager.RemoveShim(this);
		}

		public bool IsActive()
		{
			return ShimManager.IsShimActive(this);
		}

		public virtual void SetResolution(int width, int height, FullScreenMode fullscreenMode, int preferredRefreshRate)
		{
			EditorScreen.SetResolution(width, height, fullscreenMode, preferredRefreshRate);
		}
	}
}
