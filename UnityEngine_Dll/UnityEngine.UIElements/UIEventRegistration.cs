using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	internal static class UIEventRegistration
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly UIEventRegistration.<>c <>9 = new UIEventRegistration.<>c();

			internal void cctor>b__1_0()
			{
				UIEventRegistration.TakeCapture();
			}

			internal void cctor>b__1_1()
			{
				UIEventRegistration.ReleaseCapture();
			}

			internal bool cctor>b__1_2(int i, IntPtr ptr)
			{
				return UIEventRegistration.ProcessEvent(i, ptr);
			}

			internal void cctor>b__1_3()
			{
				UIEventRegistration.CleanupRoots();
			}

			internal bool cctor>b__1_4(Exception exception)
			{
				return UIEventRegistration.EndContainerGUIFromException(exception);
			}

			internal void cctor>b__1_5()
			{
				UIEventRegistration.MakeCurrentIMGUIContainerDirty();
			}
		}

		private static List<IUIElementsUtility> s_Utilities;

		static UIEventRegistration()
		{
			UIEventRegistration.s_Utilities = new List<IUIElementsUtility>();
			GUIUtility.takeCapture = (Action)Delegate.Combine(GUIUtility.takeCapture, new Action(UIEventRegistration.<>c.<>9.<.cctor>b__1_0));
			GUIUtility.releaseCapture = (Action)Delegate.Combine(GUIUtility.releaseCapture, new Action(UIEventRegistration.<>c.<>9.<.cctor>b__1_1));
			GUIUtility.processEvent = (Func<int, IntPtr, bool>)Delegate.Combine(GUIUtility.processEvent, new Func<int, IntPtr, bool>(UIEventRegistration.<>c.<>9.<.cctor>b__1_2));
			GUIUtility.cleanupRoots = (Action)Delegate.Combine(GUIUtility.cleanupRoots, new Action(UIEventRegistration.<>c.<>9.<.cctor>b__1_3));
			GUIUtility.endContainerGUIFromException = (Func<Exception, bool>)Delegate.Combine(GUIUtility.endContainerGUIFromException, new Func<Exception, bool>(UIEventRegistration.<>c.<>9.<.cctor>b__1_4));
			GUIUtility.guiChanged = (Action)Delegate.Combine(GUIUtility.guiChanged, new Action(UIEventRegistration.<>c.<>9.<.cctor>b__1_5));
		}

		internal static void RegisterUIElementSystem(IUIElementsUtility utility)
		{
			UIEventRegistration.s_Utilities.Insert(0, utility);
		}

		private static void TakeCapture()
		{
			foreach (IUIElementsUtility current in UIEventRegistration.s_Utilities)
			{
				bool flag = current.TakeCapture();
				if (flag)
				{
					break;
				}
			}
		}

		private static void ReleaseCapture()
		{
			foreach (IUIElementsUtility current in UIEventRegistration.s_Utilities)
			{
				bool flag = current.ReleaseCapture();
				if (flag)
				{
					break;
				}
			}
		}

		private static bool EndContainerGUIFromException(Exception exception)
		{
			bool result;
			foreach (IUIElementsUtility current in UIEventRegistration.s_Utilities)
			{
				bool flag = current.EndContainerGUIFromException(exception);
				if (flag)
				{
					result = true;
					return result;
				}
			}
			result = GUIUtility.ShouldRethrowException(exception);
			return result;
		}

		private static bool ProcessEvent(int instanceID, IntPtr nativeEventPtr)
		{
			bool flag = false;
			bool result;
			foreach (IUIElementsUtility current in UIEventRegistration.s_Utilities)
			{
				bool flag2 = current.ProcessEvent(instanceID, nativeEventPtr, ref flag);
				if (flag2)
				{
					result = flag;
					return result;
				}
			}
			result = false;
			return result;
		}

		private static void CleanupRoots()
		{
			foreach (IUIElementsUtility current in UIEventRegistration.s_Utilities)
			{
				bool flag = current.CleanupRoots();
				if (flag)
				{
					break;
				}
			}
		}

		internal static void MakeCurrentIMGUIContainerDirty()
		{
			foreach (IUIElementsUtility current in UIEventRegistration.s_Utilities)
			{
				bool flag = current.MakeCurrentIMGUIContainerDirty();
				if (flag)
				{
					break;
				}
			}
		}

		internal static void UpdateSchedulers()
		{
			foreach (IUIElementsUtility current in UIEventRegistration.s_Utilities)
			{
				current.UpdateSchedulers();
			}
		}

		internal static void RequestRepaintForPanels(Action<ScriptableObject> repaintCallback)
		{
			foreach (IUIElementsUtility current in UIEventRegistration.s_Utilities)
			{
				current.RequestRepaintForPanels(repaintCallback);
			}
		}
	}
}
