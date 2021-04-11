using System;

namespace UnityEngine.Scripting.APIUpdating
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate)]
	public class MovedFromAttribute : Attribute
	{
		internal MovedFromAttributeData data;

		internal bool AffectsAPIUpdater
		{
			get
			{
				return !this.data.classHasChanged && !this.data.assemblyHasChanged;
			}
		}

		public bool IsInDifferentAssembly
		{
			get
			{
				return this.data.assemblyHasChanged;
			}
		}

		public MovedFromAttribute(bool autoUpdateAPI, string sourceNamespace = null, string sourceAssembly = null, string sourceClassName = null)
		{
			this.data.Set(autoUpdateAPI, sourceNamespace, sourceAssembly, sourceClassName);
		}

		public MovedFromAttribute(string sourceNamespace)
		{
			this.data.Set(true, sourceNamespace, null, null);
		}
	}
}
