using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Editor/Src/Properties/DrivenPropertyManager.h")]
	internal class DrivenPropertyManager
	{
		[Conditional("UNITY_EDITOR")]
		public static void RegisterProperty(Object driver, Object target, string propertyPath)
		{
			DrivenPropertyManager.RegisterPropertyPartial(driver, target, propertyPath);
		}

		[Conditional("UNITY_EDITOR")]
		public static void TryRegisterProperty(Object driver, Object target, string propertyPath)
		{
			DrivenPropertyManager.TryRegisterPropertyPartial(driver, target, propertyPath);
		}

		[Conditional("UNITY_EDITOR")]
		public static void UnregisterProperty(Object driver, Object target, string propertyPath)
		{
			DrivenPropertyManager.UnregisterPropertyPartial(driver, target, propertyPath);
		}

		[Conditional("UNITY_EDITOR"), NativeConditional("UNITY_EDITOR"), StaticAccessor("GetDrivenPropertyManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UnregisterProperties([NotNull("ArgumentNullException")] Object driver);

		[NativeConditional("UNITY_EDITOR"), StaticAccessor("GetDrivenPropertyManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RegisterPropertyPartial([NotNull("ArgumentNullException")] Object driver, [NotNull("ArgumentNullException")] Object target, [NotNull("ArgumentNullException")] string propertyPath);

		[NativeConditional("UNITY_EDITOR"), StaticAccessor("GetDrivenPropertyManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TryRegisterPropertyPartial([NotNull("ArgumentNullException")] Object driver, [NotNull("ArgumentNullException")] Object target, [NotNull("ArgumentNullException")] string propertyPath);

		[NativeConditional("UNITY_EDITOR"), StaticAccessor("GetDrivenPropertyManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnregisterPropertyPartial([NotNull("ArgumentNullException")] Object driver, [NotNull("ArgumentNullException")] Object target, [NotNull("ArgumentNullException")] string propertyPath);
	}
}
