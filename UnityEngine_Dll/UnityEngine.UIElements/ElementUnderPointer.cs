using System;

namespace UnityEngine.UIElements
{
	internal class ElementUnderPointer
	{
		private VisualElement[] m_PendingTopElementUnderPointer = new VisualElement[PointerId.maxPointers];

		private VisualElement[] m_TopElementUnderPointer = new VisualElement[PointerId.maxPointers];

		private IPointerEvent[] m_TriggerPointerEvent = new IPointerEvent[PointerId.maxPointers];

		private IMouseEvent[] m_TriggerMouseEvent = new IMouseEvent[PointerId.maxPointers];

		private Vector2[] m_PickingPointerPositions = new Vector2[PointerId.maxPointers];

		private bool[] m_IsPickingPointerTemporaries = new bool[PointerId.maxPointers];

		internal VisualElement GetTopElementUnderPointer(int pointerId, out Vector2 pickPosition, out bool isTemporary)
		{
			pickPosition = this.m_PickingPointerPositions[pointerId];
			isTemporary = this.m_IsPickingPointerTemporaries[pointerId];
			return this.m_PendingTopElementUnderPointer[pointerId];
		}

		internal VisualElement GetTopElementUnderPointer(int pointerId)
		{
			return this.m_PendingTopElementUnderPointer[pointerId];
		}

		internal void SetElementUnderPointer(VisualElement newElementUnderPointer, int pointerId, Vector2 pointerPos)
		{
			Debug.Assert(pointerId >= 0);
			VisualElement visualElement = this.m_TopElementUnderPointer[pointerId];
			this.m_IsPickingPointerTemporaries[pointerId] = false;
			this.m_PickingPointerPositions[pointerId] = pointerPos;
			bool flag = newElementUnderPointer == visualElement;
			if (!flag)
			{
				this.m_PendingTopElementUnderPointer[pointerId] = newElementUnderPointer;
				this.m_TriggerPointerEvent[pointerId] = null;
				this.m_TriggerMouseEvent[pointerId] = null;
			}
		}

		private Vector2 GetEventPointerPosition(EventBase triggerEvent)
		{
			IPointerEvent pointerEvent = triggerEvent as IPointerEvent;
			bool flag = pointerEvent != null;
			Vector2 result;
			if (flag)
			{
				result = new Vector2(pointerEvent.position.x, pointerEvent.position.y);
			}
			else
			{
				IMouseEvent mouseEvent = triggerEvent as IMouseEvent;
				bool flag2 = mouseEvent != null;
				if (flag2)
				{
					result = mouseEvent.mousePosition;
				}
				else
				{
					result = new Vector2(-3.40282347E+38f, -3.40282347E+38f);
				}
			}
			return result;
		}

		internal void SetTemporaryElementUnderPointer(VisualElement newElementUnderPointer, EventBase triggerEvent)
		{
			this.SetElementUnderPointer(newElementUnderPointer, triggerEvent, true);
		}

		internal void SetElementUnderPointer(VisualElement newElementUnderPointer, EventBase triggerEvent)
		{
			this.SetElementUnderPointer(newElementUnderPointer, triggerEvent, false);
		}

		private void SetElementUnderPointer(VisualElement newElementUnderPointer, EventBase triggerEvent, bool temporary)
		{
			int num = -1;
			bool flag = triggerEvent is IPointerEvent;
			if (flag)
			{
				num = ((IPointerEvent)triggerEvent).pointerId;
			}
			else
			{
				bool flag2 = triggerEvent is IMouseEvent;
				if (flag2)
				{
					num = PointerId.mousePointerId;
				}
			}
			Debug.Assert(num >= 0);
			this.m_IsPickingPointerTemporaries[num] = temporary;
			this.m_PickingPointerPositions[num] = this.GetEventPointerPosition(triggerEvent);
			VisualElement visualElement = this.m_TopElementUnderPointer[num];
			bool flag3 = newElementUnderPointer == visualElement;
			if (!flag3)
			{
				this.m_PendingTopElementUnderPointer[num] = newElementUnderPointer;
				bool flag4 = this.m_TriggerPointerEvent[num] == null && triggerEvent is IPointerEvent;
				if (flag4)
				{
					this.m_TriggerPointerEvent[num] = (triggerEvent as IPointerEvent);
				}
				bool flag5 = this.m_TriggerMouseEvent[num] == null && triggerEvent is IMouseEvent;
				if (flag5)
				{
					this.m_TriggerMouseEvent[num] = (triggerEvent as IMouseEvent);
				}
			}
		}

