using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("ParticleSystemScriptingClasses.h"), NativeHeader("Modules/ParticleSystem/ParticleSystemRenderer.h"), NativeHeader("Modules/ParticleSystem/ScriptBindings/ParticleSystemRendererScriptBindings.h"), RequireComponent(typeof(Transform))]
	public sealed class ParticleSystemRenderer : Renderer
	{
		[NativeName("RenderAlignment")]
		public extern ParticleSystemRenderSpace alignment
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern ParticleSystemRenderMode renderMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern ParticleSystemSortMode sortMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float lengthScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float velocityScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float cameraVelocityScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float normalDirection
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float shadowBias
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float sortingFudge
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float minParticleSize
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float maxParticleSize
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 pivot
		{
			get
			{
				Vector3 result;
				this.get_pivot_Injected(out result);
				return result;
			}
			set
			{
				this.set_pivot_Injected(ref value);
			}
		}

		public Vector3 flip
		{
			get
			{
				Vector3 result;
				this.get_flip_Injected(out result);
				return result;
			}
			set
			{
				this.set_flip_Injected(ref value);
			}
		}

		public extern SpriteMaskInteraction maskInteraction
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Material trailMaterial
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool enableGPUInstancing
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool allowRoll
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool freeformStretching
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool rotateWithStretchDirection
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Mesh mesh
		{
			[FreeFunction(Name = "ParticleSystemRendererScriptBindings::GetMesh", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction(Name = "ParticleSystemRendererScriptBindings::SetMesh", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int meshCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int activeVertexStreamsCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal extern bool editorEnabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool supportsMeshInstancing
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[Obsolete("EnableVertexStreams is deprecated.Use SetActiveVertexStreams instead.", false)]
		public void EnableVertexStreams(ParticleSystemVertexStreams streams)
		{
			this.Internal_SetVertexStreams(streams, true);
		}

		[Obsolete("DisableVertexStreams is deprecated.Use SetActiveVertexStreams instead.", false)]
		public void DisableVertexStreams(ParticleSystemVertexStreams streams)
		{
			this.Internal_SetVertexStreams(streams, false);
		}

		[Obsolete("AreVertexStreamsEnabled is deprecated.Use GetActiveVertexStreams instead.", false)]
		public bool AreVertexStreamsEnabled(ParticleSystemVertexStreams streams)
		{
			return this.Internal_GetEnabledVertexStreams(streams) == streams;
		}

		[Obsolete("GetEnabledVertexStreams is deprecated.Use GetActiveVertexStreams instead.", false)]
		public ParticleSystemVertexStreams GetEnabledVertexStreams(ParticleSystemVertexStreams streams)
		{
			return this.Internal_GetEnabledVertexStreams(streams);
		}

		[Obsolete("Internal_SetVertexStreams is deprecated.Use SetActiveVertexStreams instead.", false)]
		internal void Internal_SetVertexStreams(ParticleSystemVertexStreams streams, bool enabled)
		{
			List<ParticleSystemVertexStream> list = new List<ParticleSystemVertexStream>(this.activeVertexStreamsCount);
			this.GetActiveVertexStreams(list);
			if (enabled)
			{
				bool flag = (streams & ParticleSystemVertexStreams.Position) > ParticleSystemVertexStreams.None;
				if (flag)
				{
					bool flag2 = !list.Contains(ParticleSystemVertexStream.Position);
					if (flag2)
					{
						list.Add(ParticleSystemVertexStream.Position);
					}
				}
				bool flag3 = (streams & ParticleSystemVertexStreams.Normal) > ParticleSystemVertexStreams.None;
				if (flag3)
				{
					bool flag4 = !list.Contains(ParticleSystemVertexStream.Normal);
					if (flag4)
					{
						list.Add(ParticleSystemVertexStream.Normal);
					}
				}
				bool flag5 = (streams & ParticleSystemVertexStreams.Tangent) > ParticleSystemVertexStreams.None;
				if (flag5)
				{
					bool flag6 = !list.Contains(ParticleSystemVertexStream.Tangent);
					if (flag6)
					{
						list.Add(ParticleSystemVertexStream.Tangent);
					}
				}
				bool flag7 = (streams & ParticleSystemVertexStreams.Color) > ParticleSystemVertexStreams.None;
				if (flag7)
				{
					bool flag8 = !list.Contains(ParticleSystemVertexStream.Color);
					if (flag8)
					{
						list.Add(ParticleSystemVertexStream.Color);
					}
				}
				bool flag9 = (streams & ParticleSystemVertexStreams.UV) > ParticleSystemVertexStreams.None;
				if (flag9)
				{
					bool flag10 = !list.Contains(ParticleSystemVertexStream.UV);
					if (flag10)
					{
						list.Add(ParticleSystemVertexStream.UV);
					}
				}
				bool flag11 = (streams & ParticleSystemVertexStreams.UV2BlendAndFrame) > ParticleSystemVertexStreams.None;
				if (flag11)
				{
					bool flag12 = !list.Contains(ParticleSystemVertexStream.UV2);
					if (flag12)
					{
						list.Add(ParticleSystemVertexStream.UV2);
						list.Add(ParticleSystemVertexStream.AnimBlend);
						list.Add(ParticleSystemVertexStream.AnimFrame);
					}
				}
				bool flag13 = (streams & ParticleSystemVertexStreams.CenterAndVertexID) > ParticleSystemVertexStreams.None;
				if (flag13)
				{
					bool flag14 = !list.Contains(ParticleSystemVertexStream.Center);
					if (flag14)
					{
						list.Add(ParticleSystemVertexStream.Center);
						list.Add(ParticleSystemVertexStream.VertexID);
					}
				}
				bool flag15 = (streams & ParticleSystemVertexStreams.Size) > ParticleSystemVertexStreams.None;
				if (flag15)
				{
					bool flag16 = !list.Contains(ParticleSystemVertexStream.SizeXYZ);
					if (flag16)
					{
						list.Add(ParticleSystemVertexStream.SizeXYZ);
					}
				}
				bool flag17 = (streams & ParticleSystemVertexStreams.Rotation) > ParticleSystemVertexStreams.None;
				if (flag17)
				{
					bool flag18 = !list.Contains(ParticleSystemVertexStream.Rotation3D);
					if (flag18)
					{
						list.Add(ParticleSystemVertexStream.Rotation3D);
					}
				}
				bool flag19 = (streams & ParticleSystemVertexStreams.Velocity) > ParticleSystemVertexStreams.None;
				if (flag19)
				{
					bool flag20 = !list.Contains(ParticleSystemVertexStream.Velocity);
					if (flag20)
					{
						list.Add(ParticleSystemVertexStream.Velocity);
					}
				}
				bool flag21 = (streams & ParticleSystemVertexStreams.Lifetime) > ParticleSystemVertexStreams.None;
				if (flag21)
				{
					bool flag22 = !list.Contains(ParticleSystemVertexStream.AgePercent);
					if (flag22)
					{
						list.Add(ParticleSystemVertexStream.AgePercent);
						list.Add(ParticleSystemVertexStream.InvStartLifetime);
					}
				}
				bool flag23 = (streams & ParticleSystemVertexStreams.Custom1) > ParticleSystemVertexStreams.None;
				if (flag23)
				{
					bool flag24 = !list.Contains(ParticleSystemVertexStream.Custom1XYZW);
					if (flag24)
					{
						list.Add(ParticleSystemVertexStream.Custom1XYZW);
					}
				}
				bool flag25 = (streams & ParticleSystemVertexStreams.Custom2) > ParticleSystemVertexStreams.None;
				if (flag25)
				{
					bool flag26 = !list.Contains(ParticleSystemVertexStream.Custom2XYZW);
					if (flag26)
					{
						list.Add(ParticleSystemVertexStream.Custom2XYZW);
					}
				}
				bool flag27 = (streams & ParticleSystemVertexStreams.Random) > ParticleSystemVertexStreams.None;
				if (flag27)
				{
					bool flag28 = !list.Contains(ParticleSystemVertexStream.StableRandomXYZ);
					if (flag28)
					{
						list.Add(ParticleSystemVertexStream.StableRandomXYZ);
						list.Add(ParticleSystemVertexStream.VaryingRandomX);
					}
				}
			}
			else
			{
				bool flag29 = (streams & ParticleSystemVertexStreams.Position) > ParticleSystemVertexStreams.None;
				if (flag29)
				{
					list.Remove(ParticleSystemVertexStream.Position);
				}
				bool flag30 = (streams & ParticleSystemVertexStreams.Normal) > ParticleSystemVertexStreams.None;
				if (flag30)
				{
					list.Remove(ParticleSystemVertexStream.Normal);
				}
				bool flag31 = (streams & ParticleSystemVertexStreams.Tangent) > ParticleSystemVertexStreams.None;
				if (flag31)
				{
					list.Remove(ParticleSystemVertexStream.Tangent);
				}
				bool flag32 = (streams & ParticleSystemVertexStreams.Color) > ParticleSystemVertexStreams.None;
				if (flag32)
				{
					list.Remove(ParticleSystemVertexStream.Color);
				}
				bool flag33 = (streams & ParticleSystemVertexStreams.UV) > ParticleSystemVertexStreams.None;
				if (flag33)
				{
					list.Remove(ParticleSystemVertexStream.UV);
				}
				bool flag34 = (streams & ParticleSystemVertexStreams.UV2BlendAndFrame) > ParticleSystemVertexStreams.None;
				if (flag34)
				{
					list.Remove(ParticleSystemVertexStream.UV2);
					list.Remove(ParticleSystemVertexStream.AnimBlend);
					list.Remove(ParticleSystemVertexStream.AnimFrame);
				}
				bool flag35 = (streams & ParticleSystemVertexStreams.CenterAndVertexID) > ParticleSystemVertexStreams.None;
				if (flag35)
				{
					list.Remove(ParticleSystemVertexStream.Center);
					list.Remove(ParticleSystemVertexStream.VertexID);
				}
				bool flag36 = (streams & ParticleSystemVertexStreams.Size) > ParticleSystemVertexStreams.None;
				if (flag36)
				{
					list.Remove(ParticleSystemVertexStream.SizeXYZ);
				}
				bool flag37 = (streams & ParticleSystemVertexStreams.Rotation) > ParticleSystemVertexStreams.None;
				if (flag37)
				{
					list.Remove(ParticleSystemVertexStream.Rotation3D);
				}
				bool flag38 = (streams & ParticleSystemVertexStreams.Velocity) > ParticleSystemVertexStreams.None;
				if (flag38)
				{
					list.Remove(ParticleSystemVertexStream.Velocity);
				}
				bool flag39 = (streams & ParticleSystemVertexStreams.Lifetime) > ParticleSystemVertexStreams.None;
				if (flag39)
				{
					list.Remove(ParticleSystemVertexStream.AgePercent);
					list.Remove(ParticleSystemVertexStream.InvStartLifetime);
				}
				bool flag40 = (streams & ParticleSystemVertexStreams.Custom1) > ParticleSystemVertexStreams.None;
				if (flag40)
				{
					list.Remove(ParticleSystemVertexStream.Custom1XYZW);
				}
				bool flag41 = (streams & ParticleSystemVertexStreams.Custom2) > ParticleSystemVertexStreams.None;
				if (flag41)
				{
					list.Remove(ParticleSystemVertexStream.Custom2XYZW);
				}
				bool flag42 = (streams & ParticleSystemVertexStreams.Random) > ParticleSystemVertexStreams.None;
				if (flag42)
				{
					list.Remove(ParticleSystemVertexStream.StableRandomXYZW);
					list.Remove(ParticleSystemVertexStream.VaryingRandomX);
				}
			}
			this.SetActiveVertexStreams(list);
		}

		[Obsolete("Internal_GetVertexStreams is deprecated.Use GetActiveVertexStreams instead.", false)]
		internal ParticleSystemVertexStreams Internal_GetEnabledVertexStreams(ParticleSystemVertexStreams streams)
		{
			List<ParticleSystemVertexStream> list = new List<ParticleSystemVertexStream>(this.activeVertexStreamsCount);
			this.GetActiveVertexStreams(list);
			ParticleSystemVertexStreams particleSystemVertexStreams = ParticleSystemVertexStreams.None;
			bool flag = list.Contains(ParticleSystemVertexStream.Position);
			if (flag)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Position;
			}
			bool flag2 = list.Contains(ParticleSystemVertexStream.Normal);
			if (flag2)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Normal;
			}
			bool flag3 = list.Contains(ParticleSystemVertexStream.Tangent);
			if (flag3)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Tangent;
			}
			bool flag4 = list.Contains(ParticleSystemVertexStream.Color);
			if (flag4)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Color;
			}
			bool flag5 = list.Contains(ParticleSystemVertexStream.UV);
			if (flag5)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.UV;
			}
			bool flag6 = list.Contains(ParticleSystemVertexStream.UV2);
			if (flag6)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.UV2BlendAndFrame;
			}
			bool flag7 = list.Contains(ParticleSystemVertexStream.Center);
			if (flag7)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.CenterAndVertexID;
			}
			bool flag8 = list.Contains(ParticleSystemVertexStream.SizeXYZ);
			if (flag8)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Size;
			}
			bool flag9 = list.Contains(ParticleSystemVertexStream.Rotation3D);
			if (flag9)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Rotation;
			}
			bool flag10 = list.Contains(ParticleSystemVertexStream.Velocity);
			if (flag10)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Velocity;
			}
			bool flag11 = list.Contains(ParticleSystemVertexStream.AgePercent);
			if (flag11)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Lifetime;
			}
			bool flag12 = list.Contains(ParticleSystemVertexStream.Custom1XYZW);
			if (flag12)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Custom1;
			}
			bool flag13 = list.Contains(ParticleSystemVertexStream.Custom2XYZW);
			if (flag13)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Custom2;
			}
			bool flag14 = list.Contains(ParticleSystemVertexStream.StableRandomXYZ);
			if (flag14)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Random;
			}
			return particleSystemVertexStreams & streams;
		}

		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::GetMeshes", HasExplicitThis = true), RequiredByNativeCode]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetMeshes([NotNull("ArgumentNullException")] [Out] Mesh[] meshes);

		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::SetMeshes", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetMeshes([NotNull("ArgumentNullException")] Mesh[] meshes, int size);

		public void SetMeshes(Mesh[] meshes)
		{
			this.SetMeshes(meshes, meshes.Length);
		}

		public void BakeMesh(Mesh mesh, bool useTransform = false)
		{
			this.BakeMesh(mesh, Camera.main, useTransform);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void BakeMesh([NotNull("ArgumentNullException")] Mesh mesh, [NotNull("ArgumentNullException")] Camera camera, bool useTransform = false);

		public void BakeTrailsMesh(Mesh mesh, bool useTransform = false)
		{
			this.BakeTrailsMesh(mesh, Camera.main, useTransform);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void BakeTrailsMesh([NotNull("ArgumentNullException")] Mesh mesh, [NotNull("ArgumentNullException")] Camera camera, bool useTransform = false);

		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::SetActiveVertexStreams", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetActiveVertexStreams([NotNull("ArgumentNullException")] List<ParticleSystemVertexStream> streams);

		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::GetActiveVertexStreams", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetActiveVertexStreams([NotNull("ArgumentNullException")] List<ParticleSystemVertexStream> streams);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_pivot_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_pivot_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_flip_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_flip_Injected(ref Vector3 value);
	}
}
