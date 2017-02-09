using CarMaintainDAL;
using CarMaintainModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMaintainBLL
{
    public class TMaintainTypeBLL
    {
        public static List<TMaintainType> UGetMaintainTypes(string wheres, params SqlParameter[] pms)
        {
            return TMaintainTypeDAL.UGetMaintainTypes(wheres, pms);
        }
    }
}
