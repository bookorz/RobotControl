using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotTest.Communication
{
    interface IConnection
    {
        void Connect();
        void SckSSend(string Msg);
        void Close();
    }
}
