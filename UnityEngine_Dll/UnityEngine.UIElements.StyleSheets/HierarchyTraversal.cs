using System;

namespace UnityEngine.UIElements.StyleSheets
{
	internal abstract class HierarchyTraversal
	{
		public virtual void Traverse(VisualElement element)
		{
			this.TraverseRecursive(element, 0);
		}

		public abstract void TraverseRecursive(VisualElement element, int depth);

		protected void Recurse(VisualElement element, int depth)
		{
			int i = 0;
			while (i < element.hierarchy.childCount)
			{
				VisualElement visualElement = element.hierarchy[i];
				this.TraverseRecursive(visualElement, depth + 1);
				bool flag = visualElement.hierarchy.parent != element;
				if (!flag)
				{
					i++;
				}
			}
		}
	}
}
