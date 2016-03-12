using System;
using System.Globalization;
using System.Text;

namespace BJP.Framework.Utility
{
    public static class ConvertHelper
    {
        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ByteToHexStr( byte[] bytes )
        {
            StringBuilder returnStr = new StringBuilder();

            if( bytes != null )
            {
                for( int i = 0; i < bytes.Length; i++ )
                {
                    returnStr.Append( bytes[ i ].ToString( "X2" ) );

                    //不是最后一个字节，则末尾加空格
                    if( i < bytes.Length - 1 )
                        returnStr.Append( " " );
                }
            }
            return returnStr.ToString();
        }

        /// <summary> 
        /// 字符串转16进制字节数组 
        /// </summary> 
        /// <param name="hexString"></param> 
        /// <returns></returns> 
        private static byte[] strToToHexByte( string hexString )
        {
            hexString = hexString.Replace( " ", "" );
            if( ( hexString.Length % 2 ) != 0 )
                hexString += " ";
            byte[] returnBytes = new byte[ hexString.Length / 2 ];
            for( int i = 0; i < returnBytes.Length; i++ )
                returnBytes[ i ] = Convert.ToByte( hexString.Substring( i * 2, 2 ), 16 );
            return returnBytes;
        }
        
        /// <summary> 
        /// 从汉字转换到16进制 
        /// </summary> 
        /// <param name="s"></param> 
        /// <param name="charset">编码,如"utf-8","gb2312"</param> 
        /// <param name="fenge">是否每字符用逗号分隔</param> 
        /// <returns></returns> 
        public static string ToHex( string s, string charset, bool fenge )
        {
            if( ( s.Length % 2 ) != 0 )
            {
                s += " ";//空格 
                         //throw new ArgumentException("s is not valid chinese string!"); 
            }
            System.Text.Encoding chs = System.Text.Encoding.GetEncoding( charset );
            byte[] bytes = chs.GetBytes( s );
            string str = "";
            for( int i = 0; i < bytes.Length; i++ )
            {
                str += string.Format( "{0:X}", bytes[ i ] );
                if( fenge && ( i != bytes.Length - 1 ) )
                {
                    str += string.Format( "{0}", "," );
                }
            }
            return str.ToLower();
        }

        ///<summary> 
        /// 从16进制转换成汉字 
        /// </summary> 
        /// <param name="hex"></param> 
        /// <param name="charset">编码,如"utf-8","gb2312"</param> 
        /// <returns></returns> 
        public static string UnHex( string hex, string charset )
        {
            if( hex == null )
                throw new ArgumentNullException( "hex" );
            hex = hex.Replace( ",", "" );
            hex = hex.Replace( "\n", "" );
            hex = hex.Replace( "\\", "" );
            hex = hex.Replace( " ", "" );
            if( hex.Length % 2 != 0 )
            {
                hex += "20";//空格 
            }
            // 需要将 hex 转换成 byte 数组。 
            byte[] bytes = new byte[ hex.Length / 2 ];
            for( int i = 0; i < bytes.Length; i++ )
            {
                try
                {
                    // 每两个字符是一个 byte。 
                    bytes[ i ] = byte.Parse( hex.Substring( i * 2, 2 ),
                    System.Globalization.NumberStyles.HexNumber );
                }
                catch
                {
                    // Rethrow an exception with custom message. 
                    throw new ArgumentException( "hex is not a valid hex number!", "hex" );
                }
            }
            System.Text.Encoding chs = System.Text.Encoding.GetEncoding( charset );
            return chs.GetString( bytes );
        }

