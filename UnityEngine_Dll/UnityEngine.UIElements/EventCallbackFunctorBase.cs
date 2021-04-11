using System;

namespace UnityEngine.UIElements
{
	internal abstract class EventCallbackFunctorBase
	{
		public CallbackPhase phase
		{
			get;
			private set;
		}

		protected EventCallbackFunctorBase(CallbackPhase phase)
		{
			this.phase = phase;
		}

		public abstract void Invoke(EventBase evt);

		public abstract bool IsEquivalentTo(long eventTypeId, Delegate callback, CallbackPhase phase);

		protected bool PhaseMatches(EventBase evt)
		{
			CallbackPhase phase = this.phase;
			CallbackPhase callbackPhase = phase;
			bool result;
			if (callbackPhase != CallbackPhase.TargetAndBubbleUp)
			{
				if (callbackPhase == CallbackPhase.TrickleDownAndTarget)
				{
					bool flag = evt.propagationPhase != PropagationPhase.TrickleDown && evt.propagationPhase != PropagationPhase.AtTarget;
					if (flag)
					{
						result = false;
						return result;
					}
				}
			}
			else
			{
				bool flag2 = evt.propagationPhase != PropagationPhase.AtTarget && evt.propagationPhase != PropagationPhase.BubbleUp;
				if (flag2)
				{
					result = false;
					return result;
				}
			}
			result = true;
			return result;
		}
	}
}
