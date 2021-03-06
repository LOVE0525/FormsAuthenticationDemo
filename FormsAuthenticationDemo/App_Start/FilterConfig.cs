﻿using System.Web;
using System.Web.Mvc;
using FormsAuthenticationDemo.App_Start;

namespace FormsAuthenticationDemo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //添加全局过滤
            filters.Add(new RequestAuthorizeAttribute());
        }
    }
}