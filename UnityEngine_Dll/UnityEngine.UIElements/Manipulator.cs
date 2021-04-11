using System;

namespace UnityEngine.UIElements
{
	public abstract class Manipulator : IManipulator
	{
		private VisualElement m_Target;

		public VisualElement target
		{
			get
			{
				return this.m_Target;
			}
			set
			{
				bool flag = this.target != null;
				if (flag)
				{
					this.UnregisterCallbacksFromTarget();
				}
				this.m_Target = value;
				bool flag2 = this.target != null;
				if (flag2)
				{
					this.RegisterCallbacksOnTarget();
				}
			}
		}

		protected abstract void RegisterCallbacksOnTarget();

		protected abstract void UnregisterCallbacksFromTarget();
	}
}
