using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	[Serializable]
	internal class VisualElementAsset : IUxmlAttributes, ISerializationCallbackReceiver
	{
		[SerializeField]
		private string m_Name;

		[SerializeField]
		private int m_Id;

		[SerializeField]
		private int m_OrderInDocument;

		[SerializeField]
		private int m_ParentId;

		[SerializeField]
		private int m_RuleIndex;

		[SerializeField]
		private string m_Text;

		[SerializeField]
		private PickingMode m_PickingMode;

		[SerializeField]
		private string m_FullTypeName;

		[SerializeField]
		private string[] m_Classes;

		[SerializeField]
		private List<string> m_StylesheetPaths;

		[SerializeField]
		private List<StyleSheet> m_Stylesheets;

		[SerializeField]
		private List<string> m_Properties;

		public int id
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

		public int orderInDocument
		{
			get
			{
				return this.m_OrderInDocument;
			}
			set
			{
				this.m_OrderInDocument = value;
			}
		}

		public int parentId
		{
			get
			{
				return this.m_ParentId;
			}
			set
			{
				this.m_ParentId = value;
			}
		}

		public int ruleIndex
		{
			get
			{
				return this.m_RuleIndex;
			}
			set
			{
				this.m_RuleIndex = value;
			}
		}

		public string fullTypeName
		{
			get
			{
				return this.m_FullTypeName;
			}
			set
			{
				this.m_FullTypeName = value;
			}
		}

		public string[] classes
		{
			get
			{
				return this.m_Classes;
			}
			set
			{
				this.m_Classes = value;
			}
		}

		public List<string> stylesheetPaths
		{
			get
			{
				List<string> arg_19_0;
				if ((arg_19_0 = this.m_StylesheetPaths) == null)
				{
					arg_19_0 = (this.m_StylesheetPaths = new List<string>());
				}
				return arg_19_0;
			}
			set
			{
				this.m_StylesheetPaths = value;
			}
		}

		public bool hasStylesheetPaths
		{
			get
			{
				return this.m_StylesheetPaths != null;
			}
		}

		public List<StyleSheet> stylesheets
		{
			get
			{
				List<StyleSheet> arg_19_0;
				if ((arg_19_0 = this.m_Stylesheets) == null)
				{
					arg_19_0 = (this.m_Stylesheets = new List<StyleSheet>());
				}
				return arg_19_0;
			}
			set
			{
				this.m_Stylesheets = value;
			}
		}

		public bool hasStylesheets
		{
			get
			{
				return this.m_Stylesheets != null;
			}
		}

		public VisualElementAsset(string fullTypeName)
		{
			this.m_FullTypeName = fullTypeName;
			this.m_Name = string.Empty;
			this.m_Text = string.Empty;
			this.m_PickingMode = PickingMode.Position;
		}

		public void OnBeforeSerialize()
		{
		}

		public void OnAfterDeserialize()
		{
			bool flag = !string.IsNullOrEmpty(this.m_Name) && !this.m_Properties.Contains("name");
			if (flag)
			{
				this.AddProperty("name", this.m_Name);
			}
			bool flag2 = !string.IsNullOrEmpty(this.m_Text) && !this.m_Properties.Contains("text");
			if (flag2)
			{
				this.AddProperty("text", this.m_Text);
			}
			bool flag3 = this.m_PickingMode != PickingMode.Position && !this.m_Properties.Contains("picking-mode") && !this.m_Properties.Contains("pickingMode");
			if (flag3)
			{
				this.AddProperty("picking-mode", this.m_PickingMode.ToString());
			}
		}

		public void AddProperty(string propertyName, string propertyValue)
		{
			this.SetOrAddProperty(propertyName, propertyValue);
		}

		private void SetOrAddProperty(string propertyName, string propertyValue)
		{
			bool flag = this.m_Properties == null;
			if (flag)
			{
				this.m_Properties = new List<string>();
			}
			for (int i = 0; i < this.m_Properties.Count - 1; i += 2)
			{
				bool flag2 = this.m_Properties[i] == propertyName;
				if (flag2)
				{
					this.m_Properties[i + 1] = propertyValue;
					return;
				}
			}
			this.m_Properties.Add(propertyName);
			this.m_Properties.Add(propertyValue);
		}

		public bool TryGetAttributeValue(string propertyName, out string value)
		{
			bool flag = this.m_Properties == null;
			bool result;
			if (flag)
			{
				value = null;
				result = false;
			}
			else
			{
				for (int i = 0; i < this.m_Properties.Count - 1; i += 2)
				{
					bool flag2 = this.m_Properties[i] == propertyName;
					if (flag2)
					{
						value = this.m_Properties[i + 1];
						result = true;
						return result;
					}
				}
				value = null;
				result = false;
			}
			return result;
		}
	}
}
