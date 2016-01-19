using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BJP.Framework.Utility;

namespace BJP.Framework.Code
{
    [Serializable]
    public class EntityClassPropertyInfo
    {
        public EntityClassPropertyInfo(DataRow drColumn,codeLanguage codeLanguage)
        {
            //列名及转换后的属性名
            this.ColumnName = drColumn["name"].ToString();
            this.PropertyName = ConvertHelper.SplitAndToFirstUpper(this.ColumnName,'_');

            //列数据类型及转换成系统的数据类型
            this.ColumnDataType = drColumn["DataType"].ToString();
            this.PropertyType = ConvertHelper.SqlserverTypeToSys(this.ColumnDataType,codeLanguage);

            this.AutoIncrement = drColumn["AutoIncrement"].ToString();
            this.IsPk = drColumn["IsPk"].ToString();
            this.MaxLength = Convert.ToInt16(drColumn["MaxLength"].ToString());
            this.CanNull = drColumn["CanNull"].ToString()=="是" ? "true" :"false";
            this.DefaultValue = drColumn["DefaultValue"].ToString();
            this.ColumnLabel = drColumn["Label"].ToString();
            if (string.IsNullOrEmpty(this.ColumnLabel))
                this.ColumnLabel = this.PropertyName;
        }

        //列名
        public string ColumnName
        {
            get;
            private set;
        }
        //列转换成类对应的属性名
        public string PropertyName
        {
            get;
            private set;
        }
        //列的数据类型
        public string ColumnDataType
        {
            get;
            private set;
        }
        //系统语言对应的数据类型
        public string PropertyType
        {
            get;
            private set;
        }

        //是否主键列
        public string IsPk
        {
            get;
            private set;
        }
        //是否自增长字段
        public string AutoIncrement
        {
            get;
            private set;
        }
        //列的长度
        public int MaxLength
        {
            get;
            private set;
        }
        //是否可为空
        public string CanNull
        {
            get;
            private set;
        }
        //是否有缺省值
        public string DefaultValue
        {
            get;
            private set;
        }
        //列的说明
        public string ColumnLabel
        {
            get;
            private set;
        }

        //public override bool Equals(object obj)
        //{
        //    EntityClassPropertyInfo temp = obj as EntityClassPropertyInfo;
        //    if (this.PropertyName == temp.PropertyName && this.PropertyType == temp.PropertyType)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        
    }
}
