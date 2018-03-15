using log4net;
using robotTest.Base;
using robotTest.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotTest
{
    public class ReturnMsg
    {
        ILog logger = LogManager.GetLogger(typeof(ReturnMsg));
         string ADR { get; set; }
         string FLG { get; set; }
         string CMD { get; set; }
         string DAT { get; set; }
        
        public string GetADR()
        {
            return this.ADR;
        }
        public string GetFLG()
        {
            return this.FLG;
        }
        public string GetCMD()
        {
            return this.CMD;
        }
        public string GetDAT()
        {
            return this.DAT;
        }

        public ReturnMsg(string msg)
        {
            try
            {
                msg = msg.Replace("$", "").Replace("\r", "");


                ADR = msg.Substring(1, 1);

                string[] msgAry = msg.Substring(1).Split(':');
                if (msgAry.Length > 2)
                {
                    FLG = msgAry[0];
                    CMD = msgAry[1];
                    DAT = msgAry[2];
                }
                else if (msgAry.Length == 2)
                {
                    FLG = msgAry[0];
                    CMD = msgAry[1];
                }
            }
            catch (Exception ex)
            {
                logger.Error("Connect_Click:" + ex.Message + "\n" + ex.StackTrace);
            }

        }

    }
}
