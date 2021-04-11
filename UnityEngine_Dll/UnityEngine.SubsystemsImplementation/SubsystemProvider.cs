using System;

namespace UnityEngine.SubsystemsImplementation
{
	public abstract class SubsystemProvider
	{
		internal bool m_Running;

		public bool running
		{
			get
			{
				return this.m_Running;
			}
		}
	}
	public abstract class SubsystemProvider<TSubsystem> : SubsystemProvider where TSubsystem : SubsystemWithProvider, new()
	{
		protected internal virtual bool TryInitialize()
		{
			return true;
		}

		public abstract void Start();

		public abstract void Stop();

		public abstract void Destroy();
	}
}
