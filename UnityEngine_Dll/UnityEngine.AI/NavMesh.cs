using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.AI
{
	[NativeHeader("Modules/AI/NavMeshManager.h"), NativeHeader("Modules/AI/NavMesh/NavMesh.bindings.h"), StaticAccessor("NavMeshBindings", StaticAccessorType.DoubleColon), MovedFrom("UnityEngine")]
	public static class NavMesh
	{
		public delegate void OnNavMeshPreUpdate();

		public const int AllAreas = -1;

		public static NavMesh.OnNavMeshPreUpdate onPreUpdate;

		[StaticAccessor("GetNavMeshManager()")]
		public static extern float avoidancePredictionTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetNavMeshManager()")]
		public static extern int pathfindingIterationsPerFrame
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[RequiredByNativeCode]
		private static void Internal_CallOnNavMeshPreUpdate()
		{
			bool flag = NavMesh.onPreUpdate != null;
			if (flag)
			{
				NavMesh.onPreUpdate();
			}
		}

		public static bool Raycast(Vector3 sourcePosition, Vector3 targetPosition, out NavMeshHit hit, int areaMask)
		{
			return NavMesh.Raycast_Injected(ref sourcePosition, ref targetPosition, out hit, areaMask);
		}

		public static bool CalculatePath(Vector3 sourcePosition, Vector3 targetPosition, int areaMask, NavMeshPath path)
		{
			path.ClearCorners();
			return NavMesh.CalculatePathInternal(sourcePosition, targetPosition, areaMask, path);
		}

		private static bool CalculatePathInternal(Vector3 sourcePosition, Vector3 targetPosition, int areaMask, NavMeshPath path)
		{
			return NavMesh.CalculatePathInternal_Injected(ref sourcePosition, ref targetPosition, areaMask, path);
		}

		public static bool FindClosestEdge(Vector3 sourcePosition, out NavMeshHit hit, int areaMask)
		{
			return NavMesh.FindClosestEdge_Injected(ref sourcePosition, out hit, areaMask);
		}

		public static bool SamplePosition(Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int areaMask)
		{
			return NavMesh.SamplePosition_Injected(ref sourcePosition, out hit, maxDistance, areaMask);
		}

		[Obsolete("Use SetAreaCost instead."), NativeName("SetAreaCost"), StaticAccessor("GetNavMeshProjectSettings()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetLayerCost(int layer, float cost);

		[Obsolete("Use GetAreaCost instead."), NativeName("GetAreaCost"), StaticAccessor("GetNavMeshProjectSettings()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetLayerCost(int layer);

		[Obsolete("Use GetAreaFromName instead."), NativeName("GetAreaFromName"), StaticAccessor("GetNavMeshProjectSettings()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetNavMeshLayerFromName(string layerName);

		[NativeName("SetAreaCost"), StaticAccessor("GetNavMeshProjectSettings()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetAreaCost(int areaIndex, float cost);

		[NativeName("GetAreaCost"), StaticAccessor("GetNavMeshProjectSettings()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetAreaCost(int areaIndex);

		[NativeName("GetAreaFromName"), StaticAccessor("GetNavMeshProjectSettings()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetAreaFromName(string areaName);

		public static NavMeshTriangulation CalculateTriangulation()
		{
			NavMeshTriangulation result;
			NavMesh.CalculateTriangulation_Injected(out result);
			return result;
		}

		[Obsolete("use NavMesh.CalculateTriangulation() instead.")]
		public static void Triangulate(out Vector3[] vertices, out int[] indices)
		{
			NavMeshTriangulation navMeshTriangulation = NavMesh.CalculateTriangulation();
			vertices = navMeshTriangulation.vertices;
			indices = navMeshTriangulation.indices;
		}

		[Obsolete("AddOffMeshLinks has no effect and is deprecated.")]
		public static void AddOffMeshLinks()
		{
		}

		[Obsolete("RestoreNavMesh has no effect and is deprecated.")]
		public static void RestoreNavMesh()
		{
		}

		public static NavMeshDataInstance AddNavMeshData(NavMeshData navMeshData)
		{
			bool flag = navMeshData == null;
			if (flag)
			{
				throw new ArgumentNullException("navMeshData");
			}
			return new NavMeshDataInstance
			{
				id = NavMesh.AddNavMeshDataInternal(navMeshData)
			};
		}

		public static NavMeshDataInstance AddNavMeshData(NavMeshData navMeshData, Vector3 position, Quaternion rotation)
		{
			bool flag = navMeshData == null;
			if (flag)
			{
				throw new ArgumentNullException("navMeshData");
			}
			return new NavMeshDataInstance
			{
				id = NavMesh.AddNavMeshDataTransformedInternal(navMeshData, position, rotation)
			};
		}

		public static void RemoveNavMeshData(NavMeshDataInstance handle)
		{
			NavMesh.RemoveNavMeshDataInternal(handle.id);
		}

		[NativeName("IsValidSurfaceID"), StaticAccessor("GetNavMeshManager()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsValidNavMeshDataHandle(int handle);

		[StaticAccessor("GetNavMeshManager()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsValidLinkHandle(int handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern UnityEngine.Object InternalGetOwner(int dataID);

		[NativeName("SetSurfaceUserID"), StaticAccessor("GetNavMeshManager()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool InternalSetOwner(int dataID, int ownerID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern UnityEngine.Object InternalGetLinkOwner(int linkID);

		[NativeName("SetLinkUserID"), StaticAccessor("GetNavMeshManager()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool InternalSetLinkOwner(int linkID, int ownerID);

		[NativeName("LoadData"), StaticAccessor("GetNavMeshManager()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int AddNavMeshDataInternal(NavMeshData navMeshData);

		[NativeName("LoadData"), StaticAccessor("GetNavMeshManager()")]
		internal static int AddNavMeshDataTransformedInternal(NavMeshData navMeshData, Vector3 position, Quaternion rotation)
		{
			return NavMesh.AddNavMeshDataTransformedInternal_Injected(navMeshData, ref position, ref rotation);
		}

		[NativeName("UnloadData"), StaticAccessor("GetNavMeshManager()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void RemoveNavMeshDataInternal(int handle);

		public static NavMeshLinkInstance AddLink(NavMeshLinkData link)
		{
			return new NavMeshLinkInstance
			{
				id = NavMesh.AddLinkInternal(link, Vector3.zero, Quaternion.identity)
			};
		}

		public static NavMeshLinkInstance AddLink(NavMeshLinkData link, Vector3 position, Quaternion rotation)
		{
			return new NavMeshLinkInstance
			{
				id = NavMesh.AddLinkInternal(link, position, rotation)
			};
		}

		public static void RemoveLink(NavMeshLinkInstance handle)
		{
			NavMesh.RemoveLinkInternal(handle.id);
		}

		[NativeName("AddLink"), StaticAccessor("GetNavMeshManager()")]
		internal static int AddLinkInternal(NavMeshLinkData link, Vector3 position, Quaternion rotation)
		{
			return NavMesh.AddLinkInternal_Injected(ref link, ref position, ref rotation);
		}

		[NativeName("RemoveLink"), StaticAccessor("GetNavMeshManager()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void RemoveLinkInternal(int handle);

		public static bool SamplePosition(Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, NavMeshQueryFilter filter)
		{
			return NavMesh.SamplePositionFilter(sourcePosition, out hit, maxDistance, filter.agentTypeID, filter.areaMask);
		}

		private static bool SamplePositionFilter(Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int type, int mask)
		{
			return NavMesh.SamplePositionFilter_Injected(ref sourcePosition, out hit, maxDistance, type, mask);
		}

		public static bool FindClosestEdge(Vector3 sourcePosition, out NavMeshHit hit, NavMeshQueryFilter filter)
		{
			return NavMesh.FindClosestEdgeFilter(sourcePosition, out hit, filter.agentTypeID, filter.areaMask);
		}

		private static bool FindClosestEdgeFilter(Vector3 sourcePosition, out NavMeshHit hit, int type, int mask)
		{
			return NavMesh.FindClosestEdgeFilter_Injected(ref sourcePosition, out hit, type, mask);
		}

		public static bool Raycast(Vector3 sourcePosition, Vector3 targetPosition, out NavMeshHit hit, NavMeshQueryFilter filter)
		{
			return NavMesh.RaycastFilter(sourcePosition, targetPosition, out hit, filter.agentTypeID, filter.areaMask);
		}

		private static bool RaycastFilter(Vector3 sourcePosition, Vector3 targetPosition, out NavMeshHit hit, int type, int mask)
		{
			return NavMesh.RaycastFilter_Injected(ref sourcePosition, ref targetPosition, out hit, type, mask);
		}

		public static bool CalculatePath(Vector3 sourcePosition, Vector3 targetPosition, NavMeshQueryFilter filter, NavMeshPath path)
		{
			path.ClearCorners();
			return NavMesh.CalculatePathFilterInternal(sourcePosition, targetPosition, path, filter.agentTypeID, filter.areaMask, filter.costs);
		}

		private static bool CalculatePathFilterInternal(Vector3 sourcePosition, Vector3 targetPosition, NavMeshPath path, int type, int mask, float[] costs)
		{
			return NavMesh.CalculatePathFilterInternal_Injected(ref sourcePosition, ref targetPosition, path, type, mask, costs);
		}

		[StaticAccessor("GetNavMeshProjectSettings()")]
		public static NavMeshBuildSettings CreateSettings()
		{
			NavMeshBuildSettings result;
			NavMesh.CreateSettings_Injected(out result);
			return result;
		}

		[StaticAccessor("GetNavMeshProjectSettings()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RemoveSettings(int agentTypeID);

		public static NavMeshBuildSettings GetSettingsByID(int agentTypeID)
		{
			NavMeshBuildSettings result;
			NavMesh.GetSettingsByID_Injected(agentTypeID, out result);
			return result;
		}

		[StaticAccessor("GetNavMeshProjectSettings()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetSettingsCount();

		public static NavMeshBuildSettings GetSettingsByIndex(int index)
		{
			NavMeshBuildSettings result;
			NavMesh.GetSettingsByIndex_Injected(index, out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetSettingsNameFromID(int agentTypeID);

		[NativeName("CleanupAfterCarving"), StaticAccessor("GetNavMeshManager()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RemoveAllNavMeshData();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Raycast_Injected(ref Vector3 sourcePosition, ref Vector3 targetPosition, out NavMeshHit hit, int areaMask);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CalculatePathInternal_Injected(ref Vector3 sourcePosition, ref Vector3 targetPosition, int areaMask, NavMeshPath path);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool FindClosestEdge_Injected(ref Vector3 sourcePosition, out NavMeshHit hit, int areaMask);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SamplePosition_Injected(ref Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int areaMask);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CalculateTriangulation_Injected(out NavMeshTriangulation ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddNavMeshDataTransformedInternal_Injected(NavMeshData navMeshData, ref Vector3 position, ref Quaternion rotation);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddLinkInternal_Injected(ref NavMeshLinkData link, ref Vector3 position, ref Quaternion rotation);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SamplePositionFilter_Injected(ref Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int type, int mask);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool FindClosestEdgeFilter_Injected(ref Vector3 sourcePosition, out NavMeshHit hit, int type, int mask);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RaycastFilter_Injected(ref Vector3 sourcePosition, ref Vector3 targetPosition, out NavMeshHit hit, int type, int mask);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CalculatePathFilterInternal_Injected(ref Vector3 sourcePosition, ref Vector3 targetPosition, NavMeshPath path, int type, int mask, float[] costs);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateSettings_Injected(out NavMeshBuildSettings ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSettingsByID_Injected(int agentTypeID, out NavMeshBuildSettings ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSettingsByIndex_Injected(int index, out NavMeshBuildSettings ret);
	}
}
