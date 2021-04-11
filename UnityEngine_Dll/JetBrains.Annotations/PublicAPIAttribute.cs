using System;
using System.Runtime.CompilerServices;

namespace JetBrains.Annotations
{
	[MeansImplicitUse(ImplicitUseTargetFlags.WithMembers), AttributeUsage(AttributeTargets.All, Inherited = false)]
	public sealed class PublicAPIAttribute : Attribute
	{
		[CanBeNull]
		public string Comment
		{
			[CompilerGenerated]
			get
			{
				return this.<Comment>k__BackingField;
			}
		}

		public PublicAPIAttribute()
		{
		}

		public PublicAPIAttribute([NotNull] string comment)
		{
			this.<Comment>k__BackingField = comment;
		}
	}
}
