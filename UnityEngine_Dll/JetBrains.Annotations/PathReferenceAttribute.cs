using System;
using System.Runtime.CompilerServices;

namespace JetBrains.Annotations
{
	[AttributeUsage(AttributeTargets.Parameter)]
	public sealed class PathReferenceAttribute : Attribute
	{
		[CanBeNull]
		public string BasePath
		{
			[CompilerGenerated]
			get
			{
				return this.<BasePath>k__BackingField;
			}
		}

		public PathReferenceAttribute()
		{
		}

		public PathReferenceAttribute([NotNull, PathReference] string basePath)
		{
			this.<BasePath>k__BackingField = basePath;
		}
	}
}
