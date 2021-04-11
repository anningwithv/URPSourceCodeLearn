using System;

namespace UnityEngine.SubsystemsImplementation
{
	public abstract class SubsystemDescriptorWithProvider : ISubsystemDescriptor
	{
		public string id
		{
			get;
			set;
		}

		protected internal Type providerType
		{
			get;
			set;
		}

		protected internal Type subsystemTypeOverride
		{
			get;
			set;
		}

		internal abstract ISubsystem CreateImpl();

		ISubsystem ISubsystemDescriptor.Create()
		{
			return this.CreateImpl();
		}

		internal abstract void ThrowIfInvalid();
	}
	public class SubsystemDescriptorWithProvider<TSubsystem, TProvider> : SubsystemDescriptorWithProvider where TSubsystem : SubsystemWithProvider, new() where TProvider : SubsystemProvider<TSubsystem>
	{
		internal override ISubsystem CreateImpl()
		{
			return this.Create();
		}

		public TSubsystem Create()
		{
			TSubsystem tSubsystem = SubsystemManager.FindStandaloneSubsystemByDescriptor(this) as TSubsystem;
			bool flag = tSubsystem != null;
			TSubsystem result;
			if (flag)
			{
				result = tSubsystem;
			}
			else
			{
				TProvider tProvider = this.CreateProvider();
				bool flag2 = tProvider == null;
				if (flag2)
				{
					result = default(TSubsystem);
				}
				else
				{
					tSubsystem = ((base.subsystemTypeOverride != null) ? ((TSubsystem)((object)Activator.CreateInstance(base.subsystemTypeOverride))) : Activator.CreateInstance<TSubsystem>());
					tSubsystem.Initialize(this, tProvider);
					SubsystemManager.AddStandaloneSubsystem(tSubsystem);
					result = tSubsystem;
				}
			}
			return result;
		}

		internal sealed override void ThrowIfInvalid()
		{
			bool flag = base.providerType == null;
			if (flag)
			{
				throw new InvalidOperationException("Invalid descriptor - must supply a valid providerType field!");
			}
			bool flag2 = !base.providerType.IsSubclassOf(typeof(TProvider));
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("Can't create provider - providerType '{0}' is not a subclass of '{1}'!", base.providerType.ToString(), typeof(TProvider).ToString()));
			}
			bool flag3 = base.subsystemTypeOverride != null && !base.subsystemTypeOverride.IsSubclassOf(typeof(TSubsystem));
			if (flag3)
			{
				throw new InvalidOperationException(string.Format("Can't create provider - subsystemTypeOverride '{0}' is not a subclass of '{1}'!", base.subsystemTypeOverride.ToString(), typeof(TSubsystem).ToString()));
			}
		}

		internal TProvider CreateProvider()
		{
			TProvider tProvider = (TProvider)((object)Activator.CreateInstance(base.providerType));
			return tProvider.TryInitialize() ? tProvider : default(TProvider);
		}
	}
}
