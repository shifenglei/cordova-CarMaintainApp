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
    public class TCarInfoDAL
    {
        public static int AddCarInfo(SqlConnection con, SqlTransaction sqr, ref TCarInfo ci)
        {
            String sql = String.Empty;
            try
            {
                sql = "Insert into tCarInfo(CarName,CarNumber,CarTrevalDistance,CarComeTime,CarOwner,CarOwnerTel1,CarOwnerTel2,OprtUserCode)" +
                    "values(@CarName,@CarNumber,@CarTrevalDistance,@CarComeTime,@CarOwner,@CarOwnerTel1,@CarOwnerTel2,@OprtUserCode)";
                Int32 CarID = 0;
                SqlParameter[] pms = new SqlParameter[] {
                    new SqlParameter("@CarName",ci.CarName),
                    new SqlParameter("@CarNumber",ci.CarNumber),
                    new SqlParameter("@CarTrevalDistance",ci.CarTrevalDistance),
                    new SqlParameter("@CarComeTime",ci.CarComeTime),
                    new SqlParameter("@CarOwner",ci.CarOwner),
                    new SqlParameter("@CarOwnerTel1",ci.CarOwnerTel1),
                    new SqlParameter("@CarOwnerTel2",ci.CarOwnerTel2),
                    new SqlParameter("@OprtUserCode",ci.OprtUserCode)
                };
                if (MsSqlHelper.ExecSqlUControl(con, sqr, sql, pms) == 1)
                {

                    CarID = Convert.ToInt32(MsSqlHelper.GetSingle(con, sqr, "select max(CarID) as CarID from tCarInfo where OprtUserCode=@OprtUserCode",
                        new SqlParameter("@OprtUserCode", ci.OprtUserCode)));
                    ci.CarID = CarID;
                    return 1;
                }
                else
                {
                    return 0;
                }
                 
            }
            catch(Exception e)
            {
                LogHelper.WriteLog("AddCarInfo", e);
                return 0;
            }
        }
    }
}
