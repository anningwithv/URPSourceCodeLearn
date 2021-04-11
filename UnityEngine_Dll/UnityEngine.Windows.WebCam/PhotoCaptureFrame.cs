using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Windows.WebCam
{
	[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE"), NativeHeader("PlatformDependent/Win/Webcam/PhotoCaptureFrame.h"), MovedFrom("UnityEngine.XR.WSA.WebCam")]
	public sealed class PhotoCaptureFrame : IDisposable
	{
		private IntPtr m_NativePtr;

		public int dataLength
		{
			get;
			private set;
		}

		public bool hasLocationData
		{
			get;
			private set;
		}

		public CapturePixelFormat pixelFormat
		{
			get;
			private set;
		}

		[ThreadAndSerializationSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetDataLength();

		[ThreadAndSerializationSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetHasLocationData();

		[ThreadAndSerializationSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern CapturePixelFormat GetCapturePixelFormat();

		public bool TryGetCameraToWorldMatrix(out Matrix4x4 cameraToWorldMatrix)
		{
			cameraToWorldMatrix = Matrix4x4.identity;
			bool hasLocationData = this.hasLocationData;
			bool result;
			if (hasLocationData)
			{
				cameraToWorldMatrix = this.GetCameraToWorldMatrix();
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		[NativeConditional("PLATFORM_WIN && !PLATFORM_XBOXONE", "Matrix4x4f()"), NativeName("GetCameraToWorld"), ThreadAndSerializationSafe]
		private Matrix4x4 GetCameraToWorldMatrix()
		{
			Matrix4x4 result;
			this.GetCameraToWorldMatrix_Injected(out result);
			return result;
		}

		public bool TryGetProjectionMatrix(out Matrix4x4 projectionMatrix)
		{
			bool hasLocationData = this.hasLocationData;
			bool result;
			if (hasLocationData)
			{
				projectionMatrix = this.GetProjection();
				result = true;
			}
			else
			{
				projectionMatrix = Matrix4x4.identity;
				result = false;
			}
			return result;
		}

		public bool TryGetProjectionMatrix(float nearClipPlane, float farClipPlane, out Matrix4x4 projectionMatrix)
		{
			bool hasLocationData = this.hasLocationData;
			bool result;
			if (hasLocationData)
			{
				float num = 0.01f;
				bool flag = nearClipPlane < num;
				if (flag)
				{
					nearClipPlane = num;
				}
				bool flag2 = farClipPlane < nearClipPlane + num;
				if (flag2)
				{
					farClipPlane = nearClipPlane + num;
				}
				projectionMatrix = this.GetProjection();
				float num2 = 1f / (farClipPlane - nearClipPlane);
				float m = -(farClipPlane + nearClipPlane) * num2;
				float m2 = -(2f * farClipPlane * nearClipPlane) * num2;
				projectionMatrix.m22 = m;
				projectionMatrix.m23 = m2;
				result = true;
			}
			else
			{
				projectionMatrix = Matrix4x4.identity;
				result = false;
			}
			return result;
		}

		[NativeConditional("PLATFORM_WIN && !PLATFORM_XBOXONE", "Matrix4x4f()"), ThreadAndSerializationSafe]
		private Matrix4x4 GetProjection()
		{
			Matrix4x4 result;
			this.GetProjection_Injected(out result);
			return result;
		}

		public void UploadImageDataToTexture(Texture2D targetTexture)
		{
			bool flag = targetTexture == null;
			if (flag)
			{
				throw new ArgumentNullException("targetTexture");
			}
			bool flag2 = this.pixelFormat > CapturePixelFormat.BGRA32;
			if (flag2)
			{
				throw new ArgumentException("Uploading PhotoCaptureFrame to a texture is only supported with BGRA32 CameraFrameFormat!");
			}
			this.UploadImageDataToTexture_Internal(targetTexture);
		}

		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE"), NativeName("UploadImageDataToTexture"), ThreadAndSerializationSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void UploadImageDataToTexture_Internal(Texture2D targetTexture);

		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE"), ThreadAndSerializationSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern IntPtr GetUnsafePointerToBuffer();

		public void CopyRawImageDataIntoBuffer(List<byte> byteBuffer)
		{
			bool flag = byteBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("byteBuffer");
			}
			byte[] array = new byte[this.dataLength];
			this.CopyRawImageDataIntoBuffer_Internal(array);
			bool flag2 = byteBuffer.Capacity < array.Length;
			if (flag2)
			{
				byteBuffer.Capacity = array.Length;
			}
			byteBuffer.Clear();
			byteBuffer.AddRange(array);
		}

		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE"), NativeName("CopyRawImageDataIntoBuffer"), ThreadAndSerializationSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void CopyRawImageDataIntoBuffer_Internal([Out] byte[] byteArray);

		internal PhotoCaptureFrame(IntPtr nativePtr)
		{
			this.m_NativePtr = nativePtr;
			this.dataLength = this.GetDataLength();
			this.hasLocationData = this.GetHasLocationData();
			this.pixelFormat = this.GetCapturePixelFormat();
			GC.AddMemoryPressure((long)this.dataLength);
		}

		private void Cleanup()
		{
			bool flag = this.m_NativePtr != IntPtr.Zero;
			if (flag)
			{
				GC.RemoveMemoryPressure((long)this.dataLength);
				this.Dispose_Internal();
				this.m_NativePtr = IntPtr.Zero;
			}
		}

		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE"), NativeName("Dispose"), ThreadAndSerializationSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Dispose_Internal();

		public void Dispose()
		{
			this.Cleanup();
			GC.SuppressFinalize(this);
		}

		~PhotoCaptureFrame()
		{
			this.Cleanup();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetCameraToWorldMatrix_Injected(out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetProjection_Injected(out Matrix4x4 ret);
	}
}
