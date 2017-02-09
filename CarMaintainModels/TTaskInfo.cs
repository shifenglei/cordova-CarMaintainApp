using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMaintainModels
{
    public class TTaskInfo
    {
        public int TaskID;
        public int CarID;
        public DateTime? BeginDate;
        public DateTime? EndDate;
        public int Manhour;
        public double YsTotal;
        public double SsTotal;
        public string PhotosPath;
        public string OprtUserCode;

        public List<TTaskItem> ListItems;
    }
}
