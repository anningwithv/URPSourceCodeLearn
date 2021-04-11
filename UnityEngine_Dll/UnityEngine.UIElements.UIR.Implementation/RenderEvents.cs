using System;
using System.Collections.Generic;
using Unity.Collections;

namespace UnityEngine.UIElements.UIR.Implementation
{
	internal static class RenderEvents
	{
		internal static void ProcessOnClippingChanged(RenderChain renderChain, VisualElement ve, uint dirtyID, ref ChainBuilderStats stats)
		{
			bool flag = (ve.renderChainData.dirtiedValues & RenderDataDirtyTypes.ClippingHierarchy) > RenderDataDirtyTypes.None;
			bool flag2 = flag;
			if (flag2)
			{
				stats.recursiveClipUpdates += 1u;
			}
			else
			{
				stats.nonRecursiveClipUpdates += 1u;
			}
			RenderEvents.DepthFirstOnClippingChanged(renderChain, ve.hierarchy.parent, ve, dirtyID, flag, true, false, false, false, renderChain.device, ref stats);
		}

		internal static void ProcessOnOpacityChanged(RenderChain renderChain, VisualElement ve, uint dirtyID, ref ChainBuilderStats stats)
		{
			stats.recursiveOpacityUpdates += 1u;
			RenderEvents.DepthFirstOnOpacityChanged(renderChain, (ve.hierarchy.parent != null) ? ve.hierarchy.parent.renderChainData.compositeOpacity : 1f, ve, dirtyID, ref stats, false);
		}

		internal static void ProcessOnTransformOrSizeChanged(RenderChain renderChain, VisualElement ve, uint dirtyID, ref ChainBuilderStats stats)
		{
			stats.recursiveTransformUpdates += 1u;
			RenderEvents.DepthFirstOnTransformOrSizeChanged(renderChain, ve.hierarchy.parent, ve, dirtyID, renderChain.device, false, false, ref stats);
		}

		internal static void ProcessOnVisualsChanged(RenderChain renderChain, VisualElement ve, uint dirtyID, ref ChainBuilderStats stats)
		{
			bool flag = (ve.renderChainData.dirtiedValues & RenderDataDirtyTypes.VisualsHierarchy) > RenderDataDirtyTypes.None;
			bool flag2 = flag;
			if (flag2)
			{
				stats.recursiveVisualUpdates += 1u;
			}
			else
			{
				stats.nonRecursiveVisualUpdates += 1u;
			}
			VisualElement parent = ve.hierarchy.parent;
			bool parentHierarchyHidden = parent != null && (parent.renderChainData.isHierarchyHidden || RenderEvents.IsElementHierarchyHidden(parent));
			RenderEvents.DepthFirstOnVisualsChanged(renderChain, ve, dirtyID, parentHierarchyHidden, flag, ref stats);
		}

		internal static void ProcessRegenText(RenderChain renderChain, VisualElement ve, UIRTextUpdatePainter painter, UIRenderDevice device, ref ChainBuilderStats stats)
		{
			stats.textUpdates += 1u;
			painter.Begin(ve, device);
			ve.InvokeGenerateVisualContent(painter.meshGenerationContext);
			painter.End();
		}

		private static Matrix4x4 GetTransformIDTransformInfo(VisualElement ve)
		{
			Debug.Assert(RenderChainVEData.AllocatesID(ve.renderChainData.transformID) || (ve.renderHints & RenderHints.GroupTransform) > RenderHints.None);
			bool flag = ve.renderChainData.groupTransformAncestor != null;
			Matrix4x4 result;
			if (flag)
			{
				result = ve.renderChainData.groupTransformAncestor.worldTransform.inverse * ve.worldTransform;
			}
			else
			{
				result = ve.worldTransform;
			}
			result.m22 = (result.m33 = 1f);
			return result;
		}

		private static Vector4 GetClipRectIDClipInfo(VisualElement ve)
		{
			Debug.Assert(RenderChainVEData.AllocatesID(ve.renderChainData.clipRectID));
			bool flag = ve.renderChainData.groupTransformAncestor == null;
			Vector4 result;
			if (flag)
			{
				result = UIRUtility.ToVector4(ve.worldClip);
			}
			else
			{
				Rect worldClipMinusGroup = ve.worldClipMinusGroup;
				Matrix4x4 inverse = ve.renderChainData.groupTransformAncestor.worldTransform.inverse;
				Vector3 vector = inverse.MultiplyPoint3x4(new Vector3(worldClipMinusGroup.xMin, worldClipMinusGroup.yMin, 0f));
				Vector3 vector2 = inverse.MultiplyPoint3x4(new Vector3(worldClipMinusGroup.xMax, worldClipMinusGroup.yMax, 0f));
				result = new Vector4(Mathf.Min(vector.x, vector2.x), Mathf.Min(vector.y, vector2.y), Mathf.Max(vector.x, vector2.x), Mathf.Max(vector.y, vector2.y));
			}
			return result;
		}

		private static void GetVerticesTransformInfo(VisualElement ve, out Matrix4x4 transform)
		{
			bool flag = RenderChainVEData.AllocatesID(ve.renderChainData.transformID) || (ve.renderHints & RenderHints.GroupTransform) > RenderHints.None;
			if (flag)
			{
				transform = Matrix4x4.identity;
			}
			else
			{
				bool flag2 = ve.renderChainData.boneTransformAncestor != null;
				if (flag2)
				{
					transform = ve.renderChainData.boneTransformAncestor.worldTransform.inverse * ve.worldTransform;
				}
				else
				{
					bool flag3 = ve.renderChainData.groupTransformAncestor != null;
					if (flag3)
					{
						transform = ve.renderChainData.groupTransformAncestor.worldTransform.inverse * ve.worldTransform;
					}
					else
					{
						transform = ve.worldTransform;
					}
				}
			}
			transform.m22 = (transform.m33 = 1f);
		}

