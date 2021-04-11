using System;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	[NativeHeader("Runtime/Camera/GraphicsSettings.h"), StaticAccessor("GetGraphicsSettings()", StaticAccessorType.Dot)]
	public sealed class GraphicsSettings : UnityEngine.Object
	{
		public static extern TransparencySortMode transparencySortMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static Vector3 transparencySortAxis
		{
			get
			{
				Vector3 result;
				GraphicsSettings.get_transparencySortAxis_Injected(out result);
				return result;
			}
			set
			{
				GraphicsSettings.set_transparencySortAxis_Injected(ref value);
			}
		}

		public static extern bool realtimeDirectRectangularAreaLights
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool lightsUseLinearIntensity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool lightsUseColorTemperature
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern uint defaultRenderingLayerMask
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool useScriptableRenderPipelineBatching
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool logWhenShaderIsCompiled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool disableBuiltinCustomRenderTextureUpdate
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern VideoShadersIncludeMode videoShadersIncludeMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("CurrentRenderPipeline")]
		private static extern ScriptableObject INTERNAL_currentRenderPipeline
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static RenderPipelineAsset currentRenderPipeline
		{
			get
			{
				return GraphicsSettings.INTERNAL_currentRenderPipeline as RenderPipelineAsset;
			}
		}

		public static RenderPipelineAsset renderPipelineAsset
		{
			get
			{
				return GraphicsSettings.defaultRenderPipeline;
			}
			set
			{
				GraphicsSettings.defaultRenderPipeline = value;
			}
		}

		[NativeName("DefaultRenderPipeline")]
		private static extern ScriptableObject INTERNAL_defaultRenderPipeline
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static RenderPipelineAsset defaultRenderPipeline
		{
			get
			{
				return GraphicsSettings.INTERNAL_defaultRenderPipeline as RenderPipelineAsset;
			}
			set
			{
				GraphicsSettings.INTERNAL_defaultRenderPipeline = value;
			}
		}

		public static RenderPipelineAsset[] allConfiguredRenderPipelines
		{
			get
			{
				return GraphicsSettings.GetAllConfiguredRenderPipelines().Cast<RenderPipelineAsset>().ToArray<RenderPipelineAsset>();
			}
		}

		private GraphicsSettings()
		{
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HasShaderDefine(GraphicsTier tier, BuiltinShaderDefine defineHash);

		public static bool HasShaderDefine(BuiltinShaderDefine defineHash)
		{
			return GraphicsSettings.HasShaderDefine(Graphics.activeTier, defineHash);
		}

		[NativeName("GetAllConfiguredRenderPipelinesForScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ScriptableObject[] GetAllConfiguredRenderPipelines();

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern UnityEngine.Object GetGraphicsSettings();

		[NativeName("SetShaderModeScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetShaderMode(BuiltinShaderType type, BuiltinShaderMode mode);

		[NativeName("GetShaderModeScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern BuiltinShaderMode GetShaderMode(BuiltinShaderType type);

		[NativeName("SetCustomShaderScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetCustomShader(BuiltinShaderType type, Shader shader);

		[NativeName("GetCustomShaderScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Shader GetCustomShader(BuiltinShaderType type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_transparencySortAxis_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_transparencySortAxis_Injected(ref Vector3 value);
	}
}
