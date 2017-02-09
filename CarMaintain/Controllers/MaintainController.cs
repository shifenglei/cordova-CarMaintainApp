using CarMaintainBLL;
using CarMaintainCommon;
using CarMaintainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarMaintain.Controllers
{
    public class MaintainController : Controller
    {
        // GET: Maintain
        public ActionResult EditInfo()
        {
            List<TMaintainType> lm = new List<TMaintainType>();
            List<TUserInfo> lu = new List<TUserInfo>();

            lm = TMaintainTypeBLL.UGetMaintainTypes(String.Empty);
            lu = TUserInfoBLL.UGetUserInfosByCondition("where UState = 1 and UID in (select distinct UID from tUserGroup where GID in (100002,100003,100004))");

            ViewBag.maintainType = lm;
            ViewBag.technicians = lu;
            return View();
        }
        [HttpPost]
        public ActionResult EditTask()
        {
            var jsonData = new JsonResult();            
            try
            {
                if (Request["carNo1"] == null || Request["carNo1"] == String.Empty ||
                    Request["carNo2"] == null || Request["carNo2"] == String.Empty)
                {
                    jsonData.Data = JsonHelper.SerializeObject(new { status = -1, ErrMsg = "车牌号为空" });
                    return jsonData;
                }

                if (Request["MaintainTypes"] == null || Request["MaintainTypes"] == String.Empty)
                {
                    jsonData.Data = JsonHelper.SerializeObject(new { status = -1, ErrMsg = "维修类别为空" });
                    return jsonData;
                }

                if (Request["technicians"] == null || Request["technicians"] == String.Empty)
                {
                    jsonData.Data = JsonHelper.SerializeObject(new { status = -1, ErrMsg = "维修技师为空" });
                    return jsonData;
                }

                if (TTaskInfoBLL.AddTask(Request["carNo1"]+ Request["carNo2"], Request["MaintainTypes"],
                    Request["technicians"], Request["carName"], Request["carOwner"], Request["carOwnerTel1"],
                    Request["carOwnerTel2"], Request["carTrevalDistance"], Request["UserCode"]
                    ) > 0)
                {
                    jsonData.Data = JsonHelper.SerializeObject(new { status = 1 });
                    return jsonData;
                }
                else
                {
                    jsonData.Data = JsonHelper.SerializeObject(new { status = -1, ErrMsg = "添加任务失败" });
                    return jsonData;
                }              
            }
            catch(Exception e)
            {
                jsonData.Data = JsonHelper.SerializeObject(new { status= -1, ErrMsg = e.Message});
                return jsonData;
            }
        }

    }

    
}