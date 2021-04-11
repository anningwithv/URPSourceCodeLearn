using System;

namespace UnityEngine.UIElements
{
	public class UxmlValueMatches : UxmlTypeRestriction
	{
		public string regex
		{
			get;
			set;
		}

		public override bool Equals(UxmlTypeRestriction other)
		{
			UxmlValueMatches uxmlValueMatches = other as UxmlValueMatches;
			bool flag = uxmlValueMatches == null;
			return !flag && this.regex == uxmlValueMatches.regex;
		}
	}
}
