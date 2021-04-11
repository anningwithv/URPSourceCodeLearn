using System;
using System.Runtime.CompilerServices;

namespace JetBrains.Annotations
{
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Delegate)]
	public sealed class StringFormatMethodAttribute : Attribute
	{
		[NotNull]
		public string FormatParameterName
		{
			[CompilerGenerated]
			get
			{
				return this.<FormatParameterName>k__BackingField;
			}
		}

		public StringFormatMethodAttribute([NotNull] string formatParameterName)
		{
			this.<FormatParameterName>k__BackingField = formatParameterName;
		}
	}
}
