﻿using System;
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
                .Do(_ => 
                {
                    //Type type = BindingContext.GetType();
                    //Debug.Log("Type: " + type.FullName);
                    //var nestedTypes = type.GetNestedTypes();
                    //foreach (var nestedType in nestedTypes)
                    //{
                    //    Debug.Log("nestedType: " + nestedType.Name);
                    //}
                    ViewModel.buttonOneClick("Hello!!!");
                })
                .Throttle(TimeSpan.FromSeconds(1))
                .Do(_ => MessageAggregator<object>.Instance.Publish("TestOne", this, new MessageArgs<object>("TestOne!!!!")))
                .Throttle(TimeSpan.FromSeconds(1))
                .Subscribe(_ => MessageAggregator<CustomTestData>.Instance.Publish("TestTwo", this, new MessageArgs<CustomTestData>(new CustomTestData(100, "Hello"))));

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

        public Action<string> buttonOneClick;

        protected override void OnInitialize()        
        {
            base.OnInitialize();
            Initialization();
            DelegateSubscribe();
            buttonOneClick += (str) => Debug.Log("!!! " + str);
        }

        void Initialization()
        {
            buttonText.Value = "123";
        }

        void DelegateSubscribe()
        {
            MessageAggregator<object>.Instance.Subscribe("TestOne", TestOneCallBack);
            MessageAggregator<CustomTestData>.Instance.Subscribe("TestTwo", TestTwoCallBack);
        }

        void TestOneCallBack(object sender, MessageArgs<object> args)
        {
            Debug.Log("sender: " + sender.ToString() + " args: " + (string)args.Item);
        }

        void TestTwoCallBack(object sender, MessageArgs<CustomTestData> args)
        {
            Debug.Log("sender: " + sender.ToString() + " A: " + args.Item.A + " B: " + args.Item.B);
        }
    }

    public class CustomTestData
    {
        public int A { get; private set; }
        public string B { get; private set; }

        public CustomTestData(int a, string b)
        {
            A = a; B = b;
        }
    }
}
