using System;

namespace UnityEngine.UIElements
{
	public interface INotifyValueChanged<T>
	{
		T value
		{
			get;
			set;
		}

		void SetValueWithoutNotify(T newValue);
	}
}
