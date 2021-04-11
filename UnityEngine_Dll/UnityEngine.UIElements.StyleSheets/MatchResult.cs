using System;

namespace UnityEngine.UIElements.StyleSheets
{
	internal struct MatchResult
	{
		public MatchResultErrorCode errorCode;

		public string errorValue;

		public bool success
		{
			get
			{
				return this.errorCode == MatchResultErrorCode.None;
			}
		}
	}
}
