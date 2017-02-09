using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMaintainModels
{
    public class TTaskItem
    {
        public int TaskItemID;
        public int TaskID;
        public int MID;
        public DateTime? BeginTime;
        public DateTime? EndTime;
        public int Manhour;
        public int TaskQuality;

        public List<TTaskItemWorkers> ListWorks;
    }
}
