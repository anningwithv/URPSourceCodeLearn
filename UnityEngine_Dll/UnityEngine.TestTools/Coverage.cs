using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.TestTools
{
	[NativeType("Runtime/Scripting/ScriptingCoverage.h"), NativeClass("ScriptingCoverage")]
	public static class Coverage
	{
		public static extern bool enabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[FreeFunction("ScriptingCoverageGetCoverageForMethodInfoObject", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CoveredSequencePoint[] GetSequencePointsFor_Internal(MethodBase method);

		[FreeFunction("ScriptingCoverageResetForMethodInfoObject", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetFor_Internal(MethodBase method);

		[FreeFunction("ScriptingCoverageGetStatsForMethodInfoObject", ThrowsException = true)]
		private static CoveredMethodStats GetStatsFor_Internal(MethodBase method)
		{
			CoveredMethodStats result;
			Coverage.GetStatsFor_Internal_Injected(method, out result);
			return result;
		}

		public static CoveredSequencePoint[] GetSequencePointsFor(MethodBase method)
		{
			bool flag = method == null;
			if (flag)
			{
				throw new ArgumentNullException("method");
			}
			return Coverage.GetSequencePointsFor_Internal(method);
		}

		public static CoveredMethodStats GetStatsFor(MethodBase method)
		{
			bool flag = method == null;
			if (flag)
			{
				throw new ArgumentNullException("method");
			}
			return Coverage.GetStatsFor_Internal(method);
		}

		public static CoveredMethodStats[] GetStatsFor(MethodBase[] methods)
		{
			bool flag = methods == null;
			if (flag)
			{
				throw new ArgumentNullException("methods");
			}
			CoveredMethodStats[] array = new CoveredMethodStats[methods.Length];
			for (int i = 0; i < methods.Length; i++)
			{
				array[i] = Coverage.GetStatsFor(methods[i]);
			}
			return array;
		}

		public static CoveredMethodStats[] GetStatsFor(Type type)
		{
			bool flag = type == null;
			if (flag)
			{
				throw new ArgumentNullException("type");
			}
			return Coverage.GetStatsFor(type.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).OfType<MethodBase>().ToArray<MethodBase>());
		}

		[FreeFunction("ScriptingCoverageGetStatsForAllCoveredMethodsFromScripting", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern CoveredMethodStats[] GetStatsForAllCoveredMethods();

		public static void ResetFor(MethodBase method)
		{
			bool flag = method == null;
			if (flag)
			{
				throw new ArgumentNullException("method");
			}
			Coverage.ResetFor_Internal(method);
		}

		[FreeFunction("ScriptingCoverageResetAllFromScripting", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ResetAll();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetStatsFor_Internal_Injected(MethodBase method, out CoveredMethodStats ret);
	}
}
