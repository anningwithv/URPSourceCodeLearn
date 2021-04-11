using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Profiling;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	internal class UIRAtlasManager : IDisposable
	{
		public struct ReadOnlyList<T> : IEnumerable<T>, IEnumerable
		{
			private List<T> m_List;

			public int Count
			{
				get
				{
					return this.m_List.Count;
				}
			}

			public T this[int i]
			{
				get
				{
					return this.m_List[i];
				}
			}

			public ReadOnlyList(List<T> list)
			{
				this.m_List = list;
			}

			public IEnumerator<T> GetEnumerator()
			{
				return this.m_List.GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.m_List.GetEnumerator();
			}
		}

		private static List<UIRAtlasManager> s_Instances = new List<UIRAtlasManager>();

		private static UIRAtlasManager.ReadOnlyList<UIRAtlasManager> s_InstancesreadOnly = new UIRAtlasManager.ReadOnlyList<UIRAtlasManager>(UIRAtlasManager.s_Instances);

		private int m_InitialSize;

		private UIRAtlasAllocator m_Allocator;

		private Dictionary<Texture2D, RectInt> m_UVs;

		private bool m_ForceReblitAll;

		private bool m_FloatFormat;

		private FilterMode m_FilterMode;

		private ColorSpace m_ColorSpace;

		private TextureBlitter m_Blitter;

		private int m_2SidePadding;

		private int m_1SidePadding;

		private static ProfilerMarker s_MarkerReset = new ProfilerMarker("UIR.AtlasManager.Reset");

		private static int s_TextureCounter;

		private static int s_GlobalResetVersion;

		private int m_ResetVersion = UIRAtlasManager.s_GlobalResetVersion;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<UIRAtlasManager> atlasManagerCreated;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<UIRAtlasManager> atlasManagerDisposed;

		public int maxImageSize
		{
			[CompilerGenerated]
			get
			{
				return this.<maxImageSize>k__BackingField;
			}
		}

		public RenderTextureFormat format
		{
			[CompilerGenerated]
			get
			{
				return this.<format>k__BackingField;
			}
		}

		public RenderTexture atlas
		{
			get;
			private set;
		}

		protected bool disposed
		{
			get;
			private set;
		}

		public static UIRAtlasManager.ReadOnlyList<UIRAtlasManager> Instances()
		{
			return UIRAtlasManager.s_InstancesreadOnly;
		}

		public UIRAtlasManager(RenderTextureFormat format = RenderTextureFormat.ARGB32, FilterMode filterMode = FilterMode.Bilinear, int maxImageSize = 64, int initialSize = 64)
		{
			bool flag = filterMode != FilterMode.Bilinear && filterMode > FilterMode.Point;
			if (flag)
			{
				throw new NotSupportedException("The only supported atlas filter modes are point or bilinear");
			}
			this.<format>k__BackingField = format;
			this.<maxImageSize>k__BackingField = maxImageSize;
			this.m_FloatFormat = (format == RenderTextureFormat.ARGBFloat);
			this.m_FilterMode = filterMode;
			this.m_UVs = new Dictionary<Texture2D, RectInt>(64);
			this.m_Blitter = new TextureBlitter(64);
			this.m_InitialSize = initialSize;
			this.m_2SidePadding = ((filterMode == FilterMode.Point) ? 0 : 2);
			this.m_1SidePadding = ((filterMode == FilterMode.Point) ? 0 : 1);
			this.Reset();
			UIRAtlasManager.s_Instances.Add(this);
			bool flag2 = UIRAtlasManager.atlasManagerCreated != null;
			if (flag2)
			{
				UIRAtlasManager.atlasManagerCreated(this);
			}
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			UIRAtlasManager.s_Instances.Remove(this);
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					UIRUtility.Destroy(this.atlas);
					this.atlas = null;
					bool flag = this.m_Allocator != null;
					if (flag)
					{
						this.m_Allocator.Dispose();
						this.m_Allocator = null;
					}
					bool flag2 = this.m_Blitter != null;
					if (flag2)
					{
						this.m_Blitter.Dispose();
						this.m_Blitter = null;
					}
					bool flag3 = UIRAtlasManager.atlasManagerDisposed != null;
					if (flag3)
					{
						UIRAtlasManager.atlasManagerDisposed(this);
					}
				}
				this.disposed = true;
			}
		}

		private static void LogDisposeError()
		{
			UnityEngine.Debug.LogError("An attempt to use a disposed atlas manager has been detected.");
		}

		public static void MarkAllForReset()
		{
			UIRAtlasManager.s_GlobalResetVersion++;
		}

		public void MarkForReset()
		{
			this.m_ResetVersion = UIRAtlasManager.s_GlobalResetVersion - 1;
		}

		public bool RequiresReset()
		{
			return this.m_ResetVersion != UIRAtlasManager.s_GlobalResetVersion;
		}

		public bool IsReleased()
		{
			return this.atlas != null && !this.atlas.IsCreated();
		}

		public void Reset()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				UIRAtlasManager.LogDisposeError();
			}
			else
			{
				UIRAtlasManager.s_MarkerReset.Begin();
				this.m_Blitter.Reset();
				this.m_UVs.Clear();
				this.m_Allocator = new UIRAtlasAllocator(this.m_InitialSize, 4096, this.m_1SidePadding);
				this.m_ForceReblitAll = false;
				this.m_ColorSpace = QualitySettings.activeColorSpace;
				UIRUtility.Destroy(this.atlas);
				this.atlas = null;
				UIRAtlasManager.s_MarkerReset.End();
				this.m_ResetVersion = UIRAtlasManager.s_GlobalResetVersion;
			}
		}

		public bool TryGetLocation(Texture2D image, out RectInt uvs)
		{
			uvs = default(RectInt);
			bool disposed = this.disposed;
			bool result;
			if (disposed)
			{
				UIRAtlasManager.LogDisposeError();
				result = false;
			}
			else
			{
				bool flag = image == null;
				if (flag)
				{
					result = false;
				}
				else
				{
					bool flag2 = this.m_UVs.TryGetValue(image, out uvs);
					if (flag2)
					{
						result = true;
					}
					else
					{
						bool flag3 = !this.IsTextureValid(image);
						if (flag3)
						{
							result = false;
						}
						else
						{
							bool flag4 = !this.AllocateRect(image.width, image.height, out uvs);
							if (flag4)
							{
								result = false;
							}
							else
							{
								this.m_UVs[image] = uvs;
								this.m_Blitter.QueueBlit(image, new RectInt(0, 0, image.width, image.height), new Vector2Int(uvs.x, uvs.y), true, Color.white);
								result = true;
							}
						}
					}
				}
			}
			return result;
		}

		public bool AllocateRect(int width, int height, out RectInt uvs)
		{
			bool flag = !this.m_Allocator.TryAllocate(width + this.m_2SidePadding, height + this.m_2SidePadding, out uvs);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				uvs = new RectInt(uvs.x + this.m_1SidePadding, uvs.y + this.m_1SidePadding, width, height);
				result = true;
			}
			return result;
		}

		public void EnqueueBlit(Texture image, int x, int y, bool addBorder, Color tint)
		{
			this.m_Blitter.QueueBlit(image, new RectInt(0, 0, image.width, image.height), new Vector2Int(x, y), addBorder, tint);
		}

		public static bool IsTextureFormatSupported(TextureFormat format)
		{
			switch (format)
			{
			case TextureFormat.Alpha8:
			case TextureFormat.ARGB4444:
			case TextureFormat.RGB24:
			case TextureFormat.RGBA32:
			case TextureFormat.ARGB32:
			case TextureFormat.RGB565:
			case TextureFormat.R16:
			case TextureFormat.DXT1:
			case TextureFormat.DXT5:
			case TextureFormat.RGBA4444:
			case TextureFormat.BGRA32:
			case TextureFormat.BC7:
			case TextureFormat.BC4:
			case TextureFormat.BC5:
			case TextureFormat.DXT1Crunched:
			case TextureFormat.DXT5Crunched:
			case TextureFormat.PVRTC_RGB2:
			case TextureFormat.PVRTC_RGBA2:
			case TextureFormat.PVRTC_RGB4:
			case TextureFormat.PVRTC_RGBA4:
			case TextureFormat.ETC_RGB4:
			case TextureFormat.EAC_R:
			case TextureFormat.EAC_R_SIGNED:
			case TextureFormat.EAC_RG:
			case TextureFormat.EAC_RG_SIGNED:
			case TextureFormat.ETC2_RGB:
			case TextureFormat.ETC2_RGBA1:
			case TextureFormat.ETC2_RGBA8:
			case TextureFormat.ASTC_4x4:
			case TextureFormat.ASTC_5x5:
			case TextureFormat.ASTC_6x6:
			case TextureFormat.ASTC_8x8:
			case TextureFormat.ASTC_10x10:
			case TextureFormat.ASTC_12x12:
			case TextureFormat.ASTC_RGBA_4x4:
			case TextureFormat.ASTC_RGBA_5x5:
			case TextureFormat.ASTC_RGBA_6x6:
			case TextureFormat.ASTC_RGBA_8x8:
			case TextureFormat.ASTC_RGBA_10x10:
			case TextureFormat.ASTC_RGBA_12x12:
			case TextureFormat.ETC_RGB4_3DS:
			case TextureFormat.ETC_RGBA8_3DS:
			case TextureFormat.RG16:
			case TextureFormat.R8:
			case TextureFormat.ETC_RGB4Crunched:
			case TextureFormat.ETC2_RGBA8Crunched:
			{
				bool result = true;
				return result;
			}
			case TextureFormat.RHalf:
			case TextureFormat.RGHalf:
			case TextureFormat.RGBAHalf:
			case TextureFormat.RFloat:
			case TextureFormat.RGFloat:
			case TextureFormat.RGBAFloat:
			case TextureFormat.YUY2:
			case TextureFormat.RGB9e5Float:
			case TextureFormat.BC6H:
			case TextureFormat.ASTC_HDR_4x4:
			case TextureFormat.ASTC_HDR_5x5:
			case TextureFormat.ASTC_HDR_6x6:
			case TextureFormat.ASTC_HDR_8x8:
			case TextureFormat.ASTC_HDR_10x10:
			case TextureFormat.ASTC_HDR_12x12:
			case TextureFormat.RG32:
			case TextureFormat.RGB48:
			case TextureFormat.RGBA64:
			{
				bool result = false;
				return result;
			}
			}
			throw new NotImplementedException(string.Format("The support of texture format '{0}' is undefined.", format));
		}

		private bool IsTextureValid(Texture2D image)
		{
			bool isReadable = image.isReadable;
			bool result;
			if (isReadable)
			{
				result = false;
			}
			else
			{
				bool flag = image.width > this.maxImageSize || image.height > this.maxImageSize;
				if (flag)
				{
					result = false;
				}
				else
				{
					bool flag2 = !UIRAtlasManager.IsTextureFormatSupported(image.format);
					if (flag2)
					{
						result = false;
					}
					else
					{
						bool flag3 = !this.m_FloatFormat && this.m_ColorSpace == ColorSpace.Linear && image.activeTextureColorSpace > ColorSpace.Gamma;
						if (flag3)
						{
							result = false;
						}
						else
						{
							bool flag4 = SystemInfo.graphicsShaderLevel >= 35;
							if (flag4)
							{
								bool flag5 = image.filterMode != FilterMode.Bilinear && image.filterMode > FilterMode.Point;
								if (flag5)
								{
									result = false;
									return result;
								}
							}
							else
							{
								bool flag6 = this.m_FilterMode != image.filterMode;
								if (flag6)
								{
									result = false;
									return result;
								}
							}
							bool flag7 = image.wrapMode != TextureWrapMode.Clamp;
							result = !flag7;
						}
					}
				}
			}
			return result;
		}

		public void Commit()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				UIRAtlasManager.LogDisposeError();
			}
			else
			{
				this.UpdateAtlasTexture();
				bool forceReblitAll = this.m_ForceReblitAll;
				if (forceReblitAll)
				{
					this.m_ForceReblitAll = false;
					this.m_Blitter.Reset();
					foreach (KeyValuePair<Texture2D, RectInt> current in this.m_UVs)
					{
						this.m_Blitter.QueueBlit(current.Key, new RectInt(0, 0, current.Key.width, current.Key.height), new Vector2Int(current.Value.x, current.Value.y), true, Color.white);
					}
				}
				this.m_Blitter.Commit(this.atlas);
			}
		}

		private void UpdateAtlasTexture()
		{
			bool flag = this.atlas == null;
			if (flag)
			{
				bool flag2 = this.m_UVs.Count > this.m_Blitter.queueLength;
				if (flag2)
				{
					this.m_ForceReblitAll = true;
				}
				this.atlas = this.CreateAtlasTexture();
			}
			else
			{
				bool flag3 = this.atlas.width != this.m_Allocator.physicalWidth || this.atlas.height != this.m_Allocator.physicalHeight;
				if (flag3)
				{
					RenderTexture renderTexture = this.CreateAtlasTexture();
					bool flag4 = renderTexture == null;
					if (flag4)
					{
						UnityEngine.Debug.LogErrorFormat("Failed to allocate a render texture for the dynamic atlas. Current Size = {0}x{1}. Requested Size = {2}x{3}.", new object[]
						{
							this.atlas.width,
							this.atlas.height,
							this.m_Allocator.physicalWidth,
							this.m_Allocator.physicalHeight
						});
					}
					else
					{
						this.m_Blitter.BlitOneNow(renderTexture, this.atlas, new RectInt(0, 0, this.atlas.width, this.atlas.height), new Vector2Int(0, 0), false, Color.white);
					}
					UIRUtility.Destroy(this.atlas);
					this.atlas = renderTexture;
				}
			}
		}

		private RenderTexture CreateAtlasTexture()
		{
			bool flag = this.m_Allocator.physicalWidth == 0 || this.m_Allocator.physicalHeight == 0;
			RenderTexture result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new RenderTexture(this.m_Allocator.physicalWidth, this.m_Allocator.physicalHeight, 0, this.format)
				{
					hideFlags = HideFlags.HideAndDontSave,
					name = "UIR Atlas " + UIRAtlasManager.s_TextureCounter++.ToString(),
					filterMode = this.m_FilterMode
				};
			}
			return result;
		}
	}
}
