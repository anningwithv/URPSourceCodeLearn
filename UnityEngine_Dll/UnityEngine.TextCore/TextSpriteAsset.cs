using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.TextCore
{
	[Serializable]
	internal class TextSpriteAsset : ScriptableObject
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly TextSpriteAsset.<>c <>9 = new TextSpriteAsset.<>c();

			public static Func<SpriteGlyph, uint> <>9__43_0;

			public static Func<SpriteCharacter, uint> <>9__44_0;

			internal uint <SortGlyphTable>b__43_0(SpriteGlyph item)
			{
				return item.index;
			}

			internal uint <SortCharacterTable>b__44_0(SpriteCharacter c)
			{
				return c.unicode;
			}
		}

		internal Dictionary<uint, int> m_UnicodeLookup;

		internal Dictionary<int, int> m_NameLookup;

		internal Dictionary<uint, int> m_GlyphIndexLookup;

		[SerializeField]
		private string m_Version;

		[SerializeField]
		private int m_HashCode;

		[SerializeField]
		public Texture spriteSheet;

		[SerializeField]
		private Material m_Material;

		[SerializeField]
		private int m_MaterialHashCode;

		[SerializeField]
		private List<SpriteCharacter> m_SpriteCharacterTable = new List<SpriteCharacter>();

		[SerializeField]
		private List<SpriteGlyph> m_SpriteGlyphTable = new List<SpriteGlyph>();

		[SerializeField]
		public List<TextSpriteAsset> fallbackSpriteAssets;

		internal bool m_IsSpriteAssetLookupTablesDirty = false;

		private static List<int> s_SearchedSpriteAssets;

		public string version
		{
			get
			{
				return this.m_Version;
			}
			set
			{
				this.m_Version = value;
			}
		}

		public int hashCode
		{
			get
			{
				return this.m_HashCode;
			}
			set
			{
				this.m_HashCode = value;
			}
		}

		public Material material
		{
			get
			{
				return this.m_Material;
			}
			set
			{
				this.m_Material = value;
			}
		}

		public int materialHashCode
		{
			get
			{
				return this.m_MaterialHashCode;
			}
		}

		public List<SpriteCharacter> spriteCharacterTable
		{
			get
			{
				return this.m_SpriteCharacterTable;
			}
			internal set
			{
				this.m_SpriteCharacterTable = value;
			}
		}

		public List<SpriteGlyph> spriteGlyphTable
		{
			get
			{
				return this.m_SpriteGlyphTable;
			}
			internal set
			{
				this.m_SpriteGlyphTable = value;
			}
		}

		private void Awake()
		{
			this.m_HashCode = TextUtilities.GetHashCodeCaseInSensitive(base.name);
			bool flag = this.m_Material != null;
			if (flag)
			{
				this.m_MaterialHashCode = TextUtilities.GetHashCodeCaseInSensitive(this.m_Material.name);
			}
		}

		private void OnValidate()
		{
			this.m_HashCode = TextUtilities.GetHashCodeCaseInSensitive(base.name);
			bool flag = this.m_Material != null;
			if (flag)
			{
				this.m_MaterialHashCode = TextUtilities.GetHashCodeCaseInSensitive(this.m_Material.name);
			}
			this.UpdateLookupTables();
		}

		private Material GetDefaultSpriteMaterial()
		{
			ShaderUtilities.GetShaderPropertyIDs();
			Shader shader = Shader.Find("TextMeshPro/Sprite");
			Material material = new Material(shader);
			material.SetTexture(ShaderUtilities.ID_MainTex, this.spriteSheet);
			material.hideFlags = HideFlags.HideInHierarchy;
			return material;
		}

		public void UpdateLookupTables()
		{
			bool flag = this.m_GlyphIndexLookup == null;
			if (flag)
			{
				this.m_GlyphIndexLookup = new Dictionary<uint, int>();
			}
			else
			{
				this.m_GlyphIndexLookup.Clear();
			}
			for (int i = 0; i < this.m_SpriteGlyphTable.Count; i++)
			{
				uint index = this.m_SpriteGlyphTable[i].index;
				bool flag2 = !this.m_GlyphIndexLookup.ContainsKey(index);
				if (flag2)
				{
					this.m_GlyphIndexLookup.Add(index, i);
				}
			}
			bool flag3 = this.m_NameLookup == null;
			if (flag3)
			{
				this.m_NameLookup = new Dictionary<int, int>();
			}
			else
			{
				this.m_NameLookup.Clear();
			}
			bool flag4 = this.m_UnicodeLookup == null;
			if (flag4)
			{
				this.m_UnicodeLookup = new Dictionary<uint, int>();
			}
			else
			{
				this.m_UnicodeLookup.Clear();
			}
			for (int j = 0; j < this.m_SpriteCharacterTable.Count; j++)
			{
				int hashCode = this.m_SpriteCharacterTable[j].hashCode;
				bool flag5 = !this.m_NameLookup.ContainsKey(hashCode);
				if (flag5)
				{
					this.m_NameLookup.Add(hashCode, j);
				}
				uint unicode = this.m_SpriteCharacterTable[j].unicode;
				bool flag6 = !this.m_UnicodeLookup.ContainsKey(unicode);
				if (flag6)
				{
					this.m_UnicodeLookup.Add(unicode, j);
				}
				uint glyphIndex = this.m_SpriteCharacterTable[j].glyphIndex;
				int index2;
				bool flag7 = this.m_GlyphIndexLookup.TryGetValue(glyphIndex, out index2);
				if (flag7)
				{
					this.m_SpriteCharacterTable[j].glyph = this.m_SpriteGlyphTable[index2];
				}
			}
			this.m_IsSpriteAssetLookupTablesDirty = false;
		}

		public int GetSpriteIndexFromHashcode(int hashCode)
		{
			bool flag = this.m_NameLookup == null;
			if (flag)
			{
				this.UpdateLookupTables();
			}
			int num;
			bool flag2 = this.m_NameLookup.TryGetValue(hashCode, out num);
			int result;
			if (flag2)
			{
				result = num;
			}
			else
			{
				result = -1;
			}
			return result;
		}

		public int GetSpriteIndexFromUnicode(uint unicode)
		{
			bool flag = this.m_UnicodeLookup == null;
			if (flag)
			{
				this.UpdateLookupTables();
			}
			int num;
			bool flag2 = this.m_UnicodeLookup.TryGetValue(unicode, out num);
			int result;
			if (flag2)
			{
				result = num;
			}
			else
			{
				result = -1;
			}
			return result;
		}

		public int GetSpriteIndexFromName(string spriteName)
		{
			bool flag = this.m_NameLookup == null;
			if (flag)
			{
				this.UpdateLookupTables();
			}
			int hashCodeCaseInSensitive = TextUtilities.GetHashCodeCaseInSensitive(spriteName);
			return this.GetSpriteIndexFromHashcode(hashCodeCaseInSensitive);
		}

		public static TextSpriteAsset SearchForSpriteByUnicode(TextSpriteAsset spriteAsset, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			bool flag = spriteAsset == null;
			TextSpriteAsset result;
			if (flag)
			{
				spriteIndex = -1;
				result = null;
			}
			else
			{
				spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(unicode);
				bool flag2 = spriteIndex != -1;
				if (flag2)
				{
					result = spriteAsset;
				}
				else
				{
					bool flag3 = TextSpriteAsset.s_SearchedSpriteAssets == null;
					if (flag3)
					{
						TextSpriteAsset.s_SearchedSpriteAssets = new List<int>();
					}
					TextSpriteAsset.s_SearchedSpriteAssets.Clear();
					int instanceID = spriteAsset.GetInstanceID();
					TextSpriteAsset.s_SearchedSpriteAssets.Add(instanceID);
					bool flag4 = includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0;
					if (flag4)
					{
						result = TextSpriteAsset.SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, unicode, includeFallbacks, out spriteIndex);
					}
					else
					{
						bool flag5 = includeFallbacks && TextSettings.defaultSpriteAsset != null;
						if (flag5)
						{
							result = TextSpriteAsset.SearchForSpriteByUnicodeInternal(TextSettings.defaultSpriteAsset, unicode, includeFallbacks, out spriteIndex);
						}
						else
						{
							spriteIndex = -1;
							result = null;
						}
					}
				}
			}
			return result;
		}

		private static TextSpriteAsset SearchForSpriteByUnicodeInternal(List<TextSpriteAsset> spriteAssets, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			TextSpriteAsset result;
			for (int i = 0; i < spriteAssets.Count; i++)
			{
				TextSpriteAsset textSpriteAsset = spriteAssets[i];
				bool flag = textSpriteAsset == null;
				if (!flag)
				{
					int instanceID = textSpriteAsset.GetInstanceID();
					bool flag2 = TextSpriteAsset.s_SearchedSpriteAssets.Contains(instanceID);
					if (!flag2)
					{
						TextSpriteAsset.s_SearchedSpriteAssets.Add(instanceID);
						textSpriteAsset = TextSpriteAsset.SearchForSpriteByUnicodeInternal(textSpriteAsset, unicode, includeFallbacks, out spriteIndex);
						bool flag3 = textSpriteAsset != null;
						if (flag3)
						{
							result = textSpriteAsset;
							return result;
						}
					}
				}
			}
			spriteIndex = -1;
			result = null;
			return result;
		}

		private static TextSpriteAsset SearchForSpriteByUnicodeInternal(TextSpriteAsset spriteAsset, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(unicode);
			bool flag = spriteIndex != -1;
			TextSpriteAsset result;
			if (flag)
			{
				result = spriteAsset;
			}
			else
			{
				bool flag2 = includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0;
				if (flag2)
				{
					result = TextSpriteAsset.SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, unicode, includeFallbacks, out spriteIndex);
				}
				else
				{
					spriteIndex = -1;
					result = null;
				}
			}
			return result;
		}

		public static TextSpriteAsset SearchForSpriteByHashCode(TextSpriteAsset spriteAsset, int hashCode, bool includeFallbacks, out int spriteIndex)
		{
			bool flag = spriteAsset == null;
			TextSpriteAsset result;
			if (flag)
			{
				spriteIndex = -1;
				result = null;
			}
			else
			{
				spriteIndex = spriteAsset.GetSpriteIndexFromHashcode(hashCode);
				bool flag2 = spriteIndex != -1;
				if (flag2)
				{
					result = spriteAsset;
				}
				else
				{
					bool flag3 = TextSpriteAsset.s_SearchedSpriteAssets == null;
					if (flag3)
					{
						TextSpriteAsset.s_SearchedSpriteAssets = new List<int>();
					}
					TextSpriteAsset.s_SearchedSpriteAssets.Clear();
					int instanceID = spriteAsset.GetInstanceID();
					TextSpriteAsset.s_SearchedSpriteAssets.Add(instanceID);
					bool flag4 = includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0;
					if (flag4)
					{
						result = TextSpriteAsset.SearchForSpriteByHashCodeInternal(spriteAsset.fallbackSpriteAssets, hashCode, includeFallbacks, out spriteIndex);
					}
					else
					{
						bool flag5 = includeFallbacks && TextSettings.defaultSpriteAsset != null;
						if (flag5)
						{
							result = TextSpriteAsset.SearchForSpriteByHashCodeInternal(TextSettings.defaultSpriteAsset, hashCode, includeFallbacks, out spriteIndex);
						}
						else
						{
							spriteIndex = -1;
							result = null;
						}
					}
				}
			}
			return result;
		}

		private static TextSpriteAsset SearchForSpriteByHashCodeInternal(List<TextSpriteAsset> spriteAssets, int hashCode, bool searchFallbacks, out int spriteIndex)
		{
			TextSpriteAsset result;
			for (int i = 0; i < spriteAssets.Count; i++)
			{
				TextSpriteAsset textSpriteAsset = spriteAssets[i];
				bool flag = textSpriteAsset == null;
				if (!flag)
				{
					int instanceID = textSpriteAsset.GetInstanceID();
					bool flag2 = TextSpriteAsset.s_SearchedSpriteAssets.Contains(instanceID);
					if (!flag2)
					{
						TextSpriteAsset.s_SearchedSpriteAssets.Add(instanceID);
						textSpriteAsset = TextSpriteAsset.SearchForSpriteByHashCodeInternal(textSpriteAsset, hashCode, searchFallbacks, out spriteIndex);
						bool flag3 = textSpriteAsset != null;
						if (flag3)
						{
							result = textSpriteAsset;
							return result;
						}
					}
				}
			}
			spriteIndex = -1;
			result = null;
			return result;
		}

		private static TextSpriteAsset SearchForSpriteByHashCodeInternal(TextSpriteAsset spriteAsset, int hashCode, bool searchFallbacks, out int spriteIndex)
		{
			spriteIndex = spriteAsset.GetSpriteIndexFromHashcode(hashCode);
			bool flag = spriteIndex != -1;
			TextSpriteAsset result;
			if (flag)
			{
				result = spriteAsset;
			}
			else
			{
				bool flag2 = searchFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0;
				if (flag2)
				{
					result = TextSpriteAsset.SearchForSpriteByHashCodeInternal(spriteAsset.fallbackSpriteAssets, hashCode, searchFallbacks, out spriteIndex);
				}
				else
				{
					spriteIndex = -1;
					result = null;
				}
			}
			return result;
		}

		public void SortGlyphTable()
		{
			bool flag = this.m_SpriteGlyphTable == null || this.m_SpriteGlyphTable.Count == 0;
			if (!flag)
			{
				IEnumerable<SpriteGlyph> arg_46_0 = this.m_SpriteGlyphTable;
				Func<SpriteGlyph, uint> arg_46_1;
				if ((arg_46_1 = TextSpriteAsset.<>c.<>9__43_0) == null)
				{
					arg_46_1 = (TextSpriteAsset.<>c.<>9__43_0 = new Func<SpriteGlyph, uint>(TextSpriteAsset.<>c.<>9.<SortGlyphTable>b__43_0));
				}
				this.m_SpriteGlyphTable = arg_46_0.OrderBy(arg_46_1).ToList<SpriteGlyph>();
			}
		}

		internal void SortCharacterTable()
		{
			bool flag = this.m_SpriteCharacterTable != null && this.m_SpriteCharacterTable.Count > 0;
			if (flag)
			{
				IEnumerable<SpriteCharacter> arg_44_0 = this.m_SpriteCharacterTable;
				Func<SpriteCharacter, uint> arg_44_1;
				if ((arg_44_1 = TextSpriteAsset.<>c.<>9__44_0) == null)
				{
					arg_44_1 = (TextSpriteAsset.<>c.<>9__44_0 = new Func<SpriteCharacter, uint>(TextSpriteAsset.<>c.<>9.<SortCharacterTable>b__44_0));
				}
				this.m_SpriteCharacterTable = arg_44_0.OrderBy(arg_44_1).ToList<SpriteCharacter>();
			}
		}

		internal void SortGlyphAndCharacterTables()
		{
			this.SortGlyphTable();
			this.SortCharacterTable();
		}
	}
}
