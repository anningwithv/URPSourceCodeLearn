using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	public class FocusController
	{
		private struct FocusedElement
		{
			public VisualElement m_SubTreeRoot;

			public Focusable m_FocusedElement;
		}

		private List<FocusController.FocusedElement> m_FocusedElements = new List<FocusController.FocusedElement>();

		private Focusable m_LastFocusedElement;

		private IFocusRing focusRing
		{
			[CompilerGenerated]
			get
			{
				return this.<focusRing>k__BackingField;
			}
		}

		public Focusable focusedElement
		{
			get
			{
				return this.GetRetargetedFocusedElement(null);
			}
		}

		internal int imguiKeyboardControl
		{
			get;
			set;
		}

		public FocusController(IFocusRing focusRing)
		{
			this.<focusRing>k__BackingField = focusRing;
			this.imguiKeyboardControl = 0;
		}

		internal bool IsFocused(Focusable f)
		{
			bool result;
			foreach (FocusController.FocusedElement current in this.m_FocusedElements)
			{
				bool flag = current.m_FocusedElement == f;
				if (flag)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		internal Focusable GetRetargetedFocusedElement(VisualElement retargetAgainst)
		{
			VisualElement visualElement = (retargetAgainst != null) ? retargetAgainst.hierarchy.parent : null;
			bool flag = visualElement == null;
			Focusable result;
			if (flag)
			{
				bool flag2 = this.m_FocusedElements.Count > 0;
				if (flag2)
				{
					result = this.m_FocusedElements[this.m_FocusedElements.Count - 1].m_FocusedElement;
					return result;
				}
			}
			else
			{
				while (!visualElement.isCompositeRoot && visualElement.hierarchy.parent != null)
				{
					visualElement = visualElement.hierarchy.parent;
				}
				foreach (FocusController.FocusedElement current in this.m_FocusedElements)
				{
					bool flag3 = current.m_SubTreeRoot == visualElement;
					if (flag3)
					{
						result = current.m_FocusedElement;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		internal Focusable GetLeafFocusedElement()
		{
			bool flag = this.m_FocusedElements.Count > 0;
			Focusable result;
			if (flag)
			{
				result = this.m_FocusedElements[0].m_FocusedElement;
			}
			else
			{
				result = null;
			}
			return result;
		}

		internal void SetFocusToLastFocusedElement()
		{
			bool flag = this.m_LastFocusedElement != null && !(this.m_LastFocusedElement is IMGUIContainer);
			if (flag)
			{
				this.m_LastFocusedElement.Focus();
			}
			this.m_LastFocusedElement = null;
		}

		internal void BlurLastFocusedElement()
		{
			bool flag = this.m_LastFocusedElement != null && !(this.m_LastFocusedElement is IMGUIContainer);
			if (flag)
			{
				Focusable lastFocusedElement = this.m_LastFocusedElement;
				this.m_LastFocusedElement.Blur();
				this.m_LastFocusedElement = lastFocusedElement;
			}
		}

		internal void DoFocusChange(Focusable f)
		{
			this.m_FocusedElements.Clear();
			VisualElement visualElement = f as VisualElement;
			bool flag = !(f is IMGUIContainer);
			if (flag)
			{
				this.m_LastFocusedElement = f;
			}
			while (visualElement != null)
			{
				bool flag2 = visualElement.hierarchy.parent == null || visualElement.isCompositeRoot;
				if (flag2)
				{
					this.m_FocusedElements.Add(new FocusController.FocusedElement
					{
						m_SubTreeRoot = visualElement,
						m_FocusedElement = f
					});
					f = visualElement;
				}
				visualElement = visualElement.hierarchy.parent;
			}
		}

		private void AboutToReleaseFocus(Focusable focusable, Focusable willGiveFocusTo, FocusChangeDirection direction)
		{
			using (FocusOutEvent pooled = FocusEventBase<FocusOutEvent>.GetPooled(focusable, willGiveFocusTo, direction, this, false))
			{
				focusable.SendEvent(pooled);
			}
		}

		private void ReleaseFocus(Focusable focusable, Focusable willGiveFocusTo, FocusChangeDirection direction)
		{
			using (BlurEvent pooled = FocusEventBase<BlurEvent>.GetPooled(focusable, willGiveFocusTo, direction, this, false))
			{
				focusable.SendEvent(pooled);
			}
		}

		private void AboutToGrabFocus(Focusable focusable, Focusable willTakeFocusFrom, FocusChangeDirection direction)
		{
			using (FocusInEvent pooled = FocusEventBase<FocusInEvent>.GetPooled(focusable, willTakeFocusFrom, direction, this, false))
			{
				focusable.SendEvent(pooled);
			}
		}

		private void GrabFocus(Focusable focusable, Focusable willTakeFocusFrom, FocusChangeDirection direction, bool bIsFocusDelegated = false)
		{
			using (FocusEvent pooled = FocusEventBase<FocusEvent>.GetPooled(focusable, willTakeFocusFrom, direction, this, bIsFocusDelegated))
			{
				focusable.SendEvent(pooled);
			}
		}

		internal void SwitchFocus(Focusable newFocusedElement, bool bIsFocusDelegated = false)
		{
			this.SwitchFocus(newFocusedElement, FocusChangeDirection.unspecified, bIsFocusDelegated);
		}

		internal void SwitchFocus(Focusable newFocusedElement, FocusChangeDirection direction, bool bIsFocusDelegated = false)
		{
			bool flag = this.GetLeafFocusedElement() == newFocusedElement;
			if (!flag)
			{
				Focusable leafFocusedElement = this.GetLeafFocusedElement();
				bool flag2 = newFocusedElement == null || !newFocusedElement.canGrabFocus;
				if (flag2)
				{
					bool flag3 = leafFocusedElement != null;
					if (flag3)
					{
						this.AboutToReleaseFocus(leafFocusedElement, null, direction);
						this.ReleaseFocus(leafFocusedElement, null, direction);
					}
				}
				else
				{
					bool flag4 = newFocusedElement != leafFocusedElement;
					if (flag4)
					{
						VisualElement expr_67 = newFocusedElement as VisualElement;
						VisualElement willGiveFocusTo = (expr_67 != null) ? expr_67.RetargetElement(leafFocusedElement as VisualElement) : null;
						VisualElement expr_81 = leafFocusedElement as VisualElement;
						VisualElement willTakeFocusFrom = (expr_81 != null) ? expr_81.RetargetElement(newFocusedElement as VisualElement) : null;
						bool flag5 = leafFocusedElement != null;
						if (flag5)
						{
							this.AboutToReleaseFocus(leafFocusedElement, willGiveFocusTo, direction);
						}
						this.AboutToGrabFocus(newFocusedElement, willTakeFocusFrom, direction);
						bool flag6 = leafFocusedElement != null;
						if (flag6)
						{
							this.ReleaseFocus(leafFocusedElement, willGiveFocusTo, direction);
						}
						this.GrabFocus(newFocusedElement, willTakeFocusFrom, direction, bIsFocusDelegated);
					}
				}
			}
		}

		internal Focusable SwitchFocusOnEvent(EventBase e)
		{
			bool processedByFocusController = e.processedByFocusController;
			Focusable result;
			if (processedByFocusController)
			{
				result = this.GetLeafFocusedElement();
			}
			else
			{
				using (FocusChangeDirection focusChangeDirection = this.focusRing.GetFocusChangeDirection(this.GetLeafFocusedElement(), e))
				{
					bool flag = focusChangeDirection != FocusChangeDirection.none;
					if (flag)
					{
						Focusable nextFocusable = this.focusRing.GetNextFocusable(this.GetLeafFocusedElement(), focusChangeDirection);
						focusChangeDirection.ApplyTo(this, nextFocusable);
						e.processedByFocusController = true;
						result = nextFocusable;
						return result;
					}
				}
				result = this.GetLeafFocusedElement();
			}
			return result;
		}

		internal void SyncIMGUIFocus(int imguiKeyboardControlID, Focusable imguiContainerHavingKeyboardControl, bool forceSwitch)
		{
			this.imguiKeyboardControl = imguiKeyboardControlID;
			bool flag = forceSwitch || this.imguiKeyboardControl != 0;
			if (flag)
			{
				this.SwitchFocus(imguiContainerHavingKeyboardControl, FocusChangeDirection.unspecified, false);
			}
			else
			{
				this.SwitchFocus(null, FocusChangeDirection.unspecified, false);
			}
		}
	}
}
