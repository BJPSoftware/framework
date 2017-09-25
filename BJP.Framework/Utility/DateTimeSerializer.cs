/*
* ==============================================================================
*
* File name: DateTimeSerializer
* Description: json序列化日期时间格式
*
* Version: 1.0
* Created: 2017-09-25 11:44:06
*
* Author: yiboLuo
* Company: Your company name
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BJP.Framework.Utility
{
    public class DateTimeSerializer: IsoDateTimeConverter
    {
        public DateTimeSerializer() {
            base.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        }
    }
}
