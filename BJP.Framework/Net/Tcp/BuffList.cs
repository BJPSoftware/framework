using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJP.Framework.Net.Tcp
{
    public class BuffList
    {
        public List<byte> ByteList
        {
            get;
            set;
        }
        public int Current
        {
            get;
            set;
        }

        public BuffList()
        {
            ByteList = new List<byte>( 4096 );
        }
        public void Reset()
        {
            Current = 0;
        }
        public void Clecr()
        {
            ByteList.Clear();
        }

        public void InsertByteArray( byte[] Data )
        {
            ByteList.AddRange( Data );
        }

        public byte[] GetData( int ml )
        {
            if( ByteList.Count < ml )
            {
                return null;
            }

            int res = 0;
            for( int i = 0; i < ml; i++ )
            {
                int temp = ( ( int ) ByteList[ Current + i ] ) & 0xff;
                temp <<= i * 8;
                res = res + temp;
            }

            if( res <= 0 )
            {
                Reset();
                ByteList.Clear();
                return null;
            }

            if( res > ( ByteList.Count - Current ) )
            {
                return null;
            }
            byte[] data = new byte[ res ];
            ByteList.CopyTo( Current, data, 0, data.Length );

            Current += res;

            if( Current == ByteList.Count )
            {
                Reset();
                ByteList.Clear();
            }
            return data;
        }

    }
}
