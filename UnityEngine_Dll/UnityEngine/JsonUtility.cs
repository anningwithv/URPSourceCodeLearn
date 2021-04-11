using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/JSONSerialize/Public/JsonUtility.bindings.h")]
	public static class JsonUtility
	{
		[FreeFunction("ToJsonInternal", true), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string ToJsonInternal([NotNull("ArgumentNullException")] object obj, bool prettyPrint);

		[FreeFunction("FromJsonInternal", true, ThrowsException = true), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object FromJsonInternal(string json, object objectToOverwrite, Type type);

		public static string ToJson(object obj)
		{
			return JsonUtility.ToJson(obj, false);
		}

		public static string ToJson(object obj, bool prettyPrint)
		{
			bool flag = obj == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				bool flag2 = obj is Object && !(obj is MonoBehaviour) && !(obj is ScriptableObject);
				if (flag2)
				{
					throw new ArgumentException("JsonUtility.ToJson does not support engine types.");
				}
				result = JsonUtility.ToJsonInternal(obj, prettyPrint);
			}
			return result;
		}

		public static T FromJson<T>(string json)
		{
			return (T)((object)JsonUtility.FromJson(json, typeof(T)));
		}

		public static object FromJson(string json, Type type)
		{
			bool flag = string.IsNullOrEmpty(json);
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = type == null;
				if (flag2)
				{
					throw new ArgumentNullException("type");
				}
				bool flag3 = type.IsAbstract || type.IsSubclassOf(typeof(Object));
				if (flag3)
				{
					throw new ArgumentException("Cannot deserialize JSON to new instances of type '" + type.Name + ".'");
				}
				result = JsonUtility.FromJsonInternal(json, null, type);
			}
			return result;
		}

		public static void FromJsonOverwrite(string json, object objectToOverwrite)
		{
			bool flag = string.IsNullOrEmpty(json);
			if (!flag)
			{
				bool flag2 = objectToOverwrite == null;
				if (flag2)
				{
					throw new ArgumentNullException("objectToOverwrite");
				}
				bool flag3 = objectToOverwrite is Object && !(objectToOverwrite is MonoBehaviour) && !(objectToOverwrite is ScriptableObject);
				if (flag3)
				{
					throw new ArgumentException("Engine types cannot be overwritten from JSON outside of the Editor.");
				}
				JsonUtility.FromJsonInternal(json, objectToOverwrite, objectToOverwrite.GetType());
			}
		}
	}
}
