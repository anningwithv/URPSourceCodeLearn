using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	[NativeHeader("Runtime/Director/Core/HPlayable.h"), NativeHeader("Runtime/Director/Core/HPlayableOutput.h"), NativeHeader("Runtime/Export/Director/PlayableOutputHandle.bindings.h"), UsedByNativeCode]
	public struct PlayableOutputHandle : IEquatable<PlayableOutputHandle>
	{
		internal IntPtr m_Handle;

		internal uint m_Version;

		private static readonly PlayableOutputHandle m_Null = default(PlayableOutputHandle);

		public static PlayableOutputHandle Null
		{
			get
			{
				return PlayableOutputHandle.m_Null;
			}
		}

		[VisibleToOtherModules]
		internal bool IsPlayableOutputOfType<T>()
		{
			return this.GetPlayableOutputType() == typeof(T);
		}

		public override int GetHashCode()
		{
			return this.m_Handle.GetHashCode() ^ this.m_Version.GetHashCode();
		}

		public static bool operator ==(PlayableOutputHandle lhs, PlayableOutputHandle rhs)
		{
			return PlayableOutputHandle.CompareVersion(lhs, rhs);
		}

		public static bool operator !=(PlayableOutputHandle lhs, PlayableOutputHandle rhs)
		{
			return !PlayableOutputHandle.CompareVersion(lhs, rhs);
		}

		public override bool Equals(object p)
		{
			return p is PlayableOutputHandle && this.Equals((PlayableOutputHandle)p);
		}

		public bool Equals(PlayableOutputHandle other)
		{
			return PlayableOutputHandle.CompareVersion(this, other);
		}

		internal static bool CompareVersion(PlayableOutputHandle lhs, PlayableOutputHandle rhs)
		{
			return lhs.m_Handle == rhs.m_Handle && lhs.m_Version == rhs.m_Version;
		}

		[VisibleToOtherModules]
		internal bool IsNull()
		{
			return PlayableOutputHandle.IsNull_Injected(ref this);
		}

		[VisibleToOtherModules]
		internal bool IsValid()
		{
			return PlayableOutputHandle.IsValid_Injected(ref this);
		}

		[FreeFunction("PlayableOutputHandleBindings::GetPlayableOutputType", HasExplicitThis = true, ThrowsException = true)]
		internal Type GetPlayableOutputType()
		{
			return PlayableOutputHandle.GetPlayableOutputType_Injected(ref this);
		}

		[FreeFunction("PlayableOutputHandleBindings::GetReferenceObject", HasExplicitThis = true, ThrowsException = true)]
		internal UnityEngine.Object GetReferenceObject()
		{
			return PlayableOutputHandle.GetReferenceObject_Injected(ref this);
		}

		[FreeFunction("PlayableOutputHandleBindings::SetReferenceObject", HasExplicitThis = true, ThrowsException = true)]
		internal void SetReferenceObject(UnityEngine.Object target)
		{
			PlayableOutputHandle.SetReferenceObject_Injected(ref this, target);
		}

		[FreeFunction("PlayableOutputHandleBindings::GetUserData", HasExplicitThis = true, ThrowsException = true)]
		internal UnityEngine.Object GetUserData()
		{
			return PlayableOutputHandle.GetUserData_Injected(ref this);
		}

		[FreeFunction("PlayableOutputHandleBindings::SetUserData", HasExplicitThis = true, ThrowsException = true)]
		internal void SetUserData([Writable] UnityEngine.Object target)
		{
			PlayableOutputHandle.SetUserData_Injected(ref this, target);
		}

		[FreeFunction("PlayableOutputHandleBindings::GetSourcePlayable", HasExplicitThis = true, ThrowsException = true)]
		internal PlayableHandle GetSourcePlayable()
		{
			PlayableHandle result;
			PlayableOutputHandle.GetSourcePlayable_Injected(ref this, out result);
			return result;
		}

		[FreeFunction("PlayableOutputHandleBindings::SetSourcePlayable", HasExplicitThis = true, ThrowsException = true)]
		internal void SetSourcePlayable(PlayableHandle target, int port)
		{
			PlayableOutputHandle.SetSourcePlayable_Injected(ref this, ref target, port);
		}

		[FreeFunction("PlayableOutputHandleBindings::GetSourceOutputPort", HasExplicitThis = true, ThrowsException = true)]
		internal int GetSourceOutputPort()
		{
			return PlayableOutputHandle.GetSourceOutputPort_Injected(ref this);
		}

		[FreeFunction("PlayableOutputHandleBindings::GetWeight", HasExplicitThis = true, ThrowsException = true)]
		internal float GetWeight()
		{
			return PlayableOutputHandle.GetWeight_Injected(ref this);
		}

		[FreeFunction("PlayableOutputHandleBindings::SetWeight", HasExplicitThis = true, ThrowsException = true)]
		internal void SetWeight(float weight)
		{
			PlayableOutputHandle.SetWeight_Injected(ref this, weight);
		}

		[FreeFunction("PlayableOutputHandleBindings::PushNotification", HasExplicitThis = true, ThrowsException = true)]
		internal void PushNotification(PlayableHandle origin, INotification notification, object context)
		{
			PlayableOutputHandle.PushNotification_Injected(ref this, ref origin, notification, context);
		}

		[FreeFunction("PlayableOutputHandleBindings::GetNotificationReceivers", HasExplicitThis = true, ThrowsException = true)]
		internal INotificationReceiver[] GetNotificationReceivers()
		{
			return PlayableOutputHandle.GetNotificationReceivers_Injected(ref this);
		}

		[FreeFunction("PlayableOutputHandleBindings::AddNotificationReceiver", HasExplicitThis = true, ThrowsException = true)]
		internal void AddNotificationReceiver(INotificationReceiver receiver)
		{
			PlayableOutputHandle.AddNotificationReceiver_Injected(ref this, receiver);
		}

		[FreeFunction("PlayableOutputHandleBindings::RemoveNotificationReceiver", HasExplicitThis = true, ThrowsException = true)]
		internal void RemoveNotificationReceiver(INotificationReceiver receiver)
		{
			PlayableOutputHandle.RemoveNotificationReceiver_Injected(ref this, receiver);
		}

		[FreeFunction("PlayableOutputHandleBindings::GetEditorName", HasExplicitThis = true, ThrowsException = true)]
		internal string GetEditorName()
		{
			return PlayableOutputHandle.GetEditorName_Injected(ref this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsNull_Injected(ref PlayableOutputHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsValid_Injected(ref PlayableOutputHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Type GetPlayableOutputType_Injected(ref PlayableOutputHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern UnityEngine.Object GetReferenceObject_Injected(ref PlayableOutputHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetReferenceObject_Injected(ref PlayableOutputHandle _unity_self, UnityEngine.Object target);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern UnityEngine.Object GetUserData_Injected(ref PlayableOutputHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetUserData_Injected(ref PlayableOutputHandle _unity_self, [Writable] UnityEngine.Object target);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSourcePlayable_Injected(ref PlayableOutputHandle _unity_self, out PlayableHandle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSourcePlayable_Injected(ref PlayableOutputHandle _unity_self, ref PlayableHandle target, int port);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSourceOutputPort_Injected(ref PlayableOutputHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetWeight_Injected(ref PlayableOutputHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetWeight_Injected(ref PlayableOutputHandle _unity_self, float weight);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PushNotification_Injected(ref PlayableOutputHandle _unity_self, ref PlayableHandle origin, INotification notification, object context);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern INotificationReceiver[] GetNotificationReceivers_Injected(ref PlayableOutputHandle _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddNotificationReceiver_Injected(ref PlayableOutputHandle _unity_self, INotificationReceiver receiver);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveNotificationReceiver_Injected(ref PlayableOutputHandle _unity_self, INotificationReceiver receiver);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetEditorName_Injected(ref PlayableOutputHandle _unity_self);
	}
}