        /// <summary>
        /// BCD码转换成字节数组
        /// </summary>
        /// <param name="strTemp"></param>
        /// <returns></returns>
        public static Byte[] ConvertToBCD( string strTemp )
        {
            try
            {
                if( Convert.ToBoolean( strTemp.Length & 1 ) )//数字的二进制码最后1位是1则为奇数
                {
                    strTemp = "0" + strTemp;//数位为奇数时前面补0
                }
                Byte[] aryTemp = new Byte[ strTemp.Length / 2 ];
                for( int i = 0; i < ( strTemp.Length / 2 ); i++ )
                {
                    aryTemp[ i ] = ( Byte ) ( ( ( strTemp[ i * 2 ] - '0' ) << 4 ) | ( strTemp[ i * 2 + 1 ] - '0' ) );
                }
                return aryTemp;//高位在前
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// BCD码转换16进制(压缩BCD)
        /// </summary>
        /// <param name="strTemp"></param>
        /// <returns></returns>
        public static Byte[] ConvertToBCD( string strTemp, int IntLen )
        {
            try
            {
                Byte[] Temp = ConvertToBCD( strTemp.Trim() );
                Byte[] return_Byte = new Byte[ IntLen ];
                if( IntLen != 0 )
                {
                    if( Temp.Length < IntLen )
                    {
                        for( int i = 0; i < IntLen - Temp.Length; i++ )
                        {
                            return_Byte[ i ] = 0x00;
                        }
                    }
                    Array.Copy( Temp, 0, return_Byte, IntLen - Temp.Length, Temp.Length );
                    return return_Byte;
                }
                else
                {
                    return Temp;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// BCD码(字节数组)转换成字符串
        /// </summary>
        /// <param name="bcd">BCD码的字节数组</param>
        /// <returns></returns>
        public static string BcdCodeToString( byte[] bcd )
        {
            StringBuilder sb = new StringBuilder();
            for( int i = 0; i < bcd.Length; i++ )
            {
                byte bb = bcd[ i ];
                sb.Append( ( byte ) ( bb >> 4 ) );
                sb.Append( ( byte ) ( bb & 0x0f ) );
            }
            return sb.ToString();
        }

        /// <summary>
        /// BCD码(字节)转换成字符串
        /// </summary>
        /// <param name="bcd"></param>
        /// <returns></returns>
        public static string BcdCodeToString( byte bcd )
        {
            byte[] bcdd = new byte[ 1 ] { bcd };
            return BcdCodeToString( bcdd );
        }

        public static string DateStringFormat( string datestring, string formatestring )
        {
            DateTime dtime = DateTime.ParseExact( datestring, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture );
            
            return   dtime.ToString( formatestring, DateTimeFormatInfo.InvariantInfo );
        }

        /// <summary>
        /// 将指定字符串的首字母转成大写，其它的小写
        /// </summary>
        /// <param name="convertString">要转换的字符串</param>
        /// <returns></returns>
        public static string ConvertFirstUpper(string convertString)
        {
            string temp = convertString.ToLower();    //先全部转成小写

            string after = temp.Substring(0, 1).ToUpper();
            after = after + temp.Substring(1, temp.Length-1);

            return after;
        }

        /// <summary>
        /// 将指定的字符串去掉指定的分隔符后合并成一个串
        /// 如果传入的字符串分隔后只有一个，则全部转成小写
        /// 如果有多个，则其它的首字母大写，其它的小写
        /// </summary>
        /// <param name="splitString">要处理字符串</param>
        /// <param name="splitChar">分隔符</param>
        /// <returns></returns>
        public static string SplitAndToFirstUpper(string splitString, char splitChar)
        {
            string[] tempSplit = splitString.Split(splitChar);

            string temp = tempSplit[0].ToLower();    //首单词全部小写
            for (int i = 1; i <= tempSplit.Length-1; i++)
            {
                if (tempSplit[i] != splitChar.ToString())
                temp += ConvertFirstUpper(tempSplit[i]);
            }

            return temp;
        }

        /// <summary>
        /// 将转入的数据的类型转换成系统类型
        /// </summary>
        /// <param name="sqlType">数据库对应的字段类型</param>
        /// <param name="language">要转换的语言(C#、Java)</param>
        /// <returns></returns>
        public static string SqlserverTypeToSys(string sqlType, codeLanguage codeLanguage)
        {
            string sysType = "string";
            if (codeLanguage == codeLanguage.Java)
                sysType = "String";
            switch (sqlType.ToLower())
            {
                case "bigint":
                    sysType = "Long";
                    break;
                case "smallint":
                    sysType = "Short";
                    break;
                case "int":
                    sysType = "Integer";
                    break;
                case "uniqueidentifier":
                    sysType = "String";
                    break;
                case "smalldatetime":
                    sysType = "Date";
                    break;
                case "datetime":
                    sysType = "Date";
                    break;
                case "date":
                    sysType = "Date";
                    break;
                case "float":
                    sysType = "Double";
                    break;
                case "real":
                case "numeric":
                    sysType = "BigDecimal";
                    break;
                case "smallmoney":
                    sysType = "BigDecimal";
                    break;
                case "decimal":
                    sysType = "BigDecimal";
                    break;
                case "money":
                    sysType = "BigDecimal";
                    break;
                case "tinyint":
                    sysType = "Integer";
                    break;
                case "bit":
                    sysType = "Boolean";
                    break;
                case "image":
                    sysType = "byte[]";
                    break;
                case "binary":
                    sysType = "byte[]";
                    break;
                case "varbinary":
                    sysType = "byte[]";
                    break;
                case "timestamp":
                    sysType = "byte[]";
                    break;
            }
            return sysType;
        }
    }
}
