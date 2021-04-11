using System;
using System.Runtime.CompilerServices;

namespace JetBrains.Annotations
{
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class MustUseReturnValueAttribute : Attribute
	{
		[CanBeNull]
		public string Justification
		{
			[CompilerGenerated]
			get
			{
				return this.<Justification>k__BackingField;
			}
		}

		public MustUseReturnValueAttribute()
		{
		}

		public MustUseReturnValueAttribute([NotNull] string justification)
		{
			this.<Justification>k__BackingField = justification;
		}
	}
}
