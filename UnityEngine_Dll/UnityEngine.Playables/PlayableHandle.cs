using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	[NativeHeader("Runtime/Director/Core/HPlayable.h"), NativeHeader("Runtime/Export/Director/PlayableHandle.bindings.h"), NativeHeader("Runtime/Director/Core/HPlayableGraph.h"), UsedByNativeCode]
	public struct PlayableHandle : IEquatable<PlayableHandle>
	{
		internal IntPtr m_Handle;

		internal uint m_Version;

		private static readonly PlayableHandle m_Null = default(PlayableHandle);

		public static PlayableHandle Null
		{
			get
			{
				return PlayableHandle.m_Null;
			}
		}

		internal T GetObject<T>() where T : class, IPlayableBehaviour
		{
			bool flag = !this.IsValid();
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				object scriptInstance = this.GetScriptInstance();
				bool flag2 = scriptInstance == null;
				if (flag2)
				{
					result = default(T);
				}
				else
				{
					result = (T)((object)scriptInstance);
				}
			}
			return result;
		}

		[VisibleToOtherModules]
		internal bool IsPlayableOfType<T>()
		{
			return this.GetPlayableType() == typeof(T);
		}

		internal Playable GetInput(int inputPort)
		{
			return new Playable(this.GetInputHandle(inputPort));
		}

		internal Playable GetOutput(int outputPort)
		{
			return new Playable(this.GetOutputHandle(outputPort));
		}

		internal bool SetInputWeight(int inputIndex, float weight)
		{
			bool flag = this.CheckInputBounds(inputIndex);
			bool result;
			if (flag)
			{
				this.SetInputWeightFromIndex(inputIndex, weight);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		internal float GetInputWeight(int inputIndex)
		{
			bool flag = this.CheckInputBounds(inputIndex);
			float result;
			if (flag)
			{
				result = this.GetInputWeightFromIndex(inputIndex);
			}
			else
			{
				result = 0f;
			}
			return result;
		}

		internal void Destroy()
		{
			this.GetGraph().DestroyPlayable<Playable>(new Playable(this));
		}

		public static bool operator ==(PlayableHandle x, PlayableHandle y)
		{
			return PlayableHandle.CompareVersion(x, y);
		}

		public static bool operator !=(PlayableHandle x, PlayableHandle y)
		{
			return !PlayableHandle.CompareVersion(x, y);
		}

		public override bool Equals(object p)
		{
			return p is PlayableHandle && this.Equals((PlayableHandle)p);
		}

		public bool Equals(PlayableHandle other)
		{
			return PlayableHandle.CompareVersion(this, other);
		}

		public override int GetHashCode()
		{
			return this.m_Handle.GetHashCode() ^ this.m_Version.GetHashCode();
		}

		internal static bool CompareVersion(PlayableHandle lhs, PlayableHandle rhs)
		{
			return lhs.m_Handle == rhs.m_Handle && lhs.m_Version == rhs.m_Version;
		}

		internal bool CheckInputBounds(int inputIndex)
		{
			return this.CheckInputBounds(inputIndex, false);
		}

		internal bool CheckInputBounds(int inputIndex, bool acceptAny)
		{
			bool flag = inputIndex == -1 & acceptAny;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = inputIndex < 0;
				if (flag2)
				{
					throw new IndexOutOfRangeException("Index must be greater than 0");
				}
				bool flag3 = this.GetInputCount() <= inputIndex;
				if (flag3)
				{
					throw new IndexOutOfRangeException(string.Concat(new string[]
					{
						"inputIndex ",
						inputIndex.ToString(),
						" is greater than the number of available inputs (",
						this.GetInputCount().ToString(),
						")."
					}));
				}
				result = true;
			}
			return result;
		}

		[VisibleToOtherModules]
		internal bool IsNull()
		{
			return PlayableHandle.IsNull_Injected(ref this);
		}

		[VisibleToOtherModules]
		internal bool IsValid()
		{
			return PlayableHandle.IsValid_Injected(ref this);
		}

		[FreeFunction("PlayableHandleBindings::GetPlayableType", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal Type GetPlayableType()
		{
			return PlayableHandle.GetPlayableType_Injected(ref this);
		}

		[FreeFunction("PlayableHandleBindings::GetJobType", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal Type GetJobType()
		{
			return PlayableHandle.GetJobType_Injected(ref this);
		}

		[FreeFunction("PlayableHandleBindings::SetScriptInstance", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal void SetScriptInstance(object scriptInstance)
		{
			PlayableHandle.SetScriptInstance_Injected(ref this, scriptInstance);
		}

		[FreeFunction("PlayableHandleBindings::CanChangeInputs", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal bool CanChangeInputs()
		{
			return PlayableHandle.CanChangeInputs_Injected(ref this);
		}

		[FreeFunction("PlayableHandleBindings::CanSetWeights", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal bool CanSetWeights()
		{
			return PlayableHandle.CanSetWeights_Injected(ref this);
		}

		[FreeFunction("PlayableHandleBindings::CanDestroy", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal bool CanDestroy()
		{
			return PlayableHandle.CanDestroy_Injected(ref this);
		}

		[FreeFunction("PlayableHandleBindings::GetPlayState", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal PlayState GetPlayState()
		{
			return PlayableHandle.GetPlayState_Injected(ref this);
		}

		[FreeFunction("PlayableHandleBindings::Play", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal void Play()
		{
			PlayableHandle.Play_Injected(ref this);
		}

		[FreeFunction("PlayableHandleBindings::Pause", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal void Pause()
		{
			PlayableHandle.Pause_Injected(ref this);
		}

		[FreeFunction("PlayableHandleBindings::GetSpeed", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal double GetSpeed()
		{
			return PlayableHandle.GetSpeed_Injected(ref this);
		}

		[FreeFunction("PlayableHandleBindings::SetSpeed", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal void SetSpeed(double value)
		{
			PlayableHandle.SetSpeed_Injected(ref this, value);
		}

		[FreeFunction("PlayableHandleBindings::GetTime", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal double GetTime()
		{
			return PlayableHandle.GetTime_Injected(ref this);
		}

		[FreeFunction("PlayableHandleBindings::SetTime", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal void SetTime(double value)
		{
			PlayableHandle.SetTime_Injected(ref this, value);
		}

		[FreeFunction("PlayableHandleBindings::IsDone", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal bool IsDone()
		{
			return PlayableHandle.IsDone_Injected(ref this);
		}

		[FreeFunction("PlayableHandleBindings::SetDone", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal void SetDone(bool value)
		{
			PlayableHandle.SetDone_Injected(ref this, value);
		}

		[FreeFunction("PlayableHandleBindings::GetDuration", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal double GetDuration()
		{
			return PlayableHandle.GetDuration_Injected(ref this);
		}

		[FreeFunction("PlayableHandleBindings::SetDuration", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal void SetDuration(double value)
		{
			PlayableHandle.SetDuration_Injected(ref this, value);
		}

		[FreeFunction("PlayableHandleBindings::GetPropagateSetTime", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal bool GetPropagateSetTime()
		{
			return PlayableHandle.GetPropagateSetTime_Injected(ref this);
		}

		[FreeFunction("PlayableHandleBindings::SetPropagateSetTime", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal void SetPropagateSetTime(bool value)
		{
			PlayableHandle.SetPropagateSetTime_Injected(ref this, value);
		}

		[FreeFunction("PlayableHandleBindings::GetGraph", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal PlayableGraph GetGraph()
		{
			PlayableGraph result;
			PlayableHandle.GetGraph_Injected(ref this, out result);
			return result;
		}

		[FreeFunction("PlayableHandleBindings::GetInputCount", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal int GetInputCount()
		{
			return PlayableHandle.GetInputCount_Injected(ref this);
		}

		[FreeFunction("PlayableHandleBindings::SetInputCount", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal void SetInputCount(int value)
		{
			PlayableHandle.SetInputCount_Injected(ref this, value);
		}

		[FreeFunction("PlayableHandleBindings::GetOutputCount", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal int GetOutputCount()
		{
			return PlayableHandle.GetOutputCount_Injected(ref this);
		}

		[FreeFunction("PlayableHandleBindings::SetOutputCount", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal void SetOutputCount(int value)
		{
			PlayableHandle.SetOutputCount_Injected(ref this, value);
		}

		[FreeFunction("PlayableHandleBindings::SetInputWeight", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal void SetInputWeight(PlayableHandle input, float weight)
		{
			PlayableHandle.SetInputWeight_Injected(ref this, ref input, weight);
		}

		[FreeFunction("PlayableHandleBindings::SetDelay", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal void SetDelay(double delay)
		{
			PlayableHandle.SetDelay_Injected(ref this, delay);
		}

		[FreeFunction("PlayableHandleBindings::GetDelay", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal double GetDelay()
		{
			return PlayableHandle.GetDelay_Injected(ref this);
		}

		[FreeFunction("PlayableHandleBindings::IsDelayed", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal bool IsDelayed()
		{
			return PlayableHandle.IsDelayed_Injected(ref this);
		}

		[FreeFunction("PlayableHandleBindings::GetPreviousTime", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal double GetPreviousTime()
		{
			return PlayableHandle.GetPreviousTime_Injected(ref this);
		}

		[FreeFunction("PlayableHandleBindings::SetLeadTime", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal void SetLeadTime(float value)
		{
			PlayableHandle.SetLeadTime_Injected(ref this, value);
		}

		[FreeFunction("PlayableHandleBindings::GetLeadTime", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal float GetLeadTime()
		{
			return PlayableHandle.GetLeadTime_Injected(ref this);
		}

		[FreeFunction("PlayableHandleBindings::GetTraversalMode", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal PlayableTraversalMode GetTraversalMode()
		{
			return PlayableHandle.GetTraversalMode_Injected(ref this);
		}

		[FreeFunction("PlayableHandleBindings::SetTraversalMode", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal void SetTraversalMode(PlayableTraversalMode mode)
		{
			PlayableHandle.SetTraversalMode_Injected(ref this, mode);
		}

		[FreeFunction("PlayableHandleBindings::GetJobData", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal IntPtr GetJobData()
		{
			return PlayableHandle.GetJobData_Injected(ref this);
		}

		[FreeFunction("PlayableHandleBindings::GetTimeWrapMode", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal DirectorWrapMode GetTimeWrapMode()
		{
			return PlayableHandle.GetTimeWrapMode_Injected(ref this);
		}

		[FreeFunction("PlayableHandleBindings::SetTimeWrapMode", HasExplicitThis = true, ThrowsException = true), VisibleToOtherModules]
		internal void SetTimeWrapMode(DirectorWrapMode mode)
		{
			PlayableHandle.SetTimeWrapMode_Injected(ref this, mode);
		}

		[FreeFunction("PlayableHandleBindings::GetScriptInstance", HasExplicitThis = true, ThrowsException = true)]
		private object GetScriptInstance()
		{
			return PlayableHandle.GetScriptInstance_Injected(ref this);
		}

		[FreeFunction("PlayableHandleBindings::GetInputHandle", HasExplicitThis = true, ThrowsException = true)]
		private PlayableHandle GetInputHandle(int index)
		{
			PlayableHandle result;
			PlayableHandle.GetInputHandle_Injected(ref this, index, out result);
			return result;
		}

		[FreeFunction("PlayableHandleBindings::GetOutputHandle", HasExplicitThis = true, ThrowsException = true)]
		private PlayableHandle GetOutputHandle(int index)
		{
			PlayableHandle result;
			PlayableHandle.GetOutputHandle_Injected(ref this, index, out result);
			return result;
		}

		[FreeFunction("PlayableHandleBindings::SetInputWeightFromIndex", HasExplicitThis = true, ThrowsException = true)]
		private void SetInputWeightFromIndex(int index, float weight)
		{
			PlayableHandle.SetInputWeightFromIndex_Injected(ref this, index, weight);
		}

		[FreeFunction("PlayableHandleBindings::GetInputWeightFromIndex", HasExplicitThis = true, ThrowsException = true)]
		private float GetInputWeightFromIndex(int index)
		{
			return PlayableHandle.GetInputWeightFromIndex_Injected(ref this, index);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsNull_Injected(ref PlayableHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsValid_Injected(ref PlayableHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Type GetPlayableType_Injected(ref PlayableHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Type GetJobType_Injected(ref PlayableHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetScriptInstance_Injected(ref PlayableHandle _unity_self, object scriptInstance);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CanChangeInputs_Injected(ref PlayableHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CanSetWeights_Injected(ref PlayableHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CanDestroy_Injected(ref PlayableHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PlayState GetPlayState_Injected(ref PlayableHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Play_Injected(ref PlayableHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Pause_Injected(ref PlayableHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double GetSpeed_Injected(ref PlayableHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSpeed_Injected(ref PlayableHandle _unity_self, double value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double GetTime_Injected(ref PlayableHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTime_Injected(ref PlayableHandle _unity_self, double value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsDone_Injected(ref PlayableHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDone_Injected(ref PlayableHandle _unity_self, bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double GetDuration_Injected(ref PlayableHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDuration_Injected(ref PlayableHandle _unity_self, double value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetPropagateSetTime_Injected(ref PlayableHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPropagateSetTime_Injected(ref PlayableHandle _unity_self, bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGraph_Injected(ref PlayableHandle _unity_self, out PlayableGraph ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetInputCount_Injected(ref PlayableHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetInputCount_Injected(ref PlayableHandle _unity_self, int value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetOutputCount_Injected(ref PlayableHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetOutputCount_Injected(ref PlayableHandle _unity_self, int value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetInputWeight_Injected(ref PlayableHandle _unity_self, ref PlayableHandle input, float weight);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDelay_Injected(ref PlayableHandle _unity_self, double delay);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double GetDelay_Injected(ref PlayableHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsDelayed_Injected(ref PlayableHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double GetPreviousTime_Injected(ref PlayableHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLeadTime_Injected(ref PlayableHandle _unity_self, float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetLeadTime_Injected(ref PlayableHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PlayableTraversalMode GetTraversalMode_Injected(ref PlayableHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTraversalMode_Injected(ref PlayableHandle _unity_self, PlayableTraversalMode mode);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetJobData_Injected(ref PlayableHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern DirectorWrapMode GetTimeWrapMode_Injected(ref PlayableHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTimeWrapMode_Injected(ref PlayableHandle _unity_self, DirectorWrapMode mode);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object GetScriptInstance_Injected(ref PlayableHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetInputHandle_Injected(ref PlayableHandle _unity_self, int index, out PlayableHandle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetOutputHandle_Injected(ref PlayableHandle _unity_self, int index, out PlayableHandle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetInputWeightFromIndex_Injected(ref PlayableHandle _unity_self, int index, float weight);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetInputWeightFromIndex_Injected(ref PlayableHandle _unity_self, int index);
	}
}
