using System;

namespace UnityEngine.Rendering
{
	public abstract class RenderPipelineAsset : ScriptableObject
	{
		public virtual int terrainBrushPassIndex
		{
			get
			{
				return 2500;
			}
		}

		public virtual string[] renderingLayerMaskNames
		{
			get
			{
				return null;
			}
		}

		public virtual Material defaultMaterial
		{
			get
			{
				return null;
			}
		}

		public virtual Shader autodeskInteractiveShader
		{
			get
			{
				return null;
			}
		}

		public virtual Shader autodeskInteractiveTransparentShader
		{
			get
			{
				return null;
			}
		}

		public virtual Shader autodeskInteractiveMaskedShader
		{
			get
			{
				return null;
			}
		}

		public virtual Shader terrainDetailLitShader
		{
			get
			{
				return null;
			}
		}

		public virtual Shader terrainDetailGrassShader
		{
			get
			{
				return null;
			}
		}

		public virtual Shader terrainDetailGrassBillboardShader
		{
			get
			{
				return null;
			}
		}

		public virtual Material defaultParticleMaterial
		{
			get
			{
				return null;
			}
		}

		public virtual Material defaultLineMaterial
		{
			get
			{
				return null;
			}
		}

		public virtual Material defaultTerrainMaterial
		{
			get
			{
				return null;
			}
		}

		public virtual Material defaultUIMaterial
		{
			get
			{
				return null;
			}
		}

		public virtual Material defaultUIOverdrawMaterial
		{
			get
			{
				return null;
			}
		}

		public virtual Material defaultUIETC1SupportedMaterial
		{
			get
			{
				return null;
			}
		}

		public virtual Material default2DMaterial
		{
			get
			{
				return null;
			}
		}

		public virtual Shader defaultShader
		{
			get
			{
				return null;
			}
		}

		public virtual Shader defaultSpeedTree7Shader
		{
			get
			{
				return null;
			}
		}

		public virtual Shader defaultSpeedTree8Shader
		{
			get
			{
				return null;
			}
		}

		internal RenderPipeline InternalCreatePipeline()
		{
			RenderPipeline result = null;
			try
			{
				result = this.CreatePipeline();
			}
			catch (Exception ex)
			{
				bool flag = !ex.Data.Contains("InvalidImport") || !(ex.Data["InvalidImport"] is int) || (int)ex.Data["InvalidImport"] != 1;
				if (flag)
				{
					Debug.LogException(ex);
				}
			}
			return result;
		}

		protected abstract RenderPipeline CreatePipeline();

		protected virtual void OnValidate()
		{
			bool flag = RenderPipelineManager.s_CurrentPipelineAsset == this;
			if (flag)
			{
				RenderPipelineManager.CleanupRenderPipeline();
				RenderPipelineManager.PrepareRenderPipeline(this);
			}
		}

		protected virtual void OnDisable()
		{
			RenderPipelineManager.CleanupRenderPipeline();
		}
	}
}
