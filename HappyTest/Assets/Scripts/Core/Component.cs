using System;
using Happy.Util;

namespace Happy.Core
{
    /// <summary>
    /// 组件基类///
    /// </summary>
    public abstract partial class Component : Object, IDisposable
    {
        public long InstanceId
        {
            get;
            protected set;
        }

        private bool _isFromPool;

        public bool IsFromPool
        {
            get
            {
                return _isFromPool;
            }
            set
            {
                _isFromPool = value;
                if (InstanceId == 0)
                {
                    InstanceId = IDGenerater.GenerateId();
                    //Game.EventSystem.Add(this);
                }
            }
        }

        public bool IsDisposed
        {
            get
            {
                return InstanceId == 0;
            }
        }

        public Component Parent
        {
            get;
            set;
        }

        public T GetParent<T>() where T : Component
        {
            return Parent as T;
        }


        protected Component()
        {
            InstanceId = IDGenerater.GenerateId();
            //Game.EventSystem.Add(this);
        }

        public virtual void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            //Game.EventSystem.Remove(this.InstanceId);
            InstanceId = 0;

            if (IsFromPool)
            {
                //Game.ObjectPool.Recycle(this);
            }

            //Game.EventSystem.Destroy(this);
        }
    }
}
