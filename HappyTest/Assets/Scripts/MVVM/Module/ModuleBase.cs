using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Happy.Util;

namespace Happy.MVVM
{
    public abstract class ModuleBase
    {
        public abstract void OnInitialize();

        public abstract void Excute();
    }
}
