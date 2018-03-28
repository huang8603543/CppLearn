using System;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using Happy.MVVM;

public class UnityGuiViewAdapter : CrossBindingAdaptor
{
    public override Type BaseCLRType
    {
        get
        {
            return typeof(UnityGuiView);
        }
    }

    public override Type AdaptorType
    {
        get
        {
            return typeof(Adaptor);
        }
    }

    public override object CreateCLRInstance(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
    {
        return new Adaptor(appdomain, instance);
    }

    class Adaptor : UnityGuiView, CrossBindingAdaptorType
    {
        ILTypeInstance instance;
        ILRuntime.Runtime.Enviorment.AppDomain appdomain;

        IMethod _onInitialize;
        bool _onInitializeGot;
        bool isOnInitializeInvoking = false;

        public Adaptor()
        {

        }

        public Adaptor(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
        {
            this.appdomain = appdomain;
            this.instance = instance;
        }

        public ILTypeInstance ILInstance { get { return instance; } }

        protected override void OnInitialize()
        {
            if (!_onInitializeGot)
            {
                _onInitialize = instance.Type.GetMethod("OnInitialize");
                _onInitializeGot = true;
            }

            if (_onInitialize != null && !isOnInitializeInvoking)
            {
                isOnInitializeInvoking = true;
                appdomain.Invoke(_onInitialize, instance);
                isOnInitializeInvoking = false;
            }
            else
            {
                base.OnInitialize();
            }
        }

        public override void OnAppear()
        {

        }

        public override void OnRevealed()
        {

        }

        public override void OnHidden()
        {

        }

        public override void OnDisappear()
        {

        }

        public override void OnDestory()
        {

        }

        protected override void StartAnimatedReveal()
        {

        }

        protected override void StartAnimatedHide()
        {

        }
    }
}
