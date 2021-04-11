using System;

namespace UnityEngine.UIElements
{
	internal interface IUIElementsUtility
	{
		bool TakeCapture();

		bool ReleaseCapture();

		bool ProcessEvent(int instanceID, IntPtr nativeEventPtr, ref bool eventHandled);

		bool CleanupRoots();

		bool EndContainerGUIFromException(Exception exception);

		bool MakeCurrentIMGUIContainerDirty();

		void UpdateSchedulers();

		void RequestRepaintForPanels(Action<ScriptableObject> repaintCallback);
	}
}
