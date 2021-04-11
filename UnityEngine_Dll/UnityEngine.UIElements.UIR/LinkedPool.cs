using System;

namespace UnityEngine.UIElements.UIR
{
	internal class LinkedPool<T> where T : LinkedPoolItem<T>
	{
		private readonly Func<T> m_CreateFunc;

		private readonly Action<T> m_ResetAction;

		private readonly int m_Limit;

		private T m_PoolFirst;

		public int Count
		{
			get;
			private set;
		}

		public LinkedPool(Func<T> createFunc, Action<T> resetAction, int limit = 10000)
		{
			Debug.Assert(createFunc != null);
			this.m_CreateFunc = createFunc;
			Debug.Assert(resetAction != null);
			this.m_ResetAction = resetAction;
			Debug.Assert(limit > 0);
			this.m_Limit = limit;
		}

		public void Clear()
		{
			this.m_PoolFirst = default(T);
			this.Count = 0;
		}

		public T Get()
		{
			T t = this.m_PoolFirst;
			bool flag = this.m_PoolFirst != null;
			if (flag)
			{
				int count = this.Count - 1;
				this.Count = count;
				this.m_PoolFirst = t.poolNext;
				this.m_ResetAction(t);
			}
			else
			{
				t = this.m_CreateFunc();
			}
			return t;
		}

		public void Return(T item)
		{
			bool flag = this.Count < this.m_Limit;
			if (flag)
			{
				item.poolNext = this.m_PoolFirst;
				this.m_PoolFirst = item;
				int count = this.Count + 1;
				this.Count = count;
			}
		}
	}
}
