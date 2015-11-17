using System;
using System.Globalization;
using System.IO;
using System.Data;
using System.Text;
using System.Windows.Forms;

using BJP.Framework.Utility;
using BJP.Framework.Repository;

namespace BJP.Test.WinApp
{
    public partial class FrmInportDg : Form
    {
        public FrmInportDg()
        {
            InitializeComponent();
        }

        private void btnSelInDir_Click( object sender, EventArgs e )
        {
            if( DialogResult.OK == folderBrowserDialog1.ShowDialog() )
                txtInDir.Text = folderBrowserDialog1.SelectedPath;
        }

        private void btnSelInFile_Click( object sender, EventArgs e )
        {
            if( DialogResult.OK == openFileDialog1.ShowDialog() )
                txtInFile.Text = openFileDialog1.FileName;
        }

        /// <summary>
        /// 导入线路的运营时间,文件格式为excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInTime_Click( object sender, EventArgs e )
        {
            string FileDir = txtInDir.Text;    //待处理线路文件所在目录
            string [] arrFiles = Directory.GetFiles( FileDir);
            string LineName = string.Empty;    //线路名称
            string IsMain = string.Empty;   //上下行
            int TotalStand = 1;

            //循环处理里面的每个文件
            foreach( string doFileName in arrFiles )
            {
                //得到线路名称及上下行方向
                GetLineNameAndLdirection( doFileName, out LineName, out IsMain );
                
                //打开excel文件,放到数据表中
                System.Data.DataTable dt = ExcelHelper.ReadExelToTable( doFileName, 0 );

                //打开csv文件，放到数据表中
                //System.Data.DataTable dt = ExcelHelper.ReadCsvToTable(doFileName);

                //导入数据到linestand表
                TotalStand = DoLineStandDataRow( dt, LineName, IsMain, TotalStand );
            }

            //更新在line_standinfo表中同站名、经度、纬度的站点sguid至linestand表中
            //string SQL = "UPDATE linestand SET sguid=(SELECT sguid FROM line_standinfo WHERE sname=linestand.sname and slon=linestand.slon and slat=linestand.slat)";

            //将linestand表中存在，line_standinfo表中不存在的站名、经度、纬度插入line_standinfo表中
            //同时更新这些站点在linestand表中的sguid

            //根据线路名、上下行更新 linestand表中的liguid
        }

        public void GetLineNameAndLdirection( string filename, out string linename, out string ismain )
        {
            string temp = Utility.GetFileName( filename, false );
            linename = temp.Substring( 0, temp.Length - 1 );
            if( "S" == temp.Substring( temp.Length - 1, 1 ) )
                ismain = "0";
            else
                ismain = "1";
        }
        public int  DoLineStandDataRow( System.Data.DataTable dt, string linename,string ismain,int totalstand )
        {
            int Slno = 1;   //站序
            string SName = string.Empty;  //站名
            string Slon = string.Empty;   //经度
            string Slat = string.Empty;   //纬度
            string SQL = string.Empty;

            string saveName = @"f:\临时文件\linestand.txt";
            StreamWriter sw = new StreamWriter( saveName, true, System.Text.Encoding.Default );
            StringBuilder sbStop = new StringBuilder();
            foreach( DataRow dr in dt.Rows )
            {
                string slguid = Utility.CreateGuidLeft( 36, totalstand.ToString() + "STANDDATA", '0', true );
                Slno = Convert.ToInt16(dr[ 0 ].ToString());    //站序
                SName = dr[ 1 ].ToString();    //站名
                Slon = dr[ 2 ].ToString();   //经度
                Slat = dr[ 3 ].ToString();   //纬度

                SQL = "INSERT INTO linestand(slno,sname,slon,slat,lname,slguid,ismain) VALUES({0},'{1}',{2},{3},'{4}','{5}','{6}')";
                SQL = string.Format( SQL, Slno, SName, Convert.ToDecimal( Slon ), Convert.ToDecimal( Slat ), linename, slguid, ismain );
                sw.WriteLine(SQL);
                sbStop.Append( SQL );
                totalstand++;
            }
            sw.Close();

            //入库
            DbHelper dbHelper = new DbHelper();
            int DoCount = dbHelper.Add( sbStop.ToString() );

            return totalstand;
        }
        
        private void button1_Click( object sender, EventArgs e )
        {
            textBox1.Text = Utility.TransferLonLat( Convert.ToDouble( textBox3.Text ) ).ToString();
            textBox2.Text = Utility.TransferLonLat( Convert.ToDouble( textBox4.Text ) ).ToString();
        }

