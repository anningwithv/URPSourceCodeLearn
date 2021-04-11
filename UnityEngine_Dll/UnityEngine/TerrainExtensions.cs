using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	public static class TerrainExtensions
	{
		public static void UpdateGIMaterials(this Terrain terrain)
		{
			bool flag = terrain.terrainData == null;
			if (flag)
			{
				throw new ArgumentException("Invalid terrainData.");
			}
			TerrainExtensions.UpdateGIMaterialsForTerrain(terrain.GetInstanceID(), new Rect(0f, 0f, 1f, 1f));
		}

		public static void UpdateGIMaterials(this Terrain terrain, int x, int y, int width, int height)
		{
			bool flag = terrain.terrainData == null;
			if (flag)
			{
				throw new ArgumentException("Invalid terrainData.");
			}
			float num = (float)terrain.terrainData.alphamapWidth;
			float num2 = (float)terrain.terrainData.alphamapHeight;
			TerrainExtensions.UpdateGIMaterialsForTerrain(terrain.GetInstanceID(), new Rect((float)x / num, (float)y / num2, (float)width / num, (float)height / num2));
		}

		[FreeFunction, NativeConditional("INCLUDE_DYNAMIC_GI && ENABLE_RUNTIME_GI")]
		internal static void UpdateGIMaterialsForTerrain(int terrainInstanceID, Rect uvBounds)
		{
			TerrainExtensions.UpdateGIMaterialsForTerrain_Injected(terrainInstanceID, ref uvBounds);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateGIMaterialsForTerrain_Injected(int terrainInstanceID, ref Rect uvBounds);
	}
}
