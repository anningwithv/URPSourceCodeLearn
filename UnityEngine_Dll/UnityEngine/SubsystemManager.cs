using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.SubsystemsImplementation;

namespace UnityEngine
{
	[NativeHeader("Modules/Subsystems/SubsystemManager.h")]
	public static class SubsystemManager
	{
		private static List<IntegratedSubsystem> s_IntegratedSubsystems;

		private static List<SubsystemWithProvider> s_StandaloneSubsystems;

		private static List<Subsystem> s_DeprecatedSubsystems;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action beforeReloadSubsystems;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action afterReloadSubsystems;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action reloadSubsytemsStarted;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action reloadSubsytemsCompleted;

		[RequiredByNativeCode]
		private static void ReloadSubsystemsStarted()
		{
			bool flag = SubsystemManager.reloadSubsytemsStarted != null;
			if (flag)
			{
				SubsystemManager.reloadSubsytemsStarted();
			}
			bool flag2 = SubsystemManager.beforeReloadSubsystems != null;
			if (flag2)
			{
				SubsystemManager.beforeReloadSubsystems();
			}
		}

		[RequiredByNativeCode]
		private static void ReloadSubsystemsCompleted()
		{
			bool flag = SubsystemManager.reloadSubsytemsCompleted != null;
			if (flag)
			{
				SubsystemManager.reloadSubsytemsCompleted();
			}
			bool flag2 = SubsystemManager.afterReloadSubsystems != null;
			if (flag2)
			{
				SubsystemManager.afterReloadSubsystems();
			}
		}

		[RequiredByNativeCode]
		private static void InitializeIntegratedSubsystem(IntPtr ptr, IntegratedSubsystem subsystem)
		{
			subsystem.m_Ptr = ptr;
			subsystem.SetHandle(subsystem);
			SubsystemManager.s_IntegratedSubsystems.Add(subsystem);
		}

		[RequiredByNativeCode]
		private static void ClearSubsystems()
		{
			foreach (IntegratedSubsystem current in SubsystemManager.s_IntegratedSubsystems)
			{
				current.m_Ptr = IntPtr.Zero;
			}
			SubsystemManager.s_IntegratedSubsystems.Clear();
			SubsystemManager.s_StandaloneSubsystems.Clear();
			SubsystemManager.s_DeprecatedSubsystems.Clear();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StaticConstructScriptingClassMap();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ReportSingleSubsystemAnalytics(string id);

		static SubsystemManager()
		{
			SubsystemManager.s_IntegratedSubsystems = new List<IntegratedSubsystem>();
			SubsystemManager.s_StandaloneSubsystems = new List<SubsystemWithProvider>();
			SubsystemManager.s_DeprecatedSubsystems = new List<Subsystem>();
			SubsystemManager.StaticConstructScriptingClassMap();
		}

		public static void GetAllSubsystemDescriptors(List<ISubsystemDescriptor> descriptors)
		{
			SubsystemDescriptorStore.GetAllSubsystemDescriptors(descriptors);
		}

		public static void GetSubsystemDescriptors<T>(List<T> descriptors) where T : ISubsystemDescriptor
		{
			SubsystemDescriptorStore.GetSubsystemDescriptors<T>(descriptors);
		}

		public static void GetSubsystems<T>(List<T> subsystems) where T : ISubsystem
		{
			subsystems.Clear();
			SubsystemManager.AddSubsystemSubset<IntegratedSubsystem, T>(SubsystemManager.s_IntegratedSubsystems, subsystems);
			SubsystemManager.AddSubsystemSubset<SubsystemWithProvider, T>(SubsystemManager.s_StandaloneSubsystems, subsystems);
			SubsystemManager.AddSubsystemSubset<Subsystem, T>(SubsystemManager.s_DeprecatedSubsystems, subsystems);
		}

		private static void AddSubsystemSubset<TBaseTypeInList, TQueryType>(List<TBaseTypeInList> copyFrom, List<TQueryType> copyTo) where TBaseTypeInList : ISubsystem where TQueryType : ISubsystem
		{
			foreach (TBaseTypeInList current in copyFrom)
			{
				TQueryType item;
				bool arg_36_0;
				if (current is TQueryType)
				{
					item = (current as TQueryType);
					arg_36_0 = true;
				}
				else
				{
					arg_36_0 = false;
				}
				bool flag = arg_36_0;
				if (flag)
				{
					copyTo.Add(item);
				}
			}
		}

		internal static IntegratedSubsystem GetIntegratedSubsystemByPtr(IntPtr ptr)
		{
			IntegratedSubsystem result;
			foreach (IntegratedSubsystem current in SubsystemManager.s_IntegratedSubsystems)
			{
				bool flag = current.m_Ptr == ptr;
				if (flag)
				{
					result = current;
					return result;
				}
			}
			result = null;
			return result;
		}

		internal static void RemoveIntegratedSubsystemByPtr(IntPtr ptr)
		{
			for (int i = 0; i < SubsystemManager.s_IntegratedSubsystems.Count; i++)
			{
				bool flag = SubsystemManager.s_IntegratedSubsystems[i].m_Ptr != ptr;
				if (!flag)
				{
					SubsystemManager.s_IntegratedSubsystems[i].m_Ptr = IntPtr.Zero;
					SubsystemManager.s_IntegratedSubsystems.RemoveAt(i);
					break;
				}
			}
		}

		internal static void AddStandaloneSubsystem(SubsystemWithProvider subsystem)
		{
			SubsystemManager.s_StandaloneSubsystems.Add(subsystem);
		}

		internal static bool RemoveStandaloneSubsystem(SubsystemWithProvider subsystem)
		{
			return SubsystemManager.s_StandaloneSubsystems.Remove(subsystem);
		}

		internal static SubsystemWithProvider FindStandaloneSubsystemByDescriptor(SubsystemDescriptorWithProvider descriptor)
		{
			SubsystemWithProvider result;
			foreach (SubsystemWithProvider current in SubsystemManager.s_StandaloneSubsystems)
			{
				bool flag = current.descriptor == descriptor;
				if (flag)
				{
					result = current;
					return result;
				}
			}
			result = null;
			return result;
		}

		public static void GetInstances<T>(List<T> subsystems) where T : ISubsystem
		{
			SubsystemManager.GetSubsystems<T>(subsystems);
		}

		internal static void AddDeprecatedSubsystem(Subsystem subsystem)
		{
			SubsystemManager.s_DeprecatedSubsystems.Add(subsystem);
		}

		internal static bool RemoveDeprecatedSubsystem(Subsystem subsystem)
		{
			return SubsystemManager.s_DeprecatedSubsystems.Remove(subsystem);
		}

		internal static Subsystem FindDeprecatedSubsystemByDescriptor(SubsystemDescriptor descriptor)
		{
			Subsystem result;
			foreach (Subsystem current in SubsystemManager.s_DeprecatedSubsystems)
			{
				bool flag = current.m_SubsystemDescriptor == descriptor;
				if (flag)
				{
					result = current;
					return result;
				}
			}
			result = null;
			return result;
		}
	}
}
