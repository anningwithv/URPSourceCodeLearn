using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.AI
{
	[NativeHeader("Modules/AI/Builder/NavMeshBuilder.bindings.h"), StaticAccessor("NavMeshBuilderBindings", StaticAccessorType.DoubleColon)]
	public static class NavMeshBuilder
	{
		public static void CollectSources(Bounds includedWorldBounds, int includedLayerMask, NavMeshCollectGeometry geometry, int defaultArea, List<NavMeshBuildMarkup> markups, List<NavMeshBuildSource> results)
		{
			bool flag = markups == null;
			if (flag)
			{
				throw new ArgumentNullException("markups");
			}
			bool flag2 = results == null;
			if (flag2)
			{
				throw new ArgumentNullException("results");
			}
			includedWorldBounds.extents = Vector3.Max(includedWorldBounds.extents, 0.001f * Vector3.one);
			NavMeshBuildSource[] collection = NavMeshBuilder.CollectSourcesInternal(includedLayerMask, includedWorldBounds, null, true, geometry, defaultArea, markups.ToArray());
			results.Clear();
			results.AddRange(collection);
		}

		public static void CollectSources(Transform root, int includedLayerMask, NavMeshCollectGeometry geometry, int defaultArea, List<NavMeshBuildMarkup> markups, List<NavMeshBuildSource> results)
		{
			bool flag = markups == null;
			if (flag)
			{
				throw new ArgumentNullException("markups");
			}
			bool flag2 = results == null;
			if (flag2)
			{
				throw new ArgumentNullException("results");
			}
			NavMeshBuildSource[] collection = NavMeshBuilder.CollectSourcesInternal(includedLayerMask, default(Bounds), root, false, geometry, defaultArea, markups.ToArray());
			results.Clear();
			results.AddRange(collection);
		}

		private static NavMeshBuildSource[] CollectSourcesInternal(int includedLayerMask, Bounds includedWorldBounds, Transform root, bool useBounds, NavMeshCollectGeometry geometry, int defaultArea, NavMeshBuildMarkup[] markups)
		{
			return NavMeshBuilder.CollectSourcesInternal_Injected(includedLayerMask, ref includedWorldBounds, root, useBounds, geometry, defaultArea, markups);
		}

		public static NavMeshData BuildNavMeshData(NavMeshBuildSettings buildSettings, List<NavMeshBuildSource> sources, Bounds localBounds, Vector3 position, Quaternion rotation)
		{
			bool flag = sources == null;
			if (flag)
			{
				throw new ArgumentNullException("sources");
			}
			NavMeshData navMeshData = new NavMeshData(buildSettings.agentTypeID)
			{
				position = position,
				rotation = rotation
			};
			NavMeshBuilder.UpdateNavMeshDataListInternal(navMeshData, buildSettings, sources, localBounds);
			return navMeshData;
		}

		public static bool UpdateNavMeshData(NavMeshData data, NavMeshBuildSettings buildSettings, List<NavMeshBuildSource> sources, Bounds localBounds)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = sources == null;
			if (flag2)
			{
				throw new ArgumentNullException("sources");
			}
			return NavMeshBuilder.UpdateNavMeshDataListInternal(data, buildSettings, sources, localBounds);
		}

		private static bool UpdateNavMeshDataListInternal(NavMeshData data, NavMeshBuildSettings buildSettings, object sources, Bounds localBounds)
		{
			return NavMeshBuilder.UpdateNavMeshDataListInternal_Injected(data, ref buildSettings, sources, ref localBounds);
		}

		public static AsyncOperation UpdateNavMeshDataAsync(NavMeshData data, NavMeshBuildSettings buildSettings, List<NavMeshBuildSource> sources, Bounds localBounds)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = sources == null;
			if (flag2)
			{
				throw new ArgumentNullException("sources");
			}
			return NavMeshBuilder.UpdateNavMeshDataAsyncListInternal(data, buildSettings, sources, localBounds);
		}

		[NativeHeader("Modules/AI/NavMeshManager.h"), NativeMethod("Purge"), StaticAccessor("GetNavMeshManager().GetNavMeshBuildManager()", StaticAccessorType.Arrow)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Cancel(NavMeshData data);

		private static AsyncOperation UpdateNavMeshDataAsyncListInternal(NavMeshData data, NavMeshBuildSettings buildSettings, object sources, Bounds localBounds)
		{
			return NavMeshBuilder.UpdateNavMeshDataAsyncListInternal_Injected(data, ref buildSettings, sources, ref localBounds);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern NavMeshBuildSource[] CollectSourcesInternal_Injected(int includedLayerMask, ref Bounds includedWorldBounds, Transform root, bool useBounds, NavMeshCollectGeometry geometry, int defaultArea, NavMeshBuildMarkup[] markups);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool UpdateNavMeshDataListInternal_Injected(NavMeshData data, ref NavMeshBuildSettings buildSettings, object sources, ref Bounds localBounds);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AsyncOperation UpdateNavMeshDataAsyncListInternal_Injected(NavMeshData data, ref NavMeshBuildSettings buildSettings, object sources, ref Bounds localBounds);
	}
}
