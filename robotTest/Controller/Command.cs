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
    public class Command
    {
        ILog logger = LogManager.GetLogger(typeof(Command));


        public string ADR { get; set; }
        // public string SEQ { get; set; }
        public string FLG { get; set; }
        public string CMD { get; set; }
        public string DAT { get; set; }
        public string Controller { get; set; }
        public string Desc { get; set; }
         
        public string GetController()
        {
            return this.Controller;
        }

        public void SetController(string _Controller)
        {
            this.Controller = _Controller;
        }
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

        public void SetADR(string _ADR)
        {
             this.ADR=_ADR;
        }
        public void SetFLG(string _FLG)
        {
            this.FLG = _FLG;
        }
        public void SetCMD(string _CMD)
        {
            this.CMD = _CMD;
        }
        public void SetDAT(string _DAT)
        {
            this.DAT = _DAT;
        }
       

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