		internal static uint DepthFirstOnChildAdded(RenderChain renderChain, VisualElement parent, VisualElement ve, int index, bool resetState)
		{
			Debug.Assert(ve.panel != null);
			bool isInChain = ve.renderChainData.isInChain;
			uint result;
			if (isInChain)
			{
				result = 0u;
			}
			else
			{
				if (resetState)
				{
					ve.renderChainData = default(RenderChainVEData);
				}
				ve.renderChainData.isInChain = true;
				ve.renderChainData.verticesSpace = Matrix4x4.identity;
				ve.renderChainData.transformID = UIRVEShaderInfoAllocator.identityTransform;
				ve.renderChainData.clipRectID = UIRVEShaderInfoAllocator.infiniteClipRect;
				ve.renderChainData.opacityID = UIRVEShaderInfoAllocator.fullOpacity;
				ve.renderChainData.compositeOpacity = 3.40282347E+38f;
				bool flag = parent != null;
				if (flag)
				{
					bool flag2 = (parent.renderHints & RenderHints.GroupTransform) > RenderHints.None;
					if (flag2)
					{
						ve.renderChainData.groupTransformAncestor = parent;
					}
					else
					{
						ve.renderChainData.groupTransformAncestor = parent.renderChainData.groupTransformAncestor;
					}
					ve.renderChainData.hierarchyDepth = parent.renderChainData.hierarchyDepth + 1;
				}
				else
				{
					ve.renderChainData.groupTransformAncestor = null;
					ve.renderChainData.hierarchyDepth = 0;
				}
				renderChain.EnsureFitsDepth(ve.renderChainData.hierarchyDepth);
				bool flag3 = index > 0;
				if (flag3)
				{
					Debug.Assert(parent != null);
					ve.renderChainData.prev = RenderEvents.GetLastDeepestChild(parent.hierarchy[index - 1]);
				}
				else
				{
					ve.renderChainData.prev = parent;
				}
				ve.renderChainData.next = ((ve.renderChainData.prev != null) ? ve.renderChainData.prev.renderChainData.next : null);
				bool flag4 = ve.renderChainData.prev != null;
				if (flag4)
				{
					ve.renderChainData.prev.renderChainData.next = ve;
				}
				bool flag5 = ve.renderChainData.next != null;
				if (flag5)
				{
					ve.renderChainData.next.renderChainData.prev = ve;
				}
				Debug.Assert(!RenderChainVEData.AllocatesID(ve.renderChainData.transformID));
				bool flag6 = RenderEvents.NeedsTransformID(ve);
				if (flag6)
				{
					ve.renderChainData.transformID = renderChain.shaderInfoAllocator.AllocTransform();
				}
				else
				{
					ve.renderChainData.transformID = BMPAlloc.Invalid;
				}
				ve.renderChainData.boneTransformAncestor = null;
				bool flag7 = !RenderChainVEData.AllocatesID(ve.renderChainData.transformID);
				if (flag7)
				{
					bool flag8 = parent != null && (ve.renderHints & RenderHints.GroupTransform) == RenderHints.None;
					if (flag8)
					{
						bool flag9 = RenderChainVEData.AllocatesID(parent.renderChainData.transformID);
						if (flag9)
						{
							ve.renderChainData.boneTransformAncestor = parent;
						}
						else
						{
							ve.renderChainData.boneTransformAncestor = parent.renderChainData.boneTransformAncestor;
						}
						ve.renderChainData.transformID = parent.renderChainData.transformID;
						ve.renderChainData.transformID.ownedState = OwnedState.Inherited;
					}
					else
					{
						ve.renderChainData.transformID = UIRVEShaderInfoAllocator.identityTransform;
					}
				}
				else
				{
					renderChain.shaderInfoAllocator.SetTransformValue(ve.renderChainData.transformID, RenderEvents.GetTransformIDTransformInfo(ve));
				}
				int childCount = ve.hierarchy.childCount;
				uint num = 0u;
				for (int i = 0; i < childCount; i++)
				{
					num += RenderEvents.DepthFirstOnChildAdded(renderChain, ve, ve.hierarchy[i], i, resetState);
				}
				result = 1u + num;
			}
			return result;
		}

		internal static uint DepthFirstOnChildRemoving(RenderChain renderChain, VisualElement ve)
		{
			int i = ve.hierarchy.childCount - 1;
			uint num = 0u;
			while (i >= 0)
			{
				num += RenderEvents.DepthFirstOnChildRemoving(renderChain, ve.hierarchy[i--]);
			}
			bool flag = (ve.renderHints & RenderHints.GroupTransform) > RenderHints.None;
			if (flag)
			{
				renderChain.StopTrackingGroupTransformElement(ve);
			}
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				renderChain.ChildWillBeRemoved(ve);
				RenderEvents.ResetCommands(renderChain, ve);
				ve.renderChainData.isInChain = false;
				ve.renderChainData.clipMethod = ClipMethod.Undetermined;
				bool flag2 = ve.renderChainData.next != null;
				if (flag2)
				{
					ve.renderChainData.next.renderChainData.prev = ve.renderChainData.prev;
				}
				bool flag3 = ve.renderChainData.prev != null;
				if (flag3)
				{
					ve.renderChainData.prev.renderChainData.next = ve.renderChainData.next;
				}
				bool flag4 = RenderChainVEData.AllocatesID(ve.renderChainData.opacityID);
				if (flag4)
				{
					renderChain.shaderInfoAllocator.FreeOpacity(ve.renderChainData.opacityID);
					ve.renderChainData.opacityID = UIRVEShaderInfoAllocator.fullOpacity;
				}
				bool flag5 = RenderChainVEData.AllocatesID(ve.renderChainData.clipRectID);
				if (flag5)
				{
					renderChain.shaderInfoAllocator.FreeClipRect(ve.renderChainData.clipRectID);
					ve.renderChainData.clipRectID = UIRVEShaderInfoAllocator.infiniteClipRect;
				}
				bool flag6 = RenderChainVEData.AllocatesID(ve.renderChainData.transformID);
				if (flag6)
				{
					renderChain.shaderInfoAllocator.FreeTransform(ve.renderChainData.transformID);
					ve.renderChainData.transformID = UIRVEShaderInfoAllocator.identityTransform;
				}
				ve.renderChainData.boneTransformAncestor = (ve.renderChainData.groupTransformAncestor = null);
				bool flag7 = ve.renderChainData.closingData != null;
				if (flag7)
				{
					renderChain.device.Free(ve.renderChainData.closingData);
					ve.renderChainData.closingData = null;
				}
				bool flag8 = ve.renderChainData.data != null;
				if (flag8)
				{
					renderChain.device.Free(ve.renderChainData.data);
					ve.renderChainData.data = null;
				}
			}
			return num + 1u;
		}

