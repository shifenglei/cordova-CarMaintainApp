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
    public class TUserGroupDAL
    {
        public static int UAddRecord(SqlConnection con, SqlTransaction sqt, TUserGroup ug)
        {
            try
            {
                string sql = "insert into tUserGroup(UID,GID) values (@UID,@GID)";
                return MsSqlHelper.ExecSqlUControl(con, sqt, sql,
                    new SqlParameter("@UID",ug.UID),
                    new SqlParameter("@GID",ug.GID)
                    );

            }
            catch(Exception e)
            {
                LogHelper.WriteLog("TUserGroupDAL.UAddRecord", e);
                return -1;
            }
        }
    }
}
