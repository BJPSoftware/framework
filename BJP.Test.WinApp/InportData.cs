using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

using BJP.Framework.Repository;
using BJP.Framework.Utility;

namespace BJP.Test.WinApp
{
    public static class InportData
    {
        public static void InportLineInfo( DataTable dt,string savename )
        {
            int iCount = 1;
            string lbguid = string.Empty;   //line_basicinfo表主键
            string liguidS = string.Empty;   //line_lineinfo表主键(上行线路)
            string liguidX = string.Empty;   //line_lineinfo表主键(下行线路)
            string lname = string.Empty;
            string compguid = string.Empty;
            string deptid = string.Empty;
            string SqlLineBasicInfo = string.Empty;
            string SqlLineS = string.Empty;
            string SqlLineX = string.Empty;
            string SQL = string.Empty;

            StringBuilder sb = new StringBuilder();
            string CreateDate = DateTime.Now.ToString( "yyyy-MM-dd" );
            string VerName = DateTime.Now.ToString("yyyyMMdd");

            foreach( DataRow dr in dt.Rows )
            {
                lbguid = Utility.CreateGuidLeft( 36, iCount.ToString() + "LINEBASIC", '0', true );
                liguidS = Utility.CreateGuidLeft( 36, iCount.ToString() + "LINEINFO0", '0', true );   //line_lineinfo表主键(上行线路)
                liguidX = Utility.CreateGuidLeft( 36, iCount.ToString() + "LINEINFO1", '0', true );   //line_lineinfo表主键(下行线路)
                lname = dr[0].ToString();
                compguid = dr[ 1 ].ToString();
                deptid = dr[ 2 ].ToString();

                //无向线路
                SqlLineBasicInfo = "INSERT INTO line_linebasicinfo (LBGUID,LNAME,LOLDNAME,LCOMPGUID,LDEPID,LTICKETRULE,CREATEUSER,CREATEDATE,";
                SqlLineBasicInfo += " UPDATEUSER,UPDATEDATE,GUID,ISDELETED,ISSIP,LSN,LABBR)";
                SqlLineBasicInfo += " VALUES('{0}','{1}','{2}','{3}','{4}','0','SYSTEM','{5}','SYSTEM','{6}','{7}','0','1','{8}','{9}');";
                SqlLineBasicInfo += Environment.NewLine;
                SqlLineBasicInfo = string.Format( SqlLineBasicInfo ,lbguid, lname , lname , compguid , deptid, CreateDate , CreateDate , lbguid, lname, lname );

                //上行方向
                SqlLineS = "INSERT INTO line_lineinfo( LIGUID ,LGUID ,LBGUID ,LFSTDFTIME ,LFSTDETIME ,LMSTDPEAK ,LMENDPEAK ,LESTDPEAK ,LEENDPEAK,LINTERVALH ,LINTERVALN ,";
                SqlLineS += " LINTERVALL ,VERNAME ,ISACTIVED ,GUID ,ISDELETED ,ISROUTE ,ISSEND ,ISMAIN ,ISSHUTTLE,CREATEUSER,CREATEDATE,UPDATEUSER,UPDATEDATE) VALUES ";
                SqlLineS += " ('{0}','{1}','{2}','0530','2130','0700','0830','1800','1930','3','5','8','{3}','1','{4}','0','0','0','0','0','SYSTEM','{5}','SYSTEM','{6}');";
                SqlLineS += Environment.NewLine;
                SqlLineS = string.Format( SqlLineS, liguidS, liguidS, lbguid, VerName, liguidS, CreateDate, CreateDate );

                //下行方向
                SqlLineX = "INSERT INTO line_lineinfo( LIGUID ,LGUID ,LBGUID ,LFSTDFTIME ,LFSTDETIME ,LMSTDPEAK ,LMENDPEAK ,LESTDPEAK ,LEENDPEAK,LINTERVALH ,LINTERVALN ,";
                SqlLineX += " LINTERVALL ,VERNAME ,ISACTIVED ,GUID ,ISDELETED ,ISROUTE ,ISSEND ,ISMAIN ,ISSHUTTLE,CREATEUSER,CREATEDATE,UPDATEUSER,UPDATEDATE) VALUES ";
                SqlLineX += " ('{0}','{1}','{2}','0530','2130','0700','0830','1800','1930','3','5','8','{3}','1','{4}','0','0','0','1','0','SYSTEM','{5}','SYSTEM','{6}');";
                SqlLineX += Environment.NewLine;
                SqlLineX = string.Format( SqlLineX, liguidX, liguidX, lbguid, VerName, liguidX, CreateDate, CreateDate );

                sb.Append( SqlLineBasicInfo );
                sb.Append( SqlLineS );
                sb.Append( SqlLineX );

                iCount++;
            }

            //新增线路更新到数据库
            UpateToDB( sb.ToString(), savename );

        }

