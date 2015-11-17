using System;
using System.Net.Sockets;

namespace BJP.Framework.Net.Udp
{
    /// <summary>
    /// 异步的用户令牌
    /// </summary>
    public class AsyncUserToken
    {
        /// <summary>
        /// 当前索引
        /// </summary>
        Int32 _currentIndex;
        public Int32 CurrentIndex
        {
            get
            {
                return _currentIndex;
            }
            set
            {
                _currentIndex = value;
            }
        }

        /// <summary>
        /// 当前socket
        /// </summary>
        Socket _socket;
        public Socket CurrentSocket
        {
            get
            {
                return _socket;
            }
            set
            {
                _socket = value;
            }
        }
    }
}
