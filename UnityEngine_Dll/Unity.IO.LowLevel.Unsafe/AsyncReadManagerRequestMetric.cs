using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.IO.LowLevel.Unsafe
{
	[NativeConditional("ENABLE_PROFILER"), RequiredByNativeCode]
	public struct AsyncReadManagerRequestMetric
	{
		[NativeName("assetName")]
		public string AssetName
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<AssetName>k__BackingField;
			}
		}

		[NativeName("fileName")]
		public string FileName
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<FileName>k__BackingField;
			}
		}

		[NativeName("offsetBytes")]
		public ulong OffsetBytes
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<OffsetBytes>k__BackingField;
			}
		}

		[NativeName("sizeBytes")]
		public ulong SizeBytes
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<SizeBytes>k__BackingField;
			}
		}

		[NativeName("assetTypeId")]
		public ulong AssetTypeId
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<AssetTypeId>k__BackingField;
			}
		}

		[NativeName("currentBytesRead")]
		public ulong CurrentBytesRead
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<CurrentBytesRead>k__BackingField;
			}
		}

		[NativeName("batchReadCount")]
		public uint BatchReadCount
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<BatchReadCount>k__BackingField;
			}
		}

		[NativeName("isBatchRead")]
		public bool IsBatchRead
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<IsBatchRead>k__BackingField;
			}
		}

		[NativeName("state")]
		public ProcessingState State
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<State>k__BackingField;
			}
		}

		[NativeName("readType")]
		public FileReadType ReadType
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<ReadType>k__BackingField;
			}
		}

		[NativeName("priorityLevel")]
		public Priority PriorityLevel
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<PriorityLevel>k__BackingField;
			}
		}

		[NativeName("subsystem")]
		public AssetLoadingSubsystem Subsystem
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<Subsystem>k__BackingField;
			}
		}

		[NativeName("requestTimeMicroseconds")]
		public double RequestTimeMicroseconds
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<RequestTimeMicroseconds>k__BackingField;
			}
		}

		[NativeName("timeInQueueMicroseconds")]
		public double TimeInQueueMicroseconds
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<TimeInQueueMicroseconds>k__BackingField;
			}
		}

		[NativeName("totalTimeMicroseconds")]
		public double TotalTimeMicroseconds
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<TotalTimeMicroseconds>k__BackingField;
			}
		}
	}
}
