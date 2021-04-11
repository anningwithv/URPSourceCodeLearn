using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine.Rendering;

namespace UnityEngine.UIElements.UIR
{
	internal class UIRenderDevice : IDisposable
	{
		private struct AllocToUpdate
		{
			public uint id;

			public uint allocTime;

			public MeshHandle meshHandle;

			public Alloc permAllocVerts;

			public Alloc permAllocIndices;

			public Page permPage;

			public bool copyBackIndices;
		}

		private struct AllocToFree
		{
			public Alloc alloc;

			public Page page;

			public bool vertices;
		}

		private struct DeviceToFree
		{
			public uint handle;

			public Page page;

			public void Dispose()
			{
				while (this.page != null)
				{
					Page page = this.page;
					this.page = this.page.next;
					page.Dispose();
				}
			}
		}

		internal struct AllocationStatistics
		{
			public struct PageStatistics
			{
				internal HeapStatistics vertices;

				internal HeapStatistics indices;
			}

			public UIRenderDevice.AllocationStatistics.PageStatistics[] pages;

			public int[] freesDeferred;

			public bool completeInit;
		}

		internal struct DrawStatistics
		{
			public int currentFrameIndex;

			public uint totalIndices;

			public uint commandCount;

			public uint drawCommandCount;

			public uint materialSetCount;

			public uint drawRangeCount;

			public uint drawRangeCallCount;

			public uint immediateDraws;
		}

		internal const uint k_MaxQueuedFrameCount = 4u;

		internal const int k_PruneEmptyPageFrameCount = 60;

		private readonly bool m_MockDevice;

		private IntPtr m_VertexDecl;

		private Page m_FirstPage;

		private uint m_NextPageVertexCount;

		private uint m_LargeMeshVertexCount;

		private float m_IndexToVertexCountRatio;

		private List<List<UIRenderDevice.AllocToFree>> m_DeferredFrees;

		private List<List<UIRenderDevice.AllocToUpdate>> m_Updates;

		private uint[] m_Fences;

		private MaterialPropertyBlock m_StandardMatProps;

		private MaterialPropertyBlock m_CommonMatProps;

		private uint m_FrameIndex;

		private uint m_NextUpdateID = 1u;

		private UIRenderDevice.DrawStatistics m_DrawStats;

		private readonly Pool<MeshHandle> m_MeshHandles = new Pool<MeshHandle>();

		private readonly DrawParams m_DrawParams = new DrawParams();

		private static LinkedList<UIRenderDevice.DeviceToFree> m_DeviceFreeQueue;

		private static int m_ActiveDeviceCount;

		private static bool m_SubscribedToNotifications;

		private static bool m_SynchronousFree;

		private static readonly int s_MainTexPropID;

		private static readonly int s_FontTexPropID;

		private static readonly int s_CustomTexPropID;

		private static readonly int s_1PixelClipInvViewPropID;

		private static readonly int s_GradientSettingsTexID;

		private static readonly int s_ShaderInfoTexID;

		private static readonly int s_ScreenClipRectPropID;

		private static readonly int s_TransformsPropID;

		private static readonly int s_ClipRectsPropID;

		private static ProfilerMarker s_MarkerAllocate;

		private static ProfilerMarker s_MarkerFree;

		private static ProfilerMarker s_MarkerAdvanceFrame;

		private static ProfilerMarker s_MarkerFence;

		private static ProfilerMarker s_MarkerBeforeDraw;

		private static bool? s_VertexTexturingIsAvailable;

		private const string k_VertexTexturingIsAvailableTag = "UIE_VertexTexturingIsAvailable";

		private const string k_VertexTexturingIsAvailableTrue = "1";

		internal static Func<Shader> getEditorShader;

		private static Texture2D s_WhiteTexel;

		private static Texture2D s_DefaultShaderInfoTexFloat;

		private static Texture2D s_DefaultShaderInfoTexARGB8;

		internal uint maxVerticesPerPage
		{
			[CompilerGenerated]
			get
			{
				return this.<maxVerticesPerPage>k__BackingField;
			}
		}

		internal static Texture2D whiteTexel
		{
			get
			{
				bool flag = UIRenderDevice.s_WhiteTexel == null;
				if (flag)
				{
					UIRenderDevice.s_WhiteTexel = new Texture2D(1, 1, TextureFormat.RGBA32, false);
					UIRenderDevice.s_WhiteTexel.hideFlags = HideFlags.HideAndDontSave;
					UIRenderDevice.s_WhiteTexel.filterMode = FilterMode.Bilinear;
					UIRenderDevice.s_WhiteTexel.SetPixel(0, 0, Color.white);
					UIRenderDevice.s_WhiteTexel.Apply(false, true);
				}
				return UIRenderDevice.s_WhiteTexel;
			}
		}

		internal static Texture2D defaultShaderInfoTexFloat
		{
			get
			{
				bool flag = UIRenderDevice.s_DefaultShaderInfoTexFloat == null;
				if (flag)
				{
					UIRenderDevice.s_DefaultShaderInfoTexFloat = new Texture2D(64, 64, TextureFormat.RGBAFloat, false);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.hideFlags = HideFlags.HideAndDontSave;
					UIRenderDevice.s_DefaultShaderInfoTexFloat.filterMode = FilterMode.Point;
					UIRenderDevice.s_DefaultShaderInfoTexFloat.SetPixel(UIRVEShaderInfoAllocator.identityTransformTexel.x, UIRVEShaderInfoAllocator.identityTransformTexel.y, UIRVEShaderInfoAllocator.identityTransformRow0Value);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.SetPixel(UIRVEShaderInfoAllocator.identityTransformTexel.x, UIRVEShaderInfoAllocator.identityTransformTexel.y + 1, UIRVEShaderInfoAllocator.identityTransformRow1Value);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.SetPixel(UIRVEShaderInfoAllocator.identityTransformTexel.x, UIRVEShaderInfoAllocator.identityTransformTexel.y + 2, UIRVEShaderInfoAllocator.identityTransformRow2Value);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.SetPixel(UIRVEShaderInfoAllocator.infiniteClipRectTexel.x, UIRVEShaderInfoAllocator.infiniteClipRectTexel.y, UIRVEShaderInfoAllocator.infiniteClipRectValue);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.SetPixel(UIRVEShaderInfoAllocator.fullOpacityTexel.x, UIRVEShaderInfoAllocator.fullOpacityTexel.y, UIRVEShaderInfoAllocator.fullOpacityValue);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.Apply(false, true);
				}
				return UIRenderDevice.s_DefaultShaderInfoTexFloat;
			}
		}

