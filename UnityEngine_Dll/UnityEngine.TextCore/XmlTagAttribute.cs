using System;

namespace UnityEngine.TextCore
{
	internal struct XmlTagAttribute
	{
		public int nameHashCode;

		public TagValueType valueType;

		public int valueStartIndex;

		public int valueLength;

		public int valueHashCode;
	}
}
