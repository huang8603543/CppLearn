using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Happy.Res
{
    public enum ResourceCacheType
    {
        // 只用在读取配置（建议不要使用）
        rctNone = 0,
        // 临时的,只用在同步的时候，立马实例化Prefab(建议不要使用)
        rctTemp,
        // 对原始资源进行引用计数 +1，切记把LOAD出来的资源当做指针，替换和OnDestroy的时候，不要忘记使用ResourceMgr.Instance.Destroy对引用计数-1
        rctRefAdd
    }

    public abstract class IResourceLoader
    {

        public abstract GameObject LoadPrefab(string fileName, ResourceCacheType cacheType);
    }
}
