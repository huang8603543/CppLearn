using System;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.CLR.Method;
using Happy.Core;

public class ComponentAdapter : CrossBindingAdaptor
{
    public override Type BaseCLRType
    {
        get
        {
            return typeof(Component);
        }
    }

    public override Type AdaptorType
    {
        get
        {
            return typeof(ComponentAdaptor);
        }
    }

    public override object CreateCLRInstance(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
    {
        return new ComponentAdaptor(appdomain, instance);
    }

    class ComponentAdaptor : Component, CrossBindingAdaptorType
    {
        ILTypeInstance instance;
        ILRuntime.Runtime.Enviorment.AppDomain appdomain;

        IMethod _dispose;
        bool _disposeGot;
        bool isDisposeInvoking = false;

        public ComponentAdaptor()
        {

        }

        public ComponentAdaptor(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
        {
            this.appdomain = appdomain;
            this.instance = instance;
        }

        public ILTypeInstance ILInstance { get { return instance; } }

        public override void Dispose()
        {
            if (!_disposeGot)
            {
                _dispose = instance.Type.GetMethod("Dispose");
                _disposeGot = true;
            }

            if (_dispose != null && !isDisposeInvoking)
            {
                isDisposeInvoking = true;
                appdomain.Invoke(_dispose, instance);
                isDisposeInvoking = false;
            }
            else
            {
                base.Dispose();
            }
        }
    }
}
