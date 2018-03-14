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
        delegate void UpdateMapResultDG();
        Dictionary<string, IController> ControllerList = new Dictionary<string, IController>();
        Dictionary<string, ControllerInfo> ControllerStatus = new Dictionary<string, ControllerInfo>();
        List<Command> cmdList = new List<Command>();
        List<Job> cassette1 = new List<Job>();

        int RunIdx = 0;
        string ScriptPath = "";
        bool running = false;

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

        private void UpdateMapResult()
        {
            if (Port1_gv.InvokeRequired)
            {
                //當InvokeRequired為true時，表示在不同的執行緒上，所以進行委派的動作!!
                UpdateMapResultDG ph = new UpdateMapResultDG(UpdateMapResult);
                Port1_gv.Invoke(ph);
            }
            else
            {
                Port1_gv.DataSource = cassette1;
                Conn_gv.Refresh();
                Conn_gv.ClearSelection();

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
                Cmd.ADR = DeviceNo_cb.Text;
                Cmd.FLG = CmdType_cb.Text;
                Cmd.CMD = Instruc_cb.Text;
                Cmd.DAT = param_tb.Text;
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
            if (ControllerList.TryGetValue(Cmd.Controller, out TargetController))
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
            Cmd.Controller = Controller_cb.Text;
            Cmd.ADR = DeviceNo_cb.Text;
            Cmd.FLG = CmdType_cb.Text;
            Cmd.CMD = Instruc_cb.Text;
            Cmd.DAT = param_tb.Text;
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
        void ICommandReport.On_Command_Excuted(string Device_ID, ReturnMsg Msg, Command Cmd)
        {

        }

        void ICommandReport.On_Command_Error(string Device_ID, ReturnMsg Msg, Command Cmd)
        {
            MessageBox.Show(Device_ID + "錯誤發生，錯誤碼:" + Msg.GetDAT());
        }

        void ICommandReport.On_Command_Finished(string Device_ID, ReturnMsg Msg, Command Cmd)
        {
            if (!Cmd.FLG.Equals("DELAY"))
            {
                if (Msg.GetFLG().Equals("ACK"))
                {
                    switch (Msg.GetCMD())
                    {
                        case "MAP__":
                            string[] mapResult = Msg.GetDAT().Split(',');
                            for (int i = 1; i < mapResult.Count(); i++)
                            {
                                string status = mapResult[i];
                                Job eachJob = new Job();
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
                                eachJob.JobID = "Panel-" + i.ToString();
                                eachJob.status = status;
                                cassette1.Add(eachJob);
                            }
                            UpdateMapResult();
                            break;
                    }
                }
            }

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
        }

        void ICommandReport.On_Command_TimeOut(string Device_ID, Command Cmd)
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
    }
}
