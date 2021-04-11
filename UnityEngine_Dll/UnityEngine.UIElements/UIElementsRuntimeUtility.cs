using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	internal static class UIElementsRuntimeUtility
	{
		public delegate BaseRuntimePanel CreateRuntimePanelDelegate(ScriptableObject ownerObject);

		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly UIElementsRuntimeUtility.<>c <>9 = new UIElementsRuntimeUtility.<>c();

			public static Comparison<Panel> <>9__24_0;

			internal int <SortPanels>b__24_0(Panel a, Panel b)
			{
				float num = a.sortingPriority - b.sortingPriority;
				bool flag = Mathf.Approximately(0f, num);
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					result = ((num < 0f) ? -1 : 1);
				}
				return result;
			}
		}

		private static bool s_RegisteredPlayerloopCallback;

		private static List<Panel> s_SortedRuntimePanels;

		private static bool s_PanelOrderingDirty;

		internal static readonly string s_RepaintProfilerMarkerName;

		private static readonly ProfilerMarker s_RepaintProfilerMarker;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		private static event Action s_onRepaintOverlayPanels;

		internal static event Action onRepaintOverlayPanels
		{
			add
			{
				bool flag = UIElementsRuntimeUtility.s_onRepaintOverlayPanels == null;
				if (flag)
				{
					UIElementsRuntimeUtility.RegisterPlayerloopCallback();
				}
				UIElementsRuntimeUtility.s_onRepaintOverlayPanels += value;
			}
			remove
			{
				UIElementsRuntimeUtility.s_onRepaintOverlayPanels -= value;
				bool flag = UIElementsRuntimeUtility.s_onRepaintOverlayPanels == null;
				if (flag)
				{
					UIElementsRuntimeUtility.UnregisterPlayerloopCallback();
				}
			}
		}

		static UIElementsRuntimeUtility()
		{
			UIElementsRuntimeUtility.s_RegisteredPlayerloopCallback = false;
			UIElementsRuntimeUtility.s_SortedRuntimePanels = new List<Panel>();
			UIElementsRuntimeUtility.s_PanelOrderingDirty = true;
			UIElementsRuntimeUtility.s_RepaintProfilerMarkerName = "UIElementsRuntimeUtility.DoDispatch(Repaint Event)";
			UIElementsRuntimeUtility.s_RepaintProfilerMarker = new ProfilerMarker(UIElementsRuntimeUtility.s_RepaintProfilerMarkerName);
			UIElementsRuntimeUtilityNative.RepaintOverlayPanelsCallback = new Action(UIElementsRuntimeUtility.RepaintOverlayPanels);
		}

		public static EventBase CreateEvent(Event systemEvent)
		{
			return UIElementsUtility.CreateEvent(systemEvent, systemEvent.rawType);
		}

		public static BaseRuntimePanel FindOrCreateRuntimePanel(ScriptableObject ownerObject, UIElementsRuntimeUtility.CreateRuntimePanelDelegate createDelegate)
		{
			Panel panel;
			bool flag = UIElementsUtility.TryGetPanel(ownerObject.GetInstanceID(), out panel);
			BaseRuntimePanel result;
			if (flag)
			{
				BaseRuntimePanel baseRuntimePanel = panel as BaseRuntimePanel;
				bool flag2 = baseRuntimePanel != null;
				if (flag2)
				{
					result = baseRuntimePanel;
					return result;
				}
				UIElementsRuntimeUtility.RemoveCachedPanelInternal(ownerObject.GetInstanceID());
			}
			BaseRuntimePanel baseRuntimePanel2 = createDelegate(ownerObject);
			baseRuntimePanel2.IMGUIEventInterests = new EventInterests
			{
				wantsMouseMove = true,
				wantsMouseEnterLeaveWindow = true
			};
			UIElementsRuntimeUtility.RegisterCachedPanelInternal(ownerObject.GetInstanceID(), baseRuntimePanel2);
			result = baseRuntimePanel2;
			return result;
		}

		public static void DisposeRuntimePanel(ScriptableObject ownerObject)
		{
			Panel panel;
			bool flag = UIElementsUtility.TryGetPanel(ownerObject.GetInstanceID(), out panel);
			if (flag)
			{
				panel.Dispose();
				UIElementsRuntimeUtility.RemoveCachedPanelInternal(ownerObject.GetInstanceID());
			}
		}

		private static void RegisterCachedPanelInternal(int instanceID, IPanel panel)
		{
			UIElementsUtility.RegisterCachedPanel(instanceID, panel as Panel);
			UIElementsRuntimeUtility.s_PanelOrderingDirty = true;
			bool flag = !UIElementsRuntimeUtility.s_RegisteredPlayerloopCallback;
			if (flag)
			{
				UIElementsRuntimeUtility.s_RegisteredPlayerloopCallback = true;
				UIElementsRuntimeUtility.RegisterPlayerloopCallback();
			}
		}

		private static void RemoveCachedPanelInternal(int instanceID)
		{
			UIElementsUtility.RemoveCachedPanel(instanceID);
			UIElementsRuntimeUtility.s_PanelOrderingDirty = true;
			UIElementsRuntimeUtility.s_SortedRuntimePanels.Clear();
			UIElementsUtility.GetAllPanels(UIElementsRuntimeUtility.s_SortedRuntimePanels, ContextType.Player);
			bool flag = UIElementsRuntimeUtility.s_SortedRuntimePanels.Count == 0;
			if (flag)
			{
				UIElementsRuntimeUtility.s_RegisteredPlayerloopCallback = false;
				UIElementsRuntimeUtility.UnregisterPlayerloopCallback();
			}
		}

		public static void RepaintOverlayPanels()
		{
			using (List<Panel>.Enumerator enumerator = UIElementsRuntimeUtility.GetSortedPlayerPanels().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					BaseRuntimePanel baseRuntimePanel = (BaseRuntimePanel)enumerator.Current;
					bool flag = !baseRuntimePanel.drawToCameras && (baseRuntimePanel.targetTexture == null || baseRuntimePanel.isDirty);
					if (flag)
					{
						using (UIElementsRuntimeUtility.s_RepaintProfilerMarker.Auto())
						{
							baseRuntimePanel.Repaint(Event.current);
						}
						IPanelDebug expr_79 = baseRuntimePanel.panelDebug;
						Panel expr_8A = ((expr_79 != null) ? expr_79.debuggerOverlayPanel : null) as Panel;
						if (expr_8A != null)
						{
							expr_8A.Repaint(Event.current);
						}
					}
				}
			}
			bool flag2 = UIElementsRuntimeUtility.s_onRepaintOverlayPanels != null;
			if (flag2)
			{
				UIElementsRuntimeUtility.s_onRepaintOverlayPanels();
			}
		}

		public static void UpdateRuntimePanels()
		{
			using (List<Panel>.Enumerator enumerator = UIElementsRuntimeUtility.GetSortedPlayerPanels().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					BaseRuntimePanel baseRuntimePanel = (BaseRuntimePanel)enumerator.Current;
					baseRuntimePanel.Update();
				}
			}
		}

		public static void RegisterPlayerloopCallback()
		{
			UIElementsRuntimeUtilityNative.RegisterPlayerloopCallback();
		}

		public static void UnregisterPlayerloopCallback()
		{
			UIElementsRuntimeUtilityNative.UnregisterPlayerloopCallback();
		}

		internal static void SetPanelOrderingDirty()
		{
			UIElementsRuntimeUtility.s_PanelOrderingDirty = true;
		}

		internal static List<Panel> GetSortedPlayerPanels()
		{
			bool flag = UIElementsRuntimeUtility.s_PanelOrderingDirty;
			if (flag)
			{
				UIElementsRuntimeUtility.SortPanels();
			}
			return UIElementsRuntimeUtility.s_SortedRuntimePanels;
		}

		private static void SortPanels()
		{
			UIElementsRuntimeUtility.s_SortedRuntimePanels.Clear();
			UIElementsUtility.GetAllPanels(UIElementsRuntimeUtility.s_SortedRuntimePanels, ContextType.Player);
			List<Panel> arg_3C_0 = UIElementsRuntimeUtility.s_SortedRuntimePanels;
			Comparison<Panel> arg_3C_1;
			if ((arg_3C_1 = UIElementsRuntimeUtility.<>c.<>9__24_0) == null)
			{
				arg_3C_1 = (UIElementsRuntimeUtility.<>c.<>9__24_0 = new Comparison<Panel>(UIElementsRuntimeUtility.<>c.<>9.<SortPanels>b__24_0));
			}
			arg_3C_0.Sort(arg_3C_1);
			UIElementsRuntimeUtility.s_PanelOrderingDirty = false;
		}
	}
}
