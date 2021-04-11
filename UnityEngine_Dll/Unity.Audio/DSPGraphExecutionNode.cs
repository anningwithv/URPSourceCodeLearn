using System;

namespace Unity.Audio
{
	internal struct DSPGraphExecutionNode
	{
		public unsafe void* ReflectionData;

		public unsafe void* JobStructData;

		public unsafe void* JobData;

		public unsafe void* ResourceContext;

		public int FunctionIndex;

		public int FenceIndex;

		public int FenceCount;
	}
}
