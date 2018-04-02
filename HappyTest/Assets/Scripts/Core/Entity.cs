using System;
using System.Collections.Generic;

namespace Happy.Core
{
    public partial class Entity : ComponentWithId
    {
        private HashSet<Component> components;

        private Dictionary<Type, Component> componentDict;

        protected Entity()
        {
            components = new HashSet<Component>();
            componentDict = new Dictionary<Type, Component>();
        }

        protected Entity(long id) : base(id)
        {
            components = new HashSet<Component>();
            componentDict = new Dictionary<Type, Component>();
        }

        //public K AddComponent<K>() where K : Component, new()
        //{
        //    K component = ComponentFactory
        //}

        public override void Dispose()
        {
            base.Dispose();
            //foreach (var component in GetComponents())
            //{
            //    try
            //    {
            //        component.Dispose();
            //    }
            //    catch (Exception ex)
            //    {
            //        LoggerProvider.Error.Write(ex.ToString());
            //    }
            //}
            components.Clear();
            componentDict.Clear();
        }


    }
}
