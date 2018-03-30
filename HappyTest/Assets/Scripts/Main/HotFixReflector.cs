using Happy.Util;
using System;
using System.Linq;
using System.Reflection;

namespace Happy.Main
{
    public class HotFixReflector : SingletonMono<HotFixReflector>, IHotFixMain
    {
        public Assembly assembly;

        public Type LoadType(string realTypeName)
        {
            Type type = assembly.GetTypes().FirstOrDefault(t => t.Name == realTypeName);
            if (type == null)
            {
                throw new Exception(string.Format("Cant't find Class by class name:'{0}'", realTypeName));
            }
            return type;
        }

        public object CreateInstance(string classFullName)
        {
            throw new NotImplementedException();
        }
    }
}
