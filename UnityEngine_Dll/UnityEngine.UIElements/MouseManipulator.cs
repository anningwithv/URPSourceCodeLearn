using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	public abstract class MouseManipulator : Manipulator
	{
		private ManipulatorActivationFilter m_currentActivator;

		public List<ManipulatorActivationFilter> activators
		{
			get;
			private set;
		}

		protected MouseManipulator()
		{
			this.activators = new List<ManipulatorActivationFilter>();
		}

		protected bool CanStartManipulation(IMouseEvent e)
		{
			bool result;
			foreach (ManipulatorActivationFilter current in this.activators)
			{
				bool flag = current.Matches(e);
				if (flag)
				{
					this.m_currentActivator = current;
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		protected bool CanStopManipulation(IMouseEvent e)
		{
			bool flag = e == null;
			return !flag && e.button == (int)this.m_currentActivator.button;
		}
	}
}
