using System;
using System.Runtime.CompilerServices;

namespace JetBrains.Annotations
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
	public sealed class ValueProviderAttribute : Attribute
	{
		[NotNull]
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
		}

		public ValueProviderAttribute([NotNull] string name)
		{
			this.<Name>k__BackingField = name;
		}
	}
}
