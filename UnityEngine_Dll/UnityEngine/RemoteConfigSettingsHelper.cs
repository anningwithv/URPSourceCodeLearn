using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	internal static class RemoteConfigSettingsHelper
	{
		[RequiredByNativeCode]
		internal enum Tag
		{
			kUnknown,
			kIntVal,
			kInt64Val,
			kUInt64Val,
			kDoubleVal,
			kBoolVal,
			kStringVal,
			kArrayVal,
			kMixedArrayVal,
			kMapVal,
			kMaxTags
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetSafeMap(IntPtr m, string key);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string[] GetSafeMapKeys(IntPtr m);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RemoteConfigSettingsHelper.Tag[] GetSafeMapTypes(IntPtr m);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long GetSafeNumber(IntPtr m, string key, long defaultValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern float GetSafeFloat(IntPtr m, string key, float defaultValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool GetSafeBool(IntPtr m, string key, bool defaultValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetSafeStringValue(IntPtr m, string key, string defaultValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetSafeArray(IntPtr m, string key);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long GetSafeArraySize(IntPtr a);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetSafeArrayArray(IntPtr a, long i);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetSafeArrayMap(IntPtr a, long i);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RemoteConfigSettingsHelper.Tag GetSafeArrayType(IntPtr a, long i);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long GetSafeNumberArray(IntPtr a, long i);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern float GetSafeArrayFloat(IntPtr a, long i);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool GetSafeArrayBool(IntPtr a, long i);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetSafeArrayStringValue(IntPtr a, long i);

		public static IDictionary<string, object> GetDictionary(IntPtr m, string key)
		{
			bool flag = m == IntPtr.Zero;
			IDictionary<string, object> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = !string.IsNullOrEmpty(key);
				if (flag2)
				{
					m = RemoteConfigSettingsHelper.GetSafeMap(m, key);
					bool flag3 = m == IntPtr.Zero;
					if (flag3)
					{
						result = null;
						return result;
					}
				}
				result = RemoteConfigSettingsHelper.GetDictionary(m);
			}
			return result;
		}

		internal static IDictionary<string, object> GetDictionary(IntPtr m)
		{
			bool flag = m == IntPtr.Zero;
			IDictionary<string, object> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				RemoteConfigSettingsHelper.Tag[] safeMapTypes = RemoteConfigSettingsHelper.GetSafeMapTypes(m);
				string[] safeMapKeys = RemoteConfigSettingsHelper.GetSafeMapKeys(m);
				for (int i = 0; i < safeMapKeys.Length; i++)
				{
					RemoteConfigSettingsHelper.SetDictKeyType(m, dictionary, safeMapKeys[i], safeMapTypes[i]);
				}
				result = dictionary;
			}
			return result;
		}

		internal static object GetArrayArrayEntries(IntPtr a, long i)
		{
			return RemoteConfigSettingsHelper.GetArrayEntries(RemoteConfigSettingsHelper.GetSafeArrayArray(a, i));
		}

		internal static IDictionary<string, object> GetArrayMapEntries(IntPtr a, long i)
		{
			return RemoteConfigSettingsHelper.GetDictionary(RemoteConfigSettingsHelper.GetSafeArrayMap(a, i));
		}

		internal static T[] GetArrayEntriesType<T>(IntPtr a, long size, Func<IntPtr, long, T> f)
		{
			T[] array = new T[size];
			for (long num = 0L; num < size; num += 1L)
			{
				array[(int)(checked((IntPtr)num))] = f(a, num);
			}
			return array;
		}

		internal static object GetArrayEntries(IntPtr a)
		{
			long safeArraySize = RemoteConfigSettingsHelper.GetSafeArraySize(a);
			bool flag = safeArraySize == 0L;
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				switch (RemoteConfigSettingsHelper.GetSafeArrayType(a, 0L))
				{
				case RemoteConfigSettingsHelper.Tag.kIntVal:
				case RemoteConfigSettingsHelper.Tag.kInt64Val:
					result = RemoteConfigSettingsHelper.GetArrayEntriesType<long>(a, safeArraySize, new Func<IntPtr, long, long>(RemoteConfigSettingsHelper.GetSafeNumberArray));
					return result;
				case RemoteConfigSettingsHelper.Tag.kDoubleVal:
					result = RemoteConfigSettingsHelper.GetArrayEntriesType<float>(a, safeArraySize, new Func<IntPtr, long, float>(RemoteConfigSettingsHelper.GetSafeArrayFloat));
					return result;
				case RemoteConfigSettingsHelper.Tag.kBoolVal:
					result = RemoteConfigSettingsHelper.GetArrayEntriesType<bool>(a, safeArraySize, new Func<IntPtr, long, bool>(RemoteConfigSettingsHelper.GetSafeArrayBool));
					return result;
				case RemoteConfigSettingsHelper.Tag.kStringVal:
					result = RemoteConfigSettingsHelper.GetArrayEntriesType<string>(a, safeArraySize, new Func<IntPtr, long, string>(RemoteConfigSettingsHelper.GetSafeArrayStringValue));
					return result;
				case RemoteConfigSettingsHelper.Tag.kArrayVal:
					result = RemoteConfigSettingsHelper.GetArrayEntriesType<object>(a, safeArraySize, new Func<IntPtr, long, object>(RemoteConfigSettingsHelper.GetArrayArrayEntries));
					return result;
				case RemoteConfigSettingsHelper.Tag.kMapVal:
					result = RemoteConfigSettingsHelper.GetArrayEntriesType<IDictionary<string, object>>(a, safeArraySize, new Func<IntPtr, long, IDictionary<string, object>>(RemoteConfigSettingsHelper.GetArrayMapEntries));
					return result;
				}
				result = null;
			}
			return result;
		}

		internal static object GetMixedArrayEntries(IntPtr a)
		{
			long safeArraySize = RemoteConfigSettingsHelper.GetSafeArraySize(a);
			bool flag = safeArraySize == 0L;
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				object[] array = new object[safeArraySize];
				for (long num = 0L; num < safeArraySize; num += 1L)
				{
					checked
					{
						switch (RemoteConfigSettingsHelper.GetSafeArrayType(a, num))
						{
						case RemoteConfigSettingsHelper.Tag.kIntVal:
						case RemoteConfigSettingsHelper.Tag.kInt64Val:
							array[(int)((IntPtr)num)] = RemoteConfigSettingsHelper.GetSafeNumberArray(a, num);
							break;
						case RemoteConfigSettingsHelper.Tag.kDoubleVal:
							array[(int)((IntPtr)num)] = RemoteConfigSettingsHelper.GetSafeArrayFloat(a, num);
							break;
						case RemoteConfigSettingsHelper.Tag.kBoolVal:
							array[(int)((IntPtr)num)] = RemoteConfigSettingsHelper.GetSafeArrayBool(a, num);
							break;
						case RemoteConfigSettingsHelper.Tag.kStringVal:
							array[(int)((IntPtr)num)] = RemoteConfigSettingsHelper.GetSafeArrayStringValue(a, num);
							break;
						case RemoteConfigSettingsHelper.Tag.kArrayVal:
							array[(int)((IntPtr)num)] = RemoteConfigSettingsHelper.GetArrayArrayEntries(a, num);
							break;
						case RemoteConfigSettingsHelper.Tag.kMapVal:
							array[(int)((IntPtr)num)] = RemoteConfigSettingsHelper.GetArrayMapEntries(a, num);
							break;
						}
					}
				}
				result = array;
			}
			return result;
		}

		internal static void SetDictKeyType(IntPtr m, IDictionary<string, object> dict, string key, RemoteConfigSettingsHelper.Tag tag)
		{
			switch (tag)
			{
			case RemoteConfigSettingsHelper.Tag.kIntVal:
			case RemoteConfigSettingsHelper.Tag.kInt64Val:
				dict[key] = RemoteConfigSettingsHelper.GetSafeNumber(m, key, 0L);
				break;
			case RemoteConfigSettingsHelper.Tag.kDoubleVal:
				dict[key] = RemoteConfigSettingsHelper.GetSafeFloat(m, key, 0f);
				break;
			case RemoteConfigSettingsHelper.Tag.kBoolVal:
				dict[key] = RemoteConfigSettingsHelper.GetSafeBool(m, key, false);
				break;
			case RemoteConfigSettingsHelper.Tag.kStringVal:
				dict[key] = RemoteConfigSettingsHelper.GetSafeStringValue(m, key, "");
				break;
			case RemoteConfigSettingsHelper.Tag.kArrayVal:
				dict[key] = RemoteConfigSettingsHelper.GetArrayEntries(RemoteConfigSettingsHelper.GetSafeArray(m, key));
				break;
			case RemoteConfigSettingsHelper.Tag.kMixedArrayVal:
				dict[key] = RemoteConfigSettingsHelper.GetMixedArrayEntries(RemoteConfigSettingsHelper.GetSafeArray(m, key));
				break;
			case RemoteConfigSettingsHelper.Tag.kMapVal:
				dict[key] = RemoteConfigSettingsHelper.GetDictionary(RemoteConfigSettingsHelper.GetSafeMap(m, key));
				break;
			}
		}
	}
}
