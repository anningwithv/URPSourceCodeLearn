using System;
using System.Diagnostics;
using System.Reflection;
using System.Security;
using System.Text;
using UnityEngine.Scripting;

namespace UnityEngine
{
	public static class StackTraceUtility
	{
		private static string projectFolder = "";

		[RequiredByNativeCode]
		internal static void SetProjectFolder(string folder)
		{
			StackTraceUtility.projectFolder = folder;
			bool flag = !string.IsNullOrEmpty(StackTraceUtility.projectFolder);
			if (flag)
			{
				StackTraceUtility.projectFolder = StackTraceUtility.projectFolder.Replace("\\", "/");
			}
		}

		[SecuritySafeCritical, RequiredByNativeCode]
		public unsafe static string ExtractStackTrace()
		{
			int num = 16384;
			byte* ptr = stackalloc byte[num];
			int num2 = Debug.ExtractStackTraceNoAlloc(ptr, num, StackTraceUtility.projectFolder);
			bool flag = num2 > 0;
			string result;
			if (flag)
			{
				result = new string((sbyte*)ptr, 0, num2, Encoding.UTF8);
			}
			else
			{
				StackTrace stackTrace = new StackTrace(1, true);
				string text = StackTraceUtility.ExtractFormattedStackTrace(stackTrace);
				result = text;
			}
			return result;
		}

		public static string ExtractStringFromException(object exception)
		{
			string str;
			string str2;
			StackTraceUtility.ExtractStringFromExceptionInternal(exception, out str, out str2);
			return str + "\n" + str2;
		}

		[SecuritySafeCritical, RequiredByNativeCode]
		internal static void ExtractStringFromExceptionInternal(object exceptiono, out string message, out string stackTrace)
		{
			bool flag = exceptiono == null;
			if (flag)
			{
				throw new ArgumentException("ExtractStringFromExceptionInternal called with null exception");
			}
			Exception ex = exceptiono as Exception;
			bool flag2 = ex == null;
			if (flag2)
			{
				throw new ArgumentException("ExtractStringFromExceptionInternal called with an exceptoin that was not of type System.Exception");
			}
			StringBuilder stringBuilder = new StringBuilder((ex.StackTrace == null) ? 512 : (ex.StackTrace.Length * 2));
			message = "";
			string text = "";
			while (ex != null)
			{
				bool flag3 = text.Length == 0;
				if (flag3)
				{
					text = ex.StackTrace;
				}
				else
				{
					text = ex.StackTrace + "\n" + text;
				}
				string text2 = ex.GetType().Name;
				string text3 = "";
				bool flag4 = ex.Message != null;
				if (flag4)
				{
					text3 = ex.Message;
				}
				bool flag5 = text3.Trim().Length != 0;
				if (flag5)
				{
					text2 += ": ";
					text2 += text3;
				}
				message = text2;
				bool flag6 = ex.InnerException != null;
				if (flag6)
				{
					text = "Rethrow as " + text2 + "\n" + text;
				}
				ex = ex.InnerException;
			}
			stringBuilder.Append(text + "\n");
			StackTrace stackTrace2 = new StackTrace(1, true);
			stringBuilder.Append(StackTraceUtility.ExtractFormattedStackTrace(stackTrace2));
			stackTrace = stringBuilder.ToString();
		}

		[SecuritySafeCritical]
		internal static string ExtractFormattedStackTrace(StackTrace stackTrace)
		{
			StringBuilder stringBuilder = new StringBuilder(255);
			for (int i = 0; i < stackTrace.FrameCount; i++)
			{
				StackFrame frame = stackTrace.GetFrame(i);
				MethodBase method = frame.GetMethod();
				bool flag = method == null;
				if (!flag)
				{
					Type declaringType = method.DeclaringType;
					bool flag2 = declaringType == null;
					if (!flag2)
					{
						string @namespace = declaringType.Namespace;
						bool flag3 = !string.IsNullOrEmpty(@namespace);
						if (flag3)
						{
							stringBuilder.Append(@namespace);
							stringBuilder.Append(".");
						}
						stringBuilder.Append(declaringType.Name);
						stringBuilder.Append(":");
						stringBuilder.Append(method.Name);
						stringBuilder.Append("(");
						int j = 0;
						ParameterInfo[] parameters = method.GetParameters();
						bool flag4 = true;
						while (j < parameters.Length)
						{
							bool flag5 = !flag4;
							if (flag5)
							{
								stringBuilder.Append(", ");
							}
							else
							{
								flag4 = false;
							}
							stringBuilder.Append(parameters[j].ParameterType.Name);
							j++;
						}
						stringBuilder.Append(")");
						string text = frame.GetFileName();
						bool flag6 = text != null;
						if (flag6)
						{
							bool flag7 = (declaringType.Name == "Debug" && declaringType.Namespace == "UnityEngine") || (declaringType.Name == "Logger" && declaringType.Namespace == "UnityEngine") || (declaringType.Name == "DebugLogHandler" && declaringType.Namespace == "UnityEngine") || (declaringType.Name == "Assert" && declaringType.Namespace == "UnityEngine.Assertions") || (method.Name == "print" && declaringType.Name == "MonoBehaviour" && declaringType.Namespace == "UnityEngine");
							bool flag8 = !flag7;
							if (flag8)
							{
								stringBuilder.Append(" (at ");
								bool flag9 = !string.IsNullOrEmpty(StackTraceUtility.projectFolder);
								if (flag9)
								{
									bool flag10 = text.Replace("\\", "/").StartsWith(StackTraceUtility.projectFolder);
									if (flag10)
									{
										text = text.Substring(StackTraceUtility.projectFolder.Length, text.Length - StackTraceUtility.projectFolder.Length);
									}
								}
								stringBuilder.Append(text);
								stringBuilder.Append(":");
								stringBuilder.Append(frame.GetFileLineNumber().ToString());
								stringBuilder.Append(")");
							}
						}
						stringBuilder.Append("\n");
					}
				}
			}
			return stringBuilder.ToString();
		}
	}
}
