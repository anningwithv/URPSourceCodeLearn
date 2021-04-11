using System;
using System.Runtime.CompilerServices;

namespace JetBrains.Annotations
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public sealed class ContractAnnotationAttribute : Attribute
	{
		[NotNull]
		public string Contract
		{
			[CompilerGenerated]
			get
			{
				return this.<Contract>k__BackingField;
			}
		}

		public bool ForceFullStates
		{
			[CompilerGenerated]
			get
			{
				return this.<ForceFullStates>k__BackingField;
			}
		}

		public ContractAnnotationAttribute([NotNull] string contract) : this(contract, false)
		{
		}

		public ContractAnnotationAttribute([NotNull] string contract, bool forceFullStates)
		{
			this.<Contract>k__BackingField = contract;
			this.<ForceFullStates>k__BackingField = forceFullStates;
		}
	}
}
