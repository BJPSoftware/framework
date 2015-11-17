using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJP.Framework.Repository
{
    public class DbHelper
    {
        private static string _ConnectionString = ConfigurationManager.ConnectionStrings[ "connection" ].ConnectionString;
        private static string _ProviderName = ConfigurationManager.ConnectionStrings[ "connection" ].ProviderName;

        private DbConnection Connection;

        #region 构造函数，创建数据库连接
        /// <summary> 
        /// 构造函数，创建数据库连接 
        /// </summary> 
        public DbHelper()
        {
            this.Connection = CreateConnection( DbHelper._ConnectionString );
        }

        /// <summary> 
        /// 构造函数重载，创建数据库连接 
        /// </summary> 
        /// <param name = "connectionString" > 数据库驱动 </ param >
        public DbHelper( string connectionString )
        {
            this.Connection = CreateConnection( connectionString );
        }
        #endregion

        #region 通过工厂创建数据库连接
        /// <summary> 
        /// 构造函数调用，通过工厂创建数据库连接 
        /// </summary> 
        /// <returns></returns> 
        public static DbConnection CreateConnection()
        {
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory( DbHelper._ProviderName );
            DbConnection dbconn = dbfactory.CreateConnection();
            dbconn.ConnectionString = _ConnectionString;
            return dbconn;
        }
        /// <summary> 

        /// 构造函数重载调用，通过工厂创建数据库连接 
        /// </summary> 
        /// <param name="connectionString">数据库驱动</param> 
        /// <returns></returns> 
        public static DbConnection CreateConnection( string connectionString )
        {
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory( _ProviderName );
            DbConnection dbconn = dbfactory.CreateConnection();
            dbconn.ConnectionString = connectionString;
            return dbconn;
        }
        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// 执行指定的SQL语句，并返回受影响的记录数
        /// </summary>
        /// <param name="sqlText">要执行的SQL语句</param>
        /// <returns>受影响的记录数</returns>
        public int Add( string sqlText )
        {
            Connection.Open();
            DbCommand cmd = Connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sqlText;
            int ret = cmd.ExecuteNonQuery();
            Connection.Close();

            return ret;
        }

        #endregion

        #region ExectueTable
        /// <summary>
        /// 执行指定的SQL语句，并返回对表中的数据
        /// </summary>
        /// <param name="sqlText">要执行的SQL语句</param>
        /// <returns>查询结果表中数据</returns>
        public DataTable Fill( string sqlText )
        {
            Connection.Open();
            DbCommand cmd = Connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sqlText;

            DbProviderFactory dbfactory = DbProviderFactories.GetFactory( DbHelper._ProviderName );
            DbDataAdapter da = dbfactory.CreateDataAdapter();
            da.SelectCommand = cmd;

            DataTable dt = new DataTable();
            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
            da.Fill( dt );

            Connection.Close();

            return dt;
        }
        #endregion
    }
}
