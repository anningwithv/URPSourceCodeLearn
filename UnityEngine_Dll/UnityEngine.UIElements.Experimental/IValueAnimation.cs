using System;

namespace UnityEngine.UIElements.Experimental
{
	public interface IValueAnimation
	{
		bool isRunning
		{
			get;
		}

		int durationMs
		{
			get;
			set;
		}

		void Start();

		void Stop();

		void Recycle();
	}
}