		internal static Texture2D defaultShaderInfoTexARGB8
		{
			get
			{
				bool flag = UIRenderDevice.s_DefaultShaderInfoTexARGB8 == null;
				if (flag)
				{
					UIRenderDevice.s_DefaultShaderInfoTexARGB8 = new Texture2D(64, 64, TextureFormat.RGBA32, false);
					UIRenderDevice.s_DefaultShaderInfoTexARGB8.hideFlags = HideFlags.HideAndDontSave;
					UIRenderDevice.s_DefaultShaderInfoTexARGB8.filterMode = FilterMode.Point;
					UIRenderDevice.s_DefaultShaderInfoTexARGB8.SetPixel(UIRVEShaderInfoAllocator.fullOpacityTexel.x, UIRVEShaderInfoAllocator.fullOpacityTexel.y, UIRVEShaderInfoAllocator.fullOpacityValue);
					UIRenderDevice.s_DefaultShaderInfoTexARGB8.Apply(false, true);
				}
				return UIRenderDevice.s_DefaultShaderInfoTexARGB8;
			}
		}

		internal static bool vertexTexturingIsAvailable
		{
			get
			{
				bool flag = !UIRenderDevice.s_VertexTexturingIsAvailable.HasValue;
				if (flag)
				{
					Shader shader = Shader.Find(UIRUtility.k_DefaultShaderName);
					Material material = new Material(shader);
					material.hideFlags |= HideFlags.DontSaveInEditor;
					string tag = material.GetTag("UIE_VertexTexturingIsAvailable", false);
					UIRUtility.Destroy(material);
					UIRenderDevice.s_VertexTexturingIsAvailable = new bool?(tag == "1");
				}
				return UIRenderDevice.s_VertexTexturingIsAvailable.Value;
			}
		}

		private bool fullyCreated
		{
			get
			{
				return this.m_Fences != null;
			}
		}

		protected bool disposed
		{
			get;
			private set;
		}

		static UIRenderDevice()
		{
			UIRenderDevice.m_DeviceFreeQueue = new LinkedList<UIRenderDevice.DeviceToFree>();
			UIRenderDevice.m_ActiveDeviceCount = 0;
			UIRenderDevice.s_MainTexPropID = Shader.PropertyToID("_MainTex");
			UIRenderDevice.s_FontTexPropID = Shader.PropertyToID("_FontTex");
			UIRenderDevice.s_CustomTexPropID = Shader.PropertyToID("_CustomTex");
			UIRenderDevice.s_1PixelClipInvViewPropID = Shader.PropertyToID("_1PixelClipInvView");
			UIRenderDevice.s_GradientSettingsTexID = Shader.PropertyToID("_GradientSettingsTex");
			UIRenderDevice.s_ShaderInfoTexID = Shader.PropertyToID("_ShaderInfoTex");
			UIRenderDevice.s_ScreenClipRectPropID = Shader.PropertyToID("_ScreenClipRect");
			UIRenderDevice.s_TransformsPropID = Shader.PropertyToID("_Transforms");
			UIRenderDevice.s_ClipRectsPropID = Shader.PropertyToID("_ClipRects");
			UIRenderDevice.s_MarkerAllocate = new ProfilerMarker("UIR.Allocate");
			UIRenderDevice.s_MarkerFree = new ProfilerMarker("UIR.Free");
			UIRenderDevice.s_MarkerAdvanceFrame = new ProfilerMarker("UIR.AdvanceFrame");
			UIRenderDevice.s_MarkerFence = new ProfilerMarker("UIR.WaitOnFence");
			UIRenderDevice.s_MarkerBeforeDraw = new ProfilerMarker("UIR.BeforeDraw");
			UIRenderDevice.getEditorShader = null;
			Utility.EngineUpdate += new Action(UIRenderDevice.OnEngineUpdateGlobal);
			Utility.FlushPendingResources += new Action(UIRenderDevice.OnFlushPendingResources);
		}

		public UIRenderDevice(uint initialVertexCapacity = 0u, uint initialIndexCapacity = 0u) : this(initialVertexCapacity, initialIndexCapacity, false)
		{
		}

		protected UIRenderDevice(uint initialVertexCapacity, uint initialIndexCapacity, bool mockDevice)
		{
			this.<maxVerticesPerPage>k__BackingField = 65535u;
			base..ctor();
			this.m_MockDevice = mockDevice;
			Debug.Assert(!UIRenderDevice.m_SynchronousFree);
			Debug.Assert(true);
			bool flag = UIRenderDevice.m_ActiveDeviceCount++ == 0;
			if (flag)
			{
				bool flag2 = !UIRenderDevice.m_SubscribedToNotifications && !this.m_MockDevice;
				if (flag2)
				{
					Utility.NotifyOfUIREvents(true);
					UIRenderDevice.m_SubscribedToNotifications = true;
				}
			}
			this.m_NextPageVertexCount = Math.Max(initialVertexCapacity, 2048u);
			this.m_LargeMeshVertexCount = this.m_NextPageVertexCount;
			this.m_IndexToVertexCountRatio = initialIndexCapacity / initialVertexCapacity;
			this.m_IndexToVertexCountRatio = Mathf.Max(this.m_IndexToVertexCountRatio, 2f);
			this.m_DeferredFrees = new List<List<UIRenderDevice.AllocToFree>>(4);
			this.m_Updates = new List<List<UIRenderDevice.AllocToUpdate>>(4);
			int num = 0;
			while ((long)num < 4L)
			{
				this.m_DeferredFrees.Add(new List<UIRenderDevice.AllocToFree>());
				this.m_Updates.Add(new List<UIRenderDevice.AllocToUpdate>());
				num++;
			}
		}

		private void InitVertexDeclaration()
		{
			VertexAttributeDescriptor[] vertexAttributes = new VertexAttributeDescriptor[]
			{
				new VertexAttributeDescriptor(VertexAttribute.Position, VertexAttributeFormat.Float32, 3, 0),
				new VertexAttributeDescriptor(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord0, VertexAttributeFormat.Float32, 2, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord1, VertexAttributeFormat.UNorm8, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord2, VertexAttributeFormat.UNorm8, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord3, VertexAttributeFormat.UNorm8, 4, 0)
			};
			this.m_VertexDecl = Utility.GetVertexDeclaration(vertexAttributes);
		}

