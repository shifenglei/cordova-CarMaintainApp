using CarMaintainDAL;
using CarMaintainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMaintainBLL
{
    public class TGroupInfoBLL
    {
        public static List<TGroupInfo> UGetGroupList()
        {
            return TGroupInfoDAL.UGetGroupList();
        }
    }
}
