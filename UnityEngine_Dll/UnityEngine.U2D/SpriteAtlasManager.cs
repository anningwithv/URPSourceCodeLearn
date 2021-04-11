using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.U2D
{
	[NativeHeader("Runtime/2D/SpriteAtlas/SpriteAtlas.h"), NativeHeader("Runtime/2D/SpriteAtlas/SpriteAtlasManager.h"), StaticAccessor("GetSpriteAtlasManager()", StaticAccessorType.Dot)]
	public class SpriteAtlasManager
	{
		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<string, Action<SpriteAtlas>> atlasRequested;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<SpriteAtlas> atlasRegistered;

		[RequiredByNativeCode]
		private static bool RequestAtlas(string tag)
		{
			bool flag = SpriteAtlasManager.atlasRequested != null;
			bool result;
			if (flag)
			{
				SpriteAtlasManager.atlasRequested(tag, new Action<SpriteAtlas>(SpriteAtlasManager.Register));
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		[RequiredByNativeCode]
		private static void PostRegisteredAtlas(SpriteAtlas spriteAtlas)
		{
			Action<SpriteAtlas> expr_06 = SpriteAtlasManager.atlasRegistered;
			if (expr_06 != null)
			{
				expr_06(spriteAtlas);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Register(SpriteAtlas spriteAtlas);

		static SpriteAtlasManager()
		{
			// Note: this type is marked as 'beforefieldinit'.
			SpriteAtlasManager.atlasRequested = null;
			SpriteAtlasManager.atlasRegistered = null;
		}
	}
}
