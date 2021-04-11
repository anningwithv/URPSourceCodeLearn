using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore
{
	internal class MaterialReferenceManager
	{
		private static MaterialReferenceManager s_Instance;

		private Dictionary<int, Material> m_FontMaterialReferenceLookup = new Dictionary<int, Material>();

		private Dictionary<int, FontAsset> m_FontAssetReferenceLookup = new Dictionary<int, FontAsset>();

		private Dictionary<int, TextSpriteAsset> m_SpriteAssetReferenceLookup = new Dictionary<int, TextSpriteAsset>();

		private Dictionary<int, TextGradientPreset> m_ColorGradientReferenceLookup = new Dictionary<int, TextGradientPreset>();

		public static MaterialReferenceManager instance
		{
			get
			{
				bool flag = MaterialReferenceManager.s_Instance == null;
				if (flag)
				{
					MaterialReferenceManager.s_Instance = new MaterialReferenceManager();
				}
				return MaterialReferenceManager.s_Instance;
			}
		}

		public static void AddFontAsset(FontAsset fontAsset)
		{
			MaterialReferenceManager.instance.AddFontAssetInternal(fontAsset);
		}

		private void AddFontAssetInternal(FontAsset fontAsset)
		{
			bool flag = this.m_FontAssetReferenceLookup.ContainsKey(fontAsset.hashCode);
			if (!flag)
			{
				this.m_FontAssetReferenceLookup.Add(fontAsset.hashCode, fontAsset);
				this.m_FontMaterialReferenceLookup.Add(fontAsset.materialHashCode, fontAsset.material);
			}
		}

		public static void AddSpriteAsset(TextSpriteAsset spriteAsset)
		{
			MaterialReferenceManager.instance.AddSpriteAssetInternal(spriteAsset);
		}

		private void AddSpriteAssetInternal(TextSpriteAsset spriteAsset)
		{
			bool flag = this.m_SpriteAssetReferenceLookup.ContainsKey(spriteAsset.hashCode);
			if (!flag)
			{
				this.m_SpriteAssetReferenceLookup.Add(spriteAsset.hashCode, spriteAsset);
				this.m_FontMaterialReferenceLookup.Add(spriteAsset.hashCode, spriteAsset.material);
			}
		}

		public static void AddSpriteAsset(int hashCode, TextSpriteAsset spriteAsset)
		{
			MaterialReferenceManager.instance.AddSpriteAssetInternal(hashCode, spriteAsset);
		}

		private void AddSpriteAssetInternal(int hashCode, TextSpriteAsset spriteAsset)
		{
			bool flag = this.m_SpriteAssetReferenceLookup.ContainsKey(hashCode);
			if (!flag)
			{
				this.m_SpriteAssetReferenceLookup.Add(hashCode, spriteAsset);
				this.m_FontMaterialReferenceLookup.Add(hashCode, spriteAsset.material);
				bool flag2 = spriteAsset.hashCode == 0;
				if (flag2)
				{
					spriteAsset.hashCode = hashCode;
				}
			}
		}

		public static void AddFontMaterial(int hashCode, Material material)
		{
			MaterialReferenceManager.instance.AddFontMaterialInternal(hashCode, material);
		}

		private void AddFontMaterialInternal(int hashCode, Material material)
		{
			this.m_FontMaterialReferenceLookup.Add(hashCode, material);
		}

		public static void AddColorGradientPreset(int hashCode, TextGradientPreset spriteAsset)
		{
			MaterialReferenceManager.instance.AddColorGradientPreset_Internal(hashCode, spriteAsset);
		}

		private void AddColorGradientPreset_Internal(int hashCode, TextGradientPreset spriteAsset)
		{
			bool flag = this.m_ColorGradientReferenceLookup.ContainsKey(hashCode);
			if (!flag)
			{
				this.m_ColorGradientReferenceLookup.Add(hashCode, spriteAsset);
			}
		}

		public bool Contains(FontAsset font)
		{
			return this.m_FontAssetReferenceLookup.ContainsKey(font.hashCode);
		}

		public bool Contains(TextSpriteAsset sprite)
		{
			return this.m_FontAssetReferenceLookup.ContainsKey(sprite.hashCode);
		}

		public static bool TryGetFontAsset(int hashCode, out FontAsset fontAsset)
		{
			return MaterialReferenceManager.instance.TryGetFontAssetInternal(hashCode, out fontAsset);
		}

		private bool TryGetFontAssetInternal(int hashCode, out FontAsset fontAsset)
		{
			fontAsset = null;
			return this.m_FontAssetReferenceLookup.TryGetValue(hashCode, out fontAsset);
		}

		public static bool TryGetSpriteAsset(int hashCode, out TextSpriteAsset spriteAsset)
		{
			return MaterialReferenceManager.instance.TryGetSpriteAssetInternal(hashCode, out spriteAsset);
		}

		private bool TryGetSpriteAssetInternal(int hashCode, out TextSpriteAsset spriteAsset)
		{
			spriteAsset = null;
			return this.m_SpriteAssetReferenceLookup.TryGetValue(hashCode, out spriteAsset);
		}

		public static bool TryGetColorGradientPreset(int hashCode, out TextGradientPreset gradientPreset)
		{
			return MaterialReferenceManager.instance.TryGetColorGradientPresetInternal(hashCode, out gradientPreset);
		}

		private bool TryGetColorGradientPresetInternal(int hashCode, out TextGradientPreset gradientPreset)
		{
			gradientPreset = null;
			return this.m_ColorGradientReferenceLookup.TryGetValue(hashCode, out gradientPreset);
		}

		public static bool TryGetMaterial(int hashCode, out Material material)
		{
			return MaterialReferenceManager.instance.TryGetMaterialInternal(hashCode, out material);
		}

		private bool TryGetMaterialInternal(int hashCode, out Material material)
		{
			material = null;
			return this.m_FontMaterialReferenceLookup.TryGetValue(hashCode, out material);
		}
	}
}
