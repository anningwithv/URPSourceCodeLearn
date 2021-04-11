using System;

namespace UnityEngine.UIElements
{
	internal struct InheritedData : IEquatable<InheritedData>
	{
		public StyleColor color;

		public StyleLength fontSize;

		public StyleFont unityFont;

		public StyleEnum<FontStyle> unityFontStyleAndWeight;

		public StyleEnum<TextAnchor> unityTextAlign;

		public StyleEnum<Visibility> visibility;

		public StyleEnum<WhiteSpace> whiteSpace;

		public static bool operator ==(InheritedData lhs, InheritedData rhs)
		{
			return lhs.color == rhs.color && lhs.fontSize == rhs.fontSize && lhs.unityFont == rhs.unityFont && lhs.unityFontStyleAndWeight.value == rhs.unityFontStyleAndWeight.value && lhs.unityFontStyleAndWeight.keyword == rhs.unityFontStyleAndWeight.keyword && lhs.unityTextAlign.value == rhs.unityTextAlign.value && lhs.unityTextAlign.keyword == rhs.unityTextAlign.keyword && lhs.visibility.value == rhs.visibility.value && lhs.visibility.keyword == rhs.visibility.keyword && lhs.whiteSpace.value == rhs.whiteSpace.value && lhs.whiteSpace.keyword == rhs.whiteSpace.keyword;
		}

		public static bool operator !=(InheritedData lhs, InheritedData rhs)
		{
			return !(lhs == rhs);
		}

		public bool Equals(InheritedData other)
		{
			return other == this;
		}

		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is InheritedData && this.Equals((InheritedData)obj);
		}

		public override int GetHashCode()
		{
			int num = this.color.GetHashCode();
			num = (num * 397 ^ this.fontSize.GetHashCode());
			num = (num * 397 ^ this.unityFont.GetHashCode());
			num = (num * 397 ^ this.unityFontStyleAndWeight.GetHashCode());
			num = (num * 397 ^ this.unityTextAlign.GetHashCode());
			num = (num * 397 ^ this.visibility.GetHashCode());
			return num * 397 ^ this.whiteSpace.GetHashCode();
		}
	}
}
