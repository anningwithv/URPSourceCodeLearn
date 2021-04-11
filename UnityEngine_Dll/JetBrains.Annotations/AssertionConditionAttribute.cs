using System;
using System.Runtime.CompilerServices;

namespace JetBrains.Annotations
{
	[AttributeUsage(AttributeTargets.Parameter)]
	public sealed class AssertionConditionAttribute : Attribute
	{
		public AssertionConditionType ConditionType
		{
			[CompilerGenerated]
			get
			{
				return this.<ConditionType>k__BackingField;
			}
		}

		public AssertionConditionAttribute(AssertionConditionType conditionType)
		{
			this.<ConditionType>k__BackingField = conditionType;
		}
	}
}
