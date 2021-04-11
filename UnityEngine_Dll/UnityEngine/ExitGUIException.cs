using System;

namespace UnityEngine
{
	public sealed class ExitGUIException : Exception
	{
		public ExitGUIException()
		{
			GUIUtility.guiIsExiting = true;
		}

		internal ExitGUIException(string message) : base(message)
		{
			GUIUtility.guiIsExiting = true;
			Console.WriteLine(message);
		}
	}
}
