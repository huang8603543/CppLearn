using System;

namespace Happy.Main
{
    public interface IObjectFactory
    {
        object AcquireObject(string classFullName);

        void ReleaseObject(object obj);
    }
}
