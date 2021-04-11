using System;
using System.Runtime.CompilerServices;
using Unity.Collections;

namespace UnityEngine.Profiling.Experimental
{
	public struct DebugScreenCapture
	{
		public NativeArray<byte> rawImageDataReference
		{
			[IsReadOnly]
			get;
			set;
		}

		public TextureFormat imageFormat
		{
			[IsReadOnly]
			get;
			set;
		}

		public int width
		{
			[IsReadOnly]
			get;
			set;
		}

		public int height
		{
			[IsReadOnly]
			get;
			set;
		}
	}
}
