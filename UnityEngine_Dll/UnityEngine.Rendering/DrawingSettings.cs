using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.Rendering
{
	public struct DrawingSettings : IEquatable<DrawingSettings>
	{
		[CompilerGenerated, UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 64)]
		public struct <shaderPassNames>e__FixedBuffer
		{
			public int FixedElementField;
		}

		private const int kMaxShaderPasses = 16;

		public static readonly int maxShaderPasses = 16;

		private SortingSettings m_SortingSettings;

		[FixedBuffer(typeof(int), 16)]
		internal DrawingSettings.<shaderPassNames>e__FixedBuffer shaderPassNames;

		private PerObjectData m_PerObjectData;

		private DrawRendererFlags m_Flags;

		private int m_OverrideMaterialInstanceId;

		private int m_OverrideMaterialPassIndex;

		private int m_MainLightIndex;

		private int m_UseSrpBatcher;

		public SortingSettings sortingSettings
		{
			get
			{
				return this.m_SortingSettings;
			}
			set
			{
				this.m_SortingSettings = value;
			}
		}

		public PerObjectData perObjectData
		{
			get
			{
				return this.m_PerObjectData;
			}
			set
			{
				this.m_PerObjectData = value;
			}
		}

		public bool enableDynamicBatching
		{
			get
			{
				return (this.m_Flags & DrawRendererFlags.EnableDynamicBatching) > DrawRendererFlags.None;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= DrawRendererFlags.EnableDynamicBatching;
				}
				else
				{
					this.m_Flags &= ~DrawRendererFlags.EnableDynamicBatching;
				}
			}
		}

		public bool enableInstancing
		{
			get
			{
				return (this.m_Flags & DrawRendererFlags.EnableInstancing) > DrawRendererFlags.None;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= DrawRendererFlags.EnableInstancing;
				}
				else
				{
					this.m_Flags &= ~DrawRendererFlags.EnableInstancing;
				}
			}
		}

		public Material overrideMaterial
		{
			get
			{
				return (this.m_OverrideMaterialInstanceId != 0) ? (UnityEngine.Object.FindObjectFromInstanceID(this.m_OverrideMaterialInstanceId) as Material) : null;
			}
			set
			{
				this.m_OverrideMaterialInstanceId = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		public int overrideMaterialPassIndex
		{
			get
			{
				return this.m_OverrideMaterialPassIndex;
			}
			set
			{
				this.m_OverrideMaterialPassIndex = value;
			}
		}

		public int mainLightIndex
		{
			get
			{
				return this.m_MainLightIndex;
			}
			set
			{
				this.m_MainLightIndex = value;
			}
		}

		public unsafe DrawingSettings(ShaderTagId shaderPassName, SortingSettings sortingSettings)
		{
			this.m_SortingSettings = sortingSettings;
			this.m_PerObjectData = PerObjectData.None;
			this.m_Flags = DrawRendererFlags.EnableInstancing;
			this.m_OverrideMaterialInstanceId = 0;
			this.m_OverrideMaterialPassIndex = 0;
			this.m_MainLightIndex = -1;
			fixed (int* ptr = &this.shaderPassNames.FixedElementField)
			{
				int* ptr2 = ptr;
				*ptr2 = shaderPassName.id;
				for (int i = 1; i < DrawingSettings.maxShaderPasses; i++)
				{
					ptr2[i] = -1;
				}
			}
			this.m_PerObjectData = PerObjectData.None;
			this.m_Flags = DrawRendererFlags.EnableInstancing;
			this.m_UseSrpBatcher = 0;
		}

		public unsafe ShaderTagId GetShaderPassName(int index)
		{
			bool flag = index >= DrawingSettings.maxShaderPasses || index < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Index should range from 0 to DrawSettings.maxShaderPasses ({0}), was {1}", DrawingSettings.maxShaderPasses, index));
			}
			int* ptr = &this.shaderPassNames.FixedElementField;
			return new ShaderTagId
			{
				id = ptr[index]
			};
		}

		public unsafe void SetShaderPassName(int index, ShaderTagId shaderPassName)
		{
			bool flag = index >= DrawingSettings.maxShaderPasses || index < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Index should range from 0 to DrawSettings.maxShaderPasses ({0}), was {1}", DrawingSettings.maxShaderPasses, index));
			}
			fixed (int* ptr = &this.shaderPassNames.FixedElementField)
			{
				int* ptr2 = ptr;
				ptr2[index] = shaderPassName.id;
			}
		}

		public bool Equals(DrawingSettings other)
		{
			bool result;
			for (int i = 0; i < DrawingSettings.maxShaderPasses; i++)
			{
				bool flag = !this.GetShaderPassName(i).Equals(other.GetShaderPassName(i));
				if (flag)
				{
					result = false;
					return result;
				}
			}
			result = (this.m_SortingSettings.Equals(other.m_SortingSettings) && this.m_PerObjectData == other.m_PerObjectData && this.m_Flags == other.m_Flags && this.m_OverrideMaterialInstanceId == other.m_OverrideMaterialInstanceId && this.m_OverrideMaterialPassIndex == other.m_OverrideMaterialPassIndex && this.m_UseSrpBatcher == other.m_UseSrpBatcher);
			return result;
		}

		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is DrawingSettings && this.Equals((DrawingSettings)obj);
		}

		public override int GetHashCode()
		{
			int num = this.m_SortingSettings.GetHashCode();
			num = (num * 397 ^ (int)this.m_PerObjectData);
			num = (num * 397 ^ (int)this.m_Flags);
			num = (num * 397 ^ this.m_OverrideMaterialInstanceId);
			num = (num * 397 ^ this.m_OverrideMaterialPassIndex);
			return num * 397 ^ this.m_UseSrpBatcher;
		}

		public static bool operator ==(DrawingSettings left, DrawingSettings right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(DrawingSettings left, DrawingSettings right)
		{
			return !left.Equals(right);
		}
	}
}