		private static void DepthFirstOnClippingChanged(RenderChain renderChain, VisualElement parent, VisualElement ve, uint dirtyID, bool hierarchical, bool isRootOfChange, bool isPendingHierarchicalRepaint, bool inheritedClipRectIDChanged, bool inheritedStencilClippedChanged, UIRenderDevice device, ref ChainBuilderStats stats)
		{
			bool flag = dirtyID == ve.renderChainData.dirtyID;
			bool flag2 = flag && !inheritedClipRectIDChanged && !inheritedStencilClippedChanged;
			if (!flag2)
			{
				ve.renderChainData.dirtyID = dirtyID;
				bool flag3 = !isRootOfChange;
				if (flag3)
				{
					stats.recursiveClipUpdatesExpanded += 1u;
				}
				isPendingHierarchicalRepaint |= ((ve.renderChainData.dirtiedValues & RenderDataDirtyTypes.VisualsHierarchy) > RenderDataDirtyTypes.None);
				bool flag4 = hierarchical | isRootOfChange | inheritedClipRectIDChanged;
				bool flag5 = hierarchical | isRootOfChange;
				bool flag6 = hierarchical | isRootOfChange | inheritedStencilClippedChanged;
				bool flag7 = false;
				bool flag8 = false;
				bool flag9 = false;
				bool flag10 = hierarchical;
				ClipMethod clipMethod = ve.renderChainData.clipMethod;
				ClipMethod clipMethod2 = flag5 ? RenderEvents.DetermineSelfClipMethod(renderChain, ve) : clipMethod;
				bool flag11 = false;
				bool flag12 = flag4;
				if (flag12)
				{
					BMPAlloc bMPAlloc = ve.renderChainData.clipRectID;
					bool flag13 = clipMethod2 == ClipMethod.ShaderDiscard;
					if (flag13)
					{
						bool flag14 = !RenderChainVEData.AllocatesID(ve.renderChainData.clipRectID);
						if (flag14)
						{
							bMPAlloc = renderChain.shaderInfoAllocator.AllocClipRect();
							bool flag15 = !bMPAlloc.IsValid();
							if (flag15)
							{
								clipMethod2 = ClipMethod.Scissor;
								bMPAlloc = UIRVEShaderInfoAllocator.infiniteClipRect;
							}
						}
					}
					else
					{
						bool flag16 = RenderChainVEData.AllocatesID(ve.renderChainData.clipRectID);
						if (flag16)
						{
							renderChain.shaderInfoAllocator.FreeClipRect(ve.renderChainData.clipRectID);
						}
						bool flag17 = (ve.renderHints & RenderHints.GroupTransform) == RenderHints.None;
						if (flag17)
						{
							bMPAlloc = ((clipMethod2 != ClipMethod.Scissor && parent != null) ? parent.renderChainData.clipRectID : UIRVEShaderInfoAllocator.infiniteClipRect);
							bMPAlloc.ownedState = OwnedState.Inherited;
						}
					}
					flag11 = !ve.renderChainData.clipRectID.Equals(bMPAlloc);
					Debug.Assert((ve.renderHints & RenderHints.GroupTransform) == RenderHints.None || !flag11);
					ve.renderChainData.clipRectID = bMPAlloc;
				}
				bool flag18 = clipMethod != clipMethod2;
				if (flag18)
				{
					ve.renderChainData.clipMethod = clipMethod2;
					bool flag19 = clipMethod == ClipMethod.Stencil || clipMethod2 == ClipMethod.Stencil;
					if (flag19)
					{
						flag6 = true;
						flag8 = true;
					}
					bool flag20 = clipMethod == ClipMethod.Scissor || clipMethod2 == ClipMethod.Scissor;
					if (flag20)
					{
						flag7 = true;
					}
					bool flag21 = clipMethod2 == ClipMethod.ShaderDiscard || (clipMethod == ClipMethod.ShaderDiscard && RenderChainVEData.AllocatesID(ve.renderChainData.clipRectID));
					if (flag21)
					{
						flag9 = true;
					}
				}
				bool flag22 = flag11;
				if (flag22)
				{
					flag10 = true;
					flag8 = true;
				}
				bool inheritedStencilClippedChanged2 = false;
				bool flag23 = flag6;
				if (flag23)
				{
					bool isStencilClipped = ve.renderChainData.isStencilClipped;
					bool flag24 = clipMethod2 == ClipMethod.Stencil || (parent != null && parent.renderChainData.isStencilClipped);
					ve.renderChainData.isStencilClipped = flag24;
					bool flag25 = isStencilClipped != flag24;
					if (flag25)
					{
						inheritedStencilClippedChanged2 = true;
						flag10 = true;
					}
				}
				bool flag26 = (flag7 | flag8) && !isPendingHierarchicalRepaint;
				if (flag26)
				{
					renderChain.UIEOnVisualsChanged(ve, flag8);
					isPendingHierarchicalRepaint = true;
				}
				bool flag27 = flag9;
				if (flag27)
				{
					renderChain.UIEOnTransformOrSizeChanged(ve, false, true);
				}
				bool flag28 = flag10;
				if (flag28)
				{
					int childCount = ve.hierarchy.childCount;
					for (int i = 0; i < childCount; i++)
					{
						RenderEvents.DepthFirstOnClippingChanged(renderChain, ve, ve.hierarchy[i], dirtyID, hierarchical, false, isPendingHierarchicalRepaint, flag11, inheritedStencilClippedChanged2, device, ref stats);
					}
				}
			}
		}

		private static void DepthFirstOnOpacityChanged(RenderChain renderChain, float parentCompositeOpacity, VisualElement ve, uint dirtyID, ref ChainBuilderStats stats, bool isDoingFullVertexRegeneration = false)
		{
			bool flag = dirtyID == ve.renderChainData.dirtyID;
			if (!flag)
			{
				ve.renderChainData.dirtyID = dirtyID;
				stats.recursiveOpacityUpdatesExpanded += 1u;
				float compositeOpacity = ve.renderChainData.compositeOpacity;
				float num = ve.resolvedStyle.opacity * parentCompositeOpacity;
				bool flag2 = Mathf.Abs(compositeOpacity - num) > 0.0001f;
				bool flag3 = flag2;
				if (flag3)
				{
					ve.renderChainData.compositeOpacity = num;
				}
				bool flag4 = false;
				bool flag5 = num < parentCompositeOpacity - 0.0001f;
				bool flag6 = flag5;
				if (flag6)
				{
					bool flag7 = ve.renderChainData.opacityID.ownedState == OwnedState.Inherited;
					if (flag7)
					{
						flag4 = true;
						ve.renderChainData.opacityID = renderChain.shaderInfoAllocator.AllocOpacity();
					}
					bool flag8 = (flag4 | flag2) && ve.renderChainData.opacityID.IsValid();
					if (flag8)
					{
						renderChain.shaderInfoAllocator.SetOpacityValue(ve.renderChainData.opacityID, num);
					}
				}
				else
				{
					bool flag9 = ve.renderChainData.opacityID.ownedState == OwnedState.Inherited;
					if (flag9)
					{
						bool flag10 = ve.hierarchy.parent != null && !ve.renderChainData.opacityID.Equals(ve.hierarchy.parent.renderChainData.opacityID);
						if (flag10)
						{
							flag4 = true;
							ve.renderChainData.opacityID = ve.hierarchy.parent.renderChainData.opacityID;
							ve.renderChainData.opacityID.ownedState = OwnedState.Inherited;
						}
					}
					else
					{
						bool flag11 = flag2 && ve.renderChainData.opacityID.IsValid();
						if (flag11)
						{
							renderChain.shaderInfoAllocator.SetOpacityValue(ve.renderChainData.opacityID, num);
						}
					}
				}
				bool flag12 = isDoingFullVertexRegeneration;
				if (!flag12)
				{
					bool flag13 = compositeOpacity < Mathf.Epsilon && num >= Mathf.Epsilon;
					if (flag13)
					{
						renderChain.UIEOnVisualsChanged(ve, true);
						isDoingFullVertexRegeneration = true;
					}
					else
					{
						bool flag14 = flag4 && (ve.renderChainData.dirtiedValues & RenderDataDirtyTypes.Visuals) == RenderDataDirtyTypes.None;
						if (flag14)
						{
							renderChain.UIEOnVisualsChanged(ve, false);
						}
					}
				}
				bool flag15 = flag2 | flag4;
				if (flag15)
				{
					int childCount = ve.hierarchy.childCount;
					for (int i = 0; i < childCount; i++)
					{
						RenderEvents.DepthFirstOnOpacityChanged(renderChain, num, ve.hierarchy[i], dirtyID, ref stats, isDoingFullVertexRegeneration);
					}
				}
			}
		}

