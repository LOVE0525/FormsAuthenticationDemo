using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using FormsAuthenticationDemo.App_Start;
using FormsAuthenticationDemo.Models;

namespace FormsAuthenticationDemo
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //this.AuthenticateRequest += MvcApplication_AuthenticateRequest;
        }
        
        
        //从Cookie中解析票据信息
        void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpContext.Current.User = HttpFormsAuthentication.TryParsePrincipal<UserData>(HttpContext.Current);
        }
      
      
    }
}