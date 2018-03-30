using System.Collections.Generic;
using System;
using ILRuntime.CLR.TypeSystem;
using Happy.Util;

namespace Happy.Main
{
    public class SingletonObjectFactory : Singleton<SingletonObjectFactory>, IObjectFactory
    {
        private static Dictionary<Type, object> cachedObjects = null;
        private static readonly object _lock = new object();

        private Dictionary<Type, object> CachedObjects
        {
            get
            {
                lock (_lock)
                {
                    if (cachedObjects == null)
                    {
                        cachedObjects = new Dictionary<Type, object>();
                    }
                    return cachedObjects;
                }
            }
        }

        public object AcquireObject(string classFullName)
        {
            Type type = GameApplication.Instance.hotFix.LoadType(classFullName);

            if (CachedObjects.ContainsKey(type))
            {
                return CachedObjects[type];
            }
            lock (_lock)
            {
                var instance = GameApplication.Instance.hotFix.CreateInstance(classFullName);
                CachedObjects.Add(type, instance);
                return instance;
            }
        }

        public object AcquireObject<TInstance>() where TInstance : class, new()
        {
            var type = typeof(TInstance);
            if (CachedObjects.ContainsKey(type))
            {
                return CachedObjects[type];
            }
            lock (_lock)
            {
                var instance = new TInstance();
                CachedObjects.Add(type, instance);
                return instance;
            }
        }

        public void ReleaseObject(object obj)
        {

        }
    }
}
