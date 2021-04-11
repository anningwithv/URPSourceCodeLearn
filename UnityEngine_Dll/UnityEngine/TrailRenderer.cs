using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h"), NativeHeader("Runtime/Graphics/TrailRenderer.h")]
	public sealed class TrailRenderer : Renderer
	{
		[Obsolete("Use positionCount instead (UnityUpgradable) -> positionCount", false)]
		public int numPositions
		{
			get
			{
				return this.positionCount;
			}
		}

		public extern float time
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float startWidth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float endWidth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float widthMultiplier
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool autodestruct
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool emitting
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int numCornerVertices
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int numCapVertices
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float minVertexDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Color startColor
		{
			get
			{
				Color result;
				this.get_startColor_Injected(out result);
				return result;
			}
			set
			{
				this.set_startColor_Injected(ref value);
			}
		}

		public Color endColor
		{
			get
			{
				Color result;
				this.get_endColor_Injected(out result);
				return result;
			}
			set
			{
				this.set_endColor_Injected(ref value);
			}
		}

		[NativeProperty("PositionsCount")]
		public extern int positionCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float shadowBias
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool generateLightingData
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern LineTextureMode textureMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern LineAlignment alignment
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public AnimationCurve widthCurve
		{
			get
			{
				return this.GetWidthCurveCopy();
			}
			set
			{
				this.SetWidthCurve(value);
			}
		}

		public Gradient colorGradient
		{
			get
			{
				return this.GetColorGradientCopy();
			}
			set
			{
				this.SetColorGradient(value);
			}
		}

		public void SetPosition(int index, Vector3 position)
		{
			this.SetPosition_Injected(index, ref position);
		}

		public Vector3 GetPosition(int index)
		{
			Vector3 result;
			this.GetPosition_Injected(index, out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Clear();

		public void BakeMesh(Mesh mesh, bool useTransform = false)
		{
			this.BakeMesh(mesh, Camera.main, useTransform);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void BakeMesh([NotNull("ArgumentNullException")] Mesh mesh, [NotNull("ArgumentNullException")] Camera camera, bool useTransform = false);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AnimationCurve GetWidthCurveCopy();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetWidthCurve([NotNull("ArgumentNullException")] AnimationCurve curve);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Gradient GetColorGradientCopy();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetColorGradient([NotNull("ArgumentNullException")] Gradient curve);

		[FreeFunction(Name = "TrailRendererScripting::GetPositions", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetPositions([NotNull("ArgumentNullException")] [Out] Vector3[] positions);

		[FreeFunction(Name = "TrailRendererScripting::SetPositions", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPositions([NotNull("ArgumentNullException")] Vector3[] positions);

		[FreeFunction(Name = "TrailRendererScripting::AddPosition", HasExplicitThis = true)]
		public void AddPosition(Vector3 position)
		{
			this.AddPosition_Injected(ref position);
		}

		[FreeFunction(Name = "TrailRendererScripting::AddPositions", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddPositions([NotNull("ArgumentNullException")] Vector3[] positions);

		public void SetPositions(NativeArray<Vector3> positions)
		{
			this.SetPositionsWithNativeContainer((IntPtr)positions.GetUnsafeReadOnlyPtr<Vector3>(), positions.Length);
		}

		public void SetPositions(NativeSlice<Vector3> positions)
		{
			this.SetPositionsWithNativeContainer((IntPtr)positions.GetUnsafeReadOnlyPtr<Vector3>(), positions.Length);
		}

		public int GetPositions([Out] NativeArray<Vector3> positions)
		{
			return this.GetPositionsWithNativeContainer((IntPtr)positions.GetUnsafePtr<Vector3>(), positions.Length);
		}

		public int GetPositions([Out] NativeSlice<Vector3> positions)
		{
			return this.GetPositionsWithNativeContainer((IntPtr)positions.GetUnsafePtr<Vector3>(), positions.Length);
		}

		public void AddPositions([Out] NativeArray<Vector3> positions)
		{
			this.AddPositionsWithNativeContainer((IntPtr)positions.GetUnsafePtr<Vector3>(), positions.Length);
		}

		public void AddPositions([Out] NativeSlice<Vector3> positions)
		{
			this.AddPositionsWithNativeContainer((IntPtr)positions.GetUnsafePtr<Vector3>(), positions.Length);
		}

		[FreeFunction(Name = "TrailRendererScripting::SetPositionsWithNativeContainer", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPositionsWithNativeContainer(IntPtr positions, int count);

		[FreeFunction(Name = "TrailRendererScripting::GetPositionsWithNativeContainer", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetPositionsWithNativeContainer(IntPtr positions, int length);

		[FreeFunction(Name = "TrailRendererScripting::AddPositionsWithNativeContainer", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddPositionsWithNativeContainer(IntPtr positions, int length);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_startColor_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_startColor_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_endColor_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_endColor_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPosition_Injected(int index, ref Vector3 position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPosition_Injected(int index, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddPosition_Injected(ref Vector3 position);
	}
}
