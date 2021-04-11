using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Modules/UI/Canvas.h"), NativeHeader("Modules/UI/UIStructs.h"), NativeClass("UI::Canvas"), RequireComponent(typeof(RectTransform))]
	public sealed class Canvas : Behaviour
	{
		public delegate void WillRenderCanvases();

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Canvas.WillRenderCanvases willRenderCanvases;

		public extern RenderMode renderMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool isRootCanvas
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public Rect pixelRect
		{
			get
			{
				Rect result;
				this.get_pixelRect_Injected(out result);
				return result;
			}
		}

		public extern float scaleFactor
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float referencePixelsPerUnit
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool overridePixelPerfect
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool pixelPerfect
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float planeDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int renderOrder
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool overrideSorting
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int sortingOrder
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int targetDisplay
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int sortingLayerID
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int cachedSortingLayerValue
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern AdditionalCanvasShaderChannels additionalShaderChannels
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern string sortingLayerName
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Canvas rootCanvas
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("Camera", false, TargetType.Function)]
		public extern Camera worldCamera
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("SortingBucketNormalizedSize", false, TargetType.Function)]
		public extern float normalizedSortingGridSize
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("Setting normalizedSize via a int is not supported. Please use normalizedSortingGridSize", false), NativeProperty("SortingBucketNormalizedSize", false, TargetType.Function)]
		public extern int sortingGridNormalizedSize
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("Shared default material now used for text and general UI elements, call Canvas.GetDefaultCanvasMaterial()", false), FreeFunction("UI::GetDefaultUIMaterial")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Material GetDefaultCanvasTextMaterial();

		[FreeFunction("UI::GetDefaultUIMaterial")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Material GetDefaultCanvasMaterial();

		[FreeFunction("UI::GetETC1SupportedCanvasMaterial")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Material GetETC1SupportedCanvasMaterial();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void UpdateCanvasRectTransform(bool alignWithCamera);

		public static void ForceUpdateCanvases()
		{
			Canvas.SendWillRenderCanvases();
		}

		[RequiredByNativeCode]
		private static void SendWillRenderCanvases()
		{
			Canvas.WillRenderCanvases expr_06 = Canvas.willRenderCanvases;
			if (expr_06 != null)
			{
				expr_06();
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_pixelRect_Injected(out Rect ret);
	}
}
