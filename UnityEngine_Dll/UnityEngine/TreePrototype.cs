using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class TreePrototype
	{
		internal GameObject m_Prefab;

		internal float m_BendFactor;

		internal int m_NavMeshLod;

		public GameObject prefab
		{
			get
			{
				return this.m_Prefab;
			}
			set
			{
				this.m_Prefab = value;
			}
		}

		public float bendFactor
		{
			get
			{
				return this.m_BendFactor;
			}
			set
			{
				this.m_BendFactor = value;
			}
		}

		public int navMeshLod
		{
			get
			{
				return this.m_NavMeshLod;
			}
			set
			{
				this.m_NavMeshLod = value;
			}
		}

		public TreePrototype()
		{
		}

		public TreePrototype(TreePrototype other)
		{
			this.prefab = other.prefab;
			this.bendFactor = other.bendFactor;
			this.navMeshLod = other.navMeshLod;
		}

		public override bool Equals(object obj)
		{
			return this.Equals(obj as TreePrototype);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		private bool Equals(TreePrototype other)
		{
			bool flag = other == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = other == this;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = base.GetType() != other.GetType();
					if (flag3)
					{
						result = false;
					}
					else
					{
						bool flag4 = this.prefab == other.prefab && this.bendFactor == other.bendFactor && this.navMeshLod == other.navMeshLod;
						result = flag4;
					}
				}
			}
			return result;
		}
	}
}
