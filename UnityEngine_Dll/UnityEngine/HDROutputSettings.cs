using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/GfxDevice/HDROutputSettings.h"), UsedByNativeCode]
	public class HDROutputSettings
	{
		private int m_DisplayIndex;

		public static HDROutputSettings[] displays = new HDROutputSettings[]
		{
			new HDROutputSettings()
		};

		private static HDROutputSettings _mainDisplay = HDROutputSettings.displays[0];

		public static HDROutputSettings main
		{
			get
			{
				return HDROutputSettings._mainDisplay;
			}
		}

		public bool active
		{
			get
			{
				return HDROutputSettings.GetActive(this.m_DisplayIndex);
			}
		}

		public bool available
		{
			get
			{
				return HDROutputSettings.GetAvailable(this.m_DisplayIndex);
			}
		}

		public bool automaticHDRTonemapping
		{
			get
			{
				return HDROutputSettings.GetAutomaticHDRTonemapping(this.m_DisplayIndex);
			}
			set
			{
				HDROutputSettings.SetAutomaticHDRTonemapping(this.m_DisplayIndex, value);
			}
		}

		public ColorGamut displayColorGamut
		{
			get
			{
				return HDROutputSettings.GetDisplayColorGamut(this.m_DisplayIndex);
			}
		}

		public RenderTextureFormat format
		{
			get
			{
				return GraphicsFormatUtility.GetRenderTextureFormat(HDROutputSettings.GetGraphicsFormat(this.m_DisplayIndex));
			}
		}

		public GraphicsFormat graphicsFormat
		{
			get
			{
				return HDROutputSettings.GetGraphicsFormat(this.m_DisplayIndex);
			}
		}

		public float paperWhiteNits
		{
			get
			{
				return HDROutputSettings.GetPaperWhiteNits(this.m_DisplayIndex);
			}
			set
			{
				HDROutputSettings.SetPaperWhiteNits(this.m_DisplayIndex, value);
			}
		}

		public int maxFullFrameToneMapLuminance
		{
			get
			{
				return HDROutputSettings.GetMaxFullFrameToneMapLuminance(this.m_DisplayIndex);
			}
		}

		public int maxToneMapLuminance
		{
			get
			{
				return HDROutputSettings.GetMaxToneMapLuminance(this.m_DisplayIndex);
			}
		}

		public int minToneMapLuminance
		{
			get
			{
				return HDROutputSettings.GetMinToneMapLuminance(this.m_DisplayIndex);
			}
		}

		public bool HDRModeChangeRequested
		{
			get
			{
				return HDROutputSettings.GetHDRModeChangeRequested(this.m_DisplayIndex);
			}
		}

		internal HDROutputSettings()
		{
			this.m_DisplayIndex = 0;
		}

		internal HDROutputSettings(int displayIndex)
		{
			this.m_DisplayIndex = displayIndex;
		}

		public void RequestHDRModeChange(bool enabled)
		{
			HDROutputSettings.RequestHDRModeChangeInternal(this.m_DisplayIndex, enabled);
		}

		[Obsolete("SetPaperWhiteInNits is deprecated, please use paperWhiteNits instead.")]
		public static void SetPaperWhiteInNits(float paperWhite)
		{
			int displayIndex = 0;
			bool available = HDROutputSettings.GetAvailable(displayIndex);
			if (available)
			{
				HDROutputSettings.SetPaperWhiteNits(displayIndex, paperWhite);
			}
		}

		[FreeFunction("HDROutputSettingsBindings::GetActive", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetActive(int displayIndex);

		[FreeFunction("HDROutputSettingsBindings::GetAvailable", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetAvailable(int displayIndex);

		[FreeFunction("HDROutputSettingsBindings::GetAutomaticHDRTonemapping", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetAutomaticHDRTonemapping(int displayIndex);

		[FreeFunction("HDROutputSettingsBindings::SetAutomaticHDRTonemapping", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAutomaticHDRTonemapping(int displayIndex, bool scripted);

		[FreeFunction("HDROutputSettingsBindings::GetDisplayColorGamut", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ColorGamut GetDisplayColorGamut(int displayIndex);

		[FreeFunction("HDROutputSettingsBindings::GetGraphicsFormat", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsFormat GetGraphicsFormat(int displayIndex);

		[FreeFunction("HDROutputSettingsBindings::GetPaperWhiteNits", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetPaperWhiteNits(int displayIndex);

		[FreeFunction("HDROutputSettingsBindings::SetPaperWhiteNits", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPaperWhiteNits(int displayIndex, float paperWhite);

		[FreeFunction("HDROutputSettingsBindings::GetMaxFullFrameToneMapLuminance", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxFullFrameToneMapLuminance(int displayIndex);

		[FreeFunction("HDROutputSettingsBindings::GetMaxToneMapLuminance", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxToneMapLuminance(int displayIndex);

		[FreeFunction("HDROutputSettingsBindings::GetMinToneMapLuminance", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMinToneMapLuminance(int displayIndex);

		[FreeFunction("HDROutputSettingsBindings::GetHDRModeChangeRequested", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetHDRModeChangeRequested(int displayIndex);

		[FreeFunction("HDROutputSettingsBindings::RequestHDRModeChange", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RequestHDRModeChangeInternal(int displayIndex, bool enabled);
	}
}
