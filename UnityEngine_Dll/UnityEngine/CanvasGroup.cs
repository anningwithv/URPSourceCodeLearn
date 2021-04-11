using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/UI/CanvasGroup.h"), NativeClass("UI::CanvasGroup")]
	public sealed class CanvasGroup : Behaviour, ICanvasRaycastFilter
	{
		[NativeProperty("Alpha", false, TargetType.Function)]
		public extern float alpha
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("Interactable", false, TargetType.Function)]
		public extern bool interactable
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("BlocksRaycasts", false, TargetType.Function)]
		public extern bool blocksRaycasts
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("IgnoreParentGroups", false, TargetType.Function)]
		public extern bool ignoreParentGroups
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
		{
			return this.blocksRaycasts;
		}
	}
}
