using System;

namespace Unity.Collections
{
	internal sealed class NativeSliceDebugView<T> where T : struct
	{
		private NativeSlice<T> m_Array;

		public T[] Items
		{
			get
			{
				return this.m_Array.ToArray();
			}
		}

		public NativeSliceDebugView(NativeSlice<T> array)
		{
			this.m_Array = array;
		}
	}
}
