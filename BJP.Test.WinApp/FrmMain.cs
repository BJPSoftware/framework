using System;
using System.IO;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

using BJP.Framework.Log;
using BJP.Framework.Net.Udp;
using BJP.Framework.Net.Tcp;
using BJP.Framework.Utility;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;

namespace BJP.Test.WinApp
{
    public partial class FrmMain : Form
    {
        /// <summary>
        /// 定义显示字符串的委托
        /// </summary>
        /// <param name="s"></param>
        public delegate void ShowString( string s );

        public UdpServer _server;
        public TCPClientBase _tcpClient = null ;
        public FrmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 日志功能测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogTest_Click( object sender, EventArgs e )
        {
            //LogHelper.Info( "日志测试" );
            //LogHelper.Info( "分类日志测试", "test" );
            //LogHelper.Debug( "日志测试" );
            //LogHelper.Debug( "日志测试" );
            //LogHelper.Error( "日志测试" );

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("1", "a");
            dic.Add("10", "b");
            dic.Add("11", "b");
            dic.Add("12", "b");
            dic.Add("13", "e");
            dic.Add("14", "f");
            dic.Add("15", "g");
            dic.Add("16", "h");

            List<string> ss = new List<string>();
            ss.Add("5");
            ss.Add("7");
            ss.Add("16");

            for (int i = 0; i < ss.Count; i++)
            {
                if (!dic.ContainsKey(ss[i])) {
                    LogHelper.Info(ss[i] + "未找到");
                }
            }


        }

        /// <summary>
        /// 根据宜兴的线路上、下行文件，生成基础站点、线路站点数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDoFile_Click( object sender, EventArgs e )
        {
            string[] arrList;
            string sLine = null;
            string[] LineFiles = Directory.GetFiles( txtInPath.Text, "*.csv" );
            int iFileCount = LineFiles.Length;
            int iCount = 0;
            string sSQL = "";
            lblInfo.Text = "共处理文件 " + iCount.ToString() + " / " + iFileCount.ToString() + " 个";

            //string strCon = ConfigurationManager.ConnectionStrings[ "conSQL" ].ConnectionString;
            //SqlConnection conn = new SqlConnection( strCon );
            //conn.Open();

            foreach( string sFileName in LineFiles )
            {
                LogHelper.Info( string.Format( "处理文件：{0}", sFileName ) );
                StreamReader sr = new StreamReader( sFileName, Encoding.Default );
                int isMain = 0;
                string s = sFileName.Substring( sFileName.LastIndexOf( '\\' ) + 1 );
                string lfName = s.Split( '.' )[ 0 ];    //上下行线路名
                string sDirect = lfName.Substring( lfName.Length - 1 );    //线路的上下行
                string lineName = lfName.Substring( 0, lfName.Length - 1 );//正式的线路名
                //处理上、下行
                if( sDirect.ToLower() == "s" )
                    isMain = 1;
                else
                    isMain = 0;

                while( ( sLine = sr.ReadLine() ) != null )
                {
                    arrList = sLine.Split( ',' );    //列
                    if( arrList[ 0 ] != "站台编号" )
                    {//排除第一行
                        
                    }
            //        sSQL = "if not EXISTS (select slon,slat,sname from line_standinfo where slon='{0}' and slat='{1}' and sname={2})";
            //        sSQL += "    insert into line_standinfo(sguid,sname,slon,slat) values(newid(),'{3}','{4}','{5}')";
            //        DataHelper.WriteLog( string.Format( sSQL, arrList[ 3 ], arrList[ 4 ], arrList[ 2 ], arrList[ 2 ], arrList[ 3 ], arrList[ 4 ] ), _LogPath );
            //        SqlCommand command = new SqlCommand( string.Format( sSQL, arrList[ 2 ], arrList[ 3 ], arrList[ 4 ], arrList[ 2 ], arrList[ 3 ], arrList[ 4 ], sDirect ), conn );
            //        command.ExecuteNonQuery();

            //        slguid,sguid,liguid,isdelete slno
            //        sSQL = "insert into standdata(sname,slno,slon,slat,lname,ismain) values('{0}','{1}','{2}','{3}','{4}','{5}')";
            //        DataHelper.WriteLog( string.Format( sSQL, arrList[ 2 ], Convert.ToInt32( arrList[ 0 ] ) + 1, arrList[ 3 ], arrList[ 4 ], line, sDirect ), _LogPath + "linedata" );
            //        SqlCommand command = new SqlCommand( string.Format( sSQL, arrList[ 2 ], Convert.ToInt32( arrList[ 0 ] ) + 1, arrList[ 3 ], arrList[ 4 ], line, isMain ), conn );
            //        command.ExecuteNonQuery();
                }
            //    DataHelper.WriteLog( string.Format( "处理文件:'{0}' ok", sFileName ), _LogPath + "/dofile" );
            //    DataHelper.WriteLog( "                   ", _LogPath + "/dofile" );
                sLine = null;
                sr.Close();
                iCount++;
                lblInfo.Text = "共处理文件 " + iCount.ToString() + " / " + iFileCount.ToString() + " 个";
            }
        }

