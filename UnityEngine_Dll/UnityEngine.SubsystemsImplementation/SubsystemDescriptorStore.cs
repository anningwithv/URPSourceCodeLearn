using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.SubsystemsImplementation
{
	[NativeHeader("Modules/Subsystems/SubsystemManager.h")]
	public static class SubsystemDescriptorStore
	{
		private static List<IntegratedSubsystemDescriptor> s_IntegratedDescriptors = new List<IntegratedSubsystemDescriptor>();

		private static List<SubsystemDescriptorWithProvider> s_StandaloneDescriptors = new List<SubsystemDescriptorWithProvider>();

		private static List<SubsystemDescriptor> s_DeprecatedDescriptors = new List<SubsystemDescriptor>();

		[RequiredByNativeCode]
		internal static void InitializeManagedDescriptor(IntPtr ptr, IntegratedSubsystemDescriptor desc)
		{
			desc.m_Ptr = ptr;
			SubsystemDescriptorStore.s_IntegratedDescriptors.Add(desc);
		}

		[RequiredByNativeCode]
		internal static void ClearManagedDescriptors()
		{
			foreach (IntegratedSubsystemDescriptor current in SubsystemDescriptorStore.s_IntegratedDescriptors)
			{
				current.m_Ptr = IntPtr.Zero;
			}
			SubsystemDescriptorStore.s_IntegratedDescriptors.Clear();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReportSingleSubsystemAnalytics(string id);

		public static void RegisterDescriptor(SubsystemDescriptorWithProvider descriptor)
		{
			descriptor.ThrowIfInvalid();
			SubsystemDescriptorStore.RegisterDescriptor<SubsystemDescriptorWithProvider, SubsystemDescriptorWithProvider>(descriptor, SubsystemDescriptorStore.s_StandaloneDescriptors);
		}

		internal static void GetAllSubsystemDescriptors(List<ISubsystemDescriptor> descriptors)
		{
			descriptors.Clear();
			int num = SubsystemDescriptorStore.s_IntegratedDescriptors.Count + SubsystemDescriptorStore.s_StandaloneDescriptors.Count + SubsystemDescriptorStore.s_DeprecatedDescriptors.Count;
			bool flag = descriptors.Capacity < num;
			if (flag)
			{
				descriptors.Capacity = num;
			}
			SubsystemDescriptorStore.AddDescriptorSubset<IntegratedSubsystemDescriptor>(SubsystemDescriptorStore.s_IntegratedDescriptors, descriptors);
			SubsystemDescriptorStore.AddDescriptorSubset<SubsystemDescriptorWithProvider>(SubsystemDescriptorStore.s_StandaloneDescriptors, descriptors);
			SubsystemDescriptorStore.AddDescriptorSubset<SubsystemDescriptor>(SubsystemDescriptorStore.s_DeprecatedDescriptors, descriptors);
		}

		private static void AddDescriptorSubset<TBaseTypeInList>(List<TBaseTypeInList> copyFrom, List<ISubsystemDescriptor> copyTo) where TBaseTypeInList : ISubsystemDescriptor
		{
			foreach (TBaseTypeInList current in copyFrom)
			{
				copyTo.Add(current);
			}
		}

		internal static void GetSubsystemDescriptors<T>(List<T> descriptors) where T : ISubsystemDescriptor
		{
			descriptors.Clear();
			SubsystemDescriptorStore.AddDescriptorSubset<IntegratedSubsystemDescriptor, T>(SubsystemDescriptorStore.s_IntegratedDescriptors, descriptors);
			SubsystemDescriptorStore.AddDescriptorSubset<SubsystemDescriptorWithProvider, T>(SubsystemDescriptorStore.s_StandaloneDescriptors, descriptors);
			SubsystemDescriptorStore.AddDescriptorSubset<SubsystemDescriptor, T>(SubsystemDescriptorStore.s_DeprecatedDescriptors, descriptors);
		}

		private static void AddDescriptorSubset<TBaseTypeInList, TQueryType>(List<TBaseTypeInList> copyFrom, List<TQueryType> copyTo) where TBaseTypeInList : ISubsystemDescriptor where TQueryType : ISubsystemDescriptor
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

		internal static void RegisterDescriptor<TDescriptor, TBaseTypeInList>(TDescriptor descriptor, List<TBaseTypeInList> storeInList) where TDescriptor : TBaseTypeInList where TBaseTypeInList : ISubsystemDescriptor
		{
			for (int i = 0; i < storeInList.Count; i++)
			{
				TBaseTypeInList tBaseTypeInList = storeInList[i];
				bool flag = tBaseTypeInList.id != descriptor.id;
				if (!flag)
				{
					Debug.LogWarning(string.Format("Registering subsystem descriptor with duplicate ID '{descriptor.id}' - overwriting previous entry.", new object[0]));
					storeInList[i] = (TBaseTypeInList)((object)descriptor);
					return;
				}
			}
			SubsystemDescriptorStore.ReportSingleSubsystemAnalytics(descriptor.id);
			storeInList.Add((TBaseTypeInList)((object)descriptor));
		}

		internal static void RegisterDeprecatedDescriptor(SubsystemDescriptor descriptor)
		{
			SubsystemDescriptorStore.RegisterDescriptor<SubsystemDescriptor, SubsystemDescriptor>(descriptor, SubsystemDescriptorStore.s_DeprecatedDescriptors);
		}
	}
}
