using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Playables
{
	[NativeHeader("Runtime/Export/Director/CameraPlayable.bindings.h"), NativeHeader("Runtime/Director/Core/HPlayable.h"), NativeHeader("Runtime/Camera//Director/CameraPlayable.h"), StaticAccessor("CameraPlayableBindings", StaticAccessorType.DoubleColon), RequiredByNativeCode]
	public struct CameraPlayable : IPlayable, IEquatable<CameraPlayable>
	{
		private PlayableHandle m_Handle;

		public static CameraPlayable Create(PlayableGraph graph, Camera camera)
		{
			PlayableHandle handle = CameraPlayable.CreateHandle(graph, camera);
			return new CameraPlayable(handle);
		}

		private static PlayableHandle CreateHandle(PlayableGraph graph, Camera camera)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !CameraPlayable.InternalCreateCameraPlayable(ref graph, camera, ref @null);
			PlayableHandle result;
			if (flag)
			{
				result = PlayableHandle.Null;
			}
			else
			{
				result = @null;
			}
			return result;
		}

		internal CameraPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<CameraPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an CameraPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		public static implicit operator Playable(CameraPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		public static explicit operator CameraPlayable(Playable playable)
		{
			return new CameraPlayable(playable.GetHandle());
		}

		public bool Equals(CameraPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		public Camera GetCamera()
		{
			return CameraPlayable.GetCameraInternal(ref this.m_Handle);
		}

		public void SetCamera(Camera value)
		{
			CameraPlayable.SetCameraInternal(ref this.m_Handle, value);
		}

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Camera GetCameraInternal(ref PlayableHandle hdl);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetCameraInternal(ref PlayableHandle hdl, Camera camera);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalCreateCameraPlayable(ref PlayableGraph graph, Camera camera, ref PlayableHandle handle);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ValidateType(ref PlayableHandle hdl);
	}
}