        /// <summary>
        /// 实现给文本框赋值
        /// </summary>
        /// <param name="s"></param>
        public void ShowText( string s )
        {
            textBox1.Text = s;
        }
        private void btnDirSelect_Click( object sender, EventArgs e )
        {
            if( dialogFolder.ShowDialog() == DialogResult.OK )
            {
                txtInPath.Text = dialogFolder.SelectedPath;
            }

            //if( openFileDialog1.ShowDialog() == DialogResult.OK )
            //{
            //    txtPath.Text = openFileDialog1.FileName;
            //}
            //txtPath.Text = Utility.CreateGuidLeft(36,"1",'0',true);
            //textBox1.Text = Utility.CreateGuidRight( 36, "1", '0', true );

        }

        private void btnUdpServerTest_Click( object sender, EventArgs e )
        {
            _server = new UdpServer( 5555, 4445, 10 );
            _server.OnReceiveData += new EventHandler<SocketAsyncEventArgs>( testServer_OnReceiveData );
            //_server.OnSendData += new EventHandler<SocketAsyncEventArgs>( testServer_OnSendData );

            _server.Start();
        }
        /// <summary>
        /// 接收到数据事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void testServer_OnReceiveData( object sender, SocketAsyncEventArgs e )
        {
            int iRead = e.BytesTransferred;
            byte[] data = new byte[ iRead ];
            Buffer.BlockCopy( e.Buffer, 0, data, 0, iRead );
            //string tt = ConvertHelper.ByteToHexStr( data );
            string tt = Encoding.UTF8.GetString( data );
            this.Invoke( new ShowString( ShowText ),tt );

            //接收到数据后发出回应
            _server._SendSocket.SendData( ( IPEndPoint ) e.RemoteEndPoint, data );
            LogHelper.Info( tt );
        }

        private void btnTcpClient_Click( object sender, EventArgs e )
        {
            byte[] sendData = Encoding.UTF8.GetBytes( textBox1.Text );
            if( _tcpClient == null )
                _tcpClient = new TCPClientBase();
            _tcpClient.SocketStart( @"127.0.0.1", 9999 );

            if( _tcpClient.isConnection )
                textBox1.Text = "已连接上服务器";
        }

        private void btn_TcpSend_Click( object sender, EventArgs e )
        {
            //byte[] sendData = Encoding.UTF8.GetBytes( textBox1.Text );

            //if (_tcpClient.isConnection)
            //    _tcpClient.SendData( sendData );

            StreamWriter sw = new StreamWriter( @"f:\临时文件\ttt.csv", true, System.Text.Encoding.Default );
            for( int i = 1; i < 1000; i++ )
            {
                sw.WriteLine( Utility.CreateGuidLeft( 36, i.ToString() + "STANDDATA", '0', true ) );
            }
            sw.Close();
        }

        private void FrmMain_FormClosed( object sender, FormClosedEventArgs e )
        {
            //if( _tcpClient.isConnection )
            //    _tcpClient.Close();
        }

