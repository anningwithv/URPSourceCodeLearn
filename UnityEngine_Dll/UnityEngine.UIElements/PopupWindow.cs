using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	public class PopupWindow : TextElement
	{
		public new class UxmlFactory : UxmlFactory<PopupWindow, PopupWindow.UxmlTraits>
		{
		}

		public new class UxmlTraits : TextElement.UxmlTraits
		{
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield return new UxmlChildElementDescription(typeof(VisualElement));
					yield break;
				}
			}
		}

		private VisualElement m_ContentContainer;

		public new static readonly string ussClassName = "unity-popup-window";

		public static readonly string contentUssClassName = PopupWindow.ussClassName + "__content-container";

		public override VisualElement contentContainer
		{
			get
			{
				return this.m_ContentContainer;
			}
		}

		public PopupWindow()
		{
			base.AddToClassList(PopupWindow.ussClassName);
			this.m_ContentContainer = new VisualElement
			{
				name = "unity-content-container"
			};
			this.m_ContentContainer.AddToClassList(PopupWindow.contentUssClassName);
			base.hierarchy.Add(this.m_ContentContainer);
		}
	}
}
