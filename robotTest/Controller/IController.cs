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
        string GetStatus();
        void SetStatus(string _Status);

        void Map(string Position);
        void GetMap();        
        void DoWork(string Type, Job Job);

    }
}