		private void CompleteCreation()
		{
			bool flag = this.m_MockDevice || this.fullyCreated;
			if (!flag)
			{
				this.InitVertexDeclaration();
				this.m_Fences = new uint[4];
				this.m_StandardMatProps = new MaterialPropertyBlock();
				this.m_CommonMatProps = new MaterialPropertyBlock();
			}
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		internal void DisposeImmediate()
		{
			Debug.Assert(!UIRenderDevice.m_SynchronousFree);
			UIRenderDevice.m_SynchronousFree = true;
			this.Dispose();
			UIRenderDevice.m_SynchronousFree = false;
		}

		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				UIRenderDevice.m_ActiveDeviceCount--;
				if (disposing)
				{
					UIRenderDevice.DeviceToFree deviceToFree = new UIRenderDevice.DeviceToFree
					{
						handle = (this.m_MockDevice ? 0u : Utility.InsertCPUFence()),
						page = this.m_FirstPage
					};
					bool flag = deviceToFree.handle == 0u;
					if (flag)
					{
						deviceToFree.Dispose();
					}
					else
					{
						UIRenderDevice.m_DeviceFreeQueue.AddLast(deviceToFree);
						bool synchronousFree = UIRenderDevice.m_SynchronousFree;
						if (synchronousFree)
						{
							UIRenderDevice.ProcessDeviceFreeQueue();
						}
					}
				}
				this.disposed = true;
			}
		}

		public MeshHandle Allocate(uint vertexCount, uint indexCount, out NativeSlice<Vertex> vertexData, out NativeSlice<ushort> indexData, out ushort indexOffset)
		{
			MeshHandle meshHandle = this.m_MeshHandles.Get();
			meshHandle.triangleCount = indexCount / 3u;
			this.Allocate(meshHandle, vertexCount, indexCount, out vertexData, out indexData, false);
			indexOffset = (ushort)meshHandle.allocVerts.start;
			return meshHandle;
		}

		public void Update(MeshHandle mesh, uint vertexCount, out NativeSlice<Vertex> vertexData)
		{
			Debug.Assert(mesh.allocVerts.size >= vertexCount);
			bool flag = mesh.allocTime == this.m_FrameIndex;
			if (flag)
			{
				vertexData = mesh.allocPage.vertices.cpuData.Slice((int)mesh.allocVerts.start, (int)vertexCount);
			}
			else
			{
				uint start = mesh.allocVerts.start;
				NativeSlice<ushort> nativeSlice = new NativeSlice<ushort>(mesh.allocPage.indices.cpuData, (int)mesh.allocIndices.start, (int)mesh.allocIndices.size);
				NativeSlice<ushort> nativeSlice2;
				ushort num;
				UIRenderDevice.AllocToUpdate allocToUpdate;
				this.UpdateAfterGPUUsedData(mesh, vertexCount, mesh.allocIndices.size, out vertexData, out nativeSlice2, out num, out allocToUpdate, false);
				int size = (int)mesh.allocIndices.size;
				int num2 = (int)((uint)num - start);
				for (int i = 0; i < size; i++)
				{
					nativeSlice2[i] = (ushort)((int)nativeSlice[i] + num2);
				}
			}
		}

		public void Update(MeshHandle mesh, uint vertexCount, uint indexCount, out NativeSlice<Vertex> vertexData, out NativeSlice<ushort> indexData, out ushort indexOffset)
		{
			Debug.Assert(mesh.allocVerts.size >= vertexCount);
			Debug.Assert(mesh.allocIndices.size >= indexCount);
			bool flag = mesh.allocTime == this.m_FrameIndex;
			if (flag)
			{
				vertexData = mesh.allocPage.vertices.cpuData.Slice((int)mesh.allocVerts.start, (int)vertexCount);
				indexData = mesh.allocPage.indices.cpuData.Slice((int)mesh.allocIndices.start, (int)indexCount);
				indexOffset = (ushort)mesh.allocVerts.start;
			}
			else
			{
				UIRenderDevice.AllocToUpdate allocToUpdate;
				this.UpdateAfterGPUUsedData(mesh, vertexCount, indexCount, out vertexData, out indexData, out indexOffset, out allocToUpdate, true);
			}
		}

		private bool TryAllocFromPage(Page page, uint vertexCount, uint indexCount, ref Alloc va, ref Alloc ia, bool shortLived)
		{
			va = page.vertices.allocator.Allocate(vertexCount, shortLived);
			bool flag = va.size > 0u;
			bool result;
			if (flag)
			{
				ia = page.indices.allocator.Allocate(indexCount, shortLived);
				bool flag2 = ia.size > 0u;
				if (flag2)
				{
					result = true;
					return result;
				}
				page.vertices.allocator.Free(va);
				va.size = 0u;
			}
			result = false;
			return result;
		}

