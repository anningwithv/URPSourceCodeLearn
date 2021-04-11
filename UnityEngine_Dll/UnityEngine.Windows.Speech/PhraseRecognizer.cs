using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Windows.Speech
{
	public abstract class PhraseRecognizer : IDisposable
	{
		public delegate void PhraseRecognizedDelegate(PhraseRecognizedEventArgs args);

		protected IntPtr m_Recognizer;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event PhraseRecognizer.PhraseRecognizedDelegate OnPhraseRecognized;

		public bool IsRunning
		{
			get
			{
				return this.m_Recognizer != IntPtr.Zero && PhraseRecognizer.IsRunning_Internal(this.m_Recognizer);
			}
		}

		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h"), NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		protected static extern IntPtr CreateFromKeywords(object self, string[] keywords, ConfidenceLevel minimumConfidence);

		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h"), NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		protected static extern IntPtr CreateFromGrammarFile(object self, string grammarFilePath, ConfidenceLevel minimumConfidence);

		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h"), NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Start_Internal(IntPtr recognizer);

		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Stop_Internal(IntPtr recognizer);

		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsRunning_Internal(IntPtr recognizer);

		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy(IntPtr recognizer);

		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyThreaded(IntPtr recognizer);

		internal PhraseRecognizer()
		{
		}

		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_Recognizer != IntPtr.Zero;
				if (flag)
				{
					PhraseRecognizer.DestroyThreaded(this.m_Recognizer);
					this.m_Recognizer = IntPtr.Zero;
					GC.SuppressFinalize(this);
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		public void Start()
		{
			bool flag = this.m_Recognizer == IntPtr.Zero;
			if (!flag)
			{
				PhraseRecognizer.Start_Internal(this.m_Recognizer);
			}
		}

		public void Stop()
		{
			bool flag = this.m_Recognizer == IntPtr.Zero;
			if (!flag)
			{
				PhraseRecognizer.Stop_Internal(this.m_Recognizer);
			}
		}

		public void Dispose()
		{
			bool flag = this.m_Recognizer != IntPtr.Zero;
			if (flag)
			{
				PhraseRecognizer.Destroy(this.m_Recognizer);
				this.m_Recognizer = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		[RequiredByNativeCode]
		private void InvokePhraseRecognizedEvent(string text, ConfidenceLevel confidence, SemanticMeaning[] semanticMeanings, long phraseStartFileTime, long phraseDurationTicks)
		{
			PhraseRecognizer.PhraseRecognizedDelegate onPhraseRecognized = this.OnPhraseRecognized;
			bool flag = onPhraseRecognized != null;
			if (flag)
			{
				onPhraseRecognized(new PhraseRecognizedEventArgs(text, confidence, semanticMeanings, DateTime.FromFileTime(phraseStartFileTime), TimeSpan.FromTicks(phraseDurationTicks)));
			}
		}

		[RequiredByNativeCode]
		private unsafe static SemanticMeaning[] MarshalSemanticMeaning(IntPtr keys, IntPtr values, IntPtr valueSizes, int valueCount)
		{
			SemanticMeaning[] array = new SemanticMeaning[valueCount];
			int num = 0;
			for (int i = 0; i < valueCount; i++)
			{
				uint num2 = *(uint*)((byte*)((void*)valueSizes) + (IntPtr)i * 4);
				SemanticMeaning semanticMeaning = new SemanticMeaning
				{
					key = new string(*(IntPtr*)((byte*)((void*)keys) + (IntPtr)i * (IntPtr)sizeof(char*))),
					values = new string[num2]
				};
				int num3 = 0;
				while ((long)num3 < (long)((ulong)num2))
				{
					semanticMeaning.values[num3] = new string(*(IntPtr*)((byte*)((void*)values) + (IntPtr)(num + num3) * (IntPtr)sizeof(char*)));
					num3++;
				}
				array[i] = semanticMeaning;
				num += (int)num2;
			}
			return array;
		}
	}
}
