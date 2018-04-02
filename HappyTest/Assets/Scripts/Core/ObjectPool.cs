using System.Collections.Generic;
using Happy.Util;
using System;
using Happy.Main;

namespace Happy.Core
{
    /// <summary>
    /// 对象池///
    /// </summary>
    public class ObjectPool : Singleton<ObjectPool>
    {
        private readonly Dictionary<Type, Queue<object>> dictionary = new Dictionary<Type, Queue<object>>();

        public object Fetch(Type type, string typeName = "")
        {
            if (!string.IsNullOrEmpty(typeName))
            {
                type = GameApplication.Instance.hotFix.LoadType(typeName);
            }

            Queue<object> queue;
            if (!dictionary.TryGetValue(type, out queue))
            {
                queue = new Queue<object>();
                dictionary.Add(type, queue);
            }
            object obj;
            if (queue.Count > 0)
            {
                obj = queue.Dequeue();
                return obj;
            }
            if (!string.IsNullOrEmpty(typeName))
            {
                obj = GameApplication.Instance.hotFix.CreateInstance(typeName);
            }
            else
            {
                obj = Activator.CreateInstance(type);
            }
            return obj;
        }      

        public object Fetch<T>(string typeName) where T : Component
        {
            object t = Fetch(typeof(T), typeName);
            return t;
        }

        //public T Fetch<T>() where T : Component
        //{
        //    T t = Fetch(typeof(T));
        //    return T;
        //}

        public void Recycle(object obj, string typeName)
        {
            Type type;
            if (!string.IsNullOrEmpty(typeName))
            {
                type = GameApplication.Instance.hotFix.LoadType(typeName);
            }
            else
            {
                type = obj.GetType();
            }
            Queue<object> queue;
            if (!dictionary.TryGetValue(type, out queue))
            {
                queue = new Queue<object>();
                dictionary.Add(type, queue);
            }
            queue.Enqueue(obj);
        }

        public void Recycle(Component obj)
        {
            Type type = obj.GetType();
            Queue<object> queue;
            if (!dictionary.TryGetValue(type, out queue))
            {
                queue = new Queue<object>();
                dictionary.Add(type, queue);
            }
            queue.Enqueue(obj);
        }
    }
}
