using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityEngine.Yoga
{
	internal class YogaNode : IEnumerable<YogaNode>, IEnumerable
	{
		internal IntPtr _ygNode;

		private YogaConfig _config;

		private WeakReference _parent;

		private List<YogaNode> _children;

		private MeasureFunction _measureFunction;

		private BaselineFunction _baselineFunction;

		private object _data;

		internal YogaConfig Config
		{
			get
			{
				return this._config;
			}
			set
			{
				this._config = (value ?? YogaConfig.Default);
				Native.YGNodeSetConfig(this._ygNode, this._config.Handle);
			}
		}

		public bool IsDirty
		{
			get
			{
				return Native.YGNodeIsDirty(this._ygNode);
			}
		}

		public bool HasNewLayout
		{
			get
			{
				return Native.YGNodeGetHasNewLayout(this._ygNode);
			}
		}

		public YogaNode Parent
		{
			get
			{
				return (this._parent != null) ? (this._parent.Target as YogaNode) : null;
			}
		}

		public bool IsMeasureDefined
		{
			get
			{
				return this._measureFunction != null;
			}
		}

		public bool IsBaselineDefined
		{
			get
			{
				return this._baselineFunction != null;
			}
		}

		public YogaDirection StyleDirection
		{
			get
			{
				return Native.YGNodeStyleGetDirection(this._ygNode);
			}
			set
			{
				Native.YGNodeStyleSetDirection(this._ygNode, value);
			}
		}

		public YogaFlexDirection FlexDirection
		{
			get
			{
				return Native.YGNodeStyleGetFlexDirection(this._ygNode);
			}
			set
			{
				Native.YGNodeStyleSetFlexDirection(this._ygNode, value);
			}
		}

		public YogaJustify JustifyContent
		{
			get
			{
				return Native.YGNodeStyleGetJustifyContent(this._ygNode);
			}
			set
			{
				Native.YGNodeStyleSetJustifyContent(this._ygNode, value);
			}
		}

		public YogaDisplay Display
		{
			get
			{
				return Native.YGNodeStyleGetDisplay(this._ygNode);
			}
			set
			{
				Native.YGNodeStyleSetDisplay(this._ygNode, value);
			}
		}

		public YogaAlign AlignItems
		{
			get
			{
				return Native.YGNodeStyleGetAlignItems(this._ygNode);
			}
			set
			{
				Native.YGNodeStyleSetAlignItems(this._ygNode, value);
			}
		}

		public YogaAlign AlignSelf
		{
			get
			{
				return Native.YGNodeStyleGetAlignSelf(this._ygNode);
			}
			set
			{
				Native.YGNodeStyleSetAlignSelf(this._ygNode, value);
			}
		}

		public YogaAlign AlignContent
		{
			get
			{
				return Native.YGNodeStyleGetAlignContent(this._ygNode);
			}
			set
			{
				Native.YGNodeStyleSetAlignContent(this._ygNode, value);
			}
		}

		public YogaPositionType PositionType
		{
			get
			{
				return Native.YGNodeStyleGetPositionType(this._ygNode);
			}
			set
			{
				Native.YGNodeStyleSetPositionType(this._ygNode, value);
			}
		}

		public YogaWrap Wrap
		{
			get
			{
				return Native.YGNodeStyleGetFlexWrap(this._ygNode);
			}
			set
			{
				Native.YGNodeStyleSetFlexWrap(this._ygNode, value);
			}
		}

		public float Flex
		{
			set
			{
				Native.YGNodeStyleSetFlex(this._ygNode, value);
			}
		}

		public float FlexGrow
		{
			get
			{
				return Native.YGNodeStyleGetFlexGrow(this._ygNode);
			}
			set
			{
				Native.YGNodeStyleSetFlexGrow(this._ygNode, value);
			}
		}

		public float FlexShrink
		{
			get
			{
				return Native.YGNodeStyleGetFlexShrink(this._ygNode);
			}
			set
			{
				Native.YGNodeStyleSetFlexShrink(this._ygNode, value);
			}
		}

		public YogaValue FlexBasis
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetFlexBasis(this._ygNode));
			}
			set
			{
				bool flag = value.Unit == YogaUnit.Percent;
				if (flag)
				{
					Native.YGNodeStyleSetFlexBasisPercent(this._ygNode, value.Value);
				}
				else
				{
					bool flag2 = value.Unit == YogaUnit.Auto;
					if (flag2)
					{
						Native.YGNodeStyleSetFlexBasisAuto(this._ygNode);
					}
					else
					{
						Native.YGNodeStyleSetFlexBasis(this._ygNode, value.Value);
					}
				}
			}
		}

		public YogaValue Width
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetWidth(this._ygNode));
			}
			set
			{
				bool flag = value.Unit == YogaUnit.Percent;
				if (flag)
				{
					Native.YGNodeStyleSetWidthPercent(this._ygNode, value.Value);
				}
				else
				{
					bool flag2 = value.Unit == YogaUnit.Auto;
					if (flag2)
					{
						Native.YGNodeStyleSetWidthAuto(this._ygNode);
					}
					else
					{
						Native.YGNodeStyleSetWidth(this._ygNode, value.Value);
					}
				}
			}
		}

		public YogaValue Height
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetHeight(this._ygNode));
			}
			set
			{
				bool flag = value.Unit == YogaUnit.Percent;
				if (flag)
				{
					Native.YGNodeStyleSetHeightPercent(this._ygNode, value.Value);
				}
				else
				{
					bool flag2 = value.Unit == YogaUnit.Auto;
					if (flag2)
					{
						Native.YGNodeStyleSetHeightAuto(this._ygNode);
					}
					else
					{
						Native.YGNodeStyleSetHeight(this._ygNode, value.Value);
					}
				}
			}
		}

		public YogaValue MaxWidth
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetMaxWidth(this._ygNode));
			}
			set
			{
				bool flag = value.Unit == YogaUnit.Percent;
				if (flag)
				{
					Native.YGNodeStyleSetMaxWidthPercent(this._ygNode, value.Value);
				}
				else
				{
					Native.YGNodeStyleSetMaxWidth(this._ygNode, value.Value);
				}
			}
		}

		public YogaValue MaxHeight
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetMaxHeight(this._ygNode));
			}
			set
			{
				bool flag = value.Unit == YogaUnit.Percent;
				if (flag)
				{
					Native.YGNodeStyleSetMaxHeightPercent(this._ygNode, value.Value);
				}
				else
				{
					Native.YGNodeStyleSetMaxHeight(this._ygNode, value.Value);
				}
			}
		}

		public YogaValue MinWidth
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetMinWidth(this._ygNode));
			}
			set
			{
				bool flag = value.Unit == YogaUnit.Percent;
				if (flag)
				{
					Native.YGNodeStyleSetMinWidthPercent(this._ygNode, value.Value);
				}
				else
				{
					Native.YGNodeStyleSetMinWidth(this._ygNode, value.Value);
				}
			}
		}

		public YogaValue MinHeight
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetMinHeight(this._ygNode));
			}
			set
			{
				bool flag = value.Unit == YogaUnit.Percent;
				if (flag)
				{
					Native.YGNodeStyleSetMinHeightPercent(this._ygNode, value.Value);
				}
				else
				{
					Native.YGNodeStyleSetMinHeight(this._ygNode, value.Value);
				}
			}
		}

		public float AspectRatio
		{
			get
			{
				return Native.YGNodeStyleGetAspectRatio(this._ygNode);
			}
			set
			{
				Native.YGNodeStyleSetAspectRatio(this._ygNode, value);
			}
		}

		public float LayoutX
		{
			get
			{
				return Native.YGNodeLayoutGetLeft(this._ygNode);
			}
		}

		public float LayoutY
		{
			get
			{
				return Native.YGNodeLayoutGetTop(this._ygNode);
			}
		}

		public float LayoutRight
		{
			get
			{
				return Native.YGNodeLayoutGetRight(this._ygNode);
			}
		}

		public float LayoutBottom
		{
			get
			{
				return Native.YGNodeLayoutGetBottom(this._ygNode);
			}
		}

		public float LayoutWidth
		{
			get
			{
				return Native.YGNodeLayoutGetWidth(this._ygNode);
			}
		}

		public float LayoutHeight
		{
			get
			{
				return Native.YGNodeLayoutGetHeight(this._ygNode);
			}
		}

		public YogaDirection LayoutDirection
		{
			get
			{
				return Native.YGNodeLayoutGetDirection(this._ygNode);
			}
		}

		public YogaOverflow Overflow
		{
			get
			{
				return Native.YGNodeStyleGetOverflow(this._ygNode);
			}
			set
			{
				Native.YGNodeStyleSetOverflow(this._ygNode, value);
			}
		}

		public object Data
		{
			get
			{
				return this._data;
			}
			set
			{
				this._data = value;
			}
		}

		public YogaNode this[int index]
		{
			get
			{
				return this._children[index];
			}
		}

		public int Count
		{
			get
			{
				return (this._children != null) ? this._children.Count : 0;
			}
		}

		public YogaValue Left
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPosition(this._ygNode, YogaEdge.Left));
			}
			set
			{
				this.SetStylePosition(YogaEdge.Left, value);
			}
		}

		public YogaValue Top
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPosition(this._ygNode, YogaEdge.Top));
			}
			set
			{
				this.SetStylePosition(YogaEdge.Top, value);
			}
		}

		public YogaValue Right
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPosition(this._ygNode, YogaEdge.Right));
			}
			set
			{
				this.SetStylePosition(YogaEdge.Right, value);
			}
		}

		public YogaValue Bottom
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPosition(this._ygNode, YogaEdge.Bottom));
			}
			set
			{
				this.SetStylePosition(YogaEdge.Bottom, value);
			}
		}

		public YogaValue Start
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPosition(this._ygNode, YogaEdge.Start));
			}
			set
			{
				this.SetStylePosition(YogaEdge.Start, value);
			}
		}

		public YogaValue End
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPosition(this._ygNode, YogaEdge.End));
			}
			set
			{
				this.SetStylePosition(YogaEdge.End, value);
			}
		}

		public YogaValue MarginLeft
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetMargin(this._ygNode, YogaEdge.Left));
			}
			set
			{
				this.SetStyleMargin(YogaEdge.Left, value);
			}
		}

		public YogaValue MarginTop
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetMargin(this._ygNode, YogaEdge.Top));
			}
			set
			{
				this.SetStyleMargin(YogaEdge.Top, value);
			}
		}

		public YogaValue MarginRight
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetMargin(this._ygNode, YogaEdge.Right));
			}
			set
			{
				this.SetStyleMargin(YogaEdge.Right, value);
			}
		}

		public YogaValue MarginBottom
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetMargin(this._ygNode, YogaEdge.Bottom));
			}
			set
			{
				this.SetStyleMargin(YogaEdge.Bottom, value);
			}
		}

		public YogaValue MarginStart
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetMargin(this._ygNode, YogaEdge.Start));
			}
			set
			{
				this.SetStyleMargin(YogaEdge.Start, value);
			}
		}

		public YogaValue MarginEnd
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetMargin(this._ygNode, YogaEdge.End));
			}
			set
			{
				this.SetStyleMargin(YogaEdge.End, value);
			}
		}

		public YogaValue MarginHorizontal
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetMargin(this._ygNode, YogaEdge.Horizontal));
			}
			set
			{
				this.SetStyleMargin(YogaEdge.Horizontal, value);
			}
		}

		public YogaValue MarginVertical
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetMargin(this._ygNode, YogaEdge.Vertical));
			}
			set
			{
				this.SetStyleMargin(YogaEdge.Vertical, value);
			}
		}

		public YogaValue Margin
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetMargin(this._ygNode, YogaEdge.All));
			}
			set
			{
				this.SetStyleMargin(YogaEdge.All, value);
			}
		}

		public YogaValue PaddingLeft
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPadding(this._ygNode, YogaEdge.Left));
			}
			set
			{
				this.SetStylePadding(YogaEdge.Left, value);
			}
		}

		public YogaValue PaddingTop
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPadding(this._ygNode, YogaEdge.Top));
			}
			set
			{
				this.SetStylePadding(YogaEdge.Top, value);
			}
		}

		public YogaValue PaddingRight
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPadding(this._ygNode, YogaEdge.Right));
			}
			set
			{
				this.SetStylePadding(YogaEdge.Right, value);
			}
		}

		public YogaValue PaddingBottom
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPadding(this._ygNode, YogaEdge.Bottom));
			}
			set
			{
				this.SetStylePadding(YogaEdge.Bottom, value);
			}
		}

		public YogaValue PaddingStart
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPadding(this._ygNode, YogaEdge.Start));
			}
			set
			{
				this.SetStylePadding(YogaEdge.Start, value);
			}
		}

		public YogaValue PaddingEnd
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPadding(this._ygNode, YogaEdge.End));
			}
			set
			{
				this.SetStylePadding(YogaEdge.End, value);
			}
		}

		public YogaValue PaddingHorizontal
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPadding(this._ygNode, YogaEdge.Horizontal));
			}
			set
			{
				this.SetStylePadding(YogaEdge.Horizontal, value);
			}
		}

		public YogaValue PaddingVertical
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPadding(this._ygNode, YogaEdge.Vertical));
			}
			set
			{
				this.SetStylePadding(YogaEdge.Vertical, value);
			}
		}

		public YogaValue Padding
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPadding(this._ygNode, YogaEdge.All));
			}
			set
			{
				this.SetStylePadding(YogaEdge.All, value);
			}
		}

		public float BorderLeftWidth
		{
			get
			{
				return Native.YGNodeStyleGetBorder(this._ygNode, YogaEdge.Left);
			}
			set
			{
				Native.YGNodeStyleSetBorder(this._ygNode, YogaEdge.Left, value);
			}
		}

		public float BorderTopWidth
		{
			get
			{
				return Native.YGNodeStyleGetBorder(this._ygNode, YogaEdge.Top);
			}
			set
			{
				Native.YGNodeStyleSetBorder(this._ygNode, YogaEdge.Top, value);
			}
		}

		public float BorderRightWidth
		{
			get
			{
				return Native.YGNodeStyleGetBorder(this._ygNode, YogaEdge.Right);
			}
			set
			{
				Native.YGNodeStyleSetBorder(this._ygNode, YogaEdge.Right, value);
			}
		}

		public float BorderBottomWidth
		{
			get
			{
				return Native.YGNodeStyleGetBorder(this._ygNode, YogaEdge.Bottom);
			}
			set
			{
				Native.YGNodeStyleSetBorder(this._ygNode, YogaEdge.Bottom, value);
			}
		}

		public float BorderStartWidth
		{
			get
			{
				return Native.YGNodeStyleGetBorder(this._ygNode, YogaEdge.Start);
			}
			set
			{
				Native.YGNodeStyleSetBorder(this._ygNode, YogaEdge.Start, value);
			}
		}

		public float BorderEndWidth
		{
			get
			{
				return Native.YGNodeStyleGetBorder(this._ygNode, YogaEdge.End);
			}
			set
			{
				Native.YGNodeStyleSetBorder(this._ygNode, YogaEdge.End, value);
			}
		}

		public float BorderWidth
		{
			get
			{
				return Native.YGNodeStyleGetBorder(this._ygNode, YogaEdge.All);
			}
			set
			{
				Native.YGNodeStyleSetBorder(this._ygNode, YogaEdge.All, value);
			}
		}

		public float LayoutMarginLeft
		{
			get
			{
				return Native.YGNodeLayoutGetMargin(this._ygNode, YogaEdge.Left);
			}
		}

		public float LayoutMarginTop
		{
			get
			{
				return Native.YGNodeLayoutGetMargin(this._ygNode, YogaEdge.Top);
			}
		}

		public float LayoutMarginRight
		{
			get
			{
				return Native.YGNodeLayoutGetMargin(this._ygNode, YogaEdge.Right);
			}
		}

		public float LayoutMarginBottom
		{
			get
			{
				return Native.YGNodeLayoutGetMargin(this._ygNode, YogaEdge.Bottom);
			}
		}

		public float LayoutMarginStart
		{
			get
			{
				return Native.YGNodeLayoutGetMargin(this._ygNode, YogaEdge.Start);
			}
		}

		public float LayoutMarginEnd
		{
			get
			{
				return Native.YGNodeLayoutGetMargin(this._ygNode, YogaEdge.End);
			}
		}

		public float LayoutPaddingLeft
		{
			get
			{
				return Native.YGNodeLayoutGetPadding(this._ygNode, YogaEdge.Left);
			}
		}

		public float LayoutPaddingTop
		{
			get
			{
				return Native.YGNodeLayoutGetPadding(this._ygNode, YogaEdge.Top);
			}
		}

		public float LayoutPaddingRight
		{
			get
			{
				return Native.YGNodeLayoutGetPadding(this._ygNode, YogaEdge.Right);
			}
		}

		public float LayoutPaddingBottom
		{
			get
			{
				return Native.YGNodeLayoutGetPadding(this._ygNode, YogaEdge.Bottom);
			}
		}

		public float LayoutPaddingStart
		{
			get
			{
				return Native.YGNodeLayoutGetPadding(this._ygNode, YogaEdge.Start);
			}
		}

		public float LayoutPaddingEnd
		{
			get
			{
				return Native.YGNodeLayoutGetPadding(this._ygNode, YogaEdge.End);
			}
		}

		public float ComputedFlexBasis
		{
			get
			{
				return Native.YGNodeGetComputedFlexBasis(this._ygNode);
			}
		}

		public YogaNode(YogaConfig config = null)
		{
			this._config = ((config == null) ? YogaConfig.Default : config);
			this._ygNode = Native.YGNodeNewWithConfig(this._config.Handle);
			bool flag = this._ygNode == IntPtr.Zero;
			if (flag)
			{
				throw new InvalidOperationException("Failed to allocate native memory");
			}
		}

		public YogaNode(YogaNode srcNode) : this(srcNode._config)
		{
			this.CopyStyle(srcNode);
		}

		~YogaNode()
		{
			Native.YGNodeFree(this._ygNode);
		}

		public void Reset()
		{
			this._measureFunction = null;
			this._baselineFunction = null;
			this._data = null;
			Native.YGSetManagedObject(this._ygNode, null);
			Native.YGNodeReset(this._ygNode);
		}

		public virtual void MarkDirty()
		{
			Native.YGNodeMarkDirty(this._ygNode);
		}

		public void MarkHasNewLayout()
		{
			Native.YGNodeSetHasNewLayout(this._ygNode, true);
		}

		public void CopyStyle(YogaNode srcNode)
		{
			Native.YGNodeCopyStyle(this._ygNode, srcNode._ygNode);
		}

		public void MarkLayoutSeen()
		{
			Native.YGNodeSetHasNewLayout(this._ygNode, false);
		}

		public bool ValuesEqual(float f1, float f2)
		{
			bool flag = float.IsNaN(f1) || float.IsNaN(f2);
			bool result;
			if (flag)
			{
				result = (float.IsNaN(f1) && float.IsNaN(f2));
			}
			else
			{
				result = (Math.Abs(f2 - f1) < 1.401298E-45f);
			}
			return result;
		}

		public void Insert(int index, YogaNode node)
		{
			bool flag = this._children == null;
			if (flag)
			{
				this._children = new List<YogaNode>(4);
			}
			this._children.Insert(index, node);
			node._parent = new WeakReference(this);
			Native.YGNodeInsertChild(this._ygNode, node._ygNode, (uint)index);
		}

		public void RemoveAt(int index)
		{
			YogaNode yogaNode = this._children[index];
			yogaNode._parent = null;
			this._children.RemoveAt(index);
			Native.YGNodeRemoveChild(this._ygNode, yogaNode._ygNode);
		}

		public void AddChild(YogaNode child)
		{
			this.Insert(this.Count, child);
		}

		public void RemoveChild(YogaNode child)
		{
			int num = this.IndexOf(child);
			bool flag = num >= 0;
			if (flag)
			{
				this.RemoveAt(num);
			}
		}

		public void Clear()
		{
			bool flag = this._children != null;
			if (flag)
			{
				while (this._children.Count > 0)
				{
					this.RemoveAt(this._children.Count - 1);
				}
			}
		}

		public int IndexOf(YogaNode node)
		{
			return (this._children != null) ? this._children.IndexOf(node) : -1;
		}

		public void SetMeasureFunction(MeasureFunction measureFunction)
		{
			this._measureFunction = measureFunction;
			bool flag = measureFunction == null;
			if (flag)
			{
				bool flag2 = !this.IsBaselineDefined;
				if (flag2)
				{
					Native.YGSetManagedObject(this._ygNode, null);
				}
				Native.YGNodeRemoveMeasureFunc(this._ygNode);
			}
			else
			{
				Native.YGSetManagedObject(this._ygNode, this);
				Native.YGNodeSetMeasureFunc(this._ygNode);
			}
		}

		public void SetBaselineFunction(BaselineFunction baselineFunction)
		{
			this._baselineFunction = baselineFunction;
			bool flag = baselineFunction == null;
			if (flag)
			{
				bool flag2 = !this.IsMeasureDefined;
				if (flag2)
				{
					Native.YGSetManagedObject(this._ygNode, null);
				}
				Native.YGNodeRemoveBaselineFunc(this._ygNode);
			}
			else
			{
				Native.YGSetManagedObject(this._ygNode, this);
				Native.YGNodeSetBaselineFunc(this._ygNode);
			}
		}

		public void CalculateLayout(float width = float.NaN, float height = float.NaN)
		{
			Native.YGNodeCalculateLayout(this._ygNode, width, height, Native.YGNodeStyleGetDirection(this._ygNode));
		}

		public static YogaSize MeasureInternal(YogaNode node, float width, YogaMeasureMode widthMode, float height, YogaMeasureMode heightMode)
		{
			bool flag = node == null || node._measureFunction == null;
			if (flag)
			{
				throw new InvalidOperationException("Measure function is not defined.");
			}
			return node._measureFunction(node, width, widthMode, height, heightMode);
		}

		public static float BaselineInternal(YogaNode node, float width, float height)
		{
			bool flag = node == null || node._baselineFunction == null;
			if (flag)
			{
				throw new InvalidOperationException("Baseline function is not defined.");
			}
			return node._baselineFunction(node, width, height);
		}

		public string Print(YogaPrintOptions options = YogaPrintOptions.Layout | YogaPrintOptions.Style | YogaPrintOptions.Children)
		{
			StringBuilder sb = new StringBuilder();
			Logger logger = this._config.Logger;
			this._config.Logger = delegate(YogaConfig config, YogaNode node, YogaLogLevel level, string message)
			{
				sb.Append(message);
			};
			Native.YGNodePrint(this._ygNode, options);
			this._config.Logger = logger;
			return sb.ToString();
		}

		public IEnumerator<YogaNode> GetEnumerator()
		{
			return (this._children != null) ? ((IEnumerable<YogaNode>)this._children).GetEnumerator() : Enumerable.Empty<YogaNode>().GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return (this._children != null) ? ((IEnumerable<YogaNode>)this._children).GetEnumerator() : Enumerable.Empty<YogaNode>().GetEnumerator();
		}

		public static int GetInstanceCount()
		{
			return Native.YGNodeGetInstanceCount();
		}

		private void SetStylePosition(YogaEdge edge, YogaValue value)
		{
			bool flag = value.Unit == YogaUnit.Percent;
			if (flag)
			{
				Native.YGNodeStyleSetPositionPercent(this._ygNode, edge, value.Value);
			}
			else
			{
				Native.YGNodeStyleSetPosition(this._ygNode, edge, value.Value);
			}
		}

		private void SetStyleMargin(YogaEdge edge, YogaValue value)
		{
			bool flag = value.Unit == YogaUnit.Percent;
			if (flag)
			{
				Native.YGNodeStyleSetMarginPercent(this._ygNode, edge, value.Value);
			}
			else
			{
				bool flag2 = value.Unit == YogaUnit.Auto;
				if (flag2)
				{
					Native.YGNodeStyleSetMarginAuto(this._ygNode, edge);
				}
				else
				{
					Native.YGNodeStyleSetMargin(this._ygNode, edge, value.Value);
				}
			}
		}

		private void SetStylePadding(YogaEdge edge, YogaValue value)
		{
			bool flag = value.Unit == YogaUnit.Percent;
			if (flag)
			{
				Native.YGNodeStyleSetPaddingPercent(this._ygNode, edge, value.Value);
			}
			else
			{
				Native.YGNodeStyleSetPadding(this._ygNode, edge, value.Value);
			}
		}
	}
}
