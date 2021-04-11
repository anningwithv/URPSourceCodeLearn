using System;
using System.Collections.Generic;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR
{
	internal class VectorImageManager : IDisposable
	{
		private static ProfilerMarker s_MarkerRegister = new ProfilerMarker("UIR.VectorImageManager.Register");

		private static ProfilerMarker s_MarkerUnregister = new ProfilerMarker("UIR.VectorImageManager.Unregister");

		private readonly UIRAtlasManager m_AtlasManager;

		private Dictionary<VectorImage, VectorImageRenderInfo> m_Registered;

		private VectorImageRenderInfoPool m_RenderInfoPool;

		private GradientRemapPool m_GradientRemapPool;

		private GradientSettingsAtlas m_GradientSettingsAtlas;

		private bool m_LoggedExhaustedSettingsAtlas;

		private static int s_GlobalResetVersion;

		private int m_ResetVersion = VectorImageManager.s_GlobalResetVersion;

		public Texture2D atlas
		{
			get
			{
				GradientSettingsAtlas expr_07 = this.m_GradientSettingsAtlas;
				return (expr_07 != null) ? expr_07.atlas : null;
			}
		}

		protected bool disposed
		{
			get;
			private set;
		}

		public VectorImageManager(UIRAtlasManager atlasManager)
		{
			this.m_AtlasManager = atlasManager;
			this.m_Registered = new Dictionary<VectorImage, VectorImageRenderInfo>(32);
			this.m_RenderInfoPool = new VectorImageRenderInfoPool();
			this.m_GradientRemapPool = new GradientRemapPool();
			this.m_GradientSettingsAtlas = new GradientSettingsAtlas(4096);
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
					this.m_Registered.Clear();
					this.m_RenderInfoPool.Clear();
					this.m_GradientRemapPool.Clear();
					this.m_GradientSettingsAtlas.Dispose();
				}
				this.disposed = true;
			}
		}

		public static void MarkAllForReset()
		{
			VectorImageManager.s_GlobalResetVersion++;
		}

		public void MarkForReset()
		{
			this.m_ResetVersion = VectorImageManager.s_GlobalResetVersion - 1;
		}

		public bool RequiresReset()
		{
			return this.m_ResetVersion != VectorImageManager.s_GlobalResetVersion;
		}

		public void Reset()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_Registered.Clear();
				this.m_RenderInfoPool.Clear();
				this.m_GradientRemapPool.Clear();
				this.m_GradientSettingsAtlas.Reset();
				this.m_ResetVersion = VectorImageManager.s_GlobalResetVersion;
			}
		}

		public void Commit()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_GradientSettingsAtlas.Commit();
			}
		}

		public GradientRemap AddUser(VectorImage vi)
		{
			bool disposed = this.disposed;
			GradientRemap result;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
				result = null;
			}
			else
			{
				bool flag = vi == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					VectorImageRenderInfo vectorImageRenderInfo;
					bool flag2 = this.m_Registered.TryGetValue(vi, out vectorImageRenderInfo);
					if (flag2)
					{
						vectorImageRenderInfo.useCount++;
					}
					else
					{
						vectorImageRenderInfo = this.Register(vi);
					}
					result = vectorImageRenderInfo.firstGradientRemap;
				}
			}
			return result;
		}

		public void RemoveUser(VectorImage vi)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				bool flag = vi == null;
				if (!flag)
				{
					VectorImageRenderInfo vectorImageRenderInfo;
					bool flag2 = this.m_Registered.TryGetValue(vi, out vectorImageRenderInfo);
					if (flag2)
					{
						vectorImageRenderInfo.useCount--;
						bool flag3 = vectorImageRenderInfo.useCount == 0;
						if (flag3)
						{
							this.Unregister(vi, vectorImageRenderInfo);
						}
					}
				}
			}
		}

		private VectorImageRenderInfo Register(VectorImage vi)
		{
			VectorImageManager.s_MarkerRegister.Begin();
			VectorImageRenderInfo vectorImageRenderInfo = this.m_RenderInfoPool.Get();
			vectorImageRenderInfo.useCount = 1;
			this.m_Registered[vi] = vectorImageRenderInfo;
			GradientSettings[] expr_33 = vi.settings;
			bool flag = expr_33 != null && expr_33.Length != 0;
			if (flag)
			{
				int num = vi.settings.Length;
				Alloc alloc = this.m_GradientSettingsAtlas.Add(num);
				bool flag2 = alloc.size > 0u;
				if (flag2)
				{
					RectInt rectInt;
					bool flag3 = this.m_AtlasManager.TryGetLocation(vi.atlas, out rectInt);
					if (flag3)
					{
						GradientRemap gradientRemap = null;
						for (int i = 0; i < num; i++)
						{
							GradientRemap gradientRemap2 = this.m_GradientRemapPool.Get();
							bool flag4 = i > 0;
							if (flag4)
							{
								gradientRemap.next = gradientRemap2;
							}
							else
							{
								vectorImageRenderInfo.firstGradientRemap = gradientRemap2;
							}
							gradientRemap = gradientRemap2;
							gradientRemap2.origIndex = i;
							gradientRemap2.destIndex = (int)(alloc.start + (uint)i);
							GradientSettings gradientSettings = vi.settings[i];
							RectInt location = gradientSettings.location;
							location.x += rectInt.x;
							location.y += rectInt.y;
							gradientRemap2.location = location;
							gradientRemap2.isAtlassed = true;
						}
						this.m_GradientSettingsAtlas.Write(alloc, vi.settings, vectorImageRenderInfo.firstGradientRemap);
					}
					else
					{
						GradientRemap gradientRemap3 = null;
						for (int j = 0; j < num; j++)
						{
							GradientRemap gradientRemap4 = this.m_GradientRemapPool.Get();
							bool flag5 = j > 0;
							if (flag5)
							{
								gradientRemap3.next = gradientRemap4;
							}
							else
							{
								vectorImageRenderInfo.firstGradientRemap = gradientRemap4;
							}
							gradientRemap3 = gradientRemap4;
							gradientRemap4.origIndex = j;
							gradientRemap4.destIndex = (int)(alloc.start + (uint)j);
							gradientRemap4.isAtlassed = false;
						}
						this.m_GradientSettingsAtlas.Write(alloc, vi.settings, null);
					}
				}
				else
				{
					bool flag6 = !this.m_LoggedExhaustedSettingsAtlas;
					if (flag6)
					{
						string arg_233_0 = "Exhausted max gradient settings (";
						string arg_233_1 = this.m_GradientSettingsAtlas.length.ToString();
						string arg_233_2 = ") for atlas: ";
						Texture2D expr_227 = this.m_GradientSettingsAtlas.atlas;
						Debug.LogError(arg_233_0 + arg_233_1 + arg_233_2 + ((expr_227 != null) ? expr_227.name : null));
						this.m_LoggedExhaustedSettingsAtlas = true;
					}
				}
			}
			VectorImageManager.s_MarkerRegister.End();
			return vectorImageRenderInfo;
		}

		private void Unregister(VectorImage vi, VectorImageRenderInfo renderInfo)
		{
			VectorImageManager.s_MarkerUnregister.Begin();
			bool flag = renderInfo.gradientSettingsAlloc.size > 0u;
			if (flag)
			{
				this.m_GradientSettingsAtlas.Remove(renderInfo.gradientSettingsAlloc);
			}
			GradientRemap next;
			for (GradientRemap gradientRemap = renderInfo.firstGradientRemap; gradientRemap != null; gradientRemap = next)
			{
				next = gradientRemap.next;
				this.m_GradientRemapPool.Return(gradientRemap);
			}
			this.m_Registered.Remove(vi);
			this.m_RenderInfoPool.Return(renderInfo);
			VectorImageManager.s_MarkerUnregister.End();
		}
	}
}
