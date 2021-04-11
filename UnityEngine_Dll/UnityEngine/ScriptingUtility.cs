using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	internal class ScriptingUtility
	{
		private struct TestClass
		{
			public int value;
		}

		[RequiredByNativeCode]
		private static bool IsManagedCodeWorking()
		{
			ScriptingUtility.TestClass testClass = new ScriptingUtility.TestClass
			{
				value = 42
			};
			return testClass.value == 42;
		}
	}
}
