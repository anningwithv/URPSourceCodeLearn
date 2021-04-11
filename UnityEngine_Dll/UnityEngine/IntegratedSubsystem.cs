using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Modules/Subsystems/Subsystem.h"), UsedByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class IntegratedSubsystem : ISubsystem
	{
		internal IntPtr m_Ptr;

		internal ISubsystemDescriptor m_SubsystemDescriptor;

		public bool running
		{
			get
			{
				return this.valid && this.IsRunning();
			}
		}

		internal bool valid
		{
			get
			{
				return this.m_Ptr != IntPtr.Zero;
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetHandle(IntegratedSubsystem subsystem);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Start();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Stop();

		public void Destroy()
		{
			IntPtr ptr = this.m_Ptr;
			SubsystemManager.RemoveIntegratedSubsystemByPtr(this.m_Ptr);
			SubsystemBindings.DestroySubsystem(ptr);
			this.m_Ptr = IntPtr.Zero;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsRunning();
	}
	[UsedByNativeCode("Subsystem_TSubsystemDescriptor")]
	public class IntegratedSubsystem<TSubsystemDescriptor> : IntegratedSubsystem where TSubsystemDescriptor : ISubsystemDescriptor
	{
		public TSubsystemDescriptor subsystemDescriptor
		{
			get
			{
				return (TSubsystemDescriptor)((object)this.m_SubsystemDescriptor);
			}
		}

		public TSubsystemDescriptor SubsystemDescriptor
		{
			get
			{
				return this.subsystemDescriptor;
			}
		}
	}
}
