using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	public class DropdownMenuSeparator : DropdownMenuItem
	{
		public string subMenuPath
		{
			[CompilerGenerated]
			get
			{
				return this.<subMenuPath>k__BackingField;
			}
		}

		public DropdownMenuSeparator(string subMenuPath)
		{
			this.<subMenuPath>k__BackingField = subMenuPath;
		}
	}
}
