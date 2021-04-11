using System;

namespace UnityEngine.Bindings
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Property), VisibleToOtherModules]
	internal class NativeConditionalAttribute : Attribute, IBindingsAttribute
	{
		public string Condition
		{
			get;
			set;
		}

		public string StubReturnStatement
		{
			get;
			set;
		}

		public bool Enabled
		{
			get;
			set;
		}

		public NativeConditionalAttribute()
		{
		}

		public NativeConditionalAttribute(string condition)
		{
			this.Condition = condition;
			this.Enabled = true;
		}

		public NativeConditionalAttribute(bool enabled)
		{
			this.Enabled = enabled;
		}

		public NativeConditionalAttribute(string condition, bool enabled) : this(condition)
		{
			this.Enabled = enabled;
		}

		public NativeConditionalAttribute(string condition, string stubReturnStatement, bool enabled) : this(condition, stubReturnStatement)
		{
			this.Enabled = enabled;
		}

		public NativeConditionalAttribute(string condition, string stubReturnStatement) : this(condition)
		{
			this.StubReturnStatement = stubReturnStatement;
		}
	}
}
