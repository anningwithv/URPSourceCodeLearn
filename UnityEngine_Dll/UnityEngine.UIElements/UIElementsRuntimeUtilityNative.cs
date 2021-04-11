using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.UIElements
{
	[NativeHeader("Modules/UIElementsNative/UIElementsRuntimeUtilityNative.h"), VisibleToOtherModules(new string[]
	{
		"Unity.UIElements"
	})]
	internal static class UIElementsRuntimeUtilityNative
	{
		internal static Action RepaintOverlayPanelsCallback;

		internal static Action UpdateRuntimePanelsCallback;

		[RequiredByNativeCode]
		public static void RepaintOverlayPanels()
		{
			Action expr_06 = UIElementsRuntimeUtilityNative.RepaintOverlayPanelsCallback;
			if (expr_06 != null)
			{
				expr_06();
			}
		}

		[RequiredByNativeCode]
		public static void UpdateRuntimePanels()
		{
			Action expr_06 = UIElementsRuntimeUtilityNative.UpdateRuntimePanelsCallback;
			if (expr_06 != null)
			{
				expr_06();
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RegisterPlayerloopCallback();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UnregisterPlayerloopCallback();
	}
}
