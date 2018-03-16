using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotTest
{
    interface IConnectionReport
    {
        void OnSocketMessage(string Msg);
        void OnConnecting();
        void OnConnected();
        void OnError();
    }
}
