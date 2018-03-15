using log4net;
using robotTest.Base;
using robotTest.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace robotTest
{
    class RobotController : ISocketMessage, IController
    {
        SocketClient cmdSck;
        ILog logger = LogManager.GetLogger(typeof(RobotController));
        string IP = "";
        int Port = 0;
        string ControllerName = "";
        Command LastSendCommand;
        Job LastJob;
        int Timeout = 0;

        ICommandReport tObj;

        //逾時
        System.Timers.Timer timeOutTimer = new System.Timers.Timer();
        System.Timers.Timer actionTimeOutTimer = new System.Timers.Timer();

        //狀態管理
        string Status = Disconnected;
        ReaderWriterLockSlim StatusLock = new ReaderWriterLockSlim();
        public const string Idle = "Idle";
        public const string Waiting = "Waiting";
        public const string Runing = "Runing";
        public const string Pause = "Pause";
        public const string Connecting = "Connecting";
        public const string Disconnected = "Disconnected";

        public string GetStatus()
        {
            string result = "";
            StatusLock.EnterReadLock();
            result = Status;
            StatusLock.ExitReadLock();
            return result;
        }

        public void SetStatus(string _Status)
        {
            StatusLock.EnterWriteLock();
            if (!Status.Equals(Disconnected))
            {
                Status = _Status;
            }
            StatusLock.ExitWriteLock();
            tObj.On_Status_Changed(ControllerName, GetStatus());
        }

        public RobotController(string _IP, int _Port, int _Timeout, string _ControllerName, ICommandReport _tObj)
        {
            IP = _IP;
            Port = _Port;
            ControllerName = _ControllerName;
            Timeout = Convert.ToInt16(_Timeout);

            tObj = _tObj;
            timeOutTimer.Enabled = false;

            timeOutTimer.Interval = Timeout;

            timeOutTimer.Elapsed += new System.Timers.ElapsedEventHandler(TimeOutMonitor);

            actionTimeOutTimer.Enabled = false;

            actionTimeOutTimer.Interval = Timeout;

            actionTimeOutTimer.Elapsed += new System.Timers.ElapsedEventHandler(ActionTimeOutMonitor);

        }

        private void TimeOutMonitor(object sender, System.Timers.ElapsedEventArgs e)
        {
            timeOutTimer.Enabled = false; SetStatus(Idle);
            logger.Error("Time out! Send to:" + IP + ":" + Port + " Command:" + LastSendCommand);
            tObj.On_Command_TimeOut(ControllerName, LastSendCommand,LastJob);

        }

        private void ActionTimeOutMonitor(object sender, System.Timers.ElapsedEventArgs e)
        {
            actionTimeOutTimer.Enabled = false; SetStatus(Idle);
            logger.Error("Action time out! Send to:" + IP + ":" + Port + " Command:" + LastSendCommand);
            tObj.On_Command_TimeOut(ControllerName, LastSendCommand, LastJob);

        }


        public bool SendCommand(Command command)
        {
            bool result = false;
            try
            {
                if (!Status.Equals(Waiting))
                {
                    SetStatus(Waiting);
                    LastSendCommand = command;
                    
                    if (!command.GetFLG().Equals("DELAY"))
                    {
                        cmdSck.SckSSend(command.GetCommandStr());
                        timeOutTimer.Enabled = true;
                    }
                    else
                    {
                        Thread delay = new Thread(Delay);
                        delay.IsBackground = true;
                        delay.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("SendCommand:" + ex.Message + "\n" + ex.StackTrace);
            }
            return result;
        }

        private void Delay()
        {
            Thread.Sleep(3000);
            SetStatus(Idle);
            tObj.On_Command_Finished(ControllerName, null, LastSendCommand, LastJob);
        }

        //public void Carry(string Get_Position,string Put_Position,Job Get_Job, Job Put_Job)
        //{

        //}

        public void DoWork(string Type,Job Job)
        {
            switch (Type)
            {
                
                case "Get":
                    Get(Job);
                    break;
                case "Put":
                    Put(Job);
                    break;
                case "GetAndWait":
                    GetAndWait(Job);
                    break;
                case "GetAfterWait":
                    GetAfterWait(Job);
                    break;
                case "PutAndWait":
                    PutAndWait(Job);
                    break;
            }
        }

        public void Map(string Position)
        {
            LastJob = null;
            Command Cmd = new Command();
            Cmd.SetADR("1");
            Cmd.SetFLG("CMD");
            Cmd.SetCMD("MAP__");
            Cmd.SetDAT(Position + ",1,0");
            Cmd.Desc = "Map";
            SendCommand(Cmd);
        }

        public void GetMap()
        {
            LastJob = null;
            Command Cmd = new Command();
            Cmd.SetADR("1");
            Cmd.SetFLG("GET");
            Cmd.SetCMD("MAP__");
            Cmd.SetDAT("1");
            Cmd.Desc = "GetMap";
            SendCommand(Cmd);
        }

        public void Get(Job Job)
        {
            LastJob = Job;
            Command Cmd = new Command();
            Cmd.SetADR("1");
            Cmd.SetFLG("CMD");
            Cmd.SetCMD("GET__");
            Cmd.SetDAT(Job.From + "," + Job.Slot + ",1,0,0");
            Cmd.Desc="Get";
            SendCommand(Cmd);
        }

        public void GetAndWait(Job Job)
        {
            LastJob = Job;
            Command Cmd = new Command();
            Cmd.SetADR("1");
            Cmd.SetFLG("CMD");
            Cmd.SetCMD("GET__");
            Cmd.SetDAT(Job.From + ",0,1,0,1");
            Cmd.Desc = "GetAndWait";
            SendCommand(Cmd);
        }

        public void GetAfterWait(Job Job)
        {
            LastJob = Job;
            Command Cmd = new Command();
            Cmd.SetADR("1");
            Cmd.SetFLG("CMD");
            Cmd.SetCMD("GET__");
            Cmd.SetDAT(Job.From + ","+Job.Slot+",1,0,3");
            Cmd.Desc = "GetAfterWait";
            SendCommand(Cmd);
        }

        public void Put(Job Job)
        {
            LastJob = Job;
            Command Cmd = new Command();
            Cmd.SetADR("1");
            Cmd.SetFLG("CMD");
            Cmd.SetCMD("PUT__");
            Cmd.SetDAT(Job.To + "," + Job.ToSlot + ",1,0");
            Cmd.Desc = "Put";
            SendCommand(Cmd);
        }

        public void PutAndWait(Job Job)
        {
            LastJob = Job;
            Command Cmd = new Command();
            Cmd.SetADR("1");
            Cmd.SetFLG("CMD");
            Cmd.SetCMD("PUT__");
            Cmd.SetDAT(Job.To + "," + Job.ToSlot + ",1,2");
            Cmd.Desc = "Put";
            SendCommand(Cmd);
        }

        void IController.Connect()
        {
            cmdSck = new SocketClient(IP, Convert.ToInt16(Port), ControllerName, this);
        }

        void IController.Close()
        {
            if (cmdSck != null)
            {
                cmdSck.Close();
            }
        }

        void ISocketMessage.OnSocketMessage(string Msg)
        {
            try
            {
                string[] MsgList = Msg.Split('\n');

                foreach (string each in MsgList)
                {
                    //logger.Debug("OnSocketMessage:" + each);
                    if (each.Trim().Equals(""))
                    {
                        continue;
                    }
                    ReturnMsg eachMsg = new ReturnMsg(each);
                    timeOutTimer.Enabled = false;
                    switch (eachMsg.GetFLG())
                    {
                        case "ACK":

                            if (LastSendCommand.GetFLG().Equals("CMD"))//如果送出的指令不是CMD，就做下一步，否則必須等待FIN才能繼續
                            {
                                SetStatus(Runing);
                                actionTimeOutTimer.Enabled = true;
                                tObj.On_Command_Excuted(ControllerName, eachMsg, LastSendCommand, LastJob);
                            }
                            else
                            {
                                SetStatus(Idle);
                                tObj.On_Command_Excuted(ControllerName, eachMsg, LastSendCommand, LastJob);
                            }

                            break;
                        case "NAK":
                            SetStatus(Idle);
                            tObj.On_Command_Error(ControllerName, eachMsg, LastSendCommand, LastJob);
                            //錯誤發生
                            logger.Error("Error happen:error code=" + eachMsg.GetDAT());
                            break;
                        case "FIN":
                            actionTimeOutTimer.Enabled = false;
                            SetStatus(Idle);
                            //下一步
                            if (eachMsg.GetDAT().Equals("00000000"))
                            {
                                tObj.On_Command_Finished(ControllerName, eachMsg, LastSendCommand, LastJob);
                            }
                            else
                            {
                                //錯誤發生
                                tObj.On_Command_Error(ControllerName, eachMsg, LastSendCommand, LastJob);
                                logger.Error("Error happen:error code=" + eachMsg.GetDAT());
                            }

                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                logger.Error("OnSocketMessage:" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        void ISocketMessage.OnConnected()
        {
            StatusLock.EnterWriteLock();

            Status = Idle;

            StatusLock.ExitWriteLock();
            tObj.On_Status_Changed(ControllerName, GetStatus());

        }

        void ISocketMessage.OnError()
        {
            cmdSck.Close();
            SetStatus(Disconnected);


        }

        void ISocketMessage.OnConnecting()
        {
            SetStatus(Connecting);

        }

        void IController.SetReportTarget(ICommandReport target)
        {
            tObj = target;
        }
    }
}