		private void Allocate(MeshHandle meshHandle, uint vertexCount, uint indexCount, out NativeSlice<Vertex> vertexData, out NativeSlice<ushort> indexData, bool shortLived)
		{
			UIRenderDevice.s_MarkerAllocate.Begin();
			Page page = null;
			Alloc alloc = default(Alloc);
			Alloc alloc2 = default(Alloc);
			bool flag = vertexCount <= this.m_LargeMeshVertexCount;
			if (flag)
			{
				bool flag2 = this.m_FirstPage != null;
				if (flag2)
				{
					page = this.m_FirstPage;
					while (true)
					{
						bool flag3 = this.TryAllocFromPage(page, vertexCount, indexCount, ref alloc, ref alloc2, shortLived) || page.next == null;
						if (flag3)
						{
							break;
						}
						page = page.next;
					}
				}
				else
				{
					this.CompleteCreation();
				}
				bool flag4 = alloc2.size == 0u;
				if (flag4)
				{
					this.m_NextPageVertexCount <<= 1;
					this.m_NextPageVertexCount = Math.Max(this.m_NextPageVertexCount, vertexCount * 2u);
					this.m_NextPageVertexCount = Math.Min(this.m_NextPageVertexCount, this.maxVerticesPerPage);
					uint num = (uint)(this.m_NextPageVertexCount * this.m_IndexToVertexCountRatio + 0.5f);
					num = Math.Max(num, indexCount * 2u);
					Debug.Assert(((page != null) ? page.next : null) == null);
					page = new Page(this.m_NextPageVertexCount, num, 4u, this.m_MockDevice);
					page.next = this.m_FirstPage;
					this.m_FirstPage = page;
					alloc = page.vertices.allocator.Allocate(vertexCount, shortLived);
					alloc2 = page.indices.allocator.Allocate(indexCount, shortLived);
					Debug.Assert(alloc.size > 0u);
					Debug.Assert(alloc2.size > 0u);
				}
			}
			else
			{
				this.CompleteCreation();
				Page page2 = this.m_FirstPage;
				Page page3 = this.m_FirstPage;
				int num2 = 2147483647;
				while (page2 != null)
				{
					int num3 = page2.vertices.cpuData.Length - (int)vertexCount;
					int num4 = page2.indices.cpuData.Length - (int)indexCount;
					bool flag5 = page2.isEmpty && num3 >= 0 && num4 >= 0 && num3 < num2;
					if (flag5)
					{
						page = page2;
						num2 = num3;
					}
					page3 = page2;
					page2 = page2.next;
				}
				bool flag6 = page == null;
				if (flag6)
				{
					uint vertexMaxCount = (vertexCount > this.maxVerticesPerPage) ? 2u : vertexCount;
					Debug.Assert(vertexCount <= this.maxVerticesPerPage, string.Format("Requested Vertex count ({0}) is above the limit ({1}). Alloc will fail.", vertexCount, this.maxVerticesPerPage));
					page = new Page(vertexMaxCount, indexCount, 4u, this.m_MockDevice);
					bool flag7 = page3 != null;
					if (flag7)
					{
						page3.next = page;
					}
					else
					{
						this.m_FirstPage = page;
					}
				}
				alloc = page.vertices.allocator.Allocate(vertexCount, shortLived);
				alloc2 = page.indices.allocator.Allocate(indexCount, shortLived);
			}
			Debug.Assert(alloc.size == vertexCount, string.Format("Vertices allocated ({0}) != Vertices requested ({1})", alloc.size, vertexCount));
			Debug.Assert(alloc2.size == indexCount, string.Format("Indices allocated ({0}) != Indices requested ({1})", alloc2.size, indexCount));
			bool flag8 = alloc.size != vertexCount || alloc2.size != indexCount;
			if (flag8)
			{
				bool flag9 = alloc.handle != null;
				if (flag9)
				{
					page.vertices.allocator.Free(alloc);
				}
				bool flag10 = alloc2.handle != null;
				if (flag10)
				{
					page.vertices.allocator.Free(alloc2);
				}
				alloc2 = default(Alloc);
				alloc = default(Alloc);
			}
			page.vertices.RegisterUpdate(alloc.start, alloc.size);
			page.indices.RegisterUpdate(alloc2.start, alloc2.size);
			vertexData = new NativeSlice<Vertex>(page.vertices.cpuData, (int)alloc.start, (int)alloc.size);
			indexData = new NativeSlice<ushort>(page.indices.cpuData, (int)alloc2.start, (int)alloc2.size);
			meshHandle.allocPage = page;
			meshHandle.allocVerts = alloc;
			meshHandle.allocIndices = alloc2;
			meshHandle.allocTime = this.m_FrameIndex;
			UIRenderDevice.s_MarkerAllocate.End();
		}

