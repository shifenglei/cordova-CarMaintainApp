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
    public class TTaskItemWorkersDAL
    {
        public static int AddTaskItemWorker(SqlConnection con,SqlTransaction sqt, TTaskItemWorkers tiw)
        {
            string sql = String.Empty;
            try
            {                
                sql = "insert into tTaskItemWorkers values(@TaskID,@TaskItemID,@UID,@Manhour)";

                SqlParameter[] pms = new SqlParameter[] {
                    new SqlParameter("@TaskID",tiw.TaskID),
                    new SqlParameter("@TaskItemID",tiw.TaskItemID),
                    new SqlParameter("@UID",tiw.UID),
                    new SqlParameter("@Manhour",tiw.Manhour)
                };

                return MsSqlHelper.ExecSqlUControl(con, sqt, sql, pms);
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("AddTaskItemWorker"+"\r\n"+ sql, e);
                throw e;
            }
        }
    }
}