        //导入线路车辆信息
        private void btnCar_Click( object sender, EventArgs e )
        {
            string FileDir = txtInDir.Text;    //待处理车辆文件所在目录
            string[] arrFiles = Directory.GetFiles( FileDir );  //目录内的所有车辆文件
            string LineName = string.Empty;    //线路名称
            string BusCard = string.Empty;    //车牌号
            string BusNo = string.Empty;    //自编号
            string CompanyName = string.Empty;    //所属公司
            string DeptName = string.Empty;    //所属部门(车队)
            string LineBusGuid = string.Empty; //线路车辆表主键
            string SQL = string.Empty;   //入库SQL语句
            int Total = 1;   //所有记录总数
            int DoCount = 0;

            StringBuilder sbCar = new StringBuilder();

            //循环处理目录里面的每个文件
            //foreach( string doFileName in arrFiles )
            //{
            //    //打开excel文件,放到数据表中
            //    DataTable dt = ExcelHelper.ReadExelToTable( doFileName, 0 );

            //    //循环处理每条车辆记录
            //    foreach( DataRow dr in dt.Rows )
            //    {
            //        LineBusGuid = Utility.CreateGuidLeft( 36, Total.ToString() + "LINEBUS", '0', true );
            //        LineName = dr[ 0 ].ToString();    //线路名称
            //        BusCard = dr[ 2 ].ToString();    //车牌号
            //        BusNo = dr[ 1 ].ToString();    //自编号
            //        CompanyName = dr[ 3 ].ToString();    //所属公司
            //        DeptName = dr[ 4 ].ToString();    //所属部门(车队)

            //        SQL = "INSERT INTO linecar(lname,dbuscard,compname,orgname,busno,linebusguid) VALUES('{0}','{1}','{2}','{3}','{4}','{5}')";
            //        SQL = string.Format( SQL, LineName, BusCard, CompanyName, DeptName, BusNo, LineBusGuid );

            //        sbCar.Append( SQL );

            //        Total++;
            //    }
            //}

            //数据处理入库
            DbHelper dbHelper = new DbHelper();
            //DoCount = dbHelper.Add( sbCar.ToString() );

            //更新系统中存在的公司、部门、线路、车辆的对应字段
            string UpdateExistsField = "update linecar set lbguid=(select lbguid from line_linebasicinfo where lname=linecar.lname), ";
            UpdateExistsField += "bguid=(select bguid from line_businfo where dbuscard=linecar.dbuscard), ";
            UpdateExistsField += "COMPGUID=(SELECT COMPGUID FROM dbo.Line_CompanyInfo WHERE COMPNAME=dbo.linecar.compname), ";
            UpdateExistsField += "deptid=(SELECT deptid FROM sys_org WHERE ORGNAME=dbo.linecar.orgname AND COMPGUID=(SELECT COMPGUID FROM dbo.Line_CompanyInfo WHERE COMPNAME=dbo.linecar.compname)) ";

            //DoCount = dbHelper.Add( UpdateExistsField );

            ////将系统不存在的线路加入到系统中
            //string SaveLineName = @"f:\临时文件\lineinfo.txt";
            //SQL = "SELECT Lname,compguid,deptid FROM linecar WHERE lbguid IS NULL GROUP BY Lname,compguid,deptid ";
            //DataTable dtLineBasic = dbHelper.Fill( SQL );
            //if( dtLineBasic.Rows.Count > 0 )
            //{
            //    InportData.InportLineInfo( dtLineBasic, SaveLineName );
            //}

            //将系统中不存在的车辆加入到系统中
            //string SaveCarName = @"f:\临时文件\CarInfo.txt";
            //SQL = "SELECT dbuscard,compguid,deptid,busno FROM linecar WHERE bguid IS NULL ";
            //DataTable dtBusInfo = dbHelper.Fill( SQL );
            //if( dtBusInfo.Rows.Count > 0 )
            //{
            //    InportData.InportBusInfo( dtBusInfo, SaveCarName );
            //}

            //再次更新公司、部门、线路、车辆的对应字段
            DoCount = dbHelper.Add( UpdateExistsField );

            //最后更新到定车定线表中


        }

