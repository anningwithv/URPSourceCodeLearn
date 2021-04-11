using System;

namespace UnityEngine.Bindings
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true), VisibleToOtherModules]
	internal class NativeHeaderAttribute : Attribute, IBindingsHeaderProviderAttribute, IBindingsAttribute
	{
		public string Header
		{
			get;
			set;
		}

		public NativeHeaderAttribute()
		{
		}

		public NativeHeaderAttribute(string header)
		{
			bool flag = header == null;
			if (flag)
			{
				throw new ArgumentNullException("header");
			}
			bool flag2 = header == "";
			if (flag2)
			{
				throw new ArgumentException("header cannot be empty", "header");
			}
			this.Header = header;
		}
	}
}
