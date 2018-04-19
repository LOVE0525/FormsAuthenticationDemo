using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using FormsAuthenticationDemo.Models;
using Newtonsoft.Json;

namespace FormsAuthenticationDemo.App_Start
{
    public class HttpFormsAuthentication
    {
        /// <summary>
        /// 设置授权Cookie
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userData"></param>
        /// <param name="rememberDays"></param>
        public static void SetAuthenticationCookie(string userName, IUserData userData, double rememberDays = 0)
        {
            //保存在cookie中的信息
            string userJson = JsonConvert.SerializeObject(userData);
            //创建用户票据
            double tickekDays = rememberDays == 0 ? 7 : rememberDays;
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddDays(rememberDays), false, userJson);
            //设置票据到Cookie中
            SetAuthCookie(ticket);
        }
        /// <summary>
        /// 设置票据到Cookie中
        /// </summary>
        /// <param name="ticket">票据</param>
        private static void SetAuthCookie(FormsAuthenticationTicket ticket)
        {
            //利用FormsAuthentication提供web forms身份验证服务
            string encryptValue = FormsAuthentication.Encrypt(ticket);

            //创建Cookie ，票据加密后写到Cookie中
            HttpCookie authcookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptValue);
            authcookie.Expires = ticket.Expiration;
            authcookie.HttpOnly = true;
            authcookie.Domain = FormsAuthentication.CookieDomain;
            HttpContext.Current.Response.Cookies.Remove(authcookie.Name);
            HttpContext.Current.Response.Cookies.Add(authcookie);
        }

        /// <summary>
        /// 尝试从Cookie中解析票据
        /// </summary>
        /// <typeparam name="TUserData"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Principal TryParsePrincipal<TUserData>(HttpContext context)where TUserData : IUserData
        {
            FormsAuthenticationTicket ticket = GetAuthTicket(context);
            IUserData userData = JsonConvert.DeserializeObject<TUserData>(ticket.UserData);
            return new Principal(ticket, userData);
        }
        /// <summary>
        /// 重新分发票据和身份认证的失效时间
        /// </summary>
        /// <param name="context"></param>
        public static void TryRenewAuthCookieExpires(HttpContext context)
        {
            FormsAuthenticationTicket ticket = GetAuthTicket(context);
            ticket = FormsAuthentication.RenewTicketIfOld(ticket);
            SetAuthCookie(ticket);
        }
        /// <summary>
        /// 获取身份验证Ticket
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static FormsAuthenticationTicket GetAuthTicket(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpCookie cookie = request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
            {
                return null;
            }
            //解密cookie值
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
            if (ticket == null || string.IsNullOrEmpty(ticket.UserData))
            {
                return null;
            }
            return ticket;

        }
    }
}