using System;

namespace UnityEngine.Bindings
{
	[VisibleToOtherModules]
	internal interface IBindingsPreventExecution
	{
		object singleFlagValue
		{
			get;
			set;
		}

		PreventExecutionSeverity severity
		{
			get;
			set;
		}

		string howToFix
		{
			get;
			set;
		}
	}
}
