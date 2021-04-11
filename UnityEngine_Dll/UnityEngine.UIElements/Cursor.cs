using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	public struct Cursor : IEquatable<Cursor>
	{
		public Texture2D texture
		{
			[IsReadOnly]
			get;
			set;
		}

		public Vector2 hotspot
		{
			[IsReadOnly]
			get;
			set;
		}

		internal int defaultCursorId
		{
			[IsReadOnly]
			get;
			set;
		}

		public override bool Equals(object obj)
		{
			return obj is Cursor && this.Equals((Cursor)obj);
		}

		public bool Equals(Cursor other)
		{
			return EqualityComparer<Texture2D>.Default.Equals(this.texture, other.texture) && this.hotspot.Equals(other.hotspot) && this.defaultCursorId == other.defaultCursorId;
		}

		public override int GetHashCode()
		{
			int num = 1500536833;
			num = num * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(this.texture);
			num = num * -1521134295 + EqualityComparer<Vector2>.Default.GetHashCode(this.hotspot);
			return num * -1521134295 + this.defaultCursorId.GetHashCode();
		}

		public static bool operator ==(Cursor style1, Cursor style2)
		{
			return style1.Equals(style2);
		}

		public static bool operator !=(Cursor style1, Cursor style2)
		{
			return !(style1 == style2);
		}

		public override string ToString()
		{
			return string.Format("texture={0}, hotspot={1}", this.texture, this.hotspot);
		}
	}
}
