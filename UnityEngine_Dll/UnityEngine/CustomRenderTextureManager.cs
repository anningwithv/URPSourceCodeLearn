using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/CustomRenderTextureManager.h")]
	public static class CustomRenderTextureManager
	{
		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<CustomRenderTexture> textureLoaded;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<CustomRenderTexture> textureUnloaded;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<CustomRenderTexture, int> updateTriggered;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<CustomRenderTexture> initializeTriggered;

		[RequiredByNativeCode]
		private static void InvokeOnTextureLoaded_Internal(CustomRenderTexture source)
		{
			Action<CustomRenderTexture> expr_05 = CustomRenderTextureManager.textureLoaded;
			if (expr_05 != null)
			{
				expr_05(source);
			}
		}

		[RequiredByNativeCode]
		private static void InvokeOnTextureUnloaded_Internal(CustomRenderTexture source)
		{
			Action<CustomRenderTexture> expr_05 = CustomRenderTextureManager.textureUnloaded;
			if (expr_05 != null)
			{
				expr_05(source);
			}
		}

		[FreeFunction(Name = "CustomRenderTextureManagerScripting::GetAllCustomRenderTextures", HasExplicitThis = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void GetAllCustomRenderTextures(List<CustomRenderTexture> currentCustomRenderTextures);

		internal static void InvokeTriggerUpdate(CustomRenderTexture crt, int updateCount)
		{
			Action<CustomRenderTexture, int> expr_05 = CustomRenderTextureManager.updateTriggered;
			if (expr_05 != null)
			{
				expr_05(crt, updateCount);
			}
		}

		internal static void InvokeTriggerInitialize(CustomRenderTexture crt)
		{
			Action<CustomRenderTexture> expr_05 = CustomRenderTextureManager.initializeTriggered;
			if (expr_05 != null)
			{
				expr_05(crt);
			}
		}
	}
}
