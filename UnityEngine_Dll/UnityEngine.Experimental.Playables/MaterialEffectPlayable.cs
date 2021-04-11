using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Playables
{
	[NativeHeader("Runtime/Director/Core/HPlayable.h"), NativeHeader("Runtime/Shaders/Director/MaterialEffectPlayable.h"), NativeHeader("Runtime/Export/Director/MaterialEffectPlayable.bindings.h"), StaticAccessor("MaterialEffectPlayableBindings", StaticAccessorType.DoubleColon), RequiredByNativeCode]
	public struct MaterialEffectPlayable : IPlayable, IEquatable<MaterialEffectPlayable>
	{
		private PlayableHandle m_Handle;

		public static MaterialEffectPlayable Create(PlayableGraph graph, Material material, int pass = -1)
		{
			PlayableHandle handle = MaterialEffectPlayable.CreateHandle(graph, material, pass);
			return new MaterialEffectPlayable(handle);
		}

		private static PlayableHandle CreateHandle(PlayableGraph graph, Material material, int pass)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !MaterialEffectPlayable.InternalCreateMaterialEffectPlayable(ref graph, material, pass, ref @null);
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

		internal MaterialEffectPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<MaterialEffectPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an MaterialEffectPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		public static implicit operator Playable(MaterialEffectPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		public static explicit operator MaterialEffectPlayable(Playable playable)
		{
			return new MaterialEffectPlayable(playable.GetHandle());
		}

		public bool Equals(MaterialEffectPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		public Material GetMaterial()
		{
			return MaterialEffectPlayable.GetMaterialInternal(ref this.m_Handle);
		}

		public void SetMaterial(Material value)
		{
			MaterialEffectPlayable.SetMaterialInternal(ref this.m_Handle, value);
		}

		public int GetPass()
		{
			return MaterialEffectPlayable.GetPassInternal(ref this.m_Handle);
		}

		public void SetPass(int value)
		{
			MaterialEffectPlayable.SetPassInternal(ref this.m_Handle, value);
		}

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Material GetMaterialInternal(ref PlayableHandle hdl);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMaterialInternal(ref PlayableHandle hdl, Material material);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPassInternal(ref PlayableHandle hdl);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPassInternal(ref PlayableHandle hdl, int pass);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalCreateMaterialEffectPlayable(ref PlayableGraph graph, Material material, int pass, ref PlayableHandle handle);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ValidateType(ref PlayableHandle hdl);
	}
}
