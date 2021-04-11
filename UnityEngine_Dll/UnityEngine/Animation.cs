using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	[NativeHeader("Modules/Animation/Animation.h")]
	public sealed class Animation : Behaviour, IEnumerable
	{
		private sealed class Enumerator : IEnumerator
		{
			private Animation m_Outer;

			private int m_CurrentIndex = -1;

			public object Current
			{
				get
				{
					return this.m_Outer.GetStateAtIndex(this.m_CurrentIndex);
				}
			}

			internal Enumerator(Animation outer)
			{
				this.m_Outer = outer;
			}

			public bool MoveNext()
			{
				int stateCount = this.m_Outer.GetStateCount();
				this.m_CurrentIndex++;
				return this.m_CurrentIndex < stateCount;
			}

			public void Reset()
			{
				this.m_CurrentIndex = -1;
			}
		}

		public extern AnimationClip clip
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool playAutomatically
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern WrapMode wrapMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool isPlaying
		{
			[NativeName("IsPlaying")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public AnimationState this[string name]
		{
			get
			{
				return this.GetState(name);
			}
		}

		public extern bool animatePhysics
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("Use cullingType instead")]
		public extern bool animateOnlyIfVisible
		{
			[FreeFunction("AnimationBindings::GetAnimateOnlyIfVisible", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("AnimationBindings::SetAnimateOnlyIfVisible", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern AnimationCullingType cullingType
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Bounds localBounds
		{
			[NativeName("GetLocalAABB")]
			get
			{
				Bounds result;
				this.get_localBounds_Injected(out result);
				return result;
			}
			[NativeName("SetLocalAABB")]
			set
			{
				this.set_localBounds_Injected(ref value);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Stop();

		public void Stop(string name)
		{
			this.StopNamed(name);
		}

		[NativeName("Stop")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void StopNamed(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Rewind();

		public void Rewind(string name)
		{
			this.RewindNamed(name);
		}

		[NativeName("Rewind")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RewindNamed(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Sample();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsPlaying(string name);

		[ExcludeFromDocs]
		public bool Play()
		{
			return this.Play(PlayMode.StopSameLayer);
		}

		public bool Play([DefaultValue("PlayMode.StopSameLayer")] PlayMode mode)
		{
			return this.PlayDefaultAnimation(mode);
		}

		[NativeName("Play")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool PlayDefaultAnimation(PlayMode mode);

		[ExcludeFromDocs]
		public bool Play(string animation)
		{
			return this.Play(animation, PlayMode.StopSameLayer);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool Play(string animation, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		[ExcludeFromDocs]
		public void CrossFade(string animation)
		{
			this.CrossFade(animation, 0.3f);
		}

		[ExcludeFromDocs]
		public void CrossFade(string animation, float fadeLength)
		{
			this.CrossFade(animation, fadeLength, PlayMode.StopSameLayer);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CrossFade(string animation, [DefaultValue("0.3F")] float fadeLength, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		[ExcludeFromDocs]
		public void Blend(string animation)
		{
			this.Blend(animation, 1f);
		}

		[ExcludeFromDocs]
		public void Blend(string animation, float targetWeight)
		{
			this.Blend(animation, targetWeight, 0.3f);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Blend(string animation, [DefaultValue("1.0F")] float targetWeight, [DefaultValue("0.3F")] float fadeLength);

		[ExcludeFromDocs]
		public AnimationState CrossFadeQueued(string animation)
		{
			return this.CrossFadeQueued(animation, 0.3f);
		}

		[ExcludeFromDocs]
		public AnimationState CrossFadeQueued(string animation, float fadeLength)
		{
			return this.CrossFadeQueued(animation, fadeLength, QueueMode.CompleteOthers);
		}

		[ExcludeFromDocs]
		public AnimationState CrossFadeQueued(string animation, float fadeLength, QueueMode queue)
		{
			return this.CrossFadeQueued(animation, fadeLength, queue, PlayMode.StopSameLayer);
		}

		[FreeFunction("AnimationBindings::CrossFadeQueuedImpl", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AnimationState CrossFadeQueued(string animation, [DefaultValue("0.3F")] float fadeLength, [DefaultValue("QueueMode.CompleteOthers")] QueueMode queue, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		[ExcludeFromDocs]
		public AnimationState PlayQueued(string animation)
		{
			return this.PlayQueued(animation, QueueMode.CompleteOthers);
		}

		[ExcludeFromDocs]
		public AnimationState PlayQueued(string animation, QueueMode queue)
		{
			return this.PlayQueued(animation, queue, PlayMode.StopSameLayer);
		}

		[FreeFunction("AnimationBindings::PlayQueuedImpl", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AnimationState PlayQueued(string animation, [DefaultValue("QueueMode.CompleteOthers")] QueueMode queue, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		public void AddClip(AnimationClip clip, string newName)
		{
			this.AddClip(clip, newName, -2147483648, 2147483647);
		}

		[ExcludeFromDocs]
		public void AddClip(AnimationClip clip, string newName, int firstFrame, int lastFrame)
		{
			this.AddClip(clip, newName, firstFrame, lastFrame, false);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddClip([NotNull("NullExceptionObject")] AnimationClip clip, string newName, int firstFrame, int lastFrame, [DefaultValue("false")] bool addLoopFrame);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveClip([NotNull("NullExceptionObject")] AnimationClip clip);

		public void RemoveClip(string clipName)
		{
			this.RemoveClipNamed(clipName);
		}

		[NativeName("RemoveClip")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RemoveClipNamed(string clipName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetClipCount();

		[Obsolete("use PlayMode instead of AnimationPlayMode.")]
		public bool Play(AnimationPlayMode mode)
		{
			return this.PlayDefaultAnimation((PlayMode)mode);
		}

		[Obsolete("use PlayMode instead of AnimationPlayMode.")]
		public bool Play(string animation, AnimationPlayMode mode)
		{
			return this.Play(animation, (PlayMode)mode);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SyncLayer(int layer);

		public IEnumerator GetEnumerator()
		{
			return new Animation.Enumerator(this);
		}

		[FreeFunction("AnimationBindings::GetState", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern AnimationState GetState(string name);

		[FreeFunction("AnimationBindings::GetStateAtIndex", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern AnimationState GetStateAtIndex(int index);

		[NativeName("GetAnimationStateCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetStateCount();

		public AnimationClip GetClip(string name)
		{
			AnimationState state = this.GetState(name);
			bool flag = state;
			AnimationClip result;
			if (flag)
			{
				result = state.clip;
			}
			else
			{
				result = null;
			}
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_localBounds_Injected(out Bounds ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_localBounds_Injected(ref Bounds value);
	}
}
