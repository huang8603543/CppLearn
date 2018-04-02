using System.Collections.Generic;
using System;

namespace Happy.Main
{
    public class ServiceLocatorContain
    {
        public string TypeName
        {
            get;
            private set;
        }

        public Func<object> Function
        {
            get;
            set;
        }

        public ServiceLocatorContain(string name, Func<object> func)
        {
            TypeName = name;
            Function = func;
        }
    }

    public class ServiceLocator
    {
        private SingletonObjectFactory _singletonObjectFactory = new SingletonObjectFactory();
        private TransientObjectFactory _transientObjectFactory = new TransientObjectFactory();
        private static readonly Dictionary<Type, ServiceLocatorContain> Container = new Dictionary<Type, ServiceLocatorContain>();

        public void RegisterSingleton<TInterface, TInstance>(string interfaceName, string typeName) where TInstance : class, new()
        {
            ServiceLocatorContain contain = new ServiceLocatorContain(typeName, Lazy<TInstance>(FactoryType.Singleton, typeName));
            Type type = GameApplication.Instance.hotFix.LoadType(interfaceName);
            if (!Container.ContainsKey(type))
            {
                Container.Add(type, contain);
            }
            else
            {
                throw new Exception("Container contains key: " + type);
            }
            
        }

        public void RegisterSingleton<TInstance>(string typeName) where TInstance : class, new()
        {
            ServiceLocatorContain contain = new ServiceLocatorContain(typeName, Lazy<TInstance>(FactoryType.Singleton, typeName));
            Type type = GameApplication.Instance.hotFix.LoadType(typeName);
            if (!Container.ContainsKey(type))
            {
                Container.Add(type, contain);
            }
            else
            {
                throw new Exception("Container contains key: " + type);
            }
        }

        public void RegisterTransient<TInterface, TInstance>(string interfaceName, string typeName) where TInstance : class, new()
        {
            ServiceLocatorContain contain = new ServiceLocatorContain(typeName, Lazy<TInstance>(FactoryType.Transient, typeName));
            Type type = GameApplication.Instance.hotFix.LoadType(interfaceName);
            if (!Container.ContainsKey(type))
            {
                Container.Add(type, contain);
            }
            else
            {
                throw new Exception("Container contains key: " + type);
            }
        }

        public void RegisterTransient<TInstance>(string typeName) where TInstance : class, new()
        {
            ServiceLocatorContain contain = new ServiceLocatorContain(typeName, Lazy<TInstance>(FactoryType.Transient, typeName));
            Type type = GameApplication.Instance.hotFix.LoadType(typeName);
            if (!Container.ContainsKey(type))
            {
                Container.Add(type, contain);
            }
            else
            {
                throw new Exception("Container contains key: " + type);
            }
        }

        public void Clear()
        {
            Container.Clear();
        }

        public TInterface Resolve<TInterface>(string keyName) where TInterface : class
        {
            return Resolve(GameApplication.Instance.hotFix.LoadType(keyName)) as TInterface;
        }

        private static object Resolve(Type type)
        {
            if (!Container.ContainsKey(type))
            {
                return null;
            }
            return Container[type].Function();
        }

        private Func<object> Lazy<TInstance>(FactoryType factoryType, string typeFullName) where TInstance : class, new()
        {
            return () =>
            {
                switch (factoryType)
                {
                    case FactoryType.Singleton:
                        return _singletonObjectFactory.AcquireObject(typeFullName);
                    default:
                        return _transientObjectFactory.AcquireObject(typeFullName);
                }
            };
        }
    }
}
