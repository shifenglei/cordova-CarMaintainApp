using CarMaintainBLL;
using CarMaintainCommon;
using CarMaintainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarMaintain.Models
{
    public class UserInfoController : Controller
    {
        public class TErrData
        {
            public int code;
            public string errmsg;
        }
        // GET: UserInfo
        public ActionResult Index()
        {

            ViewBag.ErrorMsg = "";
            if (Request["ErrMsg"] != null && Request["ErrMsg"] != String.Empty)
            {
                var data = JsonHelper.DeserializeJsonToObject<TErrData>(Request["ErrMsg"]);
                ViewBag.ErrorMsg = data.errmsg;
            }
            return View();
        }
        [HttpPost]
        public ActionResult ULogin()
        {
            try
            {
                string sUCode = Request["UserCode"];
                string sUPwd = Request["Password"];

                TUserInfo uinfo = TUserInfoBLL.ULogin(sUCode, sUPwd);
                JsonResult jr = new JsonResult();
                if (uinfo == null)
                {
                    jr.Data = new { code = -1, errmsg = "用户名或密码错误" };
                    var url = "~/UserInfo/Index?ErrMsg=" + JsonHelper.SerializeObject(jr.Data);
                    Server.TransferRequest(url, true);
                    return new EmptyResult();
                }
                else
                {
                    jr.Data = new { code = 1, userInfo = uinfo };
                    var url = "~/MainMenu/Index?userInfo=" + JsonHelper.SerializeObject(uinfo);

                    Server.TransferRequest(url, true);
                    //return RedirectToAction("Index", "MainMenu");
                    return new EmptyResult();
                }
                //RedirectToAction("Index", "MainMenu");

                //return jr;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("ULogin", e);
                return Content(JsonHelper.SerializeObject(new { code = -1, errmsg = "非常抱歉，登录失败" }));
            }
        }

        public ActionResult UEditUserInfo()
        {
            string userjson = Request["userInfo"];
            TUserInfo userdata = null;
            if ((userjson == null) || (userjson == String.Empty))
            {
                if (Request["UserCode"] == null || Request["UserCode"] == String.Empty ||
                    Request["Password"] == null || Request["Password"] == String.Empty
                    )
                {
                    return RedirectToAction("Index", "UserInfo");
                }
                else
                {
                    string sUCode = Request["UserCode"];
                    string sUPwd = Request["Password"];

                    userdata = TUserInfoBLL.ULogin(sUCode, sUPwd);
                }

            }
            else
                userdata = JsonHelper.DeserializeJsonToObject<TUserInfo>(Request["userInfo"]);
            //判断是否有管理员权限
            bool IsCanEditInfo = false;
            foreach (var item in userdata.Usergroups)
            {
                if (item.GType == 1 )
                {
                    IsCanEditInfo = true;
                }
            }
            if (IsCanEditInfo == false)
            {
                return Content(JsonHelper.SerializeObject(new { code = -1, errmsg = "无该操作权限" }));
            }

            return View();
        }
    }
}