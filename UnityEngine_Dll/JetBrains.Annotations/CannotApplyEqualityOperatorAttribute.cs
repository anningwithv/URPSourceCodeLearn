using System;

namespace JetBrains.Annotations
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
	public sealed class CannotApplyEqualityOperatorAttribute : Attribute
	{
	}
}
