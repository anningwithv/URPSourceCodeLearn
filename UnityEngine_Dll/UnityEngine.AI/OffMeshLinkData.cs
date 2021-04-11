using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.AI
{
	[NativeHeader("Modules/AI/Components/OffMeshLink.bindings.h"), MovedFrom("UnityEngine")]
	public struct OffMeshLinkData
	{
		internal int m_Valid;

		internal int m_Activated;

		internal int m_InstanceID;

		internal OffMeshLinkType m_LinkType;

		internal Vector3 m_StartPos;

		internal Vector3 m_EndPos;

		public bool valid
		{
			get
			{
				return this.m_Valid != 0;
			}
		}

		public bool activated
		{
			get
			{
				return this.m_Activated != 0;
			}
		}

		public OffMeshLinkType linkType
		{
			get
			{
				return this.m_LinkType;
			}
		}

		public Vector3 startPos
		{
			get
			{
				return this.m_StartPos;
			}
		}

		public Vector3 endPos
		{
			get
			{
				return this.m_EndPos;
			}
		}

		public OffMeshLink offMeshLink
		{
			get
			{
				return OffMeshLinkData.GetOffMeshLinkInternal(this.m_InstanceID);
			}
		}

		[FreeFunction("OffMeshLinkScriptBindings::GetOffMeshLinkInternal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern OffMeshLink GetOffMeshLinkInternal(int instanceID);
	}
}
