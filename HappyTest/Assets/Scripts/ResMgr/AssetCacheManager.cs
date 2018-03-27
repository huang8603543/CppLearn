using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Happy.Util;

namespace Happy.Res
{
    public class AssetCacheManager : Singleton<AssetCacheManager>
    {
        public bool OnCreateGameObject(GameObject instObj, GameObject sourceObj)
        {
            if ((instObj == null) || (sourceObj == null))
                return false;

        }

        public void OnDestroyGameObject(int objId)
        {

        }
    }
}
