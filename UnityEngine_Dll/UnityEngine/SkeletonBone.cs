using System;
using System.ComponentModel;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Modules/Animation/HumanDescription.h"), NativeType(CodegenOptions.Custom, "MonoSkeletonBone"), RequiredByNativeCode]
	public struct SkeletonBone
	{
		[NativeName("m_Name")]
		public string name;

		[NativeName("m_ParentName")]
		internal string parentName;

		[NativeName("m_Position")]
		public Vector3 position;

		[NativeName("m_Rotation")]
		public Quaternion rotation;

		[NativeName("m_Scale")]
		public Vector3 scale;

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("transformModified is no longer used and has been deprecated.", true)]
		public int transformModified
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}
	}
}
