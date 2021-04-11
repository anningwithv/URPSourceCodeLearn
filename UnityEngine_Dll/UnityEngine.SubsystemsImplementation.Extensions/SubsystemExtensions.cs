using System;

namespace UnityEngine.SubsystemsImplementation.Extensions
{
	public static class SubsystemExtensions
	{
		public static TProvider GetProvider<TSubsystem, TDescriptor, TProvider>(this SubsystemWithProvider<TSubsystem, TDescriptor, TProvider> subsystem) where TSubsystem : SubsystemWithProvider, new() where TDescriptor : SubsystemDescriptorWithProvider<TSubsystem, TProvider> where TProvider : SubsystemProvider<TSubsystem>
		{
			return subsystem.provider;
		}
	}
}
