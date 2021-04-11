using System;

namespace UnityEngine.UIElements
{
	internal interface IPanelDebugger
	{
		IPanelDebug panelDebug
		{
			get;
			set;
		}

		void Disconnect();

		void Refresh();

		void OnVersionChanged(VisualElement ele, VersionChangeType changeTypeFlag);

		bool InterceptEvent(EventBase ev);

		void PostProcessEvent(EventBase ev);
	}
}