        private void doText_Click( object sender, EventArgs e )
        {

            
            string[] LineFiles;
            string sLine = null;
            string sFileName = txtFileName.Text;
            string dir = txtInPath.Text;
            string OutName = txtOutDir.Text + "out.csv";
            int i = 1;


            StreamReader sr = new StreamReader( sFileName, Encoding.Default );
            StreamWriter sw = new StreamWriter( OutName, true, System.Text.Encoding.Default );
            //LineFiles = Directory.GetFiles( dir, "*.*" );

            //读每条记录,一条记录表示一条线路
            while( ( sLine = sr.ReadLine() ) != null )
            {
                string[] arrList = sLine.Split( ',' );    //列

                //上行数据处理
                string [] arrS = arrList[ 1 ].Split( '、' );   //上行站点清单
                int last = arrS.Length - 1;  //上行的最后一个站点
                //拼成线路名: 线路名(起站名--终站名)S
                string lineS = arrList[ 0 ] + "路(" + arrS[ 0 ] + "--" + arrS[ last ] + ")S";
                sw.WriteLine( lineS );

                //下行数据处理
                string[] arrX = arrList[ 2 ].Split( '、' );   //上行站点清单
                last = arrX.Length - 1;  //上行的最后一个站点
                //拼成线路名: 线路名(起站名--终站名)X
                string lineX = arrList[ 0 ] + "路(" + arrX[ 0 ] + "--" + arrX[ last ] + ")X";
                sw.WriteLine( lineX );

                //string pathS = dir + arrList[ 0 ] + "_上行.txt";  //
                //string pathX = 
                //i = 1;
                //arrList = sLine.Split( ',' );    //列
                ////path =  dir + arrList[ 0 ] + "_上行.txt";
                //s = arrList[1].Split( '、' );
                //int sss = s.Length-1;
                //path = dir + arrList[ 0 ] + "(" + s[ 0 ] + "--" + s[ sss ] + ").csv";

                //swTxt.WriteLine( arrList[ 0 ] + "路,0," + "(" + s[ 0 ] + "--" + s[ sss ] + ")" );

                //StreamWriter sw = new StreamWriter( path, true, System.Text.Encoding.Default );
                //sw.WriteLine( "序号,站名 ,经度,纬度" );

                //foreach( string ss in s )
                //{
                //    string tt = i.ToString() + "," + ss;
                //    sw.WriteLine( tt );
                //    i++;
                //}
                //sw.Close();
                //i = 1;

                //s = arrList[ 2 ].Split( '、' );
                //int sx = s.Length - 1;
                //path = dir + arrList[ 0 ] + "(" + s[0]+"--" + s[ sx ] + ").csv";
                //swTxt.WriteLine( arrList[ 0 ] + "路,1," + "(" + s[ 0 ] + "--" + s[ sx ] + ")" );
                //StreamWriter swX = new StreamWriter( path, true, System.Text.Encoding.Default );
                //swX.WriteLine( "序号,站名 ,经度,纬度" );
                ////( 东城同沙--创业路)
                //foreach( string ss in s )
                //{
                //    string tt = i.ToString() + "," + ss;
                //    swX.WriteLine( tt );
                //    i++;
                //}
                //swX.Close();
            }
            //swTxt.Close();
            sw.Close();
            MessageBox.Show( "数据处理完毕" );
        }

