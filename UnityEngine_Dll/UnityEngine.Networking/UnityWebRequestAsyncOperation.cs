using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Networking
{
	[NativeHeader("UnityWebRequestScriptingClasses.h"), NativeHeader("Modules/UnityWebRequest/Public/UnityWebRequestAsyncOperation.h"), UsedByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class UnityWebRequestAsyncOperation : AsyncOperation
	{
		public UnityWebRequest webRequest
		{
			get;
			internal set;
		}
	}
}
