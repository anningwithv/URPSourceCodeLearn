using System;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	[UsedByNativeCode]
	internal struct GlyphMarshallingStruct
	{
		public uint index;

		public GlyphMetrics metrics;

		public GlyphRect glyphRect;

		public float scale;

		public int atlasIndex;

		public GlyphMarshallingStruct(Glyph glyph)
		{
			this.index = glyph.index;
			this.metrics = glyph.metrics;
			this.glyphRect = glyph.glyphRect;
			this.scale = glyph.scale;
			this.atlasIndex = glyph.atlasIndex;
		}

		public GlyphMarshallingStruct(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex)
		{
			this.index = index;
			this.metrics = metrics;
			this.glyphRect = glyphRect;
			this.scale = scale;
			this.atlasIndex = atlasIndex;
		}
	}
}
