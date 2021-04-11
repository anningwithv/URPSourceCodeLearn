using System;
using UnityEngine.Scripting;

namespace UnityEngine.TestTools
{
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method), UsedByNativeCode]
	public class ExcludeFromCoverageAttribute : Attribute
	{
	}
}
