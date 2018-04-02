using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

namespace Happy.Util
{
    public static class JsonHelper
    {
        public static string ToJson(object obj)
        {
            return JsonMapper.ToJson(obj);
        }
    }
}
