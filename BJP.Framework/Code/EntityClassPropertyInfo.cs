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
        public EntityClassPropertyInfo(DataRow drColumn, codeLanguage codeLanguage)
        {
            ////列名及转换后的属性名
            //this.ColumnName = drColumn["name"].ToString();
            //this.PropertyName = ConvertHelper.SplitAndToFirstUpper(this.ColumnName, '_');

            ////列数据类型及转换成系统的数据类型
            //this.ColumnDataType = drColumn["DataType"].ToString();
            //this.PropertyType = ConvertHelper.SqlserverTypeToSys(this.ColumnDataType, codeLanguage);

            //this.AutoIncrement = drColumn["AutoIncrement"].ToString();
            //this.IsPk = drColumn["IsPk"].ToString();
            //this.MaxLength = Convert.ToInt16(drColumn["MaxLength"].ToString());
            //this.CanNull = drColumn["CanNull"].ToString() == "是" ? "true" : "false";
            //this.DefaultValue = drColumn["DefaultValue"].ToString();
            //this.ColumnLabel = drColumn["Label"].ToString();
            //if (string.IsNullOrEmpty(this.ColumnLabel))
            //    this.ColumnLabel = this.PropertyName;

            this.colCode = drColumn["COL_CODE"].ToString();
            this.propertyName = ConvertHelper.SplitAndToFirstUpper(this.colCode, '_');
            this.colName = drColumn["COL_NAME"].ToString();
            this.colType = drColumn["COL_TYPE"].ToString();
            this.propertyType = ConvertHelper.SqlserverTypeToSys(this.colType, codeLanguage);
            this.colLength = Convert.ToInt16(drColumn["COL_LENGTH"].ToString());
            this.primaryKey = Convert.ToInt16(drColumn["PRIMARY_KEY"].ToString());
            this.colPrecision = Convert.ToInt16(drColumn["COL_PRECISION"].ToString());
            this.colGrid = Convert.ToInt16(drColumn["LIST_DISPLAY"].ToString());
            this.colQuickQuery = Convert.ToInt16(drColumn["QUICK_QUERY"].ToString());
            this.colExpertQuery = Convert.ToInt16(drColumn["EXPERT_QUERY"].ToString());
            this.canEdit = Convert.ToInt16(drColumn["CAN_EDIT"].ToString());
            this.canNull = drColumn["CAN_NULL"].ToString() == "1" ? "true" : "false"; ;
            this.orderIndex = Convert.ToInt16(drColumn["ORDER_INDEX"].ToString());
            this.dicCode = drColumn["DIC_CODE"].ToString();
            this.colDesc = drColumn["COL_DESC"].ToString();
            this.tableName = drColumn["TABLE_NAME"].ToString();
            this.inputType = drColumn["INPUT_TYPE"].ToString();

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
        //列的长度
        public int MaxLength
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

        #region 重新组装生成模板时的实体属性定义
        /// <summary>
        /// 列编码，对应数据表中的列
        /// </summary>
        public string colCode
        {
            get;
            private set;
        }
        /// <summary>
        /// 列转换成类对应的属性名,是用colCode转的
        /// </summary>
        public string propertyName
        {
            get;
            private set;
        }
        /// <summary>
        /// 列中文名
        /// </summary>
        public string colName
        {
            get;
            private set;
        }
        /// <summary>
        /// 列的数据类型，和数据库表中列的数据类型对应
        /// </summary>
        public string colType
        {
            get;
            private set;
        }
        /// <summary>
        /// 系统语言对应的数据类型
        /// </summary>
        public string propertyType
        {
            get;
            private set;
        }
        /// <summary>
        /// 列的长度，对应数据库表中列的长度
        /// </summary>
        public int colLength
        {
            get;
            private set;
        }
        /// <summary>
        /// 小数位数,对应数据库中列的小数位数
        /// </summary>
        public int colPrecision
        {
            get;
            private set;
        }
        /// <summary>
        /// 是否主键列 0-不是 1-是
        /// </summary>
        public int primaryKey
        {
            get;
            private set;
        }
        /// <summary>
        /// 是否列表显示列 0-不是 1-是
        /// </summary>
        public int colGrid
        {
            get;
            private set;
        }
        /// <summary>
        /// 是否快速查询列 0-不是 1-是
        /// </summary>
        public int colQuickQuery
        {
            get;
            private set;
        }
        /// <summary>
        /// 是否高级查询列 0-不是 1-是
        /// </summary>
        public int colExpertQuery
        {
            get;
            private set;
        }
        /// <summary>
        /// 列是否可以编辑，可编辑的要生成输入框 0-不是 1-是
        /// </summary>
        public int canEdit
        {
            get;
            private set;
        }
        /// <summary>
        /// 是否可以为空 0-不是 1-是
        /// </summary>
        public string canNull
        {
            get;
            private set;
        }
        /// <summary>
        /// 列的输入方式
        /// input-手工输入, dic-字典选择, list-列表选择,  dit-数字输入, date-日期, time-时间, datetime-日期时间,
        /// checkbox ,checkbookgroup,radio,radiogroup
        /// </summary>
        public string inputType
        {
            get;
            private set;
        }
        /// <summary>
        /// 字典编码，当inputType=dic时需要配置值
        /// </summary>
        public string dicCode
        {
            get;
            private set;
        }
        /// <summary>
        /// 排序号，在列表或生成输入框时的先后顺序
        /// </summary>
        public int orderIndex
        {
            get;
            private set;
        }
        /// <summary>
        /// 列的详细说明
        /// </summary>
        public string colDesc
        {
            get;
            private set;
        }
        /// <summary>
        /// 数据表名
        /// </summary>
        public string tableName
        {
            get;
            private set;
        }
        #endregion

        //是否列表显示
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
