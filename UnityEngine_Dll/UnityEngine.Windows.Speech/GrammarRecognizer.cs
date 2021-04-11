using System;

namespace UnityEngine.Windows.Speech
{
	public sealed class GrammarRecognizer : PhraseRecognizer
	{
		public string GrammarFilePath
		{
			get;
			private set;
		}

		public GrammarRecognizer(string grammarFilePath) : this(grammarFilePath, ConfidenceLevel.Medium)
		{
		}

		public GrammarRecognizer(string grammarFilePath, ConfidenceLevel minimumConfidence)
		{
			bool flag = grammarFilePath == null;
			if (flag)
			{
				throw new ArgumentNullException("grammarFilePath");
			}
			bool flag2 = grammarFilePath.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Grammar file path cannot be empty.");
			}
			this.GrammarFilePath = grammarFilePath;
			this.m_Recognizer = PhraseRecognizer.CreateFromGrammarFile(this, grammarFilePath, minimumConfidence);
		}
	}
}
