using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	[Serializable]
	internal class StyleSelector
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly StyleSelector.<>c <>9 = new StyleSelector.<>c();

			public static Func<StyleSelectorPart, string> <>9__10_0;

			internal string <ToString>b__10_0(StyleSelectorPart p)
			{
				return p.ToString();
			}
		}

		[SerializeField]
		private StyleSelectorPart[] m_Parts;

		[SerializeField]
		private StyleSelectorRelationship m_PreviousRelationship;

		internal int pseudoStateMask = -1;

		internal int negatedPseudoStateMask = -1;

		public StyleSelectorPart[] parts
		{
			get
			{
				return this.m_Parts;
			}
			internal set
			{
				this.m_Parts = value;
			}
		}

		public StyleSelectorRelationship previousRelationship
		{
			get
			{
				return this.m_PreviousRelationship;
			}
			internal set
			{
				this.m_PreviousRelationship = value;
			}
		}

		public override string ToString()
		{
			string arg_35_0 = ", ";
			IEnumerable<StyleSelectorPart> arg_2B_0 = this.parts;
			Func<StyleSelectorPart, string> arg_2B_1;
			if ((arg_2B_1 = StyleSelector.<>c.<>9__10_0) == null)
			{
				arg_2B_1 = (StyleSelector.<>c.<>9__10_0 = new Func<StyleSelectorPart, string>(StyleSelector.<>c.<>9.<ToString>b__10_0));
			}
			return string.Join(arg_35_0, arg_2B_0.Select(arg_2B_1).ToArray<string>());
		}
	}
}
