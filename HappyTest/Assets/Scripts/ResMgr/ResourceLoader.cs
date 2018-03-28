using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Happy.Res
{
    public class ResourceLoader : IResourceLoader
    {
        //private Dictionary<CacheKey, AssetCache> cacheMap = new Dictionary<CacheKey, AssetCache>(CacheKeyComparser.Default);


        public override GameObject LoadPrefab(string fileName, ResourceCacheType cacheType)
        {
            //return LoadObject<GameObject>(fileName, cacheType);
            return null;
        }

        //public T LoadObject<T>(string fileName, ResourceCacheType cacheType) where T : UnityEngine.Object
        //{
        //    if (string.IsNullOrEmpty(fileName))
        //        return null;

        //    T ret = null;
        //    string orgFileName = fileName;
        //    ret = FindCache<T>(orgFileName);

        //    bool isFirstLoad = (ret == null);
        //    if (isFirstLoad)
        //    {
        //        if (IsResLoaderFileName(ref fileName))
        //            ret = Resource.Load<T>(fileName);
        //        else
        //            return null;
        //    }

        //    AssetCache cache = AddRefCache(orgFileName, ret, cacheType, typeof(T));

        //    if (isFirstLoad && cache != null)
        //        AddCacheMap(cache);

        //    return ret;
        //}

        //T FindCache<T>(string fileName) where T : UnityEngine.Object
        //{
        //    AssetCache cache = FindCache(fileName, typeof(T));
        //    if (cache == null)
        //        return null;

        //    ResourceAssetCache resCache = cache as ResourceAssetCache;
        //    if (resCache == null)
        //        return null;
        //    return resCache.Target as T;
        //}

        //AssetCache FindCache(string fileName, Type resType)
        //{
        //    AssetCache ret;
        //    CacheKey key = CreateCacheKey(fileName, resType);
        //    if (cacheMap.TryGetValue(key, out ret))
        //        return ret;
        //    return null;
        //}

        //#region Struct

        ////sealed class CacheKeyComparser : StructComparser<CacheKey>
        ////{ }

        //struct CacheKey : IEquatable<CacheKey>
        //{
        //    public string FileName
        //    {
        //        get;
        //        set;
        //    }

        //    public Type ResType
        //    {
        //        get;
        //        set;
        //    }

        //    public bool Equals(CacheKey other)
        //    {
        //        return this == other;
        //    }

        //    public override bool Equals(object obj)
        //    {
        //        if (obj == null)
        //            return false;

        //        if (GetType() != obj.GetType())
        //            return false;

        //        if (obj is CacheKey)
        //        {
        //            CacheKey other = (CacheKey)obj;
        //            return Equals(other);
        //        }
        //        else
        //            return false;
        //    }

        //    public override int GetHashCode()
        //    {
        //        int ret = FilePathMgr.InitHashValue();

        //    }

        //    public static bool operator == (CacheKey a, CacheKey b)
        //    {
        //        return (a.ResType == b.ResType) && (string.Compare(a.FileName, b.FileName) == 0);
        //    }

        //    public static bool operator != (CacheKey a, CacheKey b)
        //    {
        //        return !(a == b);
        //    }
        //}

        //#endregion

    }
}
