using log4net;
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
        ILog logger = LogManager.GetLogger(typeof(Command));
        string IP = "";
        int Port = 0;
        string ControllerName = "";
        Command LastSendCommand;
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
            tObj.On_Command_TimeOut(ControllerName, LastSendCommand);

        }

        private void ActionTimeOutMonitor(object sender, System.Timers.ElapsedEventArgs e)
        {
            actionTimeOutTimer.Enabled = false; SetStatus(Idle);
            logger.Error("Action time out! Send to:" + IP + ":" + Port + " Command:" + LastSendCommand);
            tObj.On_Command_TimeOut(ControllerName, LastSendCommand);

        }


        bool IController.SendCommand(Command command)
        {
            bool result = false;
            try
            {
                if (!Status.Equals(Waiting))
                {
                    SetStatus(Waiting);
                    LastSendCommand = command;
                    if (!command.FLG.Equals("DELAY"))
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
            tObj.On_Command_Finished(ControllerName, null, LastSendCommand);
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

                            if (LastSendCommand.FLG.Equals("CMD"))//如果送出的指令不是CMD，就做下一步，否則必須等待FIN才能繼續
                            {
                                SetStatus(Runing);
                                actionTimeOutTimer.Enabled = true;
                                tObj.On_Command_Excuted(ControllerName, eachMsg, LastSendCommand);
                            }
                            else
                            {
                                SetStatus(Idle);
                                tObj.On_Command_Finished(ControllerName, eachMsg, LastSendCommand);
                            }

                            break;
                        case "NAK":
                            SetStatus(Idle);
                            tObj.On_Command_Error(ControllerName, eachMsg, LastSendCommand);
                            //錯誤發生
                            logger.Error("Error happen:error code=" + eachMsg.GetDAT());
                            break;
                        case "FIN":
                            actionTimeOutTimer.Enabled = false;
                            SetStatus(Idle);
                            //下一步
                            if (eachMsg.GetDAT().Equals("00000000"))
                            {
                                tObj.On_Command_Finished(ControllerName, eachMsg, LastSendCommand);
                            }
                            else
                            {
                                //錯誤發生
                                tObj.On_Command_Error(ControllerName, eachMsg, LastSendCommand);
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
