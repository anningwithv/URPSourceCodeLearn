using System;
using System.Runtime.CompilerServices;

namespace JetBrains.Annotations
{
	[AttributeUsage(AttributeTargets.All)]
	public sealed class LocalizationRequiredAttribute : Attribute
	{
		public bool Required
		{
			[CompilerGenerated]
			get
			{
				return this.<Required>k__BackingField;
			}
		}

		public LocalizationRequiredAttribute() : this(true)
		{
		}

		public LocalizationRequiredAttribute(bool required)
		{
			this.<Required>k__BackingField = required;
		}
	}
}
