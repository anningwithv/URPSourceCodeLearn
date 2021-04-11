using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.UIElements.UIR
{
	internal class Page : IDisposable
	{
		public class DataSet<T> : IDisposable where T : struct
		{
			public Utility.GPUBuffer<T> gpuData;

			public NativeArray<T> cpuData;

			public NativeArray<GfxUpdateBufferRange> updateRanges;

			public GPUBufferAllocator allocator;

			private readonly uint m_UpdateRangePoolSize;

			private uint m_ElemStride;

			private uint m_UpdateRangeMin;

			private uint m_UpdateRangeMax;

			private uint m_UpdateRangesEnqueued;

			private uint m_UpdateRangesBatchStart;

			private bool m_UpdateRangesSaturated;

			protected bool disposed
			{
				get;
				private set;
			}

			public DataSet(Utility.GPUBufferType bufferType, uint totalCount, uint maxQueuedFrameCount, uint updateRangePoolSize, bool mockBuffer)
			{
				bool flag = !mockBuffer;
				if (flag)
				{
					this.gpuData = new Utility.GPUBuffer<T>((int)totalCount, bufferType);
				}
				this.cpuData = new NativeArray<T>((int)totalCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				this.allocator = new GPUBufferAllocator(totalCount);
				bool flag2 = !mockBuffer;
				if (flag2)
				{
					this.m_ElemStride = (uint)this.gpuData.ElementStride;
				}
				this.m_UpdateRangePoolSize = updateRangePoolSize;
				uint length = this.m_UpdateRangePoolSize * maxQueuedFrameCount;
				this.updateRanges = new NativeArray<GfxUpdateBufferRange>((int)length, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				this.m_UpdateRangeMin = 4294967295u;
				this.m_UpdateRangeMax = 0u;
				this.m_UpdateRangesEnqueued = 0u;
				this.m_UpdateRangesBatchStart = 0u;
			}

			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			public void Dispose(bool disposing)
			{
				bool disposed = this.disposed;
				if (!disposed)
				{
					if (disposing)
					{
						Utility.GPUBuffer<T> expr_19 = this.gpuData;
						if (expr_19 != null)
						{
							expr_19.Dispose();
						}
						this.cpuData.Dispose();
						this.updateRanges.Dispose();
					}
					this.disposed = true;
				}
			}

			public void RegisterUpdate(uint start, uint size)
			{
				Debug.Assert((ulong)(start + size) <= (ulong)((long)this.cpuData.Length));
				int num = (int)(this.m_UpdateRangesBatchStart + this.m_UpdateRangesEnqueued);
				bool flag = this.m_UpdateRangesEnqueued > 0u;
				if (flag)
				{
					int index = num - 1;
					GfxUpdateBufferRange gfxUpdateBufferRange = this.updateRanges[index];
					uint num2 = start * this.m_ElemStride;
					bool flag2 = gfxUpdateBufferRange.offsetFromWriteStart + gfxUpdateBufferRange.size == num2;
					if (flag2)
					{
						this.updateRanges[index] = new GfxUpdateBufferRange
						{
							source = gfxUpdateBufferRange.source,
							offsetFromWriteStart = gfxUpdateBufferRange.offsetFromWriteStart,
							size = gfxUpdateBufferRange.size + size * this.m_ElemStride
						};
						this.m_UpdateRangeMax = Math.Max(this.m_UpdateRangeMax, start + size);
						return;
					}
				}
				this.m_UpdateRangeMin = Math.Min(this.m_UpdateRangeMin, start);
				this.m_UpdateRangeMax = Math.Max(this.m_UpdateRangeMax, start + size);
				bool flag3 = this.m_UpdateRangesEnqueued == this.m_UpdateRangePoolSize;
				if (flag3)
				{
					this.m_UpdateRangesSaturated = true;
				}
				else
				{
					UIntPtr source = new UIntPtr(this.cpuData.Slice((int)start, (int)size).GetUnsafeReadOnlyPtr<T>());
					this.updateRanges[num] = new GfxUpdateBufferRange
					{
						source = source,
						offsetFromWriteStart = start * this.m_ElemStride,
						size = size * this.m_ElemStride
					};
					this.m_UpdateRangesEnqueued += 1u;
				}
			}

			public void SendUpdates()
			{
				this.SendPartialRanges();
			}

			public void SendFullRange()
			{
				uint num = (uint)((long)this.cpuData.Length * (long)((ulong)this.m_ElemStride));
				this.updateRanges[(int)this.m_UpdateRangesBatchStart] = new GfxUpdateBufferRange
				{
					source = new UIntPtr(this.cpuData.GetUnsafeReadOnlyPtr<T>()),
					offsetFromWriteStart = 0u,
					size = num
				};
				Utility.GPUBuffer<T> expr_5F = this.gpuData;
				if (expr_5F != null)
				{
					expr_5F.UpdateRanges(this.updateRanges.Slice((int)this.m_UpdateRangesBatchStart, 1), 0, (int)num);
				}
				this.ResetUpdateState();
			}

			public void SendPartialRanges()
			{
				bool flag = this.m_UpdateRangesEnqueued == 0u;
				if (!flag)
				{
					bool updateRangesSaturated = this.m_UpdateRangesSaturated;
					if (updateRangesSaturated)
					{
						uint num = this.m_UpdateRangeMax - this.m_UpdateRangeMin;
						this.m_UpdateRangesEnqueued = 1u;
						this.updateRanges[(int)this.m_UpdateRangesBatchStart] = new GfxUpdateBufferRange
						{
							source = new UIntPtr(this.cpuData.Slice((int)this.m_UpdateRangeMin, (int)num).GetUnsafeReadOnlyPtr<T>()),
							offsetFromWriteStart = this.m_UpdateRangeMin * this.m_ElemStride,
							size = num * this.m_ElemStride
						};
					}
					uint num2 = this.m_UpdateRangeMin * this.m_ElemStride;
					uint rangesMax = this.m_UpdateRangeMax * this.m_ElemStride;
					bool flag2 = num2 > 0u;
					if (flag2)
					{
						for (uint num3 = 0u; num3 < this.m_UpdateRangesEnqueued; num3 += 1u)
						{
							int index = (int)(num3 + this.m_UpdateRangesBatchStart);
							this.updateRanges[index] = new GfxUpdateBufferRange
							{
								source = this.updateRanges[index].source,
								offsetFromWriteStart = this.updateRanges[index].offsetFromWriteStart - num2,
								size = this.updateRanges[index].size
							};
						}
					}
					Utility.GPUBuffer<T> expr_15A = this.gpuData;
					if (expr_15A != null)
					{
						expr_15A.UpdateRanges(this.updateRanges.Slice((int)this.m_UpdateRangesBatchStart, (int)this.m_UpdateRangesEnqueued), (int)num2, (int)rangesMax);
					}
					this.ResetUpdateState();
				}
			}

			private void ResetUpdateState()
			{
				this.m_UpdateRangeMin = 4294967295u;
				this.m_UpdateRangeMax = 0u;
				this.m_UpdateRangesEnqueued = 0u;
				this.m_UpdateRangesBatchStart += this.m_UpdateRangePoolSize;
				bool flag = (ulong)this.m_UpdateRangesBatchStart >= (ulong)((long)this.updateRanges.Length);
				if (flag)
				{
					this.m_UpdateRangesBatchStart = 0u;
				}
				this.m_UpdateRangesSaturated = false;
			}
		}

		public Page.DataSet<Vertex> vertices;

		public Page.DataSet<ushort> indices;

		public Page next;

		public int framesEmpty;

		protected bool disposed
		{
			get;
			private set;
		}

		public bool isEmpty
		{
			get
			{
				return this.vertices.allocator.isEmpty && this.indices.allocator.isEmpty;
			}
		}

		public Page(uint vertexMaxCount, uint indexMaxCount, uint maxQueuedFrameCount, bool mockPage)
		{
			vertexMaxCount = Math.Min(vertexMaxCount, 65536u);
			this.vertices = new Page.DataSet<Vertex>(Utility.GPUBufferType.Vertex, vertexMaxCount, maxQueuedFrameCount, 32u, mockPage);
			this.indices = new Page.DataSet<ushort>(Utility.GPUBufferType.Index, indexMaxCount, maxQueuedFrameCount, 32u, mockPage);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.indices.Dispose();
					this.vertices.Dispose();
				}
				this.disposed = true;
			}
		}
	}
}