		private void UpdateAfterGPUUsedData(MeshHandle mesh, uint vertexCount, uint indexCount, out NativeSlice<Vertex> vertexData, out NativeSlice<ushort> indexData, out ushort indexOffset, out UIRenderDevice.AllocToUpdate allocToUpdate, bool copyBackIndices)
		{
			UIRenderDevice.AllocToUpdate allocToUpdate2 = default(UIRenderDevice.AllocToUpdate);
			uint nextUpdateID = this.m_NextUpdateID;
			this.m_NextUpdateID = nextUpdateID + 1u;
			allocToUpdate2.id = nextUpdateID;
			allocToUpdate2.allocTime = this.m_FrameIndex;
			allocToUpdate2.meshHandle = mesh;
			allocToUpdate2.copyBackIndices = copyBackIndices;
			allocToUpdate = allocToUpdate2;
			Debug.Assert(this.m_NextUpdateID > 0u);
			bool flag = mesh.updateAllocID == 0u;
			if (flag)
			{
				allocToUpdate.permAllocVerts = mesh.allocVerts;
				allocToUpdate.permAllocIndices = mesh.allocIndices;
				allocToUpdate.permPage = mesh.allocPage;
			}
			else
			{
				int index = (int)(mesh.updateAllocID - 1u);
				List<UIRenderDevice.AllocToUpdate> list = this.m_Updates[(int)(mesh.allocTime % (uint)this.m_Updates.Count)];
				UIRenderDevice.AllocToUpdate allocToUpdate3 = list[index];
				Debug.Assert(allocToUpdate3.id == mesh.updateAllocID);
				allocToUpdate.copyBackIndices |= allocToUpdate3.copyBackIndices;
				allocToUpdate.permAllocVerts = allocToUpdate3.permAllocVerts;
				allocToUpdate.permAllocIndices = allocToUpdate3.permAllocIndices;
				allocToUpdate.permPage = allocToUpdate3.permPage;
				allocToUpdate3.allocTime = 4294967295u;
				list[index] = allocToUpdate3;
				List<UIRenderDevice.AllocToFree> list2 = this.m_DeferredFrees[(int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count)];
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = mesh.allocVerts,
					page = mesh.allocPage,
					vertices = true
				});
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = mesh.allocIndices,
					page = mesh.allocPage,
					vertices = false
				});
			}
			bool flag2 = this.TryAllocFromPage(mesh.allocPage, vertexCount, indexCount, ref mesh.allocVerts, ref mesh.allocIndices, true);
			if (flag2)
			{
				mesh.allocPage.vertices.RegisterUpdate(mesh.allocVerts.start, mesh.allocVerts.size);
				mesh.allocPage.indices.RegisterUpdate(mesh.allocIndices.start, mesh.allocIndices.size);
			}
			else
			{
				this.Allocate(mesh, vertexCount, indexCount, out vertexData, out indexData, true);
			}
			mesh.triangleCount = indexCount / 3u;
			mesh.updateAllocID = allocToUpdate.id;
			mesh.allocTime = allocToUpdate.allocTime;
			this.m_Updates[(int)((ulong)this.m_FrameIndex % (ulong)((long)this.m_Updates.Count))].Add(allocToUpdate);
			vertexData = new NativeSlice<Vertex>(mesh.allocPage.vertices.cpuData, (int)mesh.allocVerts.start, (int)vertexCount);
			indexData = new NativeSlice<ushort>(mesh.allocPage.indices.cpuData, (int)mesh.allocIndices.start, (int)indexCount);
			indexOffset = (ushort)mesh.allocVerts.start;
		}

		public void Free(MeshHandle mesh)
		{
			bool flag = mesh.updateAllocID > 0u;
			if (flag)
			{
				int index = (int)(mesh.updateAllocID - 1u);
				List<UIRenderDevice.AllocToUpdate> list = this.m_Updates[(int)(mesh.allocTime % (uint)this.m_Updates.Count)];
				UIRenderDevice.AllocToUpdate allocToUpdate = list[index];
				Debug.Assert(allocToUpdate.id == mesh.updateAllocID);
				List<UIRenderDevice.AllocToFree> list2 = this.m_DeferredFrees[(int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count)];
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = allocToUpdate.permAllocVerts,
					page = allocToUpdate.permPage,
					vertices = true
				});
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = allocToUpdate.permAllocIndices,
					page = allocToUpdate.permPage,
					vertices = false
				});
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = mesh.allocVerts,
					page = mesh.allocPage,
					vertices = true
				});
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = mesh.allocIndices,
					page = mesh.allocPage,
					vertices = false
				});
				allocToUpdate.allocTime = 4294967295u;
				list[index] = allocToUpdate;
			}
			else
			{
				bool flag2 = mesh.allocTime != this.m_FrameIndex;
				if (flag2)
				{
					int index2 = (int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count);
					this.m_DeferredFrees[index2].Add(new UIRenderDevice.AllocToFree
					{
						alloc = mesh.allocVerts,
						page = mesh.allocPage,
						vertices = true
					});
					this.m_DeferredFrees[index2].Add(new UIRenderDevice.AllocToFree
					{
						alloc = mesh.allocIndices,
						page = mesh.allocPage,
						vertices = false
					});
				}
				else
				{
					mesh.allocPage.vertices.allocator.Free(mesh.allocVerts);
					mesh.allocPage.indices.allocator.Free(mesh.allocIndices);
				}
			}
			mesh.allocVerts = default(Alloc);
			mesh.allocIndices = default(Alloc);
			mesh.allocPage = null;
			mesh.updateAllocID = 0u;
			this.m_MeshHandles.Return(mesh);
		}

		private static void Set1PixelSizeParameter(DrawParams drawParams, MaterialPropertyBlock props)
		{
			Vector4 vector = default(Vector4);
			RectInt activeViewport = Utility.GetActiveViewport();
			vector.x = 2f / (float)activeViewport.width;
			vector.y = 2f / (float)activeViewport.height;
			Matrix4x4 unityProjectionMatrix = Utility.GetUnityProjectionMatrix();
			Vector3 vector2 = (unityProjectionMatrix * drawParams.view.Peek().transform).inverse.MultiplyVector(new Vector3(vector.x, vector.y));
			vector.z = 1f / (Mathf.Abs(vector2.x) + Mathf.Epsilon);
			vector.w = 1f / (Mathf.Abs(vector2.y) + Mathf.Epsilon);
			props.SetVector(UIRenderDevice.s_1PixelClipInvViewPropID, vector);
		}

		public void OnFrameRenderingBegin()
		{
			this.AdvanceFrame();
			this.m_DrawStats = default(UIRenderDevice.DrawStatistics);
			this.m_DrawStats.currentFrameIndex = (int)this.m_FrameIndex;
			UIRenderDevice.s_MarkerBeforeDraw.Begin();
			for (Page page = this.m_FirstPage; page != null; page = page.next)
			{
				page.vertices.SendUpdates();
				page.indices.SendUpdates();
			}
			UIRenderDevice.s_MarkerBeforeDraw.End();
		}

		private unsafe static NativeSlice<T> PtrToSlice<T>(void* p, int count) where T : struct
		{
			NativeSlice<T> result = NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<T>(p, UnsafeUtility.SizeOf<T>(), count);
			NativeSliceUnsafeUtility.SetAtomicSafetyHandle<T>(ref result, AtomicSafetyHandle.GetTempUnsafePtrSliceHandle());
			return result;
		}

		public unsafe void EvaluateChain(RenderChainCommand head, Material initialMat, Material defaultMat, Texture atlas, Texture gradientSettings, Texture shaderInfo, float pixelsPerPoint, NativeSlice<Transform3x4> transforms, NativeSlice<Vector4> clipRects, MaterialPropertyBlock stateMatProps, bool allowMaterialChange, ref Exception immediateException)
		{
			Utility.ProfileDrawChainBegin();
			DrawParams drawParams = this.m_DrawParams;
			drawParams.Reset();
			stateMatProps.Clear();
			bool fullyCreated = this.fullyCreated;
			if (fullyCreated)
			{
				bool flag = atlas != null;
				if (flag)
				{
					this.m_StandardMatProps.SetTexture(UIRenderDevice.s_MainTexPropID, atlas);
				}
				bool flag2 = gradientSettings != null;
				if (flag2)
				{
					this.m_StandardMatProps.SetTexture(UIRenderDevice.s_GradientSettingsTexID, gradientSettings);
				}
				bool flag3 = shaderInfo != null;
				if (flag3)
				{
					this.m_StandardMatProps.SetTexture(UIRenderDevice.s_ShaderInfoTexID, shaderInfo);
				}
				bool flag4 = transforms.Length > 0;
				if (flag4)
				{
					Utility.SetVectorArray<Transform3x4>(this.m_StandardMatProps, UIRenderDevice.s_TransformsPropID, transforms);
				}
				bool flag5 = clipRects.Length > 0;
				if (flag5)
				{
					Utility.SetVectorArray<Vector4>(this.m_StandardMatProps, UIRenderDevice.s_ClipRectsPropID, clipRects);
				}
				UIRenderDevice.Set1PixelSizeParameter(drawParams, this.m_CommonMatProps);
				this.m_CommonMatProps.SetVector(UIRenderDevice.s_ScreenClipRectPropID, drawParams.view.Peek().clipRect);
				Utility.SetPropertyBlock(this.m_StandardMatProps);
				Utility.SetPropertyBlock(this.m_CommonMatProps);
			}
			int num = 1024;
			DrawBufferRange* ptr = stackalloc DrawBufferRange[num];
			int num2 = num - 1;
			int num3 = 0;
			int num4 = 0;
			DrawBufferRange drawBufferRange = default(DrawBufferRange);
			Page page = null;
			State state = new State
			{
				material = initialMat
			};
			int num5 = -1;
			int num6 = 0;
			while (head != null)
			{
				this.m_DrawStats.commandCount = this.m_DrawStats.commandCount + 1u;
				this.m_DrawStats.drawCommandCount = this.m_DrawStats.drawCommandCount + ((head.type == CommandType.Draw) ? 1u : 0u);
				bool flag6 = head.type > CommandType.Draw;
				bool flag7 = true;
				bool flag8 = false;
				bool flag9 = false;
				bool flag10 = !flag6;
				if (flag10)
				{
					Material material = (head.state.material != null) ? head.state.material : defaultMat;
					flag8 = (material != state.material);
					state.material = material;
					bool flag11 = head.state.custom != null;
					if (flag11)
					{
						flag9 |= (head.state.custom != state.custom);
						state.custom = head.state.custom;
						stateMatProps.SetTexture(UIRenderDevice.s_CustomTexPropID, head.state.custom);
					}
					bool flag12 = head.state.font != null;
					if (flag12)
					{
						flag9 |= (head.state.font != state.font);
						state.font = head.state.font;
						stateMatProps.SetTexture(UIRenderDevice.s_FontTexPropID, head.state.font);
					}
					flag6 = ((flag9 | flag8) || head.mesh.allocPage != page);
					bool flag13 = !flag6;
					if (flag13)
					{
						flag7 = ((long)num5 != (long)((ulong)head.mesh.allocIndices.start + (ulong)((long)head.indexOffset)));
					}
				}
				bool flag14 = flag7;
				if (flag14)
				{
					bool flag15 = drawBufferRange.indexCount > 0;
					if (flag15)
					{
						int num7 = num3 + num4++ & num2;
						ptr[num7] = drawBufferRange;
						bool flag16 = num4 == num;
						if (flag16)
						{
							this.KickRanges(ptr, ref num4, ref num3, num, page);
						}
						drawBufferRange = default(DrawBufferRange);
						this.m_DrawStats.drawRangeCount = this.m_DrawStats.drawRangeCount + 1u;
					}
					bool flag17 = head.type == CommandType.Draw;
					if (flag17)
					{
						drawBufferRange.firstIndex = (int)(head.mesh.allocIndices.start + (uint)head.indexOffset);
						drawBufferRange.indexCount = head.indexCount;
						drawBufferRange.vertsReferenced = (int)(head.mesh.allocVerts.start + head.mesh.allocVerts.size);
						drawBufferRange.minIndexVal = (int)head.mesh.allocVerts.start;
						num5 = drawBufferRange.firstIndex + head.indexCount;
						num6 = drawBufferRange.vertsReferenced + drawBufferRange.minIndexVal;
						this.m_DrawStats.totalIndices = this.m_DrawStats.totalIndices + (uint)head.indexCount;
					}
					bool flag18 = flag6;
					if (flag18)
					{
						this.KickRanges(ptr, ref num4, ref num3, num, page);
						bool flag19 = head.type > CommandType.Draw;
						if (flag19)
						{
							bool flag20 = !this.m_MockDevice;
							if (flag20)
							{
								head.ExecuteNonDrawMesh(drawParams, pixelsPerPoint, ref immediateException);
							}
							bool flag21 = head.type == CommandType.Immediate || head.type == CommandType.ImmediateCull;
							if (flag21)
							{
								state.material = null;
								flag8 = false;
								this.m_DrawStats.immediateDraws = this.m_DrawStats.immediateDraws + 1u;
							}
						}
						else
						{
							page = head.mesh.allocPage;
						}
						bool flag22 = flag8 | flag9;
						if (flag22)
						{
							bool flag23 = !this.m_MockDevice;
							if (flag23)
							{
								bool flag24 = flag8;
								if (flag24)
								{
									bool flag25 = !allowMaterialChange;
									if (flag25)
									{
										IL_76E:
										Utility.ProfileDrawChainEnd();
										return;
									}
									state.material.SetPass(0);
									bool flag26 = this.m_StandardMatProps != null;
									if (flag26)
									{
										Utility.SetPropertyBlock(this.m_StandardMatProps);
										Utility.SetPropertyBlock(this.m_CommonMatProps);
									}
									Utility.SetPropertyBlock(stateMatProps);
								}
								else
								{
									bool flag27 = flag9;
									if (flag27)
									{
										Utility.SetPropertyBlock(stateMatProps);
									}
									else
									{
										bool flag28 = this.m_CommonMatProps != null && (head.type == CommandType.PushView || head.type == CommandType.PopView);
										if (flag28)
										{
											UIRenderDevice.Set1PixelSizeParameter(drawParams, this.m_CommonMatProps);
											this.m_CommonMatProps.SetVector(UIRenderDevice.s_ScreenClipRectPropID, drawParams.view.Peek().clipRect);
											Utility.SetPropertyBlock(this.m_CommonMatProps);
										}
									}
								}
							}
							this.m_DrawStats.materialSetCount = this.m_DrawStats.materialSetCount + 1u;
						}
						else
						{
							bool flag29 = head.type == CommandType.PushView || head.type == CommandType.PopView;
							if (flag29)
							{
								bool flag30 = this.m_CommonMatProps != null;
								if (flag30)
								{
									UIRenderDevice.Set1PixelSizeParameter(drawParams, this.m_CommonMatProps);
									this.m_CommonMatProps.SetVector(UIRenderDevice.s_ScreenClipRectPropID, drawParams.view.Peek().clipRect);
									Utility.SetPropertyBlock(this.m_CommonMatProps);
								}
								this.m_DrawStats.materialSetCount = this.m_DrawStats.materialSetCount + 1u;
							}
						}
					}
					head = head.next;
				}
				else
				{
					bool flag31 = drawBufferRange.indexCount == 0;
					if (flag31)
					{
						num5 = (drawBufferRange.firstIndex = (int)(head.mesh.allocIndices.start + (uint)head.indexOffset));
					}
					num6 = Math.Max(num6, (int)(head.mesh.allocVerts.size + head.mesh.allocVerts.start));
					drawBufferRange.indexCount += head.indexCount;
					drawBufferRange.minIndexVal = Math.Min(drawBufferRange.minIndexVal, (int)head.mesh.allocVerts.start);
					drawBufferRange.vertsReferenced = num6 - drawBufferRange.minIndexVal;
					num5 += head.indexCount;
					this.m_DrawStats.totalIndices = this.m_DrawStats.totalIndices + (uint)head.indexCount;
					head = head.next;
				}
			}
			bool flag32 = drawBufferRange.indexCount > 0;
			if (flag32)
			{
				int num8 = num3 + num4++ & num2;
				ptr[num8] = drawBufferRange;
			}
			bool flag33 = num4 > 0;
			if (flag33)
			{
				this.KickRanges(ptr, ref num4, ref num3, num, page);
			}
			this.UpdateFenceValue();
			goto IL_76E;
		}

		private unsafe void UpdateFenceValue()
		{
			bool flag = this.m_Fences != null;
			if (flag)
			{
				uint num = Utility.InsertCPUFence();
				fixed (uint* ptr = &this.m_Fences[(int)((ulong)this.m_FrameIndex % (ulong)((long)this.m_Fences.Length))])
				{
					uint* ptr2 = ptr;
					bool flag3;
					do
					{
						uint num2 = *ptr2;
						bool flag2 = num - num2 <= 0u;
						if (flag2)
						{
							break;
						}
						int num3 = Interlocked.CompareExchange(ref *(int*)ptr2, (int)num, (int)num2);
						flag3 = ((long)num3 == (long)((ulong)num2));
					}
					while (!flag3);
				}
			}
		}

		private unsafe void KickRanges(DrawBufferRange* ranges, ref int rangesReady, ref int rangesStart, int rangesCount, Page curPage)
		{
			bool flag = rangesReady > 0;
			if (flag)
			{
				bool flag2 = rangesStart + rangesReady <= rangesCount;
				if (flag2)
				{
					bool flag3 = !this.m_MockDevice;
					if (flag3)
					{
						this.DrawRanges<ushort, Vertex>(curPage.indices.gpuData, curPage.vertices.gpuData, UIRenderDevice.PtrToSlice<DrawBufferRange>((void*)(ranges + rangesStart), rangesReady));
					}
					this.m_DrawStats.drawRangeCallCount = this.m_DrawStats.drawRangeCallCount + 1u;
				}
				else
				{
					int num = rangesCount - rangesStart;
					int count = rangesReady - num;
					bool flag4 = !this.m_MockDevice;
					if (flag4)
					{
						this.DrawRanges<ushort, Vertex>(curPage.indices.gpuData, curPage.vertices.gpuData, UIRenderDevice.PtrToSlice<DrawBufferRange>((void*)(ranges + rangesStart), num));
						this.DrawRanges<ushort, Vertex>(curPage.indices.gpuData, curPage.vertices.gpuData, UIRenderDevice.PtrToSlice<DrawBufferRange>((void*)ranges, count));
					}
					this.m_DrawStats.drawRangeCallCount = this.m_DrawStats.drawRangeCallCount + 2u;
				}
				rangesStart = (rangesStart + rangesReady & rangesCount - 1);
				rangesReady = 0;
			}
		}

		private unsafe void DrawRanges<I, T>(Utility.GPUBuffer<I> ib, Utility.GPUBuffer<T> vb, NativeSlice<DrawBufferRange> ranges) where I : struct where T : struct
		{
			IntPtr* ptr = stackalloc IntPtr[checked(unchecked((UIntPtr)1) * (UIntPtr)sizeof(IntPtr)) / (UIntPtr)sizeof(IntPtr)];
			*ptr = vb.BufferPointer;
			Utility.DrawRanges(ib.BufferPointer, ptr, 1, new IntPtr(ranges.GetUnsafePtr<DrawBufferRange>()), ranges.Length, this.m_VertexDecl);
		}

		public void AdvanceFrame()
		{
			UIRenderDevice.s_MarkerAdvanceFrame.Begin();
			this.m_FrameIndex += 1u;
			this.m_DrawStats.currentFrameIndex = (int)this.m_FrameIndex;
			bool flag = this.m_Fences != null;
			if (flag)
			{
				int num = (int)((ulong)this.m_FrameIndex % (ulong)((long)this.m_Fences.Length));
				uint num2 = this.m_Fences[num];
				bool flag2 = num2 != 0u && !Utility.CPUFencePassed(num2);
				if (flag2)
				{
					UIRenderDevice.s_MarkerFence.Begin();
					Utility.WaitForCPUFencePassed(num2);
					UIRenderDevice.s_MarkerFence.End();
				}
				this.m_Fences[num] = 0u;
			}
			this.m_NextUpdateID = 1u;
			List<UIRenderDevice.AllocToFree> list = this.m_DeferredFrees[(int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count)];
			foreach (UIRenderDevice.AllocToFree current in list)
			{
				bool vertices = current.vertices;
				if (vertices)
				{
					current.page.vertices.allocator.Free(current.alloc);
				}
				else
				{
					current.page.indices.allocator.Free(current.alloc);
				}
			}
			list.Clear();
			List<UIRenderDevice.AllocToUpdate> list2 = this.m_Updates[(int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count)];
			foreach (UIRenderDevice.AllocToUpdate current2 in list2)
			{
				bool flag3 = current2.meshHandle.updateAllocID == current2.id && current2.meshHandle.allocTime == current2.allocTime;
				if (flag3)
				{
					NativeSlice<Vertex> slice = new NativeSlice<Vertex>(current2.meshHandle.allocPage.vertices.cpuData, (int)current2.meshHandle.allocVerts.start, (int)current2.meshHandle.allocVerts.size);
					NativeSlice<Vertex> nativeSlice = new NativeSlice<Vertex>(current2.permPage.vertices.cpuData, (int)current2.permAllocVerts.start, (int)current2.meshHandle.allocVerts.size);
					nativeSlice.CopyFrom(slice);
					current2.permPage.vertices.RegisterUpdate(current2.permAllocVerts.start, current2.meshHandle.allocVerts.size);
					bool copyBackIndices = current2.copyBackIndices;
					if (copyBackIndices)
					{
						NativeSlice<ushort> nativeSlice2 = new NativeSlice<ushort>(current2.meshHandle.allocPage.indices.cpuData, (int)current2.meshHandle.allocIndices.start, (int)current2.meshHandle.allocIndices.size);
						NativeSlice<ushort> nativeSlice3 = new NativeSlice<ushort>(current2.permPage.indices.cpuData, (int)current2.permAllocIndices.start, (int)current2.meshHandle.allocIndices.size);
						int length = nativeSlice3.Length;
						int num3 = (int)(current2.permAllocVerts.start - current2.meshHandle.allocVerts.start);
						for (int i = 0; i < length; i++)
						{
							nativeSlice3[i] = (ushort)((int)nativeSlice2[i] + num3);
						}
						current2.permPage.indices.RegisterUpdate(current2.permAllocIndices.start, current2.meshHandle.allocIndices.size);
					}
					list.Add(new UIRenderDevice.AllocToFree
					{
						alloc = current2.meshHandle.allocVerts,
						page = current2.meshHandle.allocPage,
						vertices = true
					});
					list.Add(new UIRenderDevice.AllocToFree
					{
						alloc = current2.meshHandle.allocIndices,
						page = current2.meshHandle.allocPage,
						vertices = false
					});
					current2.meshHandle.allocVerts = current2.permAllocVerts;
					current2.meshHandle.allocIndices = current2.permAllocIndices;
					current2.meshHandle.allocPage = current2.permPage;
					current2.meshHandle.updateAllocID = 0u;
				}
			}
			list2.Clear();
			this.PruneUnusedPages();
			UIRenderDevice.s_MarkerAdvanceFrame.End();
		}

		private void PruneUnusedPages()
		{
			Page page4;
			Page page3;
			Page page2;
			Page page = page2 = (page3 = (page4 = null));
			Page next;
			for (Page page5 = this.m_FirstPage; page5 != null; page5 = next)
			{
				bool flag = !page5.isEmpty;
				if (flag)
				{
					page5.framesEmpty = 0;
				}
				else
				{
					page5.framesEmpty++;
				}
				bool flag2 = page5.framesEmpty < 60;
				if (flag2)
				{
					bool flag3 = page2 != null;
					if (flag3)
					{
						page.next = page5;
					}
					else
					{
						page2 = page5;
					}
					page = page5;
				}
				else
				{
					bool flag4 = page3 != null;
					if (flag4)
					{
						page4.next = page5;
					}
					else
					{
						page3 = page5;
					}
					page4 = page5;
				}
				next = page5.next;
				page5.next = null;
			}
			this.m_FirstPage = page2;
			Page next2;
			for (Page page5 = page3; page5 != null; page5 = next2)
			{
				next2 = page5.next;
				page5.next = null;
				page5.Dispose();
			}
		}

		internal static void PrepareForGfxDeviceRecreate()
		{
			UIRenderDevice.m_ActiveDeviceCount++;
			bool flag = UIRenderDevice.s_WhiteTexel != null;
			if (flag)
			{
				UIRUtility.Destroy(UIRenderDevice.s_WhiteTexel);
				UIRenderDevice.s_WhiteTexel = null;
			}
			bool flag2 = UIRenderDevice.s_DefaultShaderInfoTexFloat != null;
			if (flag2)
			{
				UIRUtility.Destroy(UIRenderDevice.s_DefaultShaderInfoTexFloat);
				UIRenderDevice.s_DefaultShaderInfoTexFloat = null;
			}
			bool flag3 = UIRenderDevice.s_DefaultShaderInfoTexARGB8 != null;
			if (flag3)
			{
				UIRUtility.Destroy(UIRenderDevice.s_DefaultShaderInfoTexARGB8);
				UIRenderDevice.s_DefaultShaderInfoTexARGB8 = null;
			}
		}

		internal static void WrapUpGfxDeviceRecreate()
		{
			UIRenderDevice.m_ActiveDeviceCount--;
		}

		internal static void FlushAllPendingDeviceDisposes()
		{
			Utility.SyncRenderThread();
			UIRenderDevice.ProcessDeviceFreeQueue();
		}

		internal UIRenderDevice.AllocationStatistics GatherAllocationStatistics()
		{
			UIRenderDevice.AllocationStatistics allocationStatistics = default(UIRenderDevice.AllocationStatistics);
			allocationStatistics.completeInit = this.fullyCreated;
			allocationStatistics.freesDeferred = new int[this.m_DeferredFrees.Count];
			for (int i = 0; i < this.m_DeferredFrees.Count; i++)
			{
				allocationStatistics.freesDeferred[i] = this.m_DeferredFrees[i].Count;
			}
			int num = 0;
			for (Page page = this.m_FirstPage; page != null; page = page.next)
			{
				num++;
			}
			allocationStatistics.pages = new UIRenderDevice.AllocationStatistics.PageStatistics[num];
			num = 0;
			for (Page page = this.m_FirstPage; page != null; page = page.next)
			{
				allocationStatistics.pages[num].vertices = page.vertices.allocator.GatherStatistics();
				allocationStatistics.pages[num].indices = page.indices.allocator.GatherStatistics();
				num++;
			}
			return allocationStatistics;
		}

		internal UIRenderDevice.DrawStatistics GatherDrawStatistics()
		{
			return this.m_DrawStats;
		}

		private static void ProcessDeviceFreeQueue()
		{
			UIRenderDevice.s_MarkerFree.Begin();
			bool synchronousFree = UIRenderDevice.m_SynchronousFree;
			if (synchronousFree)
			{
				Utility.SyncRenderThread();
			}
			for (LinkedListNode<UIRenderDevice.DeviceToFree> first = UIRenderDevice.m_DeviceFreeQueue.First; first != null; first = UIRenderDevice.m_DeviceFreeQueue.First)
			{
				bool flag = !Utility.CPUFencePassed(first.Value.handle);
				if (flag)
				{
					break;
				}
				first.Value.Dispose();
				UIRenderDevice.m_DeviceFreeQueue.RemoveFirst();
			}
			Debug.Assert(!UIRenderDevice.m_SynchronousFree || UIRenderDevice.m_DeviceFreeQueue.Count == 0);
			bool flag2 = UIRenderDevice.m_ActiveDeviceCount == 0 && UIRenderDevice.m_SubscribedToNotifications;
			if (flag2)
			{
				bool flag3 = UIRenderDevice.s_WhiteTexel != null;
				if (flag3)
				{
					UIRUtility.Destroy(UIRenderDevice.s_WhiteTexel);
					UIRenderDevice.s_WhiteTexel = null;
				}
				bool flag4 = UIRenderDevice.s_DefaultShaderInfoTexFloat != null;
				if (flag4)
				{
					UIRUtility.Destroy(UIRenderDevice.s_DefaultShaderInfoTexFloat);
					UIRenderDevice.s_DefaultShaderInfoTexFloat = null;
				}
				bool flag5 = UIRenderDevice.s_DefaultShaderInfoTexARGB8 != null;
				if (flag5)
				{
					UIRUtility.Destroy(UIRenderDevice.s_DefaultShaderInfoTexARGB8);
					UIRenderDevice.s_DefaultShaderInfoTexARGB8 = null;
				}
				Utility.NotifyOfUIREvents(false);
				UIRenderDevice.m_SubscribedToNotifications = false;
			}
			UIRenderDevice.s_MarkerFree.End();
		}

		private static void OnEngineUpdateGlobal()
		{
			UIRenderDevice.ProcessDeviceFreeQueue();
		}

		private static void OnFlushPendingResources()
		{
			UIRenderDevice.m_SynchronousFree = true;
			UIRenderDevice.ProcessDeviceFreeQueue();
		}
	}
}
