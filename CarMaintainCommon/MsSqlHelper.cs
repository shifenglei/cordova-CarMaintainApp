using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;



namespace CarMaintainCommon
{
    public static class MsSqlHelper
    {        
        public static string connectionString = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
        //public static string connectionString = String.Empty;

        //public static void setConnectionString(string scon)
        //{
        //    connectionString = scon;
        //}
        #region 执行SQL语句---未启用事务 ExecSql
        public static int ExecSql(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql,con))
                {
                    try
                    {
                        cmd.Parameters.AddRange(ps);
                        con.Open();
                        int rowcount = cmd.ExecuteNonQuery();
                        return rowcount;
                    }
                    catch (SqlException e)
                    {
                        con.Close();
                        throw e;
                    }  
                }

            }
        }
        #endregion

        #region 执行SQL语句---外部控制链接和事务 ExecSql
        public static int ExecSqlUControl(SqlConnection con, SqlTransaction pTran, string sql, params SqlParameter[] ps)
        {
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                try
                {
                    cmd.Transaction = pTran;
                    cmd.Parameters.AddRange(ps);                    
                    int rowcount = cmd.ExecuteNonQuery();
                    return rowcount;
                }
                catch (SqlException e)
                {
                    throw e;
                }
            }            
        }
        #endregion


        #region 执行带事务的SQL语句 ExecSql
        public static int ExecSql(SqlConnection pCon, SqlTransaction pTran, String sql, params SqlParameter[] ps)
        {
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                try
                {
                    cmd.Connection = pCon;
                    cmd.Transaction = pTran;
                    cmd.Parameters.AddRange(ps);
                    int rowCount = cmd.ExecuteNonQuery();
                    pTran.Commit();
                    return rowCount;
                }
                catch (SqlException e)
                {
                    //CLog.UWriteErrLog("ExecSql2", e.Message, cmd.CommandText);
                    pTran.Rollback();
                    throw e;
                }
                
            }
        }
        #endregion

        #region 执行一条计算查询结果语句，返回查询结果GetSingle

        public static object GetSingle(string SqlStr, params SqlParameter[] ps)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SqlStr,con))
                {
                    con.Open();
                    try
                    {
                        cmd.Parameters.AddRange(ps);
                        object obj = cmd.ExecuteScalar();

                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (SqlException e)
                    {
                        //CLog.UWriteErrLog("GetSingle", e.Message, cmd.CommandText);
                        con.Close();
                        throw e;
                    }
                }
            }
        }

        #endregion

        #region 执行一条计算查询结果语句，返回查询结果GetSingle -- 使用外部SqlConnection
        public static object GetSingle(SqlConnection con, SqlTransaction pTran, string SqlStr, params SqlParameter[] ps)
        {
            using (SqlCommand cmd = new SqlCommand(SqlStr, con))
            {                
                try
                {
                    cmd.Transaction = pTran;
                    cmd.Parameters.AddRange(ps);
                    object obj = cmd.ExecuteScalar();

                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (SqlException e)
                {
                    //CLog.UWriteErrLog("GetSingle", e.Message, cmd.CommandText);                        
                    throw e;
                }
            }
            
        }
        #endregion

        #region 执行SQL语句返回SqlDataReader  ExecReader  -- 注：方法调用完毕后，必须关闭 SqlDataReader

        public static SqlDataReader ExecReader(string sql)
        {
            SqlConnection con = new SqlConnection(connectionString);
	        
                using(SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        return reader;
                    }
                    catch (SqlException e)
                    {
                        con.Close();
                        throw e;
                    }
                }
	        
        }
        #endregion

        public static SqlDataReader ExecReader(string sql, params SqlParameter[] ps)
        {
            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand(sql, con);
                                    
            con.Open();
            try
            {
                cmd.Parameters.AddRange(ps);
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch (SqlException e)
            {
                throw e;
            }
                
            
        }

        #region 执行SQL语句返回DataSet  ExecDataSet
        public static DataSet ExecDataSet(string sql,params SqlParameter[] ps)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                try
                {
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sql,con);
                    da.SelectCommand.Parameters.AddRange(ps);
                    da.Fill(ds,"ds");

                    return ds;
                }
                catch (SqlException e)
                {
                    con.Close();
                    throw e;
                }    
               
            }
        }
        #endregion


    }
}
