using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	[UsedByNativeCode]
	[Serializable]
	public struct GlyphAdjustmentRecord
	{
		[NativeName("glyphIndex"), SerializeField]
		private uint m_GlyphIndex;

		[NativeName("glyphValueRecord"), SerializeField]
		private GlyphValueRecord m_GlyphValueRecord;

		public uint glyphIndex
		{
			get
			{
				return this.m_GlyphIndex;
			}
			set
			{
				this.m_GlyphIndex = value;
			}
		}

		public GlyphValueRecord glyphValueRecord
		{
			get
			{
				return this.m_GlyphValueRecord;
			}
			set
			{
				this.m_GlyphValueRecord = value;
			}
		}

		public GlyphAdjustmentRecord(uint glyphIndex, GlyphValueRecord glyphValueRecord)
		{
			this.m_GlyphIndex = glyphIndex;
			this.m_GlyphValueRecord = glyphValueRecord;
		}
	}
}