		private static void DepthFirstOnTransformOrSizeChanged(RenderChain renderChain, VisualElement parent, VisualElement ve, uint dirtyID, UIRenderDevice device, bool isAncestorOfChangeSkinned, bool transformChanged, ref ChainBuilderStats stats)
		{
			bool flag = dirtyID == ve.renderChainData.dirtyID;
			if (!flag)
			{
				stats.recursiveTransformUpdatesExpanded += 1u;
				transformChanged |= ((ve.renderChainData.dirtiedValues & RenderDataDirtyTypes.Transform) > RenderDataDirtyTypes.None);
				bool flag2 = RenderChainVEData.AllocatesID(ve.renderChainData.clipRectID);
				if (flag2)
				{
					renderChain.shaderInfoAllocator.SetClipRectValue(ve.renderChainData.clipRectID, RenderEvents.GetClipRectIDClipInfo(ve));
				}
				bool flag3 = true;
				bool flag4 = RenderChainVEData.AllocatesID(ve.renderChainData.transformID);
				if (flag4)
				{
					renderChain.shaderInfoAllocator.SetTransformValue(ve.renderChainData.transformID, RenderEvents.GetTransformIDTransformInfo(ve));
					isAncestorOfChangeSkinned = true;
					stats.boneTransformed += 1u;
				}
				else
				{
					bool flag5 = !transformChanged;
					if (!flag5)
					{
						bool flag6 = (ve.renderHints & RenderHints.GroupTransform) > RenderHints.None;
						if (flag6)
						{
							stats.groupTransformElementsChanged += 1u;
						}
						else
						{
							bool flag7 = isAncestorOfChangeSkinned;
							if (flag7)
							{
								Debug.Assert(RenderChainVEData.InheritsID(ve.renderChainData.transformID));
								flag3 = false;
								stats.skipTransformed += 1u;
							}
							else
							{
								bool flag8 = (ve.renderChainData.dirtiedValues & (RenderDataDirtyTypes.Visuals | RenderDataDirtyTypes.VisualsHierarchy)) == RenderDataDirtyTypes.None && ve.renderChainData.data != null;
								if (flag8)
								{
									bool flag9 = !ve.renderChainData.disableNudging && RenderEvents.NudgeVerticesToNewSpace(ve, device);
									if (flag9)
									{
										stats.nudgeTransformed += 1u;
									}
									else
									{
										renderChain.UIEOnVisualsChanged(ve, false);
										stats.visualUpdateTransformed += 1u;
									}
								}
							}
						}
					}
				}
				bool flag10 = flag3;
				if (flag10)
				{
					ve.renderChainData.dirtyID = dirtyID;
				}
				bool drawInCameras = renderChain.drawInCameras;
				if (drawInCameras)
				{
					ve.EnsureWorldTransformAndClipUpToDate();
				}
				bool flag11 = (ve.renderHints & RenderHints.GroupTransform) == RenderHints.None;
				if (flag11)
				{
					int childCount = ve.hierarchy.childCount;
					for (int i = 0; i < childCount; i++)
					{
						RenderEvents.DepthFirstOnTransformOrSizeChanged(renderChain, ve, ve.hierarchy[i], dirtyID, device, isAncestorOfChangeSkinned, transformChanged, ref stats);
					}
				}
				else
				{
					renderChain.OnGroupTransformElementChangedTransform(ve);
				}
			}
		}

		private static void DepthFirstOnVisualsChanged(RenderChain renderChain, VisualElement ve, uint dirtyID, bool parentHierarchyHidden, bool hierarchical, ref ChainBuilderStats stats)
		{
			bool flag = dirtyID == ve.renderChainData.dirtyID;
			if (!flag)
			{
				ve.renderChainData.dirtyID = dirtyID;
				bool flag2 = hierarchical;
				if (flag2)
				{
					stats.recursiveVisualUpdatesExpanded += 1u;
				}
				bool isHierarchyHidden = ve.renderChainData.isHierarchyHidden;
				ve.renderChainData.isHierarchyHidden = (parentHierarchyHidden || RenderEvents.IsElementHierarchyHidden(ve));
				bool flag3 = isHierarchyHidden != ve.renderChainData.isHierarchyHidden;
				if (flag3)
				{
					hierarchical = true;
				}
				Debug.Assert(ve.renderChainData.clipMethod > ClipMethod.Undetermined);
				Debug.Assert(RenderChainVEData.AllocatesID(ve.renderChainData.transformID) || ve.hierarchy.parent == null || ve.renderChainData.transformID.Equals(ve.hierarchy.parent.renderChainData.transformID) || (ve.renderHints & RenderHints.GroupTransform) > RenderHints.None);
				UIRStylePainter.ClosingInfo closingInfo = RenderEvents.PaintElement(renderChain, ve, ref stats);
				bool flag4 = hierarchical;
				if (flag4)
				{
					int childCount = ve.hierarchy.childCount;
					for (int i = 0; i < childCount; i++)
					{
						RenderEvents.DepthFirstOnVisualsChanged(renderChain, ve.hierarchy[i], dirtyID, ve.renderChainData.isHierarchyHidden, true, ref stats);
					}
				}
				bool needsClosing = closingInfo.needsClosing;
				if (needsClosing)
				{
					RenderEvents.ClosePaintElement(ve, closingInfo, renderChain, ref stats);
				}
			}
		}

		private static bool IsElementHierarchyHidden(VisualElement ve)
		{
			return ve.resolvedStyle.opacity < Mathf.Epsilon || ve.resolvedStyle.display == DisplayStyle.None;
		}

		private static bool IsElementSelfHidden(VisualElement ve)
		{
			return ve.resolvedStyle.visibility == Visibility.Hidden;
		}

		private static VisualElement GetLastDeepestChild(VisualElement ve)
		{
			for (int i = ve.hierarchy.childCount; i > 0; i = ve.hierarchy.childCount)
			{
				ve = ve.hierarchy[i - 1];
			}
			return ve;
		}

		private static VisualElement GetNextDepthFirst(VisualElement ve)
		{
			VisualElement result;
			for (VisualElement parent = ve.hierarchy.parent; parent != null; parent = parent.hierarchy.parent)
			{
				int num = parent.hierarchy.IndexOf(ve);
				int childCount = parent.hierarchy.childCount;
				bool flag = num < childCount - 1;
				if (flag)
				{
					result = parent.hierarchy[num + 1];
					return result;
				}
				ve = parent;
			}
			result = null;
			return result;
		}

		private static bool IsParentOrAncestorOf(this VisualElement ve, VisualElement child)
		{
			bool result;
			while (child.hierarchy.parent != null)
			{
				bool flag = child.hierarchy.parent == ve;
				if (flag)
				{
					result = true;
					return result;
				}
				child = child.hierarchy.parent;
			}
			result = false;
			return result;
		}

		private static ClipMethod DetermineSelfClipMethod(RenderChain renderChain, VisualElement ve)
		{
			bool flag = !ve.ShouldClip();
			ClipMethod result;
			if (flag)
			{
				result = ClipMethod.NotClipped;
			}
			else
			{
				bool flag2 = !UIRUtility.IsRoundRect(ve) && !UIRUtility.IsVectorImageBackground(ve);
				if (flag2)
				{
					bool flag3 = (ve.renderHints & (RenderHints.GroupTransform | RenderHints.ClipWithScissors)) > RenderHints.None;
					if (flag3)
					{
						result = ClipMethod.Scissor;
					}
					else
					{
						result = ClipMethod.ShaderDiscard;
					}
				}
				else
				{
					VisualElement expr_51 = ve.hierarchy.parent;
					bool flag4 = expr_51 != null && expr_51.renderChainData.isStencilClipped;
					if (flag4)
					{
						result = ClipMethod.ShaderDiscard;
					}
					else
					{
						result = (renderChain.drawInCameras ? ClipMethod.ShaderDiscard : ClipMethod.Stencil);
					}
				}
			}
			return result;
		}

		private static bool NeedsTransformID(VisualElement ve)
		{
			return (ve.renderHints & RenderHints.GroupTransform) == RenderHints.None && (ve.renderHints & RenderHints.BoneTransform) == RenderHints.BoneTransform;
		}

