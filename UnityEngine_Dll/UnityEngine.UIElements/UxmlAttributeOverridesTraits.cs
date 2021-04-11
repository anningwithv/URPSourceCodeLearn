using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	public class UxmlAttributeOverridesTraits : UxmlTraits
	{
		internal const string k_ElementNameAttributeName = "element-name";

		private UxmlStringAttributeDescription m_ElementName = new UxmlStringAttributeDescription
		{
			name = "element-name",
			use = UxmlAttributeDescription.Use.Required
		};

		public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
		{
			get
			{
				yield break;
			}
		}
	}
}
