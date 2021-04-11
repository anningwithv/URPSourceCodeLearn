using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine
{
	[NativeHeader("Modules/Localization/Public/LocalizationAsset.h"), NativeHeader("Modules/Localization/Public/LocalizationAsset.bindings.h"), ExcludeFromPreset, NativeClass("LocalizationAsset"), MovedFrom("UnityEditor")]
	public sealed class LocalizationAsset : Object
	{
		public extern string localeIsoCode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool isEditorAsset
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public LocalizationAsset()
		{
			LocalizationAsset.Internal_CreateInstance(this);
		}

		[FreeFunction("Internal_CreateInstance")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateInstance([Writable] LocalizationAsset locAsset);

		[NativeMethod("StoreLocalizedString")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetLocalizedString(string original, string localized);

		[NativeMethod("GetLocalized")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetLocalizedString(string original);
	}
}
