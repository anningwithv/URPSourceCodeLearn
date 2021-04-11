using System;

namespace UnityEngine.TextCore
{
	internal struct RichTextTagAttribute
	{
		public int nameHashCode;

		public int valueHashCode;

		public TagValueType valueType;

		public int valueStartIndex;

		public int valueLength;
	}
}
