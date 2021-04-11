using System;

namespace UnityEngine.UIElements
{
	internal static class UIElementsPackageUtility
	{
		internal static readonly string EditorResourcesBasePath;

		internal static readonly bool IsUIEPackageLoaded;

		static UIElementsPackageUtility()
		{
			UIElementsPackageUtility.EditorResourcesBasePath = "";
			UIElementsPackageUtility.IsUIEPackageLoaded = false;
		}
	}
}
