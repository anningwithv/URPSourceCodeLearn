using System;

namespace UnityEngine.UIElements
{
	public abstract class UxmlTypeRestriction : IEquatable<UxmlTypeRestriction>
	{
		public virtual bool Equals(UxmlTypeRestriction other)
		{
			return this == other;
		}
	}
}
