using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	internal class AndroidReflection
	{
		private const string RELECTION_HELPER_CLASS_NAME = "com/unity3d/player/ReflectionHelper";

		private static readonly GlobalJavaObjectRef s_ReflectionHelperClass = new GlobalJavaObjectRef(AndroidJNISafe.FindClass("com/unity3d/player/ReflectionHelper"));

		private static readonly IntPtr s_ReflectionHelperGetConstructorID = AndroidReflection.GetStaticMethodID("com/unity3d/player/ReflectionHelper", "getConstructorID", "(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/reflect/Constructor;");

		private static readonly IntPtr s_ReflectionHelperGetMethodID = AndroidReflection.GetStaticMethodID("com/unity3d/player/ReflectionHelper", "getMethodID", "(Ljava/lang/Class;Ljava/lang/String;Ljava/lang/String;Z)Ljava/lang/reflect/Method;");

		private static readonly IntPtr s_ReflectionHelperGetFieldID = AndroidReflection.GetStaticMethodID("com/unity3d/player/ReflectionHelper", "getFieldID", "(Ljava/lang/Class;Ljava/lang/String;Ljava/lang/String;Z)Ljava/lang/reflect/Field;");

		private static readonly IntPtr s_ReflectionHelperGetFieldSignature = AndroidReflection.GetStaticMethodID("com/unity3d/player/ReflectionHelper", "getFieldSignature", "(Ljava/lang/reflect/Field;)Ljava/lang/String;");

		private static readonly IntPtr s_ReflectionHelperNewProxyInstance = AndroidReflection.GetStaticMethodID("com/unity3d/player/ReflectionHelper", "newProxyInstance", "(JLjava/lang/Class;)Ljava/lang/Object;");

		private static readonly IntPtr s_ReflectionHelperSetNativeExceptionOnProxy = AndroidReflection.GetStaticMethodID("com/unity3d/player/ReflectionHelper", "setNativeExceptionOnProxy", "(Ljava/lang/Object;JZ)V");

		private static readonly IntPtr s_FieldGetDeclaringClass = AndroidReflection.GetMethodID("java/lang/reflect/Field", "getDeclaringClass", "()Ljava/lang/Class;");

		public static bool IsPrimitive(Type t)
		{
			return t.IsPrimitive;
		}

		public static bool IsAssignableFrom(Type t, Type from)
		{
			return t.IsAssignableFrom(from);
		}

		private static IntPtr GetStaticMethodID(string clazz, string methodName, string signature)
		{
			IntPtr intPtr = AndroidJNISafe.FindClass(clazz);
			IntPtr staticMethodID;
			try
			{
				staticMethodID = AndroidJNISafe.GetStaticMethodID(intPtr, methodName, signature);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(intPtr);
			}
			return staticMethodID;
		}

		private static IntPtr GetMethodID(string clazz, string methodName, string signature)
		{
			IntPtr intPtr = AndroidJNISafe.FindClass(clazz);
			IntPtr methodID;
			try
			{
				methodID = AndroidJNISafe.GetMethodID(intPtr, methodName, signature);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(intPtr);
			}
			return methodID;
		}

		public static IntPtr GetConstructorMember(IntPtr jclass, string signature)
		{
			jvalue[] array = new jvalue[2];
			IntPtr result;
			try
			{
				array[0].l = jclass;
				array[1].l = AndroidJNISafe.NewString(signature);
				result = AndroidJNISafe.CallStaticObjectMethod(AndroidReflection.s_ReflectionHelperClass, AndroidReflection.s_ReflectionHelperGetConstructorID, array);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(array[1].l);
			}
			return result;
		}

		public static IntPtr GetMethodMember(IntPtr jclass, string methodName, string signature, bool isStatic)
		{
			jvalue[] array = new jvalue[4];
			IntPtr result;
			try
			{
				array[0].l = jclass;
				array[1].l = AndroidJNISafe.NewString(methodName);
				array[2].l = AndroidJNISafe.NewString(signature);
				array[3].z = isStatic;
				result = AndroidJNISafe.CallStaticObjectMethod(AndroidReflection.s_ReflectionHelperClass, AndroidReflection.s_ReflectionHelperGetMethodID, array);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(array[1].l);
				AndroidJNISafe.DeleteLocalRef(array[2].l);
			}
			return result;
		}

		public static IntPtr GetFieldMember(IntPtr jclass, string fieldName, string signature, bool isStatic)
		{
			jvalue[] array = new jvalue[4];
			IntPtr result;
			try
			{
				array[0].l = jclass;
				array[1].l = AndroidJNISafe.NewString(fieldName);
				array[2].l = AndroidJNISafe.NewString(signature);
				array[3].z = isStatic;
				result = AndroidJNISafe.CallStaticObjectMethod(AndroidReflection.s_ReflectionHelperClass, AndroidReflection.s_ReflectionHelperGetFieldID, array);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(array[1].l);
				AndroidJNISafe.DeleteLocalRef(array[2].l);
			}
			return result;
		}

		public static IntPtr GetFieldClass(IntPtr field)
		{
			return AndroidJNISafe.CallObjectMethod(field, AndroidReflection.s_FieldGetDeclaringClass, null);
		}

		public static string GetFieldSignature(IntPtr field)
		{
			jvalue[] array = new jvalue[1];
			array[0].l = field;
			return AndroidJNISafe.CallStaticStringMethod(AndroidReflection.s_ReflectionHelperClass, AndroidReflection.s_ReflectionHelperGetFieldSignature, array);
		}

		public static IntPtr NewProxyInstance(IntPtr delegateHandle, IntPtr interfaze)
		{
			jvalue[] array = new jvalue[2];
			array[0].j = delegateHandle.ToInt64();
			array[1].l = interfaze;
			return AndroidJNISafe.CallStaticObjectMethod(AndroidReflection.s_ReflectionHelperClass, AndroidReflection.s_ReflectionHelperNewProxyInstance, array);
		}

		public static void SetNativeExceptionOnProxy(IntPtr proxy, Exception e, bool methodNotFound)
		{
			jvalue[] array = new jvalue[3];
			array[0].l = proxy;
			array[1].j = GCHandle.ToIntPtr(GCHandle.Alloc(e)).ToInt64();
			array[2].z = methodNotFound;
			AndroidJNISafe.CallStaticVoidMethod(AndroidReflection.s_ReflectionHelperClass, AndroidReflection.s_ReflectionHelperSetNativeExceptionOnProxy, array);
		}
	}
}
