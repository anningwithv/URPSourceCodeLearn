using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Export/Debug/Debug.bindings.h")]
	internal sealed class DebugLogHandler : ILogHandler
	{
		[ThreadAndSerializationSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_Log(LogType level, LogOption options, string msg, Object obj);

		[ThreadAndSerializationSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_LogException(Exception ex, Object obj);

		public void LogFormat(LogType logType, Object context, string format, params object[] args)
		{
			DebugLogHandler.Internal_Log(logType, LogOption.None, string.Format(format, args), context);
		}

		public void LogFormat(LogType logType, LogOption logOptions, Object context, string format, params object[] args)
		{
			DebugLogHandler.Internal_Log(logType, logOptions, string.Format(format, args), context);
		}

		public void LogException(Exception exception, Object context)
		{
			bool flag = exception == null;
			if (flag)
			{
				throw new ArgumentNullException("exception");
			}
			DebugLogHandler.Internal_LogException(exception, context);
		}
	}
}
