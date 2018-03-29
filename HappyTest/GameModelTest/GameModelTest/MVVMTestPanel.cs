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

        Button button;
        Button button2;
        Text buttonText;

        protected override void OnInitialize()
        {
            base.OnInitialize();
            button = GameObject.Find("TestPanelButton").GetComponent<Button>();
            buttonText = button.transform.Find("Text").GetComponent<Text>();
            button2 = GameObject.Find("TestPanelButton2").GetComponent<Button>();           
        }

        public override void OnAppear()
        {
            base.OnAppear();
            button.onClick.AsObservable()
                .Throttle(TimeSpan.FromSeconds(2))
                .Do(_ => Debug.Log("111"))
                .Throttle(TimeSpan.FromSeconds(2))
                .Do(_ => Debug.Log("222"))
                .Throttle(TimeSpan.FromSeconds(2))
                .Subscribe(_ => Debug.Log("333"));

            MVVMTestModel viewModel = BindingContext as MVVMTestModel;
            if (viewModel != null)
            {
                viewModel.buttonText.OnValueChanged += (oldValue, newValue) => buttonText.text = newValue;
                button2.onClick.AsObservable()
                    .Throttle(TimeSpan.FromSeconds(2))
                    .Do(_ => viewModel.buttonText.Value = "456")
                    .Throttle(TimeSpan.FromSeconds(2))
                    .Do(_ => viewModel.buttonText.Value = "789")
                    .Throttle(TimeSpan.FromSeconds(2))
                    .Subscribe(_ => viewModel.buttonText.Value = "000");
            }
        }
    }

    public class MVVMTestModel : ViewModelBase
    {
        public readonly BindableProperty<string> buttonText = new BindableProperty<string>();

        public override void OnFinishReveal()
        {
            base.OnFinishReveal();
            buttonText.Value = "123";
        }
    }
}
