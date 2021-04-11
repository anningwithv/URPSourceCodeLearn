using System;
using System.Diagnostics;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	[DebuggerDisplay("First glyphIndex = {m_FirstAdjustmentRecord.m_GlyphIndex},  Second glyphIndex = {m_SecondAdjustmentRecord.m_GlyphIndex}"), UsedByNativeCode]
	[Serializable]
	public struct GlyphPairAdjustmentRecord
	{
		[NativeName("firstAdjustmentRecord"), SerializeField]
		private GlyphAdjustmentRecord m_FirstAdjustmentRecord;

		[NativeName("secondAdjustmentRecord"), SerializeField]
		private GlyphAdjustmentRecord m_SecondAdjustmentRecord;

		[SerializeField]
		private FontFeatureLookupFlags m_FeatureLookupFlags;

		public GlyphAdjustmentRecord firstAdjustmentRecord
		{
			get
			{
				return this.m_FirstAdjustmentRecord;
			}
			set
			{
				this.m_FirstAdjustmentRecord = value;
			}
		}

		public GlyphAdjustmentRecord secondAdjustmentRecord
		{
			get
			{
				return this.m_SecondAdjustmentRecord;
			}
			set
			{
				this.m_SecondAdjustmentRecord = value;
			}
		}

		public FontFeatureLookupFlags featureLookupFlags
		{
			get
			{
				return this.m_FeatureLookupFlags;
			}
			set
			{
				this.m_FeatureLookupFlags = value;
			}
		}

		public GlyphPairAdjustmentRecord(GlyphAdjustmentRecord firstAdjustmentRecord, GlyphAdjustmentRecord secondAdjustmentRecord)
		{
			this.m_FirstAdjustmentRecord = firstAdjustmentRecord;
			this.m_SecondAdjustmentRecord = secondAdjustmentRecord;
			this.m_FeatureLookupFlags = FontFeatureLookupFlags.None;
		}
	}
}
