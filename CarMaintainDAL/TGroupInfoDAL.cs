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
    public class TGroupInfoDAL
    {
        public static List<TGroupInfo> UGetGroupList()
        {
            SqlDataReader sdr = null;
            List<TGroupInfo> lstGI = new List<TGroupInfo>();
            try
            {
                string sql = "select * from tGroupInfo";
                sdr = MsSqlHelper.ExecReader(sql);
                while (sdr.Read())
                {
                    TGroupInfo gi = new TGroupInfo()
                    {
                        GID = sdr.GetInt32(sdr.GetOrdinal("GID")),
                        GName = sdr["GName"].ToString(),
                        GType = sdr.GetInt32(sdr.GetOrdinal("GType"))
                    };
                    lstGI.Add(gi);
                }
                return lstGI;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("TGroupInfoDAL.UGetGroupList", e);
                return null;
            }
            finally
            {
                if (sdr != null)
                    sdr.Close();
            }
        }
    }
}
