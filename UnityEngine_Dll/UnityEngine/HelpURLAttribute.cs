using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false), UsedByNativeCode]
	public sealed class HelpURLAttribute : Attribute
	{
		internal readonly string m_Url;

		internal readonly bool m_Dispatcher;

		internal readonly string m_DispatchingFieldName;

		public string URL
		{
			get
			{
				return this.m_Url;
			}
		}

		public HelpURLAttribute(string url)
		{
			this.m_Url = url;
			this.m_DispatchingFieldName = "";
			this.m_Dispatcher = false;
		}

		internal HelpURLAttribute(string defaultURL, string dispatchingFieldName)
		{
			this.m_Url = defaultURL;
			this.m_DispatchingFieldName = dispatchingFieldName;
			this.m_Dispatcher = !string.IsNullOrEmpty(dispatchingFieldName);
		}
	}
}
