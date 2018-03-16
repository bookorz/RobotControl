using log4net;
using robotTest.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace robotTest
{
    class SocketClient: IConnection
    {
        ILog logger = LogManager.GetLogger(typeof(SocketClient));
        Socket SckSPort; // 先行宣告Socket
        
        string RmIp = "192.168.0.127";  // 其中 xxx.xxx.xxx.xxx 為Server端的IP

        int SPort = 23;

        int RDataLen = 100;  //固定長度傳送資料~ 可以針對自己的需要改長度 


        string Desc = "";

        IConnectionReport tObj;

        public SocketClient(string IP, int Port,string Decription, IConnectionReport _tObj)
        {
            RmIp = IP;
            SPort = Port;
            tObj = _tObj;
           
           
            Desc = Decription;
        }

        // 連線

        public void Connect()
        {
            Thread SckTd = new Thread(ConnectServer);
            SckTd.IsBackground = true;
            SckTd.Start();
        }

        private void ConnectServer()

        {

            try

            {
                
                SckSPort = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                SckSPort.Connect(new IPEndPoint(IPAddress.Parse(RmIp), SPort));

                // RmIp和SPort分別為string和int型態, 前者為Server端的IP, 後者為Server端的Port

                if (!SckSPort.Connected)
                {
                    tObj.OnError();
                    logger.Error("Connect to " + RmIp + ":" + SPort + " Fail!");
                    return;
                }
                else
                {
                    logger.Info("Connected! "+ RmIp+":"+ SPort);
                    tObj.OnConnected();
                }

                // 同 Server 端一樣要另外開一個執行緒用來等待接收來自 Server 端傳來的資料, 與Server概念同

                Thread SckSReceiveTd = new Thread(SckSReceiveProc);
                SckSReceiveTd.IsBackground = true;
                SckSReceiveTd.Start();

            }
            catch (Exception e){
                logger.Error("(ConnectServer " + Desc + " " + RmIp + ":" + SPort + ")" + e.Message + "\n" + e.StackTrace);
                tObj.OnError();
            }

        }


        private void SckSReceiveProc()
        {

            try
            {

                int IntAcceptData;

                byte[] clientData = new byte[RDataLen];

                while (true)
                {
                    if (!SckSPort.Connected)
                    {
                        logger.Error(Desc+ " (" + RmIp + ":" + SPort + ") is disconnected.");
                        break;
                    }
                    // 程式會被 hand 在此, 等待接收來自 Server 端傳來的資料

                    IntAcceptData = SckSPort.Receive(clientData);

                    // 往下就自己寫接收到來自Server端的資料後要做什麼事唄~^^”
                    
                    string S = Encoding.Default.GetString(clientData,0, IntAcceptData);
                    //Console.WriteLine(S);
                    logger.Info( "[Rev<--]" + S.Replace("\n","")+ "(From "+ Desc + " " + RmIp + ":" + SPort +")");
                    tObj.OnSocketMessage(S);
                }

            }

            catch (Exception e)
            {
                logger.Error("(From " + Desc + " " + RmIp + ":" + SPort + ")" + e.Message + "\n" + e.StackTrace);
                tObj.OnError();
            }
        }


        // 當然 Client 端也可以傳送資料給Server端~ 和 Server 端的SckSSend一樣, 只差在Client端只有一個Socket

        public void SckSSend(string Msg)

        {
            try

            {

                //SckSPort.Send(Msg);
                logger.Info("[Snd-->]" + Msg.Replace("\r","") + "(To " + Desc + " " + RmIp + ":" + SPort + ")");
                byte[] t = new byte[Encoding.ASCII.GetByteCount(Msg)]; ;
                int i = Encoding.ASCII.GetBytes(Msg, 0, Encoding.ASCII.GetByteCount(Msg), t, 0);
                if (SckSPort.Connected == true) { 
                SckSPort.Send(t);
            }
            }

            catch (Exception e)
            {
                logger.Error("(To " + Desc + " " + RmIp + ":" + SPort + ")" + e.Message + "\n" + e.StackTrace);
                tObj.OnError();
            }





        }

        public void Close()
        {
            if (SckSPort != null)
            {
                
                SckSPort.Close();


            }
        }
    }
}
