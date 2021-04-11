using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnityEngine.UIElements
{
	internal static class GlobalCallbackRegistry
	{
		internal struct ListenerRecord
		{
			public int hashCode;

			public string name;

			public string fileName;

			public int lineNumber;
		}

		private static bool m_IsEventDebuggerConnected = false;

		internal static readonly Dictionary<CallbackEventHandler, Dictionary<Type, List<GlobalCallbackRegistry.ListenerRecord>>> s_Listeners = new Dictionary<CallbackEventHandler, Dictionary<Type, List<GlobalCallbackRegistry.ListenerRecord>>>();

		public static bool IsEventDebuggerConnected
		{
			get
			{
				return GlobalCallbackRegistry.m_IsEventDebuggerConnected;
			}
			set
			{
				bool flag = !value;
				if (flag)
				{
					GlobalCallbackRegistry.s_Listeners.Clear();
				}
				GlobalCallbackRegistry.m_IsEventDebuggerConnected = value;
			}
		}

		public static void RegisterListeners<TEventType>(CallbackEventHandler ceh, Delegate callback, TrickleDown useTrickleDown)
		{
			bool flag = !GlobalCallbackRegistry.IsEventDebuggerConnected;
			if (!flag)
			{
				Dictionary<Type, List<GlobalCallbackRegistry.ListenerRecord>> dictionary;
				bool flag2 = !GlobalCallbackRegistry.s_Listeners.TryGetValue(ceh, out dictionary);
				if (flag2)
				{
					dictionary = new Dictionary<Type, List<GlobalCallbackRegistry.ListenerRecord>>();
					GlobalCallbackRegistry.s_Listeners.Add(ceh, dictionary);
				}
				Type expr_4A = callback.Method.DeclaringType;
				string text = ((expr_4A != null) ? expr_4A.Name : null) ?? string.Empty;
				string displayName = (callback.Target as VisualElement).GetDisplayName(true);
				string name = string.Concat(new string[]
				{
					text,
					".",
					callback.Method.Name,
					" > ",
					useTrickleDown.ToString(),
					" [",
					displayName,
					"]"
				});
				List<GlobalCallbackRegistry.ListenerRecord> list;
				bool flag3 = !dictionary.TryGetValue(typeof(TEventType), out list);
				if (flag3)
				{
					list = new List<GlobalCallbackRegistry.ListenerRecord>();
					dictionary.Add(typeof(TEventType), list);
				}
				StackFrame stackFrame = new StackFrame(2, true);
				list.Add(new GlobalCallbackRegistry.ListenerRecord
				{
					hashCode = callback.GetHashCode(),
					name = name,
					fileName = stackFrame.GetFileName(),
					lineNumber = stackFrame.GetFileLineNumber()
				});
			}
		}

		public static void UnregisterListeners<TEventType>(CallbackEventHandler ceh, Delegate callback)
		{
			bool flag = !GlobalCallbackRegistry.IsEventDebuggerConnected;
			if (!flag)
			{
				Dictionary<Type, List<GlobalCallbackRegistry.ListenerRecord>> dictionary;
				bool flag2 = !GlobalCallbackRegistry.s_Listeners.TryGetValue(ceh, out dictionary);
				if (!flag2)
				{
					Type expr_3A = callback.Method.DeclaringType;
					string str = ((expr_3A != null) ? expr_3A.Name : null) ?? string.Empty;
					string b = str + "." + callback.Method.Name;
					List<GlobalCallbackRegistry.ListenerRecord> list;
					bool flag3 = !dictionary.TryGetValue(typeof(TEventType), out list);
					if (!flag3)
					{
						for (int i = list.Count - 1; i >= 0; i--)
						{
							GlobalCallbackRegistry.ListenerRecord listenerRecord = list[i];
							bool flag4 = listenerRecord.name == b;
							if (flag4)
							{
								list.RemoveAt(i);
							}
						}
					}
				}
			}
		}
	}
}
