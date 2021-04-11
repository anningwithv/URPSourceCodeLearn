using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Transform/RectTransform.h"), NativeClass("UI::RectTransform")]
	public sealed class RectTransform : Transform
	{
		public enum Edge
		{
			Left,
			Right,
			Top,
			Bottom
		}

		public enum Axis
		{
			Horizontal,
			Vertical
		}

		public delegate void ReapplyDrivenProperties(RectTransform driven);

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event RectTransform.ReapplyDrivenProperties reapplyDrivenProperties;

		public Rect rect
		{
			get
			{
				Rect result;
				this.get_rect_Injected(out result);
				return result;
			}
		}

		public Vector2 anchorMin
		{
			get
			{
				Vector2 result;
				this.get_anchorMin_Injected(out result);
				return result;
			}
			set
			{
				this.set_anchorMin_Injected(ref value);
			}
		}

		public Vector2 anchorMax
		{
			get
			{
				Vector2 result;
				this.get_anchorMax_Injected(out result);
				return result;
			}
			set
			{
				this.set_anchorMax_Injected(ref value);
			}
		}

		public Vector2 anchoredPosition
		{
			get
			{
				Vector2 result;
				this.get_anchoredPosition_Injected(out result);
				return result;
			}
			set
			{
				this.set_anchoredPosition_Injected(ref value);
			}
		}

		public Vector2 sizeDelta
		{
			get
			{
				Vector2 result;
				this.get_sizeDelta_Injected(out result);
				return result;
			}
			set
			{
				this.set_sizeDelta_Injected(ref value);
			}
		}

		public Vector2 pivot
		{
			get
			{
				Vector2 result;
				this.get_pivot_Injected(out result);
				return result;
			}
			set
			{
				this.set_pivot_Injected(ref value);
			}
		}

		public Vector3 anchoredPosition3D
		{
			get
			{
				Vector2 anchoredPosition = this.anchoredPosition;
				return new Vector3(anchoredPosition.x, anchoredPosition.y, base.localPosition.z);
			}
			set
			{
				this.anchoredPosition = new Vector2(value.x, value.y);
				Vector3 localPosition = base.localPosition;
				localPosition.z = value.z;
				base.localPosition = localPosition;
			}
		}

		public Vector2 offsetMin
		{
			get
			{
				return this.anchoredPosition - Vector2.Scale(this.sizeDelta, this.pivot);
			}
			set
			{
				Vector2 vector = value - (this.anchoredPosition - Vector2.Scale(this.sizeDelta, this.pivot));
				this.sizeDelta -= vector;
				this.anchoredPosition += Vector2.Scale(vector, Vector2.one - this.pivot);
			}
		}

		public Vector2 offsetMax
		{
			get
			{
				return this.anchoredPosition + Vector2.Scale(this.sizeDelta, Vector2.one - this.pivot);
			}
			set
			{
				Vector2 vector = value - (this.anchoredPosition + Vector2.Scale(this.sizeDelta, Vector2.one - this.pivot));
				this.sizeDelta += vector;
				this.anchoredPosition += Vector2.Scale(vector, this.pivot);
			}
		}

		internal extern Object drivenByObject
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal extern DrivenTransformProperties drivenProperties
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeMethod("UpdateIfTransformDispatchIsDirty")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ForceUpdateRectTransforms();

		public void GetLocalCorners(Vector3[] fourCornersArray)
		{
			bool flag = fourCornersArray == null || fourCornersArray.Length < 4;
			if (flag)
			{
				Debug.LogError("Calling GetLocalCorners with an array that is null or has less than 4 elements.");
			}
			else
			{
				Rect rect = this.rect;
				float x = rect.x;
				float y = rect.y;
				float xMax = rect.xMax;
				float yMax = rect.yMax;
				fourCornersArray[0] = new Vector3(x, y, 0f);
				fourCornersArray[1] = new Vector3(x, yMax, 0f);
				fourCornersArray[2] = new Vector3(xMax, yMax, 0f);
				fourCornersArray[3] = new Vector3(xMax, y, 0f);
			}
		}

		public void GetWorldCorners(Vector3[] fourCornersArray)
		{
			bool flag = fourCornersArray == null || fourCornersArray.Length < 4;
			if (flag)
			{
				Debug.LogError("Calling GetWorldCorners with an array that is null or has less than 4 elements.");
			}
			else
			{
				this.GetLocalCorners(fourCornersArray);
				Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
				for (int i = 0; i < 4; i++)
				{
					fourCornersArray[i] = localToWorldMatrix.MultiplyPoint(fourCornersArray[i]);
				}
			}
		}

		public void SetInsetAndSizeFromParentEdge(RectTransform.Edge edge, float inset, float size)
		{
			int index = (edge == RectTransform.Edge.Top || edge == RectTransform.Edge.Bottom) ? 1 : 0;
			bool flag = edge == RectTransform.Edge.Top || edge == RectTransform.Edge.Right;
			float value = (float)(flag ? 1 : 0);
			Vector2 vector = this.anchorMin;
			vector[index] = value;
			this.anchorMin = vector;
			vector = this.anchorMax;
			vector[index] = value;
			this.anchorMax = vector;
			Vector2 sizeDelta = this.sizeDelta;
			sizeDelta[index] = size;
			this.sizeDelta = sizeDelta;
			Vector2 anchoredPosition = this.anchoredPosition;
			anchoredPosition[index] = (flag ? (-inset - size * (1f - this.pivot[index])) : (inset + size * this.pivot[index]));
			this.anchoredPosition = anchoredPosition;
		}

		public void SetSizeWithCurrentAnchors(RectTransform.Axis axis, float size)
		{
			Vector2 sizeDelta = this.sizeDelta;
			sizeDelta[(int)axis] = size - this.GetParentSize()[(int)axis] * (this.anchorMax[(int)axis] - this.anchorMin[(int)axis]);
			this.sizeDelta = sizeDelta;
		}

		[RequiredByNativeCode]
		internal static void SendReapplyDrivenProperties(RectTransform driven)
		{
			RectTransform.ReapplyDrivenProperties expr_06 = RectTransform.reapplyDrivenProperties;
			if (expr_06 != null)
			{
				expr_06(driven);
			}
		}

		internal Rect GetRectInParentSpace()
		{
			Rect rect = this.rect;
			Vector2 vector = this.offsetMin + Vector2.Scale(this.pivot, rect.size);
			bool flag = base.transform.parent;
			if (flag)
			{
				RectTransform component = base.transform.parent.GetComponent<RectTransform>();
				bool flag2 = component;
				if (flag2)
				{
					vector += Vector2.Scale(this.anchorMin, component.rect.size);
				}
			}
			rect.x += vector.x;
			rect.y += vector.y;
			return rect;
		}

		private Vector2 GetParentSize()
		{
			RectTransform rectTransform = base.parent as RectTransform;
			bool flag = !rectTransform;
			Vector2 result;
			if (flag)
			{
				result = Vector2.zero;
			}
			else
			{
				result = rectTransform.rect.size;
			}
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_rect_Injected(out Rect ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_anchorMin_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_anchorMin_Injected(ref Vector2 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_anchorMax_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_anchorMax_Injected(ref Vector2 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_anchoredPosition_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_anchoredPosition_Injected(ref Vector2 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_sizeDelta_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_sizeDelta_Injected(ref Vector2 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_pivot_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_pivot_Injected(ref Vector2 value);
	}
}
