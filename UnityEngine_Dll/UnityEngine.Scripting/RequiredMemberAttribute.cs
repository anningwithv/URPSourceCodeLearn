using System;

namespace UnityEngine.Scripting
{
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event)]
	public class RequiredMemberAttribute : Attribute
	{
	}
}
