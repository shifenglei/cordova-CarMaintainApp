using CarMaintainCommon;
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
    public class TUserInfoBLL
    {
        public static TUserInfo UGetUserInfoByCode(string ucode)
        {
            return TUserInfoDAL.UGetUserInfoByCode(ucode);
        }

        public static List<TUserInfo> UGetUserInfosByCondition(string wheres, params SqlParameter[] pms)
        {
            return TUserInfoDAL.UGetUserInfosByCondition(wheres, pms);
        }

        public static TUserInfo ULogin(string ucode, string upassword)
        {
            TUserInfo uinfo = TUserInfoDAL.UGetUserInfoByCode(ucode);
            if (uinfo == null)
                return null;
            //if (Security.Decode(uinfo.Upassword) != upassword)
               // return null;

            return uinfo;
        }

        public static List<int> UGetMaintainUsers(int MaintainTypeID)
        {
            return TUserInfoDAL.UGetMaintainUsers(MaintainTypeID);
        }

        public static int UAddUserInfo(TUserInfo ui)
        {
            return TUserInfoDAL.UAddUserInfo(ui);
        }

        public static int UAddUserInfo(string ucode, string uname, string upwd, string uidentitycardno,
            string utel1, string utel2, string uaddress, List<TGroupInfo> lstGI)
        {
            TUserInfo ui = new TUserInfo()
            {
                Uid = 0,
                Ucode = ucode,
                Uname = uname,
                Upassword = upwd,
                Uidentitycardno = uidentitycardno,
                Utel1 = utel1,
                Utel2 = utel2,
                Uaddress = uaddress,
                Usergroups = lstGI
            };

            return UAddUserInfo(ui);
        }

    }
}
