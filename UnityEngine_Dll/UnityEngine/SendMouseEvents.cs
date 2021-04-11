using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	internal class SendMouseEvents
	{
		private struct HitInfo
		{
			public GameObject target;

			public Camera camera;

			public void SendMessage(string name)
			{
				this.target.SendMessage(name, null, SendMessageOptions.DontRequireReceiver);
			}

			public static implicit operator bool(SendMouseEvents.HitInfo exists)
			{
				return exists.target != null && exists.camera != null;
			}

			public static bool Compare(SendMouseEvents.HitInfo lhs, SendMouseEvents.HitInfo rhs)
			{
				return lhs.target == rhs.target && lhs.camera == rhs.camera;
			}
		}

		private const int m_HitIndexGUI = 0;

		private const int m_HitIndexPhysics3D = 1;

		private const int m_HitIndexPhysics2D = 2;

		private static bool s_MouseUsed = false;

		private static readonly SendMouseEvents.HitInfo[] m_LastHit = new SendMouseEvents.HitInfo[3];

		private static readonly SendMouseEvents.HitInfo[] m_MouseDownHit = new SendMouseEvents.HitInfo[3];

		private static readonly SendMouseEvents.HitInfo[] m_CurrentHit = new SendMouseEvents.HitInfo[3];

		private static Camera[] m_Cameras;

		[RequiredByNativeCode]
		private static void SetMouseMoved()
		{
			SendMouseEvents.s_MouseUsed = true;
		}

		[RequiredByNativeCode]
		private static void DoSendMouseEvents(int skipRTCameras)
		{
			Vector3 mousePosition = Input.mousePosition;
			int allCamerasCount = Camera.allCamerasCount;
			bool flag = SendMouseEvents.m_Cameras == null || SendMouseEvents.m_Cameras.Length != allCamerasCount;
			if (flag)
			{
				SendMouseEvents.m_Cameras = new Camera[allCamerasCount];
			}
			Camera.GetAllCameras(SendMouseEvents.m_Cameras);
			for (int i = 0; i < SendMouseEvents.m_CurrentHit.Length; i++)
			{
				SendMouseEvents.m_CurrentHit[i] = default(SendMouseEvents.HitInfo);
			}
			bool flag2 = !SendMouseEvents.s_MouseUsed;
			if (flag2)
			{
				Camera[] cameras = SendMouseEvents.m_Cameras;
				for (int j = 0; j < cameras.Length; j++)
				{
					Camera camera = cameras[j];
					bool flag3 = camera == null || (skipRTCameras != 0 && camera.targetTexture != null);
					if (!flag3)
					{
						int targetDisplay = camera.targetDisplay;
						Vector3 vector = Display.RelativeMouseAt(mousePosition);
						bool flag4 = vector != Vector3.zero;
						if (flag4)
						{
							int num = (int)vector.z;
							bool flag5 = num != targetDisplay;
							if (flag5)
							{
								goto IL_358;
							}
							float num2 = (float)Screen.width;
							float num3 = (float)Screen.height;
							bool flag6 = targetDisplay > 0 && targetDisplay < Display.displays.Length;
							if (flag6)
							{
								num2 = (float)Display.displays[targetDisplay].systemWidth;
								num3 = (float)Display.displays[targetDisplay].systemHeight;
							}
							Vector2 vector2 = new Vector2(vector.x / num2, vector.y / num3);
							bool flag7 = vector2.x < 0f || vector2.x > 1f || vector2.y < 0f || vector2.y > 1f;
							if (flag7)
							{
								goto IL_358;
							}
						}
						else
						{
							vector = mousePosition;
						}
						bool flag8 = !camera.pixelRect.Contains(vector);
						if (!flag8)
						{
							bool flag9 = camera.eventMask == 0;
							if (!flag9)
							{
								Ray ray = camera.ScreenPointToRay(vector);
								float z = ray.direction.z;
								float distance = Mathf.Approximately(0f, z) ? float.PositiveInfinity : Mathf.Abs((camera.farClipPlane - camera.nearClipPlane) / z);
								GameObject gameObject = CameraRaycastHelper.RaycastTry(camera, ray, distance, camera.cullingMask & camera.eventMask);
								bool flag10 = gameObject != null;
								if (flag10)
								{
									SendMouseEvents.m_CurrentHit[1].target = gameObject;
									SendMouseEvents.m_CurrentHit[1].camera = camera;
								}
								else
								{
									bool flag11 = camera.clearFlags == CameraClearFlags.Skybox || camera.clearFlags == CameraClearFlags.Color;
									if (flag11)
									{
										SendMouseEvents.m_CurrentHit[1].target = null;
										SendMouseEvents.m_CurrentHit[1].camera = null;
									}
								}
								GameObject gameObject2 = CameraRaycastHelper.RaycastTry2D(camera, ray, distance, camera.cullingMask & camera.eventMask);
								bool flag12 = gameObject2 != null;
								if (flag12)
								{
									SendMouseEvents.m_CurrentHit[2].target = gameObject2;
									SendMouseEvents.m_CurrentHit[2].camera = camera;
								}
								else
								{
									bool flag13 = camera.clearFlags == CameraClearFlags.Skybox || camera.clearFlags == CameraClearFlags.Color;
									if (flag13)
									{
										SendMouseEvents.m_CurrentHit[2].target = null;
										SendMouseEvents.m_CurrentHit[2].camera = null;
									}
								}
							}
						}
					}
					IL_358:;
				}
			}
			for (int k = 0; k < SendMouseEvents.m_CurrentHit.Length; k++)
			{
				SendMouseEvents.SendEvents(k, SendMouseEvents.m_CurrentHit[k]);
			}
			SendMouseEvents.s_MouseUsed = false;
		}

		private static void SendEvents(int i, SendMouseEvents.HitInfo hit)
		{
			bool mouseButtonDown = Input.GetMouseButtonDown(0);
			bool mouseButton = Input.GetMouseButton(0);
			bool flag = mouseButtonDown;
			if (flag)
			{
				bool flag2 = hit;
				if (flag2)
				{
					SendMouseEvents.m_MouseDownHit[i] = hit;
					SendMouseEvents.m_MouseDownHit[i].SendMessage("OnMouseDown");
				}
			}
			else
			{
				bool flag3 = !mouseButton;
				if (flag3)
				{
					bool flag4 = SendMouseEvents.m_MouseDownHit[i];
					if (flag4)
					{
						bool flag5 = SendMouseEvents.HitInfo.Compare(hit, SendMouseEvents.m_MouseDownHit[i]);
						if (flag5)
						{
							SendMouseEvents.m_MouseDownHit[i].SendMessage("OnMouseUpAsButton");
						}
						SendMouseEvents.m_MouseDownHit[i].SendMessage("OnMouseUp");
						SendMouseEvents.m_MouseDownHit[i] = default(SendMouseEvents.HitInfo);
					}
				}
				else
				{
					bool flag6 = SendMouseEvents.m_MouseDownHit[i];
					if (flag6)
					{
						SendMouseEvents.m_MouseDownHit[i].SendMessage("OnMouseDrag");
					}
				}
			}
			bool flag7 = SendMouseEvents.HitInfo.Compare(hit, SendMouseEvents.m_LastHit[i]);
			if (flag7)
			{
				bool flag8 = hit;
				if (flag8)
				{
					hit.SendMessage("OnMouseOver");
				}
			}
			else
			{
				bool flag9 = SendMouseEvents.m_LastHit[i];
				if (flag9)
				{
					SendMouseEvents.m_LastHit[i].SendMessage("OnMouseExit");
				}
				bool flag10 = hit;
				if (flag10)
				{
					hit.SendMessage("OnMouseEnter");
					hit.SendMessage("OnMouseOver");
				}
			}
			SendMouseEvents.m_LastHit[i] = hit;
		}
	}
}
