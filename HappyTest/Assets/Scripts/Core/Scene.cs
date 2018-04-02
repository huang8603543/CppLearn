using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Happy.Core
{
    public class Scene : Entity
    {
        public string Name
        {
            get;
            set;
        }

        public Scene()
        {
        }

        public Scene(long id) : base(id)
        {
        }
    }
}
