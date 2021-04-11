using System;

namespace UnityEngine.Rendering
{
	public struct ShaderTagId : IEquatable<ShaderTagId>
	{
		public static readonly ShaderTagId none = default(ShaderTagId);

		private int m_Id;

		internal int id
		{
			get
			{
				return this.m_Id;
			}
			set
			{
				this.m_Id = value;
			}
		}

		public string name
		{
			get
			{
				return Shader.IDToTag(this.id);
			}
		}

		public ShaderTagId(string name)
		{
			this.m_Id = Shader.TagToID(name);
		}

		public override bool Equals(object obj)
		{
			return obj is ShaderTagId && this.Equals((ShaderTagId)obj);
		}

		public bool Equals(ShaderTagId other)
		{
			return this.m_Id == other.m_Id;
		}

		public override int GetHashCode()
		{
			int num = 2079669542;
			return num * -1521134295 + this.m_Id.GetHashCode();
		}

		public static bool operator ==(ShaderTagId tag1, ShaderTagId tag2)
		{
			return tag1.Equals(tag2);
		}

		public static bool operator !=(ShaderTagId tag1, ShaderTagId tag2)
		{
			return !(tag1 == tag2);
		}

		public static explicit operator ShaderTagId(string name)
		{
			return new ShaderTagId(name);
		}

		public static explicit operator string(ShaderTagId tagId)
		{
			return tagId.name;
		}
	}
}
