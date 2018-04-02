using ILRuntime.CLR.TypeSystem;
using System;
using Happy.Util;

namespace Happy.Main
{
    public class TransientObjectFactory : IObjectFactory
    {
        public object AcquireObject(string classFullName)
        {
            var instance = GameApplication.Instance.hotFix.CreateInstance(classFullName);
            return instance;
        }

        public void ReleaseObject(object obj)
        {

        }
    }
}
