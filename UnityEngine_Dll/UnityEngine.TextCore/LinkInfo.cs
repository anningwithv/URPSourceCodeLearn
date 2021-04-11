using System;

namespace UnityEngine.TextCore
{
	internal struct LinkInfo
	{
		public int hashCode;

		public int linkIdFirstCharacterIndex;

		public int linkIdLength;

		public int linkTextfirstCharacterIndex;

		public int linkTextLength;

		internal char[] linkId;

		internal void SetLinkId(char[] text, int startIndex, int length)
		{
			bool flag = this.linkId == null || this.linkId.Length < length;
			if (flag)
			{
				this.linkId = new char[length];
			}
			for (int i = 0; i < length; i++)
			{
				this.linkId[i] = text[startIndex + i];
			}
		}

		public string GetLinkText(TextInfo textInfo)
		{
			string text = string.Empty;
			for (int i = this.linkTextfirstCharacterIndex; i < this.linkTextfirstCharacterIndex + this.linkTextLength; i++)
			{
				text += textInfo.textElementInfo[i].character.ToString();
			}
			return text;
		}

		public string GetLinkId()
		{
			return new string(this.linkId, 0, this.linkIdLength);
		}
	}
}
