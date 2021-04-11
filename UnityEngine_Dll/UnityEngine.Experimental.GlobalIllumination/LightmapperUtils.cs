using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.GlobalIllumination
{
	public static class LightmapperUtils
	{
		public static LightMode Extract(LightmapBakeType baketype)
		{
			return (baketype == LightmapBakeType.Realtime) ? LightMode.Realtime : ((baketype == LightmapBakeType.Mixed) ? LightMode.Mixed : LightMode.Baked);
		}

		public static LinearColor ExtractIndirect(Light l)
		{
			return LinearColor.Convert(l.color, l.intensity * l.bounceIntensity);
		}

		public static float ExtractInnerCone(Light l)
		{
			return 2f * Mathf.Atan(Mathf.Tan(l.spotAngle * 0.5f * 0.0174532924f) * 46f / 64f);
		}

		private static Color ExtractColorTemperature(Light l)
		{
			Color result = new Color(1f, 1f, 1f);
			bool flag = l.useColorTemperature && GraphicsSettings.lightsUseLinearIntensity;
			if (flag)
			{
				result = Mathf.CorrelatedColorTemperatureToRGB(l.colorTemperature);
			}
			return result;
		}

		private static void ApplyColorTemperature(Color cct, ref LinearColor lightColor)
		{
			lightColor.red *= cct.r;
			lightColor.green *= cct.g;
			lightColor.blue *= cct.b;
		}

		public static void Extract(Light l, ref DirectionalLight dir)
		{
			dir.instanceID = l.GetInstanceID();
			dir.mode = LightmapperUtils.Extract(l.lightmapBakeType);
			dir.shadow = (l.shadows > LightShadows.None);
			dir.position = l.transform.position;
			dir.orientation = l.transform.rotation;
			Color cct = LightmapperUtils.ExtractColorTemperature(l);
			LinearColor color = LinearColor.Convert(l.color, l.intensity);
			LinearColor indirectColor = LightmapperUtils.ExtractIndirect(l);
			LightmapperUtils.ApplyColorTemperature(cct, ref color);
			LightmapperUtils.ApplyColorTemperature(cct, ref indirectColor);
			dir.color = color;
			dir.indirectColor = indirectColor;
			dir.penumbraWidthRadian = ((l.shadows == LightShadows.Soft) ? (0.0174532924f * l.shadowAngle) : 0f);
		}

		public static void Extract(Light l, ref PointLight point)
		{
			point.instanceID = l.GetInstanceID();
			point.mode = LightmapperUtils.Extract(l.lightmapBakeType);
			point.shadow = (l.shadows > LightShadows.None);
			point.position = l.transform.position;
			Color cct = LightmapperUtils.ExtractColorTemperature(l);
			LinearColor color = LinearColor.Convert(l.color, l.intensity);
			LinearColor indirectColor = LightmapperUtils.ExtractIndirect(l);
			LightmapperUtils.ApplyColorTemperature(cct, ref color);
			LightmapperUtils.ApplyColorTemperature(cct, ref indirectColor);
			point.color = color;
			point.indirectColor = indirectColor;
			point.range = l.range;
			point.sphereRadius = ((l.shadows == LightShadows.Soft) ? l.shadowRadius : 0f);
			point.falloff = FalloffType.Legacy;
		}

		public static void Extract(Light l, ref SpotLight spot)
		{
			spot.instanceID = l.GetInstanceID();
			spot.mode = LightmapperUtils.Extract(l.lightmapBakeType);
			spot.shadow = (l.shadows > LightShadows.None);
			spot.position = l.transform.position;
			spot.orientation = l.transform.rotation;
			Color cct = LightmapperUtils.ExtractColorTemperature(l);
			LinearColor color = LinearColor.Convert(l.color, l.intensity);
			LinearColor indirectColor = LightmapperUtils.ExtractIndirect(l);
			LightmapperUtils.ApplyColorTemperature(cct, ref color);
			LightmapperUtils.ApplyColorTemperature(cct, ref indirectColor);
			spot.color = color;
			spot.indirectColor = indirectColor;
			spot.range = l.range;
			spot.sphereRadius = ((l.shadows == LightShadows.Soft) ? l.shadowRadius : 0f);
			spot.coneAngle = l.spotAngle * 0.0174532924f;
			spot.innerConeAngle = LightmapperUtils.ExtractInnerCone(l);
			spot.falloff = FalloffType.Legacy;
			spot.angularFalloff = AngularFalloffType.LUT;
		}

		public static void Extract(Light l, ref RectangleLight rect)
		{
			rect.instanceID = l.GetInstanceID();
			rect.mode = LightmapperUtils.Extract(l.lightmapBakeType);
			rect.shadow = (l.shadows > LightShadows.None);
			rect.position = l.transform.position;
			rect.orientation = l.transform.rotation;
			Color cct = LightmapperUtils.ExtractColorTemperature(l);
			LinearColor color = LinearColor.Convert(l.color, l.intensity);
			LinearColor indirectColor = LightmapperUtils.ExtractIndirect(l);
			LightmapperUtils.ApplyColorTemperature(cct, ref color);
			LightmapperUtils.ApplyColorTemperature(cct, ref indirectColor);
			rect.color = color;
			rect.indirectColor = indirectColor;
			rect.range = l.range;
			rect.width = l.areaSize.x;
			rect.height = l.areaSize.y;
			rect.falloff = FalloffType.Legacy;
		}

		public static void Extract(Light l, ref DiscLight disc)
		{
			disc.instanceID = l.GetInstanceID();
			disc.mode = LightmapperUtils.Extract(l.lightmapBakeType);
			disc.shadow = (l.shadows > LightShadows.None);
			disc.position = l.transform.position;
			disc.orientation = l.transform.rotation;
			Color cct = LightmapperUtils.ExtractColorTemperature(l);
			LinearColor color = LinearColor.Convert(l.color, l.intensity);
			LinearColor indirectColor = LightmapperUtils.ExtractIndirect(l);
			LightmapperUtils.ApplyColorTemperature(cct, ref color);
			LightmapperUtils.ApplyColorTemperature(cct, ref indirectColor);
			disc.color = color;
			disc.indirectColor = indirectColor;
			disc.range = l.range;
			disc.radius = l.areaSize.x;
			disc.falloff = FalloffType.Legacy;
		}

		public static void Extract(Light l, out Cookie cookie)
		{
			cookie.instanceID = (l.cookie ? l.cookie.GetInstanceID() : 0);
			cookie.scale = 1f;
			cookie.sizes = ((l.type == UnityEngine.LightType.Directional && l.cookie) ? new Vector2(l.cookieSize, l.cookieSize) : new Vector2(1f, 1f));
		}
	}
}
