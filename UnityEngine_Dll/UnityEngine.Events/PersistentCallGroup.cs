using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	[Serializable]
	internal class PersistentCallGroup
	{
		[FormerlySerializedAs("m_Listeners"), SerializeField]
		private List<PersistentCall> m_Calls;

		public int Count
		{
			get
			{
				return this.m_Calls.Count;
			}
		}

		public PersistentCallGroup()
		{
			this.m_Calls = new List<PersistentCall>();
		}

		public PersistentCall GetListener(int index)
		{
			return this.m_Calls[index];
		}

		public IEnumerable<PersistentCall> GetListeners()
		{
			return this.m_Calls;
		}

		public void AddListener()
		{
			this.m_Calls.Add(new PersistentCall());
		}

		public void AddListener(PersistentCall call)
		{
			this.m_Calls.Add(call);
		}

		public void RemoveListener(int index)
		{
			this.m_Calls.RemoveAt(index);
		}

		public void Clear()
		{
			this.m_Calls.Clear();
		}

		public void RegisterEventPersistentListener(int index, UnityEngine.Object targetObj, Type targetObjType, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, targetObjType, methodName);
			listener.mode = PersistentListenerMode.EventDefined;
		}

		public void RegisterVoidPersistentListener(int index, UnityEngine.Object targetObj, Type targetObjType, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, targetObjType, methodName);
			listener.mode = PersistentListenerMode.Void;
		}

		public void RegisterObjectPersistentListener(int index, UnityEngine.Object targetObj, Type targetObjType, UnityEngine.Object argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, targetObjType, methodName);
			listener.mode = PersistentListenerMode.Object;
			listener.arguments.unityObjectArgument = argument;
		}

		public void RegisterIntPersistentListener(int index, UnityEngine.Object targetObj, Type targetObjType, int argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, targetObjType, methodName);
			listener.mode = PersistentListenerMode.Int;
			listener.arguments.intArgument = argument;
		}

		public void RegisterFloatPersistentListener(int index, UnityEngine.Object targetObj, Type targetObjType, float argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, targetObjType, methodName);
			listener.mode = PersistentListenerMode.Float;
			listener.arguments.floatArgument = argument;
		}

		public void RegisterStringPersistentListener(int index, UnityEngine.Object targetObj, Type targetObjType, string argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, targetObjType, methodName);
			listener.mode = PersistentListenerMode.String;
			listener.arguments.stringArgument = argument;
		}

		public void RegisterBoolPersistentListener(int index, UnityEngine.Object targetObj, Type targetObjType, bool argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, targetObjType, methodName);
			listener.mode = PersistentListenerMode.Bool;
			listener.arguments.boolArgument = argument;
		}

		public void UnregisterPersistentListener(int index)
		{
			PersistentCall listener = this.GetListener(index);
			listener.UnregisterPersistentListener();
		}

		public void RemoveListeners(UnityEngine.Object target, string methodName)
		{
			List<PersistentCall> list = new List<PersistentCall>();
			for (int i = 0; i < this.m_Calls.Count; i++)
			{
				bool flag = this.m_Calls[i].target == target && this.m_Calls[i].methodName == methodName;
				if (flag)
				{
					list.Add(this.m_Calls[i]);
				}
			}
			this.m_Calls.RemoveAll(new Predicate<PersistentCall>(list.Contains));
		}

		public void Initialize(InvokableCallList invokableList, UnityEventBase unityEventBase)
		{
			foreach (PersistentCall current in this.m_Calls)
			{
				bool flag = !current.IsValid();
				if (!flag)
				{
					BaseInvokableCall runtimeCall = current.GetRuntimeCall(unityEventBase);
					bool flag2 = runtimeCall != null;
					if (flag2)
					{
						invokableList.AddPersistentInvokableCall(runtimeCall);
					}
				}
			}
		}
	}
}
