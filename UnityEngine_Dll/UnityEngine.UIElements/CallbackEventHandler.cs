using System;

namespace UnityEngine.UIElements
{
	public abstract class CallbackEventHandler : IEventHandler
	{
		private EventCallbackRegistry m_CallbackRegistry;

		public void RegisterCallback<TEventType>(EventCallback<TEventType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = this.m_CallbackRegistry == null;
			if (flag)
			{
				this.m_CallbackRegistry = new EventCallbackRegistry();
			}
			this.m_CallbackRegistry.RegisterCallback<TEventType>(callback, useTrickleDown);
			GlobalCallbackRegistry.RegisterListeners<TEventType>(this, callback, useTrickleDown);
		}

		public void RegisterCallback<TEventType, TUserArgsType>(EventCallback<TEventType, TUserArgsType> callback, TUserArgsType userArgs, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = this.m_CallbackRegistry == null;
			if (flag)
			{
				this.m_CallbackRegistry = new EventCallbackRegistry();
			}
			this.m_CallbackRegistry.RegisterCallback<TEventType, TUserArgsType>(callback, userArgs, useTrickleDown);
			GlobalCallbackRegistry.RegisterListeners<TEventType>(this, callback, useTrickleDown);
		}

		public void UnregisterCallback<TEventType>(EventCallback<TEventType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = this.m_CallbackRegistry != null;
			if (flag)
			{
				this.m_CallbackRegistry.UnregisterCallback<TEventType>(callback, useTrickleDown);
			}
			GlobalCallbackRegistry.UnregisterListeners<TEventType>(this, callback);
		}

		public void UnregisterCallback<TEventType, TUserArgsType>(EventCallback<TEventType, TUserArgsType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = this.m_CallbackRegistry != null;
			if (flag)
			{
				this.m_CallbackRegistry.UnregisterCallback<TEventType, TUserArgsType>(callback, useTrickleDown);
			}
			GlobalCallbackRegistry.UnregisterListeners<TEventType>(this, callback);
		}

		internal bool TryGetUserArgs<TEventType, TCallbackArgs>(EventCallback<TEventType, TCallbackArgs> callback, TrickleDown useTrickleDown, out TCallbackArgs userData) where TEventType : EventBase<TEventType>, new()
		{
			userData = default(TCallbackArgs);
			bool flag = this.m_CallbackRegistry != null;
			return flag && this.m_CallbackRegistry.TryGetUserArgs<TEventType, TCallbackArgs>(callback, useTrickleDown, out userData);
		}

		public abstract void SendEvent(EventBase e);

		internal void HandleEventAtTargetPhase(EventBase evt)
		{
			evt.currentTarget = evt.target;
			evt.propagationPhase = PropagationPhase.AtTarget;
			this.HandleEvent(evt);
			evt.propagationPhase = PropagationPhase.DefaultActionAtTarget;
			this.HandleEvent(evt);
		}

		public virtual void HandleEvent(EventBase evt)
		{
			bool flag = evt == null;
			if (!flag)
			{
				switch (evt.propagationPhase)
				{
				case PropagationPhase.TrickleDown:
				case PropagationPhase.AtTarget:
				case PropagationPhase.BubbleUp:
				{
					bool flag2 = !evt.isPropagationStopped;
					if (flag2)
					{
						EventCallbackRegistry expr_4D = this.m_CallbackRegistry;
						if (expr_4D != null)
						{
							expr_4D.InvokeCallbacks(evt);
						}
					}
					break;
				}
				case PropagationPhase.DefaultAction:
				{
					bool flag3 = !evt.isDefaultPrevented;
					if (flag3)
					{
						using (new EventDebuggerLogExecuteDefaultAction(evt))
						{
							this.ExecuteDefaultAction(evt);
						}
					}
					break;
				}
				case PropagationPhase.DefaultActionAtTarget:
				{
					bool flag4 = !evt.isDefaultPrevented;
					if (flag4)
					{
						using (new EventDebuggerLogExecuteDefaultAction(evt))
						{
							this.ExecuteDefaultActionAtTarget(evt);
						}
					}
					break;
				}
				}
			}
		}

		public bool HasTrickleDownHandlers()
		{
			return this.m_CallbackRegistry != null && this.m_CallbackRegistry.HasTrickleDownHandlers();
		}

		public bool HasBubbleUpHandlers()
		{
			return this.m_CallbackRegistry != null && this.m_CallbackRegistry.HasBubbleHandlers();
		}

		protected virtual void ExecuteDefaultActionAtTarget(EventBase evt)
		{
		}

		protected virtual void ExecuteDefaultAction(EventBase evt)
		{
		}
	}
}
