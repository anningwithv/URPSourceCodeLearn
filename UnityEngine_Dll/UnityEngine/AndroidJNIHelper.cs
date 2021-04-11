using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeConditional("PLATFORM_ANDROID"), NativeHeader("Modules/AndroidJNI/Public/AndroidJNIBindingsHelpers.h"), StaticAccessor("AndroidJNIBindingsHelpers", StaticAccessorType.DoubleColon), UsedByNativeCode]
	public static class AndroidJNIHelper
	{
		public static extern bool debug
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static IntPtr GetConstructorID(IntPtr javaClass)
		{
			return AndroidJNIHelper.GetConstructorID(javaClass, "");
		}

		public static IntPtr GetConstructorID(IntPtr javaClass, [DefaultValue("")] string signature)
		{
			return _AndroidJNIHelper.GetConstructorID(javaClass, signature);
		}

		public static IntPtr GetMethodID(IntPtr javaClass, string methodName)
		{
			return AndroidJNIHelper.GetMethodID(javaClass, methodName, "", false);
		}

		public static IntPtr GetMethodID(IntPtr javaClass, string methodName, [DefaultValue("")] string signature)
		{
			return AndroidJNIHelper.GetMethodID(javaClass, methodName, signature, false);
		}

		public static IntPtr GetMethodID(IntPtr javaClass, string methodName, [DefaultValue("")] string signature, [DefaultValue("false")] bool isStatic)
		{
			return _AndroidJNIHelper.GetMethodID(javaClass, methodName, signature, isStatic);
		}

		public static IntPtr GetFieldID(IntPtr javaClass, string fieldName)
		{
			return AndroidJNIHelper.GetFieldID(javaClass, fieldName, "", false);
		}

		public static IntPtr GetFieldID(IntPtr javaClass, string fieldName, [DefaultValue("")] string signature)
		{
			return AndroidJNIHelper.GetFieldID(javaClass, fieldName, signature, false);
		}

		public static IntPtr GetFieldID(IntPtr javaClass, string fieldName, [DefaultValue("")] string signature, [DefaultValue("false")] bool isStatic)
		{
			return _AndroidJNIHelper.GetFieldID(javaClass, fieldName, signature, isStatic);
		}

		public static IntPtr CreateJavaRunnable(AndroidJavaRunnable jrunnable)
		{
			return _AndroidJNIHelper.CreateJavaRunnable(jrunnable);
		}

		public static IntPtr CreateJavaProxy(AndroidJavaProxy proxy)
		{
			GCHandle value = GCHandle.Alloc(proxy);
			IntPtr result;
			try
			{
				result = _AndroidJNIHelper.CreateJavaProxy(GCHandle.ToIntPtr(value), proxy);
			}
			catch
			{
				value.Free();
				throw;
			}
			return result;
		}

		public static IntPtr ConvertToJNIArray(Array array)
		{
			return _AndroidJNIHelper.ConvertToJNIArray(array);
		}

		public static jvalue[] CreateJNIArgArray(object[] args)
		{
			return _AndroidJNIHelper.CreateJNIArgArray(args);
		}

		public static void DeleteJNIArgArray(object[] args, jvalue[] jniArgs)
		{
			_AndroidJNIHelper.DeleteJNIArgArray(args, jniArgs);
		}

		public static IntPtr GetConstructorID(IntPtr jclass, object[] args)
		{
			return _AndroidJNIHelper.GetConstructorID(jclass, args);
		}

		public static IntPtr GetMethodID(IntPtr jclass, string methodName, object[] args, bool isStatic)
		{
			return _AndroidJNIHelper.GetMethodID(jclass, methodName, args, isStatic);
		}

		public static string GetSignature(object obj)
		{
			return _AndroidJNIHelper.GetSignature(obj);
		}

		public static string GetSignature(object[] args)
		{
			return _AndroidJNIHelper.GetSignature(args);
		}

		public static ArrayType ConvertFromJNIArray<ArrayType>(IntPtr array)
		{
			return _AndroidJNIHelper.ConvertFromJNIArray<ArrayType>(array);
		}

		public static IntPtr GetMethodID<ReturnType>(IntPtr jclass, string methodName, object[] args, bool isStatic)
		{
			return _AndroidJNIHelper.GetMethodID<ReturnType>(jclass, methodName, args, isStatic);
		}

		public static IntPtr GetFieldID<FieldType>(IntPtr jclass, string fieldName, bool isStatic)
		{
			return _AndroidJNIHelper.GetFieldID<FieldType>(jclass, fieldName, isStatic);
		}

		public static string GetSignature<ReturnType>(object[] args)
		{
			return _AndroidJNIHelper.GetSignature<ReturnType>(args);
		}
	}
}
