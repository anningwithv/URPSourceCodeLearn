using System;

namespace UnityEngine.UIElements
{
	internal class EventCallbackFunctor<TEventType> : EventCallbackFunctorBase where TEventType : EventBase<TEventType>, new()
	{
		private readonly EventCallback<TEventType> m_Callback;

		private readonly long m_EventTypeId;

		public EventCallbackFunctor(EventCallback<TEventType> callback, CallbackPhase phase) : base(phase)
		{
			this.m_Callback = callback;
			this.m_EventTypeId = EventBase<TEventType>.TypeId();
		}

		public override void Invoke(EventBase evt)
		{
			bool flag = evt == null;
			if (flag)
			{
				throw new ArgumentNullException("evt");
			}
			bool flag2 = evt.eventTypeId != this.m_EventTypeId;
			if (!flag2)
			{
				bool flag3 = base.PhaseMatches(evt);
				if (flag3)
				{
					using (new EventDebuggerLogCall(this.m_Callback, evt))
					{
						this.m_Callback(evt as TEventType);
					}
				}
			}
		}

		public override bool IsEquivalentTo(long eventTypeId, Delegate callback, CallbackPhase phase)
		{
			return this.m_EventTypeId == eventTypeId && this.m_Callback == callback && base.phase == phase;
		}
	}
	internal class EventCallbackFunctor<TEventType, TCallbackArgs> : EventCallbackFunctorBase where TEventType : EventBase<TEventType>, new()
	{
		private readonly EventCallback<TEventType, TCallbackArgs> m_Callback;

		private readonly long m_EventTypeId;

		internal TCallbackArgs userArgs
		{
			get;
			set;
		}

		public EventCallbackFunctor(EventCallback<TEventType, TCallbackArgs> callback, TCallbackArgs userArgs, CallbackPhase phase) : base(phase)
		{
			this.userArgs = userArgs;
			this.m_Callback = callback;
			this.m_EventTypeId = EventBase<TEventType>.TypeId();
		}

		public override void Invoke(EventBase evt)
		{
			bool flag = evt == null;
			if (flag)
			{
				throw new ArgumentNullException("evt");
			}
			bool flag2 = evt.eventTypeId != this.m_EventTypeId;
			if (!flag2)
			{
				bool flag3 = base.PhaseMatches(evt);
				if (flag3)
				{
					using (new EventDebuggerLogCall(this.m_Callback, evt))
					{
						this.m_Callback(evt as TEventType, this.userArgs);
					}
				}
			}
		}

		public override bool IsEquivalentTo(long eventTypeId, Delegate callback, CallbackPhase phase)
		{
			return this.m_EventTypeId == eventTypeId && this.m_Callback == callback && base.phase == phase;
		}
	}
}
