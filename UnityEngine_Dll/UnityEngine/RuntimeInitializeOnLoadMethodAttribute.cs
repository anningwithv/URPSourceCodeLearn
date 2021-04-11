using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false), RequiredByNativeCode]
	public class RuntimeInitializeOnLoadMethodAttribute : PreserveAttribute
	{
		private RuntimeInitializeLoadType m_LoadType;

		public RuntimeInitializeLoadType loadType
		{
			get
			{
				return this.m_LoadType;
			}
			private set
			{
				this.m_LoadType = value;
			}
		}

		public RuntimeInitializeOnLoadMethodAttribute()
		{
			this.loadType = RuntimeInitializeLoadType.AfterSceneLoad;
		}

		public RuntimeInitializeOnLoadMethodAttribute(RuntimeInitializeLoadType loadType)
		{
			this.loadType = loadType;
		}
	}
}
