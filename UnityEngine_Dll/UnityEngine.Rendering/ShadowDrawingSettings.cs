using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	[UsedByNativeCode]
	public struct ShadowDrawingSettings : IEquatable<ShadowDrawingSettings>
	{
		private CullingResults m_CullingResults;

		private int m_LightIndex;

		private int m_UseRenderingLayerMaskTest;

		private ShadowSplitData m_SplitData;

		public CullingResults cullingResults
		{
			get
			{
				return this.m_CullingResults;
			}
			set
			{
				this.m_CullingResults.Validate();
				this.m_CullingResults = value;
			}
		}

		public int lightIndex
		{
			get
			{
				return this.m_LightIndex;
			}
			set
			{
				this.m_LightIndex = value;
			}
		}

		public bool useRenderingLayerMaskTest
		{
			get
			{
				return this.m_UseRenderingLayerMaskTest != 0;
			}
			set
			{
				this.m_UseRenderingLayerMaskTest = (value ? 1 : 0);
			}
		}

		public ShadowSplitData splitData
		{
			get
			{
				return this.m_SplitData;
			}
			set
			{
				this.m_SplitData = value;
			}
		}

		public ShadowDrawingSettings(CullingResults cullingResults, int lightIndex)
		{
			this.m_CullingResults = cullingResults;
			this.m_LightIndex = lightIndex;
			this.m_UseRenderingLayerMaskTest = 0;
			this.m_SplitData = default(ShadowSplitData);
			this.m_SplitData.shadowCascadeBlendCullingFactor = 1f;
		}

		public bool Equals(ShadowDrawingSettings other)
		{
			return this.m_CullingResults.Equals(other.m_CullingResults) && this.m_LightIndex == other.m_LightIndex && this.m_SplitData.Equals(other.m_SplitData) && this.m_UseRenderingLayerMaskTest.Equals(other.m_UseRenderingLayerMaskTest);
		}

		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is ShadowDrawingSettings && this.Equals((ShadowDrawingSettings)obj);
		}

		public override int GetHashCode()
		{
			int num = this.m_CullingResults.GetHashCode();
			num = (num * 397 ^ this.m_LightIndex);
			num = (num * 397 ^ this.m_UseRenderingLayerMaskTest);
			return num * 397 ^ this.m_SplitData.GetHashCode();
		}

		public static bool operator ==(ShadowDrawingSettings left, ShadowDrawingSettings right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(ShadowDrawingSettings left, ShadowDrawingSettings right)
		{
			return !left.Equals(right);
		}
	}
}
