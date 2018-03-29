using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

namespace Happy.MVVM
{
    public class PropertyBinder<ViewModelBase>
    {
        private delegate void BindHandler(ViewModelBase viewModel);
        private delegate void UnBindHandler(ViewModelBase viewModel);

        private readonly List<BindHandler> binders = new List<BindHandler>();
        private readonly List<UnBindHandler> unBinders = new List<UnBindHandler>();

        //public void Add<TProperty>(string name,  Action<TProperty, TProperty> valueChangedHandler)
        //{
        //    var fieldInfo = typeof(T).GetField(name, BindingFlags.Instance | BindingFlags.Public);
        //    if (fieldInfo == null)
        //    {
        //        throw new Exception(string.Format("Unable to find bindableproperty field '{0}.{1}'", typeof(ViewModelBase).Name, name));
        //    }

        //    _binders.Add(viewmodel =>
        //    {
        //        GetPropertyValue<TProperty>(name, viewmodel, fieldInfo).OnValueChanged += valueChangedHandler;
        //    });

        //    _unbinders.Add(viewModel =>
        //    {
        //        GetPropertyValue<TProperty>(name, viewModel, fieldInfo).OnValueChanged -= valueChangedHandler;
        //    });
        //}

        //private BindableProperty<TProperty> GetPropertyValue<TProperty>(string name, T viewModel, FieldInfo fieldInfo)
        //{
        //    var value = fieldInfo.GetValue(viewModel);
        //    BindableProperty<TProperty> bindableProperty = value as BindableProperty<TProperty>;
        //    if (bindableProperty == null)
        //    {
        //        throw new Exception(string.Format("Illegal bindableproperty field '{0}.{1}' ", typeof(T).Name, name));
        //    }

        //    return bindableProperty;
        //}

        public void Bind(ViewModelBase viewModel)
        {
            if (viewModel != null)
            {
                for (int i = 0; i < binders.Count; i++)
                {
                    binders[i](viewModel);
                }
            }
        }

        public void UnBind(ViewModelBase viewModel)
        {
            if (viewModel != null)
            {
                for (int i = 0; i < unBinders.Count; i++)
                {
                    unBinders[i](viewModel);
                }
            }
        }
    }
}
