using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FormsAuthenticationDemo.App_Start;
using FormsAuthenticationDemo.Models;

namespace FormsAuthenticationDemo.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        //
        // GET: /Login/

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login()
        {
           
            string userName = HttpContext.Request["userName"];
            string pwd = HttpContext.Request["PWD"];
            string rememberDays=HttpContext.Request["rememberDays"];
            if (userName == "4545" && pwd == "123")
            {
                UserData ud = new UserData();
                ud.UserName = "hahahh";
                ud.UserId = "78787";
                ud.Phone = "18611347259";
                HttpFormsAuthentication.SetAuthenticationCookie("刘飞", ud, 5);

             
            }
            else
            {
               
            }
            return new RedirectResult("/home/index");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return new RedirectResult(FormsAuthentication.LoginUrl);
        }
       
    }
}
