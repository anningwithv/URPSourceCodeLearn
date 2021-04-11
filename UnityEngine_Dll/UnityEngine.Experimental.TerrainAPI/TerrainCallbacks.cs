using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.TerrainAPI
{
	public static class TerrainCallbacks
	{
		public delegate void HeightmapChangedCallback(Terrain terrain, RectInt heightRegion, bool synched);

		public delegate void TextureChangedCallback(Terrain terrain, string textureName, RectInt texelRegion, bool synched);

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event TerrainCallbacks.HeightmapChangedCallback heightmapChanged;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event TerrainCallbacks.TextureChangedCallback textureChanged;

		[RequiredByNativeCode]
		internal static void InvokeHeightmapChangedCallback(TerrainData terrainData, RectInt heightRegion, bool synched)
		{
			bool flag = TerrainCallbacks.heightmapChanged != null;
			if (flag)
			{
				Terrain[] users = terrainData.users;
				for (int i = 0; i < users.Length; i++)
				{
					Terrain terrain = users[i];
					TerrainCallbacks.heightmapChanged(terrain, heightRegion, synched);
				}
			}
		}

		[RequiredByNativeCode]
		internal static void InvokeTextureChangedCallback(TerrainData terrainData, string textureName, RectInt texelRegion, bool synched)
		{
			bool flag = TerrainCallbacks.textureChanged != null;
			if (flag)
			{
				Terrain[] users = terrainData.users;
				for (int i = 0; i < users.Length; i++)
				{
					Terrain terrain = users[i];
					TerrainCallbacks.textureChanged(terrain, textureName, texelRegion, synched);
				}
			}
		}
	}
}
