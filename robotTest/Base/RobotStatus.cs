using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotTest.Base
{
    class RobotStatus
    {
        //手臂真空狀態
        public bool R_ArmVacuumNo1 { get; set; }
        public bool R_ArmVacuumNo2 { get; set; }
        public bool R_ArmVacuumNo3 { get; set; }
        public bool R_ArmVacuumNo4 { get; set; }
        public bool L_ArmVacuumNo1 { get; set; }
        public bool L_ArmVacuumNo2 { get; set; }
        public bool L_ArmVacuumNo3 { get; set; }
        public bool L_ArmVacuumNo4 { get; set; }
        //手臂在席狀態
        public bool R_ArmPresentNo1 { get; set; }
        public bool R_ArmPresentNo2 { get; set; }
        public bool R_ArmPresentNo3 { get; set; }
        public bool R_ArmPresentNo4 { get; set; }
        public bool L_ArmPresentNo1 { get; set; }
        public bool L_ArmPresentNo2 { get; set; }
        public bool L_ArmPresentNo3 { get; set; }
        public bool L_ArmPresentNo4 { get; set; }
        //Encoder目前各軸位置
        public string R_EncoderPosition { get; set; }
        public string L_EncoderPosition { get; set; }
        public string S_EncoderPosition { get; set; }
        public string Z_EncoderPosition { get; set; }
        public string X_EncoderPosition { get; set; }
        public string R1_EncoderPosition { get; set; }
    }
}
