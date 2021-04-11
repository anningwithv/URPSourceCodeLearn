using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	[NativeHeader("Runtime/Video/BaseWebCamTexture.h"), NativeHeader("AudioScriptingClasses.h"), NativeHeader("Runtime/Video/ScriptBindings/WebCamTexture.bindings.h")]
	public sealed class WebCamTexture : Texture
	{
		public static extern WebCamDevice[] devices
		{
			[NativeName("Internal_GetDevices"), StaticAccessor("WebCamTextureBindings", StaticAccessorType.DoubleColon)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool isPlaying
		{
			[NativeName("IsPlaying")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeName("Device")]
		public extern string deviceName
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float requestedFPS
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int requestedWidth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int requestedHeight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int videoRotationAngle
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool videoVerticallyMirrored
		{
			[NativeName("IsVideoVerticallyMirrored")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool didUpdateThisFrame
		{
			[NativeName("DidUpdateThisFrame")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public Vector2? autoFocusPoint
		{
			get
			{
				return (this.internalAutoFocusPoint.x < 0f) ? null : new Vector2?(this.internalAutoFocusPoint);
			}
			set
			{
				this.internalAutoFocusPoint = ((!value.HasValue) ? new Vector2(-1f, -1f) : value.Value);
			}
		}

		internal Vector2 internalAutoFocusPoint
		{
			get
			{
				Vector2 result;
				this.get_internalAutoFocusPoint_Injected(out result);
				return result;
			}
			set
			{
				this.set_internalAutoFocusPoint_Injected(ref value);
			}
		}

		public extern bool isDepth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public WebCamTexture(string deviceName, int requestedWidth, int requestedHeight, int requestedFPS)
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, deviceName, requestedWidth, requestedHeight, requestedFPS);
		}

		public WebCamTexture(string deviceName, int requestedWidth, int requestedHeight)
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, deviceName, requestedWidth, requestedHeight, 0);
		}

		public WebCamTexture(string deviceName)
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, deviceName, 0, 0, 0);
		}

		public WebCamTexture(int requestedWidth, int requestedHeight, int requestedFPS)
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, "", requestedWidth, requestedHeight, requestedFPS);
		}

		public WebCamTexture(int requestedWidth, int requestedHeight)
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, "", requestedWidth, requestedHeight, 0);
		}

		public WebCamTexture()
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, "", 0, 0, 0);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Play();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Pause();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Stop();

		public Color GetPixel(int x, int y)
		{
			Color result;
			this.GetPixel_Injected(x, y, out result);
			return result;
		}

		public Color[] GetPixels()
		{
			return this.GetPixels(0, 0, this.width, this.height);
		}

		[FreeFunction("WebCamTextureBindings::Internal_GetPixels", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color[] GetPixels(int x, int y, int blockWidth, int blockHeight);

		[ExcludeFromDocs]
		public Color32[] GetPixels32()
		{
			return this.GetPixels32(null);
		}

		[FreeFunction("WebCamTextureBindings::Internal_GetPixels32", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color32[] GetPixels32([DefaultValue("null")] Color32[] colors);

		[StaticAccessor("WebCamTextureBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateWebCamTexture([Writable] WebCamTexture self, string scriptingDevice, int requestedWidth, int requestedHeight, int maxFramerate);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPixel_Injected(int x, int y, out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_internalAutoFocusPoint_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_internalAutoFocusPoint_Injected(ref Vector2 value);
	}
}
