using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace BJP.Framework.Net.Udp
{
    public class SocketAsyncEventArgsPool
    {
        /// <summary>
        /// SocketAsyncEventArgs对象池
        /// </summary>
        private Stack<SocketAsyncEventArgs> asyncObjPool;

        /// <summary>
        /// 初始化SocketAsyncEventArgs对象池
        /// </summary>
        /// <param name="capacity"></param>
        public SocketAsyncEventArgsPool( Int32 capacity )
        {
            asyncObjPool = new Stack<SocketAsyncEventArgs>();
        }

        /// <summary>
        /// 将SocketAsyncEventArgs加入SocketAsyncEventArgs对象池
        /// </summary>
        /// <param name="item"></param>
        public void Push( SocketAsyncEventArgs item )
        {
            if( item == null )
            {
                throw new ArgumentNullException( "Items added to a SocketAsyncEventArgsPool can not be null" );
            }
            lock ( asyncObjPool )
            {
                asyncObjPool.Push( item );
            }
        }
        /// <summary>
        /// 从SocketAsyncEventArgs对象池弹出一个SocketAsyncEventArgs对象
        /// </summary>
        /// <returns></returns>
        public SocketAsyncEventArgs Pop()
        {
            lock ( asyncObjPool )
            {
                if( asyncObjPool.Count > 0 )
                {
                    return asyncObjPool.Pop();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 取得SocketAsyncEventArgs池的数量
        /// </summary>
        public Int32 Count
        {
            get
            {
                return asyncObjPool.Count;
            }
        }
    }
}
