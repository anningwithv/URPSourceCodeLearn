using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	public class UxmlTemplateTraits : UxmlTraits
	{
		private UxmlStringAttributeDescription m_Name = new UxmlStringAttributeDescription
		{
			name = "name",
			use = UxmlAttributeDescription.Use.Required
		};

		private UxmlStringAttributeDescription m_Path = new UxmlStringAttributeDescription
		{
			name = "path"
		};

		private UxmlStringAttributeDescription m_Src = new UxmlStringAttributeDescription
		{
			name = "src"
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
