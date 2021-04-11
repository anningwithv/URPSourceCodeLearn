using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	public struct SubMeshDescriptor
	{
		public Bounds bounds
		{
			[IsReadOnly]
			get;
			set;
		}

		public MeshTopology topology
		{
			[IsReadOnly]
			get;
			set;
		}

		public int indexStart
		{
			[IsReadOnly]
			get;
			set;
		}

		public int indexCount
		{
			[IsReadOnly]
			get;
			set;
		}

		public int baseVertex
		{
			[IsReadOnly]
			get;
			set;
		}

		public int firstVertex
		{
			[IsReadOnly]
			get;
			set;
		}

		public int vertexCount
		{
			[IsReadOnly]
			get;
			set;
		}

		public SubMeshDescriptor(int indexStart, int indexCount, MeshTopology topology = MeshTopology.Triangles)
		{
			this.indexStart = indexStart;
			this.indexCount = indexCount;
			this.topology = topology;
			this.bounds = default(Bounds);
			this.baseVertex = 0;
			this.firstVertex = 0;
			this.vertexCount = 0;
		}

		public override string ToString()
		{
			return string.Format("(topo={0} indices={1},{2} vertices={3},{4} basevtx={5} bounds={6})", new object[]
			{
				this.topology,
				this.indexStart,
				this.indexCount,
				this.firstVertex,
				this.vertexCount,
				this.baseVertex,
				this.bounds
			});
		}
	}
}
