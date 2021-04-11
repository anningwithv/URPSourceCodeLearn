using System;
using System.Diagnostics;
using UnityEngine.Scripting;

namespace Unity.Profiling
{
	[DebuggerDisplay("Value = {Value}; Count = {Count}"), UsedByNativeCode]
	public struct ProfilerRecorderSample
	{
		private long value;

		private long count;

		private long refValue;

		public long Value
		{
			get
			{
				return this.value;
			}
		}

		public long Count
		{
			get
			{
				return this.count;
			}
		}
	}
}
