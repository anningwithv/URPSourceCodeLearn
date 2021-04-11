using System;

namespace UnityEngine
{
	public abstract class SubsystemDescriptor : ISubsystemDescriptor
	{
		public string id
		{
			get;
			set;
		}

		public Type subsystemImplementationType
		{
			get;
			set;
		}

		ISubsystem ISubsystemDescriptor.Create()
		{
			return this.CreateImpl();
		}

		internal abstract ISubsystem CreateImpl();
	}
	public class SubsystemDescriptor<TSubsystem> : SubsystemDescriptor where TSubsystem : Subsystem
	{
		internal override ISubsystem CreateImpl()
		{
			return this.Create();
		}

		public TSubsystem Create()
		{
			TSubsystem tSubsystem = SubsystemManager.FindDeprecatedSubsystemByDescriptor(this) as TSubsystem;
			bool flag = tSubsystem != null;
			TSubsystem result;
			if (flag)
			{
				result = tSubsystem;
			}
			else
			{
				tSubsystem = (Activator.CreateInstance(base.subsystemImplementationType) as TSubsystem);
				tSubsystem.m_SubsystemDescriptor = this;
				SubsystemManager.AddDeprecatedSubsystem(tSubsystem);
				result = tSubsystem;
			}
			return result;
		}
	}
}