        private void btnJP_Click( object sender, EventArgs e )
        {
            double[] latlng = new double[2] ;
            string FileDir = txtInDir.Text;    //待处理经纬度文件所在目录
            string[] arrFiles = Directory.GetFiles( FileDir );  //目录内的所有经纬度文件
            string SQL;

            StringBuilder sbCar = new StringBuilder();
            foreach( string doFileName in arrFiles )
            {
                //打开excel文件,放到数据表中
                DataTable dt = ExcelHelper.ReadExelToTable( doFileName, 0 );

                //循环处理每条坐标 记录
                foreach( DataRow dr in dt.Rows )
                {
                    string sguid = dr[ 0 ].ToString();    //坐标标识
                    string slat = dr[ 1 ].ToString();    //纬度
                    string slon = dr[ 2 ].ToString();    //经度

                    GpsCorrect.TransToGps( Convert.ToDouble( slat ), Convert.ToDouble( slon ), latlng );

                    SQL = "UPDATE line_standinfo set slat={0},slon={1} where sguid='{2}';" + Environment.NewLine;
                    SQL = string.Format( SQL, latlng[ 0 ], latlng[ 1 ], sguid );

                    sbCar.Append( SQL );
                }
            }

            string savename = @"f:\临时文件\MapToGps.txt";
            if( File.Exists( savename ) )
                File.Delete( savename );
            StreamWriter sw = new StreamWriter( savename, true, System.Text.Encoding.Default );
            sw.Write( sbCar.ToString() );
            sw.Close();
        }
 
        private void btnBatchJp_Click( object sender, EventArgs e )
        {
            double[] latlng = new double[ 2 ];
            StringBuilder sbCar = new StringBuilder();

            //批量将高德地图坐标转为GPS坐标
            string sql = "SELECT id,latitude,longitude FROM dw_stop";
            DbHelper dbHelper = new DbHelper();
            DataTable dt =  dbHelper.Fill( sql );

            //循环处理每条坐标 记录
            foreach( DataRow dr in dt.Rows )
            {
                string sguid = dr[ 0 ].ToString();    //坐标标识
                string slat = dr[ 1 ].ToString();    //纬度
                string slon = dr[ 2 ].ToString();    //经度

                GpsCorrect.TransToGps( Convert.ToDouble( slat ), Convert.ToDouble( slon ), latlng );

                sql = "UPDATE dw_stop set latitude={0},longitude={1} where id='{2}';" + Environment.NewLine;
                sql = string.Format( sql, latlng[ 0 ], latlng[ 1 ], sguid );

                sbCar.Append( sql );
            }

            dbHelper.Add( sbCar.ToString() );

            MessageBox.Show("数据处理完成");

        }
        private void btnInGd_Click( object sender, EventArgs e )
        {
            string Lname = textBox5.Text;
            Int32 totalstand = 8000;

            //同步line_standinfo表中的sguid
            string UpdateSguid = "UPDATE linestand SET sguid=(SELECT sguid FROM dbo.line_standinfo WHERE SNAME=linestand.sname AND SLON=linestand.slon AND SLAT=linestand.slat AND slon>0)";
            UpdateSguid +=" WHERE linestand.slon > 0 AND linestand.sguid IS NULL;" + Environment.NewLine;

            //从高德数据中拿线路站点
            string sql = "	SELECT a.sequence AS slno,a.short_name AS sname,a.longitude AS slon, a.latitude AS slat, ";
            sql += "b.short_name AS lname,b.direction - 1 AS ismain  FROM dbo.dw_stop a  ";
            sql += "LEFT JOIN dbo.dw_route b ON b.id = a.route_id ";
            sql += "WHERE b.parent_id > 0 AND b.short_name LIKE '" + Lname + "%' ";
            sql += "ORDER BY b.short_name";

            string SQL = string.Empty;
            DbHelper dbHelper = new DbHelper();
            dbHelper.Add("DELETE FROM linestand;");
            DataTable dt = dbHelper.Fill( sql );
            StringBuilder sbStop = new StringBuilder();

            foreach( DataRow dr in dt.Rows )
            {
                string slguid = Utility.CreateGuidLeft( 36, totalstand.ToString() + "STANDDATA", '0', true );
                string Slno = dr[ 0 ].ToString() ;    //站序
                string SName = dr[ 1 ].ToString();    //站名
                string Slon = dr[ 2 ].ToString();   //经度
                string Slat = dr[ 3 ].ToString();   //纬度
                string linename = dr[ 4 ].ToString();   //线路名称
                string ismain = dr[ 5 ].ToString();   //上下行

                SQL = "INSERT INTO linestand(slno,sname,slon,slat,lname,slguid,ismain) VALUES({0},'{1}',{2},{3},'{4}','{5}','{6}')";
                SQL = string.Format( SQL, Slno, SName, Convert.ToDecimal( Slon ), Convert.ToDecimal( Slat ), linename, slguid, ismain );
                //sw.WriteLine( SQL );
                sbStop.Append( SQL );
                totalstand++;
            }

            //插入高德数据
            dbHelper.Add( sbStop.ToString() );

            //同步sguid
            dbHelper.Add( UpdateSguid );

            //插入line_standinfo表中不存在的站点
            sql = "SELECT sname,slon,slat FROM linestand WHERE sguid IS null GROUP BY sname,slon,slat;";
            DataTable dtStand = dbHelper.Fill( sql );
            InportData.InportStandInfo( dtStand, @"f:\临时文件\standinfo.sql" );

            //再次同步sguid并同步liguid
            string UpdateLiguid = "UPDATE linestand SET liguid=(SELECT liguid FROM line_lineinfo WHERE ismain=linestand.ismain AND LBGUID=(";
            UpdateLiguid += " SELECT LBGUID FROM line_linebasicinfo WHERE lname = linestand.lname AND ISDELETED = '0')); ";

            dbHelper.Add( UpdateSguid + UpdateLiguid );

            //准备插入line_standdata
            string InsertData = "select slguid,sguid,liguid,slno from dbo.linestand WHERE liguid IS NOT NULL ORDER by lname,ismain,slno;";
            DataTable dtStandData = dbHelper.Fill( InsertData );
            InportData.InportStandData( dtStandData, @"f:\临时文件\standdata.sql" );

            MessageBox.Show("数据已导入");
        }

