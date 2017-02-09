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
    public class MainMenuController : Controller
    {
        // GET: MainMenu
        public ActionResult Index()
        {
            string userjson = Request["userInfo"];
            TUserInfo userdata = null;
            if ((userjson == null) || (userjson == String.Empty)){
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

            if (userdata == null)
            {
                return RedirectToAction("Index", "UserInfo");
            }
            else
            {
                bool bSaveUser = false;
                HttpCookie pwdCookie = null;
                HttpCookie uerCodeCookie = null;
                HttpCookie remberCookie = null;
                if (Request.Cookies["UserCode"] != null)
                {
                    uerCodeCookie = Request.Cookies["UserCode"];
                }
                if (Request.Cookies["rememberUser"] != null)
                {
                    remberCookie = Request.Cookies["rememberUser"];
                    bSaveUser = remberCookie.Value == "true";
                }
                if (Request.Cookies["Password"] != null)
                {
                    pwdCookie = Request.Cookies["Password"];
                }


                pwdCookie.Value = Security.UGetMd5Str(userdata.Uid.ToString() + userdata.Upassword);
                pwdCookie.Path = "/";                
                uerCodeCookie.Path = "/";                
                remberCookie.Path = "/";
                

                if (bSaveUser)
                {
                    pwdCookie.Expires = DateTime.Now.AddDays(15);
                    uerCodeCookie.Expires = DateTime.Now.AddDays(15);
                    remberCookie.Expires = DateTime.Now.AddDays(15);
                }
                else
                {
                    pwdCookie.Expires = DateTime.Now.AddDays(-1);
                    uerCodeCookie.Expires = DateTime.Now.AddDays(-1);
                    remberCookie.Expires = DateTime.Now.AddDays(-1);
                }
                Response.Cookies.Add(pwdCookie);
                Response.Cookies.Add(uerCodeCookie);
                Response.Cookies.Add(remberCookie);
                
                return View();
            }
        }
    }
}