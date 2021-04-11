using System;

namespace UnityEngine.Bindings
{
	[VisibleToOtherModules]
	internal enum PreventExecutionSeverity
	{
		PreventExecution_Error,
		PreventExecution_ManagedException,
		PreventExecution_NativeException
	}
}