		internal void CommitElementUnderPointers(EventDispatcher dispatcher)
		{
			for (int i = 0; i < this.m_TopElementUnderPointer.Length; i++)
			{
				IPointerEvent pointerEvent = this.m_TriggerPointerEvent[i];
				VisualElement visualElement = this.m_TopElementUnderPointer[i];
				VisualElement visualElement2 = this.m_PendingTopElementUnderPointer[i];
				bool flag = visualElement2 == visualElement;
				if (flag)
				{
					bool flag2 = pointerEvent != null;
					if (flag2)
					{
						Vector3 position = pointerEvent.position;
						this.m_PickingPointerPositions[i] = new Vector2(position.x, position.y);
					}
					else
					{
						bool flag3 = this.m_TriggerMouseEvent[i] != null;
						if (flag3)
						{
							this.m_PickingPointerPositions[i] = this.m_TriggerMouseEvent[i].mousePosition;
						}
					}
				}
				else
				{
					this.m_TopElementUnderPointer[i] = visualElement2;
					bool flag4 = pointerEvent == null && this.m_TriggerMouseEvent[i] == null;
					if (flag4)
					{
						using (new EventDispatcherGate(dispatcher))
						{
							Vector2 pointerPosition = PointerDeviceState.GetPointerPosition(i);
							PointerEventsHelper.SendOverOut(visualElement, visualElement2, null, pointerPosition, i);
							PointerEventsHelper.SendEnterLeave<PointerLeaveEvent, PointerEnterEvent>(visualElement, visualElement2, null, pointerPosition, i);
							this.m_PickingPointerPositions[i] = pointerPosition;
							bool flag5 = i == PointerId.mousePointerId;
							if (flag5)
							{
								MouseEventsHelper.SendMouseOverMouseOut(visualElement, visualElement2, null, pointerPosition);
								MouseEventsHelper.SendEnterLeave<MouseLeaveEvent, MouseEnterEvent>(visualElement, visualElement2, null, pointerPosition);
							}
						}
					}
					bool flag6 = pointerEvent != null;
					if (flag6)
					{
						Vector3 position2 = pointerEvent.position;
						this.m_PickingPointerPositions[i] = new Vector2(position2.x, position2.y);
						EventBase eventBase = pointerEvent as EventBase;
						bool flag7 = eventBase != null && (eventBase.eventTypeId == EventBase<PointerMoveEvent>.TypeId() || eventBase.eventTypeId == EventBase<PointerDownEvent>.TypeId() || eventBase.eventTypeId == EventBase<PointerUpEvent>.TypeId() || eventBase.eventTypeId == EventBase<PointerCancelEvent>.TypeId());
						if (flag7)
						{
							using (new EventDispatcherGate(dispatcher))
							{
								PointerEventsHelper.SendOverOut(visualElement, visualElement2, pointerEvent, position2, i);
								PointerEventsHelper.SendEnterLeave<PointerLeaveEvent, PointerEnterEvent>(visualElement, visualElement2, pointerEvent, position2, i);
							}
						}
					}
					this.m_TriggerPointerEvent[i] = null;
					IMouseEvent mouseEvent = this.m_TriggerMouseEvent[i];
					bool flag8 = mouseEvent != null;
					if (flag8)
					{
						Vector2 mousePosition = mouseEvent.mousePosition;
						this.m_PickingPointerPositions[i] = mousePosition;
						EventBase eventBase2 = mouseEvent as EventBase;
						bool flag9 = eventBase2 != null;
						if (flag9)
						{
							bool flag10 = eventBase2.eventTypeId == EventBase<MouseMoveEvent>.TypeId() || eventBase2.eventTypeId == EventBase<MouseDownEvent>.TypeId() || eventBase2.eventTypeId == EventBase<MouseUpEvent>.TypeId() || eventBase2.eventTypeId == EventBase<WheelEvent>.TypeId();
							if (flag10)
							{
								using (new EventDispatcherGate(dispatcher))
								{
									MouseEventsHelper.SendMouseOverMouseOut(visualElement, visualElement2, mouseEvent, mousePosition);
									MouseEventsHelper.SendEnterLeave<MouseLeaveEvent, MouseEnterEvent>(visualElement, visualElement2, mouseEvent, mousePosition);
								}
							}
							else
							{
								bool flag11 = eventBase2.eventTypeId == EventBase<MouseEnterWindowEvent>.TypeId() || eventBase2.eventTypeId == EventBase<MouseLeaveWindowEvent>.TypeId();
								if (flag11)
								{
									using (new EventDispatcherGate(dispatcher))
									{
										PointerEventsHelper.SendOverOut(visualElement, visualElement2, null, mousePosition, i);
										PointerEventsHelper.SendEnterLeave<PointerLeaveEvent, PointerEnterEvent>(visualElement, visualElement2, null, mousePosition, i);
										bool flag12 = i == PointerId.mousePointerId;
										if (flag12)
										{
											MouseEventsHelper.SendMouseOverMouseOut(visualElement, visualElement2, mouseEvent, mousePosition);
											MouseEventsHelper.SendEnterLeave<MouseLeaveEvent, MouseEnterEvent>(visualElement, visualElement2, mouseEvent, mousePosition);
										}
									}
								}
								else
								{
									bool flag13 = eventBase2.eventTypeId == EventBase<DragUpdatedEvent>.TypeId() || eventBase2.eventTypeId == EventBase<DragExitedEvent>.TypeId();
									if (flag13)
									{
										using (new EventDispatcherGate(dispatcher))
										{
											PointerEventsHelper.SendOverOut(visualElement, visualElement2, null, mousePosition, i);
											PointerEventsHelper.SendEnterLeave<PointerLeaveEvent, PointerEnterEvent>(visualElement, visualElement2, null, mousePosition, i);
											MouseEventsHelper.SendMouseOverMouseOut(visualElement, visualElement2, mouseEvent, mousePosition);
											MouseEventsHelper.SendEnterLeave<MouseLeaveEvent, MouseEnterEvent>(visualElement, visualElement2, mouseEvent, mousePosition);
											MouseEventsHelper.SendEnterLeave<DragLeaveEvent, DragEnterEvent>(visualElement, visualElement2, mouseEvent, mousePosition);
										}
									}
								}
							}
						}
						this.m_TriggerMouseEvent[i] = null;
					}
				}
			}
		}
	}
}
