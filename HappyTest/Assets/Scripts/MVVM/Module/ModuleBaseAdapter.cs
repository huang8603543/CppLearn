﻿using System;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.CLR.Method;
using Happy.MVVM;

public class ModuleBaseAdapter : CrossBindingAdaptor
{
    public override Type BaseCLRType
    {
        get
        {
            return typeof(ModuleBase);
        }
    }

    public override Type AdaptorType
    {
        get
        {
            return typeof(ModuleBaseAdaptor);
        }
    }

    public override object CreateCLRInstance(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
    {
        return new ModuleBaseAdaptor(appdomain, instance);
    }

    class ModuleBaseAdaptor : ModuleBase, CrossBindingAdaptorType
    {
        ILTypeInstance instance;
        ILRuntime.Runtime.Enviorment.AppDomain appdomain;

        IMethod _onInitialize;
        bool _onInitializeGot;

        IMethod _excute;
        bool _excuteGot;

        public ModuleBaseAdaptor()
        {

        }

        public ModuleBaseAdaptor(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
        {
            this.appdomain = appdomain;
            this.instance = instance;
        }

        public ILTypeInstance ILInstance { get { return instance; } }

        public override void OnInitialize()
        {
            if (!_onInitializeGot)
            {
                _onInitialize = instance.Type.GetMethod("OnInitialize");
                _onInitializeGot = true;
            }
            if (_onInitialize != null)
            {
                appdomain.Invoke(_onInitialize, instance, null);
            }
        }

        public override void Excute()
        {
            if (!_excuteGot)
            {
                _excute = instance.Type.GetMethod("Excute");
                _excuteGot = true;
            }
            if (_excute != null)
            {
                appdomain.Invoke(_excute, instance, null);
            }
        }
    }
}
