using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.GlobalIllumination
{
	public static class Lightmapping
	{
		public delegate void RequestLightsDelegate(Light[] requests, NativeArray<LightDataGI> lightsOutput);

		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly Lightmapping.<>c <>9 = new Lightmapping.<>c();

			internal void cctor>b__7_0(Light[] requests, NativeArray<LightDataGI> lightsOutput)
			{
				DirectionalLight directionalLight = default(DirectionalLight);
				PointLight pointLight = default(PointLight);
				SpotLight spotLight = default(SpotLight);
				RectangleLight rectangleLight = default(RectangleLight);
				DiscLight discLight = default(DiscLight);
				Cookie cookie = default(Cookie);
				LightDataGI value = default(LightDataGI);
				for (int i = 0; i < requests.Length; i++)
				{
					Light light = requests[i];
					switch (light.type)
					{
					case UnityEngine.LightType.Spot:
						LightmapperUtils.Extract(light, ref spotLight);
						LightmapperUtils.Extract(light, out cookie);
						value.Init(ref spotLight, ref cookie);
						break;
					case UnityEngine.LightType.Directional:
						LightmapperUtils.Extract(light, ref directionalLight);
						LightmapperUtils.Extract(light, out cookie);
						value.Init(ref directionalLight, ref cookie);
						break;
					case UnityEngine.LightType.Point:
						LightmapperUtils.Extract(light, ref pointLight);
						LightmapperUtils.Extract(light, out cookie);
						value.Init(ref pointLight, ref cookie);
						break;
					case UnityEngine.LightType.Area:
						LightmapperUtils.Extract(light, ref rectangleLight);
						LightmapperUtils.Extract(light, out cookie);
						value.Init(ref rectangleLight, ref cookie);
						break;
					case UnityEngine.LightType.Disc:
						LightmapperUtils.Extract(light, ref discLight);
						LightmapperUtils.Extract(light, out cookie);
						value.Init(ref discLight, ref cookie);
						break;
					default:
						value.InitNoBake(light.GetInstanceID());
						break;
					}
					lightsOutput[i] = value;
				}
			}
		}

		[RequiredByNativeCode]
		private static readonly Lightmapping.RequestLightsDelegate s_DefaultDelegate = new Lightmapping.RequestLightsDelegate(Lightmapping.<>c.<>9.<.cctor>b__7_0);

		[RequiredByNativeCode]
		private static Lightmapping.RequestLightsDelegate s_RequestLightsDelegate = Lightmapping.s_DefaultDelegate;

		[RequiredByNativeCode]
		public static void SetDelegate(Lightmapping.RequestLightsDelegate del)
		{
			Lightmapping.s_RequestLightsDelegate = ((del != null) ? del : Lightmapping.s_DefaultDelegate);
		}

		[RequiredByNativeCode]
		public static Lightmapping.RequestLightsDelegate GetDelegate()
		{
			return Lightmapping.s_RequestLightsDelegate;
		}

		[RequiredByNativeCode]
		public static void ResetDelegate()
		{
			Lightmapping.s_RequestLightsDelegate = Lightmapping.s_DefaultDelegate;
		}

		[RequiredByNativeCode]
		internal unsafe static void RequestLights(Light[] lights, IntPtr outLightsPtr, int outLightsCount, AtomicSafetyHandle safetyHandle)
		{
			NativeArray<LightDataGI> lightsOutput = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<LightDataGI>((void*)outLightsPtr, outLightsCount, Allocator.None);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<LightDataGI>(ref lightsOutput, safetyHandle);
			Lightmapping.s_RequestLightsDelegate(lights, lightsOutput);
		}
	}
}
