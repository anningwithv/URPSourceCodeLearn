using System;

namespace UnityEngine.UIElements
{
	public interface IBindable
	{
		IBinding binding
		{
			get;
			set;
		}

		string bindingPath
		{
			get;
			set;
		}
	}
}
