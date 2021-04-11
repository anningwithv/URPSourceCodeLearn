using System;

namespace UnityEngine.UIElements.UIR
{
	internal static class Shaders
	{
		public static readonly string k_AtlasBlit;

		public static readonly string k_Editor;

		public static readonly string k_Runtime;

		public static readonly string k_RuntimeWorld;

		public static readonly string k_GraphView;

		static Shaders()
		{
			bool isUIEPackageLoaded = UIElementsPackageUtility.IsUIEPackageLoaded;
			if (isUIEPackageLoaded)
			{
				Shaders.k_AtlasBlit = "Hidden/UIE-AtlasBlit";
				Shaders.k_Editor = "Hidden/UIE-Editor";
				Shaders.k_Runtime = "Hidden/UIE-Runtime";
				Shaders.k_RuntimeWorld = "Hidden/UIE-RuntimeWorld";
				Shaders.k_GraphView = "Hidden/UIE-GraphView";
			}
			else
			{
				Shaders.k_AtlasBlit = "Hidden/Internal-UIRAtlasBlitCopy";
				Shaders.k_Editor = "Hidden/UIElements/EditorUIE";
				Shaders.k_Runtime = "Hidden/Internal-UIRDefault";
				Shaders.k_RuntimeWorld = "Hidden/Internal-UIRDefaultWorld";
				Shaders.k_GraphView = "Hidden/GraphView/GraphViewUIE";
			}
		}
	}
}
