using robotTest.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotTest.Controller
{
    public interface IController
    {
        
        void SetReportTarget(ICommandReport target);
        bool SendCommand(Command command);
        void Connect();
        void Close();
        
        void DoWork(RobotCommand Type, Job Job);
         

    }
}
