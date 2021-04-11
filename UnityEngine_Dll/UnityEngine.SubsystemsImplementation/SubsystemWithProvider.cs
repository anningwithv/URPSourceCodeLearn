using System;

namespace UnityEngine.SubsystemsImplementation
{
	public abstract class SubsystemWithProvider : ISubsystem
	{
		public bool running
		{
			get;
			private set;
		}

		internal SubsystemProvider providerBase
		{
			get;
			set;
		}

		internal abstract SubsystemDescriptorWithProvider descriptor
		{
			get;
		}

		public void Start()
		{
			bool running = this.running;
			if (!running)
			{
				this.OnStart();
				this.providerBase.m_Running = true;
				this.running = true;
			}
		}

		protected abstract void OnStart();

		public void Stop()
		{
			bool flag = !this.running;
			if (!flag)
			{
				this.OnStop();
				this.providerBase.m_Running = false;
				this.running = false;
			}
		}

		protected abstract void OnStop();

		public void Destroy()
		{
			this.Stop();
			bool flag = SubsystemManager.RemoveStandaloneSubsystem(this);
			if (flag)
			{
				this.OnDestroy();
			}
		}

		protected abstract void OnDestroy();

		internal abstract void Initialize(SubsystemDescriptorWithProvider descriptor, SubsystemProvider subsystemProvider);
	}
	public abstract class SubsystemWithProvider<TSubsystem, TSubsystemDescriptor, TProvider> : SubsystemWithProvider where TSubsystem : SubsystemWithProvider, new() where TSubsystemDescriptor : SubsystemDescriptorWithProvider where TProvider : SubsystemProvider<TSubsystem>
	{
		public TSubsystemDescriptor subsystemDescriptor
		{
			get;
			private set;
		}

		protected internal TProvider provider
		{
			get;
			private set;
		}

		internal sealed override SubsystemDescriptorWithProvider descriptor
		{
			get
			{
				return this.subsystemDescriptor;
			}
		}

		protected virtual void OnCreate()
		{
		}

		protected override void OnStart()
		{
			this.provider.Start();
		}

		protected override void OnStop()
		{
			this.provider.Stop();
		}

		protected override void OnDestroy()
		{
			this.provider.Destroy();
		}

		internal sealed override void Initialize(SubsystemDescriptorWithProvider descriptor, SubsystemProvider provider)
		{
			base.providerBase = provider;
			this.provider = (TProvider)((object)provider);
			this.subsystemDescriptor = (TSubsystemDescriptor)((object)descriptor);
			this.OnCreate();
		}
	}
}
