using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Windows.Speech
{
	public sealed class DictationRecognizer : IDisposable
	{
		public delegate void DictationHypothesisDelegate(string text);

		public delegate void DictationResultDelegate(string text, ConfidenceLevel confidence);

		public delegate void DictationCompletedDelegate(DictationCompletionCause cause);

		public delegate void DictationErrorHandler(string error, int hresult);

		private IntPtr m_Recognizer;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event DictationRecognizer.DictationHypothesisDelegate DictationHypothesis;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event DictationRecognizer.DictationResultDelegate DictationResult;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event DictationRecognizer.DictationCompletedDelegate DictationComplete;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event DictationRecognizer.DictationErrorHandler DictationError;

		public SpeechSystemStatus Status
		{
			get
			{
				return (this.m_Recognizer != IntPtr.Zero) ? DictationRecognizer.GetStatus(this.m_Recognizer) : SpeechSystemStatus.Stopped;
			}
		}

		public float AutoSilenceTimeoutSeconds
		{
			get
			{
				bool flag = this.m_Recognizer == IntPtr.Zero;
				float result;
				if (flag)
				{
					result = 0f;
				}
				else
				{
					result = DictationRecognizer.GetAutoSilenceTimeoutSeconds(this.m_Recognizer);
				}
				return result;
			}
			set
			{
				bool flag = this.m_Recognizer == IntPtr.Zero;
				if (!flag)
				{
					DictationRecognizer.SetAutoSilenceTimeoutSeconds(this.m_Recognizer, value);
				}
			}
		}

		public float InitialSilenceTimeoutSeconds
		{
			get
			{
				bool flag = this.m_Recognizer == IntPtr.Zero;
				float result;
				if (flag)
				{
					result = 0f;
				}
				else
				{
					result = DictationRecognizer.GetInitialSilenceTimeoutSeconds(this.m_Recognizer);
				}
				return result;
			}
			set
			{
				bool flag = this.m_Recognizer == IntPtr.Zero;
				if (!flag)
				{
					DictationRecognizer.SetInitialSilenceTimeoutSeconds(this.m_Recognizer, value);
				}
			}
		}

		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h"), NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create(object self, ConfidenceLevel minimumConfidence, DictationTopicConstraint topicConstraint);

		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h"), NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Start(IntPtr self);

		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Stop(IntPtr self);

		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy(IntPtr self);

		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyThreaded(IntPtr self);

		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern SpeechSystemStatus GetStatus(IntPtr self);

		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetAutoSilenceTimeoutSeconds(IntPtr self);

		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAutoSilenceTimeoutSeconds(IntPtr self, float value);

		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetInitialSilenceTimeoutSeconds(IntPtr self);

		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetInitialSilenceTimeoutSeconds(IntPtr self, float value);

		public DictationRecognizer() : this(ConfidenceLevel.Medium, DictationTopicConstraint.Dictation)
		{
		}

		public DictationRecognizer(ConfidenceLevel confidenceLevel) : this(confidenceLevel, DictationTopicConstraint.Dictation)
		{
		}

		public DictationRecognizer(DictationTopicConstraint topic) : this(ConfidenceLevel.Medium, topic)
		{
		}

		public DictationRecognizer(ConfidenceLevel minimumConfidence, DictationTopicConstraint topic)
		{
			this.m_Recognizer = DictationRecognizer.Create(this, minimumConfidence, topic);
		}

		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_Recognizer != IntPtr.Zero;
				if (flag)
				{
					DictationRecognizer.DestroyThreaded(this.m_Recognizer);
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
				DictationRecognizer.Start(this.m_Recognizer);
			}
		}

		public void Stop()
		{
			bool flag = this.m_Recognizer == IntPtr.Zero;
			if (!flag)
			{
				DictationRecognizer.Stop(this.m_Recognizer);
			}
		}

		public void Dispose()
		{
			bool flag = this.m_Recognizer != IntPtr.Zero;
			if (flag)
			{
				DictationRecognizer.Destroy(this.m_Recognizer);
				this.m_Recognizer = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		[RequiredByNativeCode]
		private void DictationRecognizer_InvokeHypothesisGeneratedEvent(string keyword)
		{
			DictationRecognizer.DictationHypothesisDelegate dictationHypothesis = this.DictationHypothesis;
			bool flag = dictationHypothesis != null;
			if (flag)
			{
				dictationHypothesis(keyword);
			}
		}

		[RequiredByNativeCode]
		private void DictationRecognizer_InvokeResultGeneratedEvent(string keyword, ConfidenceLevel minimumConfidence)
		{
			DictationRecognizer.DictationResultDelegate dictationResult = this.DictationResult;
			bool flag = dictationResult != null;
			if (flag)
			{
				dictationResult(keyword, minimumConfidence);
			}
		}

		[RequiredByNativeCode]
		private void DictationRecognizer_InvokeCompletedEvent(DictationCompletionCause cause)
		{
			DictationRecognizer.DictationCompletedDelegate dictationComplete = this.DictationComplete;
			bool flag = dictationComplete != null;
			if (flag)
			{
				dictationComplete(cause);
			}
		}

		[RequiredByNativeCode]
		private void DictationRecognizer_InvokeErrorEvent(string error, int hresult)
		{
			DictationRecognizer.DictationErrorHandler dictationError = this.DictationError;
			bool flag = dictationError != null;
			if (flag)
			{
				dictationError(error, hresult);
			}
		}
	}
}
