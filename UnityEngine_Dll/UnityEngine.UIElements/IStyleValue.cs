using System;

namespace UnityEngine.UIElements
{
	internal interface IStyleValue<T>
	{
		T value
		{
			get;
			set;
		}

		StyleKeyword keyword
		{
			get;
			set;
		}
	}
}
