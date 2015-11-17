using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace BJP.Framework.Net.Tcp
{
    /// <summary>
    /// 数据包输入代理
    /// </summary>
    /// <param name="data">输入包</param>
    /// <param name="socketAsync"></param>
    public delegate void ClientBinaryInputHandler( byte[] data, IPEndPoint endPoint );

    /// <summary>
    /// 异常错误通常是用户断开的代理
    /// </summary>
    /// <param name="message">消息</param>
    /// <param name="socketAsync"></param>
    /// <param name="erorr">错误代码</param>
    public delegate void ClientMessageInputHandler( string message );
    /// <summary>
    /// Socket链接代理
    /// </summary>
    /// <param name="conn"></param>
    public delegate void ConnectionHandler( bool conn );
    public class SocketClient
    {
        private Socket sock;
        /// <summary>
        /// socket对象
        /// </summary>
        public Socket Sock
        {
            get
            {
                return sock;
            }
        }
        /// <summary>
        /// 数据包长度
        /// </summary>
        public int BuffLength
        {
            get; set;
        }
        /// <summary>
        /// 输入数据处理
        /// </summary>
        public ClientBinaryInputHandler BinaryInput
        {
            get; set;
        }
        /// <summaBinaryInputy>
        /// 错误异常处理
        /// </summary>
        public ClientMessageInputHandler MessageInput
        {
            get; set;
        }
        /// <summary>
        /// 连接处理
        /// </summary>
        public ConnectionHandler ConnInput
        {
            get; set;
        }
        /// <summary>
        /// 错误代码
        /// </summary>
        private SocketError socketError;
        private readonly ManualResetEvent TimeoutObject = new ManualResetEvent( false );
        /// <summary>
        /// 构造函数，初始化Socket对象
        /// </summary>
        public SocketClient()
        {
            BuffLength = 4098;
            sock = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
        }
        /// <summary>
        /// 异步连接到目标主机
        /// </summary>
        /// <param name="host"></param>
        /// <param name="prot"></param>
        public void BeginConnect( string host, int prot )
        {
            try
            {
                TimeoutObject.Reset();
                IPEndPoint RemotoPoint = new IPEndPoint( IPAddress.Parse( host ), prot );
                sock.BeginConnect( RemotoPoint, ConnAsyncCallBack, sock );
                //等待指定秒数，如果未收到信号，退出链接
                if( !TimeoutObject.WaitOne( 200, false ) )
                {
                    if( ConnInput != null )
                    {
                        ConnInput( false );
                    }
                }
            }
            catch( Exception ex ) { }
        }
        void ConnAsyncCallBack( IAsyncResult result )
        {
            try
            {
                sock.EndConnect( result );
                if( sock.Connected )
                {
                    if( ConnInput != null )
                    {
                        ConnInput( true );
                    }
                }

            }
            catch( Exception ex ) { }
            finally { TimeoutObject.Set(); }
        }
        /// <summary>
        /// 开始读取数据
        /// </summary>
        public void StartRead()
        {
            BeginReceive();
        }
        void BeginReceive()
        {
            byte[] data = new byte[ BuffLength ];

            sock.BeginReceive( data, 0, data.Length, SocketFlags.None, out socketError, args_Completed, data );
        }
        void args_Completed( IAsyncResult result )
        {
            int count = 0;
            try
            {
                count = sock.EndReceive( result );
            }
            catch( SocketException ex )
            {
                socketError = ex.SocketErrorCode;
            }
            catch
            {
                socketError = SocketError.HostDown;
            }
            if( socketError == SocketError.Success && count > 0 )
            {
                byte[] buffer = result.AsyncState as byte[];
                byte[] data = new byte[ count ];
                Array.Copy( buffer, 0, data, 0, data.Length );
                if( this.BinaryInput != null )
                {
                    this.BinaryInput( data, ( IPEndPoint ) sock.RemoteEndPoint );
                    // Close();//when recieved back date,close the connection
                }
                if( sock.Connected )
                {
                    BeginReceive();
                }
            }
            else
            {
                sock.Close();
                if( MessageInput != null )
                    MessageInput( "与服务器连接断开" );
            }

        }
        /// <summary>
        /// 发送数据包
        /// </summary>
        /// <param name="sock"></param>
        /// <param name="data"></param>
        public void SendData( byte[] data )
        {
            try
            {
                if( sock == null || !sock.Connected )
                {
                    return;
                }
                sock.BeginSend( data, 0, data.Length, SocketFlags.None, AsynCallBack, sock );
            }
            catch( SocketException ex )
            {
                if( ex.SocketErrorCode == SocketError.HostDown )
                {
                    sock.Close();
                    if( MessageInput != null )
                    {
                        MessageInput( "与服务器连接断开" );
                    }
                }
            }
        }
        void AsynCallBack( IAsyncResult result )
        {
            try
            {
                Socket sock = result.AsyncState as Socket;
                if( sock != null && sock.Connected )
                {
                    sock.EndSend( result );
                }
            }
            catch( Exception ex ) { }
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            try
            {
                sock.Close();
            }
            catch { }
        }
    }
}