		private static bool TransformIDHasChanged(Alloc before, Alloc after)
		{
			bool flag = before.size == 0u && after.size == 0u;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = before.size != after.size || before.start != after.start;
				result = flag2;
			}
			return result;
		}

		internal static UIRStylePainter.ClosingInfo PaintElement(RenderChain renderChain, VisualElement ve, ref ChainBuilderStats stats)
		{
			bool flag = ve.renderChainData.clipMethod == ClipMethod.Stencil;
			bool flag2 = (RenderEvents.IsElementSelfHidden(ve) && !flag) || ve.renderChainData.isHierarchyHidden;
			UIRStylePainter.ClosingInfo result;
			if (flag2)
			{
				bool flag3 = ve.renderChainData.data != null;
				if (flag3)
				{
					renderChain.painter.device.Free(ve.renderChainData.data);
					ve.renderChainData.data = null;
				}
				bool flag4 = ve.renderChainData.firstCommand != null;
				if (flag4)
				{
					RenderEvents.ResetCommands(renderChain, ve);
				}
				UIRStylePainter.ClosingInfo closingInfo = default(UIRStylePainter.ClosingInfo);
				result = closingInfo;
			}
			else
			{
				RenderChainCommand expr_A6 = ve.renderChainData.firstCommand;
				RenderChainCommand renderChainCommand = (expr_A6 != null) ? expr_A6.prev : null;
				RenderChainCommand expr_BE = ve.renderChainData.lastCommand;
				RenderChainCommand renderChainCommand2 = (expr_BE != null) ? expr_BE.next : null;
				bool flag5 = ve.renderChainData.firstClosingCommand != null && renderChainCommand2 == ve.renderChainData.firstClosingCommand;
				bool flag6 = flag5;
				RenderChainCommand renderChainCommand4;
				RenderChainCommand renderChainCommand3;
				if (flag6)
				{
					renderChainCommand2 = ve.renderChainData.lastClosingCommand.next;
					renderChainCommand3 = (renderChainCommand4 = null);
				}
				else
				{
					RenderChainCommand expr_119 = ve.renderChainData.firstClosingCommand;
					renderChainCommand4 = ((expr_119 != null) ? expr_119.prev : null);
					RenderChainCommand expr_131 = ve.renderChainData.lastClosingCommand;
					renderChainCommand3 = ((expr_131 != null) ? expr_131.next : null);
				}
				Debug.Assert(((renderChainCommand != null) ? renderChainCommand.owner : null) != ve);
				Debug.Assert(((renderChainCommand2 != null) ? renderChainCommand2.owner : null) != ve);
				Debug.Assert(((renderChainCommand4 != null) ? renderChainCommand4.owner : null) != ve);
				Debug.Assert(((renderChainCommand3 != null) ? renderChainCommand3.owner : null) != ve);
				RenderEvents.ResetCommands(renderChain, ve);
				UIRStylePainter painter = renderChain.painter;
				painter.Begin(ve);
				bool visible = ve.visible;
				if (visible)
				{
					painter.DrawVisualElementBackground();
					painter.DrawVisualElementBorder();
					painter.ApplyVisualElementClipping();
					ve.InvokeGenerateVisualContent(painter.meshGenerationContext);
				}
				else
				{
					bool flag7 = ve.renderChainData.clipMethod == ClipMethod.Stencil;
					if (flag7)
					{
						painter.ApplyVisualElementClipping();
					}
				}
				MeshHandle meshHandle = ve.renderChainData.data;
				bool flag8 = (long)painter.totalVertices > (long)((ulong)renderChain.device.maxVerticesPerPage);
				if (flag8)
				{
					Debug.LogError(string.Format("A {0} must not allocate more than {1} vertices.", "VisualElement", renderChain.device.maxVerticesPerPage));
					bool flag9 = meshHandle != null;
					if (flag9)
					{
						painter.device.Free(meshHandle);
						meshHandle = null;
					}
					painter.Reset();
					painter.Begin(ve);
				}
				List<UIRStylePainter.Entry> entries = painter.entries;
				bool flag10 = entries.Count > 0;
				if (flag10)
				{
					NativeSlice<Vertex> nativeSlice = default(NativeSlice<Vertex>);
					NativeSlice<ushort> thisSlice = default(NativeSlice<ushort>);
					ushort num = 0;
					bool flag11 = painter.totalVertices > 0;
					if (flag11)
					{
						RenderEvents.UpdateOrAllocate(ref meshHandle, painter.totalVertices, painter.totalIndices, painter.device, out nativeSlice, out thisSlice, out num, ref stats);
					}
					int num2 = 0;
					int num3 = 0;
					RenderChainCommand renderChainCommand5 = renderChainCommand;
					RenderChainCommand renderChainCommand6 = renderChainCommand2;
					bool flag12 = renderChainCommand == null && renderChainCommand2 == null;
					if (flag12)
					{
						RenderEvents.FindCommandInsertionPoint(ve, out renderChainCommand5, out renderChainCommand6);
					}
					bool flag13 = false;
					Matrix4x4 identity = Matrix4x4.identity;
					Color32 xformClipPages = new Color32(0, 0, 0, 0);
					Color32 idsAddFlags = new Color32(0, 0, 0, 0);
					Color32 opacityPage = new Color32(0, 0, 0, 0);
					int num4 = -1;
					int num5 = -1;
					foreach (UIRStylePainter.Entry current in painter.entries)
					{
						NativeSlice<Vertex> vertices = current.vertices;
						bool arg_392_0;
						if (vertices.Length > 0)
						{
							NativeSlice<ushort> indices = current.indices;
							arg_392_0 = (indices.Length > 0);
						}
						else
						{
							arg_392_0 = false;
						}
						bool flag14 = arg_392_0;
						if (flag14)
						{
							bool flag15 = !flag13;
							if (flag15)
							{
								flag13 = true;
								RenderEvents.GetVerticesTransformInfo(ve, out identity);
								ve.renderChainData.verticesSpace = identity;
								Color32 color = renderChain.shaderInfoAllocator.TransformAllocToVertexData(ve.renderChainData.transformID);
								Color32 color2 = renderChain.shaderInfoAllocator.OpacityAllocToVertexData(ve.renderChainData.opacityID);
								xformClipPages.r = color.r;
								xformClipPages.g = color.g;
								idsAddFlags.r = color.b;
								opacityPage.r = color2.r;
								opacityPage.g = color2.g;
								idsAddFlags.b = color2.b;
							}
							Color32 color3 = renderChain.shaderInfoAllocator.ClipRectAllocToVertexData(current.clipRectID);
							xformClipPages.b = color3.r;
							xformClipPages.a = color3.g;
							idsAddFlags.g = color3.b;
							idsAddFlags.a = (byte)current.addFlags;
							NativeSlice<Vertex> arg_4AA_0 = nativeSlice;
							int arg_4AA_1 = num2;
							vertices = current.vertices;
							NativeSlice<Vertex> nativeSlice2 = arg_4AA_0.Slice(arg_4AA_1, vertices.Length);
							bool uvIsDisplacement = current.uvIsDisplacement;
							if (uvIsDisplacement)
							{
								bool flag16 = num4 < 0;
								if (flag16)
								{
									num4 = num2;
									int arg_4E1_0 = num2;
									vertices = current.vertices;
									num5 = arg_4E1_0 + vertices.Length;
								}
								else
								{
									bool flag17 = num5 == num2;
									if (flag17)
									{
										int arg_505_0 = num5;
										vertices = current.vertices;
										num5 = arg_505_0 + vertices.Length;
									}
									else
									{
										ve.renderChainData.disableNudging = true;
									}
								}
								RenderEvents.CopyTransformVertsPosAndVec(current.vertices, nativeSlice2, identity, xformClipPages, idsAddFlags, opacityPage);
							}
							else
							{
								RenderEvents.CopyTransformVertsPos(current.vertices, nativeSlice2, identity, xformClipPages, idsAddFlags, opacityPage);
							}
							NativeSlice<ushort> indices = current.indices;
							int length = indices.Length;
							int indexOffset = num2 + (int)num;
							NativeSlice<ushort> nativeSlice3 = thisSlice.Slice(num3, length);
							bool flag18 = current.isClipRegisterEntry || !current.isStencilClipped;
							if (flag18)
							{
								RenderEvents.CopyTriangleIndices(current.indices, nativeSlice3, indexOffset);
							}
							else
							{
								RenderEvents.CopyTriangleIndicesFlipWindingOrder(current.indices, nativeSlice3, indexOffset);
							}
							bool isClipRegisterEntry = current.isClipRegisterEntry;
							if (isClipRegisterEntry)
							{
								painter.LandClipRegisterMesh(nativeSlice2, nativeSlice3, indexOffset);
							}
							RenderChainCommand command = RenderEvents.InjectMeshDrawCommand(renderChain, ve, ref renderChainCommand5, ref renderChainCommand6, meshHandle, length, num3, current.material, current.custom, current.font);
							bool flag19 = current.isTextEntry && ve.renderChainData.usesLegacyText;
							if (flag19)
							{
								bool flag20 = ve.renderChainData.textEntries == null;
								if (flag20)
								{
									ve.renderChainData.textEntries = new List<RenderChainTextEntry>(1);
								}
								List<RenderChainTextEntry> arg_671_0 = ve.renderChainData.textEntries;
								RenderChainTextEntry item = default(RenderChainTextEntry);
								item.command = command;
								item.firstVertex = num2;
								vertices = current.vertices;
								item.vertexCount = vertices.Length;
								arg_671_0.Add(item);
							}
							int arg_68A_0 = num2;
							vertices = current.vertices;
							num2 = arg_68A_0 + vertices.Length;
							num3 += length;
						}
						else
						{
							bool flag21 = current.customCommand != null;
							if (flag21)
							{
								RenderEvents.InjectCommandInBetween(renderChain, current.customCommand, ref renderChainCommand5, ref renderChainCommand6);
							}
							else
							{
								Debug.Assert(false);
							}
						}
					}
					bool flag22 = !ve.renderChainData.disableNudging && num4 >= 0;
					if (flag22)
					{
						ve.renderChainData.displacementUVStart = num4;
						ve.renderChainData.displacementUVEnd = num5;
					}
				}
				else
				{
					bool flag23 = meshHandle != null;
					if (flag23)
					{
						painter.device.Free(meshHandle);
						meshHandle = null;
					}
				}
				ve.renderChainData.data = meshHandle;
				bool usesLegacyText = ve.renderChainData.usesLegacyText;
				if (usesLegacyText)
				{
					renderChain.AddTextElement(ve);
				}
				UIRStylePainter.ClosingInfo closingInfo = painter.closingInfo;
				bool flag24 = closingInfo.clipperRegisterIndices.Length == 0 && ve.renderChainData.closingData != null;
				if (flag24)
				{
					painter.device.Free(ve.renderChainData.closingData);
					ve.renderChainData.closingData = null;
				}
				bool needsClosing = painter.closingInfo.needsClosing;
				if (needsClosing)
				{
					RenderChainCommand renderChainCommand7 = renderChainCommand4;
					RenderChainCommand renderChainCommand8 = renderChainCommand3;
					bool flag25 = flag5;
					if (flag25)
					{
						renderChainCommand7 = ve.renderChainData.lastCommand;
						renderChainCommand8 = renderChainCommand7.next;
					}
					else
					{
						bool flag26 = renderChainCommand7 == null && renderChainCommand8 == null;
						if (flag26)
						{
							RenderEvents.FindClosingCommandInsertionPoint(ve, out renderChainCommand7, out renderChainCommand8);
						}
					}
					closingInfo = painter.closingInfo;
					bool flag27 = closingInfo.clipperRegisterIndices.Length > 0;
					if (flag27)
					{
						painter.LandClipUnregisterMeshDrawCommand(RenderEvents.InjectClosingMeshDrawCommand(renderChain, ve, ref renderChainCommand7, ref renderChainCommand8, null, 0, 0, null, null, null));
					}
					bool popViewMatrix = painter.closingInfo.popViewMatrix;
					if (popViewMatrix)
					{
						RenderChainCommand renderChainCommand9 = renderChain.AllocCommand();
						renderChainCommand9.type = CommandType.PopView;
						renderChainCommand9.closing = true;
						renderChainCommand9.owner = ve;
						RenderEvents.InjectClosingCommandInBetween(renderChain, renderChainCommand9, ref renderChainCommand7, ref renderChainCommand8);
					}
					bool popScissorClip = painter.closingInfo.popScissorClip;
					if (popScissorClip)
					{
						RenderChainCommand renderChainCommand10 = renderChain.AllocCommand();
						renderChainCommand10.type = CommandType.PopScissor;
						renderChainCommand10.closing = true;
						renderChainCommand10.owner = ve;
						RenderEvents.InjectClosingCommandInBetween(renderChain, renderChainCommand10, ref renderChainCommand7, ref renderChainCommand8);
					}
				}
				UIRStylePainter.ClosingInfo closingInfo2 = painter.closingInfo;
				painter.Reset();
				result = closingInfo2;
			}
			return result;
		}

		private static void ClosePaintElement(VisualElement ve, UIRStylePainter.ClosingInfo closingInfo, RenderChain renderChain, ref ChainBuilderStats stats)
		{
			bool flag = closingInfo.clipperRegisterIndices.Length > 0;
			if (flag)
			{
				NativeSlice<Vertex> nativeSlice = default(NativeSlice<Vertex>);
				NativeSlice<ushort> target = default(NativeSlice<ushort>);
				ushort num = 0;
				RenderEvents.UpdateOrAllocate(ref ve.renderChainData.closingData, closingInfo.clipperRegisterVertices.Length, closingInfo.clipperRegisterIndices.Length, renderChain.painter.device, out nativeSlice, out target, out num, ref stats);
				nativeSlice.CopyFrom(closingInfo.clipperRegisterVertices);
				RenderEvents.CopyTriangleIndicesFlipWindingOrder(closingInfo.clipperRegisterIndices, target, (int)num - closingInfo.clipperRegisterIndexOffset);
				closingInfo.clipUnregisterDrawCommand.mesh = ve.renderChainData.closingData;
				closingInfo.clipUnregisterDrawCommand.indexCount = target.Length;
			}
		}

		private static void UpdateOrAllocate(ref MeshHandle data, int vertexCount, int indexCount, UIRenderDevice device, out NativeSlice<Vertex> verts, out NativeSlice<ushort> indices, out ushort indexOffset, ref ChainBuilderStats stats)
		{
			bool flag = data != null;
			if (flag)
			{
				bool flag2 = (ulong)data.allocVerts.size >= (ulong)((long)vertexCount) && (ulong)data.allocIndices.size >= (ulong)((long)indexCount);
				if (flag2)
				{
					device.Update(data, (uint)vertexCount, (uint)indexCount, out verts, out indices, out indexOffset);
					stats.updatedMeshAllocations += 1u;
				}
				else
				{
					device.Free(data);
					data = device.Allocate((uint)vertexCount, (uint)indexCount, out verts, out indices, out indexOffset);
					stats.newMeshAllocations += 1u;
				}
			}
			else
			{
				data = device.Allocate((uint)vertexCount, (uint)indexCount, out verts, out indices, out indexOffset);
				stats.newMeshAllocations += 1u;
			}
		}

		private static void CopyTransformVertsPos(NativeSlice<Vertex> source, NativeSlice<Vertex> target, Matrix4x4 mat, Color32 xformClipPages, Color32 idsAddFlags, Color32 opacityPage)
		{
			int length = source.Length;
			for (int i = 0; i < length; i++)
			{
				Vertex vertex = source[i];
				vertex.position = mat.MultiplyPoint3x4(vertex.position);
				vertex.xformClipPages = xformClipPages;
				vertex.idsFlags.r = idsAddFlags.r;
				vertex.idsFlags.g = idsAddFlags.g;
				vertex.idsFlags.b = idsAddFlags.b;
				vertex.idsFlags.a = vertex.idsFlags.a + idsAddFlags.a;
				vertex.opacityPageSVGSettingIndex.r = opacityPage.r;
				vertex.opacityPageSVGSettingIndex.g = opacityPage.g;
				target[i] = vertex;
			}
		}

		private static void CopyTransformVertsPosAndVec(NativeSlice<Vertex> source, NativeSlice<Vertex> target, Matrix4x4 mat, Color32 xformClipPages, Color32 idsAddFlags, Color32 opacityPage)
		{
			int length = source.Length;
			Vector3 vector = new Vector3(0f, 0f, 0f);
			for (int i = 0; i < length; i++)
			{
				Vertex vertex = source[i];
				vertex.position = mat.MultiplyPoint3x4(vertex.position);
				vector.x = vertex.uv.x;
				vector.y = vertex.uv.y;
				vertex.uv = mat.MultiplyVector(vector);
				vertex.xformClipPages = xformClipPages;
				vertex.idsFlags.r = idsAddFlags.r;
				vertex.idsFlags.g = idsAddFlags.g;
				vertex.idsFlags.b = idsAddFlags.b;
				vertex.idsFlags.a = vertex.idsFlags.a + idsAddFlags.a;
				vertex.opacityPageSVGSettingIndex.r = opacityPage.r;
				vertex.opacityPageSVGSettingIndex.g = opacityPage.g;
				target[i] = vertex;
			}
		}

		private static void CopyTriangleIndicesFlipWindingOrder(NativeSlice<ushort> source, NativeSlice<ushort> target)
		{
			Debug.Assert(source != target);
			int length = source.Length;
			for (int i = 0; i < length; i += 3)
			{
				ushort value = source[i];
				target[i] = source[i + 1];
				target[i + 1] = value;
				target[i + 2] = source[i + 2];
			}
		}

		private static void CopyTriangleIndicesFlipWindingOrder(NativeSlice<ushort> source, NativeSlice<ushort> target, int indexOffset)
		{
			Debug.Assert(source != target);
			int length = source.Length;
			for (int i = 0; i < length; i += 3)
			{
				ushort value = (ushort)((int)source[i] + indexOffset);
				target[i] = (ushort)((int)source[i + 1] + indexOffset);
				target[i + 1] = value;
				target[i + 2] = (ushort)((int)source[i + 2] + indexOffset);
			}
		}

		private static void CopyTriangleIndices(NativeSlice<ushort> source, NativeSlice<ushort> target, int indexOffset)
		{
			int length = source.Length;
			for (int i = 0; i < length; i++)
			{
				target[i] = (ushort)((int)source[i] + indexOffset);
			}
		}

		private static bool NudgeVerticesToNewSpace(VisualElement ve, UIRenderDevice device)
		{
			Debug.Assert(!ve.renderChainData.disableNudging);
			Matrix4x4 matrix4x;
			RenderEvents.GetVerticesTransformInfo(ve, out matrix4x);
			Matrix4x4 lhs = matrix4x * ve.renderChainData.verticesSpace.inverse;
			Matrix4x4 matrix4x2 = lhs * ve.renderChainData.verticesSpace;
			float num = Mathf.Abs(matrix4x.m00 - matrix4x2.m00);
			num += Mathf.Abs(matrix4x.m01 - matrix4x2.m01);
			num += Mathf.Abs(matrix4x.m02 - matrix4x2.m02);
			num += Mathf.Abs(matrix4x.m03 - matrix4x2.m03);
			num += Mathf.Abs(matrix4x.m10 - matrix4x2.m10);
			num += Mathf.Abs(matrix4x.m11 - matrix4x2.m11);
			num += Mathf.Abs(matrix4x.m12 - matrix4x2.m12);
			num += Mathf.Abs(matrix4x.m13 - matrix4x2.m13);
			num += Mathf.Abs(matrix4x.m20 - matrix4x2.m20);
			num += Mathf.Abs(matrix4x.m21 - matrix4x2.m21);
			num += Mathf.Abs(matrix4x.m22 - matrix4x2.m22);
			num += Mathf.Abs(matrix4x.m23 - matrix4x2.m23);
			bool flag = num > 0.0001f;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				ve.renderChainData.verticesSpace = matrix4x;
				int size = (int)ve.renderChainData.data.allocVerts.size;
				NativeSlice<Vertex> nativeSlice = ve.renderChainData.data.allocPage.vertices.cpuData.Slice((int)ve.renderChainData.data.allocVerts.start, size);
				NativeSlice<Vertex> nativeSlice2;
				device.Update(ve.renderChainData.data, (uint)size, out nativeSlice2);
				int displacementUVStart = ve.renderChainData.displacementUVStart;
				int displacementUVEnd = ve.renderChainData.displacementUVEnd;
				for (int i = 0; i < displacementUVStart; i++)
				{
					Vertex vertex = nativeSlice[i];
					vertex.position = lhs.MultiplyPoint3x4(vertex.position);
					nativeSlice2[i] = vertex;
				}
				for (int j = displacementUVStart; j < displacementUVEnd; j++)
				{
					Vertex vertex2 = nativeSlice[j];
					vertex2.position = lhs.MultiplyPoint3x4(vertex2.position);
					vertex2.uv = lhs.MultiplyVector(vertex2.uv);
					nativeSlice2[j] = vertex2;
				}
				for (int k = displacementUVEnd; k < size; k++)
				{
					Vertex vertex3 = nativeSlice[k];
					vertex3.position = lhs.MultiplyPoint3x4(vertex3.position);
					nativeSlice2[k] = vertex3;
				}
				result = true;
			}
			return result;
		}

		private static RenderChainCommand InjectMeshDrawCommand(RenderChain renderChain, VisualElement ve, ref RenderChainCommand cmdPrev, ref RenderChainCommand cmdNext, MeshHandle mesh, int indexCount, int indexOffset, Material material, Texture custom, Texture font)
		{
			RenderChainCommand renderChainCommand = renderChain.AllocCommand();
			renderChainCommand.type = CommandType.Draw;
			renderChainCommand.state = new State
			{
				material = material,
				custom = custom,
				font = font
			};
			renderChainCommand.mesh = mesh;
			renderChainCommand.indexOffset = indexOffset;
			renderChainCommand.indexCount = indexCount;
			renderChainCommand.owner = ve;
			RenderEvents.InjectCommandInBetween(renderChain, renderChainCommand, ref cmdPrev, ref cmdNext);
			return renderChainCommand;
		}

		private static RenderChainCommand InjectClosingMeshDrawCommand(RenderChain renderChain, VisualElement ve, ref RenderChainCommand cmdPrev, ref RenderChainCommand cmdNext, MeshHandle mesh, int indexCount, int indexOffset, Material material, Texture custom, Texture font)
		{
			RenderChainCommand renderChainCommand = renderChain.AllocCommand();
			renderChainCommand.type = CommandType.Draw;
			renderChainCommand.closing = true;
			renderChainCommand.state = new State
			{
				material = material,
				custom = custom,
				font = font
			};
			renderChainCommand.mesh = mesh;
			renderChainCommand.indexOffset = indexOffset;
			renderChainCommand.indexCount = indexCount;
			renderChainCommand.owner = ve;
			RenderEvents.InjectClosingCommandInBetween(renderChain, renderChainCommand, ref cmdPrev, ref cmdNext);
			return renderChainCommand;
		}

		private static void FindCommandInsertionPoint(VisualElement ve, out RenderChainCommand prev, out RenderChainCommand next)
		{
			VisualElement prev2 = ve.renderChainData.prev;
			while (prev2 != null && prev2.renderChainData.lastCommand == null)
			{
				prev2 = prev2.renderChainData.prev;
			}
			bool flag = prev2 != null && prev2.renderChainData.lastCommand != null;
			if (flag)
			{
				bool flag2 = prev2.hierarchy.parent == ve.hierarchy.parent;
				if (flag2)
				{
					prev = prev2.renderChainData.lastClosingOrLastCommand;
				}
				else
				{
					bool flag3 = prev2.IsParentOrAncestorOf(ve);
					if (flag3)
					{
						prev = prev2.renderChainData.lastCommand;
					}
					else
					{
						RenderChainCommand renderChainCommand = prev2.renderChainData.lastClosingOrLastCommand;
						bool flag5;
						do
						{
							prev = renderChainCommand;
							renderChainCommand = renderChainCommand.next;
							bool flag4 = renderChainCommand == null || renderChainCommand.owner == ve || !renderChainCommand.closing;
							if (flag4)
							{
								break;
							}
							flag5 = renderChainCommand.owner.IsParentOrAncestorOf(ve);
						}
						while (!flag5);
					}
				}
				next = prev.next;
			}
			else
			{
				VisualElement next2 = ve.renderChainData.next;
				while (next2 != null && next2.renderChainData.firstCommand == null)
				{
					next2 = next2.renderChainData.next;
				}
				next = ((next2 != null) ? next2.renderChainData.firstCommand : null);
				prev = null;
				Debug.Assert(next == null || next.prev == null);
			}
		}

		private static void FindClosingCommandInsertionPoint(VisualElement ve, out RenderChainCommand prev, out RenderChainCommand next)
		{
			VisualElement visualElement = ve.renderChainData.next;
			while (visualElement != null && visualElement.renderChainData.firstCommand == null)
			{
				visualElement = visualElement.renderChainData.next;
			}
			bool flag = visualElement != null && visualElement.renderChainData.firstCommand != null;
			if (flag)
			{
				bool flag2 = visualElement.hierarchy.parent == ve.hierarchy.parent;
				if (flag2)
				{
					next = visualElement.renderChainData.firstCommand;
					prev = next.prev;
				}
				else
				{
					bool flag3 = ve.IsParentOrAncestorOf(visualElement);
					if (flag3)
					{
						bool flag4;
						do
						{
							prev = visualElement.renderChainData.lastClosingOrLastCommand;
							RenderChainCommand expr_B2 = prev.next;
							visualElement = ((expr_B2 != null) ? expr_B2.owner : null);
							flag4 = (visualElement == null || !ve.IsParentOrAncestorOf(visualElement));
						}
						while (!flag4);
						next = prev.next;
					}
					else
					{
						prev = ve.renderChainData.lastCommand;
						next = prev.next;
					}
				}
			}
			else
			{
				prev = ve.renderChainData.lastCommand;
				next = prev.next;
			}
		}

		private static void InjectCommandInBetween(RenderChain renderChain, RenderChainCommand cmd, ref RenderChainCommand prev, ref RenderChainCommand next)
		{
			bool flag = prev != null;
			if (flag)
			{
				cmd.prev = prev;
				prev.next = cmd;
			}
			bool flag2 = next != null;
			if (flag2)
			{
				cmd.next = next;
				next.prev = cmd;
			}
			VisualElement owner = cmd.owner;
			owner.renderChainData.lastCommand = cmd;
			bool flag3 = owner.renderChainData.firstCommand == null;
			if (flag3)
			{
				owner.renderChainData.firstCommand = cmd;
			}
			renderChain.OnRenderCommandAdded(cmd);
			prev = cmd;
			next = cmd.next;
		}

		private static void InjectClosingCommandInBetween(RenderChain renderChain, RenderChainCommand cmd, ref RenderChainCommand prev, ref RenderChainCommand next)
		{
			Debug.Assert(cmd.closing);
			bool flag = prev != null;
			if (flag)
			{
				cmd.prev = prev;
				prev.next = cmd;
			}
			bool flag2 = next != null;
			if (flag2)
			{
				cmd.next = next;
				next.prev = cmd;
			}
			VisualElement owner = cmd.owner;
			owner.renderChainData.lastClosingCommand = cmd;
			bool flag3 = owner.renderChainData.firstClosingCommand == null;
			if (flag3)
			{
				owner.renderChainData.firstClosingCommand = cmd;
			}
			renderChain.OnRenderCommandAdded(cmd);
			prev = cmd;
			next = cmd.next;
		}

		private static void ResetCommands(RenderChain renderChain, VisualElement ve)
		{
			bool flag = ve.renderChainData.firstCommand != null;
			if (flag)
			{
				renderChain.OnRenderCommandsRemoved(ve.renderChainData.firstCommand, ve.renderChainData.lastCommand);
			}
			RenderChainCommand renderChainCommand = (ve.renderChainData.firstCommand != null) ? ve.renderChainData.firstCommand.prev : null;
			RenderChainCommand renderChainCommand2 = (ve.renderChainData.lastCommand != null) ? ve.renderChainData.lastCommand.next : null;
			Debug.Assert(renderChainCommand == null || renderChainCommand.owner != ve);
			Debug.Assert(renderChainCommand2 == null || renderChainCommand2 == ve.renderChainData.firstClosingCommand || renderChainCommand2.owner != ve);
			bool flag2 = renderChainCommand != null;
			if (flag2)
			{
				renderChainCommand.next = renderChainCommand2;
			}
			bool flag3 = renderChainCommand2 != null;
			if (flag3)
			{
				renderChainCommand2.prev = renderChainCommand;
			}
			bool flag4 = ve.renderChainData.firstCommand != null;
			if (flag4)
			{
				RenderChainCommand renderChainCommand3;
				RenderChainCommand next;
				for (renderChainCommand3 = ve.renderChainData.firstCommand; renderChainCommand3 != ve.renderChainData.lastCommand; renderChainCommand3 = next)
				{
					next = renderChainCommand3.next;
					renderChain.FreeCommand(renderChainCommand3);
				}
				renderChain.FreeCommand(renderChainCommand3);
			}
			ve.renderChainData.firstCommand = (ve.renderChainData.lastCommand = null);
			renderChainCommand = ((ve.renderChainData.firstClosingCommand != null) ? ve.renderChainData.firstClosingCommand.prev : null);
			renderChainCommand2 = ((ve.renderChainData.lastClosingCommand != null) ? ve.renderChainData.lastClosingCommand.next : null);
			Debug.Assert(renderChainCommand == null || renderChainCommand.owner != ve);
			Debug.Assert(renderChainCommand2 == null || renderChainCommand2.owner != ve);
			bool flag5 = renderChainCommand != null;
			if (flag5)
			{
				renderChainCommand.next = renderChainCommand2;
			}
			bool flag6 = renderChainCommand2 != null;
			if (flag6)
			{
				renderChainCommand2.prev = renderChainCommand;
			}
			bool flag7 = ve.renderChainData.firstClosingCommand != null;
			if (flag7)
			{
				renderChain.OnRenderCommandsRemoved(ve.renderChainData.firstClosingCommand, ve.renderChainData.lastClosingCommand);
				RenderChainCommand renderChainCommand4;
				RenderChainCommand next2;
				for (renderChainCommand4 = ve.renderChainData.firstClosingCommand; renderChainCommand4 != ve.renderChainData.lastClosingCommand; renderChainCommand4 = next2)
				{
					next2 = renderChainCommand4.next;
					renderChain.FreeCommand(renderChainCommand4);
				}
				renderChain.FreeCommand(renderChainCommand4);
			}
			ve.renderChainData.firstClosingCommand = (ve.renderChainData.lastClosingCommand = null);
			bool usesLegacyText = ve.renderChainData.usesLegacyText;
			if (usesLegacyText)
			{
				Debug.Assert(ve.renderChainData.textEntries.Count > 0);
				renderChain.RemoveTextElement(ve);
				ve.renderChainData.textEntries.Clear();
				ve.renderChainData.usesLegacyText = false;
			}
		}
	}
}
