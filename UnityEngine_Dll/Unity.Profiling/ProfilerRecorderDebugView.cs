using System;

namespace Unity.Profiling
{
	internal sealed class ProfilerRecorderDebugView
	{
		private ProfilerRecorder m_Recorder;

		public ProfilerRecorderSample[] Items
		{
			get
			{
				return this.m_Recorder.ToArray();
			}
		}

		public ProfilerRecorderDebugView(ProfilerRecorder r)
		{
			this.m_Recorder = r;
		}
	}
}