        public static void InportBusInfo( DataTable dt, string savename )
        {
            int iCount = 1;
            string bguid = string.Empty;    //表的主键
            string compguid = string.Empty; //公司
            string deptid = string.Empty;   //部门(车队)
            string BusCard = string.Empty;    //车牌号
            string BusNo = string.Empty;    //自编号
            string SqlBus = string.Empty;
            string SqlDev = string.Empty;
            string EquId = string.Empty;

            //获取设备表中的最大SIM卡号
            string SQL = "SELECT MAX(EQUIPMENTNO) FROM dev_equ_store";
            DbHelper dbHelper = new DbHelper();
            DataTable dtDev = dbHelper.Fill( SQL );
            Int32 Equipmentno = Convert.ToInt32(dtDev.Rows[ 0 ][0]);
            StringBuilder sb = new StringBuilder();
            foreach( DataRow dr in dt.Rows )
            {
                //SELECT dbuscard,compguid,deptid,busno
                bguid = Utility.CreateGuidLeft( 36, iCount.ToString() + "BUSINFO", '0', true );
                BusCard = dr[ 0 ].ToString();
                compguid = dr[ 1 ].ToString();
                deptid = dr[ 2 ].ToString();
                BusNo = dr[ 3 ].ToString();

                //增加新的车辆
                SqlBus = "INSERT INTO LINE_BUSINFO(bguid,guid,busno,compguid,dbuscard,isdeleted,issend,istowlevel,safekeeping,BUSSTATUS,TGUID) ";
                SqlBus += "VALUES('{0}','{1}','{2}','{3}','{4}','0','0','0','0','3','{5}');" + Environment.NewLine;
                SqlBus = string.Format( SqlBus, bguid, bguid, BusNo, compguid, BusCard, deptid );
                sb.Append( SqlBus );

                //增加车辆后要同步增加至设备表中
                Equipmentno++;
                EquId = Utility.CreateGuidLeft( 36, iCount.ToString() + "DEVSTORE", '0', true );
                SqlDev = "INSERT INTO DEV_EQU_STORE(dev_sN,equipmentno,equtype,tocar,guid,EQUIPMENTGUID) VALUES";
                SqlDev += "values ('{0}','{1}','dace7142-58e2-4313-b5b6-fd617bcf83da','{2}','{3}','{4}')";
                SqlDev = string.Format( SqlDev, "GPS" + Equipmentno.ToString(), Equipmentno.ToString(), bguid, EquId, EquId );
                sb.Append( SqlDev );

                iCount++;
            }
            //新增加车辆更新到数据库
            UpateToDB( sb.ToString(), savename );
        }

        public static void InportStandInfo( DataTable dt, string savename )
        {
            int iCount = 1;
            string Sguid = string.Empty;
            string SName = string.Empty;
            string SqlStand = string.Empty;
            Double Slat = 0;
            Double Slon = 0;

            StringBuilder sb = new StringBuilder();
            foreach( DataRow dr in dt.Rows )
            {
                Sguid = Utility.CreateGuidLeft( 36, iCount.ToString() + "STANDINFO", '0', true );
                SName = dr[ 0 ].ToString();
                Slon = Convert.ToDouble( dr[ 1 ] );
                Slat = Convert.ToDouble( dr[ 2 ] );

                SqlStand = "INSERT INTO line_standinfo(sguid,sname,slat,slon,guid) VALUES('{0}','{1}',{2},{3},'{4}');" + Environment.NewLine;
                SqlStand = string.Format( SqlStand ,Sguid,SName,Slat,Slon,Sguid);

                sb.Append( SqlStand );

                iCount++;
            }

            //更新到数据库
            UpateToDB( sb.ToString(), savename );
        }

        public static void InportStandData( DataTable dt, string savename )
        {
            string slguid = string.Empty;
            string sguid = string.Empty;
            string liguid = string.Empty;
            string slno = string.Empty;
            string SqlStand = string.Empty;

            //slguid,sguid,liguid, slno

            StringBuilder sb = new StringBuilder();
            foreach( DataRow dr in dt.Rows )
            {
                slguid = dr[ 0 ].ToString();
                sguid = dr[ 1 ].ToString();
                liguid = dr[ 2 ].ToString();
                slno = dr[ 3 ].ToString();

                SqlStand = "INSERT INTO line_standdata( slguid, sguid, liguid, guid, isdeleted, slno ) VALUES('{0}','{1}','{2}','{3}','0','{4}');" + Environment.NewLine;
                SqlStand = string.Format( SqlStand, slguid, sguid, liguid, slguid, slno );

                sb.Append( SqlStand );
            }

            //更新到数据库
            UpateToDB( sb.ToString(), savename );
        }

        public static void UpateToDB( string sql, string savename = "" )
        {
            //执行指定的SQL语句更新数据
            DbHelper dbHelper = new DbHelper();
            if( sql != "" )
                dbHelper.Add( sql );

            //保存入库的SQL语句备查
            if( savename != string.Empty )
            {
                if( File.Exists( savename ) )
                    File.Delete( savename );

                StreamWriter sw = new StreamWriter( savename, true, System.Text.Encoding.Default );
                sw.WriteLine( "begin" );
                sw.Write( sql );
                sw.WriteLine( "end;" );
                sw.Close();
            }
        }

    }
}
