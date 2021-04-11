using System;

namespace UnityEngine.Networking.Types
{
	public class NetworkAccessToken
	{
		private const int NETWORK_ACCESS_TOKEN_SIZE = 64;

		public byte[] array;

		public NetworkAccessToken()
		{
			this.array = new byte[64];
		}

		public NetworkAccessToken(byte[] array)
		{
			this.array = array;
		}

		public NetworkAccessToken(string strArray)
		{
			try
			{
				this.array = Convert.FromBase64String(strArray);
			}
			catch (Exception)
			{
				this.array = new byte[64];
			}
		}

		public string GetByteString()
		{
			return Convert.ToBase64String(this.array);
		}

		public bool IsValid()
		{
			bool flag = this.array == null || this.array.Length != 64;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = false;
				byte[] array = this.array;
				for (int i = 0; i < array.Length; i++)
				{
					byte b = array[i];
					bool flag3 = b > 0;
					if (flag3)
					{
						flag2 = true;
						break;
					}
				}
				result = flag2;
			}
			return result;
		}
	}
}
