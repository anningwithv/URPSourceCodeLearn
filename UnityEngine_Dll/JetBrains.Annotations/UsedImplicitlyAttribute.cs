using System;
using System.Runtime.CompilerServices;

namespace JetBrains.Annotations
{
	[AttributeUsage(AttributeTargets.All, Inherited = false)]
	public sealed class UsedImplicitlyAttribute : Attribute
	{
		public ImplicitUseKindFlags UseKindFlags
		{
			[CompilerGenerated]
			get
			{
				return this.<UseKindFlags>k__BackingField;
			}
		}

		public ImplicitUseTargetFlags TargetFlags
		{
			[CompilerGenerated]
			get
			{
				return this.<TargetFlags>k__BackingField;
			}
		}

		public UsedImplicitlyAttribute() : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
		{
		}

		public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags) : this(useKindFlags, ImplicitUseTargetFlags.Default)
		{
		}

		public UsedImplicitlyAttribute(ImplicitUseTargetFlags targetFlags) : this(ImplicitUseKindFlags.Default, targetFlags)
		{
		}

		public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
		{
			this.<UseKindFlags>k__BackingField = useKindFlags;
			this.<TargetFlags>k__BackingField = targetFlags;
		}
	}
}
