using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Happy.Res
{
    public class ResInstDestroy : MonoBehaviour
    {
        void OnDestroy()
        {
            ResourceMgr.Instance.OnDestroyInstObject(gameObject);
        }
    }
}
