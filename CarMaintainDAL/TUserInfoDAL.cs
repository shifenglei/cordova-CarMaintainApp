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
    public class TUserInfoDAL
    {
        public static TUserInfo UGetUserInfoByCode(string ucode)
        {
            string ssql = "select * from tUserInfo where UCode=@UCode and UState=1";
            SqlParameter sdr = new SqlParameter("@UCode", ucode);
            SqlDataReader dr = null;

            SqlDataReader drGroupInfo = null;
            TUserInfo ui = null;
            try
            {
                dr = MsSqlHelper.ExecReader(ssql, sdr);
                DateTime? timenull = null;
                while (dr.Read())
                {
                    ui = new TUserInfo()
                    {
                        Uid = dr.GetInt32(dr.GetOrdinal("UID")),
                        Ucode = dr["UCode"].ToString() == null ? "" : dr["UCode"].ToString(),
                        Uname = dr["UName"].ToString() == null ? "" : dr["UName"].ToString(),
                        Upassword = dr["UPassword"].ToString() == null ? "" : dr["UPassword"].ToString(),
                        Uidentitycardno = dr["UIdentityCardNo"].ToString() == null ? "" : dr["UIdentityCardNo"].ToString(),
                        Utel1 = dr["UTel1"].ToString() == null ? "" : dr["UTel1"].ToString(),
                        Utel2 = dr["UTel2"].ToString() == null ? "" : dr["UTel2"].ToString(),
                        Uaddress = dr["UAddress"].ToString() == null ? "" : dr["UAddress"].ToString(),
                        Uphoto = dr["UPhoto"].ToString() == null ? "" : dr["UPhoto"].ToString(),
                        Ubrithday = dr["UBirthday"].ToString() == null ? "" : dr["UBirthday"].ToString(),
                        Uremark = dr["URemark"].ToString() == null ? "" : dr["URemark"].ToString(),
                        Uentrytime = dr.IsDBNull(dr.GetOrdinal("UEntryTime")) == true ? timenull : dr.GetDateTime(dr.GetOrdinal("UEntryTime")),
                        Ustate = dr.GetInt32(dr.GetOrdinal("UState"))
                    };
                }
                //查询用户所在用户组
                if (ui != null)
                {
                    ui.Usergroups = new List<TGroupInfo>();
                    string s1 = "select GID, (select GName from tGroupInfo b where a.GID = b.GID)  as GName" +
                                " (select GType from tGroupInfo b where a.GID = b.GID)  as GType" +
                                " from tUserGroup a where  UID = @UID";
                    SqlParameter sp = new SqlParameter("@UID", ui.Uid);
                    drGroupInfo = MsSqlHelper.ExecReader(s1, sp);

                    while (drGroupInfo.Read())
                    {
                        TGroupInfo gi = new TGroupInfo()
                        {
                            GID = drGroupInfo.GetInt32(drGroupInfo.GetOrdinal("GID")),
                            GName = drGroupInfo["GName"].ToString(),
                            GType = drGroupInfo.GetInt32(drGroupInfo.GetOrdinal("GType"))
                        };

                        ui.Usergroups.Add(gi);
                    }
                }
                return ui; 
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("UGetUserInfoByCode", e);
                return null;
            }
            finally
            {      
                if (dr != null)         
                    dr.Close();
                if (drGroupInfo != null)
                {
                    drGroupInfo.Close();
                }               
            }
        }

        public static List<TUserInfo> UGetUserInfosByCondition(string wheres, params SqlParameter[] pms)
        {
            string ssql = "select * from tUserInfo ";
            SqlDataReader dr = null;
            List<TUserInfo> listUser = new List<TUserInfo>();
            try
            {
                if (pms != null && wheres != String.Empty)
                {
                    ssql = ssql + wheres;
                    dr = MsSqlHelper.ExecReader(ssql, pms);
                }
                else
                {
                    dr = MsSqlHelper.ExecReader(ssql);
                }
                DateTime? timenull = null;
                while (dr.Read())
                {                    
                    listUser.Add(new TUserInfo()
                    {
                        Uid = dr.GetInt32(dr.GetOrdinal("UID")),
                        Ucode = dr["UCode"].ToString() == null ? "" : dr["UCode"].ToString(),
                        Uname = dr["UName"].ToString() == null ? "" : dr["UName"].ToString(),
                        Upassword = dr["UPassword"].ToString() == null ? "" : dr["UPassword"].ToString(),
                        Uidentitycardno = dr["UIdentityCardNo"].ToString() == null ? "" : dr["UIdentityCardNo"].ToString(),
                        Utel1 = dr["UTel1"].ToString() == null ? "" : dr["UTel1"].ToString(),
                        Utel2 = dr["UTel2"].ToString() == null ? "" : dr["UTel2"].ToString(),
                        Uaddress = dr["UAddress"].ToString() == null ? "" : dr["UAddress"].ToString(),
                        Uphoto = dr["UPhoto"].ToString() == null ? "" : dr["UPhoto"].ToString(),
                        Ubrithday = dr["UBirthday"].ToString() == null ? "" : dr["UBirthday"].ToString(),
                        Uremark = dr["URemark"].ToString() == null ? "" : dr["URemark"].ToString(),
                        //Uentrytime = dr.GetDateTime(dr.GetOrdinal("UEntryTime")) == null ? timenull : dr.GetDateTime(dr.GetOrdinal("UEntryTime")),
                        Uentrytime = dr.IsDBNull(dr.GetOrdinal("UEntryTime")) == true ? timenull : dr.GetDateTime(dr.GetOrdinal("UEntryTime")),
                        Ustate = dr.GetInt32(dr.GetOrdinal("UState"))
                    });
                }
                return listUser;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("UGetUserInfosByCondition", e);
                return null;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                }
            }
        }

        public static List<int> UGetMaintainUsers(int MaintainTypeID)
        {
            List<int> list = new List<int>();
            
            string sql = "SELECT UID FROM tUserGroup "+
                          "WHERE GID in "+
                          "(select GID from tGroupModuleJurisdiction where MID = @MID) ";
            SqlParameter spr = new SqlParameter("@MID", MaintainTypeID);
            SqlDataReader dr = null;
            try
            {
                dr = MsSqlHelper.ExecReader(sql, spr);
                while (dr.Read())
                {
                    list.Add(dr.GetInt32(dr.GetOrdinal("UID")));                   
                }
                return list;
            }
            catch(Exception e)
            {
                LogHelper.WriteLog("UGetUserInfoByCode"+"\r\n"+ sql, e);
                return null;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                }
            }
        }

        public static int UAddUserInfo(SqlConnection con, SqlTransaction sqt, TUserInfo ui)
        {
            try
            {
                string sql = "insert into tUserInfo(UCode,UName,UPassword,UIdentityCardNo,UTel1,UTel2,UAddress,UEntryTime,UState)" +
                    "values(@UCode,@UName,@UPassword,@UIdentityCardNo,@UTel1,@UTel2,@UAddress,GETDATE(),1)";
                int iRtn = MsSqlHelper.ExecSqlUControl(con, sqt, sql,
                    new SqlParameter("@UCode", ui.Ucode),
                    new SqlParameter("@UName", ui.Uname),
                    new SqlParameter("@UPassword", ui.Upassword),
                    new SqlParameter("@UIdentityCardNo", ui.Uidentitycardno),
                    new SqlParameter("@UTel1", ui.Utel1),
                    new SqlParameter("@UTel2", ui.Utel2),
                    new SqlParameter("@UAddress", ui.Uaddress)
                    );
                if (iRtn == 1)
                {
                    string sQuery = "select max(UID) as UID from tUserInfo where UState = 1";
                    return Convert.ToInt32(MsSqlHelper.GetSingle(con, sqt, sQuery));
                }
                else
                {
                    return -1;
                }
            }
            catch(Exception e)
            {
                LogHelper.WriteLog("TUserInfoDAL.UAddUserInfo", e);
                throw e;
            }
        }

        public static int UAddUserInfo(TUserInfo ui)
        {
            SqlConnection con = new SqlConnection(MsSqlHelper.connectionString);
            con.Open();
            SqlTransaction trn = con.BeginTransaction();
            try
            {
                int iUserId = UAddUserInfo(con, trn, ui);
                if (iUserId > 0)
                {
                    ui.Uid = iUserId;
                    foreach (var item in ui.Usergroups)
                    {
                        TUserGroup ug = new TUserGroup()
                        {
                            UID = ui.Uid,
                            GID = item.GID
                        };
                        if (TUserGroupDAL.UAddRecord(con, trn, ug) != 1)
                            throw new Exception("添加用户组失败");
                    }

                    trn.Commit();
                    con.Close();
                    con.Dispose();
                    return 1;
                }
                else
                {
                    throw new Exception("添加UserInfo出错");
                }
            }
            catch(Exception e)
            {
                LogHelper.WriteLog("TUserInfoDAL.UAddUserInfo1", e);
                trn.Rollback();
                con.Close();
                con.Dispose();
                return 0;
            }
        }
    }
}
