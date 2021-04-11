using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false), VisibleToOtherModules]
	internal sealed class AssetFileNameExtensionAttribute : Attribute
	{
		public string preferredExtension
		{
			[CompilerGenerated]
			get
			{
				return this.<preferredExtension>k__BackingField;
			}
		}

		public IEnumerable<string> otherExtensions
		{
			[CompilerGenerated]
			get
			{
				return this.<otherExtensions>k__BackingField;
			}
		}

		public AssetFileNameExtensionAttribute(string preferredExtension, params string[] otherExtensions)
		{
			this.<preferredExtension>k__BackingField = preferredExtension;
			this.<otherExtensions>k__BackingField = otherExtensions;
		}
	}
}
