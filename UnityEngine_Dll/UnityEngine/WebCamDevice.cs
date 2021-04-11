using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[UsedByNativeCode]
	public struct WebCamDevice
	{
		[NativeName("name")]
		internal string m_Name;

		[NativeName("depthCameraName")]
		internal string m_DepthCameraName;

		[NativeName("flags")]
		internal int m_Flags;

		[NativeName("kind")]
		internal WebCamKind m_Kind;

		[NativeName("resolutions")]
		internal Resolution[] m_Resolutions;

		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		public bool isFrontFacing
		{
			get
			{
				return (this.m_Flags & 1) != 0;
			}
		}

		public WebCamKind kind
		{
			get
			{
				return this.m_Kind;
			}
		}

		public string depthCameraName
		{
			get
			{
				return (this.m_DepthCameraName == "") ? null : this.m_DepthCameraName;
			}
		}

		public bool isAutoFocusPointSupported
		{
			get
			{
				return (this.m_Flags & 2) != 0;
			}
		}

		public Resolution[] availableResolutions
		{
			get
			{
				return this.m_Resolutions;
			}
		}
	}
}
