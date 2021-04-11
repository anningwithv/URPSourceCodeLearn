using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Export/PlayerConnection/PlayerConnectionInternal.bindings.h")]
	internal class PlayerConnectionInternal : IPlayerEditorConnectionNative
	{
		[Flags]
		public enum MulticastFlags
		{
			kRequestImmediateConnect = 1,
			kSupportsProfile = 2,
			kCustomMessage = 4,
			kUseAlternateIP = 8
		}

		void IPlayerEditorConnectionNative.SendMessage(Guid messageId, byte[] data, int playerId)
		{
			bool flag = messageId == Guid.Empty;
			if (flag)
			{
				throw new ArgumentException("messageId must not be empty");
			}
			PlayerConnectionInternal.SendMessage(messageId.ToString("N"), data, playerId);
		}

		bool IPlayerEditorConnectionNative.TrySendMessage(Guid messageId, byte[] data, int playerId)
		{
			bool flag = messageId == Guid.Empty;
			if (flag)
			{
				throw new ArgumentException("messageId must not be empty");
			}
			return PlayerConnectionInternal.TrySendMessage(messageId.ToString("N"), data, playerId);
		}

		void IPlayerEditorConnectionNative.Poll()
		{
			PlayerConnectionInternal.PollInternal();
		}

		void IPlayerEditorConnectionNative.RegisterInternal(Guid messageId)
		{
			PlayerConnectionInternal.RegisterInternal(messageId.ToString("N"));
		}

		void IPlayerEditorConnectionNative.UnregisterInternal(Guid messageId)
		{
			PlayerConnectionInternal.UnregisterInternal(messageId.ToString("N"));
		}

		void IPlayerEditorConnectionNative.Initialize()
		{
			PlayerConnectionInternal.Initialize();
		}

		bool IPlayerEditorConnectionNative.IsConnected()
		{
			return PlayerConnectionInternal.IsConnected();
		}

		void IPlayerEditorConnectionNative.DisconnectAll()
		{
			PlayerConnectionInternal.DisconnectAll();
		}

		[FreeFunction("PlayerConnection_Bindings::IsConnected")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsConnected();

		[FreeFunction("PlayerConnection_Bindings::Initialize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Initialize();

		[FreeFunction("PlayerConnection_Bindings::RegisterInternal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RegisterInternal(string messageId);

		[FreeFunction("PlayerConnection_Bindings::UnregisterInternal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnregisterInternal(string messageId);

		[FreeFunction("PlayerConnection_Bindings::SendMessage")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SendMessage(string messageId, byte[] data, int playerId);

		[FreeFunction("PlayerConnection_Bindings::TrySendMessage")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySendMessage(string messageId, byte[] data, int playerId);

		[FreeFunction("PlayerConnection_Bindings::PollInternal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PollInternal();

		[FreeFunction("PlayerConnection_Bindings::DisconnectAll")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisconnectAll();
	}
}
