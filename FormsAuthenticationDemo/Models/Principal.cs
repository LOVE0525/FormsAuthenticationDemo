using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace FormsAuthenticationDemo.Models
{
    public class Principal : IPrincipal
    {

        public IIdentity Identity
        {
            get;
            private set;
        }
        public IUserData UserData { get; set; }
        public bool IsInRole(string role)
        {
            return this.UserData.IsInRole(role);
        }
        public Principal(FormsAuthenticationTicket ticket, IUserData userData)
        {
            
            this.Identity = new FormsIdentity(ticket);
            this.UserData = userData;
        }
    }
 
    public interface IUserData
    {
        bool IsInRole(string role);
    }

    public class UserData : IUserData
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string Phone { get; set; }

        public bool IsInRole(string role)
        {
            return true;
        }
    }
}