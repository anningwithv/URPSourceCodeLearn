using System;

namespace UnityEngine.UIElements
{
	internal class ImmediateModeException : Exception
	{
		public ImmediateModeException(Exception inner) : base("", inner)
		{
		}
	}
}
