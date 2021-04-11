using System;

namespace UnityEngine
{
	public interface ISubsystemDescriptor
	{
		string id
		{
			get;
		}

		ISubsystem Create();
	}
}
