using System;
using UnityEngine;

namespace UnityEditor.UIElements
{
	internal static class EditorAtlasMonitorBridge
	{
		public static Action<string[], string[], string[], string[]> OnPostprocessAllAssets;

		public static Action<Texture2D> OnPostprocessTexture;

		public static void StaticInit()
		{
			PackageEditorAtlasMonitor.StaticInit();
		}
	}
}
