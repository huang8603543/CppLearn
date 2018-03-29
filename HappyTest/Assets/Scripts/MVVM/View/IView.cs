using System;

namespace Happy.MVVM
{
    public interface IView<ViewModelBase>
    {
        ViewModelBase BindingContext
        {
            get;
            set;
        }

        void Reveal(bool immediate = false, Action action = null);

        void Hide(bool immediate = false, Action action = null);
    }
}
