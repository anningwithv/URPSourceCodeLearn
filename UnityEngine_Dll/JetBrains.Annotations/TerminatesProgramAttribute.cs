using System;

namespace JetBrains.Annotations
{
	[AttributeUsage(AttributeTargets.Method), Obsolete("Use [ContractAnnotation('=> halt')] instead")]
	public sealed class TerminatesProgramAttribute : Attribute
	{
	}
}
