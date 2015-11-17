using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace BJP.Framework.Net.Udp
{
    /// <summary>
    /// 收发缓冲区的分配管理
    /// </summary>
    class BufferManager
    {
        /// <summary>
        /// 分配的缓冲区大小
        /// </summary>
        public const int BUFFER_SIZE = 1024;
        
        /// <summary>
        /// buffer pool可以分配总字节数
        /// </summary> 
        private Int32 numBytes;

        /// <summary>
        /// BufferManager维护的总的字节数
        /// </summary>
        private Byte[] buffer;

        /// <summary>
        /// 可使用的Stack索引
        /// </summary>
        private Stack<Int32> freeIndexPool;

        /// <summary>
        /// 当前索引
        /// </summary>
        private Int32 currentIndex;

        /// <summary>
        /// 数据包缓冲区大小
        /// </summary>
        private Int32 unitBufferSize;

        public BufferManager( Int32 totalBytes, Int32 bufferSize )
        {
            numBytes = totalBytes;
            currentIndex = 0;
            unitBufferSize = bufferSize;
            freeIndexPool = new Stack<Int32>();
        }

        /// <summary>
        /// 初始化缓冲区
        /// </summary>
        public void InitBuffer()
        {
            buffer = new Byte[ numBytes ];
        }

        /// <summary>
        /// 从开辟的内存中分配一块指定大小的内存供套接字使用
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool SetBuffer( SocketAsyncEventArgs args )
        {
            if( freeIndexPool.Count > 0 )
            {
                args.SetBuffer( buffer, freeIndexPool.Pop(), unitBufferSize );
            }
            else
            {
                if( ( numBytes - unitBufferSize ) < currentIndex )
                {
                    return false;
                }
                args.SetBuffer( buffer, currentIndex, unitBufferSize );
                ( ( AsyncUserToken ) args.UserToken ).CurrentIndex = currentIndex;
                currentIndex += unitBufferSize;
            }
            return true;
        }
        /// <summary>
        /// 设置SocketAsyncEventArgs
        /// </summary>
        /// <param name="args"></param>
        /// <param name="value"></param>
        public void SetBufferValue( SocketAsyncEventArgs args, Byte[] value )
        {
            Int32 offerSize = ( ( AsyncUserToken ) args.UserToken ).CurrentIndex;

            for( Int32 i = offerSize; i < unitBufferSize + offerSize; i++ )
            {
                if( i >= value.Length + offerSize )
                {
                    break;
                }
                buffer[ i ] = value[ i - offerSize ];
            }
        }
    }
}
