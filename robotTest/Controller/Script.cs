using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotTest.Controller
{
    class Script
    {
        private int CurrentIdx = 0;
        private List<Command> CommandList = new List<Command>();

        public Script(List<Command> NewList)
        {
            CommandList = NewList;
            CurrentIdx = 0;
        }

        public Command Next()
        {
            int result = CurrentIdx;
            if (CurrentIdx <= CommandList.Count - 1)
            {
                CurrentIdx++;
                return CommandList[result];
            }
            else
            {
                CurrentIdx = 0;
                return null;
            }
        }

        
    }
}
