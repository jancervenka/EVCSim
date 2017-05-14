using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVC
{
    class MessageContainer
    {
        public List<byte> Msg;

        public MessageContainer(List<byte> Message)
        {
            Msg = Message;
        }
    }
}
