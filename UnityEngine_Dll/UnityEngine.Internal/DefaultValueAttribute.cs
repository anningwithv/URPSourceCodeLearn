using System;

namespace UnityEngine.Internal
{
	[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.GenericParameter)]
	[Serializable]
	public class DefaultValueAttribute : Attribute
	{
		private object DefaultValue;

		public object Value
		{
			get
			{
				return this.DefaultValue;
			}
		}

		public DefaultValueAttribute(string value)
		{
			this.DefaultValue = value;
		}

		public override bool Equals(object obj)
		{
			DefaultValueAttribute defaultValueAttribute = obj as DefaultValueAttribute;
			bool flag = defaultValueAttribute == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.DefaultValue == null;
				if (flag2)
				{
					result = (defaultValueAttribute.Value == null);
				}
				else
				{
					result = this.DefaultValue.Equals(defaultValueAttribute.Value);
				}
			}
			return result;
		}

		public override int GetHashCode()
		{
			bool flag = this.DefaultValue == null;
			int hashCode;
			if (flag)
			{
				hashCode = base.GetHashCode();
			}
			else
			{
				hashCode = this.DefaultValue.GetHashCode();
			}
			return hashCode;
		}
	}
}
