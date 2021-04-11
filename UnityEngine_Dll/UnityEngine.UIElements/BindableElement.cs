using System;

namespace UnityEngine.UIElements
{
	public class BindableElement : VisualElement, IBindable
	{
		public new class UxmlFactory : UxmlFactory<BindableElement, BindableElement.UxmlTraits>
		{
		}

		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			private UxmlStringAttributeDescription m_PropertyPath;

			public UxmlTraits()
			{
				this.m_PropertyPath = new UxmlStringAttributeDescription
				{
					name = "binding-path"
				};
			}

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				string valueFromBag = this.m_PropertyPath.GetValueFromBag(bag, cc);
				bool flag = !string.IsNullOrEmpty(valueFromBag);
				if (flag)
				{
					IBindable bindable = ve as IBindable;
					bool flag2 = bindable != null;
					if (flag2)
					{
						bindable.bindingPath = valueFromBag;
					}
				}
			}
		}

		public IBinding binding
		{
			get;
			set;
		}

		public string bindingPath
		{
			get;
			set;
		}
	}
}
