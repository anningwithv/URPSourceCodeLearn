using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationStreamHandles.bindings.h"), MovedFrom("UnityEngine.Experimental.Animations")]
	public static class AnimationStreamHandleUtility
	{
		public static void WriteInts(AnimationStream stream, NativeArray<PropertyStreamHandle> handles, NativeArray<int> buffer, bool useMask)
		{
			stream.CheckIsValid();
			int num = AnimationSceneHandleUtility.ValidateAndGetArrayCount<PropertyStreamHandle, int>(ref stream, handles, buffer);
			bool flag = num == 0;
			if (!flag)
			{
				AnimationStreamHandleUtility.WriteStreamIntsInternal(ref stream, handles.GetUnsafePtr<PropertyStreamHandle>(), buffer.GetUnsafePtr<int>(), num, useMask);
			}
		}

		public static void WriteFloats(AnimationStream stream, NativeArray<PropertyStreamHandle> handles, NativeArray<float> buffer, bool useMask)
		{
			stream.CheckIsValid();
			int num = AnimationSceneHandleUtility.ValidateAndGetArrayCount<PropertyStreamHandle, float>(ref stream, handles, buffer);
			bool flag = num == 0;
			if (!flag)
			{
				AnimationStreamHandleUtility.WriteStreamFloatsInternal(ref stream, handles.GetUnsafePtr<PropertyStreamHandle>(), buffer.GetUnsafePtr<float>(), num, useMask);
			}
		}

		public static void ReadInts(AnimationStream stream, NativeArray<PropertyStreamHandle> handles, NativeArray<int> buffer)
		{
			stream.CheckIsValid();
			int num = AnimationSceneHandleUtility.ValidateAndGetArrayCount<PropertyStreamHandle, int>(ref stream, handles, buffer);
			bool flag = num == 0;
			if (!flag)
			{
				AnimationStreamHandleUtility.ReadStreamIntsInternal(ref stream, handles.GetUnsafePtr<PropertyStreamHandle>(), buffer.GetUnsafePtr<int>(), num);
			}
		}

		public static void ReadFloats(AnimationStream stream, NativeArray<PropertyStreamHandle> handles, NativeArray<float> buffer)
		{
			stream.CheckIsValid();
			int num = AnimationSceneHandleUtility.ValidateAndGetArrayCount<PropertyStreamHandle, float>(ref stream, handles, buffer);
			bool flag = num == 0;
			if (!flag)
			{
				AnimationStreamHandleUtility.ReadStreamFloatsInternal(ref stream, handles.GetUnsafePtr<PropertyStreamHandle>(), buffer.GetUnsafePtr<float>(), num);
			}
		}

		[NativeMethod(Name = "AnimationHandleUtilityBindings::ReadStreamIntsInternal", IsFreeFunction = true, HasExplicitThis = false, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ReadStreamIntsInternal(ref AnimationStream stream, void* propertyStreamHandles, void* intBuffer, int count);

		[NativeMethod(Name = "AnimationHandleUtilityBindings::ReadStreamFloatsInternal", IsFreeFunction = true, HasExplicitThis = false, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ReadStreamFloatsInternal(ref AnimationStream stream, void* propertyStreamHandles, void* floatBuffer, int count);

		[NativeMethod(Name = "AnimationHandleUtilityBindings::WriteStreamIntsInternal", IsFreeFunction = true, HasExplicitThis = false, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void WriteStreamIntsInternal(ref AnimationStream stream, void* propertyStreamHandles, void* intBuffer, int count, bool useMask);

		[NativeMethod(Name = "AnimationHandleUtilityBindings::WriteStreamFloatsInternal", IsFreeFunction = true, HasExplicitThis = false, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void WriteStreamFloatsInternal(ref AnimationStream stream, void* propertyStreamHandles, void* floatBuffer, int count, bool useMask);
	}
}
