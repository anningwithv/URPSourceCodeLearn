using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Modules/Terrain/Public/TerrainDataScriptingInterface.h"), NativeHeader("TerrainScriptingClasses.h"), UsedByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class DetailPrototype
	{
		internal GameObject m_Prototype = null;

		internal Texture2D m_PrototypeTexture = null;

		internal Color m_HealthyColor = new Color(0.2627451f, 0.9764706f, 0.164705887f, 1f);

		internal Color m_DryColor = new Color(0.8039216f, 0.7372549f, 0.101960786f, 1f);

		internal float m_MinWidth = 1f;

		internal float m_MaxWidth = 2f;

		internal float m_MinHeight = 1f;

		internal float m_MaxHeight = 2f;

		internal float m_NoiseSpread = 0.1f;

		internal float m_HoleEdgePadding = 0f;

		internal int m_RenderMode = 2;

		internal int m_UsePrototypeMesh = 0;

		public GameObject prototype
		{
			get
			{
				return this.m_Prototype;
			}
			set
			{
				this.m_Prototype = value;
			}
		}

		public Texture2D prototypeTexture
		{
			get
			{
				return this.m_PrototypeTexture;
			}
			set
			{
				this.m_PrototypeTexture = value;
			}
		}

		public float minWidth
		{
			get
			{
				return this.m_MinWidth;
			}
			set
			{
				this.m_MinWidth = value;
			}
		}

		public float maxWidth
		{
			get
			{
				return this.m_MaxWidth;
			}
			set
			{
				this.m_MaxWidth = value;
			}
		}

		public float minHeight
		{
			get
			{
				return this.m_MinHeight;
			}
			set
			{
				this.m_MinHeight = value;
			}
		}

		public float maxHeight
		{
			get
			{
				return this.m_MaxHeight;
			}
			set
			{
				this.m_MaxHeight = value;
			}
		}

		public float noiseSpread
		{
			get
			{
				return this.m_NoiseSpread;
			}
			set
			{
				this.m_NoiseSpread = value;
			}
		}

		[Obsolete("bendFactor has no effect and is deprecated.", false)]
		public float bendFactor
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float holeEdgePadding
		{
			get
			{
				return this.m_HoleEdgePadding;
			}
			set
			{
				this.m_HoleEdgePadding = value;
			}
		}

		public Color healthyColor
		{
			get
			{
				return this.m_HealthyColor;
			}
			set
			{
				this.m_HealthyColor = value;
			}
		}

		public Color dryColor
		{
			get
			{
				return this.m_DryColor;
			}
			set
			{
				this.m_DryColor = value;
			}
		}

		public DetailRenderMode renderMode
		{
			get
			{
				return (DetailRenderMode)this.m_RenderMode;
			}
			set
			{
				this.m_RenderMode = (int)value;
			}
		}

		public bool usePrototypeMesh
		{
			get
			{
				return this.m_UsePrototypeMesh != 0;
			}
			set
			{
				this.m_UsePrototypeMesh = (value ? 1 : 0);
			}
		}

		public DetailPrototype()
		{
		}

		public DetailPrototype(DetailPrototype other)
		{
			this.m_Prototype = other.m_Prototype;
			this.m_PrototypeTexture = other.m_PrototypeTexture;
			this.m_HealthyColor = other.m_HealthyColor;
			this.m_DryColor = other.m_DryColor;
			this.m_MinWidth = other.m_MinWidth;
			this.m_MaxWidth = other.m_MaxWidth;
			this.m_MinHeight = other.m_MinHeight;
			this.m_MaxHeight = other.m_MaxHeight;
			this.m_NoiseSpread = other.m_NoiseSpread;
			this.m_HoleEdgePadding = other.m_HoleEdgePadding;
			this.m_RenderMode = other.m_RenderMode;
			this.m_UsePrototypeMesh = other.m_UsePrototypeMesh;
		}

		public override bool Equals(object obj)
		{
			return this.Equals(obj as DetailPrototype);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		private bool Equals(DetailPrototype other)
		{
			bool flag = other == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = other == this;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = base.GetType() != other.GetType();
					if (flag3)
					{
						result = false;
					}
					else
					{
						bool flag4 = this.m_Prototype == other.m_Prototype && this.m_PrototypeTexture == other.m_PrototypeTexture && this.m_HealthyColor == other.m_HealthyColor && this.m_DryColor == other.m_DryColor && this.m_MinWidth == other.m_MinWidth && this.m_MaxWidth == other.m_MaxWidth && this.m_MinHeight == other.m_MinHeight && this.m_MaxHeight == other.m_MaxHeight && this.m_NoiseSpread == other.m_NoiseSpread && this.m_HoleEdgePadding == other.m_HoleEdgePadding && this.m_RenderMode == other.m_RenderMode && this.m_UsePrototypeMesh == other.m_UsePrototypeMesh;
						result = flag4;
					}
				}
			}
			return result;
		}

		public bool Validate()
		{
			string text;
			return DetailPrototype.ValidateDetailPrototype(this, out text);
		}

		public bool Validate(out string errorMessage)
		{
			return DetailPrototype.ValidateDetailPrototype(this, out errorMessage);
		}

		[FreeFunction("TerrainDataScriptingInterface::ValidateDetailPrototype")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool ValidateDetailPrototype([NotNull("ArgumentNullException")] DetailPrototype prototype, out string errorMessage);
	}
}
