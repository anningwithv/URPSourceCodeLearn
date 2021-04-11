using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	[NativeConditional("ENABLE_XR"), NativeHeader("Modules/XR/XRPrefix.h"), NativeType(Header = "Modules/XR/Subsystems/Display/XRDisplaySubsystem.h"), UsedByNativeCode]
	public class XRDisplaySubsystem : IntegratedSubsystem<XRDisplaySubsystemDescriptor>
	{
		[Flags]
		public enum TextureLayout
		{
			Texture2DArray = 1,
			SingleTexture2D = 2,
			SeparateTexture2Ds = 4
		}

		public enum ReprojectionMode
		{
			Unspecified,
			PositionAndOrientation,
			OrientationOnly,
			None
		}

		[NativeHeader("Modules/XR/Subsystems/Display/XRDisplaySubsystem.bindings.h")]
		public struct XRRenderParameter
		{
			public Matrix4x4 view;

			public Matrix4x4 projection;

			public Rect viewport;

			public Mesh occlusionMesh;

			public int textureArraySlice;
		}

		[NativeHeader("Runtime/Graphics/RenderTextureDesc.h"), NativeHeader("Modules/XR/Subsystems/Display/XRDisplaySubsystem.bindings.h"), NativeHeader("Runtime/Graphics/CommandBuffer/RenderingCommandBuffer.h")]
		public struct XRRenderPass
		{
			private IntPtr displaySubsystemInstance;

			public int renderPassIndex;

			public RenderTargetIdentifier renderTarget;

			public RenderTextureDescriptor renderTargetDesc;

			public bool shouldFillOutDepth;

			public int cullingPassIndex;

			[NativeConditional("ENABLE_XR"), NativeMethod(Name = "XRRenderPassScriptApi::GetRenderParameter", IsFreeFunction = true, HasExplicitThis = true, ThrowsException = true)]
			public void GetRenderParameter(Camera camera, int renderParameterIndex, out XRDisplaySubsystem.XRRenderParameter renderParameter)
			{
				XRDisplaySubsystem.XRRenderPass.GetRenderParameter_Injected(ref this, camera, renderParameterIndex, out renderParameter);
			}

			[NativeConditional("ENABLE_XR"), NativeMethod(Name = "XRRenderPassScriptApi::GetRenderParameterCount", IsFreeFunction = true, HasExplicitThis = true)]
			public int GetRenderParameterCount()
			{
				return XRDisplaySubsystem.XRRenderPass.GetRenderParameterCount_Injected(ref this);
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetRenderParameter_Injected(ref XRDisplaySubsystem.XRRenderPass _unity_self, Camera camera, int renderParameterIndex, out XRDisplaySubsystem.XRRenderParameter renderParameter);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetRenderParameterCount_Injected(ref XRDisplaySubsystem.XRRenderPass _unity_self);
		}

		[NativeHeader("Runtime/Graphics/RenderTexture.h"), NativeHeader("Modules/XR/Subsystems/Display/XRDisplaySubsystem.bindings.h")]
		public struct XRBlitParams
		{
			public RenderTexture srcTex;

			public int srcTexArraySlice;

			public Rect srcRect;

			public Rect destRect;
		}

		[NativeHeader("Modules/XR/Subsystems/Display/XRDisplaySubsystem.bindings.h")]
		public struct XRMirrorViewBlitDesc
		{
			private IntPtr displaySubsystemInstance;

			public bool nativeBlitAvailable;

			public bool nativeBlitInvalidStates;

			public int blitParamsCount;

			[NativeConditional("ENABLE_XR"), NativeMethod(Name = "XRMirrorViewBlitDescScriptApi::GetBlitParameter", IsFreeFunction = true, HasExplicitThis = true)]
			public void GetBlitParameter(int blitParameterIndex, out XRDisplaySubsystem.XRBlitParams blitParameter)
			{
				XRDisplaySubsystem.XRMirrorViewBlitDesc.GetBlitParameter_Injected(ref this, blitParameterIndex, out blitParameter);
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetBlitParameter_Injected(ref XRDisplaySubsystem.XRMirrorViewBlitDesc _unity_self, int blitParameterIndex, out XRDisplaySubsystem.XRBlitParams blitParameter);
		}

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event Action<bool> displayFocusChanged;

		[Obsolete("singlePassRenderingDisabled{get;set;} is deprecated. Use textureLayout and supportedTextureLayouts instead.", false)]
		public bool singlePassRenderingDisabled
		{
			get
			{
				return (this.textureLayout & XRDisplaySubsystem.TextureLayout.Texture2DArray) == (XRDisplaySubsystem.TextureLayout)0;
			}
			set
			{
				if (value)
				{
					this.textureLayout = XRDisplaySubsystem.TextureLayout.SeparateTexture2Ds;
				}
				else
				{
					bool flag = (this.supportedTextureLayouts & XRDisplaySubsystem.TextureLayout.Texture2DArray) > (XRDisplaySubsystem.TextureLayout)0;
					if (flag)
					{
						this.textureLayout = XRDisplaySubsystem.TextureLayout.Texture2DArray;
					}
				}
			}
		}

		public extern bool displayOpaque
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool contentProtectionEnabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float scaleOfAllViewports
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float scaleOfAllRenderTargets
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float zNear
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float zFar
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool sRGB
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern XRDisplaySubsystem.TextureLayout textureLayout
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern XRDisplaySubsystem.TextureLayout supportedTextureLayouts
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern XRDisplaySubsystem.ReprojectionMode reprojectionMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool disableLegacyRenderer
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[RequiredByNativeCode]
		private void InvokeDisplayFocusChanged(bool focus)
		{
			bool flag = this.displayFocusChanged != null;
			if (flag)
			{
				this.displayFocusChanged(focus);
			}
		}

		public void SetFocusPlane(Vector3 point, Vector3 normal, Vector3 velocity)
		{
			this.SetFocusPlane_Injected(ref point, ref normal, ref velocity);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetMSAALevel(int level);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetRenderPassCount();

		public void GetRenderPass(int renderPassIndex, out XRDisplaySubsystem.XRRenderPass renderPass)
		{
			bool flag = !this.Internal_TryGetRenderPass(renderPassIndex, out renderPass);
			if (flag)
			{
				throw new IndexOutOfRangeException("renderPassIndex");
			}
		}

		[NativeMethod("TryGetRenderPass")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Internal_TryGetRenderPass(int renderPassIndex, out XRDisplaySubsystem.XRRenderPass renderPass);

		public void GetCullingParameters(Camera camera, int cullingPassIndex, out ScriptableCullingParameters scriptableCullingParameters)
		{
			bool flag = !this.Internal_TryGetCullingParams(camera, cullingPassIndex, out scriptableCullingParameters);
			if (!flag)
			{
				return;
			}
			bool flag2 = camera == null;
			if (flag2)
			{
				throw new ArgumentNullException("camera");
			}
			throw new IndexOutOfRangeException("cullingPassIndex");
		}

		[NativeHeader("Runtime/Graphics/ScriptableRenderLoop/ScriptableCulling.h"), NativeMethod("TryGetCullingParams")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Internal_TryGetCullingParams(Camera camera, int cullingPassIndex, out ScriptableCullingParameters scriptableCullingParameters);

		[NativeMethod("TryGetAppGPUTimeLastFrame")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool TryGetAppGPUTimeLastFrame(out float gpuTimeLastFrame);

		[NativeMethod("TryGetCompositorGPUTimeLastFrame")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool TryGetCompositorGPUTimeLastFrame(out float gpuTimeLastFrameCompositor);

		[NativeMethod("TryGetDroppedFrameCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool TryGetDroppedFrameCount(out int droppedFrameCount);

		[NativeMethod("TryGetFramePresentCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool TryGetFramePresentCount(out int framePresentCount);

		[NativeMethod("TryGetDisplayRefreshRate")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool TryGetDisplayRefreshRate(out float displayRefreshRate);

		[NativeMethod("TryGetMotionToPhoton")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool TryGetMotionToPhoton(out float motionToPhoton);

		[NativeConditional("ENABLE_XR"), NativeMethod(Name = "GetTextureForRenderPass", IsThreadSafe = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern RenderTexture GetRenderTextureForRenderPass(int renderPass);

		[NativeConditional("ENABLE_XR"), NativeMethod(Name = "GetPreferredMirrorViewBlitMode", IsThreadSafe = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetPreferredMirrorBlitMode();

		[NativeConditional("ENABLE_XR"), NativeMethod(Name = "SetPreferredMirrorViewBlitMode", IsThreadSafe = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPreferredMirrorBlitMode(int blitMode);

		[Obsolete("GetMirrorViewBlitDesc(RenderTexture, out XRMirrorViewBlitDesc) is deprecated. Use GetMirrorViewBlitDesc(RenderTexture, out XRMirrorViewBlitDesc, int) instead.", false)]
		public bool GetMirrorViewBlitDesc(RenderTexture mirrorRt, out XRDisplaySubsystem.XRMirrorViewBlitDesc outDesc)
		{
			return this.GetMirrorViewBlitDesc(mirrorRt, out outDesc, -1);
		}

		[NativeConditional("ENABLE_XR"), NativeMethod(Name = "QueryMirrorViewBlitDesc", IsThreadSafe = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetMirrorViewBlitDesc(RenderTexture mirrorRt, out XRDisplaySubsystem.XRMirrorViewBlitDesc outDesc, int mode);

		[Obsolete("AddGraphicsThreadMirrorViewBlit(CommandBuffer, bool) is deprecated. Use AddGraphicsThreadMirrorViewBlit(CommandBuffer, bool, int) instead.", false)]
		public bool AddGraphicsThreadMirrorViewBlit(CommandBuffer cmd, bool allowGraphicsStateInvalidate)
		{
			return this.AddGraphicsThreadMirrorViewBlit(cmd, allowGraphicsStateInvalidate, -1);
		}

		[NativeConditional("ENABLE_XR"), NativeHeader("Runtime/Graphics/CommandBuffer/RenderingCommandBuffer.h"), NativeMethod(Name = "AddGraphicsThreadMirrorViewBlit", IsThreadSafe = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool AddGraphicsThreadMirrorViewBlit(CommandBuffer cmd, bool allowGraphicsStateInvalidate, int mode);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFocusPlane_Injected(ref Vector3 point, ref Vector3 normal, ref Vector3 velocity);
	}
}
