using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Camera/LightProbeProxyVolume.h")]
	public sealed class LightProbeProxyVolume : Behaviour
	{
		public enum ResolutionMode
		{
			Automatic,
			Custom
		}

		public enum BoundingBoxMode
		{
			AutomaticLocal,
			AutomaticWorld,
			Custom
		}

		public enum ProbePositionMode
		{
			CellCorner,
			CellCenter
		}

		public enum RefreshMode
		{
			Automatic,
			EveryFrame,
			ViaScripting
		}

		public enum QualityMode
		{
			Low,
			Normal
		}

		public enum DataFormat
		{
			HalfFloat,
			Float
		}

		public static extern bool isFeatureSupported
		{
			[NativeName("IsFeatureSupported")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeName("GlobalAABB")]
		public Bounds boundsGlobal
		{
			get
			{
				Bounds result;
				this.get_boundsGlobal_Injected(out result);
				return result;
			}
		}

		[NativeName("BoundingBoxSizeCustom")]
		public Vector3 sizeCustom
		{
			get
			{
				Vector3 result;
				this.get_sizeCustom_Injected(out result);
				return result;
			}
			set
			{
				this.set_sizeCustom_Injected(ref value);
			}
		}

		[NativeName("BoundingBoxOriginCustom")]
		public Vector3 originCustom
		{
			get
			{
				Vector3 result;
				this.get_originCustom_Injected(out result);
				return result;
			}
			set
			{
				this.set_originCustom_Injected(ref value);
			}
		}

		public extern float probeDensity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int gridResolutionX
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int gridResolutionY
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int gridResolutionZ
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern LightProbeProxyVolume.BoundingBoxMode boundingBoxMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern LightProbeProxyVolume.ResolutionMode resolutionMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern LightProbeProxyVolume.ProbePositionMode probePositionMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern LightProbeProxyVolume.RefreshMode refreshMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern LightProbeProxyVolume.QualityMode qualityMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern LightProbeProxyVolume.DataFormat dataFormat
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public void Update()
		{
			this.SetDirtyFlag(true);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetDirtyFlag(bool flag);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_boundsGlobal_Injected(out Bounds ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_sizeCustom_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_sizeCustom_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_originCustom_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_originCustom_Injected(ref Vector3 value);
	}
}
