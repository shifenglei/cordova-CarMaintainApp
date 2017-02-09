using CarMaintainCommon;
using CarMaintainModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMaintainDAL
{
    public class TMaintainTypeDAL
    {
        public static List<TMaintainType> UGetMaintainTypes(string wheres, params SqlParameter[] pms)
        {
            string sql = "select * from tMaintainType ";
            SqlDataReader dr = null;
            List<TMaintainType> lsmaintypes = new List<TMaintainType>();
            try
            {
                if (pms != null && wheres != string.Empty)
                {
                    sql += wheres;
                    dr = MsSqlHelper.ExecReader(sql, pms);
                }
                else
                {
                    dr = MsSqlHelper.ExecReader(sql);
                }

                while (dr.Read())
                {
                    lsmaintypes.Add(new TMaintainType() {
                        Mid = dr.GetInt32(dr.GetOrdinal("MID")),
                        Mname = dr["MTypeName"].ToString() == null ? "" : dr["MTypeName"].ToString(),
                        Remark = dr["Remark"].ToString() == null ? "" : dr["Remark"].ToString()
                    });
                }
                return lsmaintypes;
            }
            catch(Exception e)
            {
                LogHelper.WriteLog("UGetMaintainTypes", e);
                return null;
            }
            finally
            {
                if (dr != null)
                    dr.Close();
            }
        }
    }
}
