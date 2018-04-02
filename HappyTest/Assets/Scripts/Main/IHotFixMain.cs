using System;

namespace Happy.Main
{
    public interface IHotFixMain
    {
        Type LoadType(string typeName);

        object CreateInstance(string typeName);
    }
}
