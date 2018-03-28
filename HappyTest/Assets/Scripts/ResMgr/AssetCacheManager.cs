using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Happy.Util;

namespace Happy.Res
{
    public class AssetCacheManager : Singleton<AssetCacheManager>
    {
        /// <summary>
        /// 使用列表///
        /// </summary>
        private LinkedList<AssetCache> usedCacheList = new LinkedList<AssetCache>();

        /// <summary>
        /// Cache查找（包括Used和NotUsed）///
        /// </summary>
        private HashSet<AssetCache> cacheSet = new HashSet<AssetCache>();

        /// <summary>
        /// Instance Obj to PrefabObj///
        /// </summary>
        private Dictionary<int, int> instObjToObjMap = new Dictionary<int, int>();

        /// <summary>
        /// GameObject对应的AssetCache，在Resources里的资源是一对一，而AssetBundle里的资源是一对多(Key: GetInstanceID)///
        /// </summary>
        private Dictionary<int, AssetCache> objCacheMap = new Dictionary<int, AssetCache>();

        private LinkedList<AssetCache> tempAssetList = new LinkedList<AssetCache>();
        private HashSet<AssetCache> tempDict = new HashSet<AssetCache>();

        public bool OnCreateGameObject(GameObject instObj, GameObject sourceObj)
        {
            if ((instObj == null) || (sourceObj == null))
                return false;

            //已经包含了（不应该出现）
            int instId = instObj.GetInstanceID();
            if (instObjToObjMap.ContainsKey(instId))
            {
                Debug.LogError("OnCreateGameObject: instObj's Id is exist");
                return false;
            }

            AssetCache cache;
            int sourceId = sourceObj.GetInstanceID();
            if (!objCacheMap.TryGetValue(sourceId, out cache))
                cache = null;
            if (cache == null)
            {
                Debug.LogError("OnCreateGameObject: not call LoadGameObject");
                return false;
            }

            AddOrUpdateUsedList(cache);
            instObjToObjMap.Add(instId, sourceId);
            return true;
        }

        public void OnDestroyGameObject(int instObjId)
        {
            int sourceObjId;
            if (!instObjToObjMap.TryGetValue(instObjId, out sourceObjId))
                return;

            instObjToObjMap.Remove(instObjId);
            AssetCache cache;
            if (!objCacheMap.TryGetValue(sourceObjId, out cache))
                cache = null;
            if (cache == null)
                return;

            CacheDecRefCount(cache);
        }

        void AddOrUpdateUsedList(AssetCache newCache, int refCount = 1)
        {
            if ((newCache == null) || (usedCacheList == null))
                return;
            if (CacheAddRefCount(newCache, refCount))
                return;

            newCache.AddRefCount(refCount);
            newCache.LastUsedTime = Time.realtimeSinceStartup;
            cacheSet.Add(newCache);
            usedCacheList.AddLast(newCache.linkListNode);
        }

        bool CacheAddRefCount(AssetCache cache, int refCount = 1, bool isUseTime = true)
        {
            if (cache == null)
                return false;

            //判断一下Temp, 如果有，直接删除掉
            bool hasInTemp = tempDict.Contains(cache);
            if (hasInTemp)
            {
                RemoveTempAsset(cache);
            }

            if (cacheSet.Contains(cache))
            {
                cache.AddRefCount(refCount);
                if (isUseTime)
                    cache.LastUsedTime = Time.realtimeSinceStartup;
                return true;
            }
            else
            {
                if (hasInTemp)
                {
                    // 在Temp队列里
                    cache.AddRefCount(refCount);
                    cache.LastUsedTime = Time.realtimeSinceStartup;
                    cacheSet.Add(cache);
                    usedCacheList.AddLast(cache.linkListNode);
                    return true;
                }
            }
            return false;
        }

        bool CacheDecRefCount(AssetCache cache, int refCount = 1, bool isUseTime = true)
        {
            if (cache == null)
                return false;
            if (cacheSet.Contains(cache))
            {
                bool isUsed = cache.RefCount > 0;
                cache.DecRefCount(refCount);
                if (isUsed)
                    cache.LastUsedTime = Time.realtimeSinceStartup;
                return true;
            }
            return false;
        }

        void RemoveTempAsset(AssetCache cache)
        {
            if (cache == null)
                return;
            if (tempDict.Contains(cache))
            {
                tempDict.Remove(cache);
                tempAssetList.Remove(cache.linkListNode);
            }
        }
    }
}
