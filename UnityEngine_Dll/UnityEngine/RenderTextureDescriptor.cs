using System;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace UnityEngine
{
	public struct RenderTextureDescriptor
	{
		private GraphicsFormat _graphicsFormat;

		private int _depthBufferBits;

		private static int[] depthFormatBits = new int[]
		{
			0,
			16,
			24
		};

		private RenderTextureCreationFlags _flags;

		public int width
		{
			[IsReadOnly]
			get;
			set;
		}

		public int height
		{
			[IsReadOnly]
			get;
			set;
		}

		public int msaaSamples
		{
			[IsReadOnly]
			get;
			set;
		}

		public int volumeDepth
		{
			[IsReadOnly]
			get;
			set;
		}

		public int mipCount
		{
			[IsReadOnly]
			get;
			set;
		}

		public GraphicsFormat graphicsFormat
		{
			get
			{
				return this._graphicsFormat;
			}
			set
			{
				this._graphicsFormat = value;
				this.SetOrClearRenderTextureCreationFlag(GraphicsFormatUtility.IsSRGBFormat(value), RenderTextureCreationFlags.SRGB);
			}
		}

		public GraphicsFormat stencilFormat
		{
			[IsReadOnly]
			get;
			set;
		}

		public RenderTextureFormat colorFormat
		{
			get
			{
				return GraphicsFormatUtility.GetRenderTextureFormat(this.graphicsFormat);
			}
			set
			{
				this.graphicsFormat = SystemInfo.GetCompatibleFormat(GraphicsFormatUtility.GetGraphicsFormat(value, this.sRGB), FormatUsage.Render);
			}
		}

		public bool sRGB
		{
			get
			{
				return GraphicsFormatUtility.IsSRGBFormat(this.graphicsFormat);
			}
			set
			{
				this.graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(this.colorFormat, value);
			}
		}

		public int depthBufferBits
		{
			get
			{
				return RenderTextureDescriptor.depthFormatBits[this._depthBufferBits];
			}
			set
			{
				bool flag = value <= 0;
				if (flag)
				{
					this._depthBufferBits = 0;
				}
				else
				{
					bool flag2 = value <= 16;
					if (flag2)
					{
						this._depthBufferBits = 1;
					}
					else
					{
						this._depthBufferBits = 2;
					}
				}
			}
		}

		public TextureDimension dimension
		{
			[IsReadOnly]
			get;
			set;
		}

		public ShadowSamplingMode shadowSamplingMode
		{
			[IsReadOnly]
			get;
			set;
		}

		public VRTextureUsage vrUsage
		{
			[IsReadOnly]
			get;
			set;
		}

		public RenderTextureCreationFlags flags
		{
			get
			{
				return this._flags;
			}
		}

		public RenderTextureMemoryless memoryless
		{
			[IsReadOnly]
			get;
			set;
		}

		public bool useMipMap
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.MipMap) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.MipMap);
			}
		}

		public bool autoGenerateMips
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.AutoGenerateMips) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.AutoGenerateMips);
			}
		}

		public bool enableRandomWrite
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.EnableRandomWrite) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.EnableRandomWrite);
			}
		}

		public bool bindMS
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.BindMS) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.BindMS);
			}
		}

		internal bool createdFromScript
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.CreatedFromScript) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.CreatedFromScript);
			}
		}

		public bool useDynamicScale
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.DynamicallyScalable) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.DynamicallyScalable);
			}
		}

		public RenderTextureDescriptor(int width, int height)
		{
			this = new RenderTextureDescriptor(width, height, SystemInfo.GetGraphicsFormat(DefaultFormat.LDR), 0, Texture.GenerateAllMips);
		}

		public RenderTextureDescriptor(int width, int height, RenderTextureFormat colorFormat)
		{
			this = new RenderTextureDescriptor(width, height, colorFormat, 0);
		}

		public RenderTextureDescriptor(int width, int height, RenderTextureFormat colorFormat, int depthBufferBits)
		{
			this = new RenderTextureDescriptor(width, height, SystemInfo.GetCompatibleFormat(GraphicsFormatUtility.GetGraphicsFormat(colorFormat, false), FormatUsage.Render), depthBufferBits);
		}

		public RenderTextureDescriptor(int width, int height, GraphicsFormat colorFormat, int depthBufferBits)
		{
			this = new RenderTextureDescriptor(width, height, colorFormat, depthBufferBits, Texture.GenerateAllMips);
		}

		public RenderTextureDescriptor(int width, int height, RenderTextureFormat colorFormat, int depthBufferBits, int mipCount)
		{
			this = new RenderTextureDescriptor(width, height, SystemInfo.GetCompatibleFormat(GraphicsFormatUtility.GetGraphicsFormat(colorFormat, false), FormatUsage.Render), depthBufferBits, mipCount);
		}

		public RenderTextureDescriptor(int width, int height, GraphicsFormat colorFormat, int depthBufferBits, int mipCount)
		{
			this = default(RenderTextureDescriptor);
			this._flags = (RenderTextureCreationFlags.AutoGenerateMips | RenderTextureCreationFlags.AllowVerticalFlip);
			this.width = width;
			this.height = height;
			this.volumeDepth = 1;
			this.msaaSamples = 1;
			this.graphicsFormat = colorFormat;
			this.depthBufferBits = depthBufferBits;
			this.mipCount = mipCount;
			this.dimension = TextureDimension.Tex2D;
			this.shadowSamplingMode = ShadowSamplingMode.None;
			this.vrUsage = VRTextureUsage.None;
			this.memoryless = RenderTextureMemoryless.None;
		}

		private void SetOrClearRenderTextureCreationFlag(bool value, RenderTextureCreationFlags flag)
		{
			if (value)
			{
				this._flags |= flag;
			}
			else
			{
				this._flags &= ~flag;
			}
		}
	}
}
