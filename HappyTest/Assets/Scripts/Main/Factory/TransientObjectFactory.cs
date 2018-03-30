using ILRuntime.CLR.TypeSystem;
using System;
using Happy.Util;

namespace Happy.Main
{
    public class TransientObjectFactory : Singleton<TransientObjectFactory>, IObjectFactory
    {
        public object AcquireObject(string classFullName)
        {
            var instance = GameApplication.Instance.hotFix.CreateInstance(classFullName);
            return instance;
        }

        public object AcquireObject<TInstance>() where TInstance : class, new()
        {
            var instance = new TInstance();
            return instance;
        }

        public void ReleaseObject(object obj)
        {

        }
    }
}
