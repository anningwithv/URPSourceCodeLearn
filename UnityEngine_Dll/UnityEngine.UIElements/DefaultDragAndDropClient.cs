using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	internal class DefaultDragAndDropClient : IDragAndDrop, IDragAndDropData
	{
		private StartDragArgs m_StartDragArgs;

		public object userData
		{
			get
			{
				StartDragArgs expr_06 = this.m_StartDragArgs;
				return (expr_06 != null) ? expr_06.userData : null;
			}
		}

		public IEnumerable<UnityEngine.Object> unityObjectReferences
		{
			get
			{
				StartDragArgs expr_06 = this.m_StartDragArgs;
				return (expr_06 != null) ? expr_06.unityObjectReferences : null;
			}
		}

		public IDragAndDropData data
		{
			get
			{
				return this;
			}
		}

		public void StartDrag(StartDragArgs args)
		{
			this.m_StartDragArgs = args;
		}

		public void AcceptDrag()
		{
			this.m_StartDragArgs = null;
		}

		public void SetVisualMode(DragVisualMode visualMode)
		{
		}

		public object GetGenericData(string key)
		{
			bool flag = this.m_StartDragArgs == null;
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = (this.m_StartDragArgs.genericData.ContainsKey(key) ? this.m_StartDragArgs.genericData[key] : null);
			}
			return result;
		}
	}
}
