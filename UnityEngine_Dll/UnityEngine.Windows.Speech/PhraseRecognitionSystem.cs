using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Windows.Speech
{
	public static class PhraseRecognitionSystem
	{
		public delegate void ErrorDelegate(SpeechError errorCode);

		public delegate void StatusDelegate(SpeechSystemStatus status);

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event PhraseRecognitionSystem.ErrorDelegate OnError;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event PhraseRecognitionSystem.StatusDelegate OnStatusChanged;

		public static extern bool isSupported
		{
			[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h"), ThreadSafe]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern SpeechSystemStatus Status
		{
			[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h"), NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Restart();

		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Shutdown();

		[RequiredByNativeCode]
		private static void PhraseRecognitionSystem_InvokeErrorEvent(SpeechError errorCode)
		{
			PhraseRecognitionSystem.ErrorDelegate onError = PhraseRecognitionSystem.OnError;
			bool flag = onError != null;
			if (flag)
			{
				onError(errorCode);
			}
		}

		[RequiredByNativeCode]
		private static void PhraseRecognitionSystem_InvokeStatusChangedEvent(SpeechSystemStatus status)
		{
			PhraseRecognitionSystem.StatusDelegate onStatusChanged = PhraseRecognitionSystem.OnStatusChanged;
			bool flag = onStatusChanged != null;
			if (flag)
			{
				onStatusChanged(status);
			}
		}
	}
}
