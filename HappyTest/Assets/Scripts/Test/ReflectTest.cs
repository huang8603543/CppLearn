using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Happy.Util;
using UnityEngine.UI;
using System.Reflection;
using Happy.Main;

public class ReflectTest : SingletonMono<ReflectTest>
{
    public Transform contentRoot;
    public Button button;

    void Start()
    {
        contentRoot = GameObject.Find("Content").transform;
        button = GameObject.Find("Button").GetComponent<Button>();
        TestCase();
    }

    void TestCase()
    {
        new MVVMTestReflect("MVVMTestReflect", GameApplication.Instance.ReHotFix.assembly, contentRoot, button, true);
    }
}

public class MVVMTestReflect : ExcuteTestClass
{
    public MVVMTestReflect(string testName, Assembly assemly, Transform root, Button button, bool showTime) : base(testName, assemly, root, button, showTime)
    {

    }

    public override void Excute()
    {
        base.Excute();
        Assembly.GetType("GameModelTest.MVVMTest").GetMethod("TestThree").Invoke(null, null);
    }
}
