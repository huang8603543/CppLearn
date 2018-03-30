using System.Collections;
using System.Collections.Generic;
using System;

namespace Happy.Main
{
    public interface IHotFixMain
    {
        Type LoadType(string realTypeName);

        object CreateInstance(string classFullName);
    }
}
