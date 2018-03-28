using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Happy.Res
{
    public class AssetLoader : IResourceLoader
    {
        public override GameObject LoadPrefab(string fileName, ResourceCacheType cacheType)
        {
            return LoadObject<GameObject>(fileName, cacheType);
        }

        public T LoadObject<T>(string fileName, ResourceCacheType cacheType) where T : UnityEngine.Object
        {
            AssetInfo asset = FindAssetInfo(fileName);
            if (asset == null)
                return null;


        }
    }
}
