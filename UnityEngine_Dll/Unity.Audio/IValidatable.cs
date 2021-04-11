using System;
using UnityEngine.Bindings;

namespace Unity.Audio
{
	[VisibleToOtherModules]
	internal interface IValidatable
	{
		bool Valid
		{
			get;
		}
	}
}
