using System.Collections;
using UnityEngine;
using System.IO;
using ILRuntime.Runtime.Enviorment;
using System;
using UnityEngine.UI;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using System.Collections.Generic;

public class ILRuntimeTest : MonoBehaviour
{
    ILRuntime.Runtime.Enviorment.AppDomain appDomain;
    byte[] dllBytes;
    byte[] pdbBytes;

    public bool usePdb = true;

    public Transform contentRoot;
    public Button button;

    void Start()
    {
        contentRoot = GameObject.Find("Content").transform;
        button = GameObject.Find("Button").GetComponent<Button>();
        StartCoroutine(LoadHotFixAssembly());
    }

    IEnumerator LoadHotFixAssembly()
    {
        appDomain = new ILRuntime.Runtime.Enviorment.AppDomain();
#if UNITY_ANDROID
        WWW www = new WWW(Application.streamingAssetsPath + "/GameModelTest.dll");
#else
        WWW www = new WWW("file:///" + Application.streamingAssetsPath + "/GameModelTest.dll");
#endif
        while (!www.isDone)
        {
            yield return null;
        }
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.LogError(www.error);
        }
        dllBytes = www.bytes;
        www.Dispose();

        if (usePdb)
        {
#if UNITY_ANDROID
        www = new WWW(Application.streamingAssetsPath + "/GameModelTest.pdb");
#else
            www = new WWW("file:///" + Application.streamingAssetsPath + "/GameModelTest.pdb");
#endif
            while (!www.isDone)
            {
                yield return null;
            }
            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.LogError(www.error);
            }
            pdbBytes = www.bytes;          
        }

        using (MemoryStream fs = new MemoryStream(dllBytes))
        {
            if (pdbBytes != null)
            {
                using (MemoryStream p = new MemoryStream(pdbBytes))
                {
                    appDomain.LoadAssembly(fs, p, new Mono.Cecil.Pdb.PdbReaderProvider());
                }
            }
            else
            {
                appDomain.LoadAssembly(fs, null, new Mono.Cecil.Pdb.PdbReaderProvider());
            }
        }

        InitializeILRuntime();
        OnHotFixLoaded();
    }

    void InitializeILRuntime()
    {
        //appDomain.RegisterValueTypeBinder(typeof(Vector3), new Vector3Binder());
    }

    void OnHotFixLoaded()
    {
        #region TestOne

        new NativeTestOne("NativeTestOne", appDomain, contentRoot, button, true);
        new ILRunTimeTestOne("ILRunTimeTestOne", appDomain, contentRoot, button, true);

        #endregion

        #region Invocation

        new ILRuntimeInvocationClass("InvocationClass", appDomain, contentRoot, button, false);

        #endregion

        #region ValueTypeBinder

        new NativeValueTypeBinderClass("NativeValueTypeBinderClass", appDomain, contentRoot, button, true);
        new ILRunTimeValueTypeBinderClass("ILValueTypeBinderClass", appDomain, contentRoot, button, true);
        
        #endregion
    }

    void Update()
    {

    }
}

public class ExcuteTestClass
{
    public string TestName
    {
        get;
        set;
    }

    public Button button;

    public ILRuntime.Runtime.Enviorment.AppDomain AppDomain
    {
        get;
        set;
    }

    public ExcuteTestClass(string testName, ILRuntime.Runtime.Enviorment.AppDomain appDomain, Transform root, Button button, bool showTime)
    {
        TestName = testName;
        AppDomain = appDomain;
        button = UnityEngine.Object.Instantiate(button);
        button.transform.SetParent(root);
        button.GetComponentInChildren<Text>().text = TestName;
        button.onClick.AddListener(() => Start(showTime));
    }

    public void Start(bool showTime)
    {
        if (showTime)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            Excute();
            sw.Stop();

            Debug.Log(string.Format(TestName + " CostTime: {0}ms",sw.ElapsedMilliseconds));
            LoggerProvider.Debug.Write(string.Format(TestName + " CostTime: {0}ms", sw.ElapsedMilliseconds));
        }
        else
        {
            Excute();
        }
    }

    public virtual void Excute()
    {
        Debug.Log("Start TestName: ");
        LoggerProvider.Debug.Write("Start TestName: ");
    }
}

