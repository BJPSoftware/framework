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

        /// <summary>
        /// 将json串转换成对象
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        public static  Object fromJson(string jsonData) {
            return JsonConvert.DeserializeObject(jsonData);
        }

        /// <summary>
        /// 将json串转换成指定的泛型类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        public static T fromJson<T>(string jsonData)
        {
            return JsonConvert.DeserializeObject<T>(jsonData);
        }

        public static List<T> fromJsonArray<T>(string jsonData){
            return JsonConvert.DeserializeObject<List<T>>(jsonData);
        }
        
    }
}
