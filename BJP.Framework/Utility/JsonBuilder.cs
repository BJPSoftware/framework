using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace BJP.Framework.Utility
{
    public static class JsonBuilder
    {
        /// <summary>
        /// 将对象转换成json格式串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static String toJson(Object obj) {
           return JsonConvert.SerializeObject(obj);
        }

        public static  Object fromJson(string jsonData) {
            return JsonConvert.DeserializeObject(jsonData);
        }

        public static T fromJson<T>(string jsonData)
        {
            return JsonConvert.DeserializeObject<T>(jsonData);
        }
    }
}
