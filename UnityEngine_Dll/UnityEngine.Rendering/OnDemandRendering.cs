using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	[RequiredByNativeCode]
	public class OnDemandRendering
	{
		private static int m_RenderFrameInterval = 1;

		public static bool willCurrentFrameRender
		{
			get
			{
				return Time.frameCount % OnDemandRendering.renderFrameInterval == 0;
			}
		}

		public static int renderFrameInterval
		{
			get
			{
				return OnDemandRendering.m_RenderFrameInterval;
			}
			set
			{
				OnDemandRendering.m_RenderFrameInterval = Math.Max(1, value);
			}
		}

		public static int effectiveRenderFrameRate
		{
			get
			{
				bool flag = QualitySettings.vSyncCount > 0;
				int result;
				if (flag)
				{
					result = Screen.currentResolution.refreshRate / QualitySettings.vSyncCount / OnDemandRendering.renderFrameInterval;
				}
				else
				{
					bool flag2 = Application.targetFrameRate <= 0;
					if (flag2)
					{
						result = Application.targetFrameRate;
					}
					else
					{
						result = Application.targetFrameRate / OnDemandRendering.renderFrameInterval;
					}
				}
				return result;
			}
		}

		[RequiredByNativeCode]
		internal static void GetRenderFrameInterval(out int frameInterval)
		{
			frameInterval = OnDemandRendering.renderFrameInterval;
		}
	}
}
