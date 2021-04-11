using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	internal interface IDragAndDropData
	{
		object userData
		{
			get;
		}

		IEnumerable<UnityEngine.Object> unityObjectReferences
		{
			get;
		}

		object GetGenericData(string key);
	}
}
