using System;

namespace UnityEngine.TextCore
{
	internal class TextInfo
	{
		private static Vector2 s_InfinityVectorPositive = new Vector2(32767f, 32767f);

		private static Vector2 s_InfinityVectorNegative = new Vector2(-32767f, -32767f);

		public int characterCount;

		public int spriteCount;

		public int spaceCount;

		public int wordCount;

		public int linkCount;

		public int lineCount;

		public int pageCount;

		public int materialCount;

		public TextElementInfo[] textElementInfo;

		public WordInfo[] wordInfo;

		public LinkInfo[] linkInfo;

		public LineInfo[] lineInfo;

		public PageInfo[] pageInfo;

		public MeshInfo[] meshInfo;

		public bool isDirty;

		public TextInfo()
		{
			this.textElementInfo = new TextElementInfo[8];
			this.wordInfo = new WordInfo[16];
			this.linkInfo = new LinkInfo[0];
			this.lineInfo = new LineInfo[2];
			this.pageInfo = new PageInfo[4];
			this.meshInfo = new MeshInfo[1];
			this.materialCount = 0;
			this.isDirty = true;
		}

		internal void Clear()
		{
			this.characterCount = 0;
			this.spaceCount = 0;
			this.wordCount = 0;
			this.linkCount = 0;
			this.lineCount = 0;
			this.pageCount = 0;
			this.spriteCount = 0;
			for (int i = 0; i < this.meshInfo.Length; i++)
			{
				this.meshInfo[i].vertexCount = 0;
			}
		}

		internal void ClearMeshInfo(bool updateMesh)
		{
			for (int i = 0; i < this.meshInfo.Length; i++)
			{
				this.meshInfo[i].Clear(updateMesh);
			}
		}

		internal void ClearLineInfo()
		{
			bool flag = this.lineInfo == null;
			if (flag)
			{
				this.lineInfo = new LineInfo[2];
			}
			for (int i = 0; i < this.lineInfo.Length; i++)
			{
				this.lineInfo[i].characterCount = 0;
				this.lineInfo[i].spaceCount = 0;
				this.lineInfo[i].wordCount = 0;
				this.lineInfo[i].controlCharacterCount = 0;
				this.lineInfo[i].width = 0f;
				this.lineInfo[i].ascender = TextInfo.s_InfinityVectorNegative.x;
				this.lineInfo[i].descender = TextInfo.s_InfinityVectorPositive.x;
				this.lineInfo[i].lineExtents.min = TextInfo.s_InfinityVectorPositive;
				this.lineInfo[i].lineExtents.max = TextInfo.s_InfinityVectorNegative;
				this.lineInfo[i].maxAdvance = 0f;
			}
		}

		internal static void Resize<T>(ref T[] array, int size)
		{
			int newSize = (size > 1024) ? (size + 256) : Mathf.NextPowerOfTwo(size);
			Array.Resize<T>(ref array, newSize);
		}

		internal static void Resize<T>(ref T[] array, int size, bool isBlockAllocated)
		{
			if (isBlockAllocated)
			{
				size = ((size > 1024) ? (size + 256) : Mathf.NextPowerOfTwo(size));
			}
			bool flag = size == array.Length;
			if (!flag)
			{
				Array.Resize<T>(ref array, size);
			}
		}
	}
}
