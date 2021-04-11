using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Misc/CachingManager.h"), StaticAccessor("GetCachingManager()", StaticAccessorType.Dot)]
	public sealed class Caching
	{
		public static extern bool compressionEnabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool ready
		{
			[NativeName("GetIsReady")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[Obsolete("Please use use Cache.spaceOccupied to get used bytes per cache.")]
		public static int spaceUsed
		{
			get
			{
				return (int)Caching.spaceOccupied;
			}
		}

		[Obsolete("This property is only used for the current cache, use Cache.spaceOccupied to get used bytes per cache.")]
		public static extern long spaceOccupied
		{
			[NativeName("GetCachingDiskSpaceUsed"), StaticAccessor("GetCachingManager().GetCurrentCache()", StaticAccessorType.Dot)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[Obsolete("Please use use Cache.spaceOccupied to get used bytes per cache.")]
		public static int spaceAvailable
		{
			get
			{
				return (int)Caching.spaceFree;
			}
		}

		[Obsolete("This property is only used for the current cache, use Cache.spaceFree to get unused bytes per cache.")]
		public static extern long spaceFree
		{
			[NativeName("GetCachingDiskSpaceFree"), StaticAccessor("GetCachingManager().GetCurrentCache()", StaticAccessorType.Dot)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[Obsolete("This property is only used for the current cache, use Cache.maximumAvailableStorageSpace to access the maximum available storage space per cache."), StaticAccessor("GetCachingManager().GetCurrentCache()", StaticAccessorType.Dot)]
		public static extern long maximumAvailableDiskSpace
		{
			[NativeName("GetMaximumDiskSpaceAvailable")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("SetMaximumDiskSpaceAvailable")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("This property is only used for the current cache, use Cache.expirationDelay to access the expiration delay per cache."), StaticAccessor("GetCachingManager().GetCurrentCache()", StaticAccessorType.Dot)]
		public static extern int expirationDelay
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int cacheCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		public static Cache defaultCache
		{
			[NativeName("Caching_GetDefaultCacheHandle")]
			get
			{
				Cache result;
				Caching.get_defaultCache_Injected(out result);
				return result;
			}
		}

		[StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		public static Cache currentCacheForWriting
		{
			[NativeName("Caching_GetCurrentCacheHandle")]
			get
			{
				Cache result;
				Caching.get_currentCacheForWriting_Injected(out result);
				return result;
			}
			[NativeName("Caching_SetCurrentCacheByHandle"), NativeThrows]
			set
			{
				Caching.set_currentCacheForWriting_Injected(ref value);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool ClearCache();

		public static bool ClearCache(int expiration)
		{
			return Caching.ClearCache_Int(expiration);
		}

		[NativeName("ClearCache")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool ClearCache_Int(int expiration);

		public static bool ClearCachedVersion(string assetBundleName, Hash128 hash)
		{
			bool flag = string.IsNullOrEmpty(assetBundleName);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle name cannot be null or empty.");
			}
			return Caching.ClearCachedVersionInternal(assetBundleName, hash);
		}

		[NativeName("ClearCachedVersion")]
		internal static bool ClearCachedVersionInternal(string assetBundleName, Hash128 hash)
		{
			return Caching.ClearCachedVersionInternal_Injected(assetBundleName, ref hash);
		}

		public static bool ClearOtherCachedVersions(string assetBundleName, Hash128 hash)
		{
			bool flag = string.IsNullOrEmpty(assetBundleName);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle name cannot be null or empty.");
			}
			return Caching.ClearCachedVersions(assetBundleName, hash, true);
		}

		public static bool ClearAllCachedVersions(string assetBundleName)
		{
			bool flag = string.IsNullOrEmpty(assetBundleName);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle name cannot be null or empty.");
			}
			return Caching.ClearCachedVersions(assetBundleName, default(Hash128), false);
		}

		internal static bool ClearCachedVersions(string assetBundleName, Hash128 hash, bool keepInputVersion)
		{
			return Caching.ClearCachedVersions_Injected(assetBundleName, ref hash, keepInputVersion);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Hash128[] GetCachedVersions(string assetBundleName);

		public static void GetCachedVersions(string assetBundleName, List<Hash128> outCachedVersions)
		{
			bool flag = string.IsNullOrEmpty(assetBundleName);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle name cannot be null or empty.");
			}
			bool flag2 = outCachedVersions == null;
			if (flag2)
			{
				throw new ArgumentNullException("Input outCachedVersions cannot be null.");
			}
			outCachedVersions.AddRange(Caching.GetCachedVersions(assetBundleName));
		}

		[Obsolete("Please use IsVersionCached with Hash128 instead.")]
		public static bool IsVersionCached(string url, int version)
		{
			return Caching.IsVersionCached(url, new Hash128(0u, 0u, 0u, (uint)version));
		}

		public static bool IsVersionCached(string url, Hash128 hash)
		{
			bool flag = string.IsNullOrEmpty(url);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle url cannot be null or empty.");
			}
			return Caching.IsVersionCached(url, "", hash);
		}

		public static bool IsVersionCached(CachedAssetBundle cachedBundle)
		{
			bool flag = string.IsNullOrEmpty(cachedBundle.name);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle name cannot be null or empty.");
			}
			return Caching.IsVersionCached("", cachedBundle.name, cachedBundle.hash);
		}

		[NativeName("IsCached")]
		internal static bool IsVersionCached(string url, string assetBundleName, Hash128 hash)
		{
			return Caching.IsVersionCached_Injected(url, assetBundleName, ref hash);
		}

		[Obsolete("Please use MarkAsUsed with Hash128 instead.")]
		public static bool MarkAsUsed(string url, int version)
		{
			return Caching.MarkAsUsed(url, new Hash128(0u, 0u, 0u, (uint)version));
		}

		public static bool MarkAsUsed(string url, Hash128 hash)
		{
			bool flag = string.IsNullOrEmpty(url);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle url cannot be null or empty.");
			}
			return Caching.MarkAsUsed(url, "", hash);
		}

		public static bool MarkAsUsed(CachedAssetBundle cachedBundle)
		{
			bool flag = string.IsNullOrEmpty(cachedBundle.name);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle name cannot be null or empty.");
			}
			return Caching.MarkAsUsed("", cachedBundle.name, cachedBundle.hash);
		}

		internal static bool MarkAsUsed(string url, string assetBundleName, Hash128 hash)
		{
			return Caching.MarkAsUsed_Injected(url, assetBundleName, ref hash);
		}

		[Obsolete("Please use SetNoBackupFlag with Hash128 instead.")]
		public static void SetNoBackupFlag(string url, int version)
		{
		}

		public static void SetNoBackupFlag(string url, Hash128 hash)
		{
		}

		public static void SetNoBackupFlag(CachedAssetBundle cachedBundle)
		{
		}

		[Obsolete("Please use ResetNoBackupFlag with Hash128 instead.")]
		public static void ResetNoBackupFlag(string url, int version)
		{
		}

		public static void ResetNoBackupFlag(string url, Hash128 hash)
		{
		}

		public static void ResetNoBackupFlag(CachedAssetBundle cachedBundle)
		{
		}

		[NativeConditional("PLATFORM_IOS || PLATFORM_TVOS")]
		internal static void SetNoBackupFlag(string url, string assetBundleName, Hash128 hash, bool enabled)
		{
			Caching.SetNoBackupFlag_Injected(url, assetBundleName, ref hash, enabled);
		}

		[Obsolete("This function is obsolete and will always return -1. Use IsVersionCached instead.")]
		public static int GetVersionFromCache(string url)
		{
			return -1;
		}

		public static Cache AddCache(string cachePath)
		{
			bool flag = string.IsNullOrEmpty(cachePath);
			if (flag)
			{
				throw new ArgumentNullException("Cache path cannot be null or empty.");
			}
			bool isReadonly = false;
			bool flag2 = cachePath.Replace('\\', '/').StartsWith(Application.streamingAssetsPath);
			if (flag2)
			{
				isReadonly = true;
			}
			else
			{
				bool flag3 = !Directory.Exists(cachePath);
				if (flag3)
				{
					throw new ArgumentException("Cache path '" + cachePath + "' doesn't exist.");
				}
				bool flag4 = (File.GetAttributes(cachePath) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
				if (flag4)
				{
					isReadonly = true;
				}
			}
			bool valid = Caching.GetCacheByPath(cachePath).valid;
			if (valid)
			{
				throw new InvalidOperationException("Cache with path '" + cachePath + "' has already been added.");
			}
			return Caching.AddCache(cachePath, isReadonly);
		}

		[NativeName("AddCachePath")]
		internal static Cache AddCache(string cachePath, bool isReadonly)
		{
			Cache result;
			Caching.AddCache_Injected(cachePath, isReadonly, out result);
			return result;
		}

		[NativeName("Caching_GetCacheHandleAt"), NativeThrows, StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		public static Cache GetCacheAt(int cacheIndex)
		{
			Cache result;
			Caching.GetCacheAt_Injected(cacheIndex, out result);
			return result;
		}

		[NativeName("Caching_GetCacheHandleByPath"), NativeThrows, StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		public static Cache GetCacheByPath(string cachePath)
		{
			Cache result;
			Caching.GetCacheByPath_Injected(cachePath, out result);
			return result;
		}

		public static void GetAllCachePaths(List<string> cachePaths)
		{
			cachePaths.Clear();
			for (int i = 0; i < Caching.cacheCount; i++)
			{
				cachePaths.Add(Caching.GetCacheAt(i).path);
			}
		}

		[NativeName("Caching_RemoveCacheByHandle"), NativeThrows, StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		public static bool RemoveCache(Cache cache)
		{
			return Caching.RemoveCache_Injected(ref cache);
		}

		[NativeName("Caching_MoveCacheBeforeByHandle"), NativeThrows, StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		public static void MoveCacheBefore(Cache src, Cache dst)
		{
			Caching.MoveCacheBefore_Injected(ref src, ref dst);
		}

		[NativeName("Caching_MoveCacheAfterByHandle"), NativeThrows, StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		public static void MoveCacheAfter(Cache src, Cache dst)
		{
			Caching.MoveCacheAfter_Injected(ref src, ref dst);
		}

		[Obsolete("This function is obsolete. Please use ClearCache.  (UnityUpgradable) -> ClearCache()")]
		public static bool CleanCache()
		{
			return Caching.ClearCache();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ClearCachedVersionInternal_Injected(string assetBundleName, ref Hash128 hash);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ClearCachedVersions_Injected(string assetBundleName, ref Hash128 hash, bool keepInputVersion);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsVersionCached_Injected(string url, string assetBundleName, ref Hash128 hash);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool MarkAsUsed_Injected(string url, string assetBundleName, ref Hash128 hash);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetNoBackupFlag_Injected(string url, string assetBundleName, ref Hash128 hash, bool enabled);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddCache_Injected(string cachePath, bool isReadonly, out Cache ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCacheAt_Injected(int cacheIndex, out Cache ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCacheByPath_Injected(string cachePath, out Cache ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RemoveCache_Injected(ref Cache cache);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MoveCacheBefore_Injected(ref Cache src, ref Cache dst);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MoveCacheAfter_Injected(ref Cache src, ref Cache dst);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_defaultCache_Injected(out Cache ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_currentCacheForWriting_Injected(out Cache ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_currentCacheForWriting_Injected(ref Cache value);
	}
}
