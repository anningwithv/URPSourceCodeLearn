using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeConditional("PLATFORM_ANDROID"), NativeHeader("Modules/AndroidJNI/Public/AndroidJNIBindingsHelpers.h"), StaticAccessor("AndroidJNIBindingsHelpers", StaticAccessorType.DoubleColon)]
	public static class AndroidJNI
	{
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int AttachCurrentThread();

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int DetachCurrentThread();

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetVersion();

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr FindClass(string name);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr FromReflectedMethod(IntPtr refMethod);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr FromReflectedField(IntPtr refField);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToReflectedMethod(IntPtr clazz, IntPtr methodID, bool isStatic);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToReflectedField(IntPtr clazz, IntPtr fieldID, bool isStatic);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetSuperclass(IntPtr clazz);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsAssignableFrom(IntPtr clazz1, IntPtr clazz2);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int Throw(IntPtr obj);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int ThrowNew(IntPtr clazz, string message);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ExceptionOccurred();

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ExceptionDescribe();

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ExceptionClear();

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FatalError(string message);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int PushLocalFrame(int capacity);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr PopLocalFrame(IntPtr ptr);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewGlobalRef(IntPtr obj);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DeleteGlobalRef(IntPtr obj);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewWeakGlobalRef(IntPtr obj);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DeleteWeakGlobalRef(IntPtr obj);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewLocalRef(IntPtr obj);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DeleteLocalRef(IntPtr obj);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsSameObject(IntPtr obj1, IntPtr obj2);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int EnsureLocalCapacity(int capacity);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr AllocObject(IntPtr clazz);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewObject(IntPtr clazz, IntPtr methodID, jvalue[] args);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetObjectClass(IntPtr obj);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsInstanceOf(IntPtr obj, IntPtr clazz);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetMethodID(IntPtr clazz, string name, string sig);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetFieldID(IntPtr clazz, string name, string sig);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetStaticMethodID(IntPtr clazz, string name, string sig);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetStaticFieldID(IntPtr clazz, string name, string sig);

		public static IntPtr NewString(string chars)
		{
			return AndroidJNI.NewStringFromStr(chars);
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr NewStringFromStr(string chars);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewString(char[] chars);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewStringUTF(string bytes);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetStringChars(IntPtr str);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetStringLength(IntPtr str);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetStringUTFLength(IntPtr str);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetStringUTFChars(IntPtr str);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string CallStringMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr CallObjectMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int CallIntMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CallBooleanMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern short CallShortMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		[Obsolete("AndroidJNI.CallByteMethod is obsolete. Use AndroidJNI.CallSByteMethod method instead")]
		public static byte CallByteMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return (byte)AndroidJNI.CallSByteMethod(obj, methodID, args);
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern sbyte CallSByteMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern char CallCharMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float CallFloatMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double CallDoubleMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long CallLongMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CallVoidMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetStringField(IntPtr obj, IntPtr fieldID);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetObjectField(IntPtr obj, IntPtr fieldID);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetBooleanField(IntPtr obj, IntPtr fieldID);

		[Obsolete("AndroidJNI.GetByteField is obsolete. Use AndroidJNI.GetSByteField method instead")]
		public static byte GetByteField(IntPtr obj, IntPtr fieldID)
		{
			return (byte)AndroidJNI.GetSByteField(obj, fieldID);
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern sbyte GetSByteField(IntPtr obj, IntPtr fieldID);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern char GetCharField(IntPtr obj, IntPtr fieldID);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern short GetShortField(IntPtr obj, IntPtr fieldID);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetIntField(IntPtr obj, IntPtr fieldID);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetLongField(IntPtr obj, IntPtr fieldID);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetFloatField(IntPtr obj, IntPtr fieldID);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double GetDoubleField(IntPtr obj, IntPtr fieldID);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStringField(IntPtr obj, IntPtr fieldID, string val);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetObjectField(IntPtr obj, IntPtr fieldID, IntPtr val);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetBooleanField(IntPtr obj, IntPtr fieldID, bool val);

		[Obsolete("AndroidJNI.SetByteField is obsolete. Use AndroidJNI.SetSByteField method instead")]
		public static void SetByteField(IntPtr obj, IntPtr fieldID, byte val)
		{
			AndroidJNI.SetSByteField(obj, fieldID, (sbyte)val);
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetSByteField(IntPtr obj, IntPtr fieldID, sbyte val);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetCharField(IntPtr obj, IntPtr fieldID, char val);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetShortField(IntPtr obj, IntPtr fieldID, short val);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetIntField(IntPtr obj, IntPtr fieldID, int val);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetLongField(IntPtr obj, IntPtr fieldID, long val);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetFloatField(IntPtr obj, IntPtr fieldID, float val);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetDoubleField(IntPtr obj, IntPtr fieldID, double val);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string CallStaticStringMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr CallStaticObjectMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int CallStaticIntMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CallStaticBooleanMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern short CallStaticShortMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		[Obsolete("AndroidJNI.CallStaticByteMethod is obsolete. Use AndroidJNI.CallStaticSByteMethod method instead")]
		public static byte CallStaticByteMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return (byte)AndroidJNI.CallStaticSByteMethod(clazz, methodID, args);
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern sbyte CallStaticSByteMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern char CallStaticCharMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float CallStaticFloatMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double CallStaticDoubleMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long CallStaticLongMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CallStaticVoidMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetStaticStringField(IntPtr clazz, IntPtr fieldID);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetStaticObjectField(IntPtr clazz, IntPtr fieldID);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetStaticBooleanField(IntPtr clazz, IntPtr fieldID);

		[Obsolete("AndroidJNI.GetStaticByteField is obsolete. Use AndroidJNI.GetStaticSByteField method instead")]
		public static byte GetStaticByteField(IntPtr clazz, IntPtr fieldID)
		{
			return (byte)AndroidJNI.GetStaticSByteField(clazz, fieldID);
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern sbyte GetStaticSByteField(IntPtr clazz, IntPtr fieldID);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern char GetStaticCharField(IntPtr clazz, IntPtr fieldID);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern short GetStaticShortField(IntPtr clazz, IntPtr fieldID);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetStaticIntField(IntPtr clazz, IntPtr fieldID);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetStaticLongField(IntPtr clazz, IntPtr fieldID);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetStaticFloatField(IntPtr clazz, IntPtr fieldID);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double GetStaticDoubleField(IntPtr clazz, IntPtr fieldID);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticStringField(IntPtr clazz, IntPtr fieldID, string val);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticObjectField(IntPtr clazz, IntPtr fieldID, IntPtr val);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticBooleanField(IntPtr clazz, IntPtr fieldID, bool val);

		[Obsolete("AndroidJNI.SetStaticByteField is obsolete. Use AndroidJNI.SetStaticSByteField method instead")]
		public static void SetStaticByteField(IntPtr clazz, IntPtr fieldID, byte val)
		{
			AndroidJNI.SetStaticSByteField(clazz, fieldID, (sbyte)val);
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticSByteField(IntPtr clazz, IntPtr fieldID, sbyte val);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticCharField(IntPtr clazz, IntPtr fieldID, char val);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticShortField(IntPtr clazz, IntPtr fieldID, short val);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticIntField(IntPtr clazz, IntPtr fieldID, int val);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticLongField(IntPtr clazz, IntPtr fieldID, long val);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticFloatField(IntPtr clazz, IntPtr fieldID, float val);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticDoubleField(IntPtr clazz, IntPtr fieldID, double val);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToBooleanArray(bool[] array);

		[Obsolete("AndroidJNI.ToByteArray is obsolete. Use AndroidJNI.ToSByteArray method instead"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToByteArray(byte[] array);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToSByteArray(sbyte[] array);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToCharArray(char[] array);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToShortArray(short[] array);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToIntArray(int[] array);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToLongArray(long[] array);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToFloatArray(float[] array);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToDoubleArray(double[] array);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToObjectArray(IntPtr[] array, IntPtr arrayClass);

		public static IntPtr ToObjectArray(IntPtr[] array)
		{
			return AndroidJNI.ToObjectArray(array, IntPtr.Zero);
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool[] FromBooleanArray(IntPtr array);

		[Obsolete("AndroidJNI.FromByteArray is obsolete. Use AndroidJNI.FromSByteArray method instead"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern byte[] FromByteArray(IntPtr array);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern sbyte[] FromSByteArray(IntPtr array);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern char[] FromCharArray(IntPtr array);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern short[] FromShortArray(IntPtr array);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int[] FromIntArray(IntPtr array);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long[] FromLongArray(IntPtr array);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float[] FromFloatArray(IntPtr array);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double[] FromDoubleArray(IntPtr array);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr[] FromObjectArray(IntPtr array);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetArrayLength(IntPtr array);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewBooleanArray(int size);

		[Obsolete("AndroidJNI.NewByteArray is obsolete. Use AndroidJNI.NewSByteArray method instead")]
		public static IntPtr NewByteArray(int size)
		{
			return AndroidJNI.NewSByteArray(size);
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewSByteArray(int size);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewCharArray(int size);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewShortArray(int size);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewIntArray(int size);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewLongArray(int size);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewFloatArray(int size);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewDoubleArray(int size);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewObjectArray(int size, IntPtr clazz, IntPtr obj);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetBooleanArrayElement(IntPtr array, int index);

		[Obsolete("AndroidJNI.GetByteArrayElement is obsolete. Use AndroidJNI.GetSByteArrayElement method instead")]
		public static byte GetByteArrayElement(IntPtr array, int index)
		{
			return (byte)AndroidJNI.GetSByteArrayElement(array, index);
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern sbyte GetSByteArrayElement(IntPtr array, int index);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern char GetCharArrayElement(IntPtr array, int index);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern short GetShortArrayElement(IntPtr array, int index);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetIntArrayElement(IntPtr array, int index);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetLongArrayElement(IntPtr array, int index);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetFloatArrayElement(IntPtr array, int index);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double GetDoubleArrayElement(IntPtr array, int index);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetObjectArrayElement(IntPtr array, int index);

		[Obsolete("AndroidJNI.SetBooleanArrayElement(IntPtr, int, byte) is obsolete. Use AndroidJNI.SetBooleanArrayElement(IntPtr, int, bool) method instead")]
		public static void SetBooleanArrayElement(IntPtr array, int index, byte val)
		{
			AndroidJNI.SetBooleanArrayElement(array, index, val > 0);
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetBooleanArrayElement(IntPtr array, int index, bool val);

		[Obsolete("AndroidJNI.SetByteArrayElement is obsolete. Use AndroidJNI.SetSByteArrayElement method instead")]
		public static void SetByteArrayElement(IntPtr array, int index, sbyte val)
		{
			AndroidJNI.SetSByteArrayElement(array, index, val);
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetSByteArrayElement(IntPtr array, int index, sbyte val);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetCharArrayElement(IntPtr array, int index, char val);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetShortArrayElement(IntPtr array, int index, short val);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetIntArrayElement(IntPtr array, int index, int val);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetLongArrayElement(IntPtr array, int index, long val);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetFloatArrayElement(IntPtr array, int index, float val);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetDoubleArrayElement(IntPtr array, int index, double val);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetObjectArrayElement(IntPtr array, int index, IntPtr obj);
	}
}
