using System;

namespace UnityEngine.Networking.Match
{
	[Serializable]
	internal abstract class Response : IResponse
	{
		public bool success;

		public string extendedInfo;

		public void SetSuccess()
		{
			this.success = true;
			this.extendedInfo = "";
		}

		public void SetFailure(string info)
		{
			this.success = false;
			this.extendedInfo += info;
		}

		public override string ToString()
		{
			return UnityString.Format("[{0}]-success:{1}-extendedInfo:{2}", new object[]
			{
				base.ToString(),
				this.success,
				this.extendedInfo
			});
		}
	}
}
