using System;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR
{
	internal class GradientSettingsAtlas : IDisposable
	{
		private struct RawTexture
		{
			public Color32[] rgba;

			public int width;

			public int height;

			public void WriteRawInt2Packed(int v0, int v1, int destX, int destY)
			{
				byte b = (byte)(v0 / 255);
				byte g = (byte)(v0 - (int)(b * 255));
				byte b2 = (byte)(v1 / 255);
				byte a = (byte)(v1 - (int)(b2 * 255));
				int num = destY * this.width + destX;
				this.rgba[num] = new Color32(b, g, b2, a);
			}

			public void WriteRawFloat4Packed(float f0, float f1, float f2, float f3, int destX, int destY)
			{
				byte r = (byte)(f0 * 255f + 0.5f);
				byte g = (byte)(f1 * 255f + 0.5f);
				byte b = (byte)(f2 * 255f + 0.5f);
				byte a = (byte)(f3 * 255f + 0.5f);
				int num = destY * this.width + destX;
				this.rgba[num] = new Color32(r, g, b, a);
			}
		}

		private static ProfilerMarker s_MarkerWrite = new ProfilerMarker("UIR.GradientSettingsAtlas.Write");

		private static ProfilerMarker s_MarkerCommit = new ProfilerMarker("UIR.GradientSettingsAtlas.Commit");

		private readonly int m_Length;

		private readonly int m_ElemWidth;

		private BestFitAllocator m_Allocator;

		private Texture2D m_Atlas;

		private GradientSettingsAtlas.RawTexture m_RawAtlas;

		private static int s_TextureCounter;

		internal int length
		{
			get
			{
				return this.m_Length;
			}
		}

		protected bool disposed
		{
			get;
			private set;
		}

		public Texture2D atlas
		{
			get
			{
				return this.m_Atlas;
			}
		}

		public bool MustCommit
		{
			get;
			private set;
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
					UIRUtility.Destroy(this.m_Atlas);
				}
				this.disposed = true;
			}
		}

		public GradientSettingsAtlas(int length = 4096)
		{
			this.m_Length = length;
			this.m_ElemWidth = 3;
			this.Reset();
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
				this.m_Allocator = new BestFitAllocator((uint)this.m_Length);
				UIRUtility.Destroy(this.m_Atlas);
				this.m_RawAtlas = default(GradientSettingsAtlas.RawTexture);
				this.MustCommit = false;
			}
		}

		public Alloc Add(int count)
		{
			Debug.Assert(count > 0);
			bool disposed = this.disposed;
			Alloc result;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
				result = default(Alloc);
			}
			else
			{
				Alloc alloc = this.m_Allocator.Allocate((uint)count);
				result = alloc;
			}
			return result;
		}

		public void Remove(Alloc alloc)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_Allocator.Free(alloc);
			}
		}

		public void Write(Alloc alloc, GradientSettings[] settings, GradientRemap remap)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				bool flag = this.m_RawAtlas.rgba == null;
				if (flag)
				{
					this.m_RawAtlas = new GradientSettingsAtlas.RawTexture
					{
						rgba = new Color32[this.m_ElemWidth * this.m_Length],
						width = this.m_ElemWidth,
						height = this.m_Length
					};
					int num = this.m_ElemWidth * this.m_Length;
					for (int i = 0; i < num; i++)
					{
						this.m_RawAtlas.rgba[i] = Color.black;
					}
				}
				GradientSettingsAtlas.s_MarkerWrite.Begin();
				int num2 = (int)alloc.start;
				int j = 0;
				int num3 = settings.Length;
				while (j < num3)
				{
					int num4 = 0;
					GradientSettings gradientSettings = settings[j];
					Debug.Assert(remap == null || num2 == remap.destIndex);
					bool flag2 = gradientSettings.gradientType == GradientType.Radial;
					if (flag2)
					{
						Vector2 vector = gradientSettings.radialFocus;
						vector += Vector2.one;
						vector /= 2f;
						vector.y = 1f - vector.y;
						this.m_RawAtlas.WriteRawFloat4Packed(0.003921569f, (float)gradientSettings.addressMode / 255f, vector.x, vector.y, num4++, num2);
					}
					else
					{
						bool flag3 = gradientSettings.gradientType == GradientType.Linear;
						if (flag3)
						{
							this.m_RawAtlas.WriteRawFloat4Packed(0f, (float)gradientSettings.addressMode / 255f, 0f, 0f, num4++, num2);
						}
					}
					Vector2Int vector2Int = new Vector2Int(gradientSettings.location.x, gradientSettings.location.y);
					Vector2 vector2 = new Vector2((float)(gradientSettings.location.width - 1), (float)(gradientSettings.location.height - 1));
					bool flag4 = remap != null;
					if (flag4)
					{
						vector2Int = new Vector2Int(remap.location.x, remap.location.y);
						vector2 = new Vector2((float)(remap.location.width - 1), (float)(remap.location.height - 1));
					}
					this.m_RawAtlas.WriteRawInt2Packed(vector2Int.x, vector2Int.y, num4++, num2);
					this.m_RawAtlas.WriteRawInt2Packed((int)vector2.x, (int)vector2.y, num4++, num2);
					remap = ((remap != null) ? remap.next : null);
					num2++;
					j++;
				}
				this.MustCommit = true;
				GradientSettingsAtlas.s_MarkerWrite.End();
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
				bool flag = !this.MustCommit;
				if (!flag)
				{
					this.PrepareAtlas();
					GradientSettingsAtlas.s_MarkerCommit.Begin();
					this.m_Atlas.SetPixels32(this.m_RawAtlas.rgba);
					this.m_Atlas.Apply();
					GradientSettingsAtlas.s_MarkerCommit.End();
					this.MustCommit = false;
				}
			}
		}

		private void PrepareAtlas()
		{
			bool flag = this.m_Atlas != null;
			if (!flag)
			{
				this.m_Atlas = new Texture2D(this.m_ElemWidth, this.m_Length, TextureFormat.ARGB32, 0, true)
				{
					hideFlags = HideFlags.HideAndDontSave,
					name = "GradientSettings " + GradientSettingsAtlas.s_TextureCounter++.ToString(),
					filterMode = FilterMode.Point
				};
			}
		}
	}
}
