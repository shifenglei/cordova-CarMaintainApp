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
    public class TTaskInfoDAL
    {
        public static int AddTask(SqlConnection con,SqlTransaction sqt,ref TTaskInfo ti)
        {
            string sql = String.Empty;
            Int32 TaskID = 0;

            try
            {
                sql = "insert into tTaskHead(CarID,BeginDate,OprtUserCode) values (@CarID,@BeginDate,@OprtUserCode)";
                SqlParameter[] pms = new SqlParameter[]
                {
                    new SqlParameter("@CarID",ti.CarID),
                    new SqlParameter("@BeginDate", ti.BeginDate),
                    new SqlParameter("@OprtUserCode", ti.OprtUserCode)
                };

                if (MsSqlHelper.ExecSqlUControl(con, sqt, sql, pms) == 1)
                {
                    TaskID = Convert.ToInt32(MsSqlHelper.GetSingle(con, sqt, "select max(TaskID) as TaskID from tTaskHead where OprtUserCode=@OprtUserCode",new SqlParameter("@OprtUserCode",ti.OprtUserCode)));
                    ti.TaskID = TaskID;
                    foreach (var taskItem in ti.ListItems)
                    {
                        taskItem.TaskID = TaskID;
                        TTaskItemDAL.AddTaskItem(con, sqt, taskItem);
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
                LogHelper.WriteLog("TTaskInfoDAL.AddTask  1", e);
                throw e;
            }
        }

        public static int AddTask(ref TTaskInfo ti, ref TCarInfo ci)
        {
            SqlConnection con = new SqlConnection(MsSqlHelper.connectionString);
            con.Open();
            SqlTransaction st = con.BeginTransaction();
            try
            {
                if (TCarInfoDAL.AddCarInfo(con, st, ref ci) == 1)
                {
                    ti.CarID = ci.CarID;
                    if (TTaskInfoDAL.AddTask(con, st, ref ti) == 1)
                    {
                        st.Commit();
                        return 1;
                    }
                    else
                    {
                        throw new Exception("添加任务信息出错");
                    }
                }
                else
                {
                    throw new Exception("添加车辆信息出错");
                }                                
            }
            catch(Exception e)
            {
                LogHelper.WriteLog("TTaskInfoDAL.AddTask  2", e);
                st.Rollback();
                con.Close();
                con.Dispose();
                return 0;
            }
        }
    }
}
