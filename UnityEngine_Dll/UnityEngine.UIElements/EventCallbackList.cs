using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	internal class EventCallbackList
	{
		private List<EventCallbackFunctorBase> m_List;

		public int trickleDownCallbackCount
		{
			get;
			private set;
		}

		public int bubbleUpCallbackCount
		{
			get;
			private set;
		}

		public int Count
		{
			get
			{
				return this.m_List.Count;
			}
		}

		public EventCallbackFunctorBase this[int i]
		{
			get
			{
				return this.m_List[i];
			}
			set
			{
				this.m_List[i] = value;
			}
		}

		public EventCallbackList()
		{
			this.m_List = new List<EventCallbackFunctorBase>();
			this.trickleDownCallbackCount = 0;
			this.bubbleUpCallbackCount = 0;
		}

		public EventCallbackList(EventCallbackList source)
		{
			this.m_List = new List<EventCallbackFunctorBase>(source.m_List);
			this.trickleDownCallbackCount = 0;
			this.bubbleUpCallbackCount = 0;
		}

		public bool Contains(long eventTypeId, Delegate callback, CallbackPhase phase)
		{
			return this.Find(eventTypeId, callback, phase) != null;
		}

		public EventCallbackFunctorBase Find(long eventTypeId, Delegate callback, CallbackPhase phase)
		{
			EventCallbackFunctorBase result;
			for (int i = 0; i < this.m_List.Count; i++)
			{
				bool flag = this.m_List[i].IsEquivalentTo(eventTypeId, callback, phase);
				if (flag)
				{
					result = this.m_List[i];
					return result;
				}
			}
			result = null;
			return result;
		}

		public bool Remove(long eventTypeId, Delegate callback, CallbackPhase phase)
		{
			bool result;
			for (int i = 0; i < this.m_List.Count; i++)
			{
				bool flag = this.m_List[i].IsEquivalentTo(eventTypeId, callback, phase);
				if (flag)
				{
					this.m_List.RemoveAt(i);
					bool flag2 = phase == CallbackPhase.TrickleDownAndTarget;
					if (flag2)
					{
						int num = this.trickleDownCallbackCount;
						this.trickleDownCallbackCount = num - 1;
					}
					else
					{
						bool flag3 = phase == CallbackPhase.TargetAndBubbleUp;
						if (flag3)
						{
							int num = this.bubbleUpCallbackCount;
							this.bubbleUpCallbackCount = num - 1;
						}
					}
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public void Add(EventCallbackFunctorBase item)
		{
			this.m_List.Add(item);
			bool flag = item.phase == CallbackPhase.TrickleDownAndTarget;
			if (flag)
			{
				int num = this.trickleDownCallbackCount;
				this.trickleDownCallbackCount = num + 1;
			}
			else
			{
				bool flag2 = item.phase == CallbackPhase.TargetAndBubbleUp;
				if (flag2)
				{
					int num = this.bubbleUpCallbackCount;
					this.bubbleUpCallbackCount = num + 1;
				}
			}
		}

		public void AddRange(EventCallbackList list)
		{
			this.m_List.AddRange(list.m_List);
			foreach (EventCallbackFunctorBase current in list.m_List)
			{
				bool flag = current.phase == CallbackPhase.TrickleDownAndTarget;
				if (flag)
				{
					int num = this.trickleDownCallbackCount;
					this.trickleDownCallbackCount = num + 1;
				}
				else
				{
					bool flag2 = current.phase == CallbackPhase.TargetAndBubbleUp;
					if (flag2)
					{
						int num = this.bubbleUpCallbackCount;
						this.bubbleUpCallbackCount = num + 1;
					}
				}
			}
		}

		public void Clear()
		{
			this.m_List.Clear();
			this.trickleDownCallbackCount = 0;
			this.bubbleUpCallbackCount = 0;
		}
	}
}
