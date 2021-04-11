using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore
{
	[Serializable]
	internal class TextStyleSheet : ScriptableObject
	{
		private static TextStyleSheet s_Instance;

		[SerializeField]
		private List<TextStyle> m_StyleList = new List<TextStyle>(1);

		private Dictionary<int, TextStyle> m_StyleDictionary = new Dictionary<int, TextStyle>();

		public static TextStyleSheet instance
		{
			get
			{
				bool flag = TextStyleSheet.s_Instance == null;
				TextStyleSheet result;
				if (flag)
				{
					TextStyleSheet.s_Instance = TextSettings.defaultStyleSheet;
					bool flag2 = TextStyleSheet.s_Instance == null;
					if (flag2)
					{
						result = null;
						return result;
					}
					TextStyleSheet.s_Instance.LoadStyleDictionaryInternal();
				}
				result = TextStyleSheet.s_Instance;
				return result;
			}
		}

		public static TextStyleSheet LoadDefaultStyleSheet()
		{
			TextStyleSheet.s_Instance = null;
			return TextStyleSheet.instance;
		}

		public static TextStyle GetStyle(int hashCode)
		{
			bool flag = TextStyleSheet.instance == null;
			TextStyle result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = TextStyleSheet.instance.GetStyleInternal(hashCode);
			}
			return result;
		}

		private TextStyle GetStyleInternal(int hashCode)
		{
			TextStyle textStyle;
			bool flag = this.m_StyleDictionary.TryGetValue(hashCode, out textStyle);
			TextStyle result;
			if (flag)
			{
				result = textStyle;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public void UpdateStyleDictionaryKey(int old_key, int new_key)
		{
			bool flag = this.m_StyleDictionary.ContainsKey(old_key);
			if (flag)
			{
				TextStyle value = this.m_StyleDictionary[old_key];
				this.m_StyleDictionary.Add(new_key, value);
				this.m_StyleDictionary.Remove(old_key);
			}
		}

		public static void RefreshStyles()
		{
			TextStyleSheet.instance.LoadStyleDictionaryInternal();
		}

		private void LoadStyleDictionaryInternal()
		{
			this.m_StyleDictionary.Clear();
			for (int i = 0; i < this.m_StyleList.Count; i++)
			{
				this.m_StyleList[i].RefreshStyle();
				bool flag = !this.m_StyleDictionary.ContainsKey(this.m_StyleList[i].hashCode);
				if (flag)
				{
					this.m_StyleDictionary.Add(this.m_StyleList[i].hashCode, this.m_StyleList[i]);
				}
			}
		}
	}
}
