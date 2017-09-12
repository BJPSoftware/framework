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
        public static String toJson(Object obj) {
           return JsonConvert.SerializeObject(obj);
        }
    }
}
