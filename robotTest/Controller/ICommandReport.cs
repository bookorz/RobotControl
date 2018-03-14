using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotTest.Controller
{
    public interface ICommandReport
    {
        void On_Command_Excuted(string Device_ID, ReturnMsg Msg, Command Cmd);
        void On_Command_Error(string Device_ID, ReturnMsg Msg, Command Cmd);
        void On_Command_Finished(string Device_ID, ReturnMsg Msg, Command Cmd);
        void On_Command_TimeOut(string Device_ID, Command Cmd);
        void On_Event_Trigger(string Device_ID, string Event);
        void On_Status_Changed(string Device_ID, string Status);
    }
}
