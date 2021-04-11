using System;
using System.ComponentModel;

namespace UnityEngine
{
	public enum EventType
	{
		MouseDown,
		MouseUp,
		MouseMove,
		MouseDrag,
		KeyDown,
		KeyUp,
		ScrollWheel,
		Repaint,
		Layout,
		DragUpdated,
		DragPerform,
		DragExited = 15,
		Ignore = 11,
		Used,
		ValidateCommand,
		ExecuteCommand,
		ContextClick = 16,
		MouseEnterWindow = 20,
		MouseLeaveWindow,
		TouchDown = 30,
		TouchUp,
		TouchMove,
		TouchEnter,
		TouchLeave,
		TouchStationary,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use MouseDown instead (UnityUpgradable) -> MouseDown", true)]
		mouseDown = 0,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use MouseUp instead (UnityUpgradable) -> MouseUp", true)]
		mouseUp,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use MouseMove instead (UnityUpgradable) -> MouseMove", true)]
		mouseMove,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use MouseDrag instead (UnityUpgradable) -> MouseDrag", true)]
		mouseDrag,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use KeyDown instead (UnityUpgradable) -> KeyDown", true)]
		keyDown,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use KeyUp instead (UnityUpgradable) -> KeyUp", true)]
		keyUp,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use ScrollWheel instead (UnityUpgradable) -> ScrollWheel", true)]
		scrollWheel,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use Repaint instead (UnityUpgradable) -> Repaint", true)]
		repaint,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use Layout instead (UnityUpgradable) -> Layout", true)]
		layout,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use DragUpdated instead (UnityUpgradable) -> DragUpdated", true)]
		dragUpdated,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use DragPerform instead (UnityUpgradable) -> DragPerform", true)]
		dragPerform,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use Ignore instead (UnityUpgradable) -> Ignore", true)]
		ignore,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use Used instead (UnityUpgradable) -> Used", true)]
		used
	}
}
