using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace MovieShopWepApp.Models
{
    public class LogoutModel
    {
        public HttpSessionState logout()
        {
            var current = HttpContext.Current.Session;
            current.Clear();
            current.Abandon();

            return current.Contents;
        }
    }
}