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
        public string controllerPackageName { get; set; }  //指定的Controller层的包名
        public string jsDir { get; set; } //前端的js文件目录
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
        //public List<EntityClassPropertyInfo> DateColumnList
        //{
        //    get;
        //    private set;
        //}
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

        /// <summary>
        /// 指定需要列表显示的列
        /// </summary>
        public List<EntityClassPropertyInfo> gridColumnList
        {
            get;
            private set;
        }

        /// <summary>
        /// 指定需要快速查询的列
        /// </summary>
        public List<EntityClassPropertyInfo> quickQueryColumnList
        {
            get;
            private set;
        }
        /// <summary>
        /// 指定需要高级查询的列
        /// </summary>
        public List<EntityClassPropertyInfo> expertQueryColumnList
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

                //int iPos = excludes.IndexOf(dr["name"].ToString());
                //if (iPos>-1)
                //    continue;

                ropertyListTemp.Add(new EntityClassPropertyInfo(dr, codeLanguage));
            }
            this.RopertyList = ropertyListTemp;

            //处理主键列
            List<EntityClassPropertyInfo> primaryKeyListTemp = new List<EntityClassPropertyInfo>();

            //常规列
            List<EntityClassPropertyInfo> NormalColumnListTemp = new List<EntityClassPropertyInfo>();

            //列表显示的列
            List<EntityClassPropertyInfo> gridColumnListTemp = new List<EntityClassPropertyInfo>();

            //快速查询列
            List<EntityClassPropertyInfo> quickQueryColumnListTemp = new List<EntityClassPropertyInfo>();

            //高级查询列
            List<EntityClassPropertyInfo> expertQueryColumnListTemp = new List<EntityClassPropertyInfo>();

            foreach (EntityClassPropertyInfo entity in RopertyList)
            {
                //主键列
                if (entity.primaryKey==1)
                    primaryKeyListTemp.Add(entity);
                else
                {
                    NormalColumnListTemp.Add(entity);
                    if (entity.colGrid == 1)
                        gridColumnListTemp.Add(entity);
                    if (entity.colQuickQuery == 1)
                        quickQueryColumnListTemp.Add(entity);
                    if (entity.colExpertQuery == 1)
                        expertQueryColumnListTemp.Add(entity);
                }
            }
            this.PrimaryKeyList = primaryKeyListTemp;
            this.gridColumnList = gridColumnListTemp;
            this.quickQueryColumnList = quickQueryColumnListTemp;
            this.expertQueryColumnList = expertQueryColumnListTemp;
            this.NormalColumnList = NormalColumnListTemp;
        }
    }
}