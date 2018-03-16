using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotTest.Base
{
    public class Job
    {
        public string Slot { get; set; }
        public string JobID { get; set; }
        public string Status { get; set; }
        public string Position { get; set; }
        public string Deliver { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string ToSlot { get; set; }
        public string NotchDegree { get; set; }
        public RobotCommand FromWay { get; set; }
        public RobotCommand ToWay { get; set; }
        public string Producer { get; set; }
        public RobotCommand ProcessWay { get; set; }



        public void GetNext()
        {
            switch (Position)
            {
                case Form1.Aligner_1:
                    Deliver = "Robot_Cmd_001";
                    Slot = "1";
                    From = Form1.Aligner_1;
                    FromWay = RobotCommand.GetAfterWait;
                    To = Form1.Port_2;
                    ToWay = RobotCommand.Put;
                    Producer = "Aligner_001";
                    ProcessWay = RobotCommand.ALIGN;
                    break;
                case Form1.Port_2:
                    Deliver = "";
                    break;
            }
        }
    }
}
