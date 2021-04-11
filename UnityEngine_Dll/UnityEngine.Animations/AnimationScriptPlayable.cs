using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	[NativeHeader("Runtime/Director/Core/HPlayable.h"), NativeHeader("Runtime/Director/Core/HPlayableGraph.h"), NativeHeader("Modules/Animation/ScriptBindings/AnimationScriptPlayable.bindings.h"), StaticAccessor("AnimationScriptPlayableBindings", StaticAccessorType.DoubleColon), MovedFrom("UnityEngine.Experimental.Animations"), RequiredByNativeCode]
	public struct AnimationScriptPlayable : IAnimationJobPlayable, IPlayable, IEquatable<AnimationScriptPlayable>
	{
		private PlayableHandle m_Handle;

		private static readonly AnimationScriptPlayable m_NullPlayable = new AnimationScriptPlayable(PlayableHandle.Null);

		public static AnimationScriptPlayable Null
		{
			get
			{
				return AnimationScriptPlayable.m_NullPlayable;
			}
		}

		public static AnimationScriptPlayable Create<T>(PlayableGraph graph, T jobData, int inputCount = 0) where T : struct, IAnimationJob
		{
			PlayableHandle handle = AnimationScriptPlayable.CreateHandle<T>(graph, inputCount);
			AnimationScriptPlayable result = new AnimationScriptPlayable(handle);
			result.SetJobData<T>(jobData);
			return result;
		}

		private static PlayableHandle CreateHandle<T>(PlayableGraph graph, int inputCount) where T : struct, IAnimationJob
		{
			IntPtr jobReflectionData = ProcessAnimationJobStruct<T>.GetJobReflectionData();
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimationScriptPlayable.CreateHandleInternal(graph, ref @null, jobReflectionData);
			PlayableHandle result;
			if (flag)
			{
				result = PlayableHandle.Null;
			}
			else
			{
				@null.SetInputCount(inputCount);
				result = @null;
			}
			return result;
		}

		internal AnimationScriptPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationScriptPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationScriptPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		private void CheckJobTypeValidity<T>()
		{
			Type jobType = this.GetHandle().GetJobType();
			bool flag = jobType != typeof(T);
			if (flag)
			{
				throw new ArgumentException(string.Format("Wrong type: the given job type ({0}) is different from the creation job type ({1}).", typeof(T).FullName, jobType.FullName));
			}
		}

		public unsafe T GetJobData<T>() where T : struct, IAnimationJob
		{
			this.CheckJobTypeValidity<T>();
			T result;
			UnsafeUtility.CopyPtrToStructure<T>((void*)this.GetHandle().GetJobData(), out result);
			return result;
		}

		public unsafe void SetJobData<T>(T jobData) where T : struct, IAnimationJob
		{
			this.CheckJobTypeValidity<T>();
			UnsafeUtility.CopyStructureToPtr<T>(ref jobData, (void*)this.GetHandle().GetJobData());
		}

		public static implicit operator Playable(AnimationScriptPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		public static explicit operator AnimationScriptPlayable(Playable playable)
		{
			return new AnimationScriptPlayable(playable.GetHandle());
		}

		public bool Equals(AnimationScriptPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		public void SetProcessInputs(bool value)
		{
			AnimationScriptPlayable.SetProcessInputsInternal(this.GetHandle(), value);
		}

		public bool GetProcessInputs()
		{
			return AnimationScriptPlayable.GetProcessInputsInternal(this.GetHandle());
		}

		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle, IntPtr jobReflectionData)
		{
			return AnimationScriptPlayable.CreateHandleInternal_Injected(ref graph, ref handle, jobReflectionData);
		}

		[NativeThrows]
		private static void SetProcessInputsInternal(PlayableHandle handle, bool value)
		{
			AnimationScriptPlayable.SetProcessInputsInternal_Injected(ref handle, value);
		}

		[NativeThrows]
		private static bool GetProcessInputsInternal(PlayableHandle handle)
		{
			return AnimationScriptPlayable.GetProcessInputsInternal_Injected(ref handle);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, ref PlayableHandle handle, IntPtr jobReflectionData);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetProcessInputsInternal_Injected(ref PlayableHandle handle, bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetProcessInputsInternal_Injected(ref PlayableHandle handle);
	}
}
