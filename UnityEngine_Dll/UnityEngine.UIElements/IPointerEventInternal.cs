using System;

namespace UnityEngine.UIElements
{
	internal interface IPointerEventInternal
	{
		bool triggeredByOS
		{
			get;
			set;
		}

		bool recomputeTopElementUnderPointer
		{
			get;
			set;
		}
	}
}
