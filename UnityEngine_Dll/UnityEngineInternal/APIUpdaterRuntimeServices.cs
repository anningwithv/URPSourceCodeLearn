using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityEngineInternal
{
	public sealed class APIUpdaterRuntimeServices
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly APIUpdaterRuntimeServices.<>c <>9 = new APIUpdaterRuntimeServices.<>c();

			public static Func<Assembly, IEnumerable<Type>> <>9__1_1;

			internal IEnumerable<Type> <ResolveType>b__1_1(Assembly a)
			{
				return a.GetTypes();
			}
		}

		private static IList<Type> ComponentsFromUnityEngine;

		[Obsolete("AddComponent(string) has been deprecated. Use GameObject.AddComponent<T>() / GameObject.AddComponent(Type) instead.\nAPI Updater could not automatically update the original call to AddComponent(string name), because it was unable to resolve the type specified in parameter 'name'.\nInstead, this call has been replaced with a call to APIUpdaterRuntimeServices.AddComponent() so you can try to test your game in the editor.\nIn order to be able to build the game, replace this call (APIUpdaterRuntimeServices.AddComponent()) with a call to GameObject.AddComponent<T>() / GameObject.AddComponent(Type).")]
		public static Component AddComponent(GameObject go, string sourceInfo, string name)
		{
			Debug.LogWarningFormat("Performing a potentially slow search for component {0}.", new object[]
			{
				name
			});
			Type type = APIUpdaterRuntimeServices.ResolveType(name, Assembly.GetCallingAssembly(), sourceInfo);
			return (type == null) ? null : go.AddComponent(type);
		}

		private static Type ResolveType(string name, Assembly callingAssembly, string sourceInfo)
		{
			Type type = APIUpdaterRuntimeServices.ComponentsFromUnityEngine.FirstOrDefault((Type t) => (t.Name == name || t.FullName == name) && !APIUpdaterRuntimeServices.IsMarkedAsObsolete(t));
			bool flag = type != null;
			Type result;
			if (flag)
			{
				Debug.LogWarningFormat("[{1}] Component type '{0}' found in UnityEngine, consider replacing with go.AddComponent<{0}>()", new object[]
				{
					name,
					sourceInfo
				});
				result = type;
			}
			else
			{
				Type type2 = callingAssembly.GetType(name);
				bool flag2 = type2 != null;
				if (flag2)
				{
					Debug.LogWarningFormat("[{1}] Component type '{0}' found on caller assembly, consider replacing with go.AddComponent<{0}>()", new object[]
					{
						name,
						sourceInfo
					});
					result = type2;
				}
				else
				{
					IEnumerable<Assembly> arg_BB_0 = AppDomain.CurrentDomain.GetAssemblies();
					Func<Assembly, IEnumerable<Type>> arg_BB_1;
					if ((arg_BB_1 = APIUpdaterRuntimeServices.<>c.<>9__1_1) == null)
					{
						arg_BB_1 = (APIUpdaterRuntimeServices.<>c.<>9__1_1 = new Func<Assembly, IEnumerable<Type>>(APIUpdaterRuntimeServices.<>c.<>9.<ResolveType>b__1_1));
					}
					type2 = arg_BB_0.SelectMany(arg_BB_1).SingleOrDefault((Type t) => (t.Name == name || t.FullName == name) && typeof(Component).IsAssignableFrom(t));
					bool flag3 = type2 != null;
					if (flag3)
					{
						Debug.LogWarningFormat("[{2}] Component type '{0}' found on assembly {1}, consider replacing with go.AddComponent<{0}>()", new object[]
						{
							name,
							new AssemblyName(type2.Assembly.FullName).Name,
							sourceInfo
						});
						result = type2;
					}
					else
					{
						Debug.LogErrorFormat("[{1}] Component Type '{0}' not found.", new object[]
						{
							name,
							sourceInfo
						});
						result = null;
					}
				}
			}
			return result;
		}

		private static bool IsMarkedAsObsolete(Type t)
		{
			return t.GetCustomAttributes(typeof(ObsoleteAttribute), false).Any<object>();
		}

		static APIUpdaterRuntimeServices()
		{
			Type typeFromHandle = typeof(Component);
			APIUpdaterRuntimeServices.ComponentsFromUnityEngine = typeFromHandle.Assembly.GetTypes().Where(new Func<Type, bool>(typeFromHandle.IsAssignableFrom)).ToList<Type>();
		}
	}
}
