using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	internal class ClickDetector
	{
		private class ButtonClickStatus
		{
			public VisualElement m_Target;

			public Vector3 m_PointerDownPosition;

			public long m_LastPointerDownTime;

			public int m_ClickCount;

			public void Reset()
			{
				this.m_Target = null;
				this.m_ClickCount = 0;
				this.m_LastPointerDownTime = 0L;
				this.m_PointerDownPosition = Vector3.zero;
			}
		}

		private List<ClickDetector.ButtonClickStatus> m_ClickStatus;

		internal static int s_DoubleClickTime
		{
			get;
			set;
		}

		public ClickDetector()
		{
			this.m_ClickStatus = new List<ClickDetector.ButtonClickStatus>(PointerId.maxPointers);
			for (int i = 0; i < PointerId.maxPointers; i++)
			{
				this.m_ClickStatus.Add(new ClickDetector.ButtonClickStatus());
			}
			bool flag = ClickDetector.s_DoubleClickTime == -1;
			if (flag)
			{
				ClickDetector.s_DoubleClickTime = Event.GetDoubleClickTime();
			}
		}

		private void StartClickTracking(EventBase evt)
		{
			IPointerEvent pointerEvent = evt as IPointerEvent;
			bool flag = pointerEvent == null;
			if (!flag)
			{
				ClickDetector.ButtonClickStatus buttonClickStatus = this.m_ClickStatus[pointerEvent.pointerId];
				VisualElement visualElement = evt.target as VisualElement;
				bool flag2 = visualElement != buttonClickStatus.m_Target;
				if (flag2)
				{
					buttonClickStatus.Reset();
				}
				buttonClickStatus.m_Target = visualElement;
				bool flag3 = evt.timestamp - buttonClickStatus.m_LastPointerDownTime > (long)ClickDetector.s_DoubleClickTime;
				if (flag3)
				{
					buttonClickStatus.m_ClickCount = 1;
				}
				else
				{
					buttonClickStatus.m_ClickCount++;
				}
				buttonClickStatus.m_LastPointerDownTime = evt.timestamp;
				buttonClickStatus.m_PointerDownPosition = pointerEvent.position;
			}
		}

		private void SendClickEvent(EventBase evt)
		{
			IPointerEvent pointerEvent = evt as IPointerEvent;
			bool flag = pointerEvent == null;
			if (!flag)
			{
				ClickDetector.ButtonClickStatus buttonClickStatus = this.m_ClickStatus[pointerEvent.pointerId];
				VisualElement visualElement = evt.target as VisualElement;
				bool flag2 = visualElement != null && visualElement.worldBound.Contains(pointerEvent.position);
				if (flag2)
				{
					bool flag3 = buttonClickStatus.m_Target != null && buttonClickStatus.m_ClickCount > 0;
					if (flag3)
					{
						VisualElement visualElement2 = buttonClickStatus.m_Target.FindCommonAncestor(evt.target as VisualElement);
						bool flag4 = visualElement2 != null;
						if (flag4)
						{
							using (ClickEvent pooled = ClickEvent.GetPooled(evt as PointerUpEvent, buttonClickStatus.m_ClickCount))
							{
								pooled.target = visualElement2;
								visualElement2.SendEvent(pooled);
							}
						}
					}
				}
			}
		}

		private void CancelClickTracking(EventBase evt)
		{
			IPointerEvent pointerEvent = evt as IPointerEvent;
			bool flag = pointerEvent == null;
			if (!flag)
			{
				ClickDetector.ButtonClickStatus buttonClickStatus = this.m_ClickStatus[pointerEvent.pointerId];
				buttonClickStatus.Reset();
			}
		}

		public void ProcessEvent(EventBase evt)
		{
			IPointerEvent pointerEvent = evt as IPointerEvent;
			bool flag = pointerEvent == null;
			if (!flag)
			{
				bool flag2 = evt.eventTypeId == EventBase<PointerDownEvent>.TypeId() && pointerEvent.button == 0;
				if (flag2)
				{
					this.StartClickTracking(evt);
				}
				else
				{
					bool flag3 = evt.eventTypeId == EventBase<PointerMoveEvent>.TypeId();
					if (flag3)
					{
						bool flag4 = pointerEvent.button == 0 && (pointerEvent.pressedButtons & 1) == 1;
						if (flag4)
						{
							this.StartClickTracking(evt);
						}
						else
						{
							bool flag5 = pointerEvent.button == 0 && (pointerEvent.pressedButtons & 1) == 0;
							if (flag5)
							{
								this.SendClickEvent(evt);
							}
							else
							{
								ClickDetector.ButtonClickStatus buttonClickStatus = this.m_ClickStatus[pointerEvent.pointerId];
								bool flag6 = buttonClickStatus.m_Target != null;
								if (flag6)
								{
									buttonClickStatus.m_LastPointerDownTime = 0L;
								}
							}
						}
					}
					else
					{
						bool flag7 = evt.eventTypeId == EventBase<PointerCancelEvent>.TypeId() || evt.eventTypeId == EventBase<PointerStationaryEvent>.TypeId() || evt.eventTypeId == EventBase<DragUpdatedEvent>.TypeId();
						if (flag7)
						{
							this.CancelClickTracking(evt);
						}
						else
						{
							bool flag8 = evt.eventTypeId == EventBase<PointerUpEvent>.TypeId() && pointerEvent.button == 0;
							if (flag8)
							{
								this.SendClickEvent(evt);
							}
						}
					}
				}
			}
		}

		static ClickDetector()
		{
			// Note: this type is marked as 'beforefieldinit'.
			ClickDetector.<s_DoubleClickTime>k__BackingField = -1;
		}
	}
}
