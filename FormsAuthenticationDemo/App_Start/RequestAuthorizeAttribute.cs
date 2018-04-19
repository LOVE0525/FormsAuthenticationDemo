using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FormsAuthenticationDemo.Models;

namespace FormsAuthenticationDemo.App_Start
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequestAuthorizeAttribute : AuthorizeAttribute
    {
        
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //是否匿名访问
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), false))
            {
              return ;
            }

            Principal principal = filterContext.HttpContext.User as Principal;
            if (principal == null)
            {
                SetUnAuthorizedResult(filterContext);
                HandleUnauthorizedRequest(filterContext);
                return;
            }
            //续租Cookie
            HttpFormsAuthentication.TryRenewAuthCookieExpires(HttpContext.Current);
            //权限验证

            //.....

            base.OnAuthorization(filterContext);
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
           return base.AuthorizeCore(httpContext);
        }

       
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.Result != null)
            {
                return;
            }
            base.HandleUnauthorizedRequest(filterContext);
        }

        //验证不通过时
        private void SetUnAuthorizedResult(AuthorizationContext context)
        {
            HttpRequestBase request = context.HttpContext.Request;
            if (request.IsAjaxRequest())
            {
                //处理ajax请求
                context.Result = new ContentResult() { Content = "" };
            }
            else
            {
                //跳转到登录页面
                string loginUrl = FormsAuthentication.LoginUrl + "?ReturnUrl=" + "";
                context.Result = new RedirectResult(loginUrl);
            }
        }
    }
}