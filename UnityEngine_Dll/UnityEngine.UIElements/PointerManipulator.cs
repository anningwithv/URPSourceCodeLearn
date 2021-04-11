using System;

namespace UnityEngine.UIElements
{
	public abstract class PointerManipulator : MouseManipulator
	{
		private int m_CurrentPointerId;

		protected bool CanStartManipulation(IPointerEvent e)
		{
			bool result;
			foreach (ManipulatorActivationFilter current in base.activators)
			{
				bool flag = current.Matches(e);
				if (flag)
				{
					this.m_CurrentPointerId = e.pointerId;
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		protected bool CanStopManipulation(IPointerEvent e)
		{
			bool flag = e == null;
			return !flag && e.pointerId == this.m_CurrentPointerId;
		}
	}
}
