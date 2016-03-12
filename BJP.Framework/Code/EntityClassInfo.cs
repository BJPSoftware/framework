using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BJP.Framework.Utility;

namespace BJP.Framework.Code
{
    [Serializable]
    public class EntityClassInfo
    {
        public string tableName { get; set; }    //指定的数据表名
        public string className { get; set; }   //要生成的类的名称
        public string tableComment { get; set; } //类的中文说明

        #region 包名定义
        public string packageName { get; set; }  //指定的包名
        public string daoPackageName { get; set; }  //指定的dao接口包名
        public string servicePackageName { get; set; }  //指定的service接口包名
        #endregion

        public DataTable dataTable { get; set; }  //列信息指定的数据表
        public string excludes { get; set; }   //生成实体时要排除的字段
        public codeLanguage codeLanguage { get; set; }    //转换的编程语言

        //指定表的所有列属性
        public List<EntityClassPropertyInfo> RopertyList
        {
            get;
            private set;
        }

        //指定表的日期列
        //因为在生成实体时需要对日期列进行单独的处理，所以单独列出
        public List<EntityClassPropertyInfo> DateColumnList
        {
            get;
            private set;
        }
        //指定表的主键列
        //生成实体时要单独处理
        public List<EntityClassPropertyInfo> PrimaryKeyList
        {
            get;
            private set;
        }
        //指定表的外键列
        //用来生成一对多或多对一的关系
        public List<EntityClassPropertyInfo> ForeignKeyList
        {
            get;
            private set;
        }

        //除主键、外键、日期列外的其它列
        public List<EntityClassPropertyInfo> NormalColumnList
        {
            get;
            private set;
        }
        public EntityClassInfo()
        {
        }

        /// <summary>
        /// 处理数据表的列信息
        /// </summary>
        public void createColumnInfo()
        {
            List<EntityClassPropertyInfo> ropertyListTemp = new List<EntityClassPropertyInfo>();

            //生成所有的列
            foreach (DataRow dr in dataTable.Rows)
            {

                int iPos = excludes.IndexOf(dr["name"].ToString());
                if (iPos>-1)
                    continue;

                ropertyListTemp.Add(new EntityClassPropertyInfo(dr, codeLanguage));
            }
            this.RopertyList = ropertyListTemp;

            //处理主键列
            List<EntityClassPropertyInfo> primaryKeyListTemp = new List<EntityClassPropertyInfo>();
            //日期列
            List<EntityClassPropertyInfo> dateColumnListTemp = new List<EntityClassPropertyInfo>();
            //常规列
            List<EntityClassPropertyInfo> NormalColumnListTemp = new List<EntityClassPropertyInfo>();

            foreach (EntityClassPropertyInfo entity in RopertyList)
            {
                if (entity.IsPk == "是")
                    primaryKeyListTemp.Add(entity);
                else if (entity.ColumnDataType == "datetime" || entity.ColumnDataType == "date")
                    dateColumnListTemp.Add(entity);
                else
                    NormalColumnListTemp.Add(entity);
            }
            this.PrimaryKeyList = primaryKeyListTemp;
            this.DateColumnList = dateColumnListTemp;
            this.NormalColumnList = NormalColumnListTemp;
        }
    }
}