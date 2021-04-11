using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Camera/ReflectionProbes.h")]
	public sealed class ReflectionProbe : Behaviour
	{
		public enum ReflectionProbeEvent
		{
			ReflectionProbeAdded,
			ReflectionProbeRemoved
		}

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<ReflectionProbe, ReflectionProbe.ReflectionProbeEvent> reflectionProbeChanged;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<Cubemap> defaultReflectionSet;

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("type property has been deprecated. Starting with Unity 5.4, the only supported reflection probe type is Cube.", true), NativeName("ProbeType")]
		public extern ReflectionProbeType type
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("BoxSize")]
		public Vector3 size
		{
			get
			{
				Vector3 result;
				this.get_size_Injected(out result);
				return result;
			}
			set
			{
				this.set_size_Injected(ref value);
			}
		}

		[NativeName("BoxOffset")]
		public Vector3 center
		{
			get
			{
				Vector3 result;
				this.get_center_Injected(out result);
				return result;
			}
			set
			{
				this.set_center_Injected(ref value);
			}
		}

		[NativeName("Near")]
		public extern float nearClipPlane
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("Far")]
		public extern float farClipPlane
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("IntensityMultiplier")]
		public extern float intensity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("GlobalAABB")]
		public Bounds bounds
		{
			get
			{
				Bounds result;
				this.get_bounds_Injected(out result);
				return result;
			}
		}

		[NativeName("HDR")]
		public extern bool hdr
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("RenderDynamicObjects")]
		public extern bool renderDynamicObjects
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float shadowDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int resolution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int cullingMask
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern ReflectionProbeClearFlags clearFlags
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Color backgroundColor
		{
			get
			{
				Color result;
				this.get_backgroundColor_Injected(out result);
				return result;
			}
			set
			{
				this.set_backgroundColor_Injected(ref value);
			}
		}

		public extern float blendDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool boxProjection
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern ReflectionProbeMode mode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int importance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern ReflectionProbeRefreshMode refreshMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern ReflectionProbeTimeSlicingMode timeSlicingMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Texture bakedTexture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Texture customBakedTexture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern RenderTexture realtimeTexture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Texture texture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public Vector4 textureHDRDecodeValues
		{
			[NativeName("CalculateHDRDecodeValues")]
			get
			{
				Vector4 result;
				this.get_textureHDRDecodeValues_Injected(out result);
				return result;
			}
		}

		[StaticAccessor("GetReflectionProbes()")]
		public static extern int minBakedCubemapResolution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[StaticAccessor("GetReflectionProbes()")]
		public static extern int maxBakedCubemapResolution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[StaticAccessor("GetReflectionProbes()")]
		public static Vector4 defaultTextureHDRDecodeValues
		{
			get
			{
				Vector4 result;
				ReflectionProbe.get_defaultTextureHDRDecodeValues_Injected(out result);
				return result;
			}
		}

		[StaticAccessor("GetReflectionProbes()")]
		public static extern Texture defaultTexture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Reset();

		public int RenderProbe()
		{
			return this.RenderProbe(null);
		}

		public int RenderProbe([UnityEngine.Internal.DefaultValue("null")] RenderTexture targetTexture)
		{
			return this.ScheduleRender(this.timeSlicingMode, targetTexture);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsFinishedRendering(int renderId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int ScheduleRender(ReflectionProbeTimeSlicingMode timeSlicingMode, RenderTexture targetTexture);

		[FreeFunction("CubemapGPUBlend"), NativeHeader("Runtime/Camera/CubemapGPUUtility.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool BlendCubemap(Texture src, Texture dst, float blend, RenderTexture target);

		[RequiredByNativeCode]
		private static void CallReflectionProbeEvent(ReflectionProbe probe, ReflectionProbe.ReflectionProbeEvent probeEvent)
		{
			Action<ReflectionProbe, ReflectionProbe.ReflectionProbeEvent> action = ReflectionProbe.reflectionProbeChanged;
			bool flag = action != null;
			if (flag)
			{
				action(probe, probeEvent);
			}
		}

		[RequiredByNativeCode]
		private static void CallSetDefaultReflection(Cubemap defaultReflectionCubemap)
		{
			Action<Cubemap> action = ReflectionProbe.defaultReflectionSet;
			bool flag = action != null;
			if (flag)
			{
				action(defaultReflectionCubemap);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_size_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_size_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_center_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_center_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_bounds_Injected(out Bounds ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_backgroundColor_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_backgroundColor_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_textureHDRDecodeValues_Injected(out Vector4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_defaultTextureHDRDecodeValues_Injected(out Vector4 ret);
	}
}
