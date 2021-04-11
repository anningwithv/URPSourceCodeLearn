using System;

namespace UnityEngine
{
	public interface ISubsystem
	{
		bool running
		{
			get;
		}

		void Start();

		void Stop();

		void Destroy();
	}
}
