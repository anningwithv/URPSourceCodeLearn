using System;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering
{
	[RequiredByNativeCode]
	internal class ScriptableRuntimeReflectionSystemWrapper
	{
		internal IScriptableRuntimeReflectionSystem implementation
		{
			get;
			set;
		}

		[RequiredByNativeCode]
		private void Internal_ScriptableRuntimeReflectionSystemWrapper_TickRealtimeProbes(out bool result)
		{
			result = (this.implementation != null && this.implementation.TickRealtimeProbes());
		}
	}
}
