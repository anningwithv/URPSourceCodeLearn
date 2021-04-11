using System;

namespace UnityEngine.UIElements
{
	internal abstract class DragEventsProcessor
	{
		private bool m_CanStartDrag;

		private Vector3 m_Start;

		internal readonly VisualElement m_Target;

		private const int k_DistanceToActivation = 5;

		internal DragEventsProcessor(VisualElement target)
		{
			this.m_Target = target;
			this.m_Target.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDownEvent), TrickleDown.TrickleDown);
			this.m_Target.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUpEvent), TrickleDown.TrickleDown);
			this.m_Target.RegisterCallback<PointerLeaveEvent>(new EventCallback<PointerLeaveEvent>(this.OnPointerLeaveEvent), TrickleDown.NoTrickleDown);
			this.m_Target.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMoveEvent), TrickleDown.NoTrickleDown);
			this.m_Target.RegisterCallback<DragUpdatedEvent>(new EventCallback<DragUpdatedEvent>(this.OnDragUpdate), TrickleDown.NoTrickleDown);
			this.m_Target.RegisterCallback<DragPerformEvent>(new EventCallback<DragPerformEvent>(this.OnDragPerformEvent), TrickleDown.NoTrickleDown);
			this.m_Target.RegisterCallback<DragExitedEvent>(new EventCallback<DragExitedEvent>(this.OnDragExitedEvent), TrickleDown.NoTrickleDown);
			this.m_Target.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.UnregisterCallbacksFromTarget), TrickleDown.NoTrickleDown);
		}

		private void UnregisterCallbacksFromTarget(DetachFromPanelEvent evt)
		{
			this.m_Target.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDownEvent), TrickleDown.NoTrickleDown);
			this.m_Target.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUpEvent), TrickleDown.NoTrickleDown);
			this.m_Target.UnregisterCallback<PointerLeaveEvent>(new EventCallback<PointerLeaveEvent>(this.OnPointerLeaveEvent), TrickleDown.NoTrickleDown);
			this.m_Target.UnregisterCallback<DragUpdatedEvent>(new EventCallback<DragUpdatedEvent>(this.OnDragUpdate), TrickleDown.NoTrickleDown);
			this.m_Target.UnregisterCallback<DragPerformEvent>(new EventCallback<DragPerformEvent>(this.OnDragPerformEvent), TrickleDown.NoTrickleDown);
			this.m_Target.UnregisterCallback<DragExitedEvent>(new EventCallback<DragExitedEvent>(this.OnDragExitedEvent), TrickleDown.NoTrickleDown);
			this.m_Target.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMoveEvent), TrickleDown.NoTrickleDown);
			this.m_Target.UnregisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.UnregisterCallbacksFromTarget), TrickleDown.NoTrickleDown);
		}

		protected abstract bool CanStartDrag(Vector3 pointerPosition);

		protected abstract StartDragArgs StartDrag(Vector3 pointerPosition);

		protected abstract DragVisualMode UpdateDrag(Vector3 pointerPosition);

		protected abstract void OnDrop(Vector3 pointerPosition);

		protected abstract void ClearDragAndDropUI();

		internal void OnPointerUp()
		{
			this.m_CanStartDrag = false;
		}

		private void OnPointerDownEvent(PointerDownEvent evt)
		{
			bool flag = evt.button != 0;
			if (flag)
			{
				this.m_CanStartDrag = false;
			}
			else
			{
				bool flag2 = this.CanStartDrag(evt.position);
				if (flag2)
				{
					this.m_CanStartDrag = true;
					this.m_Start = evt.position;
				}
			}
		}

		private void OnPointerUpEvent(PointerUpEvent evt)
		{
			this.OnPointerUp();
		}

		private void OnPointerLeaveEvent(PointerLeaveEvent evt)
		{
			bool flag = evt.target == this.m_Target;
			if (flag)
			{
				this.ClearDragAndDropUI();
			}
		}

		private void OnDragExitedEvent(DragExitedEvent evt)
		{
			this.ClearDragAndDropUI();
		}

		private void OnDragPerformEvent(DragPerformEvent evt)
		{
			this.m_CanStartDrag = false;
			this.OnDrop(evt.mousePosition);
			this.ClearDragAndDropUI();
			DragAndDropUtility.dragAndDrop.AcceptDrag();
		}

		private void OnDragUpdate(DragUpdatedEvent evt)
		{
			DragVisualMode visualMode = this.UpdateDrag(evt.mousePosition);
			DragAndDropUtility.dragAndDrop.SetVisualMode(visualMode);
		}

		private void OnPointerMoveEvent(PointerMoveEvent evt)
		{
			bool flag = !this.m_CanStartDrag;
			if (!flag)
			{
				bool flag2 = Mathf.Abs(this.m_Start.x - evt.position.x) > 5f || Mathf.Abs(this.m_Start.y - evt.position.y) > 5f;
				if (flag2)
				{
					StartDragArgs args = this.StartDrag(evt.position);
					DragAndDropUtility.dragAndDrop.StartDrag(args);
					this.m_CanStartDrag = false;
				}
			}
		}
	}
}
