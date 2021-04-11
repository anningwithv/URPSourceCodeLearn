using System;

namespace UnityEngine.UIElements
{
	public class UxmlChildElementDescription
	{
		public string elementName
		{
			get;
			protected set;
		}

		public string elementNamespace
		{
			get;
			protected set;
		}

		public UxmlChildElementDescription(Type t)
		{
			bool flag = t == null;
			if (flag)
			{
				throw new ArgumentNullException("t");
			}
			this.elementName = t.Name;
			this.elementNamespace = t.Namespace;
		}
	}
}
