using System;
using Happy.MVVM;
using UnityEngine.UI;
using UnityEngine;
using UniRx;

namespace GameModelTest
{
    public class MVVMTestPanel : UnityGuiView
    {
        public override string ViewName
        {
            get
            {
                return "MVVMTestPanel";
            }
        }

        public override string ViewModelTypeName
        {
            get
            {
                return typeof(MVVMTestModel).FullName;
            }
        }

        public MVVMTestModel ViewModel
        {
            get
            {
                return (MVVMTestModel)BindingContext;
            }
        }

        Button button;
        Button button2;
        Text buttonText;

        protected override void OnInitialize()
        {
            base.OnInitialize();
            binder.Add<string>("buttonText", ViewModelTypeName, ButtonTextValueChanged);

            button = GameObject.Find("TestPanelButton").GetComponent<Button>();
            buttonText = button.transform.Find("Text").GetComponent<Text>();
            button2 = GameObject.Find("TestPanelButton2").GetComponent<Button>();

            button.onClick.AsObservable()
                .Throttle(TimeSpan.FromSeconds(2))
                .Do(_ => Debug.Log("111"))
                .Throttle(TimeSpan.FromSeconds(2))
                .Do(_ => Debug.Log("222"))
                .Throttle(TimeSpan.FromSeconds(2))
                .Subscribe(_ => Debug.Log("333"));

            button2.onClick.AsObservable()
                .Throttle(TimeSpan.FromSeconds(2))
                .Do(_ => ViewModel.buttonText.Value = "456")
                .Throttle(TimeSpan.FromSeconds(2))
                .Do(_ => ViewModel.buttonText.Value = "789")
                .Throttle(TimeSpan.FromSeconds(2))
                .Subscribe(_ => ViewModel.buttonText.Value = "000");
        }

        void ButtonTextValueChanged(string oldStr, string newStr)
        {
            buttonText.text = newStr;
        }
    }

    public class MVVMTestModel : ViewModelBase
    {
        public readonly BindableProperty<string> buttonText = new BindableProperty<string>();

        protected override void OnInitialize()        
        {
            base.OnFinishReveal();
            Initialization();
        }

        void Initialization()
        {
            buttonText.Value = "123";
        }
    }
}
