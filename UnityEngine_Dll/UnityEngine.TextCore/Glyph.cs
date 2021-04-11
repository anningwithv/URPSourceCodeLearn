using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.TextCore.LowLevel;

namespace UnityEngine.TextCore
{
	[UsedByNativeCode]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class Glyph
	{
		[NativeName("index"), SerializeField]
		private uint m_Index;

		[NativeName("metrics"), SerializeField]
		private GlyphMetrics m_Metrics;

		[NativeName("glyphRect"), SerializeField]
		private GlyphRect m_GlyphRect;

		[NativeName("scale"), SerializeField]
		private float m_Scale;

		[NativeName("atlasIndex"), SerializeField]
		private int m_AtlasIndex;

		public uint index
		{
			get
			{
				return this.m_Index;
			}
			set
			{
				this.m_Index = value;
			}
		}

		public GlyphMetrics metrics
		{
			get
			{
				return this.m_Metrics;
			}
			set
			{
				this.m_Metrics = value;
			}
		}

		public GlyphRect glyphRect
		{
			get
			{
				return this.m_GlyphRect;
			}
			set
			{
				this.m_GlyphRect = value;
			}
		}

		public float scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				this.m_Scale = value;
			}
		}

		public int atlasIndex
		{
			get
			{
				return this.m_AtlasIndex;
			}
			set
			{
				this.m_AtlasIndex = value;
			}
		}

		public Glyph()
		{
			this.m_Index = 0u;
			this.m_Metrics = default(GlyphMetrics);
			this.m_GlyphRect = default(GlyphRect);
			this.m_Scale = 1f;
			this.m_AtlasIndex = 0;
		}

		public Glyph(Glyph glyph)
		{
			this.m_Index = glyph.index;
			this.m_Metrics = glyph.metrics;
			this.m_GlyphRect = glyph.glyphRect;
			this.m_Scale = glyph.scale;
			this.m_AtlasIndex = glyph.atlasIndex;
		}

		internal Glyph(GlyphMarshallingStruct glyphStruct)
		{
			this.m_Index = glyphStruct.index;
			this.m_Metrics = glyphStruct.metrics;
			this.m_GlyphRect = glyphStruct.glyphRect;
			this.m_Scale = glyphStruct.scale;
			this.m_AtlasIndex = glyphStruct.atlasIndex;
		}

		public Glyph(uint index, GlyphMetrics metrics, GlyphRect glyphRect)
		{
			this.m_Index = index;
			this.m_Metrics = metrics;
			this.m_GlyphRect = glyphRect;
			this.m_Scale = 1f;
			this.m_AtlasIndex = 0;
		}

		public Glyph(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex)
		{
			this.m_Index = index;
			this.m_Metrics = metrics;
			this.m_GlyphRect = glyphRect;
			this.m_Scale = scale;
			this.m_AtlasIndex = atlasIndex;
		}

		public bool Compare(Glyph other)
		{
			return this.index == other.index && this.metrics == other.metrics && this.glyphRect == other.glyphRect && this.scale == other.scale && this.atlasIndex == other.atlasIndex;
		}
	}
}
