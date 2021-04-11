using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.VFX
{
	public struct VFXOutputEventArgs
	{
		public int nameId
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<nameId>k__BackingField;
			}
		}

		public VFXEventAttribute eventAttribute
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<eventAttribute>k__BackingField;
			}
		}

		public VFXOutputEventArgs(int nameId, VFXEventAttribute eventAttribute)
		{
			this.<nameId>k__BackingField = nameId;
			this.<eventAttribute>k__BackingField = eventAttribute;
		}
	}
}
