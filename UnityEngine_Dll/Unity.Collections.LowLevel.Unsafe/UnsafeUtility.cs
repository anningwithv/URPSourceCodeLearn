using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace Unity.Collections.LowLevel.Unsafe
{
	[NativeHeader("Runtime/Export/Unsafe/UnsafeUtility.bindings.h"), StaticAccessor("UnsafeUtility", StaticAccessorType.DoubleColon)]
	public static class UnsafeUtility
	{
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		internal struct IsUnmanagedCache<T>
		{
			internal static int value;
		}

		[StructLayout(LayoutKind.Sequential, Size = 1)]
		internal struct IsValidNativeContainerElementTypeCache<T>
		{
			internal static int value;
		}

		private struct AlignOfHelper<T> where T : struct
		{
			public byte dummy;

			public T data;
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetFieldOffsetInStruct(FieldInfo field);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetFieldOffsetInClass(FieldInfo field);

		public static int GetFieldOffset(FieldInfo field)
		{
			bool flag = field == null;
			if (flag)
			{
				throw new ArgumentNullException("field");
			}
			bool isValueType = field.DeclaringType.IsValueType;
			int result;
			if (isValueType)
			{
				result = UnsafeUtility.GetFieldOffsetInStruct(field);
			}
			else
			{
				bool isClass = field.DeclaringType.IsClass;
				if (!isClass)
				{
					throw new ArgumentException("field.DeclaringType must be a struct or class");
				}
				result = UnsafeUtility.GetFieldOffsetInClass(field);
			}
			return result;
		}

		public unsafe static void* PinGCObjectAndGetAddress(object target, out ulong gcHandle)
		{
			bool flag = target == null;
			if (flag)
			{
				throw new ArgumentNullException("target");
			}
			return UnsafeUtility.PinSystemObjectAndGetAddress(target, out gcHandle);
		}

		public unsafe static void* PinGCArrayAndGetDataAddress(Array target, out ulong gcHandle)
		{
			bool flag = target == null;
			if (flag)
			{
				throw new ArgumentNullException("target");
			}
			return UnsafeUtility.PinSystemArrayAndGetAddress(target, out gcHandle);
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* PinSystemArrayAndGetAddress(object target, out ulong gcHandle);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* PinSystemObjectAndGetAddress(object target, out ulong gcHandle);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ReleaseGCObject(ulong gcHandle);

		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void CopyObjectAddressToPtr(object target, void* dstPtr);

		public static bool IsBlittable<T>() where T : struct
		{
			return UnsafeUtility.IsBlittable(typeof(T));
		}

		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void* Malloc(long size, int alignment, Allocator allocator);

		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Free(void* memory, Allocator allocator);

		public static bool IsValidAllocator(Allocator allocator)
		{
			return allocator > Allocator.None;
		}

		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void MemCpy(void* destination, void* source, long size);

		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void MemCpyReplicate(void* destination, void* source, int size, int count);

		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void MemCpyStride(void* destination, int destinationStride, void* source, int sourceStride, int elementSize, int count);

		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void MemMove(void* destination, void* source, long size);

		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void MemSet(void* destination, byte value, long size);

		public unsafe static void MemClear(void* destination, long size)
		{
			UnsafeUtility.MemSet(destination, 0, size);
		}

		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern int MemCmp(void* ptr1, void* ptr2, long size);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int SizeOf(Type type);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsBlittable(Type type);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsUnmanaged(Type type);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsValidNativeContainerElementType(Type type);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void LogError(string msg, string filename, int linenumber);

		private static bool IsBlittableValueType(Type t)
		{
			return t.IsValueType && UnsafeUtility.IsBlittable(t);
		}

		private static string GetReasonForTypeNonBlittableImpl(Type t, string name)
		{
			bool flag = !t.IsValueType;
			string result;
			if (flag)
			{
				result = string.Format("{0} is not blittable because it is not of value type ({1})\n", name, t);
			}
			else
			{
				bool isPrimitive = t.IsPrimitive;
				if (isPrimitive)
				{
					result = string.Format("{0} is not blittable ({1})\n", name, t);
				}
				else
				{
					string text = "";
					FieldInfo[] fields = t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					for (int i = 0; i < fields.Length; i++)
					{
						FieldInfo fieldInfo = fields[i];
						bool flag2 = !UnsafeUtility.IsBlittableValueType(fieldInfo.FieldType);
						if (flag2)
						{
							text += UnsafeUtility.GetReasonForTypeNonBlittableImpl(fieldInfo.FieldType, string.Format("{0}.{1}", name, fieldInfo.Name));
						}
					}
					result = text;
				}
			}
			return result;
		}

		internal static bool IsArrayBlittable(Array arr)
		{
			return UnsafeUtility.IsBlittableValueType(arr.GetType().GetElementType());
		}

		internal static bool IsGenericListBlittable<T>() where T : struct
		{
			return UnsafeUtility.IsBlittable<T>();
		}

		internal static string GetReasonForArrayNonBlittable(Array arr)
		{
			Type elementType = arr.GetType().GetElementType();
			return UnsafeUtility.GetReasonForTypeNonBlittableImpl(elementType, elementType.Name);
		}

		internal static string GetReasonForGenericListNonBlittable<T>() where T : struct
		{
			Type typeFromHandle = typeof(T);
			return UnsafeUtility.GetReasonForTypeNonBlittableImpl(typeFromHandle, typeFromHandle.Name);
		}

		internal static string GetReasonForTypeNonBlittable(Type t)
		{
			return UnsafeUtility.GetReasonForTypeNonBlittableImpl(t, t.Name);
		}

		internal static string GetReasonForValueTypeNonBlittable<T>() where T : struct
		{
			Type typeFromHandle = typeof(T);
			return UnsafeUtility.GetReasonForTypeNonBlittableImpl(typeFromHandle, typeFromHandle.Name);
		}

		public static bool IsUnmanaged<T>()
		{
			int num = UnsafeUtility.IsUnmanagedCache<T>.value;
			bool flag = num == 1;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = num == 0;
				if (flag2)
				{
					num = (UnsafeUtility.IsUnmanagedCache<T>.value = (UnsafeUtility.IsUnmanaged(typeof(T)) ? 1 : -1));
				}
				result = (num == 1);
			}
			return result;
		}

		public static bool IsValidNativeContainerElementType<T>()
		{
			int num = UnsafeUtility.IsValidNativeContainerElementTypeCache<T>.value;
			bool flag = num == -1;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = num == 0;
				if (flag2)
				{
					num = (UnsafeUtility.IsValidNativeContainerElementTypeCache<T>.value = (UnsafeUtility.IsValidNativeContainerElementType(typeof(T)) ? 1 : -1));
				}
				result = (num == 1);
			}
			return result;
		}

		public static int AlignOf<T>() where T : struct
		{
			return UnsafeUtility.SizeOf<UnsafeUtility.AlignOfHelper<T>>() - UnsafeUtility.SizeOf<T>();
		}

		[MethodImpl((MethodImplOptions)256)]
		public unsafe static void CopyPtrToStructure<T>(void* ptr, out T output) where T : struct
		{
			bool flag = ptr == null;
			if (flag)
			{
				throw new ArgumentNullException();
			}
			UnsafeUtility.InternalCopyPtrToStructure<T>(ptr, out output);
		}

		private unsafe static void InternalCopyPtrToStructure<T>(void* ptr, out T output) where T : struct
		{
			output = *(T*)ptr;
		}

		[MethodImpl((MethodImplOptions)256)]
		public unsafe static void CopyStructureToPtr<T>(ref T input, void* ptr) where T : struct
		{
			bool flag = ptr == null;
			if (flag)
			{
				throw new ArgumentNullException();
			}
			UnsafeUtility.InternalCopyStructureToPtr<T>(ref input, ptr);
		}

		private unsafe static void InternalCopyStructureToPtr<T>(ref T input, void* ptr) where T : struct
		{
			*(T*)ptr = input;
		}

		public unsafe static T ReadArrayElement<T>(void* source, int index)
		{
			return *(T*)((byte*)source + (long)index * (long)sizeof(T));
		}

		public unsafe static T ReadArrayElementWithStride<T>(void* source, int index, int stride)
		{
			return *(T*)((byte*)source + (long)index * (long)stride);
		}

		public unsafe static void WriteArrayElement<T>(void* destination, int index, T value)
		{
			*(T*)((byte*)destination + (long)index * (long)sizeof(T)) = value;
		}

		public unsafe static void WriteArrayElementWithStride<T>(void* destination, int index, int stride, T value)
		{
			*(T*)((byte*)destination + (long)index * (long)stride) = value;
		}

		public unsafe static void* AddressOf<T>(ref T output) where T : struct
		{
			return (void*)(&output);
		}

		public static int SizeOf<T>() where T : struct
		{
			return sizeof(T);
		}

		public unsafe static T* As<U, T>(ref U from)
		{
			return ref from;
		}

		public unsafe static T* AsRef<T>(void* ptr) where T : struct
		{
			return ref *(T*)ptr;
		}

		public unsafe static T* ArrayElementAsRef<T>(void* ptr, int index) where T : struct
		{
			return ref *(T*)((byte*)ptr + (long)index * (long)sizeof(T));
		}

		public static int EnumToInt<T>(T enumValue) where T : struct, IConvertible
		{
			int result = 0;
			UnsafeUtility.InternalEnumToInt<T>(ref enumValue, ref result);
			return result;
		}

		private static void InternalEnumToInt<T>(ref T enumValue, ref int intValue)
		{
			intValue = enumValue;
		}

		public static bool EnumEquals<T>(T lhs, T rhs) where T : struct, IConvertible
		{
			return lhs == rhs;
		}
	}
}
