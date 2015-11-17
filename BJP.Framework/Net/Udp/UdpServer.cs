using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace BJP.Framework.Net.Udp
{
    public class UdpServer
    {
        /// <summary>
        /// 接收端口
        /// </summary>
        Int32 receivePort;

        /// <summary>
        /// 发送端口
        /// </summary>
        Int32 sendPort;

        /// <summary>
        /// 最大客户端数
        /// </summary>
        Int32 maxClient;

        /// <summary>
        /// 接收数据的socket
        /// </summary>
        public ReceiveSocket receivedData;

        /// <summary>
        /// 发送数据的socket
        /// </summary>
        public SendSocket sendData;

        /// <summary>
        /// 接收事件句柄
        /// </summary>
        public event EventHandler<SocketAsyncEventArgs> OnReceiveData;

        /// <summary>
        /// 发送事件句柄
        /// </summary>
        public event EventHandler<SocketAsyncEventArgs> OnSendData;


        /// <summary>
        /// 构造函数，不带参数
        /// 默认接收端口5555，发送端口4445，最在客户端数10000
        /// </summary>
        public UdpServer()
        {
            this.receivePort = 5555;
            this.sendPort = 4445;
            this.maxClient = 10000;

            receivedData = new ReceiveSocket( receivePort );
            receivedData.OnDataReceived += new EventHandler<SocketAsyncEventArgs>( receivedData_OnDataReceived );

            sendData = new SendSocket( maxClient );
            sendData.OnDataSend += new EventHandler<SocketAsyncEventArgs>( sendData_OnDataSend );
            sendData.Init();
        }
        /// <summary>
        /// 构造函数,带参数
        /// </summary>
        /// <param name="receivePort"></param>
        /// <param name="sendPort"></param>
        /// <param name="totalClient"></param>
        public UdpServer( Int32 receivePort, Int32 sendPort, Int32 totalClient )
        {
            receivedData = new ReceiveSocket( receivePort );
            receivedData.OnDataReceived += new EventHandler<SocketAsyncEventArgs>( receivedData_OnDataReceived );

            sendData = new SendSocket( totalClient );
            sendData.OnDataSend += new EventHandler<SocketAsyncEventArgs>( sendData_OnDataSend );
            sendData.Init();
        }

        /// <summary>
        /// 启动接收监听
        /// </summary>
        public void Start()
        {
            receivedData.StartReceive();
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void receivedData_OnDataReceived( object sender, SocketAsyncEventArgs e )
        {
            if( OnReceiveData != null )
            {
                OnReceiveData( sender, e );
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void sendData_OnDataSend( object sender, SocketAsyncEventArgs e )
        {
            if( OnSendData != null )
            {
                OnSendData( sender, e );
            }
        }

        /// <summary>
        /// 发送socket
        /// </summary>
        public SendSocket _SendSocket
        {
            get
            {
                if( sendData != null )
                {
                    return sendData;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 接收socket
        /// </summary>
        public ReceiveSocket _ReceiveSocket
        {
            get
            {
                if( receivedData != null )
                {
                    return receivedData;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
