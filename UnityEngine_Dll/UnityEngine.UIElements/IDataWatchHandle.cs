using System;

namespace UnityEngine.UIElements
{
	internal interface IDataWatchHandle : IDisposable
	{
		UnityEngine.Object watched
		{
			get;
		}

		bool disposed
		{
			get;
		}
	}
}
