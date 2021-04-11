using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.AI
{
	public struct NavMeshDataInstance
	{
		public bool valid
		{
			get
			{
				return this.id != 0 && NavMesh.IsValidNavMeshDataHandle(this.id);
			}
		}

		internal int id
		{
			[IsReadOnly]
			get;
			set;
		}

		public UnityEngine.Object owner
		{
			get
			{
				return NavMesh.InternalGetOwner(this.id);
			}
			set
			{
				int ownerID = (value != null) ? value.GetInstanceID() : 0;
				bool flag = !NavMesh.InternalSetOwner(this.id, ownerID);
				if (flag)
				{
					Debug.LogError("Cannot set 'owner' on an invalid NavMeshDataInstance");
				}
			}
		}

		public void Remove()
		{
			NavMesh.RemoveNavMeshDataInternal(this.id);
		}
	}
}
