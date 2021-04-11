using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine.Events;
using UnityEngine.Scripting;

namespace UnityEngine.Networking.PlayerConnection
{
	[Serializable]
	public class PlayerConnection : ScriptableObject, IEditorPlayerConnection
	{
		internal static IPlayerEditorConnectionNative connectionNative;

		[SerializeField]
		private PlayerEditorConnectionEvents m_PlayerEditorConnectionEvents = new PlayerEditorConnectionEvents();

		[SerializeField]
		private List<int> m_connectedPlayers = new List<int>();

		private bool m_IsInitilized;

		private static PlayerConnection s_Instance;

		public static PlayerConnection instance
		{
			get
			{
				bool flag = PlayerConnection.s_Instance == null;
				PlayerConnection result;
				if (flag)
				{
					result = PlayerConnection.CreateInstance();
				}
				else
				{
					result = PlayerConnection.s_Instance;
				}
				return result;
			}
		}

		public bool isConnected
		{
			get
			{
				return this.GetConnectionNativeApi().IsConnected();
			}
		}

		private static PlayerConnection CreateInstance()
		{
			PlayerConnection.s_Instance = ScriptableObject.CreateInstance<PlayerConnection>();
			PlayerConnection.s_Instance.hideFlags = HideFlags.HideAndDontSave;
			return PlayerConnection.s_Instance;
		}

		public void OnEnable()
		{
			bool isInitilized = this.m_IsInitilized;
			if (!isInitilized)
			{
				this.m_IsInitilized = true;
				this.GetConnectionNativeApi().Initialize();
			}
		}

		private IPlayerEditorConnectionNative GetConnectionNativeApi()
		{
			return PlayerConnection.connectionNative ?? new PlayerConnectionInternal();
		}

		public void Register(Guid messageId, UnityAction<MessageEventArgs> callback)
		{
			bool flag = messageId == Guid.Empty;
			if (flag)
			{
				throw new ArgumentException("Cant be Guid.Empty", "messageId");
			}
			bool flag2 = !this.m_PlayerEditorConnectionEvents.messageTypeSubscribers.Any((PlayerEditorConnectionEvents.MessageTypeSubscribers x) => x.MessageTypeId == messageId);
			if (flag2)
			{
				this.GetConnectionNativeApi().RegisterInternal(messageId);
			}
			this.m_PlayerEditorConnectionEvents.AddAndCreate(messageId).AddListener(callback);
		}

		public void Unregister(Guid messageId, UnityAction<MessageEventArgs> callback)
		{
			this.m_PlayerEditorConnectionEvents.UnregisterManagedCallback(messageId, callback);
			bool flag = !this.m_PlayerEditorConnectionEvents.messageTypeSubscribers.Any((PlayerEditorConnectionEvents.MessageTypeSubscribers x) => x.MessageTypeId == messageId);
			if (flag)
			{
				this.GetConnectionNativeApi().UnregisterInternal(messageId);
			}
		}

		public void RegisterConnection(UnityAction<int> callback)
		{
			foreach (int current in this.m_connectedPlayers)
			{
				callback(current);
			}
			this.m_PlayerEditorConnectionEvents.connectionEvent.AddListener(callback);
		}

		public void RegisterDisconnection(UnityAction<int> callback)
		{
			this.m_PlayerEditorConnectionEvents.disconnectionEvent.AddListener(callback);
		}

		public void UnregisterConnection(UnityAction<int> callback)
		{
			this.m_PlayerEditorConnectionEvents.connectionEvent.RemoveListener(callback);
		}

		public void UnregisterDisconnection(UnityAction<int> callback)
		{
			this.m_PlayerEditorConnectionEvents.disconnectionEvent.RemoveListener(callback);
		}

		public void Send(Guid messageId, byte[] data)
		{
			bool flag = messageId == Guid.Empty;
			if (flag)
			{
				throw new ArgumentException("Cant be Guid.Empty", "messageId");
			}
			this.GetConnectionNativeApi().SendMessage(messageId, data, 0);
		}

		public bool TrySend(Guid messageId, byte[] data)
		{
			bool flag = messageId == Guid.Empty;
			if (flag)
			{
				throw new ArgumentException("Cant be Guid.Empty", "messageId");
			}
			return this.GetConnectionNativeApi().TrySendMessage(messageId, data, 0);
		}

		public bool BlockUntilRecvMsg(Guid messageId, int timeout)
		{
			bool msgReceived = false;
			UnityAction<MessageEventArgs> callback = delegate(MessageEventArgs args)
			{
				msgReceived = true;
			};
			DateTime now = DateTime.Now;
			this.Register(messageId, callback);
			while ((DateTime.Now - now).TotalMilliseconds < (double)timeout && !msgReceived)
			{
				this.GetConnectionNativeApi().Poll();
			}
			this.Unregister(messageId, callback);
			return msgReceived;
		}

		public void DisconnectAll()
		{
			this.GetConnectionNativeApi().DisconnectAll();
		}

		[RequiredByNativeCode]
		private static void MessageCallbackInternal(IntPtr data, ulong size, ulong guid, string messageId)
		{
			byte[] array = null;
			bool flag = size > 0uL;
			if (flag)
			{
				array = new byte[size];
				Marshal.Copy(data, array, 0, (int)size);
			}
			PlayerConnection.instance.m_PlayerEditorConnectionEvents.InvokeMessageIdSubscribers(new Guid(messageId), array, (int)guid);
		}

		[RequiredByNativeCode]
		private static void ConnectedCallbackInternal(int playerId)
		{
			PlayerConnection.instance.m_connectedPlayers.Add(playerId);
			PlayerConnection.instance.m_PlayerEditorConnectionEvents.connectionEvent.Invoke(playerId);
		}

		[RequiredByNativeCode]
		private static void DisconnectedCallback(int playerId)
		{
			PlayerConnection.instance.m_connectedPlayers.Remove(playerId);
			PlayerConnection.instance.m_PlayerEditorConnectionEvents.disconnectionEvent.Invoke(playerId);
		}
	}
}
