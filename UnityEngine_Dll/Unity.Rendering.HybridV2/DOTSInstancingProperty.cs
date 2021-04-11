using System;

namespace Unity.Rendering.HybridV2
{
	public struct DOTSInstancingProperty
	{
		public int MetadataNameID;

		public int ConstantNameID;

		public int CbufferIndex;

		public int MetadataOffset;

		public int SizeBytes;

		public DOTSInstancingPropertyType ConstantType;

		public int Cols;

		public int Rows;
	}
}
