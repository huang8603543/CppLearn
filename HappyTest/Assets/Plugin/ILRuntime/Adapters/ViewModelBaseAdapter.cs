﻿using System;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using Happy.MVVM;

public class ViewModelBaseAdapter : CrossBindingAdaptor
{
    public override Type BaseCLRType
    {
        get
        {
            return typeof(ViewModelBase);
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

    class Adaptor : ViewModelBase, CrossBindingAdaptorType
    {
        ILTypeInstance instance;
        ILRuntime.Runtime.Enviorment.AppDomain appdomain;

        IMethod _onStartReveal;
        bool _onStartRevealGot;
        bool _isOnStartRevealInvoking = false;

        IMethod _onFinishReveal;
        bool _onFinishRevealGot;
        bool _isOnFinishRevealInvoking = false;

        IMethod _onStartHide;
        bool _onStartHideGot;
        bool _isOnStartHideInvoking = false;

        IMethod _onFinishHide;
        bool _onFinishHideGot;
        bool _isOnFinishHideInvoking = false;

        IMethod _onDestory;
        bool _onDestoryGot;
        bool _isOnDestoryInvoking = false;

        IMethod _onInitialize;
        bool _onInitializeGot;
        bool _isOnInitializeInvoking = false;

        public Adaptor()
        {

        }

        public Adaptor(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
        {
            this.appdomain = appdomain;
            this.instance = instance;
        }

        public ILTypeInstance ILInstance { get { return instance; } }

        public override void OnStartReveal()
        {
            if (!_onStartRevealGot)
            {
                _onStartReveal = instance.Type.GetMethod("OnStartReveal");
                _onStartRevealGot = true;
            }

            if (_onStartReveal != null && !_isOnStartRevealInvoking)
            {
                _isOnStartRevealInvoking = true;
                appdomain.Invoke(_onStartReveal, instance);
                _isOnStartRevealInvoking = false;
            }
            else
            {
                base.OnStartReveal();
            }
        }

        public override void OnFinishReveal()
        {
            if (!_onFinishRevealGot)
            {
                _onFinishReveal = instance.Type.GetMethod("OnFinishReveal");
                _onFinishRevealGot = true;
            }

            if (_onFinishReveal != null && !_isOnFinishRevealInvoking)
            {
                _isOnFinishRevealInvoking = true;
                appdomain.Invoke(_onFinishReveal, instance);
                _isOnFinishRevealInvoking = false;
            }
            else
            {
                base.OnFinishReveal();
            }
        }

        public override void OnStartHide()
        {
            if (!_onStartHideGot)
            {
                _onStartHide = instance.Type.GetMethod("OnStartHide");
                _onStartHideGot = true;
            }

            if (_onStartHide != null && !_isOnStartHideInvoking)
            {
                _isOnStartHideInvoking = true;
                appdomain.Invoke(_onStartHide, instance);
                _isOnStartHideInvoking = false;
            }
            else
            {
                base.OnStartHide();
            }
        }

        public override void OnFinishHide()
        {
            if (!_onFinishHideGot)
            {
                _onFinishHide = instance.Type.GetMethod("OnFinishHide");
                _onFinishHideGot = true;
            }

            if (_onFinishHide != null && !_isOnFinishHideInvoking)
            {
                _isOnFinishHideInvoking = true;
                appdomain.Invoke(_onFinishHide, instance);
                _isOnFinishHideInvoking = false;
            }
            else
            {
                base.OnFinishHide();
            }
        }

        public override void OnDestory()
        {
            if (!_onDestoryGot)
            {
                _onDestory = instance.Type.GetMethod("OnDestory");
                _onDestoryGot = true;
            }

            if (_onDestory != null && !_isOnDestoryInvoking)
            {
                _isOnDestoryInvoking = true;
                appdomain.Invoke(_onDestory, instance);
                _isOnDestoryInvoking = false;
            }
            else
            {
                base.OnDestory();
            }
        }

        protected override void OnInitialize()
        {
            if (!_onInitializeGot)
            {
                _onInitialize = instance.Type.GetMethod("OnInitialize");
                _onInitializeGot = true;
            }

            if (_onInitialize != null && !_isOnInitializeInvoking)
            {
                _isOnInitializeInvoking = true;
                appdomain.Invoke(_onInitialize, instance);
                _isOnInitializeInvoking = false;
            }
            else
            {
                base.OnInitialize();
            }
        }       
    }
}
