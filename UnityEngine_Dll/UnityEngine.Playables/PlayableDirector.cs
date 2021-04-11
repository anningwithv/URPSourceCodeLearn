using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	[NativeHeader("Modules/Director/PlayableDirector.h"), NativeHeader("Runtime/Mono/MonoBehaviour.h"), RequiredByNativeCode]
	public class PlayableDirector : Behaviour, IExposedPropertyTable
	{
		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event Action<PlayableDirector> played;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event Action<PlayableDirector> paused;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event Action<PlayableDirector> stopped;

		public PlayState state
		{
			get
			{
				return this.GetPlayState();
			}
		}

		public DirectorWrapMode extrapolationMode
		{
			get
			{
				return this.GetWrapMode();
			}
			set
			{
				this.SetWrapMode(value);
			}
		}

		public PlayableAsset playableAsset
		{
			get
			{
				return this.Internal_GetPlayableAsset() as PlayableAsset;
			}
			set
			{
				this.SetPlayableAsset(value);
			}
		}

		public PlayableGraph playableGraph
		{
			get
			{
				return this.GetGraphHandle();
			}
		}

		public bool playOnAwake
		{
			get
			{
				return this.GetPlayOnAwake();
			}
			set
			{
				this.SetPlayOnAwake(value);
			}
		}

		public extern DirectorUpdateMode timeUpdateMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern double time
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern double initialTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern double duration
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public void DeferredEvaluate()
		{
			this.EvaluateNextFrame();
		}

		public void Play(PlayableAsset asset)
		{
			bool flag = asset == null;
			if (flag)
			{
				throw new ArgumentNullException("asset");
			}
			this.Play(asset, this.extrapolationMode);
		}

		public void Play(PlayableAsset asset, DirectorWrapMode mode)
		{
			bool flag = asset == null;
			if (flag)
			{
				throw new ArgumentNullException("asset");
			}
			this.playableAsset = asset;
			this.extrapolationMode = mode;
			this.Play();
		}

		public void SetGenericBinding(UnityEngine.Object key, UnityEngine.Object value)
		{
			this.Internal_SetGenericBinding(key, value);
		}

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Evaluate();

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Play();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Stop();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Pause();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Resume();

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RebuildGraph();

		public void ClearReferenceValue(PropertyName id)
		{
			this.ClearReferenceValue_Injected(ref id);
		}

		public void SetReferenceValue(PropertyName id, UnityEngine.Object value)
		{
			this.SetReferenceValue_Injected(ref id, value);
		}

		public UnityEngine.Object GetReferenceValue(PropertyName id, out bool idValid)
		{
			return this.GetReferenceValue_Injected(ref id, out idValid);
		}

		[NativeMethod("GetBindingFor")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern UnityEngine.Object GetGenericBinding(UnityEngine.Object key);

		[NativeMethod("ClearBindingFor")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ClearGenericBinding(UnityEngine.Object key);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RebindPlayableGraphOutputs();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void ProcessPendingGraphChanges();

		[NativeMethod("HasBinding")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool HasGenericBinding(UnityEngine.Object key);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern PlayState GetPlayState();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetWrapMode(DirectorWrapMode mode);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern DirectorWrapMode GetWrapMode();

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void EvaluateNextFrame();

		private PlayableGraph GetGraphHandle()
		{
			PlayableGraph result;
			this.GetGraphHandle_Injected(out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPlayOnAwake(bool on);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetPlayOnAwake();

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetGenericBinding(UnityEngine.Object key, UnityEngine.Object value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPlayableAsset(ScriptableObject asset);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern ScriptableObject Internal_GetPlayableAsset();

		[NativeHeader("Runtime/Director/Core/DirectorManager.h"), StaticAccessor("GetDirectorManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ResetFrameTiming();

		[RequiredByNativeCode]
		private void SendOnPlayableDirectorPlay()
		{
			bool flag = this.played != null;
			if (flag)
			{
				this.played(this);
			}
		}

		[RequiredByNativeCode]
		private void SendOnPlayableDirectorPause()
		{
			bool flag = this.paused != null;
			if (flag)
			{
				this.paused(this);
			}
		}

		[RequiredByNativeCode]
		private void SendOnPlayableDirectorStop()
		{
			bool flag = this.stopped != null;
			if (flag)
			{
				this.stopped(this);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ClearReferenceValue_Injected(ref PropertyName id);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetReferenceValue_Injected(ref PropertyName id, UnityEngine.Object value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityEngine.Object GetReferenceValue_Injected(ref PropertyName id, out bool idValid);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetGraphHandle_Injected(out PlayableGraph ret);
	}
}
