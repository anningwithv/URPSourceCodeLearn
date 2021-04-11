using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.SearchService
{
	[AttributeUsage(AttributeTargets.Field)]
	public class ObjectSelectorHandlerWithLabelsAttribute : Attribute
	{
		public string[] labels
		{
			[CompilerGenerated]
			get
			{
				return this.<labels>k__BackingField;
			}
		}

		public bool matchAll
		{
			[CompilerGenerated]
			get
			{
				return this.<matchAll>k__BackingField;
			}
		}

		public ObjectSelectorHandlerWithLabelsAttribute(params string[] labels)
		{
			this.<labels>k__BackingField = labels;
			this.<matchAll>k__BackingField = true;
		}

		public ObjectSelectorHandlerWithLabelsAttribute(bool matchAll, params string[] labels)
		{
			this.<labels>k__BackingField = labels;
			this.<matchAll>k__BackingField = matchAll;
		}
	}
}
