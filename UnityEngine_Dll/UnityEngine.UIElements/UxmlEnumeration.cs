using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.UIElements
{
	public class UxmlEnumeration : UxmlTypeRestriction
	{
		private List<string> m_Values = new List<string>();

		public IEnumerable<string> values
		{
			get
			{
				return this.m_Values;
			}
			set
			{
				this.m_Values = value.ToList<string>();
			}
		}

		public override bool Equals(UxmlTypeRestriction other)
		{
			UxmlEnumeration uxmlEnumeration = other as UxmlEnumeration;
			bool flag = uxmlEnumeration == null;
			return !flag && this.values.All(new Func<string, bool>(uxmlEnumeration.values.Contains<string>)) && this.values.Count<string>() == uxmlEnumeration.values.Count<string>();
		}
	}
}
