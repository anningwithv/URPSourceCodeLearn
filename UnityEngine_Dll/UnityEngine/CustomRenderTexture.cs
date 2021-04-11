using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/CustomRenderTexture.h"), UsedByNativeCode]
	public sealed class CustomRenderTexture : RenderTexture
	{
		public extern Material material
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Material initializationMaterial
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Texture initializationTexture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern CustomRenderTextureInitializationSource initializationSource
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Color initializationColor
		{
			get
			{
				Color result;
				this.get_initializationColor_Injected(out result);
				return result;
			}
			set
			{
				this.set_initializationColor_Injected(ref value);
			}
		}

		public extern CustomRenderTextureUpdateMode updateMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern CustomRenderTextureUpdateMode initializationMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern CustomRenderTextureUpdateZoneSpace updateZoneSpace
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int shaderPass
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern uint cubemapFaceMask
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool doubleBuffered
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool wrapUpdateZones
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float updatePeriod
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[FreeFunction(Name = "CustomRenderTextureScripting::Create")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateCustomRenderTexture([Writable] CustomRenderTexture rt);

		[NativeName("TriggerUpdate")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void TriggerUpdate(int count);

		public void Update(int count)
		{
			CustomRenderTextureManager.InvokeTriggerUpdate(this, count);
			this.TriggerUpdate(count);
		}

		public void Update()
		{
			this.Update(1);
		}

		[NativeName("TriggerInitialization")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void TriggerInitialization();

		public void Initialize()
		{
			this.TriggerInitialization();
			CustomRenderTextureManager.InvokeTriggerInitialize(this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ClearUpdateZones();

		[FreeFunction(Name = "CustomRenderTextureScripting::GetUpdateZonesInternal", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void GetUpdateZonesInternal([NotNull("ArgumentNullException")] object updateZones);

		public void GetUpdateZones(List<CustomRenderTextureUpdateZone> updateZones)
		{
			this.GetUpdateZonesInternal(updateZones);
		}

		[FreeFunction(Name = "CustomRenderTextureScripting::SetUpdateZonesInternal", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetUpdateZonesInternal(CustomRenderTextureUpdateZone[] updateZones);

		[FreeFunction(Name = "CustomRenderTextureScripting::GetDoubleBufferRenderTexture", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern RenderTexture GetDoubleBufferRenderTexture();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void EnsureDoubleBufferConsistency();

		public void SetUpdateZones(CustomRenderTextureUpdateZone[] updateZones)
		{
			bool flag = updateZones == null;
			if (flag)
			{
				throw new ArgumentNullException("updateZones");
			}
			this.SetUpdateZonesInternal(updateZones);
		}

		public CustomRenderTexture(int width, int height, RenderTextureFormat format, RenderTextureReadWrite readWrite) : this(width, height, RenderTexture.GetCompatibleFormat(format, readWrite))
		{
		}

		public CustomRenderTexture(int width, int height, RenderTextureFormat format) : this(width, height, RenderTexture.GetCompatibleFormat(format, RenderTextureReadWrite.Default))
		{
		}

		public CustomRenderTexture(int width, int height) : this(width, height, SystemInfo.GetGraphicsFormat(DefaultFormat.LDR))
		{
		}

		public CustomRenderTexture(int width, int height, DefaultFormat defaultFormat) : this(width, height, SystemInfo.GetGraphicsFormat(defaultFormat))
		{
		}

		public CustomRenderTexture(int width, int height, GraphicsFormat format)
		{
			bool flag = !base.ValidateFormat(format, FormatUsage.Render);
			if (!flag)
			{
				CustomRenderTexture.Internal_CreateCustomRenderTexture(this);
				this.width = width;
				this.height = height;
				base.graphicsFormat = format;
				base.SetSRGBReadWrite(GraphicsFormatUtility.IsSRGBFormat(format));
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_initializationColor_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_initializationColor_Injected(ref Color value);
	}
}
