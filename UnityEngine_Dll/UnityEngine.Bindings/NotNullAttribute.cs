using System;

namespace UnityEngine.Bindings
{
	[AttributeUsage(AttributeTargets.Parameter), VisibleToOtherModules]
	internal class NotNullAttribute : Attribute, IBindingsAttribute
	{
		public string Exception
		{
			get;
			set;
		}

		public NotNullAttribute(string exception = "ArgumentNullException")
		{
			this.Exception = exception;
		}
	}
}
