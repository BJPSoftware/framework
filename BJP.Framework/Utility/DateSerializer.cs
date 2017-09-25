/*
* ==============================================================================
*
* File name: DateSerializer
* Description: json序列化日期格式
*
* Version: 1.0
* Created: 2017-09-25 14:43:21
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJP.Framework.Utility
{
    public class DateSerializer: IsoDateTimeConverter
    {
        public DateSerializer() {
            base.DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
