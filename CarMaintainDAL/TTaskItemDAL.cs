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
    public class TTaskItemDAL
    {
        public static int AddTaskItem(SqlConnection con,SqlTransaction sqt, TTaskItem ti)
        {
            string sqlHead = String.Empty;
            int TaskItemId = 0;
            try
            {
                sqlHead = "insert into tTaskItem(TaskID,MID,BeginTime) values(@TaskID,@MID,@BeginTime)";
                SqlParameter[] pms = new SqlParameter[]
                {
                    new SqlParameter("@TaskID",ti.TaskID),
                    new SqlParameter("@MID",ti.MID),
                    new SqlParameter("@BeginTime",ti.BeginTime)
                };

                if (MsSqlHelper.ExecSqlUControl(con, sqt, sqlHead, pms) == 1)
                {
                    TaskItemId = Convert.ToInt32(MsSqlHelper.GetSingle(con, sqt, "select max(TaskItemID) as TaskItemID from tTaskItem where TaskID=@TaskID"
                        ,new SqlParameter("@TaskID", ti.TaskID)));

                    foreach (TTaskItemWorkers tw in ti.ListWorks)
                    {
                        tw.TaskID = ti.TaskID;
                        tw.TaskItemID = TaskItemId;

                        TTaskItemWorkersDAL.AddTaskItemWorker(con, sqt, tw);
                    }

                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("AddTaskItem" + "\r\n" + sqlHead, e);
                throw e;
            }
        }
    }
}
