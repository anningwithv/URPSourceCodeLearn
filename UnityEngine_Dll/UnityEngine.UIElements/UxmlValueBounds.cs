using System;

namespace UnityEngine.UIElements
{
	public class UxmlValueBounds : UxmlTypeRestriction
	{
		public string min
		{
			get;
			set;
		}

		public string max
		{
			get;
			set;
		}

		public bool excludeMin
		{
			get;
			set;
		}

		public bool excludeMax
		{
			get;
			set;
		}

		public override bool Equals(UxmlTypeRestriction other)
		{
			UxmlValueBounds uxmlValueBounds = other as UxmlValueBounds;
			bool flag = uxmlValueBounds == null;
			return !flag && (this.min == uxmlValueBounds.min && this.max == uxmlValueBounds.max && this.excludeMin == uxmlValueBounds.excludeMin) && this.excludeMax == uxmlValueBounds.excludeMax;
		}
	}
}
