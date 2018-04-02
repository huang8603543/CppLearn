using Happy.Util;

namespace Happy.Core
{
    public class ComponentFactory : Singleton<ComponentFactory>
    {
        //public Component CreateWithParent(Type type, Component parent, string typeName = "")
        //{
        //    Component component = ObjectPool.Instance.Fetch(type, typeName);
        //    component.Parent = parent;
        //    //Game.EventSystem.Awake(component);
        //    return component;
        //}

        //public T CreateWithParent<T>(Component parent, string typeName = "") where T : Component
        //{
        //    T component = ObjectPool.Instance.Fetch<T>(typeName);
        //    component.Parent = parent;
        //    //Game.EventSystem.Awake(component);
        //    return component;
        //}

        //public T CreateWithParent<T, A>(Component parent, A a, string typeName = "") where T : Component
        //{
        //    T component = ObjectPool.Instance.Fetch<T>(typeName);
        //    component.Parent = parent;
        //    //Game.EventSystem.Awake(component, a);
        //    return component;
        //}

        //public T CreateWithParent<T, A, B>(Component parent, A a, B b, string typeName = "") where T : Component
        //{
        //    T component = ObjectPool.Instance.Fetch<T>(typeName);
        //    component.Parent = parent;
        //    //Game.EventSystem.Awake(component, a, b);
        //    return component;
        //}

        //public T CreateWithParent<T, A, B, C>(Component parent, A a, B b, C c, string typeName = "") where T : Component
        //{
        //    T component = ObjectPool.Instance.Fetch<T>(typeName);
        //    component.Parent = parent;
        //    //Game.EventSystem.Awake(component, a, b, c);
        //    return component;
        //}

        //public T Create<T>(string typeName = "") where T : Component
        //{
        //    T component = ObjectPool.Instance.Fetch<T>(typeName);
        //    //Game.EventSystem.Awake(component);
        //    return component;
        //}

        public object Create<T>(string typeName = "") where T : Component
        {
            object component = ObjectPool.Instance.Fetch<T>(typeName);
            //Game.EventSystem.Awake(component);
            //GameApplication.Instance.ILHotFix.appDomain.Invoke(typeName, "Awake", component, null);
            return component;
        }

        //public T Create<T, A>(A a, string typeName = "") where T : Component
        //{
        //    T component = ObjectPool.Instance.Fetch<T>(typeName);
        //    //Game.EventSystem.Awake(component, a);
        //    return component;
        //}

        //public T Create<T, A, B>(A a, B b, string typeName = "") where T : Component
        //{
        //    T component = ObjectPool.Instance.Fetch<T>(typeName);
        //    //Game.EventSystem.Awake(component, a, b);
        //    return component;
        //}

        //public T Create<T, A, B, C>(A a, B b, C c, string typeName = "") where T : Component
        //{
        //    T component = ObjectPool.Instance.Fetch<T>(typeName);
        //    //Game.EventSystem.Awake(component, a, b, c);
        //    return component;
        //}

        //public T CreateWithId<T>(long id, string typeName = "") where T : ComponentWithId
        //{
        //    T component = ObjectPool.Instance.Fetch<T>(typeName);
        //    component.Id = id;
        //    //Game.EventSystem.Awake(component);
        //    return component;
        //}

        //public T CreateWithId<T, A>(long id, A a, string typeName = "") where T : ComponentWithId
        //{
        //    T component = ObjectPool.Instance.Fetch<T>(typeName);
        //    component.Id = id;
        //    //Game.EventSystem.Awake(component, a);
        //    return component;
        //}

        //public T CreateWithId<T, A, B>(long id, A a, B b, string typeName = "") where T : ComponentWithId
        //{
        //    T component = ObjectPool.Instance.Fetch<T>(typeName);
        //    component.Id = id;
        //    //Game.EventSystem.Awake(component, a, b);
        //    return component;
        //}

        //public T CreateWithId<T, A, B, C>(long id, A a, B b, C c, string typeName = "") where T : ComponentWithId
        //{
        //    T component = ObjectPool.Instance.Fetch<T>(typeName);
        //    component.Id = id;
        //    //Game.EventSystem.Awake(component, a, b, c);
        //    return component;
        //}
    }
}
