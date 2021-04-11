using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	[UsedByNativeCode]
	public struct VertexAttributeDescriptor : IEquatable<VertexAttributeDescriptor>
	{
		public VertexAttribute attribute
		{
			[IsReadOnly]
			get;
			set;
		}

		public VertexAttributeFormat format
		{
			[IsReadOnly]
			get;
			set;
		}

		public int dimension
		{
			[IsReadOnly]
			get;
			set;
		}

		public int stream
		{
			[IsReadOnly]
			get;
			set;
		}

		public VertexAttributeDescriptor(VertexAttribute attribute = VertexAttribute.Position, VertexAttributeFormat format = VertexAttributeFormat.Float32, int dimension = 3, int stream = 0)
		{
			this.attribute = attribute;
			this.format = format;
			this.dimension = dimension;
			this.stream = stream;
		}

		public override string ToString()
		{
			return string.Format("(attr={0} fmt={1} dim={2} stream={3})", new object[]
			{
				this.attribute,
				this.format,
				this.dimension,
				this.stream
			});
		}

		public override int GetHashCode()
		{
			int num = 17;
			num = (int)(num * 23 + this.attribute);
			num = (int)(num * 23 + this.format);
			num = num * 23 + this.dimension;
			return num * 23 + this.stream;
		}

		public override bool Equals(object other)
		{
			bool flag = !(other is VertexAttributeDescriptor);
			return !flag && this.Equals((VertexAttributeDescriptor)other);
		}

		public bool Equals(VertexAttributeDescriptor other)
		{
			return this.attribute == other.attribute && this.format == other.format && this.dimension == other.dimension && this.stream == other.stream;
		}

		public static bool operator ==(VertexAttributeDescriptor lhs, VertexAttributeDescriptor rhs)
		{
			return lhs.Equals(rhs);
		}

		public static bool operator !=(VertexAttributeDescriptor lhs, VertexAttributeDescriptor rhs)
		{
			return !lhs.Equals(rhs);
		}
	}
}
