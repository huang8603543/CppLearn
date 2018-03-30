using System;

namespace Happy.Main
{
    public interface IObjectFactory
    {
        object AcquireObject(string classFullName);

        object AcquireObject<TInstance>() where TInstance : class, new();

        void ReleaseObject(object obj);
    }
}
