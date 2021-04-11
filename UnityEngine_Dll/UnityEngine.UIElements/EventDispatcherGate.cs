using System;

namespace UnityEngine.UIElements
{
	public struct EventDispatcherGate : IDisposable, IEquatable<EventDispatcherGate>
	{
		private readonly EventDispatcher m_Dispatcher;

		public EventDispatcherGate(EventDispatcher d)
		{
			bool flag = d == null;
			if (flag)
			{
				throw new ArgumentNullException("d");
			}
			this.m_Dispatcher = d;
			this.m_Dispatcher.CloseGate();
		}

		public void Dispose()
		{
			this.m_Dispatcher.OpenGate();
		}

		public bool Equals(EventDispatcherGate other)
		{
			return object.Equals(this.m_Dispatcher, other.m_Dispatcher);
		}

		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is EventDispatcherGate && this.Equals((EventDispatcherGate)obj);
		}

		public override int GetHashCode()
		{
			return (this.m_Dispatcher != null) ? this.m_Dispatcher.GetHashCode() : 0;
		}

		public static bool operator ==(EventDispatcherGate left, EventDispatcherGate right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(EventDispatcherGate left, EventDispatcherGate right)
		{
			return !left.Equals(right);
		}
	}
}
