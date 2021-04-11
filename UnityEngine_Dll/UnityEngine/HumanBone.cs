using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Modules/Animation/HumanDescription.h"), NativeType(CodegenOptions.Custom, "MonoHumanBone"), RequiredByNativeCode]
	public struct HumanBone
	{
		private string m_BoneName;

		private string m_HumanName;

		[NativeName("m_Limit")]
		public HumanLimit limit;

		public string boneName
		{
			get
			{
				return this.m_BoneName;
			}
			set
			{
				this.m_BoneName = value;
			}
		}

		public string humanName
		{
			get
			{
				return this.m_HumanName;
			}
			set
			{
				this.m_HumanName = value;
			}
		}
	}
}
