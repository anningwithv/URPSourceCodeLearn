using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.AI;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.AI
{
	[NativeContainer, NativeHeader("Runtime/Math/Matrix4x4.h"), NativeHeader("Modules/AI/NavMeshExperimental.bindings.h"), NativeHeader("Modules/AI/Public/NavMeshBindingTypes.h"), StaticAccessor("NavMeshQueryBindings", StaticAccessorType.DoubleColon)]
	public struct NavMeshQuery : IDisposable
	{
		[NativeDisableUnsafePtrRestriction]
		internal IntPtr m_NavMeshQuery;

		private const string k_NoBufferAllocatedErrorMessage = "This query has no buffer allocated for pathfinding operations. Create a different NavMeshQuery with an explicit node pool size.";

		internal AtomicSafetyHandle m_Safety;

		[NativeSetClassTypeToNullOnSchedule]
		private DisposeSentinel m_DisposeSentinel;

		public NavMeshQuery(NavMeshWorld world, Allocator allocator, int pathNodePoolSize = 0)
		{
			bool flag = !world.IsValid();
			if (flag)
			{
				throw new ArgumentNullException("world", "Invalid world");
			}
			this.m_NavMeshQuery = NavMeshQuery.Create(world, pathNodePoolSize);
			DisposeSentinel.Create(out this.m_Safety, out this.m_DisposeSentinel, 0, allocator);
			NavMeshQuery.AddQuerySafety(this.m_NavMeshQuery, this.m_Safety);
		}

		public void Dispose()
		{
			bool allowReadOrWriteAccess = AtomicSafetyHandle.GetAllowReadOrWriteAccess(this.m_Safety);
			DisposeSentinel.Dispose(ref this.m_Safety, ref this.m_DisposeSentinel);
			bool flag = allowReadOrWriteAccess;
			if (flag)
			{
				NavMeshQuery.RemoveQuerySafety(this.m_NavMeshQuery, this.m_Safety);
			}
			NavMeshQuery.Destroy(this.m_NavMeshQuery);
			this.m_NavMeshQuery = IntPtr.Zero;
		}

		private static IntPtr Create(NavMeshWorld world, int nodePoolSize)
		{
			return NavMeshQuery.Create_Injected(ref world, nodePoolSize);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy(IntPtr navMeshQuery);

		private static void AddQuerySafety(IntPtr navMeshQuery, AtomicSafetyHandle handle)
		{
			NavMeshQuery.AddQuerySafety_Injected(navMeshQuery, ref handle);
		}

		private static void RemoveQuerySafety(IntPtr navMeshQuery, AtomicSafetyHandle handle)
		{
			NavMeshQuery.RemoveQuerySafety_Injected(navMeshQuery, ref handle);
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasNodePool(IntPtr navMeshQuery);

		public unsafe PathQueryStatus BeginFindPath(NavMeshLocation start, NavMeshLocation end, int areaMask = -1, NativeArray<float> costs = default(NativeArray<float>))
		{
			AtomicSafetyHandle.CheckWriteAndThrow(this.m_Safety);
			bool flag = !NavMeshQuery.HasNodePool(this.m_NavMeshQuery);
			if (flag)
			{
				throw new InvalidOperationException("This query has no buffer allocated for pathfinding operations. Create a different NavMeshQuery with an explicit node pool size.");
			}
			bool flag2 = costs.Length != 0;
			if (flag2)
			{
				bool flag3 = costs.Length != 32;
				if (flag3)
				{
					throw new ArgumentException(string.Format("The number of costs ({0}) must be exactly {1}, one for each possible area type.", costs.Length, 32), "costs");
				}
				for (int i = 0; i < costs.Length; i++)
				{
					bool flag4 = costs[i] < 1f;
					if (flag4)
					{
						throw new ArgumentException(string.Format("The area cost ({0}) at index ({1}) must be greater or equal to 1.", costs[i], i), "costs");
					}
				}
			}
			bool flag5 = !this.IsValid(start.polygon);
			if (flag5)
			{
				throw new ArgumentException("The start location doesn't belong to any active NavMesh surface.", "start");
			}
			bool flag6 = !this.IsValid(end.polygon);
			if (flag6)
			{
				throw new ArgumentException("The end location doesn't belong to any active NavMesh surface.", "end");
			}
			int agentTypeIdForPolygon = this.GetAgentTypeIdForPolygon(start.polygon);
			int agentTypeIdForPolygon2 = this.GetAgentTypeIdForPolygon(end.polygon);
			bool flag7 = agentTypeIdForPolygon != agentTypeIdForPolygon2;
			if (flag7)
			{
				throw new ArgumentException(string.Format("The start and end locations belong to different NavMesh surfaces, with agent type IDs {0} and {1}.", agentTypeIdForPolygon, agentTypeIdForPolygon2));
			}
			void* costs2 = (costs.Length > 0) ? costs.GetUnsafePtr<float>() : null;
			return NavMeshQuery.BeginFindPath(this.m_NavMeshQuery, start, end, areaMask, costs2);
		}

		public PathQueryStatus UpdateFindPath(int iterations, out int iterationsPerformed)
		{
			AtomicSafetyHandle.CheckWriteAndThrow(this.m_Safety);
			bool flag = !NavMeshQuery.HasNodePool(this.m_NavMeshQuery);
			if (flag)
			{
				throw new InvalidOperationException("This query has no buffer allocated for pathfinding operations. Create a different NavMeshQuery with an explicit node pool size.");
			}
			return NavMeshQuery.UpdateFindPath(this.m_NavMeshQuery, iterations, out iterationsPerformed);
		}

		public PathQueryStatus EndFindPath(out int pathSize)
		{
			AtomicSafetyHandle.CheckWriteAndThrow(this.m_Safety);
			bool flag = !NavMeshQuery.HasNodePool(this.m_NavMeshQuery);
			if (flag)
			{
				throw new InvalidOperationException("This query has no buffer allocated for pathfinding operations. Create a different NavMeshQuery with an explicit node pool size.");
			}
			return NavMeshQuery.EndFindPath(this.m_NavMeshQuery, out pathSize);
		}

		public int GetPathResult(NativeSlice<PolygonId> path)
		{
			AtomicSafetyHandle.CheckWriteAndThrow(this.m_Safety);
			bool flag = !NavMeshQuery.HasNodePool(this.m_NavMeshQuery);
			if (flag)
			{
				throw new InvalidOperationException("This query has no buffer allocated for pathfinding operations. Create a different NavMeshQuery with an explicit node pool size.");
			}
			return NavMeshQuery.GetPathResult(this.m_NavMeshQuery, path.GetUnsafePtr<PolygonId>(), path.Length);
		}

		[ThreadSafe]
		private unsafe static PathQueryStatus BeginFindPath(IntPtr navMeshQuery, NavMeshLocation start, NavMeshLocation end, int areaMask, void* costs)
		{
			return NavMeshQuery.BeginFindPath_Injected(navMeshQuery, ref start, ref end, areaMask, costs);
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PathQueryStatus UpdateFindPath(IntPtr navMeshQuery, int iterations, out int iterationsPerformed);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PathQueryStatus EndFindPath(IntPtr navMeshQuery, out int pathSize);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern int GetPathResult(IntPtr navMeshQuery, void* path, int maxPath);

		[ThreadSafe]
		private static bool IsValidPolygon(IntPtr navMeshQuery, PolygonId polygon)
		{
			return NavMeshQuery.IsValidPolygon_Injected(navMeshQuery, ref polygon);
		}

		public bool IsValid(PolygonId polygon)
		{
			AtomicSafetyHandle.CheckReadAndThrow(this.m_Safety);
			return polygon.polyRef != 0uL && NavMeshQuery.IsValidPolygon(this.m_NavMeshQuery, polygon);
		}

		public bool IsValid(NavMeshLocation location)
		{
			return this.IsValid(location.polygon);
		}

		[ThreadSafe]
		private static int GetAgentTypeIdForPolygon(IntPtr navMeshQuery, PolygonId polygon)
		{
			return NavMeshQuery.GetAgentTypeIdForPolygon_Injected(navMeshQuery, ref polygon);
		}

		public int GetAgentTypeIdForPolygon(PolygonId polygon)
		{
			AtomicSafetyHandle.CheckReadAndThrow(this.m_Safety);
			return NavMeshQuery.GetAgentTypeIdForPolygon(this.m_NavMeshQuery, polygon);
		}

		[ThreadSafe]
		private static bool IsPositionInPolygon(IntPtr navMeshQuery, Vector3 position, PolygonId polygon)
		{
			return NavMeshQuery.IsPositionInPolygon_Injected(navMeshQuery, ref position, ref polygon);
		}

		[ThreadSafe]
		private static PathQueryStatus GetClosestPointOnPoly(IntPtr navMeshQuery, PolygonId polygon, Vector3 position, out Vector3 nearest)
		{
			return NavMeshQuery.GetClosestPointOnPoly_Injected(navMeshQuery, ref polygon, ref position, out nearest);
		}

		public NavMeshLocation CreateLocation(Vector3 position, PolygonId polygon)
		{
			AtomicSafetyHandle.CheckReadAndThrow(this.m_Safety);
			Vector3 position2;
			PathQueryStatus closestPointOnPoly = NavMeshQuery.GetClosestPointOnPoly(this.m_NavMeshQuery, polygon, position, out position2);
			return ((closestPointOnPoly & PathQueryStatus.Success) != (PathQueryStatus)0) ? new NavMeshLocation(position2, polygon) : default(NavMeshLocation);
		}

		[ThreadSafe]
		private static NavMeshLocation MapLocation(IntPtr navMeshQuery, Vector3 position, Vector3 extents, int agentTypeID, int areaMask = -1)
		{
			NavMeshLocation result;
			NavMeshQuery.MapLocation_Injected(navMeshQuery, ref position, ref extents, agentTypeID, areaMask, out result);
			return result;
		}

		public NavMeshLocation MapLocation(Vector3 position, Vector3 extents, int agentTypeID, int areaMask = -1)
		{
			AtomicSafetyHandle.CheckReadAndThrow(this.m_Safety);
			return NavMeshQuery.MapLocation(this.m_NavMeshQuery, position, extents, agentTypeID, areaMask);
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void MoveLocations(IntPtr navMeshQuery, void* locations, void* targets, void* areaMasks, int count);

		public void MoveLocations(NativeSlice<NavMeshLocation> locations, NativeSlice<Vector3> targets, NativeSlice<int> areaMasks)
		{
			bool flag = locations.Length != targets.Length || locations.Length != areaMasks.Length;
			if (flag)
			{
				throw new ArgumentException("locations.Length, targets.Length and areaMasks.Length must be equal");
			}
			AtomicSafetyHandle.CheckReadAndThrow(this.m_Safety);
			NavMeshQuery.MoveLocations(this.m_NavMeshQuery, locations.GetUnsafePtr<NavMeshLocation>(), targets.GetUnsafeReadOnlyPtr<Vector3>(), areaMasks.GetUnsafeReadOnlyPtr<int>(), locations.Length);
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void MoveLocationsInSameAreas(IntPtr navMeshQuery, void* locations, void* targets, int count, int areaMask);

		public void MoveLocationsInSameAreas(NativeSlice<NavMeshLocation> locations, NativeSlice<Vector3> targets, int areaMask = -1)
		{
			bool flag = locations.Length != targets.Length;
			if (flag)
			{
				throw new ArgumentException("locations.Length and targets.Length must be equal");
			}
			AtomicSafetyHandle.CheckReadAndThrow(this.m_Safety);
			NavMeshQuery.MoveLocationsInSameAreas(this.m_NavMeshQuery, locations.GetUnsafePtr<NavMeshLocation>(), targets.GetUnsafeReadOnlyPtr<Vector3>(), locations.Length, areaMask);
		}

		[ThreadSafe]
		private static NavMeshLocation MoveLocation(IntPtr navMeshQuery, NavMeshLocation location, Vector3 target, int areaMask)
		{
			NavMeshLocation result;
			NavMeshQuery.MoveLocation_Injected(navMeshQuery, ref location, ref target, areaMask, out result);
			return result;
		}

		public NavMeshLocation MoveLocation(NavMeshLocation location, Vector3 target, int areaMask = -1)
		{
			AtomicSafetyHandle.CheckReadAndThrow(this.m_Safety);
			return NavMeshQuery.MoveLocation(this.m_NavMeshQuery, location, target, areaMask);
		}

		[ThreadSafe]
		private static bool GetPortalPoints(IntPtr navMeshQuery, PolygonId polygon, PolygonId neighbourPolygon, out Vector3 left, out Vector3 right)
		{
			return NavMeshQuery.GetPortalPoints_Injected(navMeshQuery, ref polygon, ref neighbourPolygon, out left, out right);
		}

		public bool GetPortalPoints(PolygonId polygon, PolygonId neighbourPolygon, out Vector3 left, out Vector3 right)
		{
			AtomicSafetyHandle.CheckReadAndThrow(this.m_Safety);
			return NavMeshQuery.GetPortalPoints(this.m_NavMeshQuery, polygon, neighbourPolygon, out left, out right);
		}

		[ThreadSafe]
		private static Matrix4x4 PolygonLocalToWorldMatrix(IntPtr navMeshQuery, PolygonId polygon)
		{
			Matrix4x4 result;
			NavMeshQuery.PolygonLocalToWorldMatrix_Injected(navMeshQuery, ref polygon, out result);
			return result;
		}

		public Matrix4x4 PolygonLocalToWorldMatrix(PolygonId polygon)
		{
			AtomicSafetyHandle.CheckReadAndThrow(this.m_Safety);
			return NavMeshQuery.PolygonLocalToWorldMatrix(this.m_NavMeshQuery, polygon);
		}

		[ThreadSafe]
		private static Matrix4x4 PolygonWorldToLocalMatrix(IntPtr navMeshQuery, PolygonId polygon)
		{
			Matrix4x4 result;
			NavMeshQuery.PolygonWorldToLocalMatrix_Injected(navMeshQuery, ref polygon, out result);
			return result;
		}

		public Matrix4x4 PolygonWorldToLocalMatrix(PolygonId polygon)
		{
			AtomicSafetyHandle.CheckReadAndThrow(this.m_Safety);
			return NavMeshQuery.PolygonWorldToLocalMatrix(this.m_NavMeshQuery, polygon);
		}

		[ThreadSafe]
		private static NavMeshPolyTypes GetPolygonType(IntPtr navMeshQuery, PolygonId polygon)
		{
			return NavMeshQuery.GetPolygonType_Injected(navMeshQuery, ref polygon);
		}

		public NavMeshPolyTypes GetPolygonType(PolygonId polygon)
		{
			AtomicSafetyHandle.CheckReadAndThrow(this.m_Safety);
			return NavMeshQuery.GetPolygonType(this.m_NavMeshQuery, polygon);
		}

		[ThreadSafe]
		private unsafe static PathQueryStatus Raycast(IntPtr navMeshQuery, NavMeshLocation start, Vector3 targetPosition, int areaMask, void* costs, out NavMeshHit hit, void* path, out int pathCount, int maxPath)
		{
			return NavMeshQuery.Raycast_Injected(navMeshQuery, ref start, ref targetPosition, areaMask, costs, out hit, path, out pathCount, maxPath);
		}

		public unsafe PathQueryStatus Raycast(out NavMeshHit hit, NavMeshLocation start, Vector3 targetPosition, int areaMask = -1, NativeArray<float> costs = default(NativeArray<float>))
		{
			AtomicSafetyHandle.CheckReadAndThrow(this.m_Safety);
			bool flag = costs.Length != 0;
			if (flag)
			{
				bool flag2 = costs.Length != 32;
				if (flag2)
				{
					throw new ArgumentException(string.Format("The number of costs ({0}) must be exactly {1}, one for each possible area type.", costs.Length, 32), "costs");
				}
			}
			void* costs2 = (costs.Length == 32) ? costs.GetUnsafePtr<float>() : null;
			int num;
			PathQueryStatus pathQueryStatus = NavMeshQuery.Raycast(this.m_NavMeshQuery, start, targetPosition, areaMask, costs2, out hit, null, out num, 0);
			return pathQueryStatus & ~PathQueryStatus.BufferTooSmall;
		}

		public unsafe PathQueryStatus Raycast(out NavMeshHit hit, NativeSlice<PolygonId> path, out int pathCount, NavMeshLocation start, Vector3 targetPosition, int areaMask = -1, NativeArray<float> costs = default(NativeArray<float>))
		{
			AtomicSafetyHandle.CheckReadAndThrow(this.m_Safety);
			bool flag = costs.Length != 0;
			if (flag)
			{
				bool flag2 = costs.Length != 32;
				if (flag2)
				{
					throw new ArgumentException(string.Format("The number of costs ({0}) must be exactly {1}, one for each possible area type.", costs.Length, 32), "costs");
				}
			}
			void* costs2 = (costs.Length == 32) ? costs.GetUnsafePtr<float>() : null;
			void* ptr = (path.Length > 0) ? path.GetUnsafePtr<PolygonId>() : null;
			int maxPath = (ptr != null) ? path.Length : 0;
			return NavMeshQuery.Raycast(this.m_NavMeshQuery, start, targetPosition, areaMask, costs2, out hit, ptr, out pathCount, maxPath);
		}

		[ThreadSafe]
		private unsafe static PathQueryStatus GetEdgesAndNeighbors(IntPtr navMeshQuery, PolygonId node, int maxVerts, int maxNei, void* verts, void* neighbors, void* edgeIndices, out int vertCount, out int neighborsCount)
		{
			return NavMeshQuery.GetEdgesAndNeighbors_Injected(navMeshQuery, ref node, maxVerts, maxNei, verts, neighbors, edgeIndices, out vertCount, out neighborsCount);
		}

		public unsafe PathQueryStatus GetEdgesAndNeighbors(PolygonId node, NativeSlice<Vector3> edgeVertices, NativeSlice<PolygonId> neighbors, NativeSlice<byte> edgeIndices, out int verticesCount, out int neighborsCount)
		{
			AtomicSafetyHandle.CheckReadAndThrow(this.m_Safety);
			bool flag = edgeIndices.Length != neighbors.Length && neighbors.Length > 0 && edgeIndices.Length > 0;
			if (flag)
			{
				throw new ArgumentException(string.Format("The length of the {0} buffer ({1}) ", "edgeIndices", edgeIndices.Length) + string.Format("needs to be the same as that of the {0} buffer ({1}) ", "neighbors", neighbors.Length) + "because the elements from the two arrays will pair up at the same index.");
			}
			void* verts = (edgeVertices.Length > 0) ? edgeVertices.GetUnsafePtr<Vector3>() : null;
			void* neighbors2 = (neighbors.Length > 0) ? neighbors.GetUnsafePtr<PolygonId>() : null;
			void* edgeIndices2 = (edgeIndices.Length > 0) ? edgeIndices.GetUnsafePtr<byte>() : null;
			int length = edgeVertices.Length;
			int maxNei = (neighbors.Length > 0) ? neighbors.Length : edgeIndices.Length;
			return NavMeshQuery.GetEdgesAndNeighbors(this.m_NavMeshQuery, node, length, maxNei, verts, neighbors2, edgeIndices2, out verticesCount, out neighborsCount);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create_Injected(ref NavMeshWorld world, int nodePoolSize);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddQuerySafety_Injected(IntPtr navMeshQuery, ref AtomicSafetyHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveQuerySafety_Injected(IntPtr navMeshQuery, ref AtomicSafetyHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern PathQueryStatus BeginFindPath_Injected(IntPtr navMeshQuery, ref NavMeshLocation start, ref NavMeshLocation end, int areaMask, void* costs);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsValidPolygon_Injected(IntPtr navMeshQuery, ref PolygonId polygon);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetAgentTypeIdForPolygon_Injected(IntPtr navMeshQuery, ref PolygonId polygon);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsPositionInPolygon_Injected(IntPtr navMeshQuery, ref Vector3 position, ref PolygonId polygon);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PathQueryStatus GetClosestPointOnPoly_Injected(IntPtr navMeshQuery, ref PolygonId polygon, ref Vector3 position, out Vector3 nearest);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MapLocation_Injected(IntPtr navMeshQuery, ref Vector3 position, ref Vector3 extents, int agentTypeID, int areaMask = -1, out NavMeshLocation ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MoveLocation_Injected(IntPtr navMeshQuery, ref NavMeshLocation location, ref Vector3 target, int areaMask, out NavMeshLocation ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetPortalPoints_Injected(IntPtr navMeshQuery, ref PolygonId polygon, ref PolygonId neighbourPolygon, out Vector3 left, out Vector3 right);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PolygonLocalToWorldMatrix_Injected(IntPtr navMeshQuery, ref PolygonId polygon, out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PolygonWorldToLocalMatrix_Injected(IntPtr navMeshQuery, ref PolygonId polygon, out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern NavMeshPolyTypes GetPolygonType_Injected(IntPtr navMeshQuery, ref PolygonId polygon);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern PathQueryStatus Raycast_Injected(IntPtr navMeshQuery, ref NavMeshLocation start, ref Vector3 targetPosition, int areaMask, void* costs, out NavMeshHit hit, void* path, out int pathCount, int maxPath);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern PathQueryStatus GetEdgesAndNeighbors_Injected(IntPtr navMeshQuery, ref PolygonId node, int maxVerts, int maxNei, void* verts, void* neighbors, void* edgeIndices, out int vertCount, out int neighborsCount);
	}
}
