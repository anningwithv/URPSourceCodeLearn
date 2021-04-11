using System;

namespace UnityEditor.Experimental
{
	public class RenderSettings
	{
		[Obsolete("Use UnityEngine.Experimental.GlobalIllumination.useRadianceAmbientProbe instead. (UnityUpgradable) -> UnityEngine.Experimental.GlobalIllumination.RenderSettings.useRadianceAmbientProbe", true)]
		public static bool useRadianceAmbientProbe
		{
			get;
			set;
		}
	}
}
