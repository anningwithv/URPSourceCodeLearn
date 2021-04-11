using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	public class VisualElementFocusRing : IFocusRing
	{
		public enum DefaultFocusOrder
		{
			ChildOrder,
			PositionXY,
			PositionYX
		}

		private class FocusRingRecord
		{
			public int m_AutoIndex;

			public Focusable m_Focusable;

			public bool m_IsSlot;

			public List<VisualElementFocusRing.FocusRingRecord> m_ScopeNavigationOrder;
		}

		private readonly VisualElement root;

		private List<VisualElementFocusRing.FocusRingRecord> m_FocusRing;

		public VisualElementFocusRing.DefaultFocusOrder defaultFocusOrder
		{
			get;
			set;
		}

		public VisualElementFocusRing(VisualElement root, VisualElementFocusRing.DefaultFocusOrder dfo = VisualElementFocusRing.DefaultFocusOrder.ChildOrder)
		{
			this.defaultFocusOrder = dfo;
			this.root = root;
			this.m_FocusRing = new List<VisualElementFocusRing.FocusRingRecord>();
		}

		private int FocusRingAutoIndexSort(VisualElementFocusRing.FocusRingRecord a, VisualElementFocusRing.FocusRingRecord b)
		{
			switch (this.defaultFocusOrder)
			{
			case VisualElementFocusRing.DefaultFocusOrder.ChildOrder:
			{
				IL_1E:
				int result = Comparer<int>.Default.Compare(a.m_AutoIndex, b.m_AutoIndex);
				return result;
			}
			case VisualElementFocusRing.DefaultFocusOrder.PositionXY:
			{
				VisualElement visualElement = a.m_Focusable as VisualElement;
				VisualElement visualElement2 = b.m_Focusable as VisualElement;
				bool flag = visualElement != null && visualElement2 != null;
				int result;
				if (flag)
				{
					bool flag2 = visualElement.layout.position.x < visualElement2.layout.position.x;
					if (flag2)
					{
						result = -1;
						return result;
					}
					bool flag3 = visualElement.layout.position.x > visualElement2.layout.position.x;
					if (flag3)
					{
						result = 1;
						return result;
					}
					bool flag4 = visualElement.layout.position.y < visualElement2.layout.position.y;
					if (flag4)
					{
						result = -1;
						return result;
					}
					bool flag5 = visualElement.layout.position.y > visualElement2.layout.position.y;
					if (flag5)
					{
						result = 1;
						return result;
					}
				}
				result = Comparer<int>.Default.Compare(a.m_AutoIndex, b.m_AutoIndex);
				return result;
			}
			case VisualElementFocusRing.DefaultFocusOrder.PositionYX:
			{
				VisualElement visualElement3 = a.m_Focusable as VisualElement;
				VisualElement visualElement4 = b.m_Focusable as VisualElement;
				bool flag6 = visualElement3 != null && visualElement4 != null;
				int result;
				if (flag6)
				{
					bool flag7 = visualElement3.layout.position.y < visualElement4.layout.position.y;
					if (flag7)
					{
						result = -1;
						return result;
					}
					bool flag8 = visualElement3.layout.position.y > visualElement4.layout.position.y;
					if (flag8)
					{
						result = 1;
						return result;
					}
					bool flag9 = visualElement3.layout.position.x < visualElement4.layout.position.x;
					if (flag9)
					{
						result = -1;
						return result;
					}
					bool flag10 = visualElement3.layout.position.x > visualElement4.layout.position.x;
					if (flag10)
					{
						result = 1;
						return result;
					}
				}
				result = Comparer<int>.Default.Compare(a.m_AutoIndex, b.m_AutoIndex);
				return result;
			}
			}
			goto IL_1E;
		}

		private int FocusRingSort(VisualElementFocusRing.FocusRingRecord a, VisualElementFocusRing.FocusRingRecord b)
		{
			bool flag = a.m_Focusable.tabIndex == 0 && b.m_Focusable.tabIndex == 0;
			int result;
			if (flag)
			{
				result = this.FocusRingAutoIndexSort(a, b);
			}
			else
			{
				bool flag2 = a.m_Focusable.tabIndex == 0;
				if (flag2)
				{
					result = 1;
				}
				else
				{
					bool flag3 = b.m_Focusable.tabIndex == 0;
					if (flag3)
					{
						result = -1;
					}
					else
					{
						int num = Comparer<int>.Default.Compare(a.m_Focusable.tabIndex, b.m_Focusable.tabIndex);
						bool flag4 = num == 0;
						if (flag4)
						{
							num = this.FocusRingAutoIndexSort(a, b);
						}
						result = num;
					}
				}
			}
			return result;
		}

		private void DoUpdate()
		{
			this.m_FocusRing.Clear();
			bool flag = this.root != null;
			if (flag)
			{
				List<VisualElementFocusRing.FocusRingRecord> list = new List<VisualElementFocusRing.FocusRingRecord>();
				int num = 0;
				this.BuildRingForScopeRecursive(this.root, ref num, list);
				this.SortAndFlattenScopeLists(list);
			}
		}

		private void BuildRingForScopeRecursive(VisualElement ve, ref int scopeIndex, List<VisualElementFocusRing.FocusRingRecord> scopeList)
		{
			int childCount = ve.hierarchy.childCount;
			for (int i = 0; i < childCount; i++)
			{
				VisualElement visualElement = ve.hierarchy[i];
				bool flag = visualElement.parent != null && visualElement == visualElement.parent.contentContainer;
				bool flag2 = visualElement.isCompositeRoot | flag;
				if (flag2)
				{
					VisualElementFocusRing.FocusRingRecord expr_58 = new VisualElementFocusRing.FocusRingRecord();
					int num = scopeIndex;
					scopeIndex = num + 1;
					expr_58.m_AutoIndex = num;
					expr_58.m_Focusable = visualElement;
					expr_58.m_IsSlot = flag;
					expr_58.m_ScopeNavigationOrder = new List<VisualElementFocusRing.FocusRingRecord>();
					VisualElementFocusRing.FocusRingRecord focusRingRecord = expr_58;
					scopeList.Add(focusRingRecord);
					int num2 = 0;
					this.BuildRingForScopeRecursive(visualElement, ref num2, focusRingRecord.m_ScopeNavigationOrder);
				}
				else
				{
					bool flag3 = visualElement.canGrabFocus && visualElement.tabIndex >= 0;
					if (flag3)
					{
						VisualElementFocusRing.FocusRingRecord expr_CB = new VisualElementFocusRing.FocusRingRecord();
						int num = scopeIndex;
						scopeIndex = num + 1;
						expr_CB.m_AutoIndex = num;
						expr_CB.m_Focusable = visualElement;
						expr_CB.m_IsSlot = false;
						expr_CB.m_ScopeNavigationOrder = null;
						scopeList.Add(expr_CB);
					}
					this.BuildRingForScopeRecursive(visualElement, ref scopeIndex, scopeList);
				}
			}
		}

		private void SortAndFlattenScopeLists(List<VisualElementFocusRing.FocusRingRecord> rootScopeList)
		{
			bool flag = rootScopeList != null;
			if (flag)
			{
				rootScopeList.Sort(new Comparison<VisualElementFocusRing.FocusRingRecord>(this.FocusRingSort));
				foreach (VisualElementFocusRing.FocusRingRecord current in rootScopeList)
				{
					bool flag2 = current.m_Focusable.canGrabFocus && current.m_Focusable.tabIndex >= 0;
					if (flag2)
					{
						bool flag3 = !current.m_Focusable.excludeFromFocusRing;
						if (flag3)
						{
							this.m_FocusRing.Add(current);
						}
						this.SortAndFlattenScopeLists(current.m_ScopeNavigationOrder);
					}
					else
					{
						bool isSlot = current.m_IsSlot;
						if (isSlot)
						{
							this.SortAndFlattenScopeLists(current.m_ScopeNavigationOrder);
						}
					}
					current.m_ScopeNavigationOrder = null;
				}
			}
		}

		private int GetFocusableInternalIndex(Focusable f)
		{
			bool flag = f != null;
			int result;
			if (flag)
			{
				for (int i = 0; i < this.m_FocusRing.Count; i++)
				{
					bool flag2 = f == this.m_FocusRing[i].m_Focusable;
					if (flag2)
					{
						result = i;
						return result;
					}
				}
			}
			result = -1;
			return result;
		}

		public FocusChangeDirection GetFocusChangeDirection(Focusable currentFocusable, EventBase e)
		{
			bool flag = e == null;
			if (flag)
			{
				throw new ArgumentNullException("e");
			}
			bool flag2 = e.eventTypeId == EventBase<MouseDownEvent>.TypeId();
			FocusChangeDirection result;
			if (flag2)
			{
				Focusable focusable = e.target as Focusable;
				bool flag3 = focusable != null;
				if (flag3)
				{
					result = VisualElementFocusChangeTarget.GetPooled(focusable);
					return result;
				}
			}
			bool flag4 = currentFocusable is IMGUIContainer && e.imguiEvent != null;
			if (flag4)
			{
				result = FocusChangeDirection.none;
			}
			else
			{
				result = VisualElementFocusRing.GetKeyDownFocusChangeDirection(e);
			}
			return result;
		}

		internal static FocusChangeDirection GetKeyDownFocusChangeDirection(EventBase e)
		{
			bool flag = e.eventTypeId == EventBase<KeyDownEvent>.TypeId();
			FocusChangeDirection result;
			if (flag)
			{
				KeyDownEvent keyDownEvent = e as KeyDownEvent;
				bool flag2 = keyDownEvent.character == '\u0019' || keyDownEvent.character == '\t';
				if (flag2)
				{
					bool flag3 = keyDownEvent.modifiers == EventModifiers.Shift;
					if (flag3)
					{
						result = VisualElementFocusChangeDirection.left;
						return result;
					}
					bool flag4 = keyDownEvent.modifiers == EventModifiers.None;
					if (flag4)
					{
						result = VisualElementFocusChangeDirection.right;
						return result;
					}
				}
			}
			result = FocusChangeDirection.none;
			return result;
		}

		public Focusable GetNextFocusable(Focusable currentFocusable, FocusChangeDirection direction)
		{
			bool flag = direction == FocusChangeDirection.none || direction == FocusChangeDirection.unspecified;
			Focusable result;
			if (flag)
			{
				result = currentFocusable;
			}
			else
			{
				VisualElementFocusChangeTarget visualElementFocusChangeTarget = direction as VisualElementFocusChangeTarget;
				bool flag2 = visualElementFocusChangeTarget != null;
				if (flag2)
				{
					result = visualElementFocusChangeTarget.target;
				}
				else
				{
					this.DoUpdate();
					bool flag3 = this.m_FocusRing.Count == 0;
					if (flag3)
					{
						result = null;
					}
					else
					{
						int num = 0;
						bool flag4 = direction == VisualElementFocusChangeDirection.right;
						if (flag4)
						{
							num = this.GetFocusableInternalIndex(currentFocusable) + 1;
							bool flag5 = currentFocusable != null && num == 0;
							if (flag5)
							{
								result = VisualElementFocusRing.GetNextFocusableInTree(currentFocusable as VisualElement);
								return result;
							}
							bool flag6 = num == this.m_FocusRing.Count;
							if (flag6)
							{
								num = 0;
							}
							while (this.m_FocusRing[num].m_Focusable.delegatesFocus)
							{
								num++;
								bool flag7 = num == this.m_FocusRing.Count;
								if (flag7)
								{
									result = null;
									return result;
								}
							}
						}
						else
						{
							bool flag8 = direction == VisualElementFocusChangeDirection.left;
							if (flag8)
							{
								num = this.GetFocusableInternalIndex(currentFocusable) - 1;
								bool flag9 = currentFocusable != null && num == -2;
								if (flag9)
								{
									result = VisualElementFocusRing.GetPreviousFocusableInTree(currentFocusable as VisualElement);
									return result;
								}
								bool flag10 = num < 0;
								if (flag10)
								{
									num = this.m_FocusRing.Count - 1;
								}
								while (this.m_FocusRing[num].m_Focusable.delegatesFocus)
								{
									num--;
									bool flag11 = num == -1;
									if (flag11)
									{
										result = null;
										return result;
									}
								}
							}
						}
						result = this.m_FocusRing[num].m_Focusable;
					}
				}
			}
			return result;
		}

		internal static Focusable GetNextFocusableInTree(VisualElement currentFocusable)
		{
			bool flag = currentFocusable == null;
			Focusable result;
			if (flag)
			{
				result = null;
			}
			else
			{
				VisualElement nextElementDepthFirst = currentFocusable.GetNextElementDepthFirst();
				while (!nextElementDepthFirst.canGrabFocus || nextElementDepthFirst.tabIndex < 0 || nextElementDepthFirst.excludeFromFocusRing)
				{
					nextElementDepthFirst = nextElementDepthFirst.GetNextElementDepthFirst();
					bool flag2 = nextElementDepthFirst == null;
					if (flag2)
					{
						nextElementDepthFirst = currentFocusable.GetRoot();
					}
					bool flag3 = nextElementDepthFirst == currentFocusable;
					if (flag3)
					{
						result = currentFocusable;
						return result;
					}
				}
				result = nextElementDepthFirst;
			}
			return result;
		}

		internal static Focusable GetPreviousFocusableInTree(VisualElement currentFocusable)
		{
			bool flag = currentFocusable == null;
			Focusable result;
			if (flag)
			{
				result = null;
			}
			else
			{
				VisualElement visualElement = currentFocusable.GetPreviousElementDepthFirst();
				while (!visualElement.canGrabFocus || visualElement.tabIndex < 0 || visualElement.excludeFromFocusRing)
				{
					visualElement = visualElement.GetPreviousElementDepthFirst();
					bool flag2 = visualElement == null;
					if (flag2)
					{
						visualElement = currentFocusable.GetRoot();
						while (visualElement.childCount > 0)
						{
							visualElement = visualElement.hierarchy.ElementAt(visualElement.childCount - 1);
						}
					}
					bool flag3 = visualElement == currentFocusable;
					if (flag3)
					{
						result = currentFocusable;
						return result;
					}
				}
				result = visualElement;
			}
			return result;
		}
	}
}
