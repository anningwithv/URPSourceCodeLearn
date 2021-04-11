using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	internal class StartDragArgs
	{
		private readonly Hashtable m_GenericData = new Hashtable();

		public string title
		{
			[CompilerGenerated]
			get
			{
				return this.<title>k__BackingField;
			}
		}

		public object userData
		{
			[CompilerGenerated]
			get
			{
				return this.<userData>k__BackingField;
			}
		}

		internal Hashtable genericData
		{
			get
			{
				return this.m_GenericData;
			}
		}

		internal IEnumerable<UnityEngine.Object> unityObjectReferences
		{
			get;
			private set;
		}

		internal StartDragArgs()
		{
			this.<unityObjectReferences>k__BackingField = null;
			base..ctor();
			this.<title>k__BackingField = string.Empty;
		}

		public StartDragArgs(string title, object userData)
		{
			this.<unityObjectReferences>k__BackingField = null;
			base..ctor();
			this.<title>k__BackingField = title;
			this.<userData>k__BackingField = userData;
		}

		public void SetGenericData(string key, object data)
		{
			this.m_GenericData[key] = data;
		}

		public void SetUnityObjectReferences(IEnumerable<UnityEngine.Object> references)
		{
			this.unityObjectReferences = references;
		}
	}
}
