using System;

namespace UnityEngine
{
	internal class ApplicationShimBase : IDisposable
	{
		public virtual bool isEditor
		{
			get
			{
				return ApplicationEditor.isEditor;
			}
		}

		public virtual RuntimePlatform platform
		{
			get
			{
				return ApplicationEditor.platform;
			}
		}

		public virtual bool isMobilePlatform
		{
			get
			{
				return ApplicationEditor.isMobilePlatform;
			}
		}

		public virtual bool isConsolePlatform
		{
			get
			{
				return ApplicationEditor.isConsolePlatform;
			}
		}

		public virtual SystemLanguage systemLanguage
		{
			get
			{
				return ApplicationEditor.systemLanguage;
			}
		}

		public virtual NetworkReachability internetReachability
		{
			get
			{
				return ApplicationEditor.internetReachability;
			}
		}

		public void Dispose()
		{
			ShimManager.RemoveShim(this);
		}

		public bool IsActive()
		{
			return ShimManager.IsShimActive(this);
		}

		public void OnLowMemory()
		{
			Application.CallLowMemory();
		}
	}
}