        private void doSQL_Click( object sender, EventArgs e )
        {
            //string strCon = ConfigurationManager.ConnectionStrings[ "conSQL" ].ConnectionString;
            //string strCon = "Data Source=(local);UID=sa;pwd=sql2008;DATABASE=dbDgInport";
            //string dir1 = textBox2.Text + @"txt\";
            //string dir2 = textBox2.Text + @"csv\";

            //if( false == Directory.Exists( dir1 ) )
            //    Directory.CreateDirectory( dir1 );
            //if( false == Directory.Exists( dir2 ) )
            //    Directory.CreateDirectory( dir2 );

            //SqlConnection conn = new SqlConnection( strCon );
            //conn.Open();

            //string sql = "SELECT id,long_name FROM dbo.dw_route WHERE parent_id>0";
            //SqlCommand command = new SqlCommand( sql, conn );
            //SqlDataAdapter Datapter = new SqlDataAdapter( command );
            //System.Data.DataTable dt = new System.Data.DataTable();

            //Datapter.Fill( dt );
            //string lname = null;
            //int lid = 0;


            //string tt = null;
            //foreach( DataRow dr in dt.Rows )
            //{
            //    lid = Convert.ToInt32( dr["id"]);
            //    lname = dr[ "long_name" ].ToString();
            //    //string pathtxt = dir1 + lname + ".txt";
            //    string pathcsv = dir2 + lname + ".csv";

            //    sql = "select sequence,short_name,longitude,latitude,field_vchar_3 FROM dbo.dw_stop WHERE route_id={0}";
            //    SqlCommand cmdStop = new SqlCommand(string.Format( sql,lid), conn );
            //    SqlDataAdapter dpStop = new SqlDataAdapter( cmdStop );
            //    System.Data.DataTable dtStop = new System.Data.DataTable();
            //    dpStop.Fill( dtStop );

            //    //StreamWriter swtxt = new StreamWriter( pathtxt, true, System.Text.Encoding.Default );
            //    StreamWriter swtcsv = new StreamWriter( pathcsv, true, System.Text.Encoding.Default );

            //    swtcsv.WriteLine("sguid,slno,sname,slon,slat,liguid");
            //    foreach( DataRow drStop in dtStop.Rows)
            //    {
            //        tt = drStop[ "sequence" ].ToString() + "," + drStop[ "short_name" ].ToString();
            //        string scsv= drStop[ "field_vchar_3" ].ToString() + "," + drStop[ "sequence" ].ToString() + "," + drStop[ "short_name" ].ToString() + "," + drStop[ "longitude" ].ToString() + "," + drStop[ "latitude" ].ToString();
            //        //swtxt.WriteLine( tt );
            //        swtcsv.WriteLine( scsv );
            //    }
            //    //swtxt.Close();
            //    swtcsv.Close();
            //}
            //MessageBox.Show("数据处理完毕");
        }

        private void doExcelFile_Click( object sender, EventArgs e )
        {
            //string dir = textBox2.Text;
            //string[] LineFiles = Directory.GetFiles( dir, "*.xls" );
            //string lfName = string.Empty;
            //try
            //{
            //    foreach( string sFileName in LineFiles )
            //    {
            //        string s = sFileName.Substring( sFileName.LastIndexOf( '\\' ) + 1 );
            //        lfName = s.Split( '.' )[ 0 ];    //线路名称


            //        ExcelToDb( sFileName, "上行站点", lfName + "_S" );
            //        ExcelToDb( sFileName, "下行站点", lfName + "_X" );
            //    }

            //    MessageBox.Show( "线路处理完毕" );
            //}
            //catch (Exception ex )
            //{
            //    MessageBox.Show( "线路处理异常:" + lfName + "," + ex.ToString() );
            //}


        }