public class NativeTestOne : ExcuteTestClass
{
    public NativeTestOne(string name, ILRuntime.Runtime.Enviorment.AppDomain appDomain, Transform root, Button button, bool showTime) : base(name, appDomain, root, button, showTime)
    {

    }

    public override void Excute()
    {
        base.Excute();
        Vector3[] vArray = new Vector3[100000];
        for (int i = 0; i < 100000; i++)
        {
            vArray[i] = new Vector3(i, i, i);
        }
    }
}

public class ILRunTimeTestOne : ExcuteTestClass
{
    public ILRunTimeTestOne(string name, ILRuntime.Runtime.Enviorment.AppDomain appDomain, Transform root, Button button, bool showTime) : base(name, appDomain, root, button, showTime)
    {

    }

    public override void Excute()
    {
        base.Excute();
        AppDomain.Invoke("GameModelTest.TestClass", "StaticFunTest", null, null);
    }
}

public class ILRuntimeInvocationClass : ExcuteTestClass
{
    public ILRuntimeInvocationClass(string name, ILRuntime.Runtime.Enviorment.AppDomain appDomain, Transform root, Button button, bool showTime) : base(name, appDomain, root, button, showTime)
    {

    }

    public override void Excute()
    {
        base.Excute();

        //调用无参数静态方法，appdomain.Invoke("类名", "方法名", 对象引用, 参数列表);
        AppDomain.Invoke("GameModelTest.InvocationClass", "StaticFunTest", null, null);

        //调用带参数的静态方法
        AppDomain.Invoke("GameModelTest.InvocationClass", "StaticFunTest2", null, 123);

        //预先获得IMethod，可以减低每次调用查找方法耗用的时间
        IType type = AppDomain.LoadedTypes["GameModelTest.InvocationClass"];
        //根据方法名称和参数个数获取方法
        IMethod method = type.GetMethod("StaticFunTest", 0);
        AppDomain.Invoke(method, null, null);

        //指定参数类型来获得IMethod
        IType intType = AppDomain.GetType(typeof(int));
        //参数类型列表
        List<IType> paramList = new List<IType>();
        paramList.Add(intType);
        //根据方法名称和参数类型列表获取方法
        method = type.GetMethod("StaticFunTest2", paramList, null);
        AppDomain.Invoke(method, null, 456);


        //实例化热更里的类
        object obj = AppDomain.Instantiate("GameModelTest.InvocationClass", new object[] { 233, "HelloWorld"});
        //第二种方式
        object obj2 = ((ILType)type).Instantiate();

        //调用成员方法
        AppDomain.Invoke("GameModelTest.InvocationClass", "get_ID", obj, null);
        AppDomain.Invoke("GameModelTest.InvocationClass", "InstanceMethod", obj2, null);

        //调用泛型方法
        IType stringType = AppDomain.GetType(typeof(string));
        IType[] genericArguments = new IType[] { stringType };
        AppDomain.InvokeGenericMethod("GameModelTest.InvocationClass", "GenericMethod", genericArguments, null, "TestString");

        //获取泛型方法的IMethod
        paramList.Clear();
        paramList.Add(intType);
        genericArguments = new IType[] { intType };
        method = type.GetMethod("GenericMethod", paramList, genericArguments);
        AppDomain.Invoke(method, null, 33333);
    }
}

public class NativeValueTypeBinderClass : ExcuteTestClass
{
    public NativeValueTypeBinderClass(string name, ILRuntime.Runtime.Enviorment.AppDomain appDomain, Transform root, Button button, bool showTime) : base(name, appDomain, root, button, showTime)
    {

    }

    public override void Excute()
    {
        base.Excute();
        float dot = 0;
        Vector3 a = new Vector3(1, 2, 3);
        Vector3 b = Vector3.one;
        for (int i = 0; i < 100000; i++)
        {
            a += Vector3.one;
            dot += Vector3.Dot(a, Vector3.zero);
        }
    }
}

public class ILRunTimeValueTypeBinderClass : ExcuteTestClass
{
    public ILRunTimeValueTypeBinderClass(string name, ILRuntime.Runtime.Enviorment.AppDomain appDomain, Transform root, Button button, bool showTime) : base(name, appDomain, root, button, showTime)
    {

    }

    public override void Excute()
    {
        base.Excute();
        AppDomain.Invoke("GameModelTest.ValueTypeBinder", "RunTest", null, null);
    }
}


