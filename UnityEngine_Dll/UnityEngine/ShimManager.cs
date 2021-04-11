using System;

namespace UnityEngine
{
	internal class ShimManager
	{
		internal static Action ActiveShimChanged;

		private static ScreenShimBase s_ActiveScreenShim;

		private static readonly ScreenShimBase s_DefaultScreenShim = new ScreenShimBase();

		private static SystemInfoShimBase s_ActiveSystemInfoShim;

		private static readonly SystemInfoShimBase s_DefaultSystemInfoShim = new SystemInfoShimBase();

		private static ApplicationShimBase s_ActiveApplicationShim;

		private static readonly ApplicationShimBase s_DefaultApplicationShim = new ApplicationShimBase();

		internal static ScreenShimBase screenShim
		{
			get
			{
				return ShimManager.s_ActiveScreenShim ?? ShimManager.s_DefaultScreenShim;
			}
		}

		internal static SystemInfoShimBase systemInfoShim
		{
			get
			{
				return ShimManager.s_ActiveSystemInfoShim ?? ShimManager.s_DefaultSystemInfoShim;
			}
		}

		internal static ApplicationShimBase applicationShim
		{
			get
			{
				return ShimManager.s_ActiveApplicationShim ?? ShimManager.s_DefaultApplicationShim;
			}
		}

		internal static void UseShim(ScreenShimBase shim)
		{
			ShimManager.s_ActiveScreenShim = shim;
			Action expr_0C = ShimManager.ActiveShimChanged;
			if (expr_0C != null)
			{
				expr_0C();
			}
		}

		internal static void UseShim(SystemInfoShimBase shim)
		{
			ShimManager.s_ActiveSystemInfoShim = shim;
			Action expr_0C = ShimManager.ActiveShimChanged;
			if (expr_0C != null)
			{
				expr_0C();
			}
		}

		internal static void UseShim(ApplicationShimBase shim)
		{
			ShimManager.s_ActiveApplicationShim = shim;
			Action expr_0C = ShimManager.ActiveShimChanged;
			if (expr_0C != null)
			{
				expr_0C();
			}
		}

		internal static void RemoveShim(ScreenShimBase shim)
		{
			bool flag = ShimManager.s_ActiveScreenShim == shim;
			if (flag)
			{
				ShimManager.s_ActiveScreenShim = null;
				Action expr_19 = ShimManager.ActiveShimChanged;
				if (expr_19 != null)
				{
					expr_19();
				}
			}
		}

		internal static void RemoveShim(SystemInfoShimBase shim)
		{
			bool flag = ShimManager.s_ActiveSystemInfoShim == shim;
			if (flag)
			{
				ShimManager.s_ActiveSystemInfoShim = null;
				Action expr_19 = ShimManager.ActiveShimChanged;
				if (expr_19 != null)
				{
					expr_19();
				}
			}
		}

		internal static void RemoveShim(ApplicationShimBase shim)
		{
			bool flag = ShimManager.s_ActiveApplicationShim == shim;
			if (flag)
			{
				ShimManager.s_ActiveApplicationShim = null;
				Action expr_19 = ShimManager.ActiveShimChanged;
				if (expr_19 != null)
				{
					expr_19();
				}
			}
		}

		internal static bool IsShimActive(ScreenShimBase shim)
		{
			return ShimManager.s_ActiveScreenShim == shim;
		}

		internal static bool IsShimActive(SystemInfoShimBase shim)
		{
			return ShimManager.s_ActiveSystemInfoShim == shim;
		}

		internal static bool IsShimActive(ApplicationShimBase shim)
		{
			return ShimManager.s_ActiveApplicationShim == shim;
		}

		internal bool IsScreenShimActive()
		{
			return ShimManager.s_ActiveScreenShim != null;
		}

		internal bool IsSystemInfoShimActive()
		{
			return ShimManager.s_ActiveSystemInfoShim != null;
		}

		internal bool IsApplicationShimActive()
		{
			return ShimManager.s_ActiveApplicationShim != null;
		}
	}
}
