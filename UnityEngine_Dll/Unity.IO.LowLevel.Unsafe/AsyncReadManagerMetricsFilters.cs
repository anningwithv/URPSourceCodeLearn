using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.IO.LowLevel.Unsafe
{
	[NativeAsStruct, NativeConditional("ENABLE_PROFILER"), RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class AsyncReadManagerMetricsFilters
	{
		[NativeName("typeIDs")]
		internal ulong[] TypeIDs;

		[NativeName("states")]
		internal ProcessingState[] States;

		[NativeName("readTypes")]
		internal FileReadType[] ReadTypes;

		[NativeName("priorityLevels")]
		internal Priority[] PriorityLevels;

		[NativeName("subsystems")]
		internal AssetLoadingSubsystem[] Subsystems;

		public AsyncReadManagerMetricsFilters()
		{
			this.ClearFilters();
		}

		public AsyncReadManagerMetricsFilters(ulong typeID)
		{
			this.ClearFilters();
			this.SetTypeIDFilter(typeID);
		}

		public AsyncReadManagerMetricsFilters(ProcessingState state)
		{
			this.ClearFilters();
			this.SetStateFilter(state);
		}

		public AsyncReadManagerMetricsFilters(FileReadType readType)
		{
			this.ClearFilters();
			this.SetReadTypeFilter(readType);
		}

		public AsyncReadManagerMetricsFilters(Priority priorityLevel)
		{
			this.ClearFilters();
			this.SetPriorityFilter(priorityLevel);
		}

		public AsyncReadManagerMetricsFilters(AssetLoadingSubsystem subsystem)
		{
			this.ClearFilters();
			this.SetSubsystemFilter(subsystem);
		}

		public AsyncReadManagerMetricsFilters(ulong[] typeIDs)
		{
			this.ClearFilters();
			this.SetTypeIDFilter(typeIDs);
		}

		public AsyncReadManagerMetricsFilters(ProcessingState[] states)
		{
			this.ClearFilters();
			this.SetStateFilter(states);
		}

		public AsyncReadManagerMetricsFilters(FileReadType[] readTypes)
		{
			this.ClearFilters();
			this.SetReadTypeFilter(readTypes);
		}

		public AsyncReadManagerMetricsFilters(Priority[] priorityLevels)
		{
			this.ClearFilters();
			this.SetPriorityFilter(priorityLevels);
		}

		public AsyncReadManagerMetricsFilters(AssetLoadingSubsystem[] subsystems)
		{
			this.ClearFilters();
			this.SetSubsystemFilter(subsystems);
		}

		public AsyncReadManagerMetricsFilters(ulong[] typeIDs, ProcessingState[] states, FileReadType[] readTypes, Priority[] priorityLevels, AssetLoadingSubsystem[] subsystems)
		{
			this.ClearFilters();
			this.SetTypeIDFilter(typeIDs);
			this.SetStateFilter(states);
			this.SetReadTypeFilter(readTypes);
			this.SetPriorityFilter(priorityLevels);
			this.SetSubsystemFilter(subsystems);
		}

		public void SetTypeIDFilter(ulong[] _typeIDs)
		{
			this.TypeIDs = _typeIDs;
		}

		public void SetStateFilter(ProcessingState[] _states)
		{
			this.States = _states;
		}

		public void SetReadTypeFilter(FileReadType[] _readTypes)
		{
			this.ReadTypes = _readTypes;
		}

		public void SetPriorityFilter(Priority[] _priorityLevels)
		{
			this.PriorityLevels = _priorityLevels;
		}

		public void SetSubsystemFilter(AssetLoadingSubsystem[] _subsystems)
		{
			this.Subsystems = _subsystems;
		}

		public void SetTypeIDFilter(ulong _typeID)
		{
			this.TypeIDs = new ulong[]
			{
				_typeID
			};
		}

		public void SetStateFilter(ProcessingState _state)
		{
			this.States = new ProcessingState[]
			{
				_state
			};
		}

		public void SetReadTypeFilter(FileReadType _readType)
		{
			this.ReadTypes = new FileReadType[]
			{
				_readType
			};
		}

		public void SetPriorityFilter(Priority _priorityLevel)
		{
			this.PriorityLevels = new Priority[]
			{
				_priorityLevel
			};
		}

		public void SetSubsystemFilter(AssetLoadingSubsystem _subsystem)
		{
			this.Subsystems = new AssetLoadingSubsystem[]
			{
				_subsystem
			};
		}

		public void RemoveTypeIDFilter()
		{
			this.TypeIDs = null;
		}

		public void RemoveStateFilter()
		{
			this.States = null;
		}

		public void RemoveReadTypeFilter()
		{
			this.ReadTypes = null;
		}

		public void RemovePriorityFilter()
		{
			this.PriorityLevels = null;
		}

		public void RemoveSubsystemFilter()
		{
			this.Subsystems = null;
		}

		public void ClearFilters()
		{
			this.RemoveTypeIDFilter();
			this.RemoveStateFilter();
			this.RemoveReadTypeFilter();
			this.RemovePriorityFilter();
			this.RemoveSubsystemFilter();
		}
	}
}
