using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	[Serializable]
	internal class TemplateAsset : VisualElementAsset
	{
		[Serializable]
		public struct AttributeOverride
		{
			public string m_ElementName;

			public string m_AttributeName;

			public string m_Value;
		}

		[SerializeField]
		private string m_TemplateAlias;

		[SerializeField]
		private List<TemplateAsset.AttributeOverride> m_AttributeOverrides;

		[SerializeField]
		private List<VisualTreeAsset.SlotUsageEntry> m_SlotUsages;

		public string templateAlias
		{
			get
			{
				return this.m_TemplateAlias;
			}
			set
			{
				this.m_TemplateAlias = value;
			}
		}

		public List<TemplateAsset.AttributeOverride> attributeOverrides
		{
			get
			{
				return (this.m_AttributeOverrides == null) ? (this.m_AttributeOverrides = new List<TemplateAsset.AttributeOverride>()) : this.m_AttributeOverrides;
			}
			set
			{
				this.m_AttributeOverrides = value;
			}
		}

		internal List<VisualTreeAsset.SlotUsageEntry> slotUsages
		{
			get
			{
				return this.m_SlotUsages;
			}
			set
			{
				this.m_SlotUsages = value;
			}
		}

		public TemplateAsset(string templateAlias, string fullTypeName) : base(fullTypeName)
		{
			Assert.IsFalse(string.IsNullOrEmpty(templateAlias), "Template alias must not be null or empty");
			this.m_TemplateAlias = templateAlias;
		}

		public void AddSlotUsage(string slotName, int resId)
		{
			bool flag = this.m_SlotUsages == null;
			if (flag)
			{
				this.m_SlotUsages = new List<VisualTreeAsset.SlotUsageEntry>();
			}
			this.m_SlotUsages.Add(new VisualTreeAsset.SlotUsageEntry(slotName, resId));
		}
	}
}
