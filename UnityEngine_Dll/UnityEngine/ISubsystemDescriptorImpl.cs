using System;

namespace UnityEngine
{
	internal interface ISubsystemDescriptorImpl : ISubsystemDescriptor
	{
		IntPtr ptr
		{
			get;
			set;
		}
	}
}
