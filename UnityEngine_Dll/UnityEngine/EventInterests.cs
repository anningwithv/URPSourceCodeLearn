using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	internal struct EventInterests
	{
		public bool wantsMouseMove
		{
			[IsReadOnly]
			get;
			set;
		}

		public bool wantsMouseEnterLeaveWindow
		{
			[IsReadOnly]
			get;
			set;
		}

		public bool wantsLessLayoutEvents
		{
			[IsReadOnly]
			get;
			set;
		}

		public bool WantsEvent(EventType type)
		{
			bool result;
			if (type != EventType.MouseMove)
			{
				result = (type - EventType.MouseEnterWindow > 1 || this.wantsMouseEnterLeaveWindow);
			}
			else
			{
				result = this.wantsMouseMove;
			}
			return result;
		}

		public bool WantsLayoutPass(EventType type)
		{
			bool flag = !this.wantsLessLayoutEvents;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				switch (type)
				{
				case EventType.MouseDown:
				case EventType.MouseUp:
					result = this.wantsMouseMove;
					return result;
				case EventType.MouseMove:
				case EventType.MouseDrag:
				case EventType.ScrollWheel:
					goto IL_6C;
				case EventType.KeyDown:
				case EventType.KeyUp:
					result = GUIUtility.textFieldInput;
					return result;
				case EventType.Repaint:
					break;
				default:
					if (type != EventType.ExecuteCommand)
					{
						if (type - EventType.MouseEnterWindow > 1)
						{
							goto IL_6C;
						}
						result = this.wantsMouseEnterLeaveWindow;
						return result;
					}
					break;
				}
				result = true;
				return result;
				IL_6C:
				result = false;
			}
			return result;
		}
	}
}
