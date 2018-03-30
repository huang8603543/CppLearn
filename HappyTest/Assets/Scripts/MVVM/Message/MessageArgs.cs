using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Happy.MVVM
{
    public class MessageArgs<T>
    {
        public T Item
        {
            get;
            private set;
        }

        public MessageArgs(T item)
        {
            Item = item;
        }
    }
}
