using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJP.Framework.Utility
{
    public static class Utility
    {

        /// <summary>
        /// 根据基础的字符串，向左补充指定的字符到指定的长度
        /// 如果基础串达到或超过指定的长度，从左往右取指定长度 
        /// </summary>
        /// <param name="length">补充后的长度</param>
        /// <param name="baseid">基础字符串</param>
        /// <param name="addchar">不足位时补充的字符</param>
        /// <param name="timeflag">是否加时间戳</param>
        /// <returns></returns>
        public static string CreateGuidLeft( int length, string baseid, char addchar, bool timeflag=true )
        {
            string CreateID = string.Empty;
            string temp = string.Empty;
            string dateString = string.Empty;

            if( true == timeflag )
            {//要加时间戳
                dateString = DateTime.Now.ToString( "yyyyMMddHHmmssfff" );
                temp = baseid + dateString;
            }
            else
                temp = baseid;

            if( temp.Length >= length )
                CreateID = temp.Substring( 0, length );
            else
                CreateID = temp.PadLeft( length, addchar );

            return CreateID;
        }

        /// <summary>
        /// 根据基础的字符串，向左补充指定的字符到指定的长度
        /// 如果基础串达到或超过指定的长度，从左往右取指定长度 
        /// </summary>
        /// <param name="length">补充后的长度</param>
        /// <param name="baseid">基础字符串</param>
        /// <param name="addchar">不足位时补充的字符</param>
        /// <param name="timeflag">是否加时间戳</param>
        /// <returns></returns>
        public static string CreateGuidRight( int length, string baseid, char addchar, bool timeflag=true )
        {
            string CreateID = string.Empty;
            string temp = string.Empty;
            string dateString = string.Empty;

            if( true == timeflag )
            {//要加时间戳
                dateString = DateTime.Now.ToString( "yyyyMMddHHmmssfff" );
                temp = baseid + dateString;
            }
            else
                temp = baseid;

            if( temp.Length >= length )
                CreateID = temp.Substring( 0, length );
            else
                CreateID = temp.PadRight( length, addchar );

            return CreateID;
        }

        /// <summary>
        /// 获取指定路径的文件名
        /// </summary>
        /// <param name="fileName">文件全名，带有路径，如D:\aaa\bbb.txt样</param>
        /// <param name="attFlag">是否去掉后缀的标识，true:不去掉；false：去掉</param>
        /// <returns>去掉路径的文件名,如D:\aaa\bbb.txt返回bbb或bbb.txt</returns>
        public static string GetFileName( string fileName, bool attFlag=false )
        {
            string sReturn = string.Empty;
            string temp = string.Empty;

            //去掉目录后的文件名，带有后缀
            temp = fileName.Substring( fileName.LastIndexOf( '\\' ) + 1 );

            if( true == attFlag )
            {
                //不去掉后缀的文件名
                sReturn = temp;
            }
            else
            {
                //去掉后缀后的文件名
                sReturn = temp.Split( '.' )[ 0 ];    
            }

            return sReturn;
        }

        /// <summary>
        /// 重命名文件
        /// </summary>
        /// <param name="oldName">要重命名的文件名,如 D:\xx\xx\xx\XX.XX</param>
        /// <param name="newName">重命名后的文件名,无目录,如XXX</param>
        public static void ReNameFile( string oldName, string newName )
        {
            
            int lastpath = oldName.LastIndexOf( "\\" );    // 最后一个"\"
            int lastdot = oldName.LastIndexOf( "." );   // 最后一个"."
            int length = lastdot - lastpath - 1;  //  纯文件名字长度
            string beginpart = oldName.Substring( 0, lastpath + 1 );  //  文件目录字符串 xx\xx\xx\
            string namenoext = oldName.Substring( lastpath + 1, length );  //   纯文件名字
            string ext = oldName.Substring( lastdot );   //   扩展名
            string fullnewname = beginpart + newName + ext;    //组装成新文件名，带目录

            //重命名文件
            File.Move( oldName, fullnewname );
        }

        /// <summary>
        /// 将地图用的经纬度转成设备用度、分、秒形式
        /// </summary>
        /// <param name="val">要转换的经纬度的值</param>
        /// <returns></returns>
        public static double TransferLonLat( double val )
        {
            return Math.Round( Math.Truncate( val ) * 100 + ( val - Math.Truncate( val ) ) * 60, 4 );
        }

    }
}
