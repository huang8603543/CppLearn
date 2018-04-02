using System.ComponentModel;
using Happy.Util;

namespace Happy.Core
{
    public abstract class Object : ISupportInitialize
    {
        public virtual void BeginInit()
        {

        }

        public virtual void EndInit()
        {

        }

        public override string ToString()
        {
            return JsonHelper.ToJson(this);
        }
    }
}
