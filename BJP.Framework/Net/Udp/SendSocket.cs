using System;
using System.Net;
using System.Net.Sockets;
using BJP.Framework.Log;

namespace BJP.Framework.Net.Udp
{
    public class SendSocket
    {
        SocketAsyncEventArgsPool socketArgsPool;
        BufferManager bufferManager;
        static Int32 opsToPreAlloc = 2;
        Socket socket;
        SocketAsyncEventArgs socketArgs;
        Int32 totalClient;
        public event EventHandler<SocketAsyncEventArgs> OnDataSend;
        static readonly object asyncLock = new object();
                                                                       
        /// <summary>
        /// 带参数构造函数
        /// </summary>
        /// <param name="totalClient"></param>
        public SendSocket( Int32 totalClient )
        {
            socket = new Socket( AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp );
            this.totalClient = totalClient;
            Int32 bufferSize = BufferManager.BUFFER_SIZE;
            bufferManager = new BufferManager( bufferSize * totalClient * opsToPreAlloc, bufferSize );
            socketArgsPool = new SocketAsyncEventArgsPool( totalClient );
        }

        /// <summary>
        /// 生成一定量的SocketAsyncEventArgs对象，以供使用
        /// </summary>
        public void Init()
        {
            bufferManager.InitBuffer();
            for( Int32 i = 0; i < totalClient; i++ )
            {
                socketArgs = new SocketAsyncEventArgs();
                socketArgs.Completed += new EventHandler<SocketAsyncEventArgs>( socketArgs_Completed );
                socketArgs.UserToken = new AsyncUserToken();
                bufferManager.SetBuffer( socketArgs );
                socketArgsPool.Push( socketArgs );
            }
        }
        void socketArgs_Completed( object sender, SocketAsyncEventArgs e )
        {
            switch( e.LastOperation )
            {
                case SocketAsyncOperation.SendTo:
                    ProcessSend( e );
                    break;
                default:
                    throw new ArgumentException( "The last operation completed on the socket was not a send" );
            }
        }
        void ProcessSend( SocketAsyncEventArgs e )
        {
            if( e.BytesTransferred > 0 && e.SocketError == SocketError.Success )
            {
                if( OnDataSend != null )
                {
                    OnDataSend( socket, e );
                }
            }

            //重新放入对象池中
            socketArgsPool.Push( e );
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="remoteEndPoint"></param>
        /// <param name="szData"></param>
        public void SendData( EndPoint remoteEndPoint, Byte[] szData )
        {
            //弹出一个SocketAsyncEventArgs
            socketArgs = socketArgsPool.Pop();

            if( socketArgs == null )
            {
                LogHelper.Error( "SentData socketArgs==null,可能是缓存耗尽 " );
                return;
            }
            socketArgs.RemoteEndPoint = remoteEndPoint;
            bufferManager.SetBufferValue( socketArgs, szData );
            if( socketArgs.RemoteEndPoint != null )
            {
                if( !socket.SendToAsync( socketArgs ) )
                {
                    ProcessSend( socketArgs );
                }
            }
            else
            {
                if( socketArgs != null )
                {
                    socketArgsPool.Push( socketArgs );
                }
            }

        }
    }
}
