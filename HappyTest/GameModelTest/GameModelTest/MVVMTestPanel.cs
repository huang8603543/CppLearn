using System;
using Happy.MVVM;
using UnityEngine.UI;
using UnityEngine;
using UniRx;

namespace GameModelTest
{
    public class MVVMTestPanel : UnityGuiView<MVVMTestModel>
    {
        Button button;

        protected override void OnInitialize()
        {
            base.OnInitialize();
            button = GameObject.Find("TestPanelButton").GetComponent<Button>();
            button.onClick.AsObservable()
                .Throttle(TimeSpan.FromSeconds(2))
                .Do(_ => Debug.Log("111"))
                .Throttle(TimeSpan.FromSeconds(2))
                .Do(_ => Debug.Log("222"))
                .Throttle(TimeSpan.FromSeconds(2))
                .Subscribe(_ => Debug.Log("333"));
        }
    }

    public class MVVMTestModel : ViewModelBase
    {

    }
}
