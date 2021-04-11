using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Yoga
{
	[NativeHeader("Modules/UIElementsNative/YogaNative.bindings.h")]
	internal static class Native
	{
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr YGNodeNew();

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr YGNodeNewWithConfig(IntPtr config);

		public static void YGNodeFree(IntPtr ygNode)
		{
			bool flag = ygNode == IntPtr.Zero;
			if (!flag)
			{
				Native.YGNodeFreeInternal(ygNode);
			}
		}

		[FreeFunction(Name = "YGNodeFree", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void YGNodeFreeInternal(IntPtr ygNode);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeReset(IntPtr node);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGSetManagedObject(IntPtr ygNode, YogaNode node);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeSetConfig(IntPtr ygNode, IntPtr config);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr YGConfigGetDefault();

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr YGConfigNew();

		public static void YGConfigFree(IntPtr config)
		{
			bool flag = config == IntPtr.Zero;
			if (!flag)
			{
				Native.YGConfigFreeInternal(config);
			}
		}

		[FreeFunction(Name = "YGConfigFree", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void YGConfigFreeInternal(IntPtr config);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int YGNodeGetInstanceCount();

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int YGConfigGetInstanceCount();

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGConfigSetExperimentalFeatureEnabled(IntPtr config, YogaExperimentalFeature feature, bool enabled);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool YGConfigIsExperimentalFeatureEnabled(IntPtr config, YogaExperimentalFeature feature);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGConfigSetUseWebDefaults(IntPtr config, bool useWebDefaults);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool YGConfigGetUseWebDefaults(IntPtr config);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGConfigSetPointScaleFactor(IntPtr config, float pixelsInPoint);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float YGConfigGetPointScaleFactor(IntPtr config);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeInsertChild(IntPtr node, IntPtr child, uint index);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeRemoveChild(IntPtr node, IntPtr child);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeCalculateLayout(IntPtr node, float availableWidth, float availableHeight, YogaDirection parentDirection);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeMarkDirty(IntPtr node);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool YGNodeIsDirty(IntPtr node);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodePrint(IntPtr node, YogaPrintOptions options);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeCopyStyle(IntPtr dstNode, IntPtr srcNode);

		[FreeFunction(Name = "YogaCallback::SetMeasureFunc")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeSetMeasureFunc(IntPtr node);

		[FreeFunction(Name = "YogaCallback::RemoveMeasureFunc")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeRemoveMeasureFunc(IntPtr node);

		[RequiredByNativeCode]
		public unsafe static void YGNodeMeasureInvoke(YogaNode node, float width, YogaMeasureMode widthMode, float height, YogaMeasureMode heightMode, IntPtr returnValueAddress)
		{
			*(YogaSize*)((void*)returnValueAddress) = YogaNode.MeasureInternal(node, width, widthMode, height, heightMode);
		}

		[FreeFunction(Name = "YogaCallback::SetBaselineFunc")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeSetBaselineFunc(IntPtr node);

		[FreeFunction(Name = "YogaCallback::RemoveBaselineFunc")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeRemoveBaselineFunc(IntPtr node);

		[RequiredByNativeCode]
		public unsafe static void YGNodeBaselineInvoke(YogaNode node, float width, float height, IntPtr returnValueAddress)
		{
			*(float*)((void*)returnValueAddress) = YogaNode.BaselineInternal(node, width, height);
		}

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeSetHasNewLayout(IntPtr node, bool hasNewLayout);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool YGNodeGetHasNewLayout(IntPtr node);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetDirection(IntPtr node, YogaDirection direction);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern YogaDirection YGNodeStyleGetDirection(IntPtr node);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetFlexDirection(IntPtr node, YogaFlexDirection flexDirection);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern YogaFlexDirection YGNodeStyleGetFlexDirection(IntPtr node);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetJustifyContent(IntPtr node, YogaJustify justifyContent);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern YogaJustify YGNodeStyleGetJustifyContent(IntPtr node);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetAlignContent(IntPtr node, YogaAlign alignContent);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern YogaAlign YGNodeStyleGetAlignContent(IntPtr node);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetAlignItems(IntPtr node, YogaAlign alignItems);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern YogaAlign YGNodeStyleGetAlignItems(IntPtr node);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetAlignSelf(IntPtr node, YogaAlign alignSelf);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern YogaAlign YGNodeStyleGetAlignSelf(IntPtr node);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetPositionType(IntPtr node, YogaPositionType positionType);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern YogaPositionType YGNodeStyleGetPositionType(IntPtr node);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetFlexWrap(IntPtr node, YogaWrap flexWrap);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern YogaWrap YGNodeStyleGetFlexWrap(IntPtr node);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetOverflow(IntPtr node, YogaOverflow flexWrap);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern YogaOverflow YGNodeStyleGetOverflow(IntPtr node);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetDisplay(IntPtr node, YogaDisplay display);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern YogaDisplay YGNodeStyleGetDisplay(IntPtr node);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetFlex(IntPtr node, float flex);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetFlexGrow(IntPtr node, float flexGrow);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float YGNodeStyleGetFlexGrow(IntPtr node);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetFlexShrink(IntPtr node, float flexShrink);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float YGNodeStyleGetFlexShrink(IntPtr node);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetFlexBasis(IntPtr node, float flexBasis);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetFlexBasisPercent(IntPtr node, float flexBasis);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetFlexBasisAuto(IntPtr node);

		[FreeFunction]
		public static YogaValue YGNodeStyleGetFlexBasis(IntPtr node)
		{
			YogaValue result;
			Native.YGNodeStyleGetFlexBasis_Injected(node, out result);
			return result;
		}

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float YGNodeGetComputedFlexBasis(IntPtr node);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetWidth(IntPtr node, float width);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetWidthPercent(IntPtr node, float width);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetWidthAuto(IntPtr node);

		[FreeFunction]
		public static YogaValue YGNodeStyleGetWidth(IntPtr node)
		{
			YogaValue result;
			Native.YGNodeStyleGetWidth_Injected(node, out result);
			return result;
		}

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetHeight(IntPtr node, float height);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetHeightPercent(IntPtr node, float height);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetHeightAuto(IntPtr node);

		[FreeFunction]
		public static YogaValue YGNodeStyleGetHeight(IntPtr node)
		{
			YogaValue result;
			Native.YGNodeStyleGetHeight_Injected(node, out result);
			return result;
		}

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetMinWidth(IntPtr node, float minWidth);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetMinWidthPercent(IntPtr node, float minWidth);

		[FreeFunction]
		public static YogaValue YGNodeStyleGetMinWidth(IntPtr node)
		{
			YogaValue result;
			Native.YGNodeStyleGetMinWidth_Injected(node, out result);
			return result;
		}

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetMinHeight(IntPtr node, float minHeight);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetMinHeightPercent(IntPtr node, float minHeight);

		[FreeFunction]
		public static YogaValue YGNodeStyleGetMinHeight(IntPtr node)
		{
			YogaValue result;
			Native.YGNodeStyleGetMinHeight_Injected(node, out result);
			return result;
		}

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetMaxWidth(IntPtr node, float maxWidth);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetMaxWidthPercent(IntPtr node, float maxWidth);

		[FreeFunction]
		public static YogaValue YGNodeStyleGetMaxWidth(IntPtr node)
		{
			YogaValue result;
			Native.YGNodeStyleGetMaxWidth_Injected(node, out result);
			return result;
		}

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetMaxHeight(IntPtr node, float maxHeight);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetMaxHeightPercent(IntPtr node, float maxHeight);

		[FreeFunction]
		public static YogaValue YGNodeStyleGetMaxHeight(IntPtr node)
		{
			YogaValue result;
			Native.YGNodeStyleGetMaxHeight_Injected(node, out result);
			return result;
		}

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetAspectRatio(IntPtr node, float aspectRatio);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float YGNodeStyleGetAspectRatio(IntPtr node);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetPosition(IntPtr node, YogaEdge edge, float position);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetPositionPercent(IntPtr node, YogaEdge edge, float position);

		[FreeFunction]
		public static YogaValue YGNodeStyleGetPosition(IntPtr node, YogaEdge edge)
		{
			YogaValue result;
			Native.YGNodeStyleGetPosition_Injected(node, edge, out result);
			return result;
		}

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetMargin(IntPtr node, YogaEdge edge, float margin);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetMarginPercent(IntPtr node, YogaEdge edge, float margin);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetMarginAuto(IntPtr node, YogaEdge edge);

		[FreeFunction]
		public static YogaValue YGNodeStyleGetMargin(IntPtr node, YogaEdge edge)
		{
			YogaValue result;
			Native.YGNodeStyleGetMargin_Injected(node, edge, out result);
			return result;
		}

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetPadding(IntPtr node, YogaEdge edge, float padding);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetPaddingPercent(IntPtr node, YogaEdge edge, float padding);

		[FreeFunction]
		public static YogaValue YGNodeStyleGetPadding(IntPtr node, YogaEdge edge)
		{
			YogaValue result;
			Native.YGNodeStyleGetPadding_Injected(node, edge, out result);
			return result;
		}

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetBorder(IntPtr node, YogaEdge edge, float border);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float YGNodeStyleGetBorder(IntPtr node, YogaEdge edge);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float YGNodeLayoutGetLeft(IntPtr node);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float YGNodeLayoutGetTop(IntPtr node);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float YGNodeLayoutGetRight(IntPtr node);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float YGNodeLayoutGetBottom(IntPtr node);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float YGNodeLayoutGetWidth(IntPtr node);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float YGNodeLayoutGetHeight(IntPtr node);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float YGNodeLayoutGetMargin(IntPtr node, YogaEdge edge);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float YGNodeLayoutGetPadding(IntPtr node, YogaEdge edge);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern YogaDirection YGNodeLayoutGetDirection(IntPtr node);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void YGNodeStyleGetFlexBasis_Injected(IntPtr node, out YogaValue ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void YGNodeStyleGetWidth_Injected(IntPtr node, out YogaValue ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void YGNodeStyleGetHeight_Injected(IntPtr node, out YogaValue ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void YGNodeStyleGetMinWidth_Injected(IntPtr node, out YogaValue ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void YGNodeStyleGetMinHeight_Injected(IntPtr node, out YogaValue ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void YGNodeStyleGetMaxWidth_Injected(IntPtr node, out YogaValue ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void YGNodeStyleGetMaxHeight_Injected(IntPtr node, out YogaValue ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void YGNodeStyleGetPosition_Injected(IntPtr node, YogaEdge edge, out YogaValue ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void YGNodeStyleGetMargin_Injected(IntPtr node, YogaEdge edge, out YogaValue ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void YGNodeStyleGetPadding_Injected(IntPtr node, YogaEdge edge, out YogaValue ret);
	}
}
