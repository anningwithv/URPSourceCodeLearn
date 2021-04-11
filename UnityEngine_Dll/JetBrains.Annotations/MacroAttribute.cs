using System;

namespace JetBrains.Annotations
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter, AllowMultiple = true)]
	public sealed class MacroAttribute : Attribute
	{
		[CanBeNull]
		public string Expression
		{
			get;
			set;
		}

		public int Editable
		{
			get;
			set;
		}

		[CanBeNull]
		public string Target
		{
			get;
			set;
		}
	}
}
