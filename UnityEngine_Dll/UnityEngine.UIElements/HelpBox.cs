using System;

namespace UnityEngine.UIElements
{
	public class HelpBox : VisualElement
	{
		public new class UxmlFactory : UxmlFactory<HelpBox, HelpBox.UxmlTraits>
		{
		}

		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};

			private UxmlEnumAttributeDescription<HelpBoxMessageType> m_MessageType = new UxmlEnumAttributeDescription<HelpBoxMessageType>
			{
				name = "message-type",
				defaultValue = HelpBoxMessageType.None
			};

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				HelpBox helpBox = ve as HelpBox;
				helpBox.text = this.m_Text.GetValueFromBag(bag, cc);
				helpBox.messageType = this.m_MessageType.GetValueFromBag(bag, cc);
			}
		}

		public static readonly string ussClassName = "unity-help-box";

		public static readonly string labelUssClassName = HelpBox.ussClassName + "__label";

		public static readonly string iconUssClassName = HelpBox.ussClassName + "__icon";

		public static readonly string iconInfoUssClassName = HelpBox.iconUssClassName + "--info";

		public static readonly string iconwarningUssClassName = HelpBox.iconUssClassName + "--warning";

		public static readonly string iconErrorUssClassName = HelpBox.iconUssClassName + "--error";

		private HelpBoxMessageType m_HelpBoxMessageType;

		private VisualElement m_Icon;

		private string m_IconClass;

		private Label m_Label;

		public string text
		{
			get
			{
				return this.m_Label.text;
			}
			set
			{
				this.m_Label.text = value;
			}
		}

		public HelpBoxMessageType messageType
		{
			get
			{
				return this.m_HelpBoxMessageType;
			}
			set
			{
				bool flag = value != this.m_HelpBoxMessageType;
				if (flag)
				{
					this.m_HelpBoxMessageType = value;
					this.UpdateIcon(value);
				}
			}
		}

		public HelpBox() : this(string.Empty, HelpBoxMessageType.None)
		{
		}

		public HelpBox(string text, HelpBoxMessageType messageType)
		{
			base.AddToClassList(HelpBox.ussClassName);
			this.m_HelpBoxMessageType = messageType;
			this.m_Label = new Label(text);
			this.m_Label.AddToClassList(HelpBox.labelUssClassName);
			base.Add(this.m_Label);
			this.m_Icon = new VisualElement();
			this.m_Icon.AddToClassList(HelpBox.iconUssClassName);
			this.UpdateIcon(messageType);
		}

		private string GetIconClass(HelpBoxMessageType messageType)
		{
			string result;
			switch (messageType)
			{
			case HelpBoxMessageType.Info:
				result = HelpBox.iconInfoUssClassName;
				break;
			case HelpBoxMessageType.Warning:
				result = HelpBox.iconwarningUssClassName;
				break;
			case HelpBoxMessageType.Error:
				result = HelpBox.iconErrorUssClassName;
				break;
			default:
				result = null;
				break;
			}
			return result;
		}

		private void UpdateIcon(HelpBoxMessageType messageType)
		{
			bool flag = !string.IsNullOrEmpty(this.m_IconClass);
			if (flag)
			{
				this.m_Icon.RemoveFromClassList(this.m_IconClass);
			}
			this.m_IconClass = this.GetIconClass(messageType);
			bool flag2 = this.m_IconClass == null;
			if (flag2)
			{
				this.m_Icon.RemoveFromHierarchy();
			}
			else
			{
				this.m_Icon.AddToClassList(this.m_IconClass);
				bool flag3 = this.m_Icon.parent == null;
				if (flag3)
				{
					base.Insert(0, this.m_Icon);
				}
			}
		}
	}
}
