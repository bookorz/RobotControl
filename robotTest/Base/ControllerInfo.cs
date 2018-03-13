using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotTest.Base
{
    class ControllerInfo
    {
        public string ControllerName { get; set; }
        public string Status { get; set; }
        public string IPAdress { get; set; }
        public int Port { get; set; }
        public string ControllerType { get; set; }

        public int CommandTimeout { get; set; }
        public string DeviceNo { get; set; }


    }
}
