using System;

namespace UnityEngine.UIElements.Experimental
{
	internal interface IValueAnimationUpdate
	{
		void Tick(long currentTimeMs);
	}
}
