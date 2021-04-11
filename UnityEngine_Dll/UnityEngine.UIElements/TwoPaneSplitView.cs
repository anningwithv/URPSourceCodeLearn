using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	public class TwoPaneSplitView : VisualElement
	{
		public new class UxmlFactory : UxmlFactory<TwoPaneSplitView, TwoPaneSplitView.UxmlTraits>
		{
		}

		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			private UxmlIntAttributeDescription m_FixedPaneIndex = new UxmlIntAttributeDescription
			{
				name = "fixed-pane-index",
				defaultValue = 0
			};

			private UxmlIntAttributeDescription m_FixedPaneInitialDimension = new UxmlIntAttributeDescription
			{
				name = "fixed-pane-initial-dimension",
				defaultValue = 100
			};

			private UxmlEnumAttributeDescription<TwoPaneSplitViewOrientation> m_Orientation = new UxmlEnumAttributeDescription<TwoPaneSplitViewOrientation>
			{
				name = "orientation",
				defaultValue = TwoPaneSplitViewOrientation.Horizontal
			};

			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				int valueFromBag = this.m_FixedPaneIndex.GetValueFromBag(bag, cc);
				int valueFromBag2 = this.m_FixedPaneInitialDimension.GetValueFromBag(bag, cc);
				TwoPaneSplitViewOrientation valueFromBag3 = this.m_Orientation.GetValueFromBag(bag, cc);
				((TwoPaneSplitView)ve).Init(valueFromBag, (float)valueFromBag2, valueFromBag3);
			}
		}

		private static readonly string s_UssClassName = "unity-two-pane-split-view";

		private static readonly string s_ContentContainerClassName = "unity-two-pane-split-view__content-container";

		private static readonly string s_HandleDragLineClassName = "unity-two-pane-split-view__dragline";

		private static readonly string s_HandleDragLineVerticalClassName = TwoPaneSplitView.s_HandleDragLineClassName + "--vertical";

		private static readonly string s_HandleDragLineHorizontalClassName = TwoPaneSplitView.s_HandleDragLineClassName + "--horizontal";

		private static readonly string s_HandleDragLineAnchorClassName = "unity-two-pane-split-view__dragline-anchor";

		private static readonly string s_HandleDragLineAnchorVerticalClassName = TwoPaneSplitView.s_HandleDragLineAnchorClassName + "--vertical";

		private static readonly string s_HandleDragLineAnchorHorizontalClassName = TwoPaneSplitView.s_HandleDragLineAnchorClassName + "--horizontal";

		private static readonly string s_VerticalClassName = "unity-two-pane-split-view--vertical";

		private static readonly string s_HorizontalClassName = "unity-two-pane-split-view--horizontal";

		private VisualElement m_LeftPane;

		private VisualElement m_RightPane;

		private VisualElement m_FixedPane;

		private VisualElement m_FlexedPane;

		private VisualElement m_DragLine;

		private VisualElement m_DragLineAnchor;

		private bool m_CollapseMode;

		private VisualElement m_Content;

		private TwoPaneSplitViewOrientation m_Orientation;

		private int m_FixedPaneIndex;

		private float m_FixedPaneInitialDimension;

		private TwoPaneSplitViewResizer m_Resizer;

		public VisualElement fixedPane
		{
			get
			{
				return this.m_FixedPane;
			}
		}

		public VisualElement flexedPane
		{
			get
			{
				return this.m_FlexedPane;
			}
		}

		public int fixedPaneIndex
		{
			get
			{
				return this.m_FixedPaneIndex;
			}
			set
			{
				bool flag = value == this.m_FixedPaneIndex;
				if (!flag)
				{
					this.Init(value, this.m_FixedPaneInitialDimension, this.m_Orientation);
				}
			}
		}

		public float fixedPaneInitialDimension
		{
			get
			{
				return this.m_FixedPaneInitialDimension;
			}
			set
			{
				bool flag = value == this.m_FixedPaneInitialDimension;
				if (!flag)
				{
					this.Init(this.m_FixedPaneIndex, value, this.m_Orientation);
				}
			}
		}

		public TwoPaneSplitViewOrientation orientation
		{
			get
			{
				return this.m_Orientation;
			}
			set
			{
				bool flag = value == this.m_Orientation;
				if (!flag)
				{
					this.Init(this.m_FixedPaneIndex, this.m_FixedPaneInitialDimension, value);
				}
			}
		}

		public override VisualElement contentContainer
		{
			get
			{
				return this.m_Content;
			}
		}

		public TwoPaneSplitView()
		{
			base.AddToClassList(TwoPaneSplitView.s_UssClassName);
			this.m_Content = new VisualElement();
			this.m_Content.name = "unity-content-container";
			this.m_Content.AddToClassList(TwoPaneSplitView.s_ContentContainerClassName);
			base.hierarchy.Add(this.m_Content);
			this.m_DragLineAnchor = new VisualElement();
			this.m_DragLineAnchor.name = "unity-dragline-anchor";
			this.m_DragLineAnchor.AddToClassList(TwoPaneSplitView.s_HandleDragLineAnchorClassName);
			base.hierarchy.Add(this.m_DragLineAnchor);
			this.m_DragLine = new VisualElement();
			this.m_DragLine.name = "unity-dragline";
			this.m_DragLine.AddToClassList(TwoPaneSplitView.s_HandleDragLineClassName);
			this.m_DragLineAnchor.Add(this.m_DragLine);
		}

		public TwoPaneSplitView(int fixedPaneIndex, float fixedPaneStartDimension, TwoPaneSplitViewOrientation orientation) : this()
		{
			this.Init(fixedPaneIndex, fixedPaneStartDimension, orientation);
		}

		public void CollapseChild(int index)
		{
			bool flag = this.m_LeftPane == null;
			if (!flag)
			{
				this.m_DragLine.style.display = DisplayStyle.None;
				this.m_DragLineAnchor.style.display = DisplayStyle.None;
				bool flag2 = index == 0;
				if (flag2)
				{
					this.m_RightPane.style.width = StyleKeyword.Initial;
					this.m_RightPane.style.height = StyleKeyword.Initial;
					this.m_RightPane.style.flexGrow = 1f;
					this.m_LeftPane.style.display = DisplayStyle.None;
				}
				else
				{
					this.m_LeftPane.style.width = StyleKeyword.Initial;
					this.m_LeftPane.style.height = StyleKeyword.Initial;
					this.m_LeftPane.style.flexGrow = 1f;
					this.m_RightPane.style.display = DisplayStyle.None;
				}
				this.m_CollapseMode = true;
			}
		}

		public void UnCollapse()
		{
			bool flag = this.m_LeftPane == null;
			if (!flag)
			{
				this.m_LeftPane.style.display = DisplayStyle.Flex;
				this.m_RightPane.style.display = DisplayStyle.Flex;
				this.m_DragLine.style.display = DisplayStyle.Flex;
				this.m_DragLineAnchor.style.display = DisplayStyle.Flex;
				this.m_LeftPane.style.flexGrow = 0f;
				this.m_RightPane.style.flexGrow = 0f;
				this.m_CollapseMode = false;
				this.Init(this.m_FixedPaneIndex, this.m_FixedPaneInitialDimension, this.m_Orientation);
			}
		}

		internal void Init(int fixedPaneIndex, float fixedPaneInitialDimension, TwoPaneSplitViewOrientation orientation)
		{
			this.m_Orientation = orientation;
			this.m_FixedPaneIndex = fixedPaneIndex;
			this.m_FixedPaneInitialDimension = fixedPaneInitialDimension;
			this.m_Content.RemoveFromClassList(TwoPaneSplitView.s_HorizontalClassName);
			this.m_Content.RemoveFromClassList(TwoPaneSplitView.s_VerticalClassName);
			bool flag = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
			if (flag)
			{
				this.m_Content.AddToClassList(TwoPaneSplitView.s_HorizontalClassName);
			}
			else
			{
				this.m_Content.AddToClassList(TwoPaneSplitView.s_VerticalClassName);
			}
			this.m_DragLineAnchor.RemoveFromClassList(TwoPaneSplitView.s_HandleDragLineAnchorHorizontalClassName);
			this.m_DragLineAnchor.RemoveFromClassList(TwoPaneSplitView.s_HandleDragLineAnchorVerticalClassName);
			bool flag2 = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
			if (flag2)
			{
				this.m_DragLineAnchor.AddToClassList(TwoPaneSplitView.s_HandleDragLineAnchorHorizontalClassName);
			}
			else
			{
				this.m_DragLineAnchor.AddToClassList(TwoPaneSplitView.s_HandleDragLineAnchorVerticalClassName);
			}
			this.m_DragLine.RemoveFromClassList(TwoPaneSplitView.s_HandleDragLineHorizontalClassName);
			this.m_DragLine.RemoveFromClassList(TwoPaneSplitView.s_HandleDragLineVerticalClassName);
			bool flag3 = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
			if (flag3)
			{
				this.m_DragLine.AddToClassList(TwoPaneSplitView.s_HandleDragLineHorizontalClassName);
			}
			else
			{
				this.m_DragLine.AddToClassList(TwoPaneSplitView.s_HandleDragLineVerticalClassName);
			}
			bool flag4 = this.m_Resizer != null;
			if (flag4)
			{
				this.m_DragLineAnchor.RemoveManipulator(this.m_Resizer);
				this.m_Resizer = null;
			}
			bool flag5 = this.m_Content.childCount != 2;
			if (flag5)
			{
				base.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnPostDisplaySetup), TrickleDown.NoTrickleDown);
			}
			else
			{
				this.PostDisplaySetup();
			}
		}

		private void OnPostDisplaySetup(GeometryChangedEvent evt)
		{
			bool flag = this.m_Content.childCount != 2;
			if (flag)
			{
				Debug.LogError("TwoPaneSplitView needs exactly 2 chilren.");
			}
			else
			{
				this.PostDisplaySetup();
				base.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnPostDisplaySetup), TrickleDown.NoTrickleDown);
				base.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnSizeChange), TrickleDown.NoTrickleDown);
			}
		}

		private void PostDisplaySetup()
		{
			bool flag = this.m_Content.childCount != 2;
			if (flag)
			{
				Debug.LogError("TwoPaneSplitView needs exactly 2 children.");
			}
			else
			{
				this.m_LeftPane = this.m_Content[0];
				bool flag2 = this.m_FixedPaneIndex == 0;
				if (flag2)
				{
					this.m_FixedPane = this.m_LeftPane;
				}
				else
				{
					this.m_FlexedPane = this.m_LeftPane;
				}
				this.m_RightPane = this.m_Content[1];
				bool flag3 = this.m_FixedPaneIndex == 1;
				if (flag3)
				{
					this.m_FixedPane = this.m_RightPane;
				}
				else
				{
					this.m_FlexedPane = this.m_RightPane;
				}
				this.m_FixedPane.style.flexBasis = StyleKeyword.Null;
				this.m_FixedPane.style.flexShrink = StyleKeyword.Null;
				this.m_FixedPane.style.flexGrow = StyleKeyword.Null;
				this.m_FlexedPane.style.flexGrow = StyleKeyword.Null;
				this.m_FlexedPane.style.flexShrink = StyleKeyword.Null;
				this.m_FlexedPane.style.flexBasis = StyleKeyword.Null;
				this.m_FixedPane.style.width = StyleKeyword.Null;
				this.m_FixedPane.style.height = StyleKeyword.Null;
				this.m_FlexedPane.style.width = StyleKeyword.Null;
				this.m_FlexedPane.style.height = StyleKeyword.Null;
				bool flag4 = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
				if (flag4)
				{
					this.m_FixedPane.style.width = this.m_FixedPaneInitialDimension;
					this.m_FixedPane.style.height = StyleKeyword.Null;
				}
				else
				{
					this.m_FixedPane.style.width = StyleKeyword.Null;
					this.m_FixedPane.style.height = this.m_FixedPaneInitialDimension;
				}
				this.m_FixedPane.style.flexShrink = 0f;
				this.m_FixedPane.style.flexGrow = 0f;
				this.m_FlexedPane.style.flexGrow = 1f;
				this.m_FlexedPane.style.flexShrink = 0f;
				this.m_FlexedPane.style.flexBasis = 0f;
				bool flag5 = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
				if (flag5)
				{
					bool flag6 = this.m_FixedPaneIndex == 0;
					if (flag6)
					{
						this.m_DragLineAnchor.style.left = this.m_FixedPaneInitialDimension;
					}
					else
					{
						this.m_DragLineAnchor.style.left = base.resolvedStyle.width - this.m_FixedPaneInitialDimension;
					}
				}
				else
				{
					bool flag7 = this.m_FixedPaneIndex == 0;
					if (flag7)
					{
						this.m_DragLineAnchor.style.top = this.m_FixedPaneInitialDimension;
					}
					else
					{
						this.m_DragLineAnchor.style.top = base.resolvedStyle.height - this.m_FixedPaneInitialDimension;
					}
				}
				bool flag8 = this.m_FixedPaneIndex == 0;
				int dir;
				if (flag8)
				{
					dir = 1;
				}
				else
				{
					dir = -1;
				}
				bool flag9 = this.m_FixedPaneIndex == 0;
				if (flag9)
				{
					this.m_Resizer = new TwoPaneSplitViewResizer(this, dir, this.m_Orientation);
				}
				else
				{
					this.m_Resizer = new TwoPaneSplitViewResizer(this, dir, this.m_Orientation);
				}
				this.m_DragLineAnchor.AddManipulator(this.m_Resizer);
				base.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnPostDisplaySetup), TrickleDown.NoTrickleDown);
				base.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnSizeChange), TrickleDown.NoTrickleDown);
			}
		}

		private void OnSizeChange(GeometryChangedEvent evt)
		{
			this.OnSizeChange();
		}

		private void OnSizeChange()
		{
			bool collapseMode = this.m_CollapseMode;
			if (!collapseMode)
			{
				float num = base.resolvedStyle.width;
				float num2 = this.m_DragLineAnchor.resolvedStyle.left;
				float num3 = this.m_FixedPane.resolvedStyle.left;
				bool flag = this.m_Orientation == TwoPaneSplitViewOrientation.Vertical;
				if (flag)
				{
					num = base.resolvedStyle.height;
					num2 = this.m_DragLineAnchor.resolvedStyle.top;
					num3 = this.m_FixedPane.resolvedStyle.top;
				}
				bool flag2 = this.m_FixedPaneIndex == 0 && num2 > num;
				if (flag2)
				{
					float delta = num - num2;
					this.m_Resizer.ApplyDelta(delta);
				}
				else
				{
					bool flag3 = this.m_FixedPaneIndex == 1;
					if (flag3)
					{
						bool flag4 = num3 < 0f;
						if (flag4)
						{
							float delta2 = -num2;
							this.m_Resizer.ApplyDelta(delta2);
						}
						else
						{
							bool flag5 = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
							if (flag5)
							{
								this.m_DragLineAnchor.style.left = num3;
							}
							else
							{
								this.m_DragLineAnchor.style.top = num3;
							}
						}
					}
				}
			}
		}
	}
}
