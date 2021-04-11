using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Export/Debug/Debug.bindings.h")]
	public class Debug
	{
		internal static ILogger s_Logger = new Logger(new DebugLogHandler());

		public static ILogger unityLogger
		{
			get
			{
				return Debug.s_Logger;
			}
		}

		public static extern bool developerConsoleVisible
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty(TargetType = TargetType.Field), StaticAccessor("GetBuildSettings()", StaticAccessorType.Dot)]
		public static extern bool isDebugBuild
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Debug.logger is obsolete. Please use Debug.unityLogger instead (UnityUpgradable) -> unityLogger")]
		public static ILogger logger
		{
			get
			{
				return Debug.s_Logger;
			}
		}

		[ExcludeFromDocs]
		public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
		{
			bool depthTest = true;
			Debug.DrawLine(start, end, color, duration, depthTest);
		}

		[ExcludeFromDocs]
		public static void DrawLine(Vector3 start, Vector3 end, Color color)
		{
			bool depthTest = true;
			float duration = 0f;
			Debug.DrawLine(start, end, color, duration, depthTest);
		}

		[ExcludeFromDocs]
		public static void DrawLine(Vector3 start, Vector3 end)
		{
			bool depthTest = true;
			float duration = 0f;
			Color white = Color.white;
			Debug.DrawLine(start, end, white, duration, depthTest);
		}

		[FreeFunction("DebugDrawLine", IsThreadSafe = true)]
		public static void DrawLine(Vector3 start, Vector3 end, [UnityEngine.Internal.DefaultValue("Color.white")] Color color, [UnityEngine.Internal.DefaultValue("0.0f")] float duration, [UnityEngine.Internal.DefaultValue("true")] bool depthTest)
		{
			Debug.DrawLine_Injected(ref start, ref end, ref color, duration, depthTest);
		}

		[ExcludeFromDocs]
		public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration)
		{
			bool depthTest = true;
			Debug.DrawRay(start, dir, color, duration, depthTest);
		}

		[ExcludeFromDocs]
		public static void DrawRay(Vector3 start, Vector3 dir, Color color)
		{
			bool depthTest = true;
			float duration = 0f;
			Debug.DrawRay(start, dir, color, duration, depthTest);
		}

		[ExcludeFromDocs]
		public static void DrawRay(Vector3 start, Vector3 dir)
		{
			bool depthTest = true;
			float duration = 0f;
			Color white = Color.white;
			Debug.DrawRay(start, dir, white, duration, depthTest);
		}

		public static void DrawRay(Vector3 start, Vector3 dir, [UnityEngine.Internal.DefaultValue("Color.white")] Color color, [UnityEngine.Internal.DefaultValue("0.0f")] float duration, [UnityEngine.Internal.DefaultValue("true")] bool depthTest)
		{
			Debug.DrawLine(start, start + dir, color, duration, depthTest);
		}

		[FreeFunction("PauseEditor")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Break();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DebugBreak();

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern int ExtractStackTraceNoAlloc(byte* buffer, int bufferMax, string projectFolder);

		public static void Log(object message)
		{
			Debug.unityLogger.Log(LogType.Log, message);
		}

		public static void Log(object message, Object context)
		{
			Debug.unityLogger.Log(LogType.Log, message, context);
		}

		public static void LogFormat(string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Log, format, args);
		}

		public static void LogFormat(Object context, string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Log, context, format, args);
		}

		public static void LogFormat(LogType logType, LogOption logOptions, Object context, string format, params object[] args)
		{
			DebugLogHandler debugLogHandler = Debug.unityLogger.logHandler as DebugLogHandler;
			bool flag = debugLogHandler == null;
			if (flag)
			{
				Debug.unityLogger.LogFormat(logType, context, format, args);
			}
			else
			{
				debugLogHandler.LogFormat(logType, logOptions, context, format, args);
			}
		}

		public static void LogError(object message)
		{
			Debug.unityLogger.Log(LogType.Error, message);
		}

		public static void LogError(object message, Object context)
		{
			Debug.unityLogger.Log(LogType.Error, message, context);
		}

		public static void LogErrorFormat(string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Error, format, args);
		}

		public static void LogErrorFormat(Object context, string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Error, context, format, args);
		}

		internal static void LogError(string message, string fileName, int lineNumber, int columnNumber)
		{
			Debug.LogCompilerError(message, fileName, lineNumber, columnNumber);
		}

		internal static void LogWarning(string message, string fileName, int lineNumber, int columnNumber)
		{
			Debug.LogCompilerWarning(message, fileName, lineNumber, columnNumber);
		}

		internal static void LogInfo(string message, string fileName, int lineNumber, int columnNumber)
		{
			Debug.LogInformation(message, fileName, lineNumber, columnNumber);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LogCompilerWarning(string message, string fileName, int lineNumber, int columnNumber);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LogCompilerError(string message, string fileName, int lineNumber, int columnNumber);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LogInformation(string message, string fileName, int lineNumber, int columnNumber);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ClearDeveloperConsole();

		public static void LogException(Exception exception)
		{
			Debug.unityLogger.LogException(exception, null);
		}

		public static void LogException(Exception exception, Object context)
		{
			Debug.unityLogger.LogException(exception, context);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void LogPlayerBuildError(string message, string file, int line, int column);

		public static void LogWarning(object message)
		{
			Debug.unityLogger.Log(LogType.Warning, message);
		}

		public static void LogWarning(object message, Object context)
		{
			Debug.unityLogger.Log(LogType.Warning, message, context);
		}

		public static void LogWarningFormat(string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Warning, format, args);
		}

		public static void LogWarningFormat(Object context, string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Warning, context, format, args);
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, "Assertion failed");
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, Object context)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, "Assertion failed", context);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, object message)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, message);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, string message)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, message);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, object message, Object context)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, message, context);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, string message, Object context)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, message, context);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AssertFormat(bool condition, string format, params object[] args)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.LogFormat(LogType.Assert, format, args);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AssertFormat(bool condition, Object context, string format, params object[] args)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.LogFormat(LogType.Assert, context, format, args);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertion(object message)
		{
			Debug.unityLogger.Log(LogType.Assert, message);
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertion(object message, Object context)
		{
			Debug.unityLogger.Log(LogType.Assert, message, context);
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertionFormat(string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Assert, format, args);
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertionFormat(Object context, string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Assert, context, format, args);
		}

		[FreeFunction("DeveloperConsole_OpenConsoleFile")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void OpenConsoleFile();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GetDiagnosticSwitches(List<DiagnosticSwitch> results);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object GetDiagnosticSwitch(string name);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetDiagnosticSwitch(string name, object value, bool setPersistent);

		[RequiredByNativeCode]
		internal static bool CallOverridenDebugHandler(Exception exception, Object obj)
		{
			bool flag = Debug.s_Logger.logHandler is DebugLogHandler;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Debug.s_Logger.LogException(exception, obj);
				result = true;
			}
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void LogSticky(int identifier, LogType logType, LogOption logOptions, string message, Object context = null);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void RemoveLogEntriesByIdentifier(int identifier);

		[EditorBrowsable(EditorBrowsableState.Never), Conditional("UNITY_ASSERTIONS"), Obsolete("Assert(bool, string, params object[]) is obsolete. Use AssertFormat(bool, string, params object[]) (UnityUpgradable) -> AssertFormat(*)", true)]
		public static void Assert(bool condition, string format, params object[] args)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.LogFormat(LogType.Assert, format, args);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawLine_Injected(ref Vector3 start, ref Vector3 end, [UnityEngine.Internal.DefaultValue("Color.white")] ref Color color, [UnityEngine.Internal.DefaultValue("0.0f")] float duration, [UnityEngine.Internal.DefaultValue("true")] bool depthTest);
	}
}
