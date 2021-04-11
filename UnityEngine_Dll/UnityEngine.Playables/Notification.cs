using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Playables
{
	public class Notification : INotification
	{
		public PropertyName id
		{
			[CompilerGenerated]
			get
			{
				return this.<id>k__BackingField;
			}
		}

		public Notification(string name)
		{
			this.<id>k__BackingField = new PropertyName(name);
		}
	}
}
