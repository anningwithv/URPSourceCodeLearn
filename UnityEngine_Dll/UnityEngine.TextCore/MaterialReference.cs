using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore
{
	internal struct MaterialReference
	{
		public int index;

		public FontAsset fontAsset;

		public TextSpriteAsset spriteAsset;

		public Material material;

		public bool isDefaultMaterial;

		public bool isFallbackMaterial;

		public Material fallbackMaterial;

		public float padding;

		public int referenceCount;

		public MaterialReference(int index, FontAsset fontAsset, TextSpriteAsset spriteAsset, Material material, float padding)
		{
			this.index = index;
			this.fontAsset = fontAsset;
			this.spriteAsset = spriteAsset;
			this.material = material;
			this.isDefaultMaterial = (material.GetInstanceID() == fontAsset.material.GetInstanceID());
			this.isFallbackMaterial = false;
			this.fallbackMaterial = null;
			this.padding = padding;
			this.referenceCount = 0;
		}

		public static bool Contains(MaterialReference[] materialReferences, FontAsset fontAsset)
		{
			int instanceID = fontAsset.GetInstanceID();
			int num = 0;
			bool result;
			while (num < materialReferences.Length && materialReferences[num].fontAsset != null)
			{
				bool flag = materialReferences[num].fontAsset.GetInstanceID() == instanceID;
				if (flag)
				{
					result = true;
					return result;
				}
				num++;
			}
			result = false;
			return result;
		}

		public static int AddMaterialReference(Material material, FontAsset fontAsset, MaterialReference[] materialReferences, Dictionary<int, int> materialReferenceIndexLookup)
		{
			int instanceID = material.GetInstanceID();
			int count;
			bool flag = materialReferenceIndexLookup.TryGetValue(instanceID, out count);
			int result;
			if (flag)
			{
				result = count;
			}
			else
			{
				count = materialReferenceIndexLookup.Count;
				materialReferenceIndexLookup[instanceID] = count;
				materialReferences[count].index = count;
				materialReferences[count].fontAsset = fontAsset;
				materialReferences[count].spriteAsset = null;
				materialReferences[count].material = material;
				materialReferences[count].isDefaultMaterial = (instanceID == fontAsset.material.GetInstanceID());
				materialReferences[count].referenceCount = 0;
				result = count;
			}
			return result;
		}

		public static int AddMaterialReference(Material material, TextSpriteAsset spriteAsset, MaterialReference[] materialReferences, Dictionary<int, int> materialReferenceIndexLookup)
		{
			int instanceID = material.GetInstanceID();
			int count;
			bool flag = materialReferenceIndexLookup.TryGetValue(instanceID, out count);
			int result;
			if (flag)
			{
				result = count;
			}
			else
			{
				count = materialReferenceIndexLookup.Count;
				materialReferenceIndexLookup[instanceID] = count;
				materialReferences[count].index = count;
				materialReferences[count].fontAsset = materialReferences[0].fontAsset;
				materialReferences[count].spriteAsset = spriteAsset;
				materialReferences[count].material = material;
				materialReferences[count].isDefaultMaterial = true;
				materialReferences[count].referenceCount = 0;
				result = count;
			}
			return result;
		}
	}
}
