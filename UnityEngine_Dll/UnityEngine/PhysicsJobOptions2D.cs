using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Modules/Physics2D/Public/Physics2DSettings.h"), NativeClass("PhysicsJobOptions2D", "struct PhysicsJobOptions2D;"), RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	public struct PhysicsJobOptions2D
	{
		private bool m_UseMultithreading;

		private bool m_UseConsistencySorting;

		private int m_InterpolationPosesPerJob;

		private int m_NewContactsPerJob;

		private int m_CollideContactsPerJob;

		private int m_ClearFlagsPerJob;

		private int m_ClearBodyForcesPerJob;

		private int m_SyncDiscreteFixturesPerJob;

		private int m_SyncContinuousFixturesPerJob;

		private int m_FindNearestContactsPerJob;

		private int m_UpdateTriggerContactsPerJob;

		private int m_IslandSolverCostThreshold;

		private int m_IslandSolverBodyCostScale;

		private int m_IslandSolverContactCostScale;

		private int m_IslandSolverJointCostScale;

		private int m_IslandSolverBodiesPerJob;

		private int m_IslandSolverContactsPerJob;

		public bool useMultithreading
		{
			get
			{
				return this.m_UseMultithreading;
			}
			set
			{
				this.m_UseMultithreading = value;
			}
		}

		public bool useConsistencySorting
		{
			get
			{
				return this.m_UseConsistencySorting;
			}
			set
			{
				this.m_UseConsistencySorting = value;
			}
		}

		public int interpolationPosesPerJob
		{
			get
			{
				return this.m_InterpolationPosesPerJob;
			}
			set
			{
				this.m_InterpolationPosesPerJob = value;
			}
		}

		public int newContactsPerJob
		{
			get
			{
				return this.m_NewContactsPerJob;
			}
			set
			{
				this.m_NewContactsPerJob = value;
			}
		}

		public int collideContactsPerJob
		{
			get
			{
				return this.m_CollideContactsPerJob;
			}
			set
			{
				this.m_CollideContactsPerJob = value;
			}
		}

		public int clearFlagsPerJob
		{
			get
			{
				return this.m_ClearFlagsPerJob;
			}
			set
			{
				this.m_ClearFlagsPerJob = value;
			}
		}

		public int clearBodyForcesPerJob
		{
			get
			{
				return this.m_ClearBodyForcesPerJob;
			}
			set
			{
				this.m_ClearBodyForcesPerJob = value;
			}
		}

		public int syncDiscreteFixturesPerJob
		{
			get
			{
				return this.m_SyncDiscreteFixturesPerJob;
			}
			set
			{
				this.m_SyncDiscreteFixturesPerJob = value;
			}
		}

		public int syncContinuousFixturesPerJob
		{
			get
			{
				return this.m_SyncContinuousFixturesPerJob;
			}
			set
			{
				this.m_SyncContinuousFixturesPerJob = value;
			}
		}

		public int findNearestContactsPerJob
		{
			get
			{
				return this.m_FindNearestContactsPerJob;
			}
			set
			{
				this.m_FindNearestContactsPerJob = value;
			}
		}

		public int updateTriggerContactsPerJob
		{
			get
			{
				return this.m_UpdateTriggerContactsPerJob;
			}
			set
			{
				this.m_UpdateTriggerContactsPerJob = value;
			}
		}

		public int islandSolverCostThreshold
		{
			get
			{
				return this.m_IslandSolverCostThreshold;
			}
			set
			{
				this.m_IslandSolverCostThreshold = value;
			}
		}

		public int islandSolverBodyCostScale
		{
			get
			{
				return this.m_IslandSolverBodyCostScale;
			}
			set
			{
				this.m_IslandSolverBodyCostScale = value;
			}
		}

		public int islandSolverContactCostScale
		{
			get
			{
				return this.m_IslandSolverContactCostScale;
			}
			set
			{
				this.m_IslandSolverContactCostScale = value;
			}
		}

		public int islandSolverJointCostScale
		{
			get
			{
				return this.m_IslandSolverJointCostScale;
			}
			set
			{
				this.m_IslandSolverJointCostScale = value;
			}
		}

		public int islandSolverBodiesPerJob
		{
			get
			{
				return this.m_IslandSolverBodiesPerJob;
			}
			set
			{
				this.m_IslandSolverBodiesPerJob = value;
			}
		}

		public int islandSolverContactsPerJob
		{
			get
			{
				return this.m_IslandSolverContactsPerJob;
			}
			set
			{
				this.m_IslandSolverContactsPerJob = value;
			}
		}
	}
}
