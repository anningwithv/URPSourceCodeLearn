using System;
using System.Collections.Generic;
using System.Text;

namespace UnityEngine.UIElements.StyleSheets
{
	internal class StylePropertyValueParser
	{
		private string m_PropertyValue;

		private List<string> m_ValueList = new List<string>();

		private StringBuilder m_StringBuilder = new StringBuilder();

		private int m_ParseIndex = 0;

		public string[] Parse(string propertyValue)
		{
			this.m_PropertyValue = propertyValue;
			this.m_ValueList.Clear();
			this.m_StringBuilder.Remove(0, this.m_StringBuilder.Length);
			this.m_ParseIndex = 0;
			while (this.m_ParseIndex < this.m_PropertyValue.Length)
			{
				char c = this.m_PropertyValue[this.m_ParseIndex];
				char c2 = c;
				char c3 = c2;
				if (c3 != ' ')
				{
					if (c3 != '(')
					{
						if (c3 != ',')
						{
							this.m_StringBuilder.Append(c);
						}
						else
						{
							this.EatSpace();
							this.AddValuePart();
							this.m_ValueList.Add(",");
						}
					}
					else
					{
						this.AppendFunction();
					}
				}
				else
				{
					this.EatSpace();
					this.AddValuePart();
				}
				this.m_ParseIndex++;
			}
			string text = this.m_StringBuilder.ToString();
			bool flag = !string.IsNullOrEmpty(text);
			if (flag)
			{
				this.m_ValueList.Add(text);
			}
			return this.m_ValueList.ToArray();
		}

		private void AddValuePart()
		{
			string item = this.m_StringBuilder.ToString();
			this.m_StringBuilder.Remove(0, this.m_StringBuilder.Length);
			this.m_ValueList.Add(item);
		}

		private void AppendFunction()
		{
			while (this.m_ParseIndex < this.m_PropertyValue.Length && this.m_PropertyValue[this.m_ParseIndex] != ')')
			{
				this.m_StringBuilder.Append(this.m_PropertyValue[this.m_ParseIndex]);
				this.m_ParseIndex++;
			}
			this.m_StringBuilder.Append(this.m_PropertyValue[this.m_ParseIndex]);
		}

		private void EatSpace()
		{
			while (this.m_ParseIndex + 1 < this.m_PropertyValue.Length && this.m_PropertyValue[this.m_ParseIndex + 1] == ' ')
			{
				this.m_ParseIndex++;
			}
		}
	}
}
