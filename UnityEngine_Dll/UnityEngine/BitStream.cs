using System;
using System.ComponentModel;

namespace UnityEngine
{
	[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
	public class BitStream
	{
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public bool isReading
		{
			get
			{
				throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public bool isWriting
		{
			get
			{
				throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public void Serialize(ref bool value)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public void Serialize(ref char value)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public void Serialize(ref short value)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public void Serialize(ref int value)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public void Serialize(ref float value)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public void Serialize(ref float value, float maxDelta)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public void Serialize(ref Quaternion value)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public void Serialize(ref Quaternion value, float maxDelta)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public void Serialize(ref Vector3 value)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public void Serialize(ref Vector3 value, float maxDelta)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public void Serialize(ref NetworkPlayer value)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public void Serialize(ref NetworkViewID viewID)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}
	}
}
