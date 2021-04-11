using System;

namespace UnityEngine.Bindings
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum), VisibleToOtherModules]
	internal class NativeTypeAttribute : Attribute, IBindingsHeaderProviderAttribute, IBindingsAttribute, IBindingsGenerateMarshallingTypeAttribute
	{
		public string Header
		{
			get;
			set;
		}

		public string IntermediateScriptingStructName
		{
			get;
			set;
		}

		public CodegenOptions CodegenOptions
		{
			get;
			set;
		}

		public NativeTypeAttribute()
		{
			this.CodegenOptions = CodegenOptions.Auto;
		}

		public NativeTypeAttribute(CodegenOptions codegenOptions)
		{
			this.CodegenOptions = codegenOptions;
		}

		public NativeTypeAttribute(string header)
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
			this.CodegenOptions = CodegenOptions.Auto;
			this.Header = header;
		}

		public NativeTypeAttribute(string header, CodegenOptions codegenOptions) : this(header)
		{
			this.CodegenOptions = codegenOptions;
		}

		public NativeTypeAttribute(CodegenOptions codegenOptions, string intermediateStructName) : this(codegenOptions)
		{
			this.IntermediateScriptingStructName = intermediateStructName;
		}
	}
}
