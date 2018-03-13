using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotTest
{
    class Command
    {
        ILog logger = LogManager.GetLogger(typeof(Command));

        public string Controller { get; set; }
        public string ADR { get; set; }
       // public string SEQ { get; set; }
        public string FLG { get; set; }
        public string CMD { get; set; }
        public string DAT { get; set; }       
        

        //public string GetADR()
        //{
        //    return this.ADR;
        //}

        //public string GetSEQ()
        //{
        //    return this.SEQ;
        //}
        //public string GetFLG()
        //{
        //    return this.FLG;
        //}
        //public string GetCMD()
        //{
        //    return this.CMD;
        //}
        //public string GetDAT()
        //{
        //    return this.DAT;
        //}
        //public string GetScriptNo()
        //{
        //    return this.ScriptNo;
        //}
        //public int GetScriptIdx()
        //{
        //    return this.ScriptIdx;
        //}

        //public Command SetParam(string _ScriptNo, int _ScriptIdx,string _ADR, string _SEQ, string _FLG, string _CMD, string _DAT)
        //{
        //    ScriptNo = _ScriptNo;
        //    ScriptIdx = _ScriptIdx;
        //    ADR = _ADR;
        //    SEQ = _SEQ;
        //    FLG = _FLG;
        //    CMD = _CMD;
        //    DAT = _DAT;
        //    return this;
        //}

        private string GetCheckSum(string input)
        {
            string result = "";
            try
            {
                byte[] byteAry = Encoding.ASCII.GetBytes(input);
                int sum = 0;
                foreach (byte each in byteAry)
                {
                    sum += each;
                }
                result = sum.ToString("X2");
                result = result.Substring(result.Length - 2);
            }
            catch (Exception ex)
            {
                logger.Error("GetCheckSum:" + ex.Message + "\n" + ex.StackTrace);
            }
            return result;
        }

        public string GetCommandStr()
        {
            string result = "";
            try
            {

                //string SUM = GetCheckSum(ADR + SEQ + FLG + CMD + DAT);
                //According to Sanwa-eng communication spec.
                if (this.DAT.Equals(""))
                {
                    result = "$" + this.ADR + this.FLG + ":" + this.CMD + "\r";
                }
                else
                {
                    result = "$" + this.ADR + this.FLG + ":" + this.CMD + ":" + this.DAT + "\r";
                }
            }
            catch (Exception ex)
            {
                logger.Error("GetCommand:" + ex.Message + "\n" + ex.StackTrace);
            }
            return result;
        }

    }
}
