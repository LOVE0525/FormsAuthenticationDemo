using System.Web;
using System.Web.Mvc;
using FormsAuthenticationDemo.App_Start;

namespace FormsAuthenticationDemo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new RequestAuthorizeAttribute());
        }
    }
}