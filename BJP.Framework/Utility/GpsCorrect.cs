using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJP.Framework.Utility
{
    public static class GpsCorrect
    {
        const double pi = 3.14159265358979324;
        const double a = 6378245.0;
        const double ee = 0.00669342162296594323;

        /// <summary>
        /// 纠偏函数
        /// </summary>
        /// <param name="wgLat">纬度</param>
        /// <param name="wgLon">经度</param>
        /// <param name="latlng">结果数组</param>
        public static void TransToMap( double wgLat, double wgLon, double[] latlng )
        {
            if( OutOfChina( wgLat, wgLon ) )
            {
                latlng[ 0 ] = wgLat;
                latlng[ 1 ] = wgLon;
                return;
            }
            double dLat = TransformLat( wgLon - 105.0, wgLat - 35.0 );
            double dLon = TransformLon( wgLon - 105.0, wgLat - 35.0 );
            double radLat = wgLat / 180.0 * pi;
            double magic = Math.Sin( radLat );
            magic = 1 - ee * magic * magic;
            double sqrtMagic = Math.Sqrt( magic );
            dLat = ( dLat * 180.0 ) / ( ( a * ( 1 - ee ) ) / ( magic * sqrtMagic ) * pi );
            dLon = ( dLon * 180.0 ) / ( a / sqrtMagic * Math.Cos( radLat ) * pi );
            latlng[ 0 ] = wgLat + dLat;
            latlng[ 1 ] = wgLon + dLon;
        }

        /// <summary>
        /// 根据地图坐标反查GPS坐标
        /// </summary>
        /// <param name="wgLat">纬度</param>
        /// <param name="wgLon">经度</param>
        /// <param name="latlng">结果数组</param>
        public static void TransToGps( double wgLat, double wgLon, double[] latlng )
        {
            if( OutOfChina( wgLat, wgLon ) )
            {
                latlng[ 0 ] = wgLat;
                latlng[ 1 ] = wgLon;
                return;
            }
            double dLat = TransformLat( wgLon - 105.0, wgLat - 35.0 );
            double dLon = TransformLon( wgLon - 105.0, wgLat - 35.0 );
            double radLat = wgLat / 180.0 * pi;
            double magic = Math.Sin( radLat );
            magic = 1 - ee * magic * magic;
            double sqrtMagic = Math.Sqrt( magic );
            dLat = ( dLat * 180.0 ) / ( ( a * ( 1 - ee ) ) / ( magic * sqrtMagic ) * pi );
            dLon = ( dLon * 180.0 ) / ( a / sqrtMagic * Math.Cos( radLat ) * pi );
            latlng[ 0 ] = wgLat - dLat;
            latlng[ 1 ] = wgLon - dLon;
        }

        /// <summary>
        /// 是否国外
        /// </summary>
        /// <param name="lat">纬度</param>
        /// <param name="lon">经度</param>
        /// <returns></returns>
        private static bool OutOfChina( double lat, double lon )
        {
            if( lon < 72.004 || lon > 137.8347 )
                return true;
            if( lat < 0.8293 || lat > 55.8271 )
                return true;
            return false;
        }

        /// <summary>
        /// 纬度转换
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static double TransformLat( double x, double y )
        {
            double ret = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y + 0.2 * Math.Sqrt( Math.Abs( x ) );
            ret += ( 20.0 * Math.Sin( 6.0 * x * pi ) + 20.0 * Math.Sin( 2.0 * x * pi ) ) * 2.0 / 3.0;
            ret += ( 20.0 * Math.Sin( y * pi ) + 40.0 * Math.Sin( y / 3.0 * pi ) ) * 2.0 / 3.0;
            ret += ( 160.0 * Math.Sin( y / 12.0 * pi ) + 320 * Math.Sin( y * pi / 30.0 ) ) * 2.0 / 3.0;
            return ret;
        }
        
        /// <summary>
        /// 经度转换
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static double TransformLon( double x, double y )
        {
            double ret = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1 * Math.Sqrt( Math.Abs( x ) );
            ret += ( 20.0 * Math.Sin( 6.0 * x * pi ) + 20.0 * Math.Sin( 2.0 * x * pi ) ) * 2.0 / 3.0;
            ret += ( 20.0 * Math.Sin( x * pi ) + 40.0 * Math.Sin( x / 3.0 * pi ) ) * 2.0 / 3.0;
            ret += ( 150.0 * Math.Sin( x / 12.0 * pi ) + 300.0 * Math.Sin( x / 30.0 * pi ) ) * 2.0 / 3.0;
            return ret;
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
