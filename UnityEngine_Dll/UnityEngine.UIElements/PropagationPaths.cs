using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	internal class PropagationPaths
	{
		[Flags]
		public enum Type
		{
			None = 0,
			TrickleDown = 1,
			BubbleUp = 2
		}

		private static readonly ObjectPool<PropagationPaths> s_Pool = new ObjectPool<PropagationPaths>(100);

		public readonly List<VisualElement> trickleDownPath;

		public readonly List<VisualElement> targetElements;

		public readonly List<VisualElement> bubbleUpPath;

		private const int k_DefaultPropagationDepth = 16;

		private const int k_DefaultTargetCount = 4;

		public PropagationPaths()
		{
			this.trickleDownPath = new List<VisualElement>(16);
			this.targetElements = new List<VisualElement>(4);
			this.bubbleUpPath = new List<VisualElement>(16);
		}

		public PropagationPaths(PropagationPaths paths)
		{
			this.trickleDownPath = new List<VisualElement>(paths.trickleDownPath);
			this.targetElements = new List<VisualElement>(paths.targetElements);
			this.bubbleUpPath = new List<VisualElement>(paths.bubbleUpPath);
		}

		internal static PropagationPaths Copy(PropagationPaths paths)
		{
			PropagationPaths propagationPaths = PropagationPaths.s_Pool.Get();
			propagationPaths.trickleDownPath.AddRange(paths.trickleDownPath);
			propagationPaths.targetElements.AddRange(paths.targetElements);
			propagationPaths.bubbleUpPath.AddRange(paths.bubbleUpPath);
			return propagationPaths;
		}

		public static PropagationPaths Build(VisualElement elem, PropagationPaths.Type pathTypesRequested)
		{
			bool flag = elem == null || pathTypesRequested == PropagationPaths.Type.None;
			PropagationPaths result;
			if (flag)
			{
				result = null;
			}
			else
			{
				PropagationPaths propagationPaths = PropagationPaths.s_Pool.Get();
				propagationPaths.targetElements.Add(elem);
				while (elem.hierarchy.parent != null)
				{
					bool enabledInHierarchy = elem.hierarchy.parent.enabledInHierarchy;
					if (enabledInHierarchy)
					{
						bool isCompositeRoot = elem.hierarchy.parent.isCompositeRoot;
						if (isCompositeRoot)
						{
							propagationPaths.targetElements.Add(elem.hierarchy.parent);
						}
						else
						{
							bool flag2 = (pathTypesRequested & PropagationPaths.Type.TrickleDown) == PropagationPaths.Type.TrickleDown && elem.hierarchy.parent.HasTrickleDownHandlers();
							if (flag2)
							{
								propagationPaths.trickleDownPath.Add(elem.hierarchy.parent);
							}
							bool flag3 = (pathTypesRequested & PropagationPaths.Type.BubbleUp) == PropagationPaths.Type.BubbleUp && elem.hierarchy.parent.HasBubbleUpHandlers();
							if (flag3)
							{
								propagationPaths.bubbleUpPath.Add(elem.hierarchy.parent);
							}
						}
					}
					elem = elem.hierarchy.parent;
				}
				result = propagationPaths;
			}
			return result;
		}

		public void Release()
		{
			this.bubbleUpPath.Clear();
			this.targetElements.Clear();
			this.trickleDownPath.Clear();
			PropagationPaths.s_Pool.Release(this);
		}
	}
}
