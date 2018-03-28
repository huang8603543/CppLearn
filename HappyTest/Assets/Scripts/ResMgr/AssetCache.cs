using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Happy.Res
{
    public abstract class AssetCache
    {
        private int _refCount = 0;

        public int RefCount
        {
            get
            {
                return _refCount;
            }
        }

        private HashSet<int> objSet;
        public LinkedListNode<AssetCache> linkListNode;

        /// <summary>
        /// 最后使用时间///
        /// </summary>
        public float LastUsedTime
        {
            get;
            set;
        }

        public int AddRefCount(int refCount = 1)
        {
            _refCount += refCount;
            return _refCount;
        }

        public int DecRefCount(int refCount = 1)
        {
            if (_refCount <= 0)
            {
                _refCount = 0;
                return _refCount;
            }

            _refCount -= refCount;
            if (_refCount < 0)
                _refCount = 0;
            return _refCount;
        }
    }
}
