using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.UIElements
{
	public abstract class UxmlAttributeDescription
	{
		public enum Use
		{
			None,
			Optional,
			Prohibited,
			Required
		}

		protected const string xmlSchemaNamespace = "http://www.w3.org/2001/XMLSchema";

		private string[] m_ObsoleteNames;

		public string name
		{
			get;
			set;
		}

		public IEnumerable<string> obsoleteNames
		{
			get
			{
				return this.m_ObsoleteNames;
			}
			set
			{
				this.m_ObsoleteNames = value.ToArray<string>();
			}
		}

		public string type
		{
			get;
			protected set;
		}

		public string typeNamespace
		{
			get;
			protected set;
		}

		public abstract string defaultValueAsString
		{
			get;
		}

		public UxmlAttributeDescription.Use use
		{
			get;
			set;
		}

		public UxmlTypeRestriction restriction
		{
			get;
			set;
		}

		protected UxmlAttributeDescription()
		{
			this.use = UxmlAttributeDescription.Use.Optional;
			this.restriction = null;
		}

		internal bool TryGetValueFromBagAsString(IUxmlAttributes bag, CreationContext cc, out string value)
		{
			bool flag = this.name == null && (this.m_ObsoleteNames == null || this.m_ObsoleteNames.Length == 0);
			bool result;
			if (flag)
			{
				Debug.LogError("Attribute description has no name.");
				value = null;
				result = false;
			}
			else
			{
				string text;
				bag.TryGetAttributeValue("name", out text);
				bool flag2 = !string.IsNullOrEmpty(text) && cc.attributeOverrides != null;
				if (flag2)
				{
					for (int i = 0; i < cc.attributeOverrides.Count; i++)
					{
						bool flag3 = cc.attributeOverrides[i].m_ElementName != text;
						if (!flag3)
						{
							bool flag4 = cc.attributeOverrides[i].m_AttributeName != this.name;
							if (flag4)
							{
								bool flag5 = this.m_ObsoleteNames != null;
								if (!flag5)
								{
									goto IL_147;
								}
								bool flag6 = false;
								for (int j = 0; j < this.m_ObsoleteNames.Length; j++)
								{
									bool flag7 = cc.attributeOverrides[i].m_AttributeName == this.m_ObsoleteNames[j];
									if (flag7)
									{
										flag6 = true;
										break;
									}
								}
								bool flag8 = !flag6;
								if (flag8)
								{
									goto IL_147;
								}
							}
							value = cc.attributeOverrides[i].m_Value;
							result = true;
							return result;
						}
						IL_147:;
					}
				}
				bool flag9 = this.name == null;
				if (flag9)
				{
					for (int k = 0; k < this.m_ObsoleteNames.Length; k++)
					{
						bool flag10 = bag.TryGetAttributeValue(this.m_ObsoleteNames[k], out value);
						if (flag10)
						{
							bool flag11 = cc.visualTreeAsset != null;
							if (flag11)
							{
							}
							result = true;
							return result;
						}
					}
					value = null;
					result = false;
				}
				else
				{
					bool flag12 = !bag.TryGetAttributeValue(this.name, out value);
					if (flag12)
					{
						bool flag13 = this.m_ObsoleteNames != null;
						if (flag13)
						{
							for (int l = 0; l < this.m_ObsoleteNames.Length; l++)
							{
								bool flag14 = bag.TryGetAttributeValue(this.m_ObsoleteNames[l], out value);
								if (flag14)
								{
									bool flag15 = cc.visualTreeAsset != null;
									if (flag15)
									{
									}
									result = true;
									return result;
								}
							}
						}
						value = null;
						result = false;
					}
					else
					{
						result = true;
					}
				}
			}
			return result;
		}

		protected bool TryGetValueFromBag<T>(IUxmlAttributes bag, CreationContext cc, Func<string, T, T> converterFunc, T defaultValue, ref T value)
		{
			string arg;
			bool flag = this.TryGetValueFromBagAsString(bag, cc, out arg);
			bool result;
			if (flag)
			{
				bool flag2 = converterFunc != null;
				if (flag2)
				{
					value = converterFunc(arg, defaultValue);
				}
				else
				{
					value = defaultValue;
				}
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		protected T GetValueFromBag<T>(IUxmlAttributes bag, CreationContext cc, Func<string, T, T> converterFunc, T defaultValue)
		{
			bool flag = converterFunc == null;
			if (flag)
			{
				throw new ArgumentNullException("converterFunc");
			}
			string arg;
			bool flag2 = this.TryGetValueFromBagAsString(bag, cc, out arg);
			T result;
			if (flag2)
			{
				result = converterFunc(arg, defaultValue);
			}
			else
			{
				result = defaultValue;
			}
			return result;
		}
	}
}
