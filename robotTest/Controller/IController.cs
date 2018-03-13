using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotTest.Controller
{
    interface IController
    {
        bool SendCommand(Command command);
        void Connect();
        void Close();
    }
}
