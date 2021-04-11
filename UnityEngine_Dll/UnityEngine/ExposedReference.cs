using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[UsedByNativeCode(Name = "ExposedReference")]
	[Serializable]
	public struct ExposedReference<T> where T : Object
	{
		[SerializeField]
		public PropertyName exposedName;

		[SerializeField]
		public Object defaultValue;

		public T Resolve(IExposedPropertyTable resolver)
		{
			bool flag = resolver != null;
			T result;
			if (flag)
			{
				bool flag2;
				Object referenceValue = resolver.GetReferenceValue(this.exposedName, out flag2);
				bool flag3 = flag2;
				if (flag3)
				{
					result = (referenceValue as T);
					return result;
				}
			}
			result = (this.defaultValue as T);
			return result;
		}
	}
}
