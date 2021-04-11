using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.AI
{
	[NativeHeader("Modules/AI/Public/NavMeshBindingTypes.h")]
	public struct NavMeshBuildMarkup
	{
		private int m_OverrideArea;

		private int m_Area;

		private int m_IgnoreFromBuild;

		private int m_InstanceID;

		public bool overrideArea
		{
			get
			{
				return this.m_OverrideArea != 0;
			}
			set
			{
				this.m_OverrideArea = (value ? 1 : 0);
			}
		}

		public int area
		{
			get
			{
				return this.m_Area;
			}
			set
			{
				this.m_Area = value;
			}
		}

		public bool ignoreFromBuild
		{
			get
			{
				return this.m_IgnoreFromBuild != 0;
			}
			set
			{
				this.m_IgnoreFromBuild = (value ? 1 : 0);
			}
		}

		public Transform root
		{
			get
			{
				return NavMeshBuildMarkup.InternalGetRootGO(this.m_InstanceID);
			}
			set
			{
				this.m_InstanceID = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		[StaticAccessor("NavMeshBuildMarkup", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Transform InternalGetRootGO(int instanceID);
	}
}
