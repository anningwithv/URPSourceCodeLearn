using System;

namespace UnityEngine.UIElements.UIR
{
	internal class Pool<T> where T : PoolItem, new()
	{
		private PoolItem m_Pool;

		public T Get()
		{
			bool flag = this.m_Pool == null;
			T result;
			if (flag)
			{
				result = Activator.CreateInstance<T>();
			}
			else
			{
				Debug.Assert(this.m_Pool != null);
				T t = (T)((object)this.m_Pool);
				this.m_Pool = this.m_Pool.poolNext;
				t.poolNext = null;
				result = t;
			}
			return result;
		}

		public void Return(T obj)
		{
			obj.poolNext = this.m_Pool;
			this.m_Pool = obj;
		}
	}
}
