using System;

namespace Unity.Collections
{
	internal sealed class NativeArrayDebugView<T> where T : struct
	{
		private NativeArray<T> m_Array;

		public T[] Items
		{
			get
			{
				return this.m_Array.ToArray();
			}
		}

		public NativeArrayDebugView(NativeArray<T> array)
		{
			this.m_Array = array;
		}
	}
}
