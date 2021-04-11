using System;
using UnityEngine.Bindings;

namespace UnityEngine.AI
{
	[NativeHeader("Modules/AI/Public/NavMeshBuildDebugSettings.h")]
	public struct NavMeshBuildDebugSettings
	{
		private byte m_Flags;

		public NavMeshBuildDebugFlags flags
		{
			get
			{
				return (NavMeshBuildDebugFlags)this.m_Flags;
			}
			set
			{
				this.m_Flags = (byte)value;
			}
		}
	}
}
