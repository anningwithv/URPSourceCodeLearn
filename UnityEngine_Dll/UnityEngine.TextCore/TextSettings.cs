using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore
{
	[Serializable]
	internal class TextSettings : ScriptableObject
	{
		public class LineBreakingTable
		{
			public Dictionary<int, char> leadingCharacters;

			public Dictionary<int, char> followingCharacters;
		}

		private const string k_DefaultLeadingCharacters = "([｛〔〈《「『【〘〖〝‘“｟«$—…‥〳〴〵\\［（{£¥\"々〇〉》」＄｠￥￦ #";

		private const string k_DefaultFollowingCharacters = ")]｝〕〉》」』】〙〗〟’”｠»ヽヾーァィゥェォッャュョヮヵヶぁぃぅぇぉっゃゅょゎゕゖㇰㇱㇲㇳㇴㇵㇶㇷㇸㇹㇺㇻㇼㇽㇾㇿ々〻‐゠–〜?!‼⁇⁈⁉・、%,.:;。！？］）：；＝}¢°\"†‡℃〆％，．";

		private static TextSettings s_Instance;

		[SerializeField]
		private int m_missingGlyphCharacter;

		[SerializeField]
		private bool m_warningsDisabled = true;

		[SerializeField]
		private FontAsset m_defaultFontAsset;

		[SerializeField]
		private string m_defaultFontAssetPath;

		[SerializeField]
		private List<FontAsset> m_fallbackFontAssets;

		[SerializeField]
		private bool m_matchMaterialPreset;

		[SerializeField]
		private TextSpriteAsset m_defaultSpriteAsset;

		[SerializeField]
		private string m_defaultSpriteAssetPath;

		[SerializeField]
		private string m_defaultColorGradientPresetsPath;

		[SerializeField]
		private TextStyleSheet m_defaultStyleSheet;

		[SerializeField]
		private TextAsset m_leadingCharacters = null;

		[SerializeField]
		private TextAsset m_followingCharacters = null;

		[SerializeField]
		private TextSettings.LineBreakingTable m_linebreakingRules;

		public static int missingGlyphCharacter
		{
			get
			{
				return TextSettings.instance.m_missingGlyphCharacter;
			}
			set
			{
				TextSettings.instance.m_missingGlyphCharacter = value;
			}
		}

		public static bool warningsDisabled
		{
			get
			{
				return TextSettings.instance.m_warningsDisabled;
			}
			set
			{
				TextSettings.instance.m_warningsDisabled = value;
			}
		}

		public static FontAsset defaultFontAsset
		{
			get
			{
				return TextSettings.instance.m_defaultFontAsset;
			}
			set
			{
				TextSettings.instance.m_defaultFontAsset = value;
			}
		}

		public static string defaultFontAssetPath
		{
			get
			{
				return TextSettings.instance.m_defaultFontAssetPath;
			}
			set
			{
				TextSettings.instance.m_defaultFontAssetPath = value;
			}
		}

		public static List<FontAsset> fallbackFontAssets
		{
			get
			{
				return TextSettings.instance.m_fallbackFontAssets;
			}
			set
			{
				TextSettings.instance.m_fallbackFontAssets = value;
			}
		}

		public static bool matchMaterialPreset
		{
			get
			{
				return TextSettings.instance.m_matchMaterialPreset;
			}
			set
			{
				TextSettings.instance.m_matchMaterialPreset = value;
			}
		}

		public static TextSpriteAsset defaultSpriteAsset
		{
			get
			{
				return TextSettings.instance.m_defaultSpriteAsset;
			}
			set
			{
				TextSettings.instance.m_defaultSpriteAsset = value;
			}
		}

		public static string defaultSpriteAssetPath
		{
			get
			{
				return TextSettings.instance.m_defaultSpriteAssetPath;
			}
			set
			{
				TextSettings.instance.m_defaultSpriteAssetPath = value;
			}
		}

		public static string defaultColorGradientPresetsPath
		{
			get
			{
				return TextSettings.instance.m_defaultColorGradientPresetsPath;
			}
			set
			{
				TextSettings.instance.m_defaultColorGradientPresetsPath = value;
			}
		}

		public static TextStyleSheet defaultStyleSheet
		{
			get
			{
				return TextSettings.instance.m_defaultStyleSheet;
			}
			set
			{
				TextSettings.instance.m_defaultStyleSheet = value;
				TextStyleSheet.LoadDefaultStyleSheet();
			}
		}

		public static TextSettings.LineBreakingTable linebreakingRules
		{
			get
			{
				bool flag = TextSettings.instance.m_linebreakingRules == null;
				if (flag)
				{
					TextSettings.LoadLinebreakingRules();
				}
				return TextSettings.instance.m_linebreakingRules;
			}
		}

		public static TextSettings instance
		{
			get
			{
				bool flag = TextSettings.s_Instance == null;
				if (flag)
				{
					TextSettings.s_Instance = (Resources.Load<TextSettings>("TextSettings") ?? ScriptableObject.CreateInstance<TextSettings>());
				}
				return TextSettings.s_Instance;
			}
		}

		public static void LoadLinebreakingRules()
		{
			bool flag = TextSettings.instance == null;
			if (!flag)
			{
				bool flag2 = TextSettings.s_Instance.m_linebreakingRules == null;
				if (flag2)
				{
					TextSettings.s_Instance.m_linebreakingRules = new TextSettings.LineBreakingTable();
				}
				TextSettings.s_Instance.m_linebreakingRules.leadingCharacters = ((TextSettings.s_Instance.m_leadingCharacters != null) ? TextSettings.GetCharacters(TextSettings.s_Instance.m_leadingCharacters.text) : TextSettings.GetCharacters("([｛〔〈《「『【〘〖〝‘“｟«$—…‥〳〴〵\\［（{£¥\"々〇〉》」＄｠￥￦ #"));
				TextSettings.s_Instance.m_linebreakingRules.followingCharacters = ((TextSettings.s_Instance.m_followingCharacters != null) ? TextSettings.GetCharacters(TextSettings.s_Instance.m_followingCharacters.text) : TextSettings.GetCharacters(")]｝〕〉》」』】〙〗〟’”｠»ヽヾーァィゥェォッャュョヮヵヶぁぃぅぇぉっゃゅょゎゕゖㇰㇱㇲㇳㇴㇵㇶㇷㇸㇹㇺㇻㇼㇽㇾㇿ々〻‐゠–〜?!‼⁇⁈⁉・、%,.:;。！？］）：；＝}¢°\"†‡℃〆％，．"));
			}
		}

		private static Dictionary<int, char> GetCharacters(string text)
		{
			Dictionary<int, char> dictionary = new Dictionary<int, char>();
			for (int i = 0; i < text.Length; i++)
			{
				char c = text[i];
				bool flag = !dictionary.ContainsKey((int)c);
				if (flag)
				{
					dictionary.Add((int)c, c);
				}
			}
			return dictionary;
		}
	}
}
