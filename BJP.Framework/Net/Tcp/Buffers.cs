﻿using System;
using System.Text;
using System.Threading;
using System.IO;

namespace BJP.Framework.Net.Tcp
{
    public static class Buffers
    {
         /// <summary>
        /// 将1个2维数据包整合成以个一维数据包
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static public Byte[] MergeBytes( params Byte[][] args )
        {
            Int32 length = 0;
            foreach( byte[] tempbyte in args )
            {
                length += tempbyte.Length;  //计算数据包总长度
            }

            Byte[] bytes = new Byte[ length ]; //建立新的数据包

            Int32 tempLength = 0;

            foreach( byte[] tempByte in args )
            {
                tempByte.CopyTo( bytes, tempLength );
                tempLength += tempByte.Length;  //复制数据包到新数据包
            }

            return bytes;

        }

        /// <summary>
        /// 将一个32位整形转换成一个BYTE[]4字节
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static public Byte[] GetSocketBytes( Int32 data )
        {
            return BitConverter.GetBytes( data );
        }

        /// <summary>
        /// 将一个64位整型转换成以个BYTE[] 8字节
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static public Byte[] GetSocketBytes( UInt64 data )
        {
            return BitConverter.GetBytes( data );
        }

        /// <summary>
        /// 将一个 1位CHAR转换成1位的BYTE
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static public Byte[] GetSocketBytes( Char data )
        {
            Byte[] bytes = new Byte[] { ( Byte ) data };
            return bytes;
        }

        /// <summary>
        /// 将一个BYTE[]数据包添加首位长度
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static public Byte[] GetSocketBytes( Byte[] data )
        {
            return MergeBytes(
                GetSocketBytes( data.Length ),
                data
                );
        }

        /// <summary>
        /// 将一个字符串转换成BYTE[]，BYTE[]的首位是字符串的长度
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static public Byte[] GetSocketBytes( String data )
        {
            Byte[] bytes = Encoding.Unicode.GetBytes( data );

            return MergeBytes(
                GetSocketBytes( bytes.Length ),
                bytes
                );
        }

        /// <summary>
        /// 将一个DATATIME转换成为BYTE[]数组
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static public Byte[] GetSocketBytes( DateTime data )
        {
            return GetSocketBytes( data.ToString() );
        }
    }

    public class ReadBytes
    {

        private int Current;

        private byte[] Data;

        public int Length
        {
            get; set;
        }


        public void Reset()
        {
            Current = 0;
        }

        public ReadBytes( Byte[] data )
        {
            Data = data;
            this.Length = Data.Length;
            Current = 0;
        }

        /// <summary>
        /// 读取内存流中的头4位并转换成整型
        /// </summary>
        /// <param name="ms">内存流</param>
        /// <returns></returns>
        public bool ReadInt32( out int values )
        {
            try
            {
                values = BitConverter.ToInt32( Data, Current );
                Current = Interlocked.Add( ref Current, 4 );
                return true;
            }
            catch
            {
                values = 0;
                return false;
            }
        }

        /// <summary>
        /// 读取内存流中的首位
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public bool ReadByte( out byte values )
        {
            try
            {
                values = ( byte ) Data[ Current ];
                Current = Interlocked.Increment( ref Current );
                return true;
            }
            catch
            {
                values = 0;
                return false;
            }
        }


        /// <summary>
        /// 读取内存流中的头2位并转换成整型
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public bool ReadInt16( out short values )
        {

            try
            {
                values = BitConverter.ToInt16( Data, Current );
                Current = Interlocked.Add( ref Current, 2 );
                return true;
            }
            catch
            {
                values = 0;
                return false;
            }
        }


        /// <summary>
        /// 读取内存流中一段字符串
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public bool ReadString( out string values )
        {
            int lengt;
            try
            {
                if( ReadInt32( out lengt ) )
                {

                    Byte[] buf = new Byte[ lengt ];

                    Array.Copy( Data, Current, buf, 0, buf.Length );

                    values = Encoding.Unicode.GetString( buf, 0, buf.Length );

                    Current = Interlocked.Add( ref Current, lengt );

                    return true;

                }
                else
                {
                    values = "";
                    return false;
                }
            }
            catch
            {
                values = "";
                return false;
            }

        }


        /// <summary>
        /// 读取内存流中一段数据
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public bool ReadByteArray( out byte[] values )
        {
            int lengt;
            try
            {
                if( ReadInt32( out lengt ) )
                {
                    values = new Byte[ lengt ];
                    Array.Copy( Data, Current, values, 0, values.Length );
                    Current = Interlocked.Add( ref Current, lengt );
                    return true;

                }
                else
                {
                    values = null;
                    return false;
                }
            }
            catch
            {
                values = null;
                return false;
            }

        }


    }





    /// <summary>
    /// 内存流读取类
    /// </summary>
    public static class PacketReads
    {
        /// <summary>
        /// 读取内存流中的头4位并转换成整型
        /// </summary>
        /// <param name="ms">内存流</param>
        /// <returns></returns>
        public static Int32 ReadInt32( MemoryStream ms )
        {
            Byte[] buf = new Byte[ 4 ];
            ms.Read( buf, 0, 4 );
            return BitConverter.ToInt32( buf, 0 );
        }

        /// <summary>
        /// 读取内存流中的头2位并转换成整型
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static Int16 ReadInt16( MemoryStream ms )
        {
            Byte[] buf = new Byte[ 2 ];
            ms.Read( buf, 0, 2 );
            return BitConverter.ToInt16( buf, 0 );
        }

        /// <summary>
        /// 读取内存流中的首位
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static Char ReadChar( MemoryStream ms )
        {
            return ( Char ) ms.ReadByte();
        }

        /// <summary>
        /// 读取内存流中一段字符串
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static String ReadString( MemoryStream ms )
        {
            Byte[] buf = new Byte[ ReadInt32( ms ) ];
            ms.Read( buf, 0, buf.Length );
            return Encoding.Default.GetString( buf );
        }

        /// <summary>
        /// 读取内存流中一段数据
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static Byte[] ReadByte( MemoryStream ms )
        {
            Byte[] buf = new Byte[ ReadInt32( ms ) ];
            ms.Read( buf, 0, buf.Length );
            return buf;
        }

    }
}
