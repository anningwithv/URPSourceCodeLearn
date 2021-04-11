using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore
{
	internal static class MaterialManager
	{
		private static Dictionary<long, Material> s_FallbackMaterials = new Dictionary<long, Material>();

		public static Material GetFallbackMaterial(Material sourceMaterial, Material targetMaterial)
		{
			int instanceID = sourceMaterial.GetInstanceID();
			Texture texture = targetMaterial.GetTexture(ShaderUtilities.ID_MainTex);
			int instanceID2 = texture.GetInstanceID();
			long key = (long)instanceID << 32 | (long)((ulong)instanceID2);
			Material material;
			bool flag = MaterialManager.s_FallbackMaterials.TryGetValue(key, out material);
			Material result;
			if (flag)
			{
				result = material;
			}
			else
			{
				bool flag2 = sourceMaterial.HasProperty(ShaderUtilities.ID_GradientScale) && targetMaterial.HasProperty(ShaderUtilities.ID_GradientScale);
				if (flag2)
				{
					material = new Material(sourceMaterial);
					material.hideFlags = HideFlags.HideAndDontSave;
					Material expr_7A = material;
					expr_7A.name = expr_7A.name + " + " + texture.name;
					material.SetTexture(ShaderUtilities.ID_MainTex, texture);
					material.SetFloat(ShaderUtilities.ID_GradientScale, targetMaterial.GetFloat(ShaderUtilities.ID_GradientScale));
					material.SetFloat(ShaderUtilities.ID_TextureWidth, targetMaterial.GetFloat(ShaderUtilities.ID_TextureWidth));
					material.SetFloat(ShaderUtilities.ID_TextureHeight, targetMaterial.GetFloat(ShaderUtilities.ID_TextureHeight));
					material.SetFloat(ShaderUtilities.ID_WeightNormal, targetMaterial.GetFloat(ShaderUtilities.ID_WeightNormal));
					material.SetFloat(ShaderUtilities.ID_WeightBold, targetMaterial.GetFloat(ShaderUtilities.ID_WeightBold));
				}
				else
				{
					material = new Material(targetMaterial);
				}
				MaterialManager.s_FallbackMaterials.Add(key, material);
				result = material;
			}
			return result;
		}
	}
}
