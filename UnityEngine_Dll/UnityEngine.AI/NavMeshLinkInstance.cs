using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.AI
{
	public struct NavMeshLinkInstance
	{
		public bool valid
		{
			get
			{
				return this.id != 0 && NavMesh.IsValidLinkHandle(this.id);
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
				return NavMesh.InternalGetLinkOwner(this.id);
			}
			set
			{
				int ownerID = (value != null) ? value.GetInstanceID() : 0;
				bool flag = !NavMesh.InternalSetLinkOwner(this.id, ownerID);
				if (flag)
				{
					Debug.LogError("Cannot set 'owner' on an invalid NavMeshLinkInstance");
				}
			}
		}

		public void Remove()
		{
			NavMesh.RemoveLinkInternal(this.id);
		}
	}
}
