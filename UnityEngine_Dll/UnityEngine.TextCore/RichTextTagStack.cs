using System;

namespace UnityEngine.TextCore
{
	internal struct RichTextTagStack<T>
	{
		public T[] m_ItemStack;

		public int m_Index;

		private int m_Capacity;

		private T m_DefaultItem;

		private const int k_DefaultCapacity = 4;

		public RichTextTagStack(T[] tagStack)
		{
			this.m_ItemStack = tagStack;
			this.m_Capacity = tagStack.Length;
			this.m_Index = 0;
			this.m_DefaultItem = default(T);
		}

		public RichTextTagStack(int capacity)
		{
			this.m_ItemStack = new T[capacity];
			this.m_Capacity = capacity;
			this.m_Index = 0;
			this.m_DefaultItem = default(T);
		}

		public void Clear()
		{
			this.m_Index = 0;
		}

		public void SetDefault(T item)
		{
			this.m_ItemStack[0] = item;
			this.m_Index = 1;
		}

		public void Add(T item)
		{
			bool flag = this.m_Index < this.m_ItemStack.Length;
			if (flag)
			{
				this.m_ItemStack[this.m_Index] = item;
				this.m_Index++;
			}
		}

		public T Remove()
		{
			this.m_Index--;
			bool flag = this.m_Index <= 0;
			T result;
			if (flag)
			{
				this.m_Index = 1;
				result = this.m_ItemStack[0];
			}
			else
			{
				result = this.m_ItemStack[this.m_Index - 1];
			}
			return result;
		}

		public void Push(T item)
		{
			bool flag = this.m_Index == this.m_Capacity;
			if (flag)
			{
				this.m_Capacity *= 2;
				bool flag2 = this.m_Capacity == 0;
				if (flag2)
				{
					this.m_Capacity = 4;
				}
				Array.Resize<T>(ref this.m_ItemStack, this.m_Capacity);
			}
			this.m_ItemStack[this.m_Index] = item;
			this.m_Index++;
		}

		public T Pop()
		{
			bool flag = this.m_Index == 0;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				this.m_Index--;
				T t = this.m_ItemStack[this.m_Index];
				this.m_ItemStack[this.m_Index] = this.m_DefaultItem;
				result = t;
			}
			return result;
		}

		public T Peek()
		{
			bool flag = this.m_Index == 0;
			T result;
			if (flag)
			{
				result = this.m_DefaultItem;
			}
			else
			{
				result = this.m_ItemStack[this.m_Index - 1];
			}
			return result;
		}

		public T CurrentItem()
		{
			bool flag = this.m_Index > 0;
			T result;
			if (flag)
			{
				result = this.m_ItemStack[this.m_Index - 1];
			}
			else
			{
				result = this.m_ItemStack[0];
			}
			return result;
		}

		public T PreviousItem()
		{
			bool flag = this.m_Index > 1;
			T result;
			if (flag)
			{
				result = this.m_ItemStack[this.m_Index - 2];
			}
			else
			{
				result = this.m_ItemStack[0];
			}
			return result;
		}
	}
}
