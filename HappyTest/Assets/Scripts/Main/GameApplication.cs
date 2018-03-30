using UnityEngine;

namespace Happy.Main
{
    public class GameApplication : MonoBehaviour
    {
        private static GameApplication _instance;
        public static GameApplication Instance
        {
            get
            {
                return _instance;
            }
        }

        public IHotFixMain hotFix;

        public HotFixILRunTime ILHotFix
        {
            get
            {
                return (HotFixILRunTime)hotFix;
            }
        }

        public HotFixReflector ReHotFix
        {
            get
            {
                return (HotFixReflector)hotFix;
            }
        }

        public bool useILHotFix = true;

        void Awake()
        {
            _instance = this;

            if (useILHotFix)
            {
                hotFix = HotFixILRunTime.GetInstance();
            }
            else
            {
                hotFix = HotFixReflector.GetInstance();
            }
        }

        void Start()
        {
            ILRuntimeTest.GetInstance();
        }
    }
}
