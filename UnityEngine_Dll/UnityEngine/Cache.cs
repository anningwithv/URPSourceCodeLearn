using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Misc/Cache.h"), StaticAccessor("CacheWrapper", StaticAccessorType.DoubleColon)]
	public struct Cache : IEquatable<Cache>
	{
		private int m_Handle;

		internal int handle
		{
			get
			{
				return this.m_Handle;
			}
		}

		public bool valid
		{
			get
			{
				return Cache.Cache_IsValid(this.m_Handle);
			}
		}

		public bool ready
		{
			get
			{
				return Cache.Cache_IsReady(this.m_Handle);
			}
		}

		public bool readOnly
		{
			get
			{
				return Cache.Cache_IsReadonly(this.m_Handle);
			}
		}

		public string path
		{
			get
			{
				return Cache.Cache_GetPath(this.m_Handle);
			}
		}

		public int index
		{
			get
			{
				return Cache.Cache_GetIndex(this.m_Handle);
			}
		}

		public long spaceFree
		{
			get
			{
				return Cache.Cache_GetSpaceFree(this.m_Handle);
			}
		}

		public long maximumAvailableStorageSpace
		{
			get
			{
				return Cache.Cache_GetMaximumDiskSpaceAvailable(this.m_Handle);
			}
			set
			{
				Cache.Cache_SetMaximumDiskSpaceAvailable(this.m_Handle, value);
			}
		}

		public long spaceOccupied
		{
			get
			{
				return Cache.Cache_GetCachingDiskSpaceUsed(this.m_Handle);
			}
		}

		public int expirationDelay
		{
			get
			{
				return Cache.Cache_GetExpirationDelay(this.m_Handle);
			}
			set
			{
				Cache.Cache_SetExpirationDelay(this.m_Handle, value);
			}
		}

		public static bool operator ==(Cache lhs, Cache rhs)
		{
			return lhs.handle == rhs.handle;
		}

		public static bool operator !=(Cache lhs, Cache rhs)
		{
			return lhs.handle != rhs.handle;
		}

		public override int GetHashCode()
		{
			return this.m_Handle;
		}

		public override bool Equals(object other)
		{
			return other is Cache && this.Equals((Cache)other);
		}

		public bool Equals(Cache other)
		{
			return this.handle == other.handle;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Cache_IsValid(int handle);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Cache_IsReady(int handle);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Cache_IsReadonly(int handle);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string Cache_GetPath(int handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int Cache_GetIndex(int handle);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long Cache_GetSpaceFree(int handle);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long Cache_GetMaximumDiskSpaceAvailable(int handle);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Cache_SetMaximumDiskSpaceAvailable(int handle, long value);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long Cache_GetCachingDiskSpaceUsed(int handle);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int Cache_GetExpirationDelay(int handle);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Cache_SetExpirationDelay(int handle, int value);

		public bool ClearCache()
		{
			return Cache.Cache_ClearCache(this.m_Handle);
		}

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Cache_ClearCache(int handle);

		public bool ClearCache(int expiration)
		{
			return Cache.Cache_ClearCache_Expiration(this.m_Handle, expiration);
		}

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Cache_ClearCache_Expiration(int handle, int expiration);
	}
}
