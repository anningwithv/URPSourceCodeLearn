using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Export/Logging/UnityLogWriter.bindings.h")]
	internal class UnityLogWriter : TextWriter
	{
		public override Encoding Encoding
		{
			get
			{
				return Encoding.UTF8;
			}
		}

		[ThreadAndSerializationSafe]
		public static void WriteStringToUnityLog(string s)
		{
			bool flag = s == null;
			if (!flag)
			{
				UnityLogWriter.WriteStringToUnityLogImpl(s);
			}
		}

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WriteStringToUnityLogImpl(string s);

		public static void Init()
		{
			Console.SetOut(new UnityLogWriter());
		}

		public override void Write(char value)
		{
			UnityLogWriter.WriteStringToUnityLog(value.ToString());
		}

		public override void Write(string s)
		{
			UnityLogWriter.WriteStringToUnityLog(s);
		}

		public override void Write(char[] buffer, int index, int count)
		{
			UnityLogWriter.WriteStringToUnityLogImpl(new string(buffer, index, count));
		}
	}
}
