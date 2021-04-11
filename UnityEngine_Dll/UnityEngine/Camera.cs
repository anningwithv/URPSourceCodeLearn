using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Shaders/Shader.h"), NativeHeader("Runtime/Misc/GameObjectUtility.h"), NativeHeader("Runtime/Graphics/CommandBuffer/RenderingCommandBuffer.h"), NativeHeader("Runtime/Camera/Camera.h"), NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h"), NativeHeader("Runtime/Camera/RenderManager.h"), NativeHeader("Runtime/Graphics/RenderTexture.h"), RequireComponent(typeof(Transform)), UsedByNativeCode]
	public sealed class Camera : Behaviour
	{
		internal enum ProjectionMatrixMode
		{
			Explicit,
			Implicit,
			PhysicalPropertiesBased
		}

		public enum GateFitMode
		{
			Vertical = 1,
			Horizontal,
			Fill,
			Overscan,
			None = 0
		}

		public enum FieldOfViewAxis
		{
			Vertical,
			Horizontal
		}

		public struct GateFitParameters
		{
			public Camera.GateFitMode mode
			{
				[IsReadOnly]
				get;
				set;
			}

			public float aspect
			{
				[IsReadOnly]
				get;
				set;
			}

			public GateFitParameters(Camera.GateFitMode mode, float aspect)
			{
				this.mode = mode;
				this.aspect = aspect;
			}
		}

		public enum StereoscopicEye
		{
			Left,
			Right
		}

		public enum MonoOrStereoscopicEye
		{
			Left,
			Right,
			Mono
		}

		public enum RenderRequestMode
		{
			None,
			ObjectId,
			Depth,
			VertexNormal,
			WorldPosition,
			EntityId,
			BaseColor,
			SpecularColor,
			Metallic,
			Emission,
			Normal,
			Smoothness,
			Occlusion,
			DiffuseColor
		}

		public enum RenderRequestOutputSpace
		{
			ScreenSpace = -1,
			UV0,
			UV1,
			UV2,
			UV3,
			UV4,
			UV5,
			UV6,
			UV7,
			UV8
		}

		public struct RenderRequest
		{
			private readonly Camera.RenderRequestMode m_CameraRenderMode;

			private readonly RenderTexture m_ResultRT;

			private readonly Camera.RenderRequestOutputSpace m_OutputSpace;

			public bool isValid
			{
				get
				{
					return this.m_CameraRenderMode != Camera.RenderRequestMode.None && this.m_ResultRT != null;
				}
			}

			public Camera.RenderRequestMode mode
			{
				get
				{
					return this.m_CameraRenderMode;
				}
			}

			public RenderTexture result
			{
				get
				{
					return this.m_ResultRT;
				}
			}

			public Camera.RenderRequestOutputSpace outputSpace
			{
				get
				{
					return this.m_OutputSpace;
				}
			}

			public RenderRequest(Camera.RenderRequestMode mode, RenderTexture rt)
			{
				this.m_CameraRenderMode = mode;
				this.m_ResultRT = rt;
				this.m_OutputSpace = Camera.RenderRequestOutputSpace.ScreenSpace;
			}

			public RenderRequest(Camera.RenderRequestMode mode, Camera.RenderRequestOutputSpace space, RenderTexture rt)
			{
				this.m_CameraRenderMode = mode;
				this.m_ResultRT = rt;
				this.m_OutputSpace = space;
			}
		}

		public delegate void CameraCallback(Camera cam);

		public static Camera.CameraCallback onPreCull;

		public static Camera.CameraCallback onPreRender;

		public static Camera.CameraCallback onPostRender;

		[NativeProperty("Near")]
		public extern float nearClipPlane
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("Far")]
		public extern float farClipPlane
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("VerticalFieldOfView")]
		public extern float fieldOfView
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern RenderingPath renderingPath
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern RenderingPath actualRenderingPath
		{
			[NativeName("CalculateRenderingPath")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool allowHDR
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool allowMSAA
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool allowDynamicResolution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("ForceIntoRT")]
		public extern bool forceIntoRenderTexture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float orthographicSize
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool orthographic
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern OpaqueSortMode opaqueSortMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern TransparencySortMode transparencySortMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 transparencySortAxis
		{
			get
			{
				Vector3 result;
				this.get_transparencySortAxis_Injected(out result);
				return result;
			}
			set
			{
				this.set_transparencySortAxis_Injected(ref value);
			}
		}

		public extern float depth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float aspect
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 velocity
		{
			get
			{
				Vector3 result;
				this.get_velocity_Injected(out result);
				return result;
			}
		}

		public extern int cullingMask
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int eventMask
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool layerCullSpherical
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern CameraType cameraType
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeConditional("UNITY_EDITOR")]
		public extern ulong overrideSceneCullingMask
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeConditional("UNITY_EDITOR")]
		internal extern ulong sceneCullingMask
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public float[] layerCullDistances
		{
			get
			{
				return this.GetLayerCullDistances();
			}
			set
			{
				bool flag = value.Length != 32;
				if (flag)
				{
					throw new UnityException("Array needs to contain exactly 32 floats for layerCullDistances.");
				}
				this.SetLayerCullDistances(value);
			}
		}

		internal static extern int PreviewCullingLayer
		{
			[FreeFunction("CameraScripting::GetPreviewCullingLayer")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool useOcclusionCulling
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Matrix4x4 cullingMatrix
		{
			get
			{
				Matrix4x4 result;
				this.get_cullingMatrix_Injected(out result);
				return result;
			}
			set
			{
				this.set_cullingMatrix_Injected(ref value);
			}
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

		public extern CameraClearFlags clearFlags
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern DepthTextureMode depthTextureMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool clearStencilAfterLightingPass
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal extern Camera.ProjectionMatrixMode projectionMatrixMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool usePhysicalProperties
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector2 sensorSize
		{
			get
			{
				Vector2 result;
				this.get_sensorSize_Injected(out result);
				return result;
			}
			set
			{
				this.set_sensorSize_Injected(ref value);
			}
		}

		public Vector2 lensShift
		{
			get
			{
				Vector2 result;
				this.get_lensShift_Injected(out result);
				return result;
			}
			set
			{
				this.set_lensShift_Injected(ref value);
			}
		}

		public extern float focalLength
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Camera.GateFitMode gateFit
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("NormalizedViewportRect")]
		public Rect rect
		{
			get
			{
				Rect result;
				this.get_rect_Injected(out result);
				return result;
			}
			set
			{
				this.set_rect_Injected(ref value);
			}
		}

		[NativeProperty("ScreenViewportRect")]
		public Rect pixelRect
		{
			get
			{
				Rect result;
				this.get_pixelRect_Injected(out result);
				return result;
			}
			set
			{
				this.set_pixelRect_Injected(ref value);
			}
		}

		public extern int pixelWidth
		{
			[FreeFunction("CameraScripting::GetPixelWidth", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int pixelHeight
		{
			[FreeFunction("CameraScripting::GetPixelHeight", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int scaledPixelWidth
		{
			[FreeFunction("CameraScripting::GetScaledPixelWidth", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int scaledPixelHeight
		{
			[FreeFunction("CameraScripting::GetScaledPixelHeight", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern RenderTexture targetTexture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern RenderTexture activeTexture
		{
			[NativeName("GetCurrentTargetTexture")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int targetDisplay
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Matrix4x4 cameraToWorldMatrix
		{
			get
			{
				Matrix4x4 result;
				this.get_cameraToWorldMatrix_Injected(out result);
				return result;
			}
		}

		public Matrix4x4 worldToCameraMatrix
		{
			get
			{
				Matrix4x4 result;
				this.get_worldToCameraMatrix_Injected(out result);
				return result;
			}
			set
			{
				this.set_worldToCameraMatrix_Injected(ref value);
			}
		}

		public Matrix4x4 projectionMatrix
		{
			get
			{
				Matrix4x4 result;
				this.get_projectionMatrix_Injected(out result);
				return result;
			}
			set
			{
				this.set_projectionMatrix_Injected(ref value);
			}
		}

		public Matrix4x4 nonJitteredProjectionMatrix
		{
			get
			{
				Matrix4x4 result;
				this.get_nonJitteredProjectionMatrix_Injected(out result);
				return result;
			}
			set
			{
				this.set_nonJitteredProjectionMatrix_Injected(ref value);
			}
		}

		[NativeProperty("UseJitteredProjectionMatrixForTransparent")]
		public extern bool useJitteredProjectionMatrixForTransparentRendering
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Matrix4x4 previousViewProjectionMatrix
		{
			get
			{
				Matrix4x4 result;
				this.get_previousViewProjectionMatrix_Injected(out result);
				return result;
			}
		}

		public static extern Camera main
		{
			[FreeFunction("FindMainCamera")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern Camera current
		{
			[FreeFunction("GetCurrentCameraPPtr")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public Scene scene
		{
			[FreeFunction("CameraScripting::GetScene", HasExplicitThis = true)]
			get
			{
				Scene result;
				this.get_scene_Injected(out result);
				return result;
			}
			[FreeFunction("CameraScripting::SetScene", HasExplicitThis = true)]
			set
			{
				this.set_scene_Injected(ref value);
			}
		}

		public extern bool stereoEnabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float stereoSeparation
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float stereoConvergence
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool areVRStereoViewMatricesWithinSingleCullTolerance
		{
			[NativeName("AreVRStereoViewMatricesWithinSingleCullTolerance")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern StereoTargetEyeMask stereoTargetEye
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Camera.MonoOrStereoscopicEye stereoActiveEye
		{
			[FreeFunction("CameraScripting::GetStereoActiveEye", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static int allCamerasCount
		{
			get
			{
				return Camera.GetAllCamerasCount();
			}
		}

		public static Camera[] allCameras
		{
			get
			{
				Camera[] array = new Camera[Camera.allCamerasCount];
				Camera.GetAllCamerasImpl(array);
				return array;
			}
		}

		public extern int commandBufferCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Property isOrthoGraphic has been deprecated. Use orthographic (UnityUpgradable) -> orthographic", true)]
		public bool isOrthoGraphic
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Property mainCamera has been deprecated. Use Camera.main instead (UnityUpgradable) -> main", true)]
		public static Camera mainCamera
		{
			get
			{
				return null;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Property near has been deprecated. Use Camera.nearClipPlane instead (UnityUpgradable) -> UnityEngine.Camera.nearClipPlane", false)]
		public float near
		{
			get
			{
				return this.nearClipPlane;
			}
			set
			{
				this.nearClipPlane = value;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Property far has been deprecated. Use Camera.farClipPlane instead (UnityUpgradable) -> UnityEngine.Camera.farClipPlane", false)]
		public float far
		{
			get
			{
				return this.farClipPlane;
			}
			set
			{
				this.farClipPlane = value;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Property fov has been deprecated. Use Camera.fieldOfView instead (UnityUpgradable) -> UnityEngine.Camera.fieldOfView", false)]
		public float fov
		{
			get
			{
				return this.fieldOfView;
			}
			set
			{
				this.fieldOfView = value;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Property hdr has been deprecated. Use Camera.allowHDR instead (UnityUpgradable) -> UnityEngine.Camera.allowHDR", false)]
		public bool hdr
		{
			get
			{
				return this.allowHDR;
			}
			set
			{
				this.allowHDR = value;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Property stereoMirrorMode is no longer supported. Please use single pass stereo rendering instead.", true)]
		public bool stereoMirrorMode
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Reset();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetTransparencySortSettings();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetAspect();

		[FreeFunction("CameraScripting::GetLayerCullDistances", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float[] GetLayerCullDistances();

		[FreeFunction("CameraScripting::SetLayerCullDistances", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetLayerCullDistances([NotNull("ArgumentNullException")] float[] d);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetCullingMatrix();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetReplacementShader(Shader shader, string replacementTag);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetReplacementShader();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetGateFittedFieldOfView();

		public Vector2 GetGateFittedLensShift()
		{
			Vector2 result;
			this.GetGateFittedLensShift_Injected(out result);
			return result;
		}

		internal Vector3 GetLocalSpaceAim()
		{
			Vector3 result;
			this.GetLocalSpaceAim_Injected(out result);
			return result;
		}

		[FreeFunction("CameraScripting::SetTargetBuffers", HasExplicitThis = true)]
		private void SetTargetBuffersImpl(RenderBuffer color, RenderBuffer depth)
		{
			this.SetTargetBuffersImpl_Injected(ref color, ref depth);
		}

		public void SetTargetBuffers(RenderBuffer colorBuffer, RenderBuffer depthBuffer)
		{
			this.SetTargetBuffersImpl(colorBuffer, depthBuffer);
		}

		[FreeFunction("CameraScripting::SetTargetBuffers", HasExplicitThis = true)]
		private void SetTargetBuffersMRTImpl(RenderBuffer[] color, RenderBuffer depth)
		{
			this.SetTargetBuffersMRTImpl_Injected(color, ref depth);
		}

		public void SetTargetBuffers(RenderBuffer[] colorBuffer, RenderBuffer depthBuffer)
		{
			this.SetTargetBuffersMRTImpl(colorBuffer, depthBuffer);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string[] GetCameraBufferWarnings();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetWorldToCameraMatrix();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetProjectionMatrix();

		[FreeFunction("CameraScripting::CalculateObliqueMatrix", HasExplicitThis = true)]
		public Matrix4x4 CalculateObliqueMatrix(Vector4 clipPlane)
		{
			Matrix4x4 result;
			this.CalculateObliqueMatrix_Injected(ref clipPlane, out result);
			return result;
		}

		public Vector3 WorldToScreenPoint(Vector3 position, Camera.MonoOrStereoscopicEye eye)
		{
			Vector3 result;
			this.WorldToScreenPoint_Injected(ref position, eye, out result);
			return result;
		}

		public Vector3 WorldToViewportPoint(Vector3 position, Camera.MonoOrStereoscopicEye eye)
		{
			Vector3 result;
			this.WorldToViewportPoint_Injected(ref position, eye, out result);
			return result;
		}

		public Vector3 ViewportToWorldPoint(Vector3 position, Camera.MonoOrStereoscopicEye eye)
		{
			Vector3 result;
			this.ViewportToWorldPoint_Injected(ref position, eye, out result);
			return result;
		}

		public Vector3 ScreenToWorldPoint(Vector3 position, Camera.MonoOrStereoscopicEye eye)
		{
			Vector3 result;
			this.ScreenToWorldPoint_Injected(ref position, eye, out result);
			return result;
		}

		public Vector3 WorldToScreenPoint(Vector3 position)
		{
			return this.WorldToScreenPoint(position, Camera.MonoOrStereoscopicEye.Mono);
		}

		public Vector3 WorldToViewportPoint(Vector3 position)
		{
			return this.WorldToViewportPoint(position, Camera.MonoOrStereoscopicEye.Mono);
		}

		public Vector3 ViewportToWorldPoint(Vector3 position)
		{
			return this.ViewportToWorldPoint(position, Camera.MonoOrStereoscopicEye.Mono);
		}

		public Vector3 ScreenToWorldPoint(Vector3 position)
		{
			return this.ScreenToWorldPoint(position, Camera.MonoOrStereoscopicEye.Mono);
		}

		public Vector3 ScreenToViewportPoint(Vector3 position)
		{
			Vector3 result;
			this.ScreenToViewportPoint_Injected(ref position, out result);
			return result;
		}

		public Vector3 ViewportToScreenPoint(Vector3 position)
		{
			Vector3 result;
			this.ViewportToScreenPoint_Injected(ref position, out result);
			return result;
		}

		internal Vector2 GetFrustumPlaneSizeAt(float distance)
		{
			Vector2 result;
			this.GetFrustumPlaneSizeAt_Injected(distance, out result);
			return result;
		}

		private Ray ViewportPointToRay(Vector2 pos, Camera.MonoOrStereoscopicEye eye)
		{
			Ray result;
			this.ViewportPointToRay_Injected(ref pos, eye, out result);
			return result;
		}

		public Ray ViewportPointToRay(Vector3 pos, Camera.MonoOrStereoscopicEye eye)
		{
			return this.ViewportPointToRay(pos, eye);
		}

		public Ray ViewportPointToRay(Vector3 pos)
		{
			return this.ViewportPointToRay(pos, Camera.MonoOrStereoscopicEye.Mono);
		}

		private Ray ScreenPointToRay(Vector2 pos, Camera.MonoOrStereoscopicEye eye)
		{
			Ray result;
			this.ScreenPointToRay_Injected(ref pos, eye, out result);
			return result;
		}

		public Ray ScreenPointToRay(Vector3 pos, Camera.MonoOrStereoscopicEye eye)
		{
			return this.ScreenPointToRay(pos, eye);
		}

		public Ray ScreenPointToRay(Vector3 pos)
		{
			return this.ScreenPointToRay(pos, Camera.MonoOrStereoscopicEye.Mono);
		}

		[FreeFunction("CameraScripting::CalculateViewportRayVectors", HasExplicitThis = true)]
		private void CalculateFrustumCornersInternal(Rect viewport, float z, Camera.MonoOrStereoscopicEye eye, [Out] Vector3[] outCorners)
		{
			this.CalculateFrustumCornersInternal_Injected(ref viewport, z, eye, outCorners);
		}

		public void CalculateFrustumCorners(Rect viewport, float z, Camera.MonoOrStereoscopicEye eye, Vector3[] outCorners)
		{
			bool flag = outCorners == null;
			if (flag)
			{
				throw new ArgumentNullException("outCorners");
			}
			bool flag2 = outCorners.Length < 4;
			if (flag2)
			{
				throw new ArgumentException("outCorners minimum size is 4", "outCorners");
			}
			this.CalculateFrustumCornersInternal(viewport, z, eye, outCorners);
		}

		[NativeName("CalculateProjectionMatrixFromPhysicalProperties")]
		private static void CalculateProjectionMatrixFromPhysicalPropertiesInternal(out Matrix4x4 output, float focalLength, Vector2 sensorSize, Vector2 lensShift, float nearClip, float farClip, float gateAspect, Camera.GateFitMode gateFitMode)
		{
			Camera.CalculateProjectionMatrixFromPhysicalPropertiesInternal_Injected(out output, focalLength, ref sensorSize, ref lensShift, nearClip, farClip, gateAspect, gateFitMode);
		}

		public static void CalculateProjectionMatrixFromPhysicalProperties(out Matrix4x4 output, float focalLength, Vector2 sensorSize, Vector2 lensShift, float nearClip, float farClip, Camera.GateFitParameters gateFitParameters = default(Camera.GateFitParameters))
		{
			Camera.CalculateProjectionMatrixFromPhysicalPropertiesInternal(out output, focalLength, sensorSize, lensShift, nearClip, farClip, gateFitParameters.aspect, gateFitParameters.mode);
		}

		[NativeName("FocalLengthToFieldOfView_Safe")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float FocalLengthToFieldOfView(float focalLength, float sensorSize);

		[NativeName("FieldOfViewToFocalLength_Safe")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float FieldOfViewToFocalLength(float fieldOfView, float sensorSize);

		[NativeName("HorizontalToVerticalFieldOfView_Safe")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float HorizontalToVerticalFieldOfView(float horizontalFieldOfView, float aspectRatio);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float VerticalToHorizontalFieldOfView(float verticalFieldOfView, float aspectRatio);

		public Matrix4x4 GetStereoNonJitteredProjectionMatrix(Camera.StereoscopicEye eye)
		{
			Matrix4x4 result;
			this.GetStereoNonJitteredProjectionMatrix_Injected(eye, out result);
			return result;
		}

		[FreeFunction("CameraScripting::GetStereoViewMatrix", HasExplicitThis = true)]
		public Matrix4x4 GetStereoViewMatrix(Camera.StereoscopicEye eye)
		{
			Matrix4x4 result;
			this.GetStereoViewMatrix_Injected(eye, out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CopyStereoDeviceProjectionMatrixToNonJittered(Camera.StereoscopicEye eye);

		[FreeFunction("CameraScripting::GetStereoProjectionMatrix", HasExplicitThis = true)]
		public Matrix4x4 GetStereoProjectionMatrix(Camera.StereoscopicEye eye)
		{
			Matrix4x4 result;
			this.GetStereoProjectionMatrix_Injected(eye, out result);
			return result;
		}

		public void SetStereoProjectionMatrix(Camera.StereoscopicEye eye, Matrix4x4 matrix)
		{
			this.SetStereoProjectionMatrix_Injected(eye, ref matrix);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetStereoProjectionMatrices();

		public void SetStereoViewMatrix(Camera.StereoscopicEye eye, Matrix4x4 matrix)
		{
			this.SetStereoViewMatrix_Injected(eye, ref matrix);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetStereoViewMatrices();

		[FreeFunction("CameraScripting::GetAllCamerasCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetAllCamerasCount();

		[FreeFunction("CameraScripting::GetAllCameras")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetAllCamerasImpl([NotNull("ArgumentNullException")] [Out] Camera[] cam);

		public static int GetAllCameras(Camera[] cameras)
		{
			bool flag = cameras == null;
			if (flag)
			{
				throw new NullReferenceException();
			}
			bool flag2 = cameras.Length < Camera.allCamerasCount;
			if (flag2)
			{
				throw new ArgumentException("Passed in array to fill with cameras is to small to hold the number of cameras. Use Camera.allCamerasCount to get the needed size.");
			}
			return Camera.GetAllCamerasImpl(cameras);
		}

		[FreeFunction("CameraScripting::RenderToCubemap", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool RenderToCubemapImpl(Texture tex, [UnityEngine.Internal.DefaultValue("63")] int faceMask);

		public bool RenderToCubemap(Cubemap cubemap, int faceMask)
		{
			return this.RenderToCubemapImpl(cubemap, faceMask);
		}

		public bool RenderToCubemap(Cubemap cubemap)
		{
			return this.RenderToCubemapImpl(cubemap, 63);
		}

		public bool RenderToCubemap(RenderTexture cubemap, int faceMask)
		{
			return this.RenderToCubemapImpl(cubemap, faceMask);
		}

		public bool RenderToCubemap(RenderTexture cubemap)
		{
			return this.RenderToCubemapImpl(cubemap, 63);
		}

		[NativeName("RenderToCubemap")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool RenderToCubemapEyeImpl(RenderTexture cubemap, int faceMask, Camera.MonoOrStereoscopicEye stereoEye);

		public bool RenderToCubemap(RenderTexture cubemap, int faceMask, Camera.MonoOrStereoscopicEye stereoEye)
		{
			return this.RenderToCubemapEyeImpl(cubemap, faceMask, stereoEye);
		}

		[FreeFunction("CameraScripting::Render", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Render();

		[FreeFunction("CameraScripting::RenderWithShader", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RenderWithShader(Shader shader, string replacementTag);

		[FreeFunction("CameraScripting::RenderDontRestore", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RenderDontRestore();

		public void SubmitRenderRequests(List<Camera.RenderRequest> renderRequests)
		{
			bool flag = renderRequests == null || renderRequests.Count == 0;
			if (flag)
			{
				throw new ArgumentException("SubmitRenderRequests has been invoked with invalid renderRequests");
			}
			bool flag2 = GraphicsSettings.currentRenderPipeline == null;
			if (flag2)
			{
				Debug.LogWarning("Trying to invoke 'SubmitRenderRequests' when no SRP is set. A scriptable render pipeline is needed for this function call");
			}
			else
			{
				this.SubmitRenderRequestsInternal(renderRequests);
			}
		}

		[FreeFunction("CameraScripting::SubmitRenderRequests", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SubmitRenderRequestsInternal(object requests);

		[FreeFunction("CameraScripting::SetupCurrent")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetupCurrent(Camera cur);

		[FreeFunction("CameraScripting::CopyFrom", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CopyFrom(Camera other);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveCommandBuffers(CameraEvent evt);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveAllCommandBuffers();

		[NativeName("AddCommandBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddCommandBufferImpl(CameraEvent evt, [NotNull("ArgumentNullException")] CommandBuffer buffer);

		[NativeName("AddCommandBufferAsync")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddCommandBufferAsyncImpl(CameraEvent evt, [NotNull("ArgumentNullException")] CommandBuffer buffer, ComputeQueueType queueType);

		[NativeName("RemoveCommandBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RemoveCommandBufferImpl(CameraEvent evt, [NotNull("ArgumentNullException")] CommandBuffer buffer);

		public void AddCommandBuffer(CameraEvent evt, CommandBuffer buffer)
		{
			bool flag = !CameraEventUtils.IsValid(evt);
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid CameraEvent value \"{0}\".", (int)evt), "evt");
			}
			bool flag2 = buffer == null;
			if (flag2)
			{
				throw new NullReferenceException("buffer is null");
			}
			this.AddCommandBufferImpl(evt, buffer);
		}

		public void AddCommandBufferAsync(CameraEvent evt, CommandBuffer buffer, ComputeQueueType queueType)
		{
			bool flag = !CameraEventUtils.IsValid(evt);
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid CameraEvent value \"{0}\".", (int)evt), "evt");
			}
			bool flag2 = buffer == null;
			if (flag2)
			{
				throw new NullReferenceException("buffer is null");
			}
			this.AddCommandBufferAsyncImpl(evt, buffer, queueType);
		}

		public void RemoveCommandBuffer(CameraEvent evt, CommandBuffer buffer)
		{
			bool flag = !CameraEventUtils.IsValid(evt);
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid CameraEvent value \"{0}\".", (int)evt), "evt");
			}
			bool flag2 = buffer == null;
			if (flag2)
			{
				throw new NullReferenceException("buffer is null");
			}
			this.RemoveCommandBufferImpl(evt, buffer);
		}

		[FreeFunction("CameraScripting::GetCommandBuffers", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern CommandBuffer[] GetCommandBuffers(CameraEvent evt);

		[RequiredByNativeCode]
		private static void FireOnPreCull(Camera cam)
		{
			bool flag = Camera.onPreCull != null;
			if (flag)
			{
				Camera.onPreCull(cam);
			}
		}

		[RequiredByNativeCode]
		private static void FireOnPreRender(Camera cam)
		{
			bool flag = Camera.onPreRender != null;
			if (flag)
			{
				Camera.onPreRender(cam);
			}
		}

		[RequiredByNativeCode]
		private static void FireOnPostRender(Camera cam)
		{
			bool flag = Camera.onPostRender != null;
			if (flag)
			{
				Camera.onPostRender(cam);
			}
		}

		internal void OnlyUsedForTesting1()
		{
		}

		internal void OnlyUsedForTesting2()
		{
		}

		public bool TryGetCullingParameters(out ScriptableCullingParameters cullingParameters)
		{
			return Camera.GetCullingParameters_Internal(this, false, out cullingParameters, sizeof(ScriptableCullingParameters));
		}

		public bool TryGetCullingParameters(bool stereoAware, out ScriptableCullingParameters cullingParameters)
		{
			return Camera.GetCullingParameters_Internal(this, stereoAware, out cullingParameters, sizeof(ScriptableCullingParameters));
		}

		[FreeFunction("ScriptableRenderPipeline_Bindings::GetCullingParameters_Internal"), NativeHeader("Runtime/Export/RenderPipeline/ScriptableRenderPipeline.bindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetCullingParameters_Internal(Camera camera, bool stereoAware, out ScriptableCullingParameters cullingParameters, int managedCullingParametersSize);

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Camera.GetScreenWidth has been deprecated. Use Screen.width instead (UnityUpgradable) -> System.Int32 Screen.width", true)]
		public float GetScreenWidth()
		{
			return 0f;
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Camera.GetScreenHeight has been deprecated. Use Screen.height instead (UnityUpgradable) -> System.Int32 Screen.height", true)]
		public float GetScreenHeight()
		{
			return 0f;
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Camera.DoClear has been deprecated (UnityUpgradable).", true)]
		public void DoClear()
		{
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Camera.ResetFieldOfView has been deprecated in Unity 5.6 and will be removed in the future. Please replace it by explicitly setting the camera's FOV to 60 degrees.", false)]
		public void ResetFieldOfView()
		{
			this.fieldOfView = 60f;
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Camera.SetStereoViewMatrices has been deprecated. Use SetStereoViewMatrix(StereoscopicEye eye) instead.", false)]
		public void SetStereoViewMatrices(Matrix4x4 leftMatrix, Matrix4x4 rightMatrix)
		{
			this.SetStereoViewMatrix(Camera.StereoscopicEye.Left, leftMatrix);
			this.SetStereoViewMatrix(Camera.StereoscopicEye.Right, rightMatrix);
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Camera.SetStereoProjectionMatrices has been deprecated. Use SetStereoProjectionMatrix(StereoscopicEye eye) instead.", false)]
		public void SetStereoProjectionMatrices(Matrix4x4 leftMatrix, Matrix4x4 rightMatrix)
		{
			this.SetStereoProjectionMatrix(Camera.StereoscopicEye.Left, leftMatrix);
			this.SetStereoProjectionMatrix(Camera.StereoscopicEye.Right, rightMatrix);
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Camera.GetStereoViewMatrices has been deprecated. Use GetStereoViewMatrix(StereoscopicEye eye) instead.", false)]
		public Matrix4x4[] GetStereoViewMatrices()
		{
			return new Matrix4x4[]
			{
				this.GetStereoViewMatrix(Camera.StereoscopicEye.Left),
				this.GetStereoViewMatrix(Camera.StereoscopicEye.Right)
			};
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Camera.GetStereoProjectionMatrices has been deprecated. Use GetStereoProjectionMatrix(StereoscopicEye eye) instead.", false)]
		public Matrix4x4[] GetStereoProjectionMatrices()
		{
			return new Matrix4x4[]
			{
				this.GetStereoProjectionMatrix(Camera.StereoscopicEye.Left),
				this.GetStereoProjectionMatrix(Camera.StereoscopicEye.Right)
			};
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_transparencySortAxis_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_transparencySortAxis_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_velocity_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_cullingMatrix_Injected(out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_cullingMatrix_Injected(ref Matrix4x4 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_backgroundColor_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_backgroundColor_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_sensorSize_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_sensorSize_Injected(ref Vector2 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_lensShift_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_lensShift_Injected(ref Vector2 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetGateFittedLensShift_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetLocalSpaceAim_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_rect_Injected(out Rect ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_rect_Injected(ref Rect value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_pixelRect_Injected(out Rect ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_pixelRect_Injected(ref Rect value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTargetBuffersImpl_Injected(ref RenderBuffer color, ref RenderBuffer depth);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTargetBuffersMRTImpl_Injected(RenderBuffer[] color, ref RenderBuffer depth);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_cameraToWorldMatrix_Injected(out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_worldToCameraMatrix_Injected(out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_worldToCameraMatrix_Injected(ref Matrix4x4 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_projectionMatrix_Injected(out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_projectionMatrix_Injected(ref Matrix4x4 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_nonJitteredProjectionMatrix_Injected(out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_nonJitteredProjectionMatrix_Injected(ref Matrix4x4 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_previousViewProjectionMatrix_Injected(out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void CalculateObliqueMatrix_Injected(ref Vector4 clipPlane, out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void WorldToScreenPoint_Injected(ref Vector3 position, Camera.MonoOrStereoscopicEye eye, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void WorldToViewportPoint_Injected(ref Vector3 position, Camera.MonoOrStereoscopicEye eye, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ViewportToWorldPoint_Injected(ref Vector3 position, Camera.MonoOrStereoscopicEye eye, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ScreenToWorldPoint_Injected(ref Vector3 position, Camera.MonoOrStereoscopicEye eye, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ScreenToViewportPoint_Injected(ref Vector3 position, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ViewportToScreenPoint_Injected(ref Vector3 position, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetFrustumPlaneSizeAt_Injected(float distance, out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ViewportPointToRay_Injected(ref Vector2 pos, Camera.MonoOrStereoscopicEye eye, out Ray ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ScreenPointToRay_Injected(ref Vector2 pos, Camera.MonoOrStereoscopicEye eye, out Ray ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void CalculateFrustumCornersInternal_Injected(ref Rect viewport, float z, Camera.MonoOrStereoscopicEye eye, [Out] Vector3[] outCorners);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CalculateProjectionMatrixFromPhysicalPropertiesInternal_Injected(out Matrix4x4 output, float focalLength, ref Vector2 sensorSize, ref Vector2 lensShift, float nearClip, float farClip, float gateAspect, Camera.GateFitMode gateFitMode);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_scene_Injected(out Scene ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_scene_Injected(ref Scene value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetStereoNonJitteredProjectionMatrix_Injected(Camera.StereoscopicEye eye, out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetStereoViewMatrix_Injected(Camera.StereoscopicEye eye, out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetStereoProjectionMatrix_Injected(Camera.StereoscopicEye eye, out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetStereoProjectionMatrix_Injected(Camera.StereoscopicEye eye, ref Matrix4x4 matrix);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetStereoViewMatrix_Injected(Camera.StereoscopicEye eye, ref Matrix4x4 matrix);
	}
}
