using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/DisplayManager.h"), UsedByNativeCode]
	public class Display
	{
		public delegate void DisplaysUpdatedDelegate();

		internal IntPtr nativeDisplay;

		public static Display[] displays = new Display[]
		{
			new Display()
		};

		private static Display _mainDisplay = Display.displays[0];

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Display.DisplaysUpdatedDelegate onDisplaysUpdated;

		public int renderingWidth
		{
			get
			{
				int result = 0;
				int num = 0;
				Display.GetRenderingExtImpl(this.nativeDisplay, out result, out num);
				return result;
			}
		}

		public int renderingHeight
		{
			get
			{
				int num = 0;
				int result = 0;
				Display.GetRenderingExtImpl(this.nativeDisplay, out num, out result);
				return result;
			}
		}

		public int systemWidth
		{
			get
			{
				int result = 0;
				int num = 0;
				Display.GetSystemExtImpl(this.nativeDisplay, out result, out num);
				return result;
			}
		}

		public int systemHeight
		{
			get
			{
				int num = 0;
				int result = 0;
				Display.GetSystemExtImpl(this.nativeDisplay, out num, out result);
				return result;
			}
		}

		public RenderBuffer colorBuffer
		{
			get
			{
				RenderBuffer result;
				RenderBuffer renderBuffer;
				Display.GetRenderingBuffersImpl(this.nativeDisplay, out result, out renderBuffer);
				return result;
			}
		}

		public RenderBuffer depthBuffer
		{
			get
			{
				RenderBuffer renderBuffer;
				RenderBuffer result;
				Display.GetRenderingBuffersImpl(this.nativeDisplay, out renderBuffer, out result);
				return result;
			}
		}

		public bool active
		{
			get
			{
				return Display.GetActiveImpl(this.nativeDisplay);
			}
		}

		public bool requiresBlitToBackbuffer
		{
			get
			{
				int num = this.nativeDisplay.ToInt32();
				bool flag = num < HDROutputSettings.displays.Length;
				bool result;
				if (flag)
				{
					bool flag2 = HDROutputSettings.displays[num].available && HDROutputSettings.displays[num].active;
					bool flag3 = flag2;
					if (flag3)
					{
						result = true;
						return result;
					}
				}
				result = Display.RequiresBlitToBackbufferImpl(this.nativeDisplay);
				return result;
			}
		}

		public bool requiresSrgbBlitToBackbuffer
		{
			get
			{
				return Display.RequiresSrgbBlitToBackbufferImpl(this.nativeDisplay);
			}
		}

		public static Display main
		{
			get
			{
				return Display._mainDisplay;
			}
		}

		internal Display()
		{
			this.nativeDisplay = new IntPtr(0);
		}

		internal Display(IntPtr nativeDisplay)
		{
			this.nativeDisplay = nativeDisplay;
		}

		public void Activate()
		{
			Display.ActivateDisplayImpl(this.nativeDisplay, 0, 0, 60);
		}

		public void Activate(int width, int height, int refreshRate)
		{
			Display.ActivateDisplayImpl(this.nativeDisplay, width, height, refreshRate);
		}

		public void SetParams(int width, int height, int x, int y)
		{
			Display.SetParamsImpl(this.nativeDisplay, width, height, x, y);
		}

		public void SetRenderingResolution(int w, int h)
		{
			Display.SetRenderingResolutionImpl(this.nativeDisplay, w, h);
		}

		[Obsolete("MultiDisplayLicense has been deprecated.", false)]
		public static bool MultiDisplayLicense()
		{
			return true;
		}

		public static Vector3 RelativeMouseAt(Vector3 inputMouseCoordinates)
		{
			int num = 0;
			int num2 = 0;
			int x = (int)inputMouseCoordinates.x;
			int y = (int)inputMouseCoordinates.y;
			Vector3 result;
			result.z = (float)Display.RelativeMouseAtImpl(x, y, out num, out num2);
			result.x = (float)num;
			result.y = (float)num2;
			return result;
		}

		[RequiredByNativeCode]
		private static void RecreateDisplayList(IntPtr[] nativeDisplay)
		{
			bool flag = nativeDisplay.Length == 0;
			if (!flag)
			{
				Display.displays = new Display[nativeDisplay.Length];
				for (int i = 0; i < nativeDisplay.Length; i++)
				{
					Display.displays[i] = new Display(nativeDisplay[i]);
				}
				Display._mainDisplay = Display.displays[0];
			}
		}

		[RequiredByNativeCode]
		private static void FireDisplaysUpdated()
		{
			bool flag = Display.onDisplaysUpdated != null;
			if (flag)
			{
				Display.onDisplaysUpdated();
			}
		}

		[FreeFunction("UnityDisplayManager_DisplaySystemResolution")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSystemExtImpl(IntPtr nativeDisplay, out int w, out int h);

		[FreeFunction("UnityDisplayManager_DisplayRenderingResolution")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRenderingExtImpl(IntPtr nativeDisplay, out int w, out int h);

		[FreeFunction("UnityDisplayManager_GetRenderingBuffersWrapper")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRenderingBuffersImpl(IntPtr nativeDisplay, out RenderBuffer color, out RenderBuffer depth);

		[FreeFunction("UnityDisplayManager_SetRenderingResolution")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRenderingResolutionImpl(IntPtr nativeDisplay, int w, int h);

		[FreeFunction("UnityDisplayManager_ActivateDisplay")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ActivateDisplayImpl(IntPtr nativeDisplay, int width, int height, int refreshRate);

		[FreeFunction("UnityDisplayManager_SetDisplayParam")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetParamsImpl(IntPtr nativeDisplay, int width, int height, int x, int y);

		[FreeFunction("UnityDisplayManager_RelativeMouseAt")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RelativeMouseAtImpl(int x, int y, out int rx, out int ry);

		[FreeFunction("UnityDisplayManager_DisplayActive")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetActiveImpl(IntPtr nativeDisplay);

		[FreeFunction("UnityDisplayManager_RequiresBlitToBackbuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RequiresBlitToBackbufferImpl(IntPtr nativeDisplay);

		[FreeFunction("UnityDisplayManager_RequiresSRGBBlitToBackbuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RequiresSrgbBlitToBackbufferImpl(IntPtr nativeDisplay);

		static Display()
		{
			// Note: this type is marked as 'beforefieldinit'.
			Display.onDisplaysUpdated = null;
		}
	}
}