        public void ExcelToDb( string filename, string sheetname,string lname )
        {
            //string sqlCon = "Data Source=(local);UID=sa;pwd=sql2008;DATABASE=dbDgInport";
            //SqlConnection conn = new SqlConnection( sqlCon );
            //conn.Open();

            //SqlCommand cmdDel = conn.CreateCommand();
            //cmdDel.CommandText = "DELETE FROM linestand WHERE lname='" + lname + "'";
            //int del = cmdDel.ExecuteNonQuery();

            //System.Data.DataTable dt;
            //string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;" +
            //                                "Extended Properties=Excel 8.0;" +
            //                                "data source=" + filename;
            //OleDbConnection myConn = new OleDbConnection( strCon );
            //string strCom = " SELECT * FROM [" + sheetname  + "$]";
            //myConn.Open();
            //OleDbDataAdapter myCommand = new OleDbDataAdapter( strCom, myConn );
            //dt = new System.Data.DataTable();
            //myCommand.Fill( dt );
            //myConn.Close();
            //int i = 1;

            //foreach( DataRow drStop in dt.Rows )
            //{
            //    string sname = drStop[ 0 ].ToString().Split( '#' )[ 2 ];
            //    string slon = drStop[ 3 ].ToString().Substring( 1 );
            //    string slat = drStop[ 2 ].ToString().Substring( 1 );
            //    string slguid = Utility.CreateGuidLeft( 36, i.ToString() + "STANDDATA", '0', true );

            //    string sqlStop = "INSERT INTO linestand(slno,sname,slon,slat,lname,slguid) VALUES({0},'{1}',{2},{3},'{4}','{5}')";
            //    SqlCommand cmdStop = conn.CreateCommand();
            //    cmdStop.CommandText = string.Format(sqlStop,i,sname,Convert.ToDecimal(slon),Convert.ToDecimal(slat), lname, slguid );
            //    int ret = cmdStop.ExecuteNonQuery();
            //    i++;

            //}
        }

        private void btnDoData_Click( object sender, EventArgs e )
        {
            //找出线路站点表中存在，但数据库中不存在的站点
            FindNotEixtsStand();
        }

        /// <summary>
        /// 
        /// </summary>
        private void FindNotEixtsStand()
        {
            ////线路站点文件目录
            //string dir = txtInPath.Text;
            
            ////里面的文件清单
            //string[] LineFiles = Directory.GetFiles( txtInPath.Text, "*.*" );

            ////总的站点数
            //Int32 iCount = 1;

            //string sqlCon = "Data Source=(local);UID=sa;pwd=sql2008;DATABASE=dbDgInport";
            //SqlConnection conn = new SqlConnection( sqlCon );
            //conn.Open();

            //string saveName = @"f:\临时文件\无坐标站点.csv";
            //StreamWriter sw = new StreamWriter( saveName, true, System.Text.Encoding.Default );

            //foreach( string fileName in LineFiles )
            //{
            //    //不带后缀的文件名
            //    string lastFileName = Utility.GetFileName( fileName, false );

            //    //打开excel文件,放到数据表中
            //    System.Data.DataTable dt = ExcelHelper.ReadExelToTable(fileName,0);

            //    //处理Excel数据
            //    int slno = 1;   //站点序号
            //    StringBuilder sbSQL = new StringBuilder();
            //    foreach( DataRow drStop in dt.Rows )
            //    {
            //        string sname = drStop[ 1 ].ToString();    //站名
            //        string slon = drStop[ 2 ].ToString();     //经度
            //        if( slon == string.Empty )
            //            slon = "0";
            //        string slat = drStop[ 3 ].ToString();     //纬度
            //        if( slat == string.Empty )
            //            slat = "0";
            //        string ismain = drStop[ 4 ].ToString();  //上下行标识

            //        string slguid = Utility.CreateGuidLeft( 36, iCount.ToString() + "STANDDATA", '0', true );  //slguid

            //        string sqlStop = "INSERT INTO linestand(slno,sname,slon,slat,slguid,lname,ismain) VALUES({0},'{1}',{2},{3},'{4}','{5}','{6}')";
            //        SqlCommand cmdStop = conn.CreateCommand();
            //        cmdStop.CommandText = string.Format( sqlStop, slno, sname, Convert.ToDecimal( slon ), Convert.ToDecimal( slat ),  slguid , lastFileName ,ismain);
            //        int ret = cmdStop.ExecuteNonQuery();

            //        slno++;
            //        iCount++;
            //    }
            //}
            //sw.Close();
            //MessageBox.Show( "处理完毕" );
        }

        private void btnSelectFile_Click( object sender, EventArgs e )
        {
            if( DialogResult.OK == openFileDialog1.ShowDialog() )
            {
                txtFileName.Text = openFileDialog1.FileName;
            }
        }

        private void btnOutDir_Click( object sender, EventArgs e )
        {
            if( DialogResult.OK == dialogFolder.ShowDialog() )
                txtOutDir.Text = dialogFolder.SelectedPath;
        }
    }
}
