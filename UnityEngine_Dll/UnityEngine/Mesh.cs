using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/Mesh/MeshScriptBindings.h"), RequiredByNativeCode]
	public sealed class Mesh : Object
	{
		private enum SafetyHandleIndex
		{
			BonesPerVertexArray,
			BonesWeightsArray
		}

		[NativeHeader("Runtime/Graphics/Mesh/MeshScriptBindings.h"), StaticAccessor("MeshDataBindings", StaticAccessorType.DoubleColon)]
		public struct MeshData
		{
			[NativeDisableUnsafePtrRestriction]
			internal IntPtr m_Ptr;

			internal AtomicSafetyHandle m_Safety;

			public int vertexCount
			{
				get
				{
					this.CheckReadAccess();
					return Mesh.MeshData.GetVertexCount(this.m_Ptr);
				}
			}

			public int vertexBufferCount
			{
				get
				{
					this.CheckReadAccess();
					return Mesh.MeshData.GetVertexBufferCount(this.m_Ptr);
				}
			}

			public IndexFormat indexFormat
			{
				get
				{
					this.CheckReadAccess();
					return Mesh.MeshData.GetIndexFormat(this.m_Ptr);
				}
			}

			public int subMeshCount
			{
				get
				{
					this.CheckReadAccess();
					return Mesh.MeshData.GetSubMeshCount(this.m_Ptr);
				}
				set
				{
					this.CheckWriteAccess();
					Mesh.MeshData.SetSubMeshCount(this.m_Ptr, value);
				}
			}

			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool HasVertexAttribute(IntPtr self, VertexAttribute attr);

			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetVertexAttributeDimension(IntPtr self, VertexAttribute attr);

			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern VertexAttributeFormat GetVertexAttributeFormat(IntPtr self, VertexAttribute attr);

			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetVertexCount(IntPtr self);

			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetVertexBufferCount(IntPtr self);

			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern IntPtr GetVertexDataPtr(IntPtr self, int stream);

			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ulong GetVertexDataSize(IntPtr self, int stream);

			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void CopyAttributeIntoPtr(IntPtr self, VertexAttribute attr, VertexAttributeFormat format, int dim, IntPtr dst);

			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void CopyIndicesIntoPtr(IntPtr self, int submesh, bool applyBaseVertex, int dstStride, IntPtr dst);

			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern IndexFormat GetIndexFormat(IntPtr self);

			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetIndexCount(IntPtr self, int submesh);

			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern IntPtr GetIndexDataPtr(IntPtr self);

			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ulong GetIndexDataSize(IntPtr self);

			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetSubMeshCount(IntPtr self);

			[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
			private static SubMeshDescriptor GetSubMesh(IntPtr self, int index)
			{
				SubMeshDescriptor result;
				Mesh.MeshData.GetSubMesh_Injected(self, index, out result);
				return result;
			}

			[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetVertexBufferParamsFromPtr(IntPtr self, int vertexCount, IntPtr attributesPtr, int attributesCount);

			[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetVertexBufferParamsFromArray(IntPtr self, int vertexCount, params VertexAttributeDescriptor[] attributes);

			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetIndexBufferParamsImpl(IntPtr self, int indexCount, IndexFormat indexFormat);

			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetSubMeshCount(IntPtr self, int count);

			[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
			private static void SetSubMeshImpl(IntPtr self, int index, SubMeshDescriptor desc, MeshUpdateFlags flags)
			{
				Mesh.MeshData.SetSubMeshImpl_Injected(self, index, ref desc, flags);
			}

			public bool HasVertexAttribute(VertexAttribute attr)
			{
				this.CheckReadAccess();
				return Mesh.MeshData.HasVertexAttribute(this.m_Ptr, attr);
			}

			public int GetVertexAttributeDimension(VertexAttribute attr)
			{
				this.CheckReadAccess();
				return Mesh.MeshData.GetVertexAttributeDimension(this.m_Ptr, attr);
			}

			public VertexAttributeFormat GetVertexAttributeFormat(VertexAttribute attr)
			{
				this.CheckReadAccess();
				return Mesh.MeshData.GetVertexAttributeFormat(this.m_Ptr, attr);
			}

			public void GetVertices(NativeArray<Vector3> outVertices)
			{
				this.CopyAttributeInto<Vector3>(outVertices, VertexAttribute.Position, VertexAttributeFormat.Float32, 3);
			}

			public void GetNormals(NativeArray<Vector3> outNormals)
			{
				this.CopyAttributeInto<Vector3>(outNormals, VertexAttribute.Normal, VertexAttributeFormat.Float32, 3);
			}

			public void GetTangents(NativeArray<Vector4> outTangents)
			{
				this.CopyAttributeInto<Vector4>(outTangents, VertexAttribute.Tangent, VertexAttributeFormat.Float32, 4);
			}

			public void GetColors(NativeArray<Color> outColors)
			{
				this.CopyAttributeInto<Color>(outColors, VertexAttribute.Color, VertexAttributeFormat.Float32, 4);
			}

			public void GetColors(NativeArray<Color32> outColors)
			{
				this.CopyAttributeInto<Color32>(outColors, VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4);
			}

			public void GetUVs(int channel, NativeArray<Vector2> outUVs)
			{
				bool flag = channel < 0 || channel > 7;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("channel", channel, "The uv index is invalid. Must be in the range 0 to 7.");
				}
				this.CopyAttributeInto<Vector2>(outUVs, Mesh.GetUVChannel(channel), VertexAttributeFormat.Float32, 2);
			}

			public void GetUVs(int channel, NativeArray<Vector3> outUVs)
			{
				bool flag = channel < 0 || channel > 7;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("channel", channel, "The uv index is invalid. Must be in the range 0 to 7.");
				}
				this.CopyAttributeInto<Vector3>(outUVs, Mesh.GetUVChannel(channel), VertexAttributeFormat.Float32, 3);
			}

			public void GetUVs(int channel, NativeArray<Vector4> outUVs)
			{
				bool flag = channel < 0 || channel > 7;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("channel", channel, "The uv index is invalid. Must be in the range 0 to 7.");
				}
				this.CopyAttributeInto<Vector4>(outUVs, Mesh.GetUVChannel(channel), VertexAttributeFormat.Float32, 4);
			}

			public unsafe NativeArray<T> GetVertexData<T>([UnityEngine.Internal.DefaultValue("0")] int stream = 0) where T : struct
			{
				this.CheckReadAccess();
				bool flag = stream < 0 || stream >= this.vertexBufferCount;
				if (flag)
				{
					throw new ArgumentOutOfRangeException(string.Format("{0} out of bounds, should be below {1} but was {2}", "stream", this.vertexBufferCount, stream));
				}
				ulong vertexDataSize = Mesh.MeshData.GetVertexDataSize(this.m_Ptr, stream);
				ulong num = (ulong)((long)UnsafeUtility.SizeOf<T>());
				bool flag2 = vertexDataSize % num > 0uL;
				if (flag2)
				{
					throw new ArgumentException(string.Format("Type passed to {0} can't capture the vertex buffer. Mesh vertex buffer size is {1} which is not a multiple of type size {2}", "GetVertexData", vertexDataSize, num));
				}
				ulong num2 = vertexDataSize / num;
				NativeArray<T> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)Mesh.MeshData.GetVertexDataPtr(this.m_Ptr, stream), (int)num2, Allocator.None);
				NativeArrayUnsafeUtility.SetAtomicSafetyHandle<T>(ref result, this.m_Safety);
				return result;
			}

			private void CopyAttributeInto<T>(NativeArray<T> buffer, VertexAttribute channel, VertexAttributeFormat format, int dim) where T : struct
			{
				this.CheckReadAccess();
				bool flag = !this.HasVertexAttribute(channel);
				if (flag)
				{
					throw new InvalidOperationException(string.Format("Mesh data does not have {0} vertex component", channel));
				}
				bool flag2 = buffer.Length < this.vertexCount;
				if (flag2)
				{
					throw new InvalidOperationException(string.Format("Not enough space in output buffer (need {0}, has {1})", this.vertexCount, buffer.Length));
				}
				Mesh.MeshData.CopyAttributeIntoPtr(this.m_Ptr, channel, format, dim, (IntPtr)buffer.GetUnsafePtr<T>());
			}

			public void SetVertexBufferParams(int vertexCount, params VertexAttributeDescriptor[] attributes)
			{
				this.CheckWriteAccess();
				Mesh.MeshData.SetVertexBufferParamsFromArray(this.m_Ptr, vertexCount, attributes);
			}

			public void SetVertexBufferParams(int vertexCount, NativeArray<VertexAttributeDescriptor> attributes)
			{
				this.CheckWriteAccess();
				Mesh.MeshData.SetVertexBufferParamsFromPtr(this.m_Ptr, vertexCount, (IntPtr)attributes.GetUnsafeReadOnlyPtr<VertexAttributeDescriptor>(), attributes.Length);
			}

			public void SetIndexBufferParams(int indexCount, IndexFormat format)
			{
				this.CheckWriteAccess();
				Mesh.MeshData.SetIndexBufferParamsImpl(this.m_Ptr, indexCount, format);
			}

			public void GetIndices(NativeArray<ushort> outIndices, int submesh, [UnityEngine.Internal.DefaultValue("true")] bool applyBaseVertex = true)
			{
				this.CheckReadAccess();
				bool flag = submesh < 0 || submesh >= this.subMeshCount;
				if (flag)
				{
					throw new IndexOutOfRangeException(string.Format("Specified submesh ({0}) is out of range. Must be greater or equal to 0 and less than subMeshCount ({1}).", submesh, this.subMeshCount));
				}
				int indexCount = Mesh.MeshData.GetIndexCount(this.m_Ptr, submesh);
				bool flag2 = outIndices.Length < indexCount;
				if (flag2)
				{
					throw new InvalidOperationException(string.Format("Not enough space in output buffer (need {0}, has {1})", indexCount, outIndices.Length));
				}
				Mesh.MeshData.CopyIndicesIntoPtr(this.m_Ptr, submesh, applyBaseVertex, 2, (IntPtr)outIndices.GetUnsafePtr<ushort>());
			}

			public void GetIndices(NativeArray<int> outIndices, int submesh, [UnityEngine.Internal.DefaultValue("true")] bool applyBaseVertex = true)
			{
				this.CheckReadAccess();
				bool flag = submesh < 0 || submesh >= this.subMeshCount;
				if (flag)
				{
					throw new IndexOutOfRangeException(string.Format("Specified submesh ({0}) is out of range. Must be greater or equal to 0 and less than subMeshCount ({1}).", submesh, this.subMeshCount));
				}
				int indexCount = Mesh.MeshData.GetIndexCount(this.m_Ptr, submesh);
				bool flag2 = outIndices.Length < indexCount;
				if (flag2)
				{
					throw new InvalidOperationException(string.Format("Not enough space in output buffer (need {0}, has {1})", indexCount, outIndices.Length));
				}
				Mesh.MeshData.CopyIndicesIntoPtr(this.m_Ptr, submesh, applyBaseVertex, 4, (IntPtr)outIndices.GetUnsafePtr<int>());
			}

			public unsafe NativeArray<T> GetIndexData<T>() where T : struct
			{
				this.CheckReadAccess();
				ulong indexDataSize = Mesh.MeshData.GetIndexDataSize(this.m_Ptr);
				ulong num = (ulong)((long)UnsafeUtility.SizeOf<T>());
				bool flag = indexDataSize % num > 0uL;
				if (flag)
				{
					throw new ArgumentException(string.Format("Type passed to {0} can't capture the index buffer. Mesh index buffer size is {1} which is not a multiple of type size {2}", "GetIndexData", indexDataSize, num));
				}
				ulong num2 = indexDataSize / num;
				NativeArray<T> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)Mesh.MeshData.GetIndexDataPtr(this.m_Ptr), (int)num2, Allocator.None);
				NativeArrayUnsafeUtility.SetAtomicSafetyHandle<T>(ref result, this.m_Safety);
				return result;
			}

			public SubMeshDescriptor GetSubMesh(int index)
			{
				this.CheckReadAccess();
				return Mesh.MeshData.GetSubMesh(this.m_Ptr, index);
			}

			public void SetSubMesh(int index, SubMeshDescriptor desc, MeshUpdateFlags flags = MeshUpdateFlags.Default)
			{
				this.CheckWriteAccess();
				Mesh.MeshData.SetSubMeshImpl(this.m_Ptr, index, desc, flags);
			}

			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private void CheckReadAccess()
			{
				AtomicSafetyHandle.CheckReadAndThrow(this.m_Safety);
			}

			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private void CheckWriteAccess()
			{
				AtomicSafetyHandle.CheckWriteAndThrow(this.m_Safety);
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetSubMesh_Injected(IntPtr self, int index, out SubMeshDescriptor ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetSubMeshImpl_Injected(IntPtr self, int index, ref SubMeshDescriptor desc, MeshUpdateFlags flags);
		}

		[NativeContainer, NativeContainerSupportsMinMaxWriteRestriction, StaticAccessor("MeshDataArrayBindings", StaticAccessorType.DoubleColon)]
		public struct MeshDataArray : IDisposable
		{
			[NativeDisableUnsafePtrRestriction]
			private unsafe IntPtr* m_Ptrs;

			internal int m_Length;

			internal int m_MinIndex;

			internal int m_MaxIndex;

			private AtomicSafetyHandle m_Safety;

			[NativeSetClassTypeToNullOnSchedule]
			private DisposeSentinel m_DisposeSentinel;

			public int Length
			{
				get
				{
					return this.m_Length;
				}
			}

			public unsafe Mesh.MeshData this[int index]
			{
				get
				{
					this.CheckElementReadAccess(index);
					Mesh.MeshData result;
					result.m_Ptr = this.m_Ptrs[(IntPtr)index * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)];
					result.m_Safety = this.m_Safety;
					return result;
				}
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			private unsafe static extern void AcquireReadOnlyMeshData([NotNull("ArgumentNullException")] Mesh mesh, IntPtr* datas);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private unsafe static extern void AcquireReadOnlyMeshDatas([NotNull("ArgumentNullException")] Mesh[] meshes, IntPtr* datas, int count);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private unsafe static extern void ReleaseMeshDatas(IntPtr* datas, int count);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private unsafe static extern void CreateNewMeshDatas(IntPtr* datas, int count);

			[NativeThrows]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private unsafe static extern void ApplyToMeshesImpl([NotNull("ArgumentNullException")] Mesh[] meshes, IntPtr* datas, int count, MeshUpdateFlags flags);

			[NativeThrows]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void ApplyToMeshImpl([NotNull("ArgumentNullException")] Mesh mesh, IntPtr data, MeshUpdateFlags flags);

			public unsafe void Dispose()
			{
				DisposeSentinel.Dispose(ref this.m_Safety, ref this.m_DisposeSentinel);
				bool flag = this.m_Length != 0;
				if (flag)
				{
					Mesh.MeshDataArray.ReleaseMeshDatas(this.m_Ptrs, this.m_Length);
					UnsafeUtility.Free((void*)this.m_Ptrs, Allocator.Persistent);
				}
				this.m_Ptrs = null;
				this.m_Length = 0;
			}

			internal unsafe void ApplyToMeshAndDispose(Mesh mesh, MeshUpdateFlags flags)
			{
				bool flag = !mesh.canAccess;
				if (flag)
				{
					throw new InvalidOperationException("Not allowed to access vertex data on mesh '" + mesh.name + "' (isReadable is false; Read/Write must be enabled in import settings)");
				}
				Mesh.MeshDataArray.ApplyToMeshImpl(mesh, *this.m_Ptrs, flags);
				this.Dispose();
			}

			internal void ApplyToMeshesAndDispose(Mesh[] meshes, MeshUpdateFlags flags)
			{
				for (int i = 0; i < this.m_Length; i++)
				{
					Mesh mesh = meshes[i];
					bool flag = mesh == null;
					if (flag)
					{
						throw new ArgumentNullException("meshes", string.Format("Mesh at index {0} is null", i));
					}
					bool flag2 = !mesh.canAccess;
					if (flag2)
					{
						throw new InvalidOperationException(string.Format("Not allowed to access vertex data on mesh '{0}' at array index {1} (isReadable is false; Read/Write must be enabled in import settings)", mesh.name, i));
					}
				}
				Mesh.MeshDataArray.ApplyToMeshesImpl(meshes, this.m_Ptrs, this.m_Length, flags);
				this.Dispose();
			}

			internal unsafe MeshDataArray(Mesh mesh, bool checkReadWrite = true)
			{
				bool flag = mesh == null;
				if (flag)
				{
					throw new ArgumentNullException("mesh", "Mesh is null");
				}
				bool flag2 = checkReadWrite && !mesh.canAccess;
				if (flag2)
				{
					throw new InvalidOperationException("Not allowed to access vertex data on mesh '" + mesh.name + "' (isReadable is false; Read/Write must be enabled in import settings)");
				}
				this.m_Length = 1;
				int num = UnsafeUtility.SizeOf<IntPtr>();
				this.m_Ptrs = (IntPtr*)UnsafeUtility.Malloc((long)num, UnsafeUtility.AlignOf<IntPtr>(), Allocator.Persistent);
				Mesh.MeshDataArray.AcquireReadOnlyMeshData(mesh, this.m_Ptrs);
				this.m_MinIndex = 0;
				this.m_MaxIndex = this.m_Length - 1;
				DisposeSentinel.Create(out this.m_Safety, out this.m_DisposeSentinel, 1, Allocator.TempJob);
				AtomicSafetyHandle.SetAllowSecondaryVersionWriting(this.m_Safety, false);
				AtomicSafetyHandle.UseSecondaryVersion(ref this.m_Safety);
			}

			internal unsafe MeshDataArray(Mesh[] meshes, int meshesCount, bool checkReadWrite = true)
			{
				bool flag = meshes.Length < meshesCount;
				if (flag)
				{
					throw new InvalidOperationException(string.Format("Meshes array size ({0}) is smaller than meshes count ({1})", meshes.Length, meshesCount));
				}
				for (int i = 0; i < meshesCount; i++)
				{
					Mesh mesh = meshes[i];
					bool flag2 = mesh == null;
					if (flag2)
					{
						throw new ArgumentNullException("meshes", string.Format("Mesh at index {0} is null", i));
					}
					bool flag3 = checkReadWrite && !mesh.canAccess;
					if (flag3)
					{
						throw new InvalidOperationException(string.Format("Not allowed to access vertex data on mesh '{0}' at array index {1} (isReadable is false; Read/Write must be enabled in import settings)", mesh.name, i));
					}
				}
				this.m_Length = meshesCount;
				int num = UnsafeUtility.SizeOf<IntPtr>() * meshesCount;
				this.m_Ptrs = (IntPtr*)UnsafeUtility.Malloc((long)num, UnsafeUtility.AlignOf<IntPtr>(), Allocator.Persistent);
				Mesh.MeshDataArray.AcquireReadOnlyMeshDatas(meshes, this.m_Ptrs, meshesCount);
				this.m_MinIndex = 0;
				this.m_MaxIndex = this.m_Length - 1;
				DisposeSentinel.Create(out this.m_Safety, out this.m_DisposeSentinel, 1, Allocator.TempJob);
				AtomicSafetyHandle.SetAllowSecondaryVersionWriting(this.m_Safety, false);
				AtomicSafetyHandle.UseSecondaryVersion(ref this.m_Safety);
			}

			internal unsafe MeshDataArray(int meshesCount)
			{
				bool flag = meshesCount < 0;
				if (flag)
				{
					throw new InvalidOperationException(string.Format("Mesh count can not be negative (was {0})", meshesCount));
				}
				this.m_Length = meshesCount;
				int num = UnsafeUtility.SizeOf<IntPtr>() * meshesCount;
				this.m_Ptrs = (IntPtr*)UnsafeUtility.Malloc((long)num, UnsafeUtility.AlignOf<IntPtr>(), Allocator.Persistent);
				Mesh.MeshDataArray.CreateNewMeshDatas(this.m_Ptrs, meshesCount);
				this.m_MinIndex = 0;
				this.m_MaxIndex = this.m_Length - 1;
				DisposeSentinel.Create(out this.m_Safety, out this.m_DisposeSentinel, 1, Allocator.TempJob);
			}

			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private void CheckElementReadAccess(int index)
			{
				bool flag = index < this.m_MinIndex || index > this.m_MaxIndex;
				if (flag)
				{
					this.FailOutOfRangeError(index);
				}
				AtomicSafetyHandle.CheckReadAndThrow(this.m_Safety);
			}

			[BurstDiscard]
			private void FailOutOfRangeError(int index)
			{
				bool flag = index < this.Length && (this.m_MinIndex != 0 || this.m_MaxIndex != this.Length - 1);
				if (flag)
				{
					throw new IndexOutOfRangeException(string.Format("Index {0} is out of restricted IJobParallelFor range [{1}...{2}] in {3}.\n", new object[]
					{
						index,
						this.m_MinIndex,
						this.m_MaxIndex,
						"MeshDataArray"
					}) + "You can only access the element at the job index, unless [NativeDisableParallelForRestriction] is used on the variable.");
				}
				throw new IndexOutOfRangeException(string.Format("Index {0} is out of range of '{1}' Length.", index, this.Length));
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Property Mesh.uv1 has been deprecated. Use Mesh.uv2 instead (UnityUpgradable) -> uv2", true)]
		public Vector2[] uv1
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public extern IndexFormat indexFormat
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int vertexBufferCount
		{
			[FreeFunction(Name = "MeshScripting::GetVertexBufferCount", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int blendShapeCount
		{
			[NativeMethod(Name = "GetBlendShapeChannelCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeName("BindPosesFromScript")]
		public extern Matrix4x4[] bindposes
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool isReadable
		{
			[NativeMethod("GetIsReadable")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal extern bool canAccess
		{
			[NativeMethod("CanAccessFromScript")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int vertexCount
		{
			[NativeMethod("GetVertexCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int subMeshCount
		{
			[NativeMethod(Name = "GetSubMeshCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction(Name = "MeshScripting::SetSubMeshCount", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Bounds bounds
		{
			get
			{
				Bounds result;
				this.get_bounds_Injected(out result);
				return result;
			}
			set
			{
				this.set_bounds_Injected(ref value);
			}
		}

		public Vector3[] vertices
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector3>(VertexAttribute.Position);
			}
			set
			{
				this.SetArrayForChannel<Vector3>(VertexAttribute.Position, value, MeshUpdateFlags.Default);
			}
		}

		public Vector3[] normals
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector3>(VertexAttribute.Normal);
			}
			set
			{
				this.SetArrayForChannel<Vector3>(VertexAttribute.Normal, value, MeshUpdateFlags.Default);
			}
		}

		public Vector4[] tangents
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector4>(VertexAttribute.Tangent);
			}
			set
			{
				this.SetArrayForChannel<Vector4>(VertexAttribute.Tangent, value, MeshUpdateFlags.Default);
			}
		}

		public Vector2[] uv
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord0);
			}
			set
			{
				this.SetArrayForChannel<Vector2>(VertexAttribute.TexCoord0, value, MeshUpdateFlags.Default);
			}
		}

		public Vector2[] uv2
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord1);
			}
			set
			{
				this.SetArrayForChannel<Vector2>(VertexAttribute.TexCoord1, value, MeshUpdateFlags.Default);
			}
		}

		public Vector2[] uv3
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord2);
			}
			set
			{
				this.SetArrayForChannel<Vector2>(VertexAttribute.TexCoord2, value, MeshUpdateFlags.Default);
			}
		}

		public Vector2[] uv4
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord3);
			}
			set
			{
				this.SetArrayForChannel<Vector2>(VertexAttribute.TexCoord3, value, MeshUpdateFlags.Default);
			}
		}

		public Vector2[] uv5
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord4);
			}
			set
			{
				this.SetArrayForChannel<Vector2>(VertexAttribute.TexCoord4, value, MeshUpdateFlags.Default);
			}
		}

		public Vector2[] uv6
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord5);
			}
			set
			{
				this.SetArrayForChannel<Vector2>(VertexAttribute.TexCoord5, value, MeshUpdateFlags.Default);
			}
		}

		public Vector2[] uv7
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord6);
			}
			set
			{
				this.SetArrayForChannel<Vector2>(VertexAttribute.TexCoord6, value, MeshUpdateFlags.Default);
			}
		}

		public Vector2[] uv8
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord7);
			}
			set
			{
				this.SetArrayForChannel<Vector2>(VertexAttribute.TexCoord7, value, MeshUpdateFlags.Default);
			}
		}

		public Color[] colors
		{
			get
			{
				return this.GetAllocArrayFromChannel<Color>(VertexAttribute.Color);
			}
			set
			{
				this.SetArrayForChannel<Color>(VertexAttribute.Color, value, MeshUpdateFlags.Default);
			}
		}

		public Color32[] colors32
		{
			get
			{
				return this.GetAllocArrayFromChannel<Color32>(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4);
			}
			set
			{
				this.SetArrayForChannel<Color32>(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4, value, MeshUpdateFlags.Default);
			}
		}

		public int vertexAttributeCount
		{
			get
			{
				return this.GetVertexAttributeCountImpl();
			}
		}

		public int[] triangles
		{
			get
			{
				bool canAccess = this.canAccess;
				int[] result;
				if (canAccess)
				{
					result = this.GetTrianglesImpl(-1, true);
				}
				else
				{
					this.PrintErrorCantAccessIndices();
					result = new int[0];
				}
				return result;
			}
			set
			{
				bool canAccess = this.canAccess;
				if (canAccess)
				{
					this.SetTrianglesImpl(-1, IndexFormat.UInt32, value, NoAllocHelpers.SafeLength(value), 0, NoAllocHelpers.SafeLength(value), true, 0);
				}
				else
				{
					this.PrintErrorCantAccessIndices();
				}
			}
		}

		public BoneWeight[] boneWeights
		{
			get
			{
				return this.GetBoneWeightsImpl();
			}
			set
			{
				this.SetBoneWeightsImpl(value);
			}
		}

		[FreeFunction("MeshScripting::CreateMesh")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] Mesh mono);

		[RequiredByNativeCode]
		public Mesh()
		{
			Mesh.Internal_Create(this);
		}

		[FreeFunction("MeshScripting::MeshFromInstanceId")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Mesh FromInstanceID(int id);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern uint GetTotalIndexCount();

		[FreeFunction(Name = "MeshScripting::SetIndexBufferParams", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetIndexBufferParams(int indexCount, IndexFormat format);

		[FreeFunction(Name = "MeshScripting::InternalSetIndexBufferData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalSetIndexBufferData(IntPtr data, int dataStart, int meshBufferStart, int count, int elemSize, MeshUpdateFlags flags);

		[FreeFunction(Name = "MeshScripting::InternalSetIndexBufferDataFromArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalSetIndexBufferDataFromArray(Array data, int dataStart, int meshBufferStart, int count, int elemSize, MeshUpdateFlags flags);

		[FreeFunction(Name = "MeshScripting::SetVertexBufferParamsFromPtr", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVertexBufferParamsFromPtr(int vertexCount, IntPtr attributesPtr, int attributesCount);

		[FreeFunction(Name = "MeshScripting::SetVertexBufferParamsFromArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVertexBufferParamsFromArray(int vertexCount, params VertexAttributeDescriptor[] attributes);

		[FreeFunction(Name = "MeshScripting::InternalSetVertexBufferData", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalSetVertexBufferData(int stream, IntPtr data, int dataStart, int meshBufferStart, int count, int elemSize, MeshUpdateFlags flags);

		[FreeFunction(Name = "MeshScripting::InternalSetVertexBufferDataFromArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalSetVertexBufferDataFromArray(int stream, Array data, int dataStart, int meshBufferStart, int count, int elemSize, MeshUpdateFlags flags);

		[FreeFunction(Name = "MeshScripting::GetVertexAttributesAlloc", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Array GetVertexAttributesAlloc();

		[FreeFunction(Name = "MeshScripting::GetVertexAttributesArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetVertexAttributesArray([NotNull("ArgumentNullException")] VertexAttributeDescriptor[] attributes);

		[FreeFunction(Name = "MeshScripting::GetVertexAttributesList", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetVertexAttributesList([NotNull("ArgumentNullException")] List<VertexAttributeDescriptor> attributes);

		[FreeFunction(Name = "MeshScripting::GetVertexAttributesCount", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetVertexAttributeCountImpl();

		[FreeFunction(Name = "MeshScripting::GetVertexAttributeByIndex", HasExplicitThis = true, ThrowsException = true)]
		public VertexAttributeDescriptor GetVertexAttribute(int index)
		{
			VertexAttributeDescriptor result;
			this.GetVertexAttribute_Injected(index, out result);
			return result;
		}

		[FreeFunction(Name = "MeshScripting::GetIndexStart", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern uint GetIndexStartImpl(int submesh);

		[FreeFunction(Name = "MeshScripting::GetIndexCount", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern uint GetIndexCountImpl(int submesh);

		[FreeFunction(Name = "MeshScripting::GetTrianglesCount", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern uint GetTrianglesCountImpl(int submesh);

		[FreeFunction(Name = "MeshScripting::GetBaseVertex", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern uint GetBaseVertexImpl(int submesh);

		[FreeFunction(Name = "MeshScripting::GetTriangles", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int[] GetTrianglesImpl(int submesh, bool applyBaseVertex);

		[FreeFunction(Name = "MeshScripting::GetIndices", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int[] GetIndicesImpl(int submesh, bool applyBaseVertex);

		[FreeFunction(Name = "SetMeshIndicesFromScript", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetIndicesImpl(int submesh, MeshTopology topology, IndexFormat indicesFormat, Array indices, int arrayStart, int arraySize, bool calculateBounds, int baseVertex);

		[FreeFunction(Name = "SetMeshIndicesFromNativeArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetIndicesNativeArrayImpl(int submesh, MeshTopology topology, IndexFormat indicesFormat, IntPtr indices, int arrayStart, int arraySize, bool calculateBounds, int baseVertex);

		[FreeFunction(Name = "MeshScripting::ExtractTrianglesToArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetTrianglesNonAllocImpl([Out] int[] values, int submesh, bool applyBaseVertex);

		[FreeFunction(Name = "MeshScripting::ExtractTrianglesToArray16", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetTrianglesNonAllocImpl16([Out] ushort[] values, int submesh, bool applyBaseVertex);

		[FreeFunction(Name = "MeshScripting::ExtractIndicesToArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetIndicesNonAllocImpl([Out] int[] values, int submesh, bool applyBaseVertex);

		[FreeFunction(Name = "MeshScripting::ExtractIndicesToArray16", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetIndicesNonAllocImpl16([Out] ushort[] values, int submesh, bool applyBaseVertex);

		[FreeFunction(Name = "MeshScripting::PrintErrorCantAccessChannel", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void PrintErrorCantAccessChannel(VertexAttribute ch);

		[FreeFunction(Name = "MeshScripting::HasChannel", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasVertexAttribute(VertexAttribute attr);

		[FreeFunction(Name = "MeshScripting::GetChannelDimension", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetVertexAttributeDimension(VertexAttribute attr);

		[FreeFunction(Name = "MeshScripting::GetChannelFormat", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern VertexAttributeFormat GetVertexAttributeFormat(VertexAttribute attr);

		[FreeFunction(Name = "SetMeshComponentFromArrayFromScript", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetArrayForChannelImpl(VertexAttribute channel, VertexAttributeFormat format, int dim, Array values, int arraySize, int valuesStart, int valuesCount, MeshUpdateFlags flags);

		[FreeFunction(Name = "SetMeshComponentFromNativeArrayFromScript", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetNativeArrayForChannelImpl(VertexAttribute channel, VertexAttributeFormat format, int dim, IntPtr values, int arraySize, int valuesStart, int valuesCount, MeshUpdateFlags flags);

		[FreeFunction(Name = "AllocExtractMeshComponentFromScript", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Array GetAllocArrayFromChannelImpl(VertexAttribute channel, VertexAttributeFormat format, int dim);

		[FreeFunction(Name = "ExtractMeshComponentFromScript", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetArrayFromChannelImpl(VertexAttribute channel, VertexAttributeFormat format, int dim, Array values);

		[FreeFunction(Name = "MeshScripting::GetNativeVertexBufferPtr", HasExplicitThis = true), NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern IntPtr GetNativeVertexBufferPtr(int index);

		[FreeFunction(Name = "MeshScripting::GetNativeIndexBufferPtr", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern IntPtr GetNativeIndexBufferPtr();

		[FreeFunction(Name = "MeshScripting::ClearBlendShapes", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ClearBlendShapes();

		[FreeFunction(Name = "MeshScripting::GetBlendShapeName", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetBlendShapeName(int shapeIndex);

		[FreeFunction(Name = "MeshScripting::GetBlendShapeIndex", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetBlendShapeIndex(string blendShapeName);

		[FreeFunction(Name = "MeshScripting::GetBlendShapeFrameCount", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetBlendShapeFrameCount(int shapeIndex);

		[FreeFunction(Name = "MeshScripting::GetBlendShapeFrameWeight", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetBlendShapeFrameWeight(int shapeIndex, int frameIndex);

		[FreeFunction(Name = "GetBlendShapeFrameVerticesFromScript", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetBlendShapeFrameVertices(int shapeIndex, int frameIndex, Vector3[] deltaVertices, Vector3[] deltaNormals, Vector3[] deltaTangents);

		[FreeFunction(Name = "AddBlendShapeFrameFromScript", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddBlendShapeFrame(string shapeName, float frameWeight, Vector3[] deltaVertices, Vector3[] deltaNormals, Vector3[] deltaTangents);

		[NativeMethod("HasBoneWeights")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool HasBoneWeights();

		[FreeFunction(Name = "MeshScripting::GetBoneWeights", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern BoneWeight[] GetBoneWeightsImpl();

		[FreeFunction(Name = "MeshScripting::SetBoneWeights", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBoneWeightsImpl(BoneWeight[] weights);

		public void SetBoneWeights(NativeArray<byte> bonesPerVertex, NativeArray<BoneWeight1> weights)
		{
			this.InternalSetBoneWeights((IntPtr)bonesPerVertex.GetUnsafeReadOnlyPtr<byte>(), bonesPerVertex.Length, (IntPtr)weights.GetUnsafeReadOnlyPtr<BoneWeight1>(), weights.Length);
		}

		[SecurityCritical, FreeFunction(Name = "MeshScripting::SetBoneWeights", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalSetBoneWeights(IntPtr bonesPerVertex, int bonesPerVertexSize, IntPtr weights, int weightsSize);

		public unsafe NativeArray<BoneWeight1> GetAllBoneWeights()
		{
			NativeArray<BoneWeight1> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<BoneWeight1>((void*)this.GetAllBoneWeightsArray(), this.GetAllBoneWeightsArraySize(), Allocator.None);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<BoneWeight1>(ref result, this.GetReadOnlySafetyHandle(Mesh.SafetyHandleIndex.BonesWeightsArray));
			return result;
		}

		public unsafe NativeArray<byte> GetBonesPerVertex()
		{
			int length = this.HasBoneWeights() ? this.vertexCount : 0;
			NativeArray<byte> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>((void*)this.GetBonesPerVertexArray(), length, Allocator.None);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<byte>(ref result, this.GetReadOnlySafetyHandle(Mesh.SafetyHandleIndex.BonesPerVertexArray));
			return result;
		}

		[FreeFunction(Name = "MeshScripting::GetAllBoneWeightsArraySize", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetAllBoneWeightsArraySize();

		[SecurityCritical, FreeFunction(Name = "MeshScripting::GetAllBoneWeightsArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetAllBoneWeightsArray();

		[SecurityCritical, FreeFunction(Name = "MeshScripting::GetBonesPerVertexArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetBonesPerVertexArray();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetBindposeCount();

		[FreeFunction(Name = "MeshScripting::ExtractBoneWeightsIntoArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetBoneWeightsNonAllocImpl([Out] BoneWeight[] values);

		[FreeFunction(Name = "MeshScripting::ExtractBindPosesIntoArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetBindposesNonAllocImpl([Out] Matrix4x4[] values);

		[FreeFunction(Name = "MeshScripting::GetReadOnlySafetyHandle", HasExplicitThis = true)]
		private AtomicSafetyHandle GetReadOnlySafetyHandle(Mesh.SafetyHandleIndex index)
		{
			AtomicSafetyHandle result;
			this.GetReadOnlySafetyHandle_Injected(index, out result);
			return result;
		}

		[FreeFunction("MeshScripting::SetSubMesh", HasExplicitThis = true, ThrowsException = true)]
		public void SetSubMesh(int index, SubMeshDescriptor desc, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			this.SetSubMesh_Injected(index, ref desc, flags);
		}

		[FreeFunction("MeshScripting::GetSubMesh", HasExplicitThis = true, ThrowsException = true)]
		public SubMeshDescriptor GetSubMesh(int index)
		{
			SubMeshDescriptor result;
			this.GetSubMesh_Injected(index, out result);
			return result;
		}

		[FreeFunction("MeshScripting::SetAllSubMeshesAtOnceFromArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetAllSubMeshesAtOnceFromArray(SubMeshDescriptor[] desc, int start, int count, MeshUpdateFlags flags = MeshUpdateFlags.Default);

		[FreeFunction("MeshScripting::SetAllSubMeshesAtOnceFromNativeArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetAllSubMeshesAtOnceFromNativeArray(IntPtr desc, int start, int count, MeshUpdateFlags flags = MeshUpdateFlags.Default);

		[NativeMethod("Clear")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ClearImpl(bool keepVertexLayout);

		[NativeMethod("RecalculateBounds")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RecalculateBoundsImpl(MeshUpdateFlags flags);

		[NativeMethod("RecalculateNormals")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RecalculateNormalsImpl(MeshUpdateFlags flags);

		[NativeMethod("RecalculateTangents")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RecalculateTangentsImpl(MeshUpdateFlags flags);

		[NativeMethod("MarkDynamic")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void MarkDynamicImpl();

		[NativeMethod("MarkModified")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void MarkModified();

		[NativeMethod("UploadMeshData")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void UploadMeshDataImpl(bool markNoLongerReadable);

		[FreeFunction(Name = "MeshScripting::GetPrimitiveType", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern MeshTopology GetTopologyImpl(int submesh);

		[NativeMethod("RecalculateMeshMetric")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RecalculateUVDistributionMetricImpl(int uvSetIndex, float uvAreaThreshold);

		[NativeMethod("RecalculateMeshMetrics")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RecalculateUVDistributionMetricsImpl(float uvAreaThreshold);

		[NativeMethod("GetMeshMetric")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetUVDistributionMetric(int uvSetIndex);

		[NativeMethod(Name = "MeshScripting::CombineMeshes", IsFreeFunction = true, ThrowsException = true, HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void CombineMeshesImpl(CombineInstance[] combine, bool mergeSubMeshes, bool useMatrices, bool hasLightmapData);

		[NativeMethod("Optimize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void OptimizeImpl();

		[NativeMethod("OptimizeIndexBuffers")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void OptimizeIndexBuffersImpl();

		[NativeMethod("OptimizeReorderVertexBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void OptimizeReorderVertexBufferImpl();

		internal static VertexAttribute GetUVChannel(int uvIndex)
		{
			bool flag = uvIndex < 0 || uvIndex > 7;
			if (flag)
			{
				throw new ArgumentException("GetUVChannel called for bad uvIndex", "uvIndex");
			}
			return VertexAttribute.TexCoord0 + uvIndex;
		}

		internal static int DefaultDimensionForChannel(VertexAttribute channel)
		{
			bool flag = channel == VertexAttribute.Position || channel == VertexAttribute.Normal;
			int result;
			if (flag)
			{
				result = 3;
			}
			else
			{
				bool flag2 = channel >= VertexAttribute.TexCoord0 && channel <= VertexAttribute.TexCoord7;
				if (flag2)
				{
					result = 2;
				}
				else
				{
					bool flag3 = channel == VertexAttribute.Tangent || channel == VertexAttribute.Color;
					if (!flag3)
					{
						throw new ArgumentException("DefaultDimensionForChannel called for bad channel", "channel");
					}
					result = 4;
				}
			}
			return result;
		}

		private T[] GetAllocArrayFromChannel<T>(VertexAttribute channel, VertexAttributeFormat format, int dim)
		{
			bool canAccess = this.canAccess;
			T[] result;
			if (canAccess)
			{
				bool flag = this.HasVertexAttribute(channel);
				if (flag)
				{
					result = (T[])this.GetAllocArrayFromChannelImpl(channel, format, dim);
					return result;
				}
			}
			else
			{
				this.PrintErrorCantAccessChannel(channel);
			}
			result = new T[0];
			return result;
		}

		private T[] GetAllocArrayFromChannel<T>(VertexAttribute channel)
		{
			return this.GetAllocArrayFromChannel<T>(channel, VertexAttributeFormat.Float32, Mesh.DefaultDimensionForChannel(channel));
		}

		private void SetSizedArrayForChannel(VertexAttribute channel, VertexAttributeFormat format, int dim, Array values, int valuesArrayLength, int valuesStart, int valuesCount, MeshUpdateFlags flags)
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				bool flag = valuesStart < 0;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("valuesStart", valuesStart, "Mesh data array start index can't be negative.");
				}
				bool flag2 = valuesCount < 0;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("valuesCount", valuesCount, "Mesh data array length can't be negative.");
				}
				bool flag3 = valuesStart >= valuesArrayLength && valuesCount != 0;
				if (flag3)
				{
					throw new ArgumentOutOfRangeException("valuesStart", valuesStart, "Mesh data array start is outside of array size.");
				}
				bool flag4 = valuesStart + valuesCount > valuesArrayLength;
				if (flag4)
				{
					throw new ArgumentOutOfRangeException("valuesCount", valuesStart + valuesCount, "Mesh data array start+count is outside of array size.");
				}
				bool flag5 = values == null;
				if (flag5)
				{
					valuesStart = 0;
				}
				this.SetArrayForChannelImpl(channel, format, dim, values, valuesArrayLength, valuesStart, valuesCount, flags);
			}
			else
			{
				this.PrintErrorCantAccessChannel(channel);
			}
		}

		private void SetSizedNativeArrayForChannel(VertexAttribute channel, VertexAttributeFormat format, int dim, IntPtr values, int valuesArrayLength, int valuesStart, int valuesCount, MeshUpdateFlags flags)
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				bool flag = valuesStart < 0;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("valuesStart", valuesStart, "Mesh data array start index can't be negative.");
				}
				bool flag2 = valuesCount < 0;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("valuesCount", valuesCount, "Mesh data array length can't be negative.");
				}
				bool flag3 = valuesStart >= valuesArrayLength && valuesCount != 0;
				if (flag3)
				{
					throw new ArgumentOutOfRangeException("valuesStart", valuesStart, "Mesh data array start is outside of array size.");
				}
				bool flag4 = valuesStart + valuesCount > valuesArrayLength;
				if (flag4)
				{
					throw new ArgumentOutOfRangeException("valuesCount", valuesStart + valuesCount, "Mesh data array start+count is outside of array size.");
				}
				this.SetNativeArrayForChannelImpl(channel, format, dim, values, valuesArrayLength, valuesStart, valuesCount, flags);
			}
			else
			{
				this.PrintErrorCantAccessChannel(channel);
			}
		}

		private void SetArrayForChannel<T>(VertexAttribute channel, VertexAttributeFormat format, int dim, T[] values, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			int num = NoAllocHelpers.SafeLength(values);
			this.SetSizedArrayForChannel(channel, format, dim, values, num, 0, num, flags);
		}

		private void SetArrayForChannel<T>(VertexAttribute channel, T[] values, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			int num = NoAllocHelpers.SafeLength(values);
			this.SetSizedArrayForChannel(channel, VertexAttributeFormat.Float32, Mesh.DefaultDimensionForChannel(channel), values, num, 0, num, flags);
		}

		private void SetListForChannel<T>(VertexAttribute channel, VertexAttributeFormat format, int dim, List<T> values, int start, int length, MeshUpdateFlags flags)
		{
			this.SetSizedArrayForChannel(channel, format, dim, NoAllocHelpers.ExtractArrayFromList(values), NoAllocHelpers.SafeLength<T>(values), start, length, flags);
		}

		private void SetListForChannel<T>(VertexAttribute channel, List<T> values, int start, int length, MeshUpdateFlags flags)
		{
			this.SetSizedArrayForChannel(channel, VertexAttributeFormat.Float32, Mesh.DefaultDimensionForChannel(channel), NoAllocHelpers.ExtractArrayFromList(values), NoAllocHelpers.SafeLength<T>(values), start, length, flags);
		}

		private void GetListForChannel<T>(List<T> buffer, int capacity, VertexAttribute channel, int dim)
		{
			this.GetListForChannel<T>(buffer, capacity, channel, dim, VertexAttributeFormat.Float32);
		}

		private void GetListForChannel<T>(List<T> buffer, int capacity, VertexAttribute channel, int dim, VertexAttributeFormat channelType)
		{
			buffer.Clear();
			bool flag = !this.canAccess;
			if (flag)
			{
				this.PrintErrorCantAccessChannel(channel);
			}
			else
			{
				bool flag2 = !this.HasVertexAttribute(channel);
				if (!flag2)
				{
					NoAllocHelpers.EnsureListElemCount<T>(buffer, capacity);
					this.GetArrayFromChannelImpl(channel, channelType, dim, NoAllocHelpers.ExtractArrayFromList(buffer));
				}
			}
		}

		public void GetVertices(List<Vector3> vertices)
		{
			bool flag = vertices == null;
			if (flag)
			{
				throw new ArgumentNullException("vertices", "The result vertices list cannot be null.");
			}
			this.GetListForChannel<Vector3>(vertices, this.vertexCount, VertexAttribute.Position, Mesh.DefaultDimensionForChannel(VertexAttribute.Position));
		}

		public void SetVertices(List<Vector3> inVertices)
		{
			this.SetVertices(inVertices, 0, NoAllocHelpers.SafeLength<Vector3>(inVertices));
		}

		[ExcludeFromDocs]
		public void SetVertices(List<Vector3> inVertices, int start, int length)
		{
			this.SetVertices(inVertices, start, length, MeshUpdateFlags.Default);
		}

		public void SetVertices(List<Vector3> inVertices, int start, int length, [UnityEngine.Internal.DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetListForChannel<Vector3>(VertexAttribute.Position, inVertices, start, length, flags);
		}

		public void SetVertices(Vector3[] inVertices)
		{
			this.SetVertices(inVertices, 0, NoAllocHelpers.SafeLength(inVertices));
		}

		[ExcludeFromDocs]
		public void SetVertices(Vector3[] inVertices, int start, int length)
		{
			this.SetVertices(inVertices, start, length, MeshUpdateFlags.Default);
		}

		public void SetVertices(Vector3[] inVertices, int start, int length, [UnityEngine.Internal.DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetSizedArrayForChannel(VertexAttribute.Position, VertexAttributeFormat.Float32, Mesh.DefaultDimensionForChannel(VertexAttribute.Position), inVertices, NoAllocHelpers.SafeLength(inVertices), start, length, flags);
		}

		public void SetVertices<T>(NativeArray<T> inVertices) where T : struct
		{
			this.SetVertices<T>(inVertices, 0, inVertices.Length);
		}

		[ExcludeFromDocs]
		public void SetVertices<T>(NativeArray<T> inVertices, int start, int length) where T : struct
		{
			this.SetVertices<T>(inVertices, start, length, MeshUpdateFlags.Default);
		}

		public void SetVertices<T>(NativeArray<T> inVertices, int start, int length, [UnityEngine.Internal.DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags) where T : struct
		{
			bool flag = UnsafeUtility.SizeOf<T>() != 12;
			if (flag)
			{
				throw new ArgumentException("SetVertices with NativeArray should use struct type that is 12 bytes (3x float) in size");
			}
			this.SetSizedNativeArrayForChannel(VertexAttribute.Position, VertexAttributeFormat.Float32, 3, (IntPtr)inVertices.GetUnsafeReadOnlyPtr<T>(), inVertices.Length, start, length, flags);
		}

		public void GetNormals(List<Vector3> normals)
		{
			bool flag = normals == null;
			if (flag)
			{
				throw new ArgumentNullException("normals", "The result normals list cannot be null.");
			}
			this.GetListForChannel<Vector3>(normals, this.vertexCount, VertexAttribute.Normal, Mesh.DefaultDimensionForChannel(VertexAttribute.Normal));
		}

		public void SetNormals(List<Vector3> inNormals)
		{
			this.SetNormals(inNormals, 0, NoAllocHelpers.SafeLength<Vector3>(inNormals));
		}

		[ExcludeFromDocs]
		public void SetNormals(List<Vector3> inNormals, int start, int length)
		{
			this.SetNormals(inNormals, start, length, MeshUpdateFlags.Default);
		}

		public void SetNormals(List<Vector3> inNormals, int start, int length, [UnityEngine.Internal.DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetListForChannel<Vector3>(VertexAttribute.Normal, inNormals, start, length, flags);
		}

		public void SetNormals(Vector3[] inNormals)
		{
			this.SetNormals(inNormals, 0, NoAllocHelpers.SafeLength(inNormals));
		}

		[ExcludeFromDocs]
		public void SetNormals(Vector3[] inNormals, int start, int length)
		{
			this.SetNormals(inNormals, start, length, MeshUpdateFlags.Default);
		}

		public void SetNormals(Vector3[] inNormals, int start, int length, [UnityEngine.Internal.DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetSizedArrayForChannel(VertexAttribute.Normal, VertexAttributeFormat.Float32, Mesh.DefaultDimensionForChannel(VertexAttribute.Normal), inNormals, NoAllocHelpers.SafeLength(inNormals), start, length, flags);
		}

		public void SetNormals<T>(NativeArray<T> inNormals) where T : struct
		{
			this.SetNormals<T>(inNormals, 0, inNormals.Length);
		}

		[ExcludeFromDocs]
		public void SetNormals<T>(NativeArray<T> inNormals, int start, int length) where T : struct
		{
			this.SetNormals<T>(inNormals, start, length, MeshUpdateFlags.Default);
		}

		public void SetNormals<T>(NativeArray<T> inNormals, int start, int length, [UnityEngine.Internal.DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags) where T : struct
		{
			bool flag = UnsafeUtility.SizeOf<T>() != 12;
			if (flag)
			{
				throw new ArgumentException("SetNormals with NativeArray should use struct type that is 12 bytes (3x float) in size");
			}
			this.SetSizedNativeArrayForChannel(VertexAttribute.Normal, VertexAttributeFormat.Float32, 3, (IntPtr)inNormals.GetUnsafeReadOnlyPtr<T>(), inNormals.Length, start, length, flags);
		}

		public void GetTangents(List<Vector4> tangents)
		{
			bool flag = tangents == null;
			if (flag)
			{
				throw new ArgumentNullException("tangents", "The result tangents list cannot be null.");
			}
			this.GetListForChannel<Vector4>(tangents, this.vertexCount, VertexAttribute.Tangent, Mesh.DefaultDimensionForChannel(VertexAttribute.Tangent));
		}

		public void SetTangents(List<Vector4> inTangents)
		{
			this.SetTangents(inTangents, 0, NoAllocHelpers.SafeLength<Vector4>(inTangents));
		}

		[ExcludeFromDocs]
		public void SetTangents(List<Vector4> inTangents, int start, int length)
		{
			this.SetTangents(inTangents, start, length, MeshUpdateFlags.Default);
		}

		public void SetTangents(List<Vector4> inTangents, int start, int length, [UnityEngine.Internal.DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetListForChannel<Vector4>(VertexAttribute.Tangent, inTangents, start, length, flags);
		}

		public void SetTangents(Vector4[] inTangents)
		{
			this.SetTangents(inTangents, 0, NoAllocHelpers.SafeLength(inTangents));
		}

		[ExcludeFromDocs]
		public void SetTangents(Vector4[] inTangents, int start, int length)
		{
			this.SetTangents(inTangents, start, length, MeshUpdateFlags.Default);
		}

		public void SetTangents(Vector4[] inTangents, int start, int length, [UnityEngine.Internal.DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetSizedArrayForChannel(VertexAttribute.Tangent, VertexAttributeFormat.Float32, Mesh.DefaultDimensionForChannel(VertexAttribute.Tangent), inTangents, NoAllocHelpers.SafeLength(inTangents), start, length, flags);
		}

		public void SetTangents<T>(NativeArray<T> inTangents) where T : struct
		{
			this.SetTangents<T>(inTangents, 0, inTangents.Length);
		}

		[ExcludeFromDocs]
		public void SetTangents<T>(NativeArray<T> inTangents, int start, int length) where T : struct
		{
			this.SetTangents<T>(inTangents, start, length, MeshUpdateFlags.Default);
		}

		public void SetTangents<T>(NativeArray<T> inTangents, int start, int length, [UnityEngine.Internal.DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags) where T : struct
		{
			bool flag = UnsafeUtility.SizeOf<T>() != 16;
			if (flag)
			{
				throw new ArgumentException("SetTangents with NativeArray should use struct type that is 16 bytes (4x float) in size");
			}
			this.SetSizedNativeArrayForChannel(VertexAttribute.Tangent, VertexAttributeFormat.Float32, 4, (IntPtr)inTangents.GetUnsafeReadOnlyPtr<T>(), inTangents.Length, start, length, flags);
		}

		public void GetColors(List<Color> colors)
		{
			bool flag = colors == null;
			if (flag)
			{
				throw new ArgumentNullException("colors", "The result colors list cannot be null.");
			}
			this.GetListForChannel<Color>(colors, this.vertexCount, VertexAttribute.Color, Mesh.DefaultDimensionForChannel(VertexAttribute.Color));
		}

		public void SetColors(List<Color> inColors)
		{
			this.SetColors(inColors, 0, NoAllocHelpers.SafeLength<Color>(inColors));
		}

		[ExcludeFromDocs]
		public void SetColors(List<Color> inColors, int start, int length)
		{
			this.SetColors(inColors, start, length, MeshUpdateFlags.Default);
		}

		public void SetColors(List<Color> inColors, int start, int length, [UnityEngine.Internal.DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetListForChannel<Color>(VertexAttribute.Color, inColors, start, length, flags);
		}

		public void SetColors(Color[] inColors)
		{
			this.SetColors(inColors, 0, NoAllocHelpers.SafeLength(inColors));
		}

		[ExcludeFromDocs]
		public void SetColors(Color[] inColors, int start, int length)
		{
			this.SetColors(inColors, start, length, MeshUpdateFlags.Default);
		}

		public void SetColors(Color[] inColors, int start, int length, [UnityEngine.Internal.DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetSizedArrayForChannel(VertexAttribute.Color, VertexAttributeFormat.Float32, Mesh.DefaultDimensionForChannel(VertexAttribute.Color), inColors, NoAllocHelpers.SafeLength(inColors), start, length, flags);
		}

		public void GetColors(List<Color32> colors)
		{
			bool flag = colors == null;
			if (flag)
			{
				throw new ArgumentNullException("colors", "The result colors list cannot be null.");
			}
			this.GetListForChannel<Color32>(colors, this.vertexCount, VertexAttribute.Color, 4, VertexAttributeFormat.UNorm8);
		}

		public void SetColors(List<Color32> inColors)
		{
			this.SetColors(inColors, 0, NoAllocHelpers.SafeLength<Color32>(inColors));
		}

		[ExcludeFromDocs]
		public void SetColors(List<Color32> inColors, int start, int length)
		{
			this.SetColors(inColors, start, length, MeshUpdateFlags.Default);
		}

		public void SetColors(List<Color32> inColors, int start, int length, [UnityEngine.Internal.DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetListForChannel<Color32>(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4, inColors, start, length, flags);
		}

		public void SetColors(Color32[] inColors)
		{
			this.SetColors(inColors, 0, NoAllocHelpers.SafeLength(inColors));
		}

		[ExcludeFromDocs]
		public void SetColors(Color32[] inColors, int start, int length)
		{
			this.SetColors(inColors, start, length, MeshUpdateFlags.Default);
		}

		public void SetColors(Color32[] inColors, int start, int length, [UnityEngine.Internal.DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetSizedArrayForChannel(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4, inColors, NoAllocHelpers.SafeLength(inColors), start, length, flags);
		}

		public void SetColors<T>(NativeArray<T> inColors) where T : struct
		{
			this.SetColors<T>(inColors, 0, inColors.Length);
		}

		[ExcludeFromDocs]
		public void SetColors<T>(NativeArray<T> inColors, int start, int length) where T : struct
		{
			this.SetColors<T>(inColors, start, length, MeshUpdateFlags.Default);
		}

		public void SetColors<T>(NativeArray<T> inColors, int start, int length, [UnityEngine.Internal.DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags) where T : struct
		{
			int num = UnsafeUtility.SizeOf<T>();
			bool flag = num != 16 && num != 4;
			if (flag)
			{
				throw new ArgumentException("SetColors with NativeArray should use struct type that is 16 bytes (4x float) or 4 bytes (4x unorm) in size");
			}
			this.SetSizedNativeArrayForChannel(VertexAttribute.Color, (num == 4) ? VertexAttributeFormat.UNorm8 : VertexAttributeFormat.Float32, 4, (IntPtr)inColors.GetUnsafeReadOnlyPtr<T>(), inColors.Length, start, length, flags);
		}

		private void SetUvsImpl<T>(int uvIndex, int dim, List<T> uvs, int start, int length, MeshUpdateFlags flags)
		{
			bool flag = uvIndex < 0 || uvIndex > 7;
			if (flag)
			{
				Debug.LogError("The uv index is invalid. Must be in the range 0 to 7.");
			}
			else
			{
				this.SetListForChannel<T>(Mesh.GetUVChannel(uvIndex), VertexAttributeFormat.Float32, dim, uvs, start, length, flags);
			}
		}

		public void SetUVs(int channel, List<Vector2> uvs)
		{
			this.SetUVs(channel, uvs, 0, NoAllocHelpers.SafeLength<Vector2>(uvs));
		}

		public void SetUVs(int channel, List<Vector3> uvs)
		{
			this.SetUVs(channel, uvs, 0, NoAllocHelpers.SafeLength<Vector3>(uvs));
		}

		public void SetUVs(int channel, List<Vector4> uvs)
		{
			this.SetUVs(channel, uvs, 0, NoAllocHelpers.SafeLength<Vector4>(uvs));
		}

		[ExcludeFromDocs]
		public void SetUVs(int channel, List<Vector2> uvs, int start, int length)
		{
			this.SetUVs(channel, uvs, start, length, MeshUpdateFlags.Default);
		}

		public void SetUVs(int channel, List<Vector2> uvs, int start, int length, [UnityEngine.Internal.DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetUvsImpl<Vector2>(channel, 2, uvs, start, length, flags);
		}

		[ExcludeFromDocs]
		public void SetUVs(int channel, List<Vector3> uvs, int start, int length)
		{
			this.SetUVs(channel, uvs, start, length, MeshUpdateFlags.Default);
		}

		public void SetUVs(int channel, List<Vector3> uvs, int start, int length, [UnityEngine.Internal.DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetUvsImpl<Vector3>(channel, 3, uvs, start, length, flags);
		}

		[ExcludeFromDocs]
		public void SetUVs(int channel, List<Vector4> uvs, int start, int length)
		{
			this.SetUVs(channel, uvs, start, length, MeshUpdateFlags.Default);
		}

		public void SetUVs(int channel, List<Vector4> uvs, int start, int length, [UnityEngine.Internal.DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetUvsImpl<Vector4>(channel, 4, uvs, start, length, flags);
		}

		private void SetUvsImpl(int uvIndex, int dim, Array uvs, int arrayStart, int arraySize, MeshUpdateFlags flags)
		{
			bool flag = uvIndex < 0 || uvIndex > 7;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("uvIndex", uvIndex, "The uv index is invalid. Must be in the range 0 to 7.");
			}
			this.SetSizedArrayForChannel(Mesh.GetUVChannel(uvIndex), VertexAttributeFormat.Float32, dim, uvs, NoAllocHelpers.SafeLength(uvs), arrayStart, arraySize, flags);
		}

		public void SetUVs(int channel, Vector2[] uvs)
		{
			this.SetUVs(channel, uvs, 0, NoAllocHelpers.SafeLength(uvs));
		}

		public void SetUVs(int channel, Vector3[] uvs)
		{
			this.SetUVs(channel, uvs, 0, NoAllocHelpers.SafeLength(uvs));
		}

		public void SetUVs(int channel, Vector4[] uvs)
		{
			this.SetUVs(channel, uvs, 0, NoAllocHelpers.SafeLength(uvs));
		}

		[ExcludeFromDocs]
		public void SetUVs(int channel, Vector2[] uvs, int start, int length)
		{
			this.SetUVs(channel, uvs, start, length, MeshUpdateFlags.Default);
		}

		public void SetUVs(int channel, Vector2[] uvs, int start, int length, [UnityEngine.Internal.DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetUvsImpl(channel, 2, uvs, start, length, flags);
		}

		[ExcludeFromDocs]
		public void SetUVs(int channel, Vector3[] uvs, int start, int length)
		{
			this.SetUVs(channel, uvs, start, length, MeshUpdateFlags.Default);
		}

		public void SetUVs(int channel, Vector3[] uvs, int start, int length, [UnityEngine.Internal.DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetUvsImpl(channel, 3, uvs, start, length, flags);
		}

		[ExcludeFromDocs]
		public void SetUVs(int channel, Vector4[] uvs, int start, int length)
		{
			this.SetUVs(channel, uvs, start, length, MeshUpdateFlags.Default);
		}

		public void SetUVs(int channel, Vector4[] uvs, int start, int length, [UnityEngine.Internal.DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetUvsImpl(channel, 4, uvs, start, length, flags);
		}

		public void SetUVs<T>(int channel, NativeArray<T> uvs) where T : struct
		{
			this.SetUVs<T>(channel, uvs, 0, uvs.Length);
		}

		[ExcludeFromDocs]
		public void SetUVs<T>(int channel, NativeArray<T> uvs, int start, int length) where T : struct
		{
			this.SetUVs<T>(channel, uvs, start, length, MeshUpdateFlags.Default);
		}

		public void SetUVs<T>(int channel, NativeArray<T> uvs, int start, int length, [UnityEngine.Internal.DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags) where T : struct
		{
			bool flag = channel < 0 || channel > 7;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("channel", channel, "The uv index is invalid. Must be in the range 0 to 7.");
			}
			int num = UnsafeUtility.SizeOf<T>();
			bool flag2 = (num & 3) != 0;
			if (flag2)
			{
				throw new ArgumentException("SetUVs with NativeArray should use struct type that is multiple of 4 bytes in size");
			}
			int num2 = num / 4;
			bool flag3 = num2 < 1 || num2 > 4;
			if (flag3)
			{
				throw new ArgumentException("SetUVs with NativeArray should use struct type that is 1..4 floats in size");
			}
			this.SetSizedNativeArrayForChannel(Mesh.GetUVChannel(channel), VertexAttributeFormat.Float32, num2, (IntPtr)uvs.GetUnsafeReadOnlyPtr<T>(), uvs.Length, start, length, flags);
		}

		private void GetUVsImpl<T>(int uvIndex, List<T> uvs, int dim)
		{
			bool flag = uvs == null;
			if (flag)
			{
				throw new ArgumentNullException("uvs", "The result uvs list cannot be null.");
			}
			bool flag2 = uvIndex < 0 || uvIndex > 7;
			if (flag2)
			{
				throw new IndexOutOfRangeException("The uv index is invalid. Must be in the range 0 to 7.");
			}
			this.GetListForChannel<T>(uvs, this.vertexCount, Mesh.GetUVChannel(uvIndex), dim);
		}

		public void GetUVs(int channel, List<Vector2> uvs)
		{
			this.GetUVsImpl<Vector2>(channel, uvs, 2);
		}

		public void GetUVs(int channel, List<Vector3> uvs)
		{
			this.GetUVsImpl<Vector3>(channel, uvs, 3);
		}

		public void GetUVs(int channel, List<Vector4> uvs)
		{
			this.GetUVsImpl<Vector4>(channel, uvs, 4);
		}

		public VertexAttributeDescriptor[] GetVertexAttributes()
		{
			return (VertexAttributeDescriptor[])this.GetVertexAttributesAlloc();
		}

		public int GetVertexAttributes(VertexAttributeDescriptor[] attributes)
		{
			return this.GetVertexAttributesArray(attributes);
		}

		public int GetVertexAttributes(List<VertexAttributeDescriptor> attributes)
		{
			return this.GetVertexAttributesList(attributes);
		}

		public void SetVertexBufferParams(int vertexCount, params VertexAttributeDescriptor[] attributes)
		{
			this.SetVertexBufferParamsFromArray(vertexCount, attributes);
		}

		public void SetVertexBufferParams(int vertexCount, NativeArray<VertexAttributeDescriptor> attributes)
		{
			this.SetVertexBufferParamsFromPtr(vertexCount, (IntPtr)attributes.GetUnsafeReadOnlyPtr<VertexAttributeDescriptor>(), attributes.Length);
		}

		public void SetVertexBufferData<T>(NativeArray<T> data, int dataStart, int meshBufferStart, int count, int stream = 0, MeshUpdateFlags flags = MeshUpdateFlags.Default) where T : struct
		{
			bool flag = !this.canAccess;
			if (flag)
			{
				throw new InvalidOperationException("Not allowed to access vertex data on mesh '" + base.name + "' (isReadable is false; Read/Write must be enabled in import settings)");
			}
			bool flag2 = dataStart < 0 || meshBufferStart < 0 || count < 0 || dataStart + count > data.Length;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (dataStart:{0} meshBufferStart:{1} count:{2})", dataStart, meshBufferStart, count));
			}
			this.InternalSetVertexBufferData(stream, (IntPtr)data.GetUnsafeReadOnlyPtr<T>(), dataStart, meshBufferStart, count, UnsafeUtility.SizeOf<T>(), flags);
		}

		public void SetVertexBufferData<T>(T[] data, int dataStart, int meshBufferStart, int count, int stream = 0, MeshUpdateFlags flags = MeshUpdateFlags.Default) where T : struct
		{
			bool flag = !this.canAccess;
			if (flag)
			{
				throw new InvalidOperationException("Not allowed to access vertex data on mesh '" + base.name + "' (isReadable is false; Read/Write must be enabled in import settings)");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException("Array passed to SetVertexBufferData must be blittable.\n" + UnsafeUtility.GetReasonForArrayNonBlittable(data));
			}
			bool flag3 = dataStart < 0 || meshBufferStart < 0 || count < 0 || dataStart + count > data.Length;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (dataStart:{0} meshBufferStart:{1} count:{2})", dataStart, meshBufferStart, count));
			}
			this.InternalSetVertexBufferDataFromArray(stream, data, dataStart, meshBufferStart, count, UnsafeUtility.SizeOf<T>(), flags);
		}

		public void SetVertexBufferData<T>(List<T> data, int dataStart, int meshBufferStart, int count, int stream = 0, MeshUpdateFlags flags = MeshUpdateFlags.Default) where T : struct
		{
			bool flag = !this.canAccess;
			if (flag)
			{
				throw new InvalidOperationException("Not allowed to access vertex data on mesh '" + base.name + "' (isReadable is false; Read/Write must be enabled in import settings)");
			}
			bool flag2 = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag2)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to {1} must be blittable.\n{2}", typeof(T), "SetVertexBufferData", UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			bool flag3 = dataStart < 0 || meshBufferStart < 0 || count < 0 || dataStart + count > data.Count;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (dataStart:{0} meshBufferStart:{1} count:{2})", dataStart, meshBufferStart, count));
			}
			this.InternalSetVertexBufferDataFromArray(stream, NoAllocHelpers.ExtractArrayFromList(data), dataStart, meshBufferStart, count, UnsafeUtility.SizeOf<T>(), flags);
		}

		public static Mesh.MeshDataArray AcquireReadOnlyMeshData(Mesh mesh)
		{
			return new Mesh.MeshDataArray(mesh, true);
		}

		public static Mesh.MeshDataArray AcquireReadOnlyMeshData(Mesh[] meshes)
		{
			bool flag = meshes == null;
			if (flag)
			{
				throw new ArgumentNullException("meshes", "Mesh array is null");
			}
			return new Mesh.MeshDataArray(meshes, meshes.Length, true);
		}

		public static Mesh.MeshDataArray AcquireReadOnlyMeshData(List<Mesh> meshes)
		{
			bool flag = meshes == null;
			if (flag)
			{
				throw new ArgumentNullException("meshes", "Mesh list is null");
			}
			return new Mesh.MeshDataArray(NoAllocHelpers.ExtractArrayFromListT<Mesh>(meshes), meshes.Count, true);
		}

		public static Mesh.MeshDataArray AllocateWritableMeshData(int meshCount)
		{
			return new Mesh.MeshDataArray(meshCount);
		}

		public static void ApplyAndDisposeWritableMeshData(Mesh.MeshDataArray data, Mesh mesh, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			bool flag = mesh == null;
			if (flag)
			{
				throw new ArgumentNullException("mesh", "Mesh is null");
			}
			bool flag2 = data.Length != 1;
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("{0} length must be 1 to apply to one mesh, was {1}", "MeshDataArray", data.Length));
			}
			data.ApplyToMeshAndDispose(mesh, flags);
		}

		public static void ApplyAndDisposeWritableMeshData(Mesh.MeshDataArray data, Mesh[] meshes, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			bool flag = meshes == null;
			if (flag)
			{
				throw new ArgumentNullException("meshes", "Mesh array is null");
			}
			bool flag2 = data.Length != meshes.Length;
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("{0} length ({1}) must match destination meshes array length ({2})", "MeshDataArray", data.Length, meshes.Length));
			}
			data.ApplyToMeshesAndDispose(meshes, flags);
		}

		public static void ApplyAndDisposeWritableMeshData(Mesh.MeshDataArray data, List<Mesh> meshes, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			bool flag = meshes == null;
			if (flag)
			{
				throw new ArgumentNullException("meshes", "Mesh list is null");
			}
			bool flag2 = data.Length != meshes.Count;
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("{0} length ({1}) must match destination meshes list length ({2})", "MeshDataArray", data.Length, meshes.Count));
			}
			data.ApplyToMeshesAndDispose(NoAllocHelpers.ExtractArrayFromListT<Mesh>(meshes), flags);
		}

		private void PrintErrorCantAccessIndices()
		{
			Debug.LogError(string.Format("Not allowed to access triangles/indices on mesh '{0}' (isReadable is false; Read/Write must be enabled in import settings)", base.name));
		}

		private bool CheckCanAccessSubmesh(int submesh, bool errorAboutTriangles)
		{
			bool flag = !this.canAccess;
			bool result;
			if (flag)
			{
				this.PrintErrorCantAccessIndices();
				result = false;
			}
			else
			{
				bool flag2 = submesh < 0 || submesh >= this.subMeshCount;
				if (flag2)
				{
					Debug.LogError(string.Format("Failed getting {0}. Submesh index is out of bounds.", errorAboutTriangles ? "triangles" : "indices"), this);
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		private bool CheckCanAccessSubmeshTriangles(int submesh)
		{
			return this.CheckCanAccessSubmesh(submesh, true);
		}

		private bool CheckCanAccessSubmeshIndices(int submesh)
		{
			return this.CheckCanAccessSubmesh(submesh, false);
		}

		public int[] GetTriangles(int submesh)
		{
			return this.GetTriangles(submesh, true);
		}

		public int[] GetTriangles(int submesh, [UnityEngine.Internal.DefaultValue("true")] bool applyBaseVertex)
		{
			return this.CheckCanAccessSubmeshTriangles(submesh) ? this.GetTrianglesImpl(submesh, applyBaseVertex) : new int[0];
		}

		public void GetTriangles(List<int> triangles, int submesh)
		{
			this.GetTriangles(triangles, submesh, true);
		}

		public void GetTriangles(List<int> triangles, int submesh, [UnityEngine.Internal.DefaultValue("true")] bool applyBaseVertex)
		{
			bool flag = triangles == null;
			if (flag)
			{
				throw new ArgumentNullException("triangles", "The result triangles list cannot be null.");
			}
			bool flag2 = submesh < 0 || submesh >= this.subMeshCount;
			if (flag2)
			{
				throw new IndexOutOfRangeException("Specified sub mesh is out of range. Must be greater or equal to 0 and less than subMeshCount.");
			}
			NoAllocHelpers.EnsureListElemCount<int>(triangles, (int)(3u * this.GetTrianglesCountImpl(submesh)));
			this.GetTrianglesNonAllocImpl(NoAllocHelpers.ExtractArrayFromListT<int>(triangles), submesh, applyBaseVertex);
		}

		public void GetTriangles(List<ushort> triangles, int submesh, bool applyBaseVertex = true)
		{
			bool flag = triangles == null;
			if (flag)
			{
				throw new ArgumentNullException("triangles", "The result triangles list cannot be null.");
			}
			bool flag2 = submesh < 0 || submesh >= this.subMeshCount;
			if (flag2)
			{
				throw new IndexOutOfRangeException("Specified sub mesh is out of range. Must be greater or equal to 0 and less than subMeshCount.");
			}
			NoAllocHelpers.EnsureListElemCount<ushort>(triangles, (int)(3u * this.GetTrianglesCountImpl(submesh)));
			this.GetTrianglesNonAllocImpl16(NoAllocHelpers.ExtractArrayFromListT<ushort>(triangles), submesh, applyBaseVertex);
		}

		[ExcludeFromDocs]
		public int[] GetIndices(int submesh)
		{
			return this.GetIndices(submesh, true);
		}

		public int[] GetIndices(int submesh, [UnityEngine.Internal.DefaultValue("true")] bool applyBaseVertex)
		{
			return this.CheckCanAccessSubmeshIndices(submesh) ? this.GetIndicesImpl(submesh, applyBaseVertex) : new int[0];
		}

		[ExcludeFromDocs]
		public void GetIndices(List<int> indices, int submesh)
		{
			this.GetIndices(indices, submesh, true);
		}

		public void GetIndices(List<int> indices, int submesh, [UnityEngine.Internal.DefaultValue("true")] bool applyBaseVertex)
		{
			bool flag = indices == null;
			if (flag)
			{
				throw new ArgumentNullException("indices", "The result indices list cannot be null.");
			}
			bool flag2 = submesh < 0 || submesh >= this.subMeshCount;
			if (flag2)
			{
				throw new IndexOutOfRangeException("Specified sub mesh is out of range. Must be greater or equal to 0 and less than subMeshCount.");
			}
			NoAllocHelpers.EnsureListElemCount<int>(indices, (int)this.GetIndexCount(submesh));
			this.GetIndicesNonAllocImpl(NoAllocHelpers.ExtractArrayFromListT<int>(indices), submesh, applyBaseVertex);
		}

		public void GetIndices(List<ushort> indices, int submesh, bool applyBaseVertex = true)
		{
			bool flag = indices == null;
			if (flag)
			{
				throw new ArgumentNullException("indices", "The result indices list cannot be null.");
			}
			bool flag2 = submesh < 0 || submesh >= this.subMeshCount;
			if (flag2)
			{
				throw new IndexOutOfRangeException("Specified sub mesh is out of range. Must be greater or equal to 0 and less than subMeshCount.");
			}
			NoAllocHelpers.EnsureListElemCount<ushort>(indices, (int)this.GetIndexCount(submesh));
			this.GetIndicesNonAllocImpl16(NoAllocHelpers.ExtractArrayFromListT<ushort>(indices), submesh, applyBaseVertex);
		}

		public void SetIndexBufferData<T>(NativeArray<T> data, int dataStart, int meshBufferStart, int count, MeshUpdateFlags flags = MeshUpdateFlags.Default) where T : struct
		{
			bool flag = !this.canAccess;
			if (flag)
			{
				this.PrintErrorCantAccessIndices();
			}
			else
			{
				bool flag2 = dataStart < 0 || meshBufferStart < 0 || count < 0 || dataStart + count > data.Length;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (dataStart:{0} meshBufferStart:{1} count:{2})", dataStart, meshBufferStart, count));
				}
				this.InternalSetIndexBufferData((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), dataStart, meshBufferStart, count, UnsafeUtility.SizeOf<T>(), flags);
			}
		}

		public void SetIndexBufferData<T>(T[] data, int dataStart, int meshBufferStart, int count, MeshUpdateFlags flags = MeshUpdateFlags.Default) where T : struct
		{
			bool flag = !this.canAccess;
			if (flag)
			{
				this.PrintErrorCantAccessIndices();
			}
			else
			{
				bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
				if (flag2)
				{
					throw new ArgumentException("Array passed to SetIndexBufferData must be blittable.\n" + UnsafeUtility.GetReasonForArrayNonBlittable(data));
				}
				bool flag3 = dataStart < 0 || meshBufferStart < 0 || count < 0 || dataStart + count > data.Length;
				if (flag3)
				{
					throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (dataStart:{0} meshBufferStart:{1} count:{2})", dataStart, meshBufferStart, count));
				}
				this.InternalSetIndexBufferDataFromArray(data, dataStart, meshBufferStart, count, UnsafeUtility.SizeOf<T>(), flags);
			}
		}

		public void SetIndexBufferData<T>(List<T> data, int dataStart, int meshBufferStart, int count, MeshUpdateFlags flags = MeshUpdateFlags.Default) where T : struct
		{
			bool flag = !this.canAccess;
			if (flag)
			{
				this.PrintErrorCantAccessIndices();
			}
			else
			{
				bool flag2 = !UnsafeUtility.IsGenericListBlittable<T>();
				if (flag2)
				{
					throw new ArgumentException(string.Format("List<{0}> passed to {1} must be blittable.\n{2}", typeof(T), "SetIndexBufferData", UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
				}
				bool flag3 = dataStart < 0 || meshBufferStart < 0 || count < 0 || dataStart + count > data.Count;
				if (flag3)
				{
					throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (dataStart:{0} meshBufferStart:{1} count:{2})", dataStart, meshBufferStart, count));
				}
				this.InternalSetIndexBufferDataFromArray(NoAllocHelpers.ExtractArrayFromList(data), dataStart, meshBufferStart, count, UnsafeUtility.SizeOf<T>(), flags);
			}
		}

		public uint GetIndexStart(int submesh)
		{
			bool flag = submesh < 0 || submesh >= this.subMeshCount;
			if (flag)
			{
				throw new IndexOutOfRangeException("Specified sub mesh is out of range. Must be greater or equal to 0 and less than subMeshCount.");
			}
			return this.GetIndexStartImpl(submesh);
		}

		public uint GetIndexCount(int submesh)
		{
			bool flag = submesh < 0 || submesh >= this.subMeshCount;
			if (flag)
			{
				throw new IndexOutOfRangeException("Specified sub mesh is out of range. Must be greater or equal to 0 and less than subMeshCount.");
			}
			return this.GetIndexCountImpl(submesh);
		}

		public uint GetBaseVertex(int submesh)
		{
			bool flag = submesh < 0 || submesh >= this.subMeshCount;
			if (flag)
			{
				throw new IndexOutOfRangeException("Specified sub mesh is out of range. Must be greater or equal to 0 and less than subMeshCount.");
			}
			return this.GetBaseVertexImpl(submesh);
		}

		private void CheckIndicesArrayRange(int valuesLength, int start, int length)
		{
			bool flag = start < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("start", start, "Mesh indices array start can't be negative.");
			}
			bool flag2 = length < 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("length", length, "Mesh indices array length can't be negative.");
			}
			bool flag3 = start >= valuesLength && length != 0;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("start", start, "Mesh indices array start is outside of array size.");
			}
			bool flag4 = start + length > valuesLength;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("length", start + length, "Mesh indices array start+count is outside of array size.");
			}
		}

		private void SetTrianglesImpl(int submesh, IndexFormat indicesFormat, Array triangles, int trianglesArrayLength, int start, int length, bool calculateBounds, int baseVertex)
		{
			this.CheckIndicesArrayRange(trianglesArrayLength, start, length);
			this.SetIndicesImpl(submesh, MeshTopology.Triangles, indicesFormat, triangles, start, length, calculateBounds, baseVertex);
		}

		[ExcludeFromDocs]
		public void SetTriangles(int[] triangles, int submesh)
		{
			this.SetTriangles(triangles, submesh, true, 0);
		}

		[ExcludeFromDocs]
		public void SetTriangles(int[] triangles, int submesh, bool calculateBounds)
		{
			this.SetTriangles(triangles, submesh, calculateBounds, 0);
		}

		public void SetTriangles(int[] triangles, int submesh, [UnityEngine.Internal.DefaultValue("true")] bool calculateBounds, [UnityEngine.Internal.DefaultValue("0")] int baseVertex)
		{
			this.SetTriangles(triangles, 0, NoAllocHelpers.SafeLength(triangles), submesh, calculateBounds, baseVertex);
		}

		public void SetTriangles(int[] triangles, int trianglesStart, int trianglesLength, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			bool flag = this.CheckCanAccessSubmeshTriangles(submesh);
			if (flag)
			{
				this.SetTrianglesImpl(submesh, IndexFormat.UInt32, triangles, NoAllocHelpers.SafeLength(triangles), trianglesStart, trianglesLength, calculateBounds, baseVertex);
			}
		}

		public void SetTriangles(ushort[] triangles, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			this.SetTriangles(triangles, 0, NoAllocHelpers.SafeLength(triangles), submesh, calculateBounds, baseVertex);
		}

		public void SetTriangles(ushort[] triangles, int trianglesStart, int trianglesLength, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			bool flag = this.CheckCanAccessSubmeshTriangles(submesh);
			if (flag)
			{
				this.SetTrianglesImpl(submesh, IndexFormat.UInt16, triangles, NoAllocHelpers.SafeLength(triangles), trianglesStart, trianglesLength, calculateBounds, baseVertex);
			}
		}

		[ExcludeFromDocs]
		public void SetTriangles(List<int> triangles, int submesh)
		{
			this.SetTriangles(triangles, submesh, true, 0);
		}

		[ExcludeFromDocs]
		public void SetTriangles(List<int> triangles, int submesh, bool calculateBounds)
		{
			this.SetTriangles(triangles, submesh, calculateBounds, 0);
		}

		public void SetTriangles(List<int> triangles, int submesh, [UnityEngine.Internal.DefaultValue("true")] bool calculateBounds, [UnityEngine.Internal.DefaultValue("0")] int baseVertex)
		{
			this.SetTriangles(triangles, 0, NoAllocHelpers.SafeLength<int>(triangles), submesh, calculateBounds, baseVertex);
		}

		public void SetTriangles(List<int> triangles, int trianglesStart, int trianglesLength, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			bool flag = this.CheckCanAccessSubmeshTriangles(submesh);
			if (flag)
			{
				this.SetTrianglesImpl(submesh, IndexFormat.UInt32, NoAllocHelpers.ExtractArrayFromList(triangles), NoAllocHelpers.SafeLength<int>(triangles), trianglesStart, trianglesLength, calculateBounds, baseVertex);
			}
		}

		public void SetTriangles(List<ushort> triangles, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			this.SetTriangles(triangles, 0, NoAllocHelpers.SafeLength<ushort>(triangles), submesh, calculateBounds, baseVertex);
		}

		public void SetTriangles(List<ushort> triangles, int trianglesStart, int trianglesLength, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			bool flag = this.CheckCanAccessSubmeshTriangles(submesh);
			if (flag)
			{
				this.SetTrianglesImpl(submesh, IndexFormat.UInt16, NoAllocHelpers.ExtractArrayFromList(triangles), NoAllocHelpers.SafeLength<ushort>(triangles), trianglesStart, trianglesLength, calculateBounds, baseVertex);
			}
		}

		[ExcludeFromDocs]
		public void SetIndices(int[] indices, MeshTopology topology, int submesh)
		{
			this.SetIndices(indices, topology, submesh, true, 0);
		}

		[ExcludeFromDocs]
		public void SetIndices(int[] indices, MeshTopology topology, int submesh, bool calculateBounds)
		{
			this.SetIndices(indices, topology, submesh, calculateBounds, 0);
		}

		public void SetIndices(int[] indices, MeshTopology topology, int submesh, [UnityEngine.Internal.DefaultValue("true")] bool calculateBounds, [UnityEngine.Internal.DefaultValue("0")] int baseVertex)
		{
			this.SetIndices(indices, 0, NoAllocHelpers.SafeLength(indices), topology, submesh, calculateBounds, baseVertex);
		}

		public void SetIndices(int[] indices, int indicesStart, int indicesLength, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			bool flag = this.CheckCanAccessSubmeshIndices(submesh);
			if (flag)
			{
				this.CheckIndicesArrayRange(NoAllocHelpers.SafeLength(indices), indicesStart, indicesLength);
				this.SetIndicesImpl(submesh, topology, IndexFormat.UInt32, indices, indicesStart, indicesLength, calculateBounds, baseVertex);
			}
		}

		public void SetIndices(ushort[] indices, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			this.SetIndices(indices, 0, NoAllocHelpers.SafeLength(indices), topology, submesh, calculateBounds, baseVertex);
		}

		public void SetIndices(ushort[] indices, int indicesStart, int indicesLength, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			bool flag = this.CheckCanAccessSubmeshIndices(submesh);
			if (flag)
			{
				this.CheckIndicesArrayRange(NoAllocHelpers.SafeLength(indices), indicesStart, indicesLength);
				this.SetIndicesImpl(submesh, topology, IndexFormat.UInt16, indices, indicesStart, indicesLength, calculateBounds, baseVertex);
			}
		}

		public void SetIndices<T>(NativeArray<T> indices, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0) where T : struct
		{
			this.SetIndices<T>(indices, 0, indices.Length, topology, submesh, calculateBounds, baseVertex);
		}

		public void SetIndices<T>(NativeArray<T> indices, int indicesStart, int indicesLength, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0) where T : struct
		{
			bool flag = this.CheckCanAccessSubmeshIndices(submesh);
			if (flag)
			{
				int num = UnsafeUtility.SizeOf<T>();
				bool flag2 = num != 2 && num != 4;
				if (flag2)
				{
					throw new ArgumentException("SetIndices with NativeArray should use type is 2 or 4 bytes in size");
				}
				this.CheckIndicesArrayRange(indices.Length, indicesStart, indicesLength);
				this.SetIndicesNativeArrayImpl(submesh, topology, (num == 2) ? IndexFormat.UInt16 : IndexFormat.UInt32, (IntPtr)indices.GetUnsafeReadOnlyPtr<T>(), indicesStart, indicesLength, calculateBounds, baseVertex);
			}
		}

		public void SetIndices(List<int> indices, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			this.SetIndices(indices, 0, NoAllocHelpers.SafeLength<int>(indices), topology, submesh, calculateBounds, baseVertex);
		}

		public void SetIndices(List<int> indices, int indicesStart, int indicesLength, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			bool flag = this.CheckCanAccessSubmeshIndices(submesh);
			if (flag)
			{
				Array indices2 = NoAllocHelpers.ExtractArrayFromList(indices);
				this.CheckIndicesArrayRange(NoAllocHelpers.SafeLength<int>(indices), indicesStart, indicesLength);
				this.SetIndicesImpl(submesh, topology, IndexFormat.UInt32, indices2, indicesStart, indicesLength, calculateBounds, baseVertex);
			}
		}

		public void SetIndices(List<ushort> indices, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			this.SetIndices(indices, 0, NoAllocHelpers.SafeLength<ushort>(indices), topology, submesh, calculateBounds, baseVertex);
		}

		public void SetIndices(List<ushort> indices, int indicesStart, int indicesLength, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			bool flag = this.CheckCanAccessSubmeshIndices(submesh);
			if (flag)
			{
				Array indices2 = NoAllocHelpers.ExtractArrayFromList(indices);
				this.CheckIndicesArrayRange(NoAllocHelpers.SafeLength<ushort>(indices), indicesStart, indicesLength);
				this.SetIndicesImpl(submesh, topology, IndexFormat.UInt16, indices2, indicesStart, indicesLength, calculateBounds, baseVertex);
			}
		}

		public void SetSubMeshes(SubMeshDescriptor[] desc, int start, int count, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			bool flag = count > 0 && desc == null;
			if (flag)
			{
				throw new ArgumentNullException("desc", "Array of submeshes cannot be null unless count is zero.");
			}
			int num = (desc != null) ? desc.Length : 0;
			bool flag2 = start < 0 || count < 0 || start + count > num;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (start:{0} count:{1} desc.Length:{2})", start, count, num));
			}
			for (int i = start; i < start + count; i++)
			{
				MeshTopology topology = desc[i].topology;
				bool flag3 = topology < MeshTopology.Triangles || topology > MeshTopology.Points;
				if (flag3)
				{
					throw new ArgumentException("desc", string.Format("{0}-th submesh descriptor has invalid topology ({1}).", i, (int)topology));
				}
				bool flag4 = topology == (MeshTopology)1;
				if (flag4)
				{
					throw new ArgumentException("desc", string.Format("{0}-th submesh descriptor has triangles strip topology, which is no longer supported.", i));
				}
			}
			this.SetAllSubMeshesAtOnceFromArray(desc, start, count, flags);
		}

		public void SetSubMeshes(SubMeshDescriptor[] desc, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			this.SetSubMeshes(desc, 0, (desc != null) ? desc.Length : 0, flags);
		}

		public void SetSubMeshes(List<SubMeshDescriptor> desc, int start, int count, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			this.SetSubMeshes(NoAllocHelpers.ExtractArrayFromListT<SubMeshDescriptor>(desc), start, count, flags);
		}

		public void SetSubMeshes(List<SubMeshDescriptor> desc, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			this.SetSubMeshes(NoAllocHelpers.ExtractArrayFromListT<SubMeshDescriptor>(desc), 0, (desc != null) ? desc.Count : 0, flags);
		}

		public void SetSubMeshes<T>(NativeArray<T> desc, int start, int count, MeshUpdateFlags flags = MeshUpdateFlags.Default) where T : struct
		{
			bool flag = UnsafeUtility.SizeOf<T>() != UnsafeUtility.SizeOf<SubMeshDescriptor>();
			if (flag)
			{
				throw new ArgumentException(string.Format("{0} with NativeArray should use struct type that is {1} bytes in size", "SetSubMeshes", UnsafeUtility.SizeOf<SubMeshDescriptor>()));
			}
			bool flag2 = start < 0 || count < 0 || start + count > desc.Length;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (start:{0} count:{1} desc.Length:{2})", start, count, desc.Length));
			}
			this.SetAllSubMeshesAtOnceFromNativeArray((IntPtr)desc.GetUnsafeReadOnlyPtr<T>(), start, count, MeshUpdateFlags.Default);
		}

		public void SetSubMeshes<T>(NativeArray<T> desc, MeshUpdateFlags flags = MeshUpdateFlags.Default) where T : struct
		{
			this.SetSubMeshes<T>(desc, 0, desc.Length, flags);
		}

		public void GetBindposes(List<Matrix4x4> bindposes)
		{
			bool flag = bindposes == null;
			if (flag)
			{
				throw new ArgumentNullException("bindposes", "The result bindposes list cannot be null.");
			}
			NoAllocHelpers.EnsureListElemCount<Matrix4x4>(bindposes, this.GetBindposeCount());
			this.GetBindposesNonAllocImpl(NoAllocHelpers.ExtractArrayFromListT<Matrix4x4>(bindposes));
		}

		public void GetBoneWeights(List<BoneWeight> boneWeights)
		{
			bool flag = boneWeights == null;
			if (flag)
			{
				throw new ArgumentNullException("boneWeights", "The result boneWeights list cannot be null.");
			}
			bool flag2 = this.HasBoneWeights();
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<BoneWeight>(boneWeights, this.vertexCount);
			}
			this.GetBoneWeightsNonAllocImpl(NoAllocHelpers.ExtractArrayFromListT<BoneWeight>(boneWeights));
		}

		public void Clear([UnityEngine.Internal.DefaultValue("true")] bool keepVertexLayout)
		{
			this.ClearImpl(keepVertexLayout);
		}

		[ExcludeFromDocs]
		public void Clear()
		{
			this.ClearImpl(true);
		}

		[ExcludeFromDocs]
		public void RecalculateBounds()
		{
			this.RecalculateBounds(MeshUpdateFlags.Default);
		}

		[ExcludeFromDocs]
		public void RecalculateNormals()
		{
			this.RecalculateNormals(MeshUpdateFlags.Default);
		}

		[ExcludeFromDocs]
		public void RecalculateTangents()
		{
			this.RecalculateTangents(MeshUpdateFlags.Default);
		}

		public void RecalculateBounds([UnityEngine.Internal.DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.RecalculateBoundsImpl(flags);
			}
			else
			{
				Debug.LogError(string.Format("Not allowed to call RecalculateBounds() on mesh '{0}'", base.name));
			}
		}

		public void RecalculateNormals([UnityEngine.Internal.DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.RecalculateNormalsImpl(flags);
			}
			else
			{
				Debug.LogError(string.Format("Not allowed to call RecalculateNormals() on mesh '{0}'", base.name));
			}
		}

		public void RecalculateTangents([UnityEngine.Internal.DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.RecalculateTangentsImpl(flags);
			}
			else
			{
				Debug.LogError(string.Format("Not allowed to call RecalculateTangents() on mesh '{0}'", base.name));
			}
		}

		public void RecalculateUVDistributionMetric(int uvSetIndex, float uvAreaThreshold = 1E-09f)
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.RecalculateUVDistributionMetricImpl(uvSetIndex, uvAreaThreshold);
			}
			else
			{
				Debug.LogError(string.Format("Not allowed to call RecalculateUVDistributionMetric() on mesh '{0}'", base.name));
			}
		}

		public void RecalculateUVDistributionMetrics(float uvAreaThreshold = 1E-09f)
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.RecalculateUVDistributionMetricsImpl(uvAreaThreshold);
			}
			else
			{
				Debug.LogError(string.Format("Not allowed to call RecalculateUVDistributionMetrics() on mesh '{0}'", base.name));
			}
		}

		public void MarkDynamic()
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.MarkDynamicImpl();
			}
		}

		public void UploadMeshData(bool markNoLongerReadable)
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.UploadMeshDataImpl(markNoLongerReadable);
			}
		}

		public void Optimize()
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.OptimizeImpl();
			}
			else
			{
				Debug.LogError(string.Format("Not allowed to call Optimize() on mesh '{0}'", base.name));
			}
		}

		public void OptimizeIndexBuffers()
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.OptimizeIndexBuffersImpl();
			}
			else
			{
				Debug.LogError(string.Format("Not allowed to call OptimizeIndexBuffers() on mesh '{0}'", base.name));
			}
		}

		public void OptimizeReorderVertexBuffer()
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.OptimizeReorderVertexBufferImpl();
			}
			else
			{
				Debug.LogError(string.Format("Not allowed to call OptimizeReorderVertexBuffer() on mesh '{0}'", base.name));
			}
		}

		public MeshTopology GetTopology(int submesh)
		{
			bool flag = submesh < 0 || submesh >= this.subMeshCount;
			MeshTopology result;
			if (flag)
			{
				Debug.LogError("Failed getting topology. Submesh index is out of bounds.", this);
				result = MeshTopology.Triangles;
			}
			else
			{
				result = this.GetTopologyImpl(submesh);
			}
			return result;
		}

		public void CombineMeshes(CombineInstance[] combine, [UnityEngine.Internal.DefaultValue("true")] bool mergeSubMeshes, [UnityEngine.Internal.DefaultValue("true")] bool useMatrices, [UnityEngine.Internal.DefaultValue("false")] bool hasLightmapData)
		{
			this.CombineMeshesImpl(combine, mergeSubMeshes, useMatrices, hasLightmapData);
		}

		[ExcludeFromDocs]
		public void CombineMeshes(CombineInstance[] combine, bool mergeSubMeshes, bool useMatrices)
		{
			this.CombineMeshesImpl(combine, mergeSubMeshes, useMatrices, false);
		}

		[ExcludeFromDocs]
		public void CombineMeshes(CombineInstance[] combine, bool mergeSubMeshes)
		{
			this.CombineMeshesImpl(combine, mergeSubMeshes, true, false);
		}

		[ExcludeFromDocs]
		public void CombineMeshes(CombineInstance[] combine)
		{
			this.CombineMeshesImpl(combine, true, true, false);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVertexAttribute_Injected(int index, out VertexAttributeDescriptor ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetReadOnlySafetyHandle_Injected(Mesh.SafetyHandleIndex index, out AtomicSafetyHandle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetSubMesh_Injected(int index, ref SubMeshDescriptor desc, MeshUpdateFlags flags = MeshUpdateFlags.Default);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetSubMesh_Injected(int index, out SubMeshDescriptor ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_bounds_Injected(out Bounds ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_bounds_Injected(ref Bounds value);
	}
}
