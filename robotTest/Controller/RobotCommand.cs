using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotTest
{
    [Flags]
    public enum RobotCommand
    {
        Get = 0,
        Put = 1,
        GetAndWait = 2,
        GetAfterWait = 3,
        PutAndWait = 4,
        ALIGN = 5
    }
}
