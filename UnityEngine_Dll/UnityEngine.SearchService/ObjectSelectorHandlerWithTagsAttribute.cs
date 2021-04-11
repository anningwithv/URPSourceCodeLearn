using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.SearchService
{
	[AttributeUsage(AttributeTargets.Field)]
	public class ObjectSelectorHandlerWithTagsAttribute : Attribute
	{
		public string[] tags
		{
			[CompilerGenerated]
			get
			{
				return this.<tags>k__BackingField;
			}
		}

		public ObjectSelectorHandlerWithTagsAttribute(params string[] tags)
		{
			this.<tags>k__BackingField = tags;
		}
	}
}
