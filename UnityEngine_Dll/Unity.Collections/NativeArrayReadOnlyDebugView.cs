using System;

namespace Unity.Collections
{
	internal sealed class NativeArrayReadOnlyDebugView<T> where T : struct
	{
		private NativeArray<T>.ReadOnly m_Array;

		public T[] Items
		{
			get
			{
				return this.m_Array.ToArray();
			}
		}

		public NativeArrayReadOnlyDebugView(NativeArray<T>.ReadOnly array)
		{
			this.m_Array = array;
		}
	}
}
