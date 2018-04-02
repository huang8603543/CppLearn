using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Happy.Util;

namespace Happy.Core
{
    public class GameMgr : Singleton<GameMgr>
    {
        private Scene _scene;

        public Scene Scene
        {
            get
            {
                if (_scene != null)
                    return _scene;
                _scene = new Scene();
                return _scene;
            }
        }
    }
}
