using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct NetworkMessageInfo
	{
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public double timestamp
		{
			get
			{
				throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public NetworkPlayer sender
		{
			get
			{
				throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public NetworkView networkView
		{
			get
			{
				throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
			}
		}
	}
}
