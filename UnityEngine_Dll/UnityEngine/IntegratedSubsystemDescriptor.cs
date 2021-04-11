using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[UsedByNativeCode("SubsystemDescriptorBase")]
	[StructLayout(LayoutKind.Sequential)]
	public abstract class IntegratedSubsystemDescriptor : ISubsystemDescriptorImpl, ISubsystemDescriptor
	{
		internal IntPtr m_Ptr;

		public string id
		{
			get
			{
				return SubsystemDescriptorBindings.GetId(this.m_Ptr);
			}
		}

		IntPtr ISubsystemDescriptorImpl.ptr
		{
			get
			{
				return this.m_Ptr;
			}
			set
			{
				this.m_Ptr = value;
			}
		}

		ISubsystem ISubsystemDescriptor.Create()
		{
			return this.CreateImpl();
		}

		internal abstract ISubsystem CreateImpl();
	}
	[NativeHeader("Modules/Subsystems/SubsystemDescriptor.h"), UsedByNativeCode("SubsystemDescriptor")]
	[StructLayout(LayoutKind.Sequential)]
	public class IntegratedSubsystemDescriptor<TSubsystem> : IntegratedSubsystemDescriptor where TSubsystem : IntegratedSubsystem
	{
		internal override ISubsystem CreateImpl()
		{
			return this.Create();
		}

		public TSubsystem Create()
		{
			IntPtr ptr = SubsystemDescriptorBindings.Create(this.m_Ptr);
			TSubsystem tSubsystem = (TSubsystem)((object)SubsystemManager.GetIntegratedSubsystemByPtr(ptr));
			bool flag = tSubsystem != null;
			if (flag)
			{
				tSubsystem.m_SubsystemDescriptor = this;
			}
			return tSubsystem;
		}
	}
}
