using System;

namespace UnityEngine.UIElements
{
	public interface IBinding
	{
		void PreUpdate();

		void Update();

		void Release();
	}
}
