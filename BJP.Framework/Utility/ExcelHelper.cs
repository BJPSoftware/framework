using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;


namespace BJP.Framework.Utility
{
    public static class ExcelHelper
    {
        public static string mFilename;
        public static Microsoft.Office.Interop.Excel.Application app;
        public static Microsoft.Office.Interop.Excel.Workbooks wbs;
        public static Microsoft.Office.Interop.Excel.Workbook wb;
        public static Microsoft.Office.Interop.Excel.Worksheets wss;
        public static Microsoft.Office.Interop.Excel.Worksheet ws;

        /// <summary>
        /// 创建一个Excel对象
        /// </summary>
        public static void Create()
        {
            app = new Microsoft.Office.Interop.Excel.Application();
            wbs = app.Workbooks;
            wb = wbs.Add( true );
        }

        /// <summary>
        /// 打开一个Excel文件
        /// </summary>
        /// <param name="FileName"></param>
        public static void Open( string fileName )//
        {
            app = new Microsoft.Office.Interop.Excel.Application();
            wbs = app.Workbooks;
            wb = wbs.Add( fileName );
            ws = wb.Worksheets[ 1 ] as Worksheet;
            mFilename = fileName;
        }

        /// <summary>
        /// 导入指定excel文件中的数据到数据表
        /// </summary>
        /// <param name="fileName">要读取的文件名</param>
        /// <param name="sheetIndex">要读取的sheet</param>
        /// <returns>数据表</returns>
        public static System.Data.DataTable ReadExelToTable( string fileName, int sheetIndex )
        {
            System.Data.DataTable dt = null;
            string sheetName = "sheet1$";
            string strCon = "Provider = Microsoft.ACE.OLEDB.12.0;" +
                               "Extended Properties=Excel 8.0;" +
                               "data source=" + fileName;
            
            OleDbConnection myConn = new OleDbConnection( strCon );
            myConn.Open();

            System.Data.DataTable dtSheetName = myConn.GetOleDbSchemaTable( OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" } );

            int iCount = dtSheetName.Rows.Count;
            if( sheetIndex >= iCount )
                sheetName = dtSheetName.Rows[ iCount - 1 ][ 2 ].ToString();
            else
                sheetName = dtSheetName.Rows[ sheetIndex ][ 2 ].ToString();

            string strCom = " SELECT * FROM [" + sheetName + "]";

            OleDbDataAdapter myCommand = new OleDbDataAdapter( strCom, myConn );
            dt = new System.Data.DataTable();
            myCommand.Fill( dt );
            myConn.Close();

            return dt;

        }

        /// <summary>
        /// 导入指定csv文件中的数据到数据表
        /// </summary>
        /// <param name="fileName">要读取的文件名</param>
        /// <param name="IsFirst">标示是否是读取的第一行，默认为true</param>
        /// <returns>数据表</returns>
        public static System.Data.DataTable ReadCsvToTable( string fileName, bool IsFirst = true )
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            //读取文件
            FileStream fs = new FileStream( fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read );
            StreamReader sr = new StreamReader( fs, Encoding.Default );
            
            //记录每次读取的一行记录
            string strLine = "";
           
            //记录每行记录中的各字段内容
            string[] aryLine = null;

            //记录表头
            string[] tableHead = null;
           
            //标示列数
            int columnCount = 0;
            
            //逐行读取CSV中的数据
            while( ( strLine = sr.ReadLine() ) != null )
            {
                if( IsFirst == true )
                {
                    tableHead = strLine.Split( ',' );
                    IsFirst = false;
                    columnCount = tableHead.Length;
                    //创建列
                    for( int i = 0; i < columnCount; i++ )
                    {
                        DataColumn dc = new DataColumn( tableHead[ i ] );
                        dt.Columns.Add( dc );
                    }
                }
                else
                {
                    aryLine = strLine.Split( ',' );
                    DataRow dr = dt.NewRow();
                    for( int j = 0; j < columnCount; j++ )
                    {
                        dr[ j ] = aryLine[ j ];
                    }
                    dt.Rows.Add( dr );
                }
            }
            if( aryLine != null && aryLine.Length > 0 )
            {
                dt.DefaultView.Sort = tableHead[ 0 ] + " " + "asc";
            }

            sr.Close();
            fs.Close();

            return dt;
        }

    }
}