        private void btnShuiX_Click( object sender, EventArgs e )
        {
            string FileDir = txtInDir.Text;    //待处理车辆文件所在目录
            string[] arrFiles = Directory.GetFiles( FileDir,"*.txt" );  //目录内的所有车辆文件

            //将文本文件处理成csv文件
            //循环处理目录里面的每个文件
            //foreach( string doFileName in arrFiles )
            //{
            //    //打开excel文件,放到数据表中
            //    //DataTable dt = ExcelHelper.ReadExelToTable( doFileName, 0 );
            //    FileStream fs = new FileStream( doFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read );
            //    string FileName = Utility.GetFileName( doFileName );
            //    StreamReader sr = new StreamReader( fs, Encoding.Default );
            //    string strLine = "";
            //    StringBuilder sb = new StringBuilder();
            //    int Kong = 1;
            //    //循环处理每条记录
            //    while( ( strLine = sr.ReadLine() ) != null )
            //    {
            //        int iLen = strLine.Length;
            //        if( iLen > 0 )
            //        {
            //            for( int i = 0; i < iLen; i++ )
            //            {
            //                string s = strLine.Substring( i, 1 );
            //                if( s == " " )
            //                {
            //                    if( Kong == 1 )
            //                        sb.Append( "," );
            //                    Kong++;
            //                }
            //                else
            //                {
            //                    sb.Append( s );
            //                    Kong = 1;
            //                }
            //            }
            //            sb.Append( Environment.NewLine );
            //        }
            //    }
            //    StreamWriter sw = new StreamWriter( @"f:\临时文件\输入目录\" + FileName + ".csv", true, System.Text.Encoding.Default );
            //    sw.Write( sb.ToString() );
            //    sw.Close();
            //}
            //MessageBox.Show( "文本文件处理完毕" );
            //处理csv文件

            arrFiles = Directory.GetFiles( FileDir, "*.csv" );  //目录内的所有车辆文件
            int totalstand = 1;
            int Slno = 1;   //站序
            int Slno_X = 1;
            int Slno_S = 1;
            string IsMain = "0";
            string SName = string.Empty;  //站名
            string Slon = string.Empty;   //经度
            string Slat = string.Empty;   //纬度
            string SQL = string.Empty;
            string LineName = string.Empty;
            StringBuilder sbStop = new StringBuilder();
            foreach( string doFileName in arrFiles )
            {
                DataTable dt = ExcelHelper.ReadCsvToTable( doFileName );
                //LineName = Utility.GetFileName( doFileName ) + "路";
                Slno = 1;
                Slno_S = 1;
                Slno_X = 1;
                //处理每条记录
                foreach( DataRow dr in dt.Rows )
                {
                    string slguid = Utility.CreateGuidLeft( 36, totalstand.ToString() + "STANDDATA", '0', true );
                    Slno = Convert.ToInt16( dr[ 0 ].ToString());
                    Slon = dr[ 2 ].ToString();   //经度
                    Slat = dr[ 3 ].ToString();   //纬度
                    SName = dr[ 1 ].ToString();  //站名
                    LineName = dr[ 4 ].ToString();  //线路名
                    IsMain = dr[ 5 ].ToString();  //上下行
                    //if( tempName[ 0 ] == "上行" )
                    //{
                    //    IsMain = "0";
                    //    Slno = Slno_S;
                    //    Slno_S ++;
                    //}
                        
                    //else
                    //{
                    //    IsMain = "1";
                    //    Slno = Slno_X;
                    //    Slno_X++;
                    //} 


                    SQL = "INSERT INTO linestand(slno,sname,slon,slat,lname,slguid,ismain) VALUES({0},'{1}',{2},{3},'{4}','{5}','{6}')" + Environment.NewLine;
                    SQL = string.Format( SQL, Slno, SName, Convert.ToDecimal( Slon ), Convert.ToDecimal( Slat ), LineName, slguid, IsMain );
                    Slno++;
                    sbStop.Append( SQL );
                    totalstand++;
                }
            }
            StreamWriter swStop = new StreamWriter( @"f:\临时文件\linstand.sql", true, System.Text.Encoding.Default );
            swStop.Write( sbStop.ToString() );
            swStop.Close();

            MessageBox.Show( "CSV文件处理完毕" );

            //InportData.InportLineInfo()

        }

