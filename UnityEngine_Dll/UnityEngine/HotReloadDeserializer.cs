using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeConditional("HOT_RELOAD_AVAILABLE"), NativeType(Header = "Runtime/Export/HotReload/HotReload.bindings.h")]
	internal static class HotReloadDeserializer
	{
		[FreeFunction("HotReload::Prepare")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void PrepareHotReload();

		[FreeFunction("HotReload::Finish")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void FinishHotReload(Type[] typesToReset);

		[FreeFunction("HotReload::CreateEmptyAsset"), NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Object CreateEmptyAsset(Type type);

		[FreeFunction("HotReload::DeserializeAsset"), NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void DeserializeAsset(Object asset, byte[] data);

		[FreeFunction("HotReload::RemapInstanceIds"), NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemapInstanceIds(Object editorAsset, int[] editorToPlayerInstanceIdMapKeys, int[] editorToPlayerInstanceIdMapValues);

		internal static void RemapInstanceIds(Object editorAsset, Dictionary<int, int> editorToPlayerInstanceIdMap)
		{
			HotReloadDeserializer.RemapInstanceIds(editorAsset, editorToPlayerInstanceIdMap.Keys.ToArray<int>(), editorToPlayerInstanceIdMap.Values.ToArray<int>());
		}

		[FreeFunction("HotReload::FinalizeAssetCreation")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void FinalizeAssetCreation(Object asset);

		[FreeFunction("HotReload::GetDependencies")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Object[] GetDependencies(Object asset);

		[FreeFunction("HotReload::GetNullDependencies")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int[] GetNullDependencies(Object asset);
	}
}
