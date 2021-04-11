using System;

namespace UnityEngine.SceneManagement
{
	[Serializable]
	public struct LoadSceneParameters
	{
		[SerializeField]
		private LoadSceneMode m_LoadSceneMode;

		[SerializeField]
		private LocalPhysicsMode m_LocalPhysicsMode;

		public LoadSceneMode loadSceneMode
		{
			get
			{
				return this.m_LoadSceneMode;
			}
			set
			{
				this.m_LoadSceneMode = value;
			}
		}

		public LocalPhysicsMode localPhysicsMode
		{
			get
			{
				return this.m_LocalPhysicsMode;
			}
			set
			{
				this.m_LocalPhysicsMode = value;
			}
		}

		public LoadSceneParameters(LoadSceneMode mode)
		{
			this.m_LoadSceneMode = mode;
			this.m_LocalPhysicsMode = LocalPhysicsMode.None;
		}

		public LoadSceneParameters(LoadSceneMode mode, LocalPhysicsMode physicsMode)
		{
			this.m_LoadSceneMode = mode;
			this.m_LocalPhysicsMode = physicsMode;
		}
	}
}
