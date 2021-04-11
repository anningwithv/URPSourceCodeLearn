using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Windows.WebCam
{
	[NativeHeader("PlatformDependent/Win/Webcam/CameraParameters.h"), MovedFrom("UnityEngine.XR.WSA.WebCam"), UsedByNativeCode]
	public struct CameraParameters
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly CameraParameters.<>c <>9 = new CameraParameters.<>c();

			public static Func<Resolution, int> <>9__20_0;

			public static Func<Resolution, int> <>9__20_1;

			public static Func<float, float> <>9__20_2;

			internal int ctor>b__20_0(Resolution res)
			{
				return res.width * res.height;
			}

			internal int ctor>b__20_1(Resolution res)
			{
				return res.width * res.height;
			}

			internal float ctor>b__20_2(float fps)
			{
				return fps;
			}
		}

		private float m_HologramOpacity;

		private float m_FrameRate;

		private int m_CameraResolutionWidth;

		private int m_CameraResolutionHeight;

		private CapturePixelFormat m_PixelFormat;

		public float hologramOpacity
		{
			get
			{
				return this.m_HologramOpacity;
			}
			set
			{
				this.m_HologramOpacity = value;
			}
		}

		public float frameRate
		{
			get
			{
				return this.m_FrameRate;
			}
			set
			{
				this.m_FrameRate = value;
			}
		}

		public int cameraResolutionWidth
		{
			get
			{
				return this.m_CameraResolutionWidth;
			}
			set
			{
				this.m_CameraResolutionWidth = value;
			}
		}

		public int cameraResolutionHeight
		{
			get
			{
				return this.m_CameraResolutionHeight;
			}
			set
			{
				this.m_CameraResolutionHeight = value;
			}
		}

		public CapturePixelFormat pixelFormat
		{
			get
			{
				return this.m_PixelFormat;
			}
			set
			{
				this.m_PixelFormat = value;
			}
		}

		public CameraParameters(WebCamMode webCamMode)
		{
			this.m_HologramOpacity = 1f;
			this.m_PixelFormat = CapturePixelFormat.BGRA32;
			this.m_FrameRate = 0f;
			this.m_CameraResolutionWidth = 0;
			this.m_CameraResolutionHeight = 0;
			bool flag = webCamMode == WebCamMode.PhotoMode;
			if (flag)
			{
				IEnumerable<Resolution> arg_59_0 = PhotoCapture.SupportedResolutions;
				Func<Resolution, int> arg_59_1;
				if ((arg_59_1 = CameraParameters.<>c.<>9__20_0) == null)
				{
					arg_59_1 = (CameraParameters.<>c.<>9__20_0 = new Func<Resolution, int>(CameraParameters.<>c.<>9.<.ctor>b__20_0));
				}
				Resolution resolution = arg_59_0.OrderByDescending(arg_59_1).First<Resolution>();
				this.m_CameraResolutionWidth = resolution.width;
				this.m_CameraResolutionHeight = resolution.height;
			}
			else
			{
				bool flag2 = webCamMode == WebCamMode.VideoMode;
				if (flag2)
				{
					IEnumerable<Resolution> arg_B4_0 = VideoCapture.SupportedResolutions;
					Func<Resolution, int> arg_B4_1;
					if ((arg_B4_1 = CameraParameters.<>c.<>9__20_1) == null)
					{
						arg_B4_1 = (CameraParameters.<>c.<>9__20_1 = new Func<Resolution, int>(CameraParameters.<>c.<>9.<.ctor>b__20_1));
					}
					Resolution resolution2 = arg_B4_0.OrderByDescending(arg_B4_1).First<Resolution>();
					IEnumerable<float> arg_E4_0 = VideoCapture.GetSupportedFrameRatesForResolution(resolution2);
					Func<float, float> arg_E4_1;
					if ((arg_E4_1 = CameraParameters.<>c.<>9__20_2) == null)
					{
						arg_E4_1 = (CameraParameters.<>c.<>9__20_2 = new Func<float, float>(CameraParameters.<>c.<>9.<.ctor>b__20_2));
					}
					float frameRate = arg_E4_0.OrderByDescending(arg_E4_1).First<float>();
					this.m_CameraResolutionWidth = resolution2.width;
					this.m_CameraResolutionHeight = resolution2.height;
					this.m_FrameRate = frameRate;
				}
			}
		}
	}
}
