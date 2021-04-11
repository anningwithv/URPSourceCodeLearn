using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.AI
{
	[NativeHeader("Modules/AI/Public/NavMeshBindingTypes.h"), UsedByNativeCode]
	public struct NavMeshBuildSource
	{
		private Matrix4x4 m_Transform;

		private Vector3 m_Size;

		private NavMeshBuildSourceShape m_Shape;

		private int m_Area;

		private int m_InstanceID;

		private int m_ComponentID;

		public Matrix4x4 transform
		{
			get
			{
				return this.m_Transform;
			}
			set
			{
				this.m_Transform = value;
			}
		}

		public Vector3 size
		{
			get
			{
				return this.m_Size;
			}
			set
			{
				this.m_Size = value;
			}
		}

		public NavMeshBuildSourceShape shape
		{
			get
			{
				return this.m_Shape;
			}
			set
			{
				this.m_Shape = value;
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

		public UnityEngine.Object sourceObject
		{
			get
			{
				return NavMeshBuildSource.InternalGetObject(this.m_InstanceID);
			}
			set
			{
				this.m_InstanceID = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		public Component component
		{
			get
			{
				return NavMeshBuildSource.InternalGetComponent(this.m_ComponentID);
			}
			set
			{
				this.m_ComponentID = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		[StaticAccessor("NavMeshBuildSource", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Component InternalGetComponent(int instanceID);

		[StaticAccessor("NavMeshBuildSource", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern UnityEngine.Object InternalGetObject(int instanceID);
	}
}
