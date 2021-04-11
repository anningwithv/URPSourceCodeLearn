using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("PlatformDependent/iPhonePlayer/IOSScriptBindings.h")]
	internal sealed class UnhandledExceptionHandler
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly UnhandledExceptionHandler.<>c <>9 = new UnhandledExceptionHandler.<>c();

			public static UnhandledExceptionEventHandler <>9__0_0;

			internal void <RegisterUECatcher>b__0_0(object sender, UnhandledExceptionEventArgs e)
			{
				Debug.LogException(e.ExceptionObject as Exception);
			}
		}

		[RequiredByNativeCode]
		private static void RegisterUECatcher()
		{
			AppDomain arg_25_0 = AppDomain.CurrentDomain;
			UnhandledExceptionEventHandler arg_25_1;
			if ((arg_25_1 = UnhandledExceptionHandler.<>c.<>9__0_0) == null)
			{
				arg_25_1 = (UnhandledExceptionHandler.<>c.<>9__0_0 = new UnhandledExceptionEventHandler(UnhandledExceptionHandler.<>c.<>9.<RegisterUECatcher>b__0_0));
			}
			arg_25_0.UnhandledException += arg_25_1;
		}
	}
}
