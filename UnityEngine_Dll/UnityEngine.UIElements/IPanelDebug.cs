using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	internal interface IPanelDebug
	{
		IPanel panel
		{
			get;
		}

		IPanel debuggerOverlayPanel
		{
			get;
		}

		VisualElement visualTree
		{
			get;
		}

		VisualElement debugContainer
		{
			get;
		}

		void AttachDebugger(IPanelDebugger debugger);

		void DetachDebugger(IPanelDebugger debugger);

		void DetachAllDebuggers();

		IEnumerable<IPanelDebugger> GetAttachedDebuggers();

		void MarkDirtyRepaint();

		void MarkDebugContainerDirtyRepaint();

		void Refresh();

		void OnVersionChanged(VisualElement ele, VersionChangeType changeTypeFlag);

		bool InterceptEvent(EventBase ev);

		void PostProcessEvent(EventBase ev);
	}
}
