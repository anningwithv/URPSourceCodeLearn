using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	public struct ManipulatorActivationFilter : IEquatable<ManipulatorActivationFilter>
	{
		public MouseButton button
		{
			[IsReadOnly]
			get;
			set;
		}

		public EventModifiers modifiers
		{
			[IsReadOnly]
			get;
			set;
		}

		public int clickCount
		{
			[IsReadOnly]
			get;
			set;
		}

		public override bool Equals(object obj)
		{
			return obj is ManipulatorActivationFilter && this.Equals((ManipulatorActivationFilter)obj);
		}

		public bool Equals(ManipulatorActivationFilter other)
		{
			return this.button == other.button && this.modifiers == other.modifiers && this.clickCount == other.clickCount;
		}

		public override int GetHashCode()
		{
			int num = 390957112;
			num = num * -1521134295 + this.button.GetHashCode();
			num = num * -1521134295 + this.modifiers.GetHashCode();
			return num * -1521134295 + this.clickCount.GetHashCode();
		}

		public bool Matches(IMouseEvent e)
		{
			bool flag = e == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.clickCount == 0 || e.clickCount >= this.clickCount;
				result = ((this.button == (MouseButton)e.button && this.HasModifiers(e)) & flag2);
			}
			return result;
		}

		private bool HasModifiers(IMouseEvent e)
		{
			bool flag = e == null;
			return !flag && this.MatchModifiers(e.altKey, e.ctrlKey, e.shiftKey, e.commandKey);
		}

		public bool Matches(IPointerEvent e)
		{
			bool flag = e == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.clickCount == 0 || e.clickCount >= this.clickCount;
				result = ((this.button == (MouseButton)e.button && this.HasModifiers(e)) & flag2);
			}
			return result;
		}

		private bool HasModifiers(IPointerEvent e)
		{
			bool flag = e == null;
			return !flag && this.MatchModifiers(e.altKey, e.ctrlKey, e.shiftKey, e.commandKey);
		}

		private bool MatchModifiers(bool alt, bool ctrl, bool shift, bool command)
		{
			bool flag = ((this.modifiers & EventModifiers.Alt) != EventModifiers.None && !alt) || ((this.modifiers & EventModifiers.Alt) == EventModifiers.None & alt);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = ((this.modifiers & EventModifiers.Control) != EventModifiers.None && !ctrl) || ((this.modifiers & EventModifiers.Control) == EventModifiers.None & ctrl);
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = ((this.modifiers & EventModifiers.Shift) != EventModifiers.None && !shift) || ((this.modifiers & EventModifiers.Shift) == EventModifiers.None & shift);
					result = (!flag3 && ((this.modifiers & EventModifiers.Command) == EventModifiers.None | command) && ((this.modifiers & EventModifiers.Command) != EventModifiers.None || !command));
				}
			}
			return result;
		}

		public static bool operator ==(ManipulatorActivationFilter filter1, ManipulatorActivationFilter filter2)
		{
			return filter1.Equals(filter2);
		}

		public static bool operator !=(ManipulatorActivationFilter filter1, ManipulatorActivationFilter filter2)
		{
			return !(filter1 == filter2);
		}
	}
}
