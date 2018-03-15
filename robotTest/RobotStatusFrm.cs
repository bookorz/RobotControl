using robotTest.Base;
using robotTest.Controller;
using robotTest.FileHandle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace robotTest
{
    public partial class RobotStatusFrm : Form,ICommandReport
    {
        IController _Controller;
        ICommandReport _Report;
        List<Command> cmdList = new List<Command>();
        RobotStatus status = new RobotStatus();
        List<ObjectView> statusList = new List<ObjectView>();
        delegate void RefreshStatusDg();
        int RunIdx = 0;
        public RobotStatusFrm(IController Controller,ICommandReport Report)
        {
            InitializeComponent();
            _Controller = Controller;
            _Report = Report;
            _Controller.SetReportTarget(this);
        }

        private void RobotStatus_Load(object sender, EventArgs e)
        {
            ConfigReader<Command> Script = new ConfigReader<Command>();
            cmdList = Script.ReadFile("config/StatusScript/RobotStatus.json");
            RunIdx = 0;
            _Controller.SendCommand(cmdList[RunIdx]);
           
        }

        void ICommandReport.On_Command_Error(string Device_ID, ReturnMsg Msg, Command Cmd, Job Job)
        {
           
        }

        void ICommandReport.On_Command_Excuted(string Device_ID, ReturnMsg Msg, Command Cmd, Job Job)
        {
            
        }

        void ICommandReport.On_Command_Finished(string Device_ID, ReturnMsg Msg, Command Cmd, Job Job)
        {
            string[] reply;
            switch (Msg.GetCMD())
            {
                case "PNSTS":
                    reply = Msg.GetDAT().Split(',');
                    string arm = reply[0];
                    string vac = reply[1];
                    string pre = reply[2];
                    switch (arm)
                    {
                        case "1":
                            //R
                            
                            status.R_ArmVacuumNo1 = Convert.ToBoolean(Convert.ToInt16(vac[0]));
                            status.R_ArmVacuumNo2 = Convert.ToBoolean(Convert.ToInt16(vac[1]));
                            status.R_ArmVacuumNo3 = Convert.ToBoolean(Convert.ToInt16(vac[2]));
                            status.R_ArmVacuumNo4 = Convert.ToBoolean(Convert.ToInt16(vac[3]));

                            status.R_ArmPresentNo1 = Convert.ToBoolean(Convert.ToInt16(pre[0]));
                            status.R_ArmPresentNo2 = Convert.ToBoolean(Convert.ToInt16(pre[1]));
                            status.R_ArmPresentNo3 = Convert.ToBoolean(Convert.ToInt16(pre[2]));
                            status.R_ArmPresentNo4 = Convert.ToBoolean(Convert.ToInt16(pre[3]));
                            break;
                        case "2":
                            //L
                            status.L_ArmVacuumNo1 = Convert.ToBoolean(Convert.ToInt16(vac[0]));
                            status.L_ArmVacuumNo2 = Convert.ToBoolean(Convert.ToInt16(vac[1]));
                            status.L_ArmVacuumNo3 = Convert.ToBoolean(Convert.ToInt16(vac[2]));
                            status.L_ArmVacuumNo4 = Convert.ToBoolean(Convert.ToInt16(vac[3]));

                            status.L_ArmPresentNo1 = Convert.ToBoolean(Convert.ToInt16(pre[0]));
                            status.L_ArmPresentNo2 = Convert.ToBoolean(Convert.ToInt16(pre[1]));
                            status.L_ArmPresentNo3 = Convert.ToBoolean(Convert.ToInt16(pre[2]));
                            status.L_ArmPresentNo4 = Convert.ToBoolean(Convert.ToInt16(pre[3]));
                            break;
                    }

                    break;
                case "POS__":
                    reply = Msg.GetDAT().Split(',');
                    status.R_EncoderPosition = reply[0];
                    status.L_EncoderPosition = reply[1];
                    status.S_EncoderPosition = reply[2];
                    status.Z_EncoderPosition = reply[3];
                    status.X_EncoderPosition = reply[4];
                    status.R1_EncoderPosition = reply[5];
                    break;
            }



            RunIdx++;
            if (RunIdx < cmdList.Count)
            {
                _Controller.SendCommand(cmdList[RunIdx]);
               
            }
            else
            {
                RefreshStatus();
                RunIdx = 0;
                _Controller.SendCommand(cmdList[RunIdx]);
            }
        }

        private void RefreshStatus()
        {
            if (Status_gv.InvokeRequired)
            {
                //當InvokeRequired為true時，表示在不同的執行緒上，所以進行委派的動作!!
                RefreshStatusDg ph = new RefreshStatusDg(RefreshStatus);
                Status_gv.Invoke(ph);
            }
            else
            {
                statusList.Clear();
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(status))
                {
                    ObjectView each = new ObjectView();
                    each.Name = descriptor.Name;
                    each.Value = descriptor.GetValue(status).ToString();
                    statusList.Add(each);
                }
                Status_gv.DataSource = statusList;
                Status_gv.Refresh();
            }
        }

        void ICommandReport.On_Command_TimeOut(string Device_ID, Command Cmd, Job Job)
        {
          
        }

        void ICommandReport.On_Event_Trigger(string Device_ID, string Event)
        {
         
        }

        void ICommandReport.On_Status_Changed(string Device_ID, string Status)
        {
            _Report.On_Status_Changed(Device_ID, Status);
        }

        private void RobotStatusFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _Controller = null;
        }
    }
}
