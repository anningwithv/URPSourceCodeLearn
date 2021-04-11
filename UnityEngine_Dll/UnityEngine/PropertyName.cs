using System;
using System.Text;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[UsedByNativeCode]
	public struct PropertyName : IEquatable<PropertyName>
	{
		internal int id;

		internal int conflictIndex;

		public PropertyName(string name)
		{
			this = new PropertyName(PropertyNameUtils.PropertyNameFromString(name));
		}

		public PropertyName(PropertyName other)
		{
			this.id = other.id;
			this.conflictIndex = other.conflictIndex;
		}

		public PropertyName(int id)
		{
			this.id = id;
			this.conflictIndex = 0;
		}

		public static bool IsNullOrEmpty(PropertyName prop)
		{
			return prop.id == 0;
		}

		public static bool operator ==(PropertyName lhs, PropertyName rhs)
		{
			return lhs.id == rhs.id;
		}

		public static bool operator !=(PropertyName lhs, PropertyName rhs)
		{
			return lhs.id != rhs.id;
		}

		public override int GetHashCode()
		{
			return this.id;
		}

		public override bool Equals(object other)
		{
			return other is PropertyName && this.Equals((PropertyName)other);
		}

		public bool Equals(PropertyName other)
		{
			return this == other;
		}

		public static implicit operator PropertyName(string name)
		{
			return new PropertyName(name);
		}

		public static implicit operator PropertyName(int id)
		{
			return new PropertyName(id);
		}

		public override string ToString()
		{
			int num = PropertyNameUtils.ConflictCountForID(this.id);
			string text = string.Format("{0}:{1}", PropertyNameUtils.StringFromPropertyName(this), this.id);
			bool flag = num > 0;
			if (flag)
			{
				StringBuilder stringBuilder = new StringBuilder(text);
				stringBuilder.Append(" conflicts with ");
				for (int i = 0; i < num; i++)
				{
					bool flag2 = i == this.conflictIndex;
					if (!flag2)
					{
						stringBuilder.AppendFormat("\"{0}\"", PropertyNameUtils.StringFromPropertyName(new PropertyName(this.id)
						{
							conflictIndex = i
						}));
					}
				}
				text = stringBuilder.ToString();
			}
			return text;
		}
	}
}
