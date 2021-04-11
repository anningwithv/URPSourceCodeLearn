using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	internal class EventCallbackListPool
	{
		private readonly Stack<EventCallbackList> m_Stack = new Stack<EventCallbackList>();

		public EventCallbackList Get(EventCallbackList initializer)
		{
			bool flag = this.m_Stack.Count == 0;
			EventCallbackList eventCallbackList;
			if (flag)
			{
				bool flag2 = initializer != null;
				if (flag2)
				{
					eventCallbackList = new EventCallbackList(initializer);
				}
				else
				{
					eventCallbackList = new EventCallbackList();
				}
			}
			else
			{
				eventCallbackList = this.m_Stack.Pop();
				bool flag3 = initializer != null;
				if (flag3)
				{
					eventCallbackList.AddRange(initializer);
				}
			}
			return eventCallbackList;
		}

		public void Release(EventCallbackList element)
		{
			element.Clear();
			this.m_Stack.Push(element);
		}
	}
}
