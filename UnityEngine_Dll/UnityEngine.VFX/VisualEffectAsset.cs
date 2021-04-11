using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	[NativeHeader("VFXScriptingClasses.h"), NativeHeader("Modules/VFX/Public/VisualEffectAsset.h"), UsedByNativeCode]
	public class VisualEffectAsset : VisualEffectObject
	{
		public const string PlayEventName = "OnPlay";

		public const string StopEventName = "OnStop";

		public static readonly int PlayEventID = Shader.PropertyToID("OnPlay");

		public static readonly int StopEventID = Shader.PropertyToID("OnStop");

		internal static extern uint currentRuntimeDataVersion
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern uint GetCompilationVersion();

		[FreeFunction(Name = "VisualEffectAssetBindings::GetTextureDimension", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern TextureDimension GetTextureDimension(int nameID);

		[FreeFunction(Name = "VisualEffectAssetBindings::GetExposedProperties", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetExposedProperties([NotNull("ArgumentNullException")] List<VFXExposedProperty> exposedProperties);

		[FreeFunction(Name = "VisualEffectAssetBindings::GetEvents", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetEvents([NotNull("ArgumentNullException")] List<string> names);

		public TextureDimension GetTextureDimension(string name)
		{
			return this.GetTextureDimension(Shader.PropertyToID(name));
		}
	}
}
