using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	[NativeAsStruct, NativeConditional("ENABLE_PROFILER")]
	[StructLayout(LayoutKind.Sequential)]
	public class AsyncReadManagerSummaryMetrics
	{
		[NativeName("totalBytesRead")]
		public ulong TotalBytesRead
		{
			[CompilerGenerated]
			get
			{
				return this.<TotalBytesRead>k__BackingField;
			}
		}

		[NativeName("averageBandwidthMBPerSecond")]
		public float AverageBandwidthMBPerSecond
		{
			[CompilerGenerated]
			get
			{
				return this.<AverageBandwidthMBPerSecond>k__BackingField;
			}
		}

		[NativeName("averageReadSizeInBytes")]
		public float AverageReadSizeInBytes
		{
			[CompilerGenerated]
			get
			{
				return this.<AverageReadSizeInBytes>k__BackingField;
			}
		}

		[NativeName("averageWaitTimeMicroseconds")]
		public float AverageWaitTimeMicroseconds
		{
			[CompilerGenerated]
			get
			{
				return this.<AverageWaitTimeMicroseconds>k__BackingField;
			}
		}

		[NativeName("averageReadTimeMicroseconds")]
		public float AverageReadTimeMicroseconds
		{
			[CompilerGenerated]
			get
			{
				return this.<AverageReadTimeMicroseconds>k__BackingField;
			}
		}

		[NativeName("averageTotalRequestTimeMicroseconds")]
		public float AverageTotalRequestTimeMicroseconds
		{
			[CompilerGenerated]
			get
			{
				return this.<AverageTotalRequestTimeMicroseconds>k__BackingField;
			}
		}

		[NativeName("averageThroughputMBPerSecond")]
		public float AverageThroughputMBPerSecond
		{
			[CompilerGenerated]
			get
			{
				return this.<AverageThroughputMBPerSecond>k__BackingField;
			}
		}

		[NativeName("longestWaitTimeMicroseconds")]
		public float LongestWaitTimeMicroseconds
		{
			[CompilerGenerated]
			get
			{
				return this.<LongestWaitTimeMicroseconds>k__BackingField;
			}
		}

		[NativeName("longestReadTimeMicroseconds")]
		public float LongestReadTimeMicroseconds
		{
			[CompilerGenerated]
			get
			{
				return this.<LongestReadTimeMicroseconds>k__BackingField;
			}
		}

		[NativeName("longestReadAssetType")]
		public ulong LongestReadAssetType
		{
			[CompilerGenerated]
			get
			{
				return this.<LongestReadAssetType>k__BackingField;
			}
		}

		[NativeName("longestWaitAssetType")]
		public ulong LongestWaitAssetType
		{
			[CompilerGenerated]
			get
			{
				return this.<LongestWaitAssetType>k__BackingField;
			}
		}

		[NativeName("longestReadSubsystem")]
		public AssetLoadingSubsystem LongestReadSubsystem
		{
			[CompilerGenerated]
			get
			{
				return this.<LongestReadSubsystem>k__BackingField;
			}
		}

		[NativeName("longestWaitSubsystem")]
		public AssetLoadingSubsystem LongestWaitSubsystem
		{
			[CompilerGenerated]
			get
			{
				return this.<LongestWaitSubsystem>k__BackingField;
			}
		}

		[NativeName("numberOfInProgressRequests")]
		public int NumberOfInProgressRequests
		{
			[CompilerGenerated]
			get
			{
				return this.<NumberOfInProgressRequests>k__BackingField;
			}
		}

		[NativeName("numberOfCompletedRequests")]
		public int NumberOfCompletedRequests
		{
			[CompilerGenerated]
			get
			{
				return this.<NumberOfCompletedRequests>k__BackingField;
			}
		}

		[NativeName("numberOfFailedRequests")]
		public int NumberOfFailedRequests
		{
			[CompilerGenerated]
			get
			{
				return this.<NumberOfFailedRequests>k__BackingField;
			}
		}

		[NativeName("numberOfWaitingRequests")]
		public int NumberOfWaitingRequests
		{
			[CompilerGenerated]
			get
			{
				return this.<NumberOfWaitingRequests>k__BackingField;
			}
		}

		[NativeName("numberOfCanceledRequests")]
		public int NumberOfCanceledRequests
		{
			[CompilerGenerated]
			get
			{
				return this.<NumberOfCanceledRequests>k__BackingField;
			}
		}

		[NativeName("totalNumberOfRequests")]
		public int TotalNumberOfRequests
		{
			[CompilerGenerated]
			get
			{
				return this.<TotalNumberOfRequests>k__BackingField;
			}
		}

		[NativeName("numberOfCachedReads")]
		public int NumberOfCachedReads
		{
			[CompilerGenerated]
			get
			{
				return this.<NumberOfCachedReads>k__BackingField;
			}
		}

		[NativeName("numberOfAsyncReads")]
		public int NumberOfAsyncReads
		{
			[CompilerGenerated]
			get
			{
				return this.<NumberOfAsyncReads>k__BackingField;
			}
		}

		[NativeName("numberOfSyncReads")]
		public int NumberOfSyncReads
		{
			[CompilerGenerated]
			get
			{
				return this.<NumberOfSyncReads>k__BackingField;
			}
		}
	}
}
