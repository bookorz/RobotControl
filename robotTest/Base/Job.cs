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
        public string FromWay { get; set; }
        public string ToWay { get; set; }

        const string Port_1 = "1201";
        const string Port_2 = "1204";
        const string Aligner_1 = "101";

        public void GetNext()
        {
            switch (Position)
            {
                case Aligner_1:
                    Deliver = "Robot_Cmd_001";
                    Slot = "1";
                    From = Aligner_1;
                    FromWay = "GetAfterWait";
                    To = Port_2;
                    ToWay = "Put";
                    break;
                case Port_2:
                    Deliver = "";
                    break;
            }
        }
    }
}
