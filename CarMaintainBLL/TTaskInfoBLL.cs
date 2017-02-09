using CarMaintainCommon;
using CarMaintainDAL;
using CarMaintainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMaintainBLL
{
    public class TTaskInfoBLL
    {
        public static int AddTask(ref TTaskInfo ti, ref TCarInfo ci)
        {
            return TTaskInfoDAL.AddTask(ref ti, ref ci);
        }

        public static int AddTask(string carNo,string MaintainTypes, string technicians, string carName,
            string carOwner, string carOwnerTel1, string carOwnerTel2, string carTrevalDistance, string OprtUserCode
            )
        {
            try
            {
                DateTime dt = DateTime.Now;

                TCarInfo tCi = new TCarInfo();
                tCi.CarID = 0;
                tCi.CarNumber = carNo;
                tCi.CarName = carName;
                tCi.CarOwner = carOwner;
                tCi.CarOwnerTel1 = carOwnerTel1;
                tCi.CarOwnerTel2 = carOwnerTel2;
                if (carTrevalDistance != null && carTrevalDistance != String.Empty)
                {
                    tCi.CarTrevalDistance = int.Parse(carTrevalDistance);
                }
                tCi.CarComeTime = dt;
                tCi.OprtUserCode = OprtUserCode;

                TTaskInfo tTi = new TTaskInfo();
                tTi.TaskID = 0;
                tTi.CarID = 0;
                tTi.BeginDate = dt;
                tTi.OprtUserCode = OprtUserCode;
                tTi.ListItems = new List<TTaskItem>();
                if (MaintainTypes != String.Empty)
                {
                    String[] mtTypes = MaintainTypes.Split(',');
                    for (int i = 0; i < mtTypes.Length; i++)
                    {
                        //分配维修师傅
                        List<TTaskItemWorkers> listTw = new List<TTaskItemWorkers>();
                        if (technicians != String.Empty)
                        {
                            //获取可进行该维修类别的所有维修师傅ID列表
                            List<int> listmt = TUserInfoBLL.UGetMaintainUsers(int.Parse(mtTypes[i]));
                            String[] tns = technicians.Split(',');
                            foreach (var item in tns)
                            {
                                //判断循环中当前维修师傅，是否在上面的列表中                                
                                if (listmt.Contains(int.Parse(item)))
                                {
                                    listTw.Add(new TTaskItemWorkers()
                                    {
                                        TaskID = 0,
                                        TaskItemID = 0,
                                        UID = int.Parse(item)
                                    });
                                }                                 
                            }                            
                        }

                        tTi.ListItems.Add(new TTaskItem()
                                            { TaskID = 0,
                                            MID =int.Parse(mtTypes[i]),
                                            TaskItemID = i,
                                            BeginTime = dt,
                                            ListWorks = listTw
                                            }
                        );
                    }                                                                                   
                }

                return TTaskInfoBLL.AddTask(ref tTi, ref tCi);
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("AddTask", e);
                return 0;
            }
        }
    }
}
