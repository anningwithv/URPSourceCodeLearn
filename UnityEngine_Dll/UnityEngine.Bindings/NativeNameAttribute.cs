using System;

namespace UnityEngine.Bindings
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field), VisibleToOtherModules]
	internal class NativeNameAttribute : Attribute, IBindingsNameProviderAttribute, IBindingsAttribute
	{
		public string Name
		{
			get;
			set;
		}

		public NativeNameAttribute()
		{
		}

		public NativeNameAttribute(string name)
		{
			bool flag = name == null;
			if (flag)
			{
				throw new ArgumentNullException("name");
			}
			bool flag2 = name == "";
			if (flag2)
			{
				throw new ArgumentException("name cannot be empty", "name");
			}
			this.Name = name;
		}
	}
}
