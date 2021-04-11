using System;

namespace UnityEngine.UIElements
{
	public class Foldout : BindableElement, INotifyValueChanged<bool>
	{
		public new class UxmlFactory : UxmlFactory<Foldout, Foldout.UxmlTraits>
		{
		}

		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};

			private UxmlBoolAttributeDescription m_Value = new UxmlBoolAttributeDescription
			{
				name = "value",
				defaultValue = true
			};

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				Foldout foldout = ve as Foldout;
				bool flag = foldout != null;
				if (flag)
				{
					foldout.text = this.m_Text.GetValueFromBag(bag, cc);
					foldout.SetValueWithoutNotify(this.m_Value.GetValueFromBag(bag, cc));
				}
			}
		}

		internal static readonly string ussFoldoutDepthClassName = "unity-foldout--depth-";

		internal static readonly int ussFoldoutMaxDepth = 4;

		private Toggle m_Toggle;

		private VisualElement m_Container;

		[SerializeField]
		private bool m_Value;

		public static readonly string ussClassName = "unity-foldout";

		public static readonly string toggleUssClassName = Foldout.ussClassName + "__toggle";

		public static readonly string contentUssClassName = Foldout.ussClassName + "__content";

		public override VisualElement contentContainer
		{
			get
			{
				return this.m_Container;
			}
		}

		public string text
		{
			get
			{
				return this.m_Toggle.text;
			}
			set
			{
				this.m_Toggle.text = value;
			}
		}

		public bool value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				bool flag = this.m_Value == value;
				if (!flag)
				{
					using (ChangeEvent<bool> pooled = ChangeEvent<bool>.GetPooled(this.m_Value, value))
					{
						pooled.target = this;
						this.SetValueWithoutNotify(value);
						this.SendEvent(pooled);
						base.SaveViewData();
					}
				}
			}
		}

		public void SetValueWithoutNotify(bool newValue)
		{
			this.m_Value = newValue;
			this.m_Toggle.value = this.m_Value;
			this.contentContainer.style.display = (newValue ? DisplayStyle.Flex : DisplayStyle.None);
		}

		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
			base.OverwriteFromViewData(this, fullHierarchicalViewDataKey);
			this.SetValueWithoutNotify(this.m_Value);
		}

		public Foldout()
		{
			this.m_Value = true;
			base.AddToClassList(Foldout.ussClassName);
			this.m_Toggle = new Toggle
			{
				value = true
			};
			this.m_Toggle.RegisterValueChangedCallback(delegate(ChangeEvent<bool> evt)
			{
				this.value = this.m_Toggle.value;
				evt.StopPropagation();
			});
			this.m_Toggle.AddToClassList(Foldout.toggleUssClassName);
			base.hierarchy.Add(this.m_Toggle);
			this.m_Container = new VisualElement
			{
				name = "unity-content"
			};
			this.m_Container.AddToClassList(Foldout.contentUssClassName);
			base.hierarchy.Add(this.m_Container);
			base.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
		}

		private void OnAttachToPanel(AttachToPanelEvent evt)
		{
			int num = 0;
			for (int i = 0; i <= Foldout.ussFoldoutMaxDepth; i++)
			{
				base.RemoveFromClassList(Foldout.ussFoldoutDepthClassName + i.ToString());
			}
			base.RemoveFromClassList(Foldout.ussFoldoutDepthClassName + "max");
			bool flag = base.parent != null;
			if (flag)
			{
				for (VisualElement parent = base.parent; parent != null; parent = parent.parent)
				{
					bool flag2 = parent.GetType() == typeof(Foldout);
					if (flag2)
					{
						num++;
					}
				}
			}
			bool flag3 = num > Foldout.ussFoldoutMaxDepth;
			if (flag3)
			{
				base.AddToClassList(Foldout.ussFoldoutDepthClassName + "max");
			}
			else
			{
				base.AddToClassList(Foldout.ussFoldoutDepthClassName + num.ToString());
			}
		}
	}
}