        private void button2_Click( object sender, EventArgs e )
        {
            string FileDir = txtInDir.Text;    //待处理车辆文件所在目录
            string[] arrFiles = Directory.GetFiles( FileDir, "*.xls" );  //目录内的所有车辆文件
            StringBuilder sbLine = new StringBuilder();
            string LineDir = string.Empty;
            string LineS = string.Empty;
            string LineX = string.Empty;

            foreach( string doFileName in arrFiles )
            {
                DataTable dt = ExcelHelper.ReadExelToTable( doFileName,0 );
                string LineName = string.Empty;
                int LineNumber = 0;
                foreach( DataRow dr in dt.Rows )
                {
                    //line.csv的数据
                    LineName = dr[ 0 ].ToString();
                    sbLine.Append( LineNumber.ToString() );
                    sbLine.Append( "," );
                    sbLine.Append( LineName );
                    sbLine.Append( "," );
                    sbLine.Append( "否" );
                    sbLine.Append( Environment.NewLine );

                    //创建线路目录
                    LineDir = @"F:\临时文件\采点线路\bus\" + LineName;
                    if( !Directory.Exists( LineDir ) )
                        Directory.CreateDirectory( LineDir );

                    //创建线路的上下行文件
                    LineS = LineDir + @"\" + LineName + "S.csv";
                    LineX = LineDir + @"\" + LineName + "X.csv";

                    //组装上下行文件内容
                    StringBuilder sbStand = new StringBuilder();
                    sbStand.Append( "站台编号,报站语音,站名,经度,纬度,角度,进站广告,出站广告,进站提示,出站提示,进站扩展,出站扩展,限速,站前里程,是否大站,外音开否" );
                    sbStand.Append( Environment.NewLine );
                    for( int i = 0; i < 100; i++ )
                    {
                        sbStand.Append( i.ToString() );
                        sbStand.Append( "," );
                        sbStand.Append( "站点" + i.ToString() + "," );
                        sbStand.Append( "站点" + i.ToString() + "," );
                        if (i==0)
                            sbStand.Append(",,,,,,,,,36,20,起点站,是" );
                        else
                           if (i==99)
                                sbStand.Append( ",,,,,,,,,36,20,终点站,是" );
                           else
                                sbStand.Append( ",,,,,,,,,36,20,小站,是" );

                        sbStand.Append( Environment.NewLine );
                    }
                    //生成上行文件
                    StreamWriter sw_L = new StreamWriter( LineS, true, System.Text.Encoding.Default );
                    sw_L.Write( sbStand.ToString() );
                    sw_L.Close();

                    //生成下行文件
                    StreamWriter sw_X = new StreamWriter( LineX, true, System.Text.Encoding.Default );
                    sw_X.Write( sbStand.ToString() );
                    sw_X.Close();
                    LineNumber++;

                }
            }

            StreamWriter swStop = new StreamWriter( @"F:\临时文件\采点线路\bus\line.csv", true, System.Text.Encoding.Default );
            swStop.WriteLine( "编号,线路名称,当前线路" );
            swStop.Write( sbLine.ToString() );
            swStop.Close();
        }
    }
}
