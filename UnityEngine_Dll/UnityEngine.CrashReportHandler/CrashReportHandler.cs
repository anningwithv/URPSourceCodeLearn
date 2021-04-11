using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.CrashReportHandler
{
	[NativeHeader("Modules/CrashReporting/Public/CrashReporter.h"), StaticAccessor("CrashReporting::CrashReporter::Get()", StaticAccessorType.Dot)]
	public class CrashReportHandler
	{
		[NativeProperty("Enabled")]
		public static extern bool enableCaptureExceptions
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeThrows]
		public static extern uint logBufferSize
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		private CrashReportHandler()
		{
		}

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetUserMetadata(string key);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetUserMetadata(string key, string value);
	}
}
