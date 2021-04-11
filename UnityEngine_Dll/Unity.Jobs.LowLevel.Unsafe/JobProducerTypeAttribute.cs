using System;
using System.Runtime.CompilerServices;

namespace Unity.Jobs.LowLevel.Unsafe
{
	[AttributeUsage(AttributeTargets.Interface)]
	public sealed class JobProducerTypeAttribute : Attribute
	{
		public Type ProducerType
		{
			[CompilerGenerated]
			get
			{
				return this.<ProducerType>k__BackingField;
			}
		}

		public JobProducerTypeAttribute(Type producerType)
		{
			this.<ProducerType>k__BackingField = producerType;
		}
	}
}
