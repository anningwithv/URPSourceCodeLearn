using System;
using System.Collections.Generic;

namespace UnityEngine.Windows.Speech
{
	public sealed class KeywordRecognizer : PhraseRecognizer
	{
		public IEnumerable<string> Keywords
		{
			get;
			private set;
		}

		public KeywordRecognizer(string[] keywords) : this(keywords, ConfidenceLevel.Medium)
		{
		}

		public KeywordRecognizer(string[] keywords, ConfidenceLevel minimumConfidence)
		{
			bool flag = keywords == null;
			if (flag)
			{
				throw new ArgumentNullException("keywords");
			}
			bool flag2 = keywords.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("At least one keyword must be specified.", "keywords");
			}
			int num = keywords.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag3 = keywords[i] == null;
				if (flag3)
				{
					throw new ArgumentNullException(string.Format("Keyword at index {0} is null.", i));
				}
			}
			this.Keywords = keywords;
			this.m_Recognizer = PhraseRecognizer.CreateFromKeywords(this, keywords, minimumConfidence);
		}
	}
}
