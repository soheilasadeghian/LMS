using LMSPricing.Areas.admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSPricing.Areas.admin.ClassCollection
{
    public class Method
    {
        public static bool isAdminLogin(HttpContextBase context)
        {

            if (context.Session["admin9652"] != null )
            {
                return true;
            }

            return false;
        }
        public static bool isAdminLogin(HttpContext context)
        {

            if (context.Session["admin9652"] != null)
            {
                return true;
            }

            return false;
        }
        public static User getAdmin(HttpContext context)
        {
            return context.Session["admin9652"] as User;
        }
        public static User getAdmin(HttpContextBase context)
        {
            return context.Session["admin9652"] as User;
        }
    }
}