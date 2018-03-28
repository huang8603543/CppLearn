using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Happy.MVVM
{
    public class PropertyBinder<T> where T : ViewModelBase
    {
        private delegate void BindHandler(T viewModel);
        private delegate void UnBindHandler(T viewModel);

        private readonly List<BindHandler> binders = new List<BindHandler>();
        private readonly List<UnBindHandler> unBinders = new List<UnBindHandler>();
        

        
        public void Bind(T viewModel)
        {
            if (viewModel != null)
            {
                for (int i = 0; i < binders.Count; i++)
                {
                    binders[i](viewModel);
                }
            }
        }

        public void UnBind(T viewModel)
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
