using System;

namespace UnityEngine.UIElements
{
	public class CustomStyleResolvedEvent : EventBase<CustomStyleResolvedEvent>
	{
		public ICustomStyle customStyle
		{
			get
			{
				VisualElement expr_0C = base.target as VisualElement;
				return (expr_0C != null) ? expr_0C.customStyle : null;
			}
		}
	}
}
