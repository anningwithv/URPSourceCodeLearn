using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[UsedByNativeCode]
	[Serializable]
	public struct BuildCompression
	{
		public static readonly BuildCompression Uncompressed = new BuildCompression(CompressionType.None, CompressionLevel.Maximum, 131072u);

		public static readonly BuildCompression LZ4 = new BuildCompression(CompressionType.Lz4HC, CompressionLevel.Maximum, 131072u);

		public static readonly BuildCompression LZMA = new BuildCompression(CompressionType.Lzma, CompressionLevel.Maximum, 131072u);

		public static readonly BuildCompression UncompressedRuntime = BuildCompression.Uncompressed;

		public static readonly BuildCompression LZ4Runtime = new BuildCompression(CompressionType.Lz4, CompressionLevel.Maximum, 131072u);

		[NativeName("compression")]
		private CompressionType _compression;

		[NativeName("level")]
		private CompressionLevel _level;

		[NativeName("blockSize")]
		private uint _blockSize;

		public CompressionType compression
		{
			get
			{
				return this._compression;
			}
			private set
			{
				this._compression = value;
			}
		}

		public CompressionLevel level
		{
			get
			{
				return this._level;
			}
			private set
			{
				this._level = value;
			}
		}

		public uint blockSize
		{
			get
			{
				return this._blockSize;
			}
			private set
			{
				this._blockSize = value;
			}
		}

		private BuildCompression(CompressionType in_compression, CompressionLevel in_level, uint in_blockSize)
		{
			this = default(BuildCompression);
			this.compression = in_compression;
			this.level = in_level;
			this.blockSize = in_blockSize;
		}
	}
}
