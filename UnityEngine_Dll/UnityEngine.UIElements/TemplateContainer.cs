using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	public class TemplateContainer : BindableElement
	{
		public new class UxmlFactory : UxmlFactory<TemplateContainer, TemplateContainer.UxmlTraits>
		{
			internal const string k_ElementName = "Instance";

			public override string uxmlName
			{
				get
				{
					return "Instance";
				}
			}

			public override string uxmlQualifiedName
			{
				get
				{
					return this.uxmlNamespace + "." + this.uxmlName;
				}
			}
		}

		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			internal const string k_TemplateAttributeName = "template";

			private UxmlStringAttributeDescription m_Template = new UxmlStringAttributeDescription
			{
				name = "template",
				use = UxmlAttributeDescription.Use.Required
			};

			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				TemplateContainer templateContainer = (TemplateContainer)ve;
				templateContainer.templateId = this.m_Template.GetValueFromBag(bag, cc);
				VisualTreeAsset expr_2D = cc.visualTreeAsset;
				VisualTreeAsset visualTreeAsset = (expr_2D != null) ? expr_2D.ResolveTemplate(templateContainer.templateId) : null;
				bool flag = visualTreeAsset == null;
				if (flag)
				{
					templateContainer.Add(new Label(string.Format("Unknown Template: '{0}'", templateContainer.templateId)));
				}
				else
				{
					TemplateAsset expr_70 = bag as TemplateAsset;
					List<TemplateAsset.AttributeOverride> list = (expr_70 != null) ? expr_70.attributeOverrides : null;
					List<TemplateAsset.AttributeOverride> attributeOverrides = cc.attributeOverrides;
					List<TemplateAsset.AttributeOverride> list2 = null;
					bool flag2 = list != null || attributeOverrides != null;
					if (flag2)
					{
						list2 = new List<TemplateAsset.AttributeOverride>();
						bool flag3 = attributeOverrides != null;
						if (flag3)
						{
							list2.AddRange(attributeOverrides);
						}
						bool flag4 = list != null;
						if (flag4)
						{
							list2.AddRange(list);
						}
					}
					visualTreeAsset.CloneTree(ve, cc.slotInsertionPoints, list2);
				}
				bool flag5 = visualTreeAsset == null;
				if (flag5)
				{
					Debug.LogErrorFormat("Could not resolve template with name '{0}'", new object[]
					{
						templateContainer.templateId
					});
				}
			}
		}

		private VisualElement m_ContentContainer;

		public string templateId
		{
			get;
			private set;
		}

		public override VisualElement contentContainer
		{
			get
			{
				return this.m_ContentContainer;
			}
		}

		public TemplateContainer() : this(null)
		{
		}

		public TemplateContainer(string templateId)
		{
			this.templateId = templateId;
			this.m_ContentContainer = this;
		}

		internal void SetContentContainer(VisualElement content)
		{
			this.m_ContentContainer = content;
		}
	}
}
