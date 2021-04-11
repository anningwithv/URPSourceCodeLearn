using System;
using System.Runtime.CompilerServices;

namespace JetBrains.Annotations
{
	[BaseTypeRequired(typeof(Attribute)), AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class BaseTypeRequiredAttribute : Attribute
	{
		[NotNull]
		public Type BaseType
		{
			[CompilerGenerated]
			get
			{
				return this.<BaseType>k__BackingField;
			}
		}

		public BaseTypeRequiredAttribute([NotNull] Type baseType)
		{
			this.<BaseType>k__BackingField = baseType;
		}
	}
}
