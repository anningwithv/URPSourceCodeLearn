using System;
using System.Runtime.CompilerServices;

namespace JetBrains.Annotations
{
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class NotifyPropertyChangedInvocatorAttribute : Attribute
	{
		[CanBeNull]
		public string ParameterName
		{
			[CompilerGenerated]
			get
			{
				return this.<ParameterName>k__BackingField;
			}
		}

		public NotifyPropertyChangedInvocatorAttribute()
		{
		}

		public NotifyPropertyChangedInvocatorAttribute([NotNull] string parameterName)
		{
			this.<ParameterName>k__BackingField = parameterName;
		}
	}
}
