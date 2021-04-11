using System;

namespace UnityEngine.UIElements
{
	internal class PointerDispatchState
	{
		private IEventHandler[] m_PendingPointerCapture = new IEventHandler[PointerId.maxPointers];

		private IEventHandler[] m_PointerCapture = new IEventHandler[PointerId.maxPointers];

		private bool[] m_ShouldSendCompatibilityMouseEvents = new bool[PointerId.maxPointers];

		public PointerDispatchState()
		{
			this.Reset();
		}

		internal void Reset()
		{
			for (int i = 0; i < this.m_PointerCapture.Length; i++)
			{
				this.m_PendingPointerCapture[i] = null;
				this.m_PointerCapture[i] = null;
				this.m_ShouldSendCompatibilityMouseEvents[i] = true;
			}
		}

		public IEventHandler GetCapturingElement(int pointerId)
		{
			return this.m_PendingPointerCapture[pointerId];
		}

		public bool HasPointerCapture(IEventHandler handler, int pointerId)
		{
			return this.m_PendingPointerCapture[pointerId] == handler;
		}

		public void CapturePointer(IEventHandler handler, int pointerId)
		{
			bool flag = pointerId == PointerId.mousePointerId && this.m_PendingPointerCapture[pointerId] != handler && GUIUtility.hotControl != 0;
			if (flag)
			{
				Debug.LogWarning("Should not be capturing when there is a hotcontrol");
			}
			else
			{
				this.m_PendingPointerCapture[pointerId] = handler;
			}
		}

		public void ReleasePointer(int pointerId)
		{
			this.m_PendingPointerCapture[pointerId] = null;
		}

		public void ReleasePointer(IEventHandler handler, int pointerId)
		{
			bool flag = handler == this.m_PendingPointerCapture[pointerId];
			if (flag)
			{
				this.m_PendingPointerCapture[pointerId] = null;
			}
		}

		public void ProcessPointerCapture(int pointerId)
		{
			bool flag = this.m_PointerCapture[pointerId] == this.m_PendingPointerCapture[pointerId];
			if (!flag)
			{
				bool flag2 = this.m_PointerCapture[pointerId] != null;
				if (flag2)
				{
					using (PointerCaptureOutEvent pooled = PointerCaptureEventBase<PointerCaptureOutEvent>.GetPooled(this.m_PointerCapture[pointerId], this.m_PendingPointerCapture[pointerId], pointerId))
					{
						this.m_PointerCapture[pointerId].SendEvent(pooled);
					}
					bool flag3 = pointerId == PointerId.mousePointerId;
					if (flag3)
					{
						using (MouseCaptureOutEvent pooled2 = PointerCaptureEventBase<MouseCaptureOutEvent>.GetPooled(this.m_PointerCapture[pointerId], this.m_PendingPointerCapture[pointerId], pointerId))
						{
							this.m_PointerCapture[pointerId].SendEvent(pooled2);
						}
					}
				}
				bool flag4 = this.m_PendingPointerCapture[pointerId] != null;
				if (flag4)
				{
					using (PointerCaptureEvent pooled3 = PointerCaptureEventBase<PointerCaptureEvent>.GetPooled(this.m_PendingPointerCapture[pointerId], this.m_PointerCapture[pointerId], pointerId))
					{
						this.m_PendingPointerCapture[pointerId].SendEvent(pooled3);
					}
					bool flag5 = pointerId == PointerId.mousePointerId;
					if (flag5)
					{
						using (MouseCaptureEvent pooled4 = PointerCaptureEventBase<MouseCaptureEvent>.GetPooled(this.m_PendingPointerCapture[pointerId], this.m_PointerCapture[pointerId], pointerId))
						{
							this.m_PendingPointerCapture[pointerId].SendEvent(pooled4);
						}
					}
				}
				this.m_PointerCapture[pointerId] = this.m_PendingPointerCapture[pointerId];
			}
		}

		public void ActivateCompatibilityMouseEvents(int pointerId)
		{
			this.m_ShouldSendCompatibilityMouseEvents[pointerId] = true;
		}

		public void PreventCompatibilityMouseEvents(int pointerId)
		{
			this.m_ShouldSendCompatibilityMouseEvents[pointerId] = false;
		}

		public bool ShouldSendCompatibilityMouseEvents(IPointerEvent evt)
		{
			return evt.isPrimary && this.m_ShouldSendCompatibilityMouseEvents[evt.pointerId];
		}
	}
}
