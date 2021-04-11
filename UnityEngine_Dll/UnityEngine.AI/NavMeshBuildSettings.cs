using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.AI
{
	[NativeHeader("Modules/AI/Public/NavMeshBuildSettings.h")]
	public struct NavMeshBuildSettings
	{
		private int m_AgentTypeID;

		private float m_AgentRadius;

		private float m_AgentHeight;

		private float m_AgentSlope;

		private float m_AgentClimb;

		private float m_LedgeDropHeight;

		private float m_MaxJumpAcrossDistance;

		private float m_MinRegionArea;

		private int m_OverrideVoxelSize;

		private float m_VoxelSize;

		private int m_OverrideTileSize;

		private int m_TileSize;

		private int m_AccuratePlacement;

		private uint m_MaxJobWorkers;

		private int m_PreserveTilesOutsideBounds;

		private NavMeshBuildDebugSettings m_Debug;

		public int agentTypeID
		{
			get
			{
				return this.m_AgentTypeID;
			}
			set
			{
				this.m_AgentTypeID = value;
			}
		}

		public float agentRadius
		{
			get
			{
				return this.m_AgentRadius;
			}
			set
			{
				this.m_AgentRadius = value;
			}
		}

		public float agentHeight
		{
			get
			{
				return this.m_AgentHeight;
			}
			set
			{
				this.m_AgentHeight = value;
			}
		}

		public float agentSlope
		{
			get
			{
				return this.m_AgentSlope;
			}
			set
			{
				this.m_AgentSlope = value;
			}
		}

		public float agentClimb
		{
			get
			{
				return this.m_AgentClimb;
			}
			set
			{
				this.m_AgentClimb = value;
			}
		}

		public float minRegionArea
		{
			get
			{
				return this.m_MinRegionArea;
			}
			set
			{
				this.m_MinRegionArea = value;
			}
		}

		public bool overrideVoxelSize
		{
			get
			{
				return this.m_OverrideVoxelSize != 0;
			}
			set
			{
				this.m_OverrideVoxelSize = (value ? 1 : 0);
			}
		}

		public float voxelSize
		{
			get
			{
				return this.m_VoxelSize;
			}
			set
			{
				this.m_VoxelSize = value;
			}
		}

		public bool overrideTileSize
		{
			get
			{
				return this.m_OverrideTileSize != 0;
			}
			set
			{
				this.m_OverrideTileSize = (value ? 1 : 0);
			}
		}

		public int tileSize
		{
			get
			{
				return this.m_TileSize;
			}
			set
			{
				this.m_TileSize = value;
			}
		}

		public uint maxJobWorkers
		{
			get
			{
				return this.m_MaxJobWorkers;
			}
			set
			{
				this.m_MaxJobWorkers = value;
			}
		}

		public bool preserveTilesOutsideBounds
		{
			get
			{
				return this.m_PreserveTilesOutsideBounds != 0;
			}
			set
			{
				this.m_PreserveTilesOutsideBounds = (value ? 1 : 0);
			}
		}

		public NavMeshBuildDebugSettings debug
		{
			get
			{
				return this.m_Debug;
			}
			set
			{
				this.m_Debug = value;
			}
		}

		public string[] ValidationReport(Bounds buildBounds)
		{
			return NavMeshBuildSettings.InternalValidationReport(this, buildBounds);
		}

		[FreeFunction, NativeHeader("Modules/AI/Public/NavMeshBuildSettings.h")]
		private static string[] InternalValidationReport(NavMeshBuildSettings buildSettings, Bounds buildBounds)
		{
			return NavMeshBuildSettings.InternalValidationReport_Injected(ref buildSettings, ref buildBounds);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] InternalValidationReport_Injected(ref NavMeshBuildSettings buildSettings, ref Bounds buildBounds);
	}
}
