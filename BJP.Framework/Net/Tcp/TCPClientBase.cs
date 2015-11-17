using System;
using System.Text;
using System.Net;

namespace BJP.Framework.Net.Tcp
{
    public class TCPClientBase
    {
        SocketClient client;
        public bool isConnection = false;

        public void SocketStart( string ipaddress,int ipport )
        {

            client = new SocketClient();
            //设置数据包回调方法
            client.BinaryInput = new ClientBinaryInputHandler( ClientBinaryInputHandler );
            //服务器断开处理
            client.MessageInput = new ClientMessageInputHandler( ClientMessageInputHandler );
            //链接处理
            client.ConnInput = new ConnectionHandler( ConnectionHandler );
            client.BeginConnect( ipaddress, ipport );
        }

        /// <summary>
        /// 异步链接时使用
        /// </summary>
        /// <param name="connection"></param>
        void ConnectionHandler( bool connection )
        {
            isConnection = connection;
            if( connection )
            {
                client.StartRead();
            }
        }
        /// <summary>
        /// 收到服务端的数据包时处理
        /// </summary>
        /// <param name="data"></param>
        void ClientBinaryInputHandler( byte[] data, IPEndPoint endPoint )
        {
            
        }
        void ClientMessageInputHandler( string message )
        {
            //与服务器链接断开
            isConnection = false;
        }

        public bool SendData( byte[] szData )
        {
            client.SendData( szData );
            return true;
        }
        public bool SendData( string szData )
        {
            byte[] sendData = Encoding.UTF8.GetBytes( szData );
            client.SendData( sendData );
            return true;
        }
        public void Close()
        {
            isConnection = false;
            client.Close();
        }

    }
}
