using System;

namespace UnityEngine.TextCore.LowLevel
{
	internal enum OTFLookupTableType
	{
		Single_Adjustment = 16385,
		Pair_Adjustment,
		Cursive_Attachment,
		Mark_to_Base_Attachment,
		Mark_to_Ligature_Attachment,
		Mark_to_Mark_Attachment,
		Contextual_Positioning,
		Chaining_Contextual_Positioning,
		Extension_Positioning,
		Single_Substitution = 32769,
		Multiple_Substitution,
		Alternate_Substitution,
		Ligature_Substitution,
		Contextual_Substitution,
		Chaining_Contextual_Substitution,
		Extension_Substitution,
		Reverse_Chaining_Contextual_Single_Substitution
	}
}
