using System;

namespace UnityEngine.Bindings
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true), VisibleToOtherModules]
	internal class PreventExecutionInStateAttribute : Attribute, IBindingsPreventExecution
	{
		public object singleFlagValue
		{
			get;
			set;
		}

		public PreventExecutionSeverity severity
		{
			get;
			set;
		}

		public string howToFix
		{
			get;
			set;
		}

		public PreventExecutionInStateAttribute(object systemAndFlags, PreventExecutionSeverity reportSeverity, string howToString = "")
		{
			this.singleFlagValue = systemAndFlags;
			this.severity = reportSeverity;
			this.howToFix = howToString;
		}
	}
}
