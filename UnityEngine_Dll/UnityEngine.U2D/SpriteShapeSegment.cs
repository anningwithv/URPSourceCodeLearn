using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D
{
	[MovedFrom("UnityEngine.Experimental.U2D")]
	public struct SpriteShapeSegment
	{
		private int m_GeomIndex;

		private int m_IndexCount;

		private int m_VertexCount;

		private int m_SpriteIndex;

		public int geomIndex
		{
			get
			{
				return this.m_GeomIndex;
			}
			set
			{
				this.m_GeomIndex = value;
			}
		}

		public int indexCount
		{
			get
			{
				return this.m_IndexCount;
			}
			set
			{
				this.m_IndexCount = value;
			}
		}

		public int vertexCount
		{
			get
			{
				return this.m_VertexCount;
			}
			set
			{
				this.m_VertexCount = value;
			}
		}

		public int spriteIndex
		{
			get
			{
				return this.m_SpriteIndex;
			}
			set
			{
				this.m_SpriteIndex = value;
			}
		}
	}
}
