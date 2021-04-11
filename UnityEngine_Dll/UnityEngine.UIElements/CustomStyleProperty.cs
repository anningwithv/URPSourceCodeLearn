using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	public struct CustomStyleProperty<T> : IEquatable<CustomStyleProperty<T>>
	{
		public string name
		{
			[IsReadOnly]
			get;
			private set;
		}

		public CustomStyleProperty(string propertyName)
		{
			bool flag = !string.IsNullOrEmpty(propertyName) && !propertyName.StartsWith("--");
			if (flag)
			{
				throw new ArgumentException("Custom style property \"" + propertyName + "\" must start with \"--\" prefix.");
			}
			this.name = propertyName;
		}

		public override bool Equals(object obj)
		{
			bool flag = !(obj is CustomStyleProperty<T>);
			return !flag && this.Equals((CustomStyleProperty<T>)obj);
		}

		public bool Equals(CustomStyleProperty<T> other)
		{
			return this.name == other.name;
		}

		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		public static bool operator ==(CustomStyleProperty<T> a, CustomStyleProperty<T> b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(CustomStyleProperty<T> a, CustomStyleProperty<T> b)
		{
			return !(a == b);
		}
	}
}
