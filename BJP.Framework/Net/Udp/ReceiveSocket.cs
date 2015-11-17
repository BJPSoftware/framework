using System;
using System.Net;
using System.Net.Sockets;

namespace BJP.Framework.Net.Udp
{
    public class ReceiveSocket
    {
        /// <summary>
        /// 接收Socket
        /// </summary>
        private Socket receiveSocket;

        /// <summary>
        /// 用来接收数据的SocketAsyncEventArgs
        /// </summary>
        private SocketAsyncEventArgs receiveSocketAsyncEventArgs;
        
        /// <summary>
        /// 本地IP终结点
        /// </summary>
        private IPEndPoint localEndPoint;

        /// <summary>
        /// 接收缓存
        /// </summary>
        private byte[] receivebuffer;

        /// <summary>
        /// 接发到数据时触发
        /// </summary>
        public event EventHandler<SocketAsyncEventArgs> OnDataReceived;
        
        /// <summary>
        /// 带参数构造函数
        /// </summary>
        /// <param name="port"></param>
        public ReceiveSocket( Int32 port )
        {
            receiveSocket = new Socket( AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp );
            localEndPoint = new IPEndPoint( IPAddress.Loopback, port );
            receiveSocket.Bind( localEndPoint );

            receivebuffer = new Byte[ BufferManager.BUFFER_SIZE ];
            receiveSocketAsyncEventArgs = new SocketAsyncEventArgs();
            receiveSocketAsyncEventArgs.RemoteEndPoint = localEndPoint;
            receiveSocketAsyncEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>( receiveSocketAsyncEventArgs_Completed );
            receiveSocketAsyncEventArgs.SetBuffer( receivebuffer, 0, receivebuffer.Length );
        }
        /// <summary>
        /// 异步接收完触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void receiveSocketAsyncEventArgs_Completed( object sender, SocketAsyncEventArgs e )
        {
            switch( e.LastOperation )
            {
                case SocketAsyncOperation.ReceiveFrom:
                    ProcessReceived( e );
                    break;
                default:
                    throw new ArgumentException( "The last operation completed on the socket was not a receive" );
            }
        }
        /// <summary>
        ///  开始接收数据
        /// </summary>
        public void StartReceive()
        {
            if( !receiveSocket.ReceiveFromAsync( receiveSocketAsyncEventArgs ) )
            {
                ProcessReceived( receiveSocketAsyncEventArgs );
            }
        }

        /// <summary>
        /// 开始发送数据
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="szData"></param>
        /// <param name="realLength"></param>
        public void SentData( IPEndPoint endpoint, Byte[] szData, Int32 realLength )
        {
            if( endpoint != null )
            {
                receiveSocket.SendTo( szData, realLength, SocketFlags.None, endpoint );
            }
        }


        /// <summary>
        /// 处理接收到的数据
        /// </summary>
        /// <param name="e"></param>
        void ProcessReceived( SocketAsyncEventArgs e )
        {
            if( e.BytesTransferred > 0 && e.SocketError == SocketError.Success )
            {
                if( OnDataReceived != null )
                {
                    OnDataReceived( receiveSocketAsyncEventArgs, e );
                }
            }
            StartReceive();
        }
    }
}
