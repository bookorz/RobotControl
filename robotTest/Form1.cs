using log4net;
using log4net.Config;
using robotTest.Base;
using robotTest.Controller;
using robotTest.FileHandle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace robotTest
{
    public partial class Form1 : Form, ICommandReport
    {
        ILog logger = LogManager.GetLogger(typeof(Form1));
        delegate void UpdateController(string Device_ID, string Detail);
        delegate void UpdateScriptController(int Idx);
        delegate void UpdateMapResultDG(DataGridView TarDG, List<Slot> Port);
        delegate void UpdateAlignerDG(DataGridView TarDG, List<string> Aligner);
        Dictionary<string, IController> ControllerList = new Dictionary<string, IController>();
        Dictionary<string, ControllerInfo> ControllerStatus = new Dictionary<string, ControllerInfo>();
        List<Command> cmdList = new List<Command>();
        Dictionary<string, Job> JobList = new Dictionary<string, Job>();
        List<Slot> Port1 = new List<Slot>();
        List<Slot> Port2 = new List<Slot>();
        List<Slot> Aligner = new List<Slot>();
        int RunIdx = 0;
        string ScriptPath = "";
        bool running = false;
        const string Port_1 = "1201";
        const string Port_2 = "1204";
        const string Aligner_1 = "101";

        public Form1()
        {
            InitializeComponent();
            // 因為要讀取從App.config讀取設定，所以使用XmlConfigurator
            XmlConfigurator.Configure();


            logger.Info("initial complete.");
        }

        private void LoadConnCfg()
        {
            ConfigReader<ControllerInfo> ControllerCfg = new ConfigReader<ControllerInfo>();
            foreach (ControllerInfo each in ControllerCfg.ReadFile("config/controllers.json"))
            {
                if (!ControllerStatus.ContainsKey(each.ControllerName))
                {
                    each.Status = "Disconnected";
                    ControllerStatus.Add(each.ControllerName, each);
                    if (!DeviceNo_cb.Items.Contains(each.DeviceNo))
                    {
                        DeviceNo_cb.Items.Add(each.DeviceNo);
                    }
                    Controller_cb.Items.Add(each.ControllerName);
                }
                if (!ControllerList.ContainsKey(each.ControllerName))
                {
                    switch (each.ControllerType)
                    {
                        case "Robot":
                            ControllerList.Add(each.ControllerName, new RobotController(each.IPAdress, each.Port, each.CommandTimeout, each.ControllerName, this));
                            break;
                    }

                }
            }

            Conn_gv.AutoGenerateColumns = true;
            Conn_gv.DataSource = ControllerStatus.Values.ToList();
            Conn_gv.ClearSelection();
            ConfigReader<Common> CommandTypeCfg = new ConfigReader<Common>();
            foreach (Common each in CommandTypeCfg.ReadFile("config/Command/CommandType.json"))
            //foreach (Common each in CommandTypeCfg.ReadFile("config/controllers.json"))
            {
                CmdType_cb.Items.Add(each.Name);
            }
        }

        private void Connect_Click(object sender, EventArgs e)
        {


            try
            {
                foreach (IController each in ControllerList.Values)
                {
                    each.Connect();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Connect_Click:" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadConnCfg();
            
           
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (IController each in ControllerList.Values)
            {
                each.Close();
            }
        }

        private void UpdateMapResult(DataGridView TarDG, List<Slot> Port)
        {
            if (TarDG.InvokeRequired)
            {
                //當InvokeRequired為true時，表示在不同的執行緒上，所以進行委派的動作!!
                UpdateMapResultDG ph = new UpdateMapResultDG(UpdateMapResult);
                TarDG.Invoke(ph, TarDG, Port);
            }
            else
            {
                TarDG.DataSource = null;
                
                TarDG.DataSource = Port;
                TarDG.Refresh();

            }
        }

        private void UpdateAligner(DataGridView TarDG, List<string> Aligner)
        {
            if (TarDG.InvokeRequired)
            {
                //當InvokeRequired為true時，表示在不同的執行緒上，所以進行委派的動作!!
                UpdateAlignerDG ph = new UpdateAlignerDG(UpdateAligner);
                TarDG.Invoke(ph, TarDG, Aligner);
            }
            else
            {
                TarDG.DataSource = null;
                TarDG.DataSource = Aligner;
                TarDG.Refresh();

            }
        }

        private void UpdateControllerStatus(string Device_ID, string Status)
        {
            if (Conn_gv.InvokeRequired)
            {
                //當InvokeRequired為true時，表示在不同的執行緒上，所以進行委派的動作!!
                UpdateController ph = new UpdateController(UpdateControllerStatus);
                Conn_gv.Invoke(ph, Device_ID, Status);
            }
            else
            {
                ControllerInfo target;
                if (ControllerStatus.TryGetValue(Device_ID, out target))
                {
                    target.Status = Status;
                    Conn_gv.DataSource = ControllerStatus.Values.ToList();
                    //Conn_gv.Refresh();
                    Conn_gv.ClearSelection();
                }
            }
        }

        private void UpdateScriptProgress(int Idx)
        {
            if (Script_gv.InvokeRequired)
            {
                //當InvokeRequired為true時，表示在不同的執行緒上，所以進行委派的動作!!
                UpdateScriptController ph = new UpdateScriptController(UpdateScriptProgress);
                Script_gv.Invoke(ph, Idx);
            }
            else
            {
                for (int i = 0; i < Script_gv.Rows[Idx].Cells.Count; i++)
                {
                    Script_gv.Rows[Idx].Cells[i].Style.BackColor = Color.Green;
                    if (Idx != 0)
                    {
                        Script_gv.Rows[Idx - 1].Cells[i].Style.BackColor = Color.White;
                    }
                    else
                    {
                        for (int k = 0; k < Script_gv.Rows.Count; k++)
                        {
                            for (int j = 0; j < Script_gv.Rows[k].Cells.Count; j++)
                            {
                                Script_gv.Rows[k].Cells[j].Style.BackColor = Color.White;
                            }
                        }
                    }
                }
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void Conn_gv_DataSourceChanged(object sender, EventArgs e)
        {

        }

        private void Conn_gv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                switch (e.Value)
                {
                    case "Idle":
                        e.CellStyle.BackColor = Color.Green;
                        break;
                    case "Waiting":
                        e.CellStyle.BackColor = Color.Yellow;
                        break;
                    case "Disconnected":
                        e.CellStyle.BackColor = Color.Red;
                        break;
                    case "Runing":
                        e.CellStyle.BackColor = Color.Yellow;
                        break;
                }
            }
        }

        private void CmdType_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Instruc_cb.Text = "";

                ConfigReader<Common> InstructionCfg = new ConfigReader<Common>();
                //foreach (Common each in InstructionCfg.ReadFile("config/Command/"+ CmdType_cb.Text+ ".json"))
                //{
                //    Instruc_cb.Items.Add(each.Name);

                //}
                Instruc_cb.DisplayMember = "Name";
                Instruc_cb.ValueMember = "Option";
                Instruc_cb.DataSource = InstructionCfg.ReadFile("config/Command/" + CmdType_cb.Text + ".json");
            }
            catch (Exception ex)
            {
                logger.Error("CmdType_cb_SelectedIndexChanged:" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void ExcuteCommand_bt_Click(object sender, EventArgs e)
        {
            IController TargetController;
            if (ControllerList.TryGetValue(Controller_cb.Text, out TargetController))
            {
                Command Cmd = new Command();
                Cmd.SetADR(DeviceNo_cb.Text);
                Cmd.SetFLG(CmdType_cb.Text);
                Cmd.SetCMD(Instruc_cb.Text);
                Cmd.SetDAT(param_tb.Text);
                TargetController.SetReportTarget(this);
                TargetController.SendCommand(Cmd);
            }
            else
            {
                logger.Error("找不到" + Controller_cb.Text);
            }
        }

        private void Run(Command Cmd)
        {
            IController TargetController;
            if (ControllerList.TryGetValue(Cmd.GetController(), out TargetController))
            {

                TargetController.SendCommand(Cmd);
            }
            else
            {
                logger.Error("找不到" + Controller_cb.Text);
            }
        }

        private void Instruc_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            param_tb.Text = ((Common)Instruc_cb.SelectedItem).Option;
        }

        private void AddToScript_bt_Click(object sender, EventArgs e)
        {
            Command Cmd = new Command();
            Cmd.SetController(Controller_cb.Text);
            Cmd.SetADR(DeviceNo_cb.Text);
            Cmd.SetFLG(CmdType_cb.Text);
            Cmd.SetCMD(Instruc_cb.Text);
            Cmd.SetDAT(param_tb.Text);
            cmdList.Add(Cmd);
            Script_gv.DataSource = null;
            Script_gv.DataSource = cmdList;
            //Script_gv.Refresh();
        }

        private void Delete_bt_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedIdx = 0;
                if (Script_gv.SelectedRows.Count != 0)
                {
                    selectedIdx = Script_gv.SelectedRows[0].Index;
                }
                else
                {
                    selectedIdx = Script_gv.SelectedCells[0].RowIndex;
                }

                Command selectedItem = cmdList[selectedIdx];
                cmdList.RemoveAt(selectedIdx);
                Script_gv.DataSource = null;
                Script_gv.DataSource = cmdList;
            }
            catch (Exception ex)
            {
                logger.Error("Delete_bt_Click:" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void SaveScript_bt_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDg = new SaveFileDialog();
            saveDg.DefaultExt = ".json";
            saveDg.RestoreDirectory = true;
            if (saveDg.ShowDialog() == DialogResult.OK)
            {
                ConfigReader<Command> Script = new ConfigReader<Command>();
                Script.WriteFile(saveDg.FileName.ToString(), cmdList);
            }
        }

        private void LoadScript_bt_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Title = "Select file";
                dialog.InitialDirectory = "config\\Script";
                //dialog.Filter = "xls files (*.*)|*.xls";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ConfigReader<Command> Script = new ConfigReader<Command>();
                    cmdList = Script.ReadFile(dialog.FileName);
                    ScriptPath = dialog.FileName;
                    Script_gv.DataSource = null;
                    Script_gv.DataSource = cmdList;
                }
            }
            catch (Exception ex)
            {
                logger.Error("LoadScript_bt_Click:" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void RunScript_bt_Click(object sender, EventArgs e)
        {
            running = true;
            RunIdx = 0;
            Run(cmdList[RunIdx]);
            UpdateScriptProgress(RunIdx);
        }

        private void Up_bt_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedIdx = 0;
                if (Script_gv.SelectedRows.Count != 0)
                {
                    selectedIdx = Script_gv.SelectedRows[0].Index;
                }
                else
                {
                    selectedIdx = Script_gv.SelectedCells[0].RowIndex;
                }
                if (selectedIdx != 0)
                {
                    Command selectedItem = cmdList[selectedIdx];
                    cmdList.RemoveAt(selectedIdx);
                    cmdList.Insert(selectedIdx - 1, selectedItem);
                    Script_gv.Refresh();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Up_bt_Click:" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void Down_bt_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedIdx = 0;
                if (Script_gv.SelectedRows.Count != 0)
                {
                    selectedIdx = Script_gv.SelectedRows[0].Index;
                }
                else
                {
                    selectedIdx = Script_gv.SelectedCells[0].RowIndex;
                }
                if (selectedIdx != cmdList.Count - 1)
                {
                    Command selectedItem = cmdList[selectedIdx];
                    cmdList.RemoveAt(selectedIdx);
                    cmdList.Insert(selectedIdx + 1, selectedItem);
                    Script_gv.Refresh();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Down_bt_Click:" + ex.Message + "\n" + ex.StackTrace);
            }
        }
        void ICommandReport.On_Command_Excuted(string Device_ID, ReturnMsg Msg, Command Cmd, Job Job)
        {
            if (!running)
            {
                IController robot1;
                if (Cmd.GetFLG().Equals("GET"))
                {
                    switch (Msg.GetCMD())
                    {
                        case "MAP__":

                            #region MAP回傳結果
                            //string[] mapResult = Msg.GetDAT().Split(',');
                            string[] mapResult = "1,1,1,1".Split(',');//模擬slot 1~4 有片
                            for (int i = 1; i < mapResult.Count(); i++)
                            {
                                string status = mapResult[i];

                                switch (status)
                                {
                                    case "0":
                                        status = "無";
                                        break;
                                    case "1":
                                        status = "有";
                                        break;
                                    case "W":
                                        status = "厚度異常";
                                        break;
                                    case "E":
                                        status = "傾斜異常";
                                        break;
                                }
                                Slot eachSlot = new Slot();
                                eachSlot.JobID = "";
                                eachSlot.SlotNo = i.ToString(); ;
                                eachSlot.Status = status;
                                Port1.Add(eachSlot);

                            }
                            UpdateMapResult(Port1_gv, Port1);
                            #endregion
                            break;

                    }
                }
                else if (Cmd.GetFLG().Equals("CMD"))
                {
                    switch (Msg.GetCMD())
                    {
                        case "GET__"://Panel 在手臂上
                            if (Job.Position.Equals(Aligner))
                            {
                                Aligner.Clear(); ;
                                UpdateMapResult(Aligner_gv, Aligner);
                            }
                            Job.Position = Device_ID;
                            
                           
                            break;
                        case "PUT__":
                        case "GETW_":

                            break;
                    }
                }
            }
        }

        void ICommandReport.On_Command_Error(string Device_ID, ReturnMsg Msg, Command Cmd, Job Job)
        {
            MessageBox.Show(Device_ID + "錯誤發生，錯誤碼:" + Msg.GetDAT());
        }

        public void On_Command_Finished(string Device_ID, ReturnMsg Msg, Command Cmd, Job Job)
        {
            if (running)
            {
                RunIdx++;
                if (RunIdx < cmdList.Count)
                {
                    Run(cmdList[RunIdx]);
                    UpdateScriptProgress(RunIdx);
                }
                else
                {
                    RunIdx = 0;
                    Run(cmdList[RunIdx]);
                    UpdateScriptProgress(RunIdx);

                }
            }
            else
            {
                IController robot1;
                if (Device_ID.Equals("Aligner"))
                {
                    foreach(Job eachJob in JobList.Values)
                    {
                        if (eachJob.Position.Equals(Aligner_1))
                        {
                            if (ControllerList.TryGetValue(eachJob.Deliver, out robot1))
                            {
                                

                                robot1.DoWork("GetAfterWait", eachJob);
                            }
                        }
                    }
                    
                    

                }
                else
                {


                    switch (Msg.GetCMD())
                    {
                        case "MAP__":
                            #region MAP動作完成

                            if (ControllerList.TryGetValue(Device_ID, out robot1))
                            {
                                robot1.GetMap();
                            }
                            #endregion
                            break;
                        case "GET__":
                            if (Job.From.Equals(Port_1))
                            {
                                for(int i = 0; i < Port1.Count; i++)
                                {
                                    if (Port1[i].JobID.Equals(Job.JobID))
                                    {
                                        Port1.Remove(Port1[i]);
                                    }
                                }
                                
                                UpdateMapResult(Port1_gv, Port1);
                            }
                            if ((Job.From.Equals(Aligner_1)))
                            {
                                Aligner.Clear();
                                UpdateMapResult(Aligner_gv,Aligner);
                            }
                            if (ControllerList.TryGetValue(Device_ID, out robot1))
                            {
                                if (Job.ToWay.Equals(Port_2))
                                {
                                    Job.ToSlot = Port2.Count.ToString("000");
                                }
                                robot1.DoWork(Job.ToWay, Job);
                            }

                            break;
                        case "PUT__":
                            Job.Position = Job.To;
                            //Job取得下一站目的地
                            Job.GetNext();
                            if (Job.Position.Equals(Aligner_1))
                            {
                                Slot inPanel = new Slot();
                                inPanel.JobID = Job.JobID;
                                Aligner.Add(inPanel);
                                UpdateMapResult(Aligner_gv, Aligner);
                                break;
                            }
                            if (Job.Position.Equals(Port_2))
                            {
                                Slot inPanel = new Slot();
                                inPanel.JobID = Job.JobID;
                                inPanel.SlotNo = (Port2.Count+1).ToString();
                                Port2.Add(inPanel);
                                UpdateMapResult(Port2_gv,Port2);
                               
                            }

                            //該Robot命令完成,尋找需要搬運的Panel
                            foreach (Job eachJob in JobList.Values)
                            {
                                if (eachJob.Deliver.Equals(Device_ID) && eachJob.Position.Equals(eachJob.From))
                                {
                                    if (ControllerList.TryGetValue(Device_ID, out robot1))
                                    {
                                        robot1.DoWork(eachJob.FromWay, eachJob);
                                    }
                                }
                            }

                            break;
                    }
                }
            }
        }

        void ICommandReport.On_Command_TimeOut(string Device_ID, Command Cmd, Job Job)
        {

        }

        void ICommandReport.On_Status_Changed(string Device_ID, string Status)
        {
            UpdateControllerStatus(Device_ID, Status);
        }

        void ICommandReport.On_Event_Trigger(string Device_ID, string Event)
        {

        }

        private void Conn_gv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ControllerInfo controller = ((List<ControllerInfo>)Conn_gv.DataSource)[e.RowIndex];
            IController target;
            if (ControllerList.TryGetValue(controller.ControllerName, out target))
            {
                RobotStatusFrm status_frm = new RobotStatusFrm(target, this);
                status_frm.Show();
            }
        }

        private void Stop_bt_Click(object sender, EventArgs e)
        {
            running = false;
        }

        private void LDCM_bt_Click(object sender, EventArgs e)
        {
            JobList.Clear();
            Port1.Clear();
            Port2.Clear();
            //IController robot1;
            //if (ControllerList.TryGetValue("Robot_Cmd_001", out robot1))
            //{
            //    robot1.Map(Port_1);
            //}
            string[] mapResult = "1,1,1,1,1,1,1,1,1,1,1,1".Split(',');//模擬slot 1~4 有片
            for (int i = 1; i < mapResult.Count(); i++)
            {
                string status = mapResult[i];

                switch (status)
                {
                    case "0":
                        status = "無";
                        break;
                    case "1":
                        status = "有";
                        break;
                    case "W":
                        status = "厚度異常";
                        break;
                    case "E":
                        status = "傾斜異常";
                        break;
                }
                Slot eachSlot = new Slot();
                eachSlot.JobID = "";
                eachSlot.SlotNo = i.ToString(); ;
                eachSlot.Status = status;
                Port1.Add(eachSlot);

            }
            UpdateMapResult(Port1_gv, Port1);
        }

        private void DataReq_bt_Click(object sender, EventArgs e)
        {
            int i = 1;
            foreach (Slot eachSlot in Port1)
            {
                eachSlot.JobID = "Panel" + i.ToString("000");
                i++;
                Job eachJob = new Job();
                eachJob.JobID = eachSlot.JobID;
                eachJob.Position = Port_1;
                eachJob.Slot = eachSlot.SlotNo;
                eachJob.From = Port_1;
                eachJob.To = Aligner_1;
                eachJob.ToSlot = "1";
                eachJob.FromWay = "Get";
                eachJob.ToWay = "PutAndWait";
                eachJob.Deliver = "Robot_Cmd_001";
                JobList.Add(eachJob.JobID, eachJob);
            }
            UpdateMapResult(Port1_gv, Port1);
        }



        private void ProcessStart_bt_Click(object sender, EventArgs e)
        {
            IController robot1;
            Job first = JobList.Values.ToList()[0];
            if (ControllerList.TryGetValue(first.Deliver, out robot1))
            {
                robot1.DoWork(first.FromWay, first);
            }
        }

        private void ALComplete_bt_Click(object sender, EventArgs e)
        {
            On_Command_Finished("Aligner", null, null, null);
        }

        
       

        
    }
}
