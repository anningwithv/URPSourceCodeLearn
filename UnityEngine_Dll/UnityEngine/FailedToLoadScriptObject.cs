using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[ExcludeFromObjectFactory, NativeClass(null), RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	internal class FailedToLoadScriptObject : Object
	{
		private FailedToLoadScriptObject()
		{
		}
	}
}
