using System;

namespace UnityEngine.UIElements
{
	public class BaseFieldTraits<TValueType, TValueUxmlAttributeType> : BaseField<TValueType>.UxmlTraits where TValueUxmlAttributeType : TypedUxmlAttributeDescription<TValueType>, new()
	{
		private TValueUxmlAttributeType m_Value;

		public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
		{
			base.Init(ve, bag, cc);
			((INotifyValueChanged<TValueType>)ve).SetValueWithoutNotify(this.m_Value.GetValueFromBag(bag, cc));
		}

		public BaseFieldTraits()
		{
			TValueUxmlAttributeType expr_06 = Activator.CreateInstance<TValueUxmlAttributeType>();
			expr_06.name = "value";
			this.m_Value = expr_06;
			base..ctor();
		}
	}
}
