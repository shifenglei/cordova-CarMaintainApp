using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMaintainModels
{
    public class TMaintainType
    {
        private Int32 mid;
        private string mname;
        private string remark;

        public int Mid
        {
            get
            {
                return mid;
            }

            set
            {
                mid = value;
            }
        }

        public string Mname
        {
            get
            {
                return mname;
            }

            set
            {
                mname = value;
            }
        }

        public string Remark
        {
            get
            {
                return remark;
            }

            set
            {
                remark = value;
            }
        }
    }
}
