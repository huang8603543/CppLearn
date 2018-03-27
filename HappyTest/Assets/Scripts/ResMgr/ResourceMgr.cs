using Happy.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Happy.Res
{
    /// <summary>
    /// 
    /// </summary>
    public class ResourceMgr : Singleton<ResourceMgr>
    {
        private IResourceLoader resLoader = new ResourceLoader();
        private IResourceLoader assetLoader = new AssetLoader();

        public GameObject CreateGameObject(string fileName)
        {
            GameObject obj = CreatePrefab(fileName);
            if (obj != null)
                obj.AddComponent<ResInstDestroy>();
            return obj;
        }

        GameObject CreatePrefab(string fileName)
        {
            GameObject obj = LoadPrefab(fileName, ResourceCacheType.rctTemp);
            if (obj != null)
                return InstantiateGameObject(obj);
            return null;
        }

        GameObject InstantiateGameObject(GameObject obj)
        {
            if (obj == null)
                return null;
            GameObject ret = UnityEngine.Object.Instantiate(obj) as GameObject;
            if (ret != null)
            {
                AssetCacheManager.Instance.OnCreateGameObject(ret, obj);
            }
            return ret;
        }

        #region OnDestory

        public void OnDestroyInstObject(GameObject obj)
        {
            if (obj == null)
                return;
            OnDestroyInstObject(obj.GetInstanceID());
        }

        public void OnDestroyInstObject(int objID)
        {
            AssetCacheManager.Instance.OnDestroyGameObject(objID);
        }

        #endregion

        #region Load

        public GameObject LoadPrefab(string fileName, ResourceCacheType cacheType)
        {
            GameObject obj = assetLoader.LoadPrefab(fileName, cacheType);
            if (obj != null)
                return obj;
            return resLoader.LoadPrefab(fileName, cacheType);
        }

        #endregion

    }
}
