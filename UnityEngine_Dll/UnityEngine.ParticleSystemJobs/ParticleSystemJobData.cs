using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	public struct ParticleSystemJobData
	{
		internal AtomicSafetyHandle m_Safety;

		public int count
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<count>k__BackingField;
			}
		}

		public ParticleSystemNativeArray3 positions
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<positions>k__BackingField;
			}
		}

		public ParticleSystemNativeArray3 velocities
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<velocities>k__BackingField;
			}
		}

		public ParticleSystemNativeArray3 rotations
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<rotations>k__BackingField;
			}
		}

		public ParticleSystemNativeArray3 rotationalSpeeds
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<rotationalSpeeds>k__BackingField;
			}
		}

		public ParticleSystemNativeArray3 sizes
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<sizes>k__BackingField;
			}
		}

		public NativeArray<Color32> startColors
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<startColors>k__BackingField;
			}
		}

		public NativeArray<float> aliveTimePercent
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<aliveTimePercent>k__BackingField;
			}
		}

		public NativeArray<float> inverseStartLifetimes
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<inverseStartLifetimes>k__BackingField;
			}
		}

		public NativeArray<uint> randomSeeds
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<randomSeeds>k__BackingField;
			}
		}

		public ParticleSystemNativeArray4 customData1
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<customData1>k__BackingField;
			}
		}

		public ParticleSystemNativeArray4 customData2
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<customData2>k__BackingField;
			}
		}

		internal ParticleSystemJobData(ref NativeParticleData nativeData)
		{
			this = default(ParticleSystemJobData);
			this.m_Safety = AtomicSafetyHandle.Create();
			this.<count>k__BackingField = nativeData.count;
			this.<positions>k__BackingField = this.CreateNativeArray3(ref nativeData.positions, this.count);
			this.<velocities>k__BackingField = this.CreateNativeArray3(ref nativeData.velocities, this.count);
			this.<rotations>k__BackingField = this.CreateNativeArray3(ref nativeData.rotations, this.count);
			this.<rotationalSpeeds>k__BackingField = this.CreateNativeArray3(ref nativeData.rotationalSpeeds, this.count);
			this.<sizes>k__BackingField = this.CreateNativeArray3(ref nativeData.sizes, this.count);
			this.<startColors>k__BackingField = this.CreateNativeArray<Color32>(nativeData.startColors, this.count);
			this.<aliveTimePercent>k__BackingField = this.CreateNativeArray<float>(nativeData.aliveTimePercent, this.count);
			this.<inverseStartLifetimes>k__BackingField = this.CreateNativeArray<float>(nativeData.inverseStartLifetimes, this.count);
			this.<randomSeeds>k__BackingField = this.CreateNativeArray<uint>(nativeData.randomSeeds, this.count);
			this.<customData1>k__BackingField = this.CreateNativeArray4(ref nativeData.customData1, this.count);
			this.<customData2>k__BackingField = this.CreateNativeArray4(ref nativeData.customData2, this.count);
		}

		internal unsafe NativeArray<T> CreateNativeArray<T>(void* src, int count) where T : struct
		{
			NativeArray<T> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(src, count, Allocator.Invalid);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<T>(ref result, this.m_Safety);
			return result;
		}

		internal unsafe ParticleSystemNativeArray3 CreateNativeArray3(ref NativeParticleData.Array3 ptrs, int count)
		{
			return new ParticleSystemNativeArray3
			{
				x = this.CreateNativeArray<float>((void*)ptrs.x, count),
				y = this.CreateNativeArray<float>((void*)ptrs.y, count),
				z = this.CreateNativeArray<float>((void*)ptrs.z, count)
			};
		}

		internal unsafe ParticleSystemNativeArray4 CreateNativeArray4(ref NativeParticleData.Array4 ptrs, int count)
		{
			return new ParticleSystemNativeArray4
			{
				x = this.CreateNativeArray<float>((void*)ptrs.x, count),
				y = this.CreateNativeArray<float>((void*)ptrs.y, count),
				z = this.CreateNativeArray<float>((void*)ptrs.z, count),
				w = this.CreateNativeArray<float>((void*)ptrs.w, count)
			};
		}
	}
}
