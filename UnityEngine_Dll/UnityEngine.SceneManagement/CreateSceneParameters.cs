using System;

namespace UnityEngine.SceneManagement
{
	[Serializable]
	public struct CreateSceneParameters
	{
		[SerializeField]
		private LocalPhysicsMode m_LocalPhysicsMode;

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

		public CreateSceneParameters(LocalPhysicsMode physicsMode)
		{
			this.m_LocalPhysicsMode = physicsMode;
		}
	}
}
