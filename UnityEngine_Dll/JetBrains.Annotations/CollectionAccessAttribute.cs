using System;
using System.Runtime.CompilerServices;

namespace JetBrains.Annotations
{
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property)]
	public sealed class CollectionAccessAttribute : Attribute
	{
		public CollectionAccessType CollectionAccessType
		{
			[CompilerGenerated]
			get
			{
				return this.<CollectionAccessType>k__BackingField;
			}
		}

		public CollectionAccessAttribute(CollectionAccessType collectionAccessType)
		{
			this.<CollectionAccessType>k__BackingField = collectionAccessType;
		}
	}
}
